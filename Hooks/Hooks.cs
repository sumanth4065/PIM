using AventStack.ExtentReports.Gherkin.Model;
using Microsoft.Playwright;
using NUnit.Framework;
using PIMQAAutomation.Helpers;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Support;
using PIMQAAutomation.Utilities;
using TechTalk.SpecFlow;

[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: LevelOfParallelism(8)]

namespace PIMQAAutomation.Hooks
{
    [Binding]
    public class Hooks : ExtentReport
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;
        private IBrowser _browser;
        private IBrowserContext _context;
        private IPlaywright _playwright;
        private readonly HttpClient _httpClient;
        private static bool _isDeleteApiCalled = false;
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private const string FeatureFailedFlag = "FeatureFailed";

        private string SkippedScenarioMessage => $"[SKIPPED] Scenario: '{_scenarioContext.ScenarioInfo.Title}' skipped due to a previous scenario failure.";

        private string _screenshotBase64 = string.Empty;
        private string _failureMessage = string.Empty;

        public Hooks(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
            _httpClient = new HttpClient();
        }

        [BeforeTestRun]
        public static async Task BeforeTestRun()
        {
            Console.WriteLine("Before test run");
            ExtentReportInit();

            var dbConnectionString = SecureDataHelper.DecryptWithMachineKey(TestContext.Parameters[RunSettingsConstants.DbConnectionString]);
            await DBClient.GetInstanceAsync(dbConnectionString);
            //DependencyInjection.SetupDI();
            Console.WriteLine("End of Before test run");
        }

        [AfterTestRun]
        public static async Task AfterTestRun()
        {
            Console.WriteLine("After test run...");
            ExtentReportTearDown();
            await DBClient.DisposeInstanceAsync();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            Console.WriteLine("Before feature...");
            _feature = _extentReports.CreateTest<Feature>(featureContext.FeatureInfo.Title);
        }

        [AfterFeature]
        public static async Task AfterFeature(FeatureContext featureContext)
        {
            Console.WriteLine("After feature...");
            var page = featureContext["Page"] as IPage;
            if (page != null)
            {
                await page.CloseAsync(); // Only close the page
            }

            // Optional: Dispose browser/context if feature is done
            var browser = featureContext["Browser"] as IBrowser;
            var context = featureContext["BrowserContext"] as IBrowserContext;
            if (context != null)
            {
                await context.CloseAsync();
            }

            if (browser != null)
            {
                await browser.CloseAsync();
            }
        }

        [BeforeScenario("UI")]
        public async Task BeforeScenarioUI()
        {
            SkipIfFeatureFailed();

            try
            {
                // Initialize Playwright only once per feature
                if (_playwright == null)
                {
                    _playwright = await Playwright.CreateAsync();
                    _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
                    _context = await _browser.NewContextAsync();

                    _featureContext["Browser"] = _browser;
                    _featureContext["BrowserContext"] = _context;
                }

                await StartScenarioTracing();

                var page = await _context.NewPageAsync();
                _scenarioContext["context"] = _context;
                _scenarioContext["page"] = page;
                _featureContext["Page"] = page;

                _scenario = _feature.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
            }
            catch (InconclusiveException) { throw; }
            catch (Exception ex) { Assert.Fail("Before Scenario Failed: " + ex.Message); }
        }

        [BeforeScenario("NoNewBrowser")]
        public async Task BeforeScenarioNoNewBrowser()
        {
            SkipIfFeatureFailed();
            try
            {
                if (!_scenarioContext.ContainsKey("context"))
                {
                    Console.WriteLine("Set context....");
                    _context = _featureContext["BrowserContext"] as IBrowserContext;
                }
                if (_context != null)
                {
                    await StartScenarioTracing();
                    _scenarioContext["context"] = _context;
                }

                _scenario = _feature.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
            }
            catch (InconclusiveException) { throw; }
            catch (Exception ex) { Assert.Fail("Before Scenario (NoNewBrowser) Failed: " + ex.Message); }
        }

        [AfterScenario]
        public async Task AfterScenario()
        {
            Console.WriteLine("After Scenario...");

            bool testSkipped = _featureContext.ContainsKey(FeatureFailedFlag) && (bool)_featureContext[FeatureFailedFlag];
            bool testFailed = _scenarioContext.TestError != null && !testSkipped;

            if (_scenarioContext.TestError != null)
            {
                _featureContext[FeatureFailedFlag] = true;
            }

            try
            {
                if (testSkipped)
                {
                    Console.WriteLine("Scenario skipped. Trace and defect creation skipped.");
                    return;
                }

                if (_context == null)
                {
                    Console.WriteLine("No context assigned");
                    return;
                }

                string tracePath = string.Empty;

                if (testFailed)
                {
                    tracePath = await CaptureTraceAsync(_context);
                    await CreateDefectIfRequired(tracePath);
                }
                else
                {
                    // Scenario passed: stop tracing without saving
                    await _context.Tracing.StopAsync();
                }
            }
            catch (Exception e)
            {
                Assert.Fail($"AfterScenario failed: {e.Message}");
            }
            finally
            {
                _failureMessage = string.Empty;
                _screenshotBase64 = string.Empty;
            }
        }

        [AfterStep]
        public async Task AfterStep()
        {
            var page = _featureContext["Page"] as IPage;
            var stepType = _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            var stepName = _scenarioContext.StepContext.StepInfo.Text;

            try
            {
                if (_scenarioContext.TestError != null)
                {
                    if (string.IsNullOrEmpty(_failureMessage))
                    {
                        _failureMessage = _scenarioContext.TestError.Message;
                        _failureMessage = _failureMessage.Replace("<", "&lt;").Replace(">", "&gt;");
                    }

                    _screenshotBase64 = await CaptureScreenshotAsync(page);

                    var failedStep = stepType switch
                    {
                        "Given" => _scenario.CreateNode<Given>(stepName),
                        "When" => _scenario.CreateNode<When>(stepName),
                        "Then" => _scenario.CreateNode<Then>(stepName),
                        _ => _scenario.CreateNode<And>(stepName)
                    };

                    if (!string.IsNullOrEmpty(_screenshotBase64))
                    {
                        failedStep.Fail(_failureMessage).AddScreenCaptureFromBase64String(_screenshotBase64, "Failed Step Screenshot");

                        // Remove special charactors from step def to make use as file name and Restrict file name length to 50 character 
                        var safeFile = string.Concat(stepName.Replace(" ", string.Empty).Where(c => !Path.GetInvalidFileNameChars().Contains(c)));
                        safeFile = safeFile.Length > 50 ? safeFile.Substring(0, 50) : safeFile;

                        var filePath = Path.Combine(Path.GetTempPath(), $"{safeFile}_{DateTime.Now:yyyyMMdd_HHmmss}.png");

                        File.WriteAllBytes(filePath, Convert.FromBase64String(_screenshotBase64));

                        TestContext.AddTestAttachment(filePath, "Failure Screenshot");
                    }
                    else
                    {
                        failedStep.Fail($"{_failureMessage} (- Failed to capture Screenshot)");
                    }
                }
                else
                {
                    switch (stepType.ToLower())
                    {
                        case "given": _scenario.CreateNode<Given>(stepName); break;
                        case "when": _scenario.CreateNode<When>(stepName); break;
                        case "then": _scenario.CreateNode<Then>(stepName); break;
                        default: _scenario.CreateNode<And>(stepName); break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AfterStep error: {ex.Message}");
            }
        }

        private void SkipIfFeatureFailed()
        {
            if (_featureContext.ContainsKey(FeatureFailedFlag) && (bool)_featureContext[FeatureFailedFlag])
            {
                Console.WriteLine(SkippedScenarioMessage);
                _scenario = _feature.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);

                _scenario.Skip(SkippedScenarioMessage);
                Assert.Ignore(SkippedScenarioMessage);
            }
        }

        private async Task StartScenarioTracing()
        {
            if (_context != null)
            {
                await _context.Tracing.StartAsync(new() { Screenshots = true, Snapshots = true, Sources = true });
                _context.SetDefaultTimeout(TimeConstants.DefaultTimeOut);
                _context.SetDefaultNavigationTimeout(TimeConstants.DefaultNavigationTimeOut);
            }
        }

        private static async Task<string> CaptureScreenshotAsync(IPage page)
        {
            if (page == null) return string.Empty;
            try
            {
                var bytes = await page.ScreenshotAsync(new PageScreenshotOptions { FullPage = true });
                return Convert.ToBase64String(bytes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Screenshot capture failed: {ex.Message}");
                return string.Empty;
            }
        }

        private async Task<string> CaptureTraceAsync(IBrowserContext context)
        {
            var createTraceLog = TestContext.Parameters[RunSettingsConstants.CreateTraceLog];
            if (string.IsNullOrEmpty(createTraceLog) || !createTraceLog.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                return string.Empty;
            }

            if (context == null) return string.Empty;

            try
            {
                string title = _scenarioContext.ScenarioInfo.Title;
                var safeTitle = string.Concat(title.Where(c => !Path.GetInvalidFileNameChars().Contains(c)));
                safeTitle = safeTitle.Length > 50 ? safeTitle.Substring(0, 50) : safeTitle;

                var traceDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestResults", "Traces");
                Directory.CreateDirectory(traceDir);

                var tracePath = Path.Combine(traceDir, $"{safeTitle}_trace.zip");
                await context.Tracing.StopAsync(new TracingStopOptions { Path = tracePath });
                Console.WriteLine($"Trace saved: {tracePath}");
                return tracePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Trace capture failed: {ex.Message}");
                return string.Empty;
            }
        }

        // <Summary> create Bug on Failure
        // Conditions:
        // 1. On Failure of scenario
        // 2. Check if existing Bug with same title already available or not
        // 3. Assign the bug to Specific Area and Current Iteration
        // </summary>
        private async Task CreateDefectIfRequired(string tracePath)
        {
            var createBug = TestContext.Parameters[RunSettingsConstants.CreateBug];
            if (!IsRunningInAzureDevOps() || string.IsNullOrEmpty(createBug) || !createBug.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            try
            {
                var bugCreation = new BugCreation();
                int? existingBug = await bugCreation.FindExistingBugAsync(_scenarioContext.ScenarioInfo.Title, _failureMessage);

                if (existingBug.HasValue)
                {
                    Console.WriteLine($"⚠️ Bug already exists for this scenario with ID {existingBug.Value}");
                    return;
                }
                string moduleName = Utility.GetModuleNameFromTags(_scenarioContext);
                int bugID = await bugCreation.CreateBugAsync(_scenarioContext.ScenarioInfo.Title, _failureMessage, _screenshotBase64, tracePath, moduleName);
                if (bugID > 0)
                {
                    Console.WriteLine($"Bug created with ID: {bugID}");
                }
                else
                {
                    Console.WriteLine($"Failed to create bug");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bug creation failed: {ex.Message}");
            }
        }

        private static bool IsRunningInAzureDevOps()
        {
            var tfBuild = Environment.GetEnvironmentVariable("TF_BUILD");
            return !string.IsNullOrEmpty(tfBuild) && tfBuild.Equals("true", StringComparison.OrdinalIgnoreCase);
        }

        public static async Task DeleteApiIfRequiredAsync(string token)
        {
            // Read pipeline variable from runsettings
            string cleanupEnabled = TestContext.Parameters[RunSettingsConstants.EnableAutomationDataCleanup];

            // If cleanup is disabled or no token, skip the operation
            if (_isDeleteApiCalled
                || string.IsNullOrEmpty(token)
                || string.IsNullOrEmpty(cleanupEnabled)
                || !cleanupEnabled.Equals("true", StringComparison.OrdinalIgnoreCase))
                return;

            await _semaphore.WaitAsync();
            try
            {
                if (_isDeleteApiCalled)
                    return;

                Console.WriteLine("Calling Delete API...");
                bool result = await ApiService.CleanAutomationData(token);

                if (!result)
                    Console.WriteLine("Warning: Delete API call failed. Proceeding with test run.");
                else
                    Console.WriteLine("Delete API called successfully.");

                _isDeleteApiCalled = true;
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}

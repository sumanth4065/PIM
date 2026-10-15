using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;

namespace PIMQAAutomation.Utilities
{
    public class ExtentReport
    {
        public static ExtentReports _extentReports;
        [ThreadStatic] public static ExtentTest _feature;
        [ThreadStatic] public static ExtentTest _scenario;
        [ThreadStatic] public static ExtentTest _test;
        public static string testResultPath; //= Path.Combine(AppContext.BaseDirectory.Replace("bin/Debug/net6.0", ""), "TestResults");

        // Initialize the Extent Reports
        public static void ExtentReportInit()
        {
            testResultPath = Path.Combine(AppContext.BaseDirectory.Replace("TestResults", ""), "TestResults");
            var htmlReporter = new ExtentHtmlReporter(Path.Combine(testResultPath, "index.html"));
            htmlReporter.Config.ReportName = "Automation Status Report";
            htmlReporter.Config.DocumentTitle = "Automation Status Report";
            htmlReporter.Config.Theme = Theme.Standard;
            htmlReporter.Start();

            _extentReports = new ExtentReports();
            _extentReports.AttachReporter(htmlReporter);
            _extentReports.AddSystemInfo("Application", "ParkPlace Information Management");
            _extentReports.AddSystemInfo("OS", Environment.OSVersion.Platform.ToString());
        }

        // Tear down the Extent Reports
        public static void ExtentReportTearDown()
        {
            _extentReports.Flush();
        }

        // Create a test with Test Case ID in the test name (only at the start)
        public static void CreateTestWithTestCaseId(string testCaseId, string testName)
        {
            // Create the test with the Test Case ID in the name (this will appear at the top of the report)
            _test = _extentReports.CreateTest(testCaseId + " - " + testName);

            // Optionally, you can add the Test Case ID to the categories or author fields
            _test.AssignCategory(testCaseId);
            _test.AssignAuthor("Tester Name");
        }

        // Log Test Steps (without repeating the Test Case ID for each step)
        public static void LogTestStep(string stepDescription)
        {
            _test.Info(stepDescription);
        }

        // Log Pass Test Step
        public static void LogPassTestStep(string stepDescription)
        {
            _test.Pass(stepDescription);
        }

        // Log Fail Test Step
        public static void LogFailTestStep(string stepDescription, Exception ex)
        {
            _test.Fail(stepDescription + " - Error: " + ex.Message);
        }
    }
}
using Microsoft.Playwright;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Modules.Items.Constants;
using PIMQAAutomation.Utilities;
using TechTalk.SpecFlow;
using Utilities;
namespace PIMQAAutomation.Modules.Items.Actions
{
   

    /// <summary>
    /// Contains business logic for Admin OEM operations
    /// </summary>
    public class AdminOemActions
    {
        private readonly IPage page;
        private readonly ScenarioContext scenarioContext;
        private readonly FeatureContext featureContext;
        private string testCaseName = string.Empty;
        private readonly string oem = "AdminOem";

        public AdminOemActions(IPage page, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            this.page = page;
            this.scenarioContext = scenarioContext;
            this.featureContext = featureContext;
        }

        public async Task CreateOem(string newOrExistingGroup)
        {
            try
            {
                testCaseName = scenarioContext.ScenarioInfo.Title;

                // Step 1: Click "Create" button and wait for the tab
                await WebUIHelper.ClickAsync(scenarioContext, page, $"{oem}.CreateButton");
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{oem}.PageTitle", TimeConstants.MinimumWait, WaitForSelectorState.Visible);

                // Step 2: Load field mappings (ordered dictionary preserves order)
                var fieldMappings = await GetOemFieldMappingsAsync();
                if (newOrExistingGroup == AdminConstants.ExistingGroup)
                {
                    await WebUIHelper.FillFieldsFromTestDataAsync(page, scenarioContext, featureContext, testCaseName, fieldMappings, randomFieldKey: "OemTextbox", randomContextKey: "OemName");
                    var groupName = featureContext[AdminConstants.FeatureContextValues.GroupName].ToString();
                    await WebUIHelper.SelectDropDownOptionAsync(scenarioContext, page, $"{oem}.GroupNameDropdown", groupName);
                }
                else if (newOrExistingGroup == AdminConstants.NewGroup)
                {
                    await WebUIHelper.ClickAsync(scenarioContext, page, $"{oem}.CreateNewgroupButton");
                    await WebUIHelper.WaitForAsync(scenarioContext, page, $"{oem}.NewGroupPopupTitle", TimeConstants.MinimumWait, WaitForSelectorState.Visible);
                    var groupName = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.NewGroupName");
                    var expectedGroupName = $"{groupName}-{Guid.NewGuid().ToString().Substring(0, 4).ToUpper()}";
                    await WebUIHelper.FillAsync(scenarioContext, page, $"{oem}.NewGroupName", $"{expectedGroupName}");
                    await WebUIHelper.ClickAsync(scenarioContext, page, $"{oem}.CreateGroupbutton");
                    await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);
                    var actualGroupName = await WebUIHelper.GetInputValueAsync(scenarioContext, page, $"{oem}.GroupNameDropdown");
                    featureContext[AdminConstants.FeatureContextValues.GroupName] = actualGroupName;
                    ValidationHelper.AssertEqual("GroupNameDropdown", expectedGroupName, actualGroupName, $"$[ASSERT FAILED] Expected GroupNameDropdown value: '{expectedGroupName}', but found: '{actualGroupName}'.");
                    await WebUIHelper.FillFieldsFromTestDataAsync(page, scenarioContext, featureContext, testCaseName, fieldMappings, randomFieldKey: "OemTextbox", randomContextKey: "OemName");
                }

                await WebUIHelper.ClickAsync(scenarioContext, page, $"{oem}.SaveButton");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Playwright failure: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw ExceptionHandler.BuildCustomException($"validate create Oem for {newOrExistingGroup}.", ex);
            }
        }

        public async Task UpdateExistingOemActiveStatus()
        {
            try
            {
                testCaseName = scenarioContext.ScenarioInfo.Title;
                string oemName = featureContext[AdminConstants.FeatureContextValues.OemName].ToString();
                string activeStatus = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.ActiveCheckbox");

                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{oem}.PageTitle", TimeConstants.MinimumWait, WaitForSelectorState.Visible);
                await WebUIHelper.SelectDropDownOptionAsync(scenarioContext, page, $"{oem}.OemTextbox", oemName);
                await WebUIHelper.ClickAsync(scenarioContext, page, $"{oem}.EditButton");
                await WebUIHelper.HandleCheckboxAsync(scenarioContext, page, $"{oem}.ActiveCheckbox", activeStatus, "Active");
                await WebUIHelper.ClickAsync(scenarioContext, page, $"{oem}.UpdateButton");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Playwright failure: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw ExceptionHandler.BuildCustomException($"validate Update existing Oem Active Status.", ex);
            }
        }

        public async Task UpdateExistingOemDetails()
        {
            try
            {
                testCaseName = scenarioContext.ScenarioInfo.Title;
                string oemName = featureContext[AdminConstants.FeatureContextValues.OemName].ToString();
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{oem}.PageTitle", TimeConstants.MinimumWait, WaitForSelectorState.Visible);
                await WebUIHelper.SelectDropDownOptionAsync(scenarioContext, page, $"{oem}.OemTextbox", oemName);
                await WebUIHelper.ClickAsync(scenarioContext, page, $"{oem}.EditButton");
                var fieldMappings = await GetOemFieldMappingsAsync();
                await WebUIHelper.FillFieldsFromTestDataAsync(page, scenarioContext, featureContext, testCaseName, fieldMappings, randomFieldKey: null, randomContextKey: null);
                await WebUIHelper.ClickAsync(scenarioContext, page, $"{oem}.UpdateButton");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Playwright failure: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw ExceptionHandler.BuildCustomException($"Failed to validate update of existing OEM details.", ex);
            }
        }

        public async Task VerifyModifiedOemDetails()
        {
            try
            {
                ValidationHelper.AreValuesEqual(Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.GroupNameDropdown"), await WebUIHelper.GetInputValueAsync(scenarioContext, page, $"{oem}.GroupNameDropdown"));
                ValidationHelper.AreValuesEqual(Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.DescriptionTextbox"), await WebUIHelper.GetInputValueAsync(scenarioContext, page, $"{oem}.DescriptionTextbox"));
                ValidationHelper.AreValuesEqual(Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.InternalNotesTextbox"), await WebUIHelper.GetInputValueAsync(scenarioContext, page, $"{oem}.InternalNotesTextbox"));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Playwright failure: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw ExceptionHandler.BuildCustomException($"Falied to validate modified Oem details.", ex);
            }
        }

        public Task<Dictionary<string, (string locatorKey, string controlType)>> GetOemFieldMappingsAsync()
        {
            var fieldMappings = new Dictionary<string, (string locatorKey, string controlType)>
    {
    { "OemTextbox", ($"{oem}.OemTextbox", "textbox") },
    { "GroupNameDropdown", ($"{oem}.GroupNameDropdown", "dropdown") },
    { "DescriptionTextbox", ($"{oem}.DescriptionTextbox", "textbox") },
    { "ActiveCheckbox", ($"{oem}.ActiveCheckbox", "checkbox") },
    { "InternalNotesTextbox", ($"{oem}.InternalNotesTextbox", "textbox") }
    };
            return Task.FromResult(fieldMappings);
        }

        public async Task ValidateFieldsAsync(Table fieldsToCheck)
        {
            try
            {
                foreach (var field in fieldsToCheck.Rows)
                {
                    var fieldKey = field["FieldKey"];
                    var validateEmpty = bool.Parse(field["ValidateEmpty"]);
                    var validateMaxLength = bool.Parse(field["ValidateMaxLength"]);
                    var (maxLength, errorLocator, emptyError, maxLengthError) = AdminConstants.FieldValidations[fieldKey];
                    await ValidateFieldAsync(fieldKey, $"{oem}.{fieldKey}", errorLocator, maxLength, emptyError, maxLengthError, validateEmpty, validateMaxLength);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Playwright failure: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw ExceptionHandler.BuildCustomException($"Field validation failed", ex);
            }
        }

        public async Task ValidateFieldAsync(string fieldKey, string fieldLocator, string errorLocator, int maxLength, string emptyError, string maxLengthError, bool validateEmpty = true, bool validateMaxLength = true)
        {
            try
            {
                await WebUIHelper.ClickAsync(scenarioContext, page, $"{oem}.CreateButton");

                // EMPTY VALIDATION
                if (validateEmpty)
                {
                    await WebUIHelper.ClickAsync(scenarioContext, page, $"{oem}.SaveButton");
                    await AdminCommonActionsHelper.ValidateErrorMessageAsync(scenarioContext, page, errorLocator, emptyError);
                    Logger.LogInfo($"[PASS] Empty validation passed for {fieldKey}");
                }

                // MAX LENGTH VALIDATION
                if (validateMaxLength && maxLength > 0 && !string.IsNullOrEmpty(maxLengthError))
                {
                    string longText = new string(AdminConstants.MaxLengthTestChar, maxLength + 5);
                    await WebUIHelper.FillAsync(scenarioContext, page, fieldLocator, longText);
                    await AdminCommonActionsHelper.ValidateErrorMessageAsync(scenarioContext, page, errorLocator, maxLengthError);
                    Logger.LogInfo($"[PASS] Max length validation passed for {fieldKey}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Playwright failure: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw ExceptionHandler.BuildCustomException($"Field validation failed for {fieldKey}", ex);
            }
        }
    }
}

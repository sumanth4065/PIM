using global::PIMQAAutomation.Modules.Common.Constants;
using global::PIMQAAutomation.Modules.Items.Constants;
using global::Utilities;
using Microsoft.Playwright;
using PIMQAAutomation.Utilities;
using TechTalk.SpecFlow;
using Utilities;
using static global::PIMQAAutomation.Modules.Items.Constants.AdminConstants;
namespace PIMQAAutomation.Modules.Items.Actions
{
   

    /// <summary>
    /// Contains common business logic and validation methods for Admin operations
    /// </summary>
    public class AdminCommonActions
    {
        private readonly IPage page;
        private readonly ScenarioContext scenarioContext;
        private readonly FeatureContext featureContext;
        private string testCaseName = string.Empty;
        private readonly string commonActions = "AdminCommonActions";
        private readonly string standard = "AdminStandardCharacteristics";
        private readonly string value = "AdminValue";

        public AdminCommonActions(IPage page, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            this.page = page;
            this.scenarioContext = scenarioContext;
            this.featureContext = featureContext;
        }

        public static Dictionary<string, string> GetMandatoryFieldErrors(string moduleName)
        {
            return moduleName switch
            {
                "Characteristic Kind" => new Dictionary<string, string>
 {
         { "Kind Name", "kind Name is required!" },
         { "Kind Rank Memo", "kind Rank Memo is required!" },
         { "Kind Description", "kind Description is required!" }
 },

                "Characteristic Type" => new Dictionary<string, string>
       {
   { "Type Name", "Type Name is required!" },
   { "Type Rank Memo", "Type Rank Memo is required!" },
   { "Type Description", "Type Description is required!" },
   { "Type Data Type", "Data type is required!" }
       },

                "Value" => new Dictionary<string, string>
     {
        { "LOV Name", "Characteristic LOV Name is required!" },
        { "LOV Description", "Description is required!" }
     },

                "User Management" => new Dictionary<string, string>
        {
  { "Internal Notes", "Notes must be modified when status changes" }
        },

                "Taxonomy" => new Dictionary<string, string>
          {
  { "LOV Name", "Name is required!" },
  { "Description", "Description is required!" }
          },
                _ => throw new ArgumentException($"Unknown module: {moduleName}")
            };
        }

        public async Task ValidateSaveChangesPopupAsync()
        {
            try
            {
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{commonActions}.PopUpTitle", TimeConstants.MediumWait, WaitForSelectorState.Visible);

                var actualPopUpTitle = await WebUIHelper.GetInnerTextAsync(scenarioContext, page, $"{commonActions}.PopUpTitle");
                var expectedPopUpTitle = AdminConstants.SaveChangesTitle;

                Console.WriteLine($"The extracted pop Up title is {actualPopUpTitle}");

                var actualPopupMessage = await WebUIHelper.GetInnerTextAsync(scenarioContext, page, $"{commonActions}.PopUpContent");
                var expectedPopupMessage = AdminConstants.SaveChangesMessage;

                ValidationHelper.AssertEqual($"{commonActions}.PopUpTitle", expectedPopUpTitle, actualPopUpTitle);

                //Validate the pop up text message and Click on Yes button
                ValidationHelper.AssertEqual($"{commonActions}.PopUpContent", expectedPopupMessage, actualPopupMessage);
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{commonActions}.PopUpYesButton", TimeConstants.MediumWait, WaitForSelectorState.Visible);
                await WebUIHelper.ClickAsync(scenarioContext, page, $"{commonActions}.PopUpYesButton");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Playwright failure: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw ExceptionHandler.BuildCustomException("validate pop up content and click on Yes button.", ex);
            }
        }

        public async Task ValidateToastMessageAsync(Table fieldNames)
        {
            try
            {
                testCaseName = scenarioContext.ScenarioInfo.Title;

                string expectedTitle = string.Empty;
                string expectedMessage = string.Empty;

                ExtractExpectedValues(fieldNames, ref expectedTitle, ref expectedMessage);

                await WaitForToastAsync();

                var (actualTitle, actualMessage) = await GetToastTextsAsync();

                LogDebugInfo(expectedTitle, actualTitle, expectedMessage, actualMessage);

                ValidateToastContent(expectedTitle, actualTitle, expectedMessage, actualMessage);

                await CloseToastAsync();
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("validate the toast title and toast message.", ex);
            }
        }

        private void ExtractExpectedValues(Table fieldNames, ref string expectedTitle, ref string expectedMessage)
        {
            try
            {
                foreach (var row in fieldNames.Rows)
                {
                    string fieldName = row["FieldName"].Trim();
                    string fieldValue = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.{fieldName}");

                    if (string.IsNullOrEmpty(fieldValue))
                    {
                        throw new CustomException($"[ERROR] Test data for '{fieldName}' is missing/empty in test case '{testCaseName}'.");
                    }

                    switch (fieldName)
                    {
                        case "SuccessToastTitle":
                            expectedTitle = fieldValue;
                            break;

                        default:
                            // Mapping between toast messages and featureContext keys
                            var toastKeyMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
          {
            { "KindSavedSuccessfullyToastMessage", "RandomKindName" },
            { "TypeSavedSuccessfullyToastMessage", "RandomTypeName" },
            { "FamilySavedSuccessfullyToastMessage", "RandomFamilyName" },
            { "LineSavedSuccessfullyToastMessage", "RandomLineName" },
            { "GroupSavedSuccessfullyToastMessage", "RandomGroupName" }
          };

                            // Handle OEM separately (2 placeholders)
                            if (fieldName.Equals("OemSavedSuccessfullyToastMessage", StringComparison.OrdinalIgnoreCase))
                            {
                                expectedMessage = BuildOemMessage(fieldValue);
                            }

                            // Handle all others dynamically
                            else if (toastKeyMap.TryGetValue(fieldName, out string featureKey))
                            {
                                expectedMessage = BuildExpectedMessage(fieldValue, featureKey);
                            }
                            else
                            {
                                Console.WriteLine($"[WARN] No mapping found for '{fieldName}' in toast validation.");
                            }

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException(
                   $"Error occurred while extracting expected toast values for test case: {testCaseName}", ex);
            }
        }

        private string BuildExpectedMessage(string template, string featureKey)
        {
            if (!featureContext.ContainsKey(featureKey))
            {
                throw new CustomException($"[ERROR] {featureKey} is missing in FeatureContext. Ensure that creation step stored it.");
            }

            string contextValue = featureContext[featureKey]?.ToString();
            return template.Replace("[TEXT]", contextValue);
        }

        private string BuildOemMessage(string template)
        {
            string oemKey = AdminConstants.FeatureContextValues.OemName;
            string groupKey = AdminConstants.FeatureContextValues.GroupName;

            if (!featureContext.ContainsKey(oemKey))
            {
                throw new CustomException($"[ERROR] {oemKey} is missing in FeatureContext. Ensure Type creation step stored it.");
            }

            if (!featureContext.ContainsKey(groupKey))
            {
                throw new CustomException($"[ERROR] {groupKey} is missing in FeatureContext. Ensure Group creation step stored it.");
            }

            string oemName = featureContext[oemKey]?.ToString();
            string groupName = featureContext[groupKey]?.ToString();

            return template.Replace("[TEXT1]", oemName).Replace("[TEXT2]", groupName);
        }

        private async Task WaitForToastAsync()
        {
            try
            {
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{commonActions}.ToastMessageTitle", TimeConstants.MaximumWait, WaitForSelectorState.Visible);
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("wait for toast message to be visible.", ex);
            }
        }

        private async Task<(string title, string message)> GetToastTextsAsync()
        {
            try
            {
                var title = (await WebUIHelper.GetInnerTextAsync(scenarioContext, page, $"{commonActions}.ToastMessageTitle") ?? string.Empty).Trim();
                var message = (await WebUIHelper.GetInnerTextAsync(scenarioContext, page, $"{commonActions}.ToastMessageText") ?? string.Empty).Trim();
                return (title, message);
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("get toast message texts.", ex);
            }
        }

        private void ValidateToastContent(string expectedTitle, string actualTitle, string expectedMessage, string actualMessage)
        {
            try
            {
                if (!string.IsNullOrEmpty(expectedTitle))
                {
                    ValidationHelper.AssertEqual($"{commonActions}.ToastMessageTitle", expectedTitle, actualTitle);
                }

                if (!string.IsNullOrEmpty(expectedMessage))
                {
                    ValidationHelper.AssertEqual($"{commonActions}.ToastMessageText", expectedMessage, actualMessage);
                }

                Console.WriteLine($"[INFO] Toast validated successfully for test case '{testCaseName}'.");
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("validate the toast content", ex);
            }
        }

        private async Task CloseToastAsync()
        {
            try
            {
                await WebUIHelper.JSClickAsync(scenarioContext, page, $"{commonActions}.ToastCloseButton");
                await WebUIHelper.WaitForElementToDisappearAsync(scenarioContext, page, $"{commonActions}.ToastMessageTitle", TimeConstants.MaximumWait);
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("close toast message.", ex);
            }
        }

        private void LogDebugInfo(string expectedTitle, string actualTitle, string expectedMessage, string actualMessage)
        {
            try
            {
                Console.WriteLine($"[DEBUG] Expected Title:   '{expectedTitle}'");
                Console.WriteLine($"[DEBUG] Actual Title:     '{actualTitle}'");
                Console.WriteLine($"[DEBUG] Expected Message: '{expectedMessage}'");
                Console.WriteLine($"[DEBUG] Actual Message:   '{actualMessage}'");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to log debug info: {ex.Message}");
            }
        }

        public async Task CheckFieldErrors(string moduleName)
        {
            var expectedErrors = GetMandatoryFieldErrors(moduleName);

            foreach (var field in expectedErrors)
            {
                string expectedMessage = field.Value;

                if (string.IsNullOrWhiteSpace(expectedMessage))
                {
                    Console.WriteLine($"[INFO] No expected error message defined for field '{field.Key}' in module '{moduleName}'.");
                    continue;
                }

                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{commonActions}.MandatoryFieldErrorMessages", TimeConstants.MediumWait, expectedMessage, WaitForSelectorState.Visible);

                bool isVisible = await WebUIHelper.IsVisibleAsync(scenarioContext, page, $"{commonActions}.MandatoryFieldErrorMessages", expectedMessage);

                if (!isVisible)
                {
                    throw new CustomException($"[FAIL] Error message not visible: '{expectedMessage}' for field '{field.Key}' in module '{moduleName}'.");
                }

                Console.WriteLine($"[PASS] Error message visible: '{expectedMessage}' for field '{field.Key}' in module '{moduleName}'.");
            }
        }

        private async Task EnterTextInFieldAsync(string inputLocatorKey, string longText)
        {
            LocatorReader locatorReader = new LocatorReader(scenarioContext);
            var inputElement = locatorReader.GetByKey(page, inputLocatorKey);
            await inputElement.ClickAsync();
            await inputElement.FillAsync(longText);
        }

        public async Task EnterLongTextAndVerifyErrorAsync(Table fieldName)
        {
            foreach (var row in fieldName.Rows)
            {
                string field = row["FieldName"];
                try
                {
                    // Get field-specific parameters
                    var (charLimit, inputLocatorKey, errorLocatorKey, expectedErrorMessage) = GetFieldDetails(field);

                    // Generate and enter long text
                    string longText = WebUIHelper.GenerateRandomString(charLimit);
                    await EnterTextInFieldAsync(inputLocatorKey, longText);

                    // Verify the error message
                    await VerifyErrorMessageAsync(field, errorLocatorKey, expectedErrorMessage);
                }
                catch (Exception ex)
                {
                    throw ExceptionHandler.BuildCustomException($"[ERROR] Failed in EnterTextAndVerifyErrorAsync for field '{field}'", ex);
                }
            }
        }

        private (int charLimit, string inputLocatorKey, string errorLocatorKey, string expectedErrorMessage) GetFieldDetails(string field)
        {
            string lowerField = field.ToLower();

            var mappings = new Dictionary<string, (int charLimit, string inputLocatorKey, string errorLocatorKey, string expectedErrorMessage)>
 {
                { "kindname", (CharacterLimit.KindName, $"{standard}.KindNameTextBox", $"{standard}.KindNameCharacterLimitError", ErrorMessages.CharacterLimitExceeded) },
                { "kindrank", (CharacterLimit.KindRank, $"{standard}.KindRankTextBox", $"{standard}.KindRankCharacterLimit", ErrorMessages.KindRankMustContainNumbers) },
                { "kindrankmemo", (CharacterLimit.KindRankMemo, $"{standard}.KindRankMemoTextBox", $"{standard}.KindRankMemoCharacterLimitError", ErrorMessages.CharacterLimitExceeded) },
                { "kinddescription", (CharacterLimit.KindDescription, $"{standard}.KindDescriptionTextBox", $"{standard}.KindDescriptionCharacterLimitError", ErrorMessages.CharacterLimitExceeded) },
                { "typename", (CharacterLimit.TypeName, $"{standard}.TypeNameTextBox", $"{standard}.TypeNameCharacterLimitError", ErrorMessages.CharacterLimitExceeded) },
                { "typerankmemo", (CharacterLimit.TypeRankMemo, $"{standard}.TypeRankmemoTextbox", $"{standard}.TypeRankMemoCharacterLimitError", ErrorMessages.CharacterLimitExceeded) },
                { "typedescription", (CharacterLimit.TypeDescription, $"{standard}.TypeDescriptionTextbox", $"{standard}.TypeDescriptionCharacterLimitError", ErrorMessages.CharacterLimitExceeded) },
                { "lovname", (CharacterLimit.LovName, $"{value}.LOVNameTextBox", $"{value}.LOVNameCharacterLimitError", ErrorMessages.CharacterLimitExceeded) },
                { "lovdescription", (CharacterLimit.LovDescription, $"{value}.LOVNameDescriptionTextBox", $"{value}.LOVDescriptionCharacterLimitError", ErrorMessages.CharacterLimitExceeded) }
 };

            if (!mappings.TryGetValue(lowerField, out var details))
            {
                throw new ArgumentException($"Unsupported field '{field}'");
            }

            return details;
        }

        private async Task VerifyErrorMessageAsync(string field, string errorLocatorKey, string expectedErrorMessage)
        {
            await page.Keyboard.PressAsync("Tab");
            string actualMessage = await WebUIHelper.GetInnerTextAsync(scenarioContext, page, errorLocatorKey);

            ValidationHelper.FailIfNullOrEmpty($"Expected error message for '{field}'", expectedErrorMessage, "Expected error message is required.");
            ValidationHelper.FailIfNullOrEmpty($"Actual error message for '{field}'", actualMessage, "Actual error message is missing.");
            ValidationHelper.AssertEqual($"Error message for '{field}'", expectedErrorMessage.Trim(), actualMessage.Trim(), $"Locator: {errorLocatorKey}");
        }

        public async Task Logout()
        {
            await WebUIHelper.WaitForAsync(scenarioContext, page, $"{commonActions}.LogOutButton", TimeConstants.MediumWait, WaitForSelectorState.Visible);
            await WebUIHelper.ClickAsync(scenarioContext, page, $"{commonActions}.LogOutButton");
            await WebUIHelper.WaitForAsync(scenarioContext, page, $"{commonActions}.LogInButton", TimeConstants.MediumWait, WaitForSelectorState.Visible);
        }

        public async Task ValidateWarningPopupAsync(string buttonType)
        {
            try
            {
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{commonActions}.PopUpTitle", TimeConstants.MediumWait, WaitForSelectorState.Visible);

                var actualPopUpTitle = await WebUIHelper.GetInnerTextAsync(scenarioContext, page, $"{commonActions}.PopUpTitle");
                var expectedPopUpTitle = AdminConstants.LossOfDataTitle;

                Console.WriteLine($"The extracted pop Up title is {actualPopUpTitle}");

                var actualPopupMessage = await WebUIHelper.GetInnerTextAsync(scenarioContext, page, $"{commonActions}.PopUpContent");
                var expectedPopupMessage = AdminConstants.LossOfDataMessage;

                ValidationHelper.AssertEqual($"{commonActions}.PopUpTitle", expectedPopUpTitle, actualPopUpTitle);

                //Validate the pop up text message and Click on Yes button
                ValidationHelper.AssertEqual($"{commonActions}.PopUpContent", expectedPopupMessage, actualPopupMessage);
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{commonActions}.PopUpYesButton", TimeConstants.MediumWait, WaitForSelectorState.Visible);
                if (buttonType == AdminConstants.PopUpYesButton)
                {
                    await WebUIHelper.ClickAsync(scenarioContext, page, $"{commonActions}.PopUpYesButton");
                }
                else
                {
                    await WebUIHelper.ClickAsync(scenarioContext, page, $"{commonActions}.PopUpNoButton");
                }

                await WebUIHelper.WaitForElementToDisappearAsync(scenarioContext, page, $"{commonActions}.LoadSpinner", TimeConstants.MediumWait);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Playwright failure: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw ExceptionHandler.BuildCustomException("validate pop up content and click on Yes button.", ex);
            }
        }
    }
}

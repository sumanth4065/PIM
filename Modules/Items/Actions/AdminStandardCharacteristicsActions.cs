using Microsoft.Playwright;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Modules.Items.Constants;
using PIMQAAutomation.Utilities;
using TechTalk.SpecFlow;
using Utilities;
namespace PIMQAAutomation.Modules.Items.Actions
{
    

    /// <summary>
    /// Contains business logic for Admin Standard Characteristics operations
    /// </summary>
    public class AdminStandardCharacteristicsActions
    {
        private readonly IPage page;
        private readonly ScenarioContext scenarioContext;
        private readonly FeatureContext featureContext;
        private string defaultTypeOption;
        private const string Standard = "AdminStandardCharacteristics";
        private const string CommonActions = "AdminCommonActions";
        private string testCaseName = string.Empty;

        public AdminStandardCharacteristicsActions(IPage page, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            this.page = page;
            this.scenarioContext = scenarioContext;
            this.featureContext = featureContext;
        }


        /// <summary>
        /// Returns field mappings for Characteristic Type
        /// </summary>
        private Dictionary<string, (string locatorKey, string controlType)> GetCharTypeFieldMappings()
        {
            var fieldMappings = new Dictionary<string, (string locatorKey, string controlType)>
     {
         { "TypeNameTextBox", ($"{Standard}.TypeNameTextBox", "textbox") },
         { "TypeRankTextBox", ($"{Standard}.TypeRankTextBox", "textbox") },
         { "TypeRankmemoTextbox", ($"{Standard}.TypeRankmemoTextbox", "textbox") },
         { "TypeDescriptionTextbox", ($"{Standard}.TypeDescriptionTextbox", "textbox") },
         { "TypeActiveCheckbox", ($"{Standard}.TypeActiveCheckbox", "checkbox") },
         { "TrackHistoryCheckbox", ($"{Standard}.TrackHistoryCheckbox", "checkbox") },
         { "TypeisReadOnlyCheckbox", ($"{Standard}.TypeisReadOnlyCheckbox", "checkbox") },
         { "Is4SNIPricingCheckbox", ($"{Standard}.Is4SNIPricingCheckbox", "checkbox") },
         { "AllowExpilicitValueCheckbox", ($"{Standard}.AllowExpilicitValueCheckbox", "checkbox") },
         { "EnableBaselineEditingCheckbox", ($"{Standard}.EnableBaselineEditingCheckbox", "checkbox") },
         { "EnableExplicitEditingCheckbox", ($"{Standard}.EnableExplicitEditingCheckbox", "checkbox") },
         { "EnableTaxonomyEditingCheckbox", ($"{Standard}.EnableTaxonomyEditingCheckbox", "checkbox") },
         { "ItemIdDropdown", ($"{Standard}.ItemIdDropdown", "dropdown") },
         { "OEMDropdown", ($"{Standard}.OEMDropdown", "dropdown") },
         { "ExplicitUILineofBusinessFilter", ($"{Standard}.ExplicitUILineofBusinessFilter", "dropdown") },
         { "ItemDescriptionTextbox", ($"{Standard}.ItemDescriptionTextbox", "textbox") },
         { "DataTypeDropDown", ($"{Standard}.DataTypeDropDown", "dropdown") },
         { "MaximumTextBox", ($"{Standard}.MaximumTextBox", "textbox") },
         { "DefaultValueDropDown", ($"{Standard}.DefaultValueDropDown", "dropdown") },
         { "DefaultValueTextBoxForDate", ($"{Standard}.DefaultValueTextBoxForDate", "textbox") },
         { "TypeInternalNotesTextbox", ($"{Standard}.TypeInternalNotesTextbox", "textbox") }
     };

            return fieldMappings;
        }

        /// <summary>
        /// Returns field mappings for Characteristic Kind
        /// </summary>
        private Dictionary<string, (string locatorKey, string controlType)> GetCharKindFieldMappings()
        {
            var fieldMappings = new Dictionary<string, (string locatorKey, string controlType)>
{
           { "KindNameTextBox", ($"{Standard}.KindNameTextBox", "textbox") },
           { "KindRankTextBox", ($"{Standard}.KindRankTextBox", "textbox") },
           { "KindRankMemoTextBox", ($"{Standard}.KindRankMemoTextBox", "textbox") },
           { "KindDescriptionTextBox", ($"{Standard}.KindDescriptionTextBox", "textbox") },
           { "KindActiveCheckBox", ($"{Standard}.KindActiveCheckBox", "checkbox") },
           { "ItemLinkedToTypeCheckbox", ($"{Standard}.ItemLinkedToTypeCheckbox", "checkbox") },
           { "LinkedToLOBCheckBox", ($"{Standard}.LinkedToLOBCheckBox", "checkbox") },
           { "RestrictedCheckBox", ($"{Standard}.RestrictedCheckBox", "checkbox") },
           { "KindInternalNotesTextBox", ($"{Standard}.KindInternalNotesTextBox", "textbox") }
};
            return fieldMappings;
        }



        /// <summary>
        /// Validates that the Standard Characteristics page header is visible
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task ValidateStandardPageHeaderAsync()
        {
            try
            {
                Console.WriteLine("Waiting for Standard Characteristics page header");
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Standard}.StandardPageHeader", TimeConstants.MediumWait, WaitForSelectorState.Visible);

                var isStandardPageHeaderVisible = await WebUIHelper.IsVisibleAsync(scenarioContext, page, $"{Standard}.StandardPageHeader");
                if (!isStandardPageHeaderVisible)
                {
                    ValidationHelper.Fail("Standard Characteristics page header is not visible.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Playwright failure: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw ExceptionHandler.BuildCustomException("validate Standard Characteristics page header. The header was not found or not visible within the timeout.", ex);
            }
        }



        /// <summary>
        /// Creates a new Characteristic Kind
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task CreateCharKindAsync()
        {
            try
            {
                await ClickOnNewCharKindButtonAsync();
                await EnterKindDetailsAsync();
                await ClickCharStandardSaveButtonAsync();
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("create Characteristic Kind", ex);
            }
        }

        /// <summary>
        /// Validates the default Characteristic Kind details
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task ValidateDefaultCharacteristicKindAsync()
        {
            try
            {
                await page.ReloadAsync();

                // Before search: break loop when actualKindName == displayedKindOption
                await ValidateCharKindDetailsAsync(isSearch: false, shouldBreakLoop: (actual, displayed) => actual.Equals(displayed, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Playwright failure: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw ExceptionHandler.BuildCustomException("validate Characteristic Kind (before search)", ex);
            }
        }

        /// <summary>
        /// Validates the searched Characteristic Kind details
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task ValidateCharacteristicKindAsync()
        {
            try
            {
                await WebUIHelper.WaitForElementToDisappearAsync(scenarioContext, page, $"{CommonActions}.LoadSpinner", TimeConstants.MediumWait);

                // After search: break loop when actualKindName ? displayedKindOption
                await ValidateCharKindDetailsAsync(isSearch: true, shouldBreakLoop: (actual, displayed) => !actual.Equals(displayed, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Playwright failure: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw ExceptionHandler.BuildCustomException("validate Characteristic Kind (after search)", ex);
            }
        }

        /// <summary>
        /// Enters Characteristic Kind details from test data
        /// </summary>
        private async Task EnterKindDetailsAsync()
        {
            try
            {
                testCaseName = scenarioContext.ScenarioInfo.Title;

                var fieldMappings = GetCharKindFieldMappings();

                await WebUIHelper.FillFieldsFromTestDataAsync(page, scenarioContext, featureContext, testCaseName, fieldMappings, randomFieldKey: "KindNameTextBox", randomContextKey: "RandomKindName");

                string expectedValue = AdminConstants.NoTranslation;
                string actualValue = await WebUIHelper.GetInputValueAsync(scenarioContext, page, $"{Standard}.TranslationActionDropDown");

                ValidationHelper.FailIfNullOrEmpty($"{Standard}.TranslationActionDropDown", actualValue, "Expected not to be null. But was null");

                ValidationHelper.AssertEqual($"{Standard}.TranslationActionDropDown", expectedValue, actualValue, $"[ASSERT FAILED] Expected default Translation option: '{expectedValue}', but found: '{actualValue}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed while creating characteristic kind. \nError: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw ExceptionHandler.BuildCustomException("create Characteristic Kind", ex);
            }
        }

        /// <summary>
        /// Creates Characteristic Kind without mandatory details to generate error messages
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task CreateCharKindErrorMsgsAsync()
        {
            try
            {
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Standard}.NewKindButton", TimeConstants.MediumWait, WaitForSelectorState.Visible);
                await ClickOnNewCharKindButtonAsync();
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Standard}.CharStandardSaveButton", TimeConstants.MediumWait, WaitForSelectorState.Visible);
                await ClickCharStandardSaveButtonAsync();
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("generate Characteristic Kind error messages", ex);
            }
        }



        /// <summary>
        /// Creates a new Characteristic Type
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task CreateCharTypeAsync()
        {
            try
            {
                await ClickOnNewTypeButtonAsync();
                await EnterTypeDetailsAsync();
                await ClickCharStandardSaveButtonAsync();
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("create Characteristic Type", ex);
            }
        }

        /// <summary>
        /// Validates the default Characteristic Type details
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task ValidateDefaultCharacteristicTypeAsync()
        {
            try
            {
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Standard}.DefaultCharKindOptionSelected", TimeConstants.MediumWait, WaitForSelectorState.Visible);
                await WebUIHelper.ToHaveAttributeAsync(scenarioContext, page, $"{Standard}.DefaultCharKindOptionSelected", "aria-expanded", "true");
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Standard}.DefaultCharTypeOption", TimeConstants.MediumWait, WaitForSelectorState.Visible);
                await WebUIHelper.IsVisibleAsync(scenarioContext, page, $"{Standard}.DefaultCharTypeOptionSelected");

                defaultTypeOption = await WebUIHelper.GetInnerTextAsync(scenarioContext, page, $"{Standard}.DefaultCharTypeOption");
                Console.WriteLine($"[INFO] Extracted default Characteristic Type Option: {defaultTypeOption}");

                bool isTypeTabVisible = await WebUIHelper.IsVisibleAsync(scenarioContext, page, $"{Standard}.TypeNameTextBox");

                if (!isTypeTabVisible)
                {
                    Console.WriteLine("[INFO] Type tab not visible, clicking Type tab...");
                    await WebUIHelper.ClickAsync(scenarioContext, page, $"{Standard}.TypeTab");
                    await WebUIHelper.IsVisibleAsync(scenarioContext, page, $"{Standard}.SelectedTypeTab");
                }
                else
                {
                    Console.WriteLine("[INFO] Type tab already visible, skipping tab click.");
                }

                var typeFields = new List<(string Field, string Locator, Func<string> Expected)>
        {
      ("Type Name", $"{Standard}.TypeNameTextBox", () => defaultTypeOption),
      ("Type Rank", $"{Standard}.TypeRankTextBox", null),
      ("Type Rank Memo", $"{Standard}.TypeRankmemoTextbox", null),
      ("Type Description", $"{Standard}.TypeDescriptionTextbox", null)
        };

                foreach (var (field, locator, expectedProvider) in typeFields)
                {
                    string expected = expectedProvider?.Invoke();
                    await ValidateCharacteristicFieldAsync(field, locator, expected);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Playwright failure: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw ExceptionHandler.BuildCustomException("validate Characteristic Type", ex);
            }
        }

        /// <summary>
        /// Enters Characteristic Type details from test data
        /// </summary>
        private async Task EnterTypeDetailsAsync()
        {
            try
            {
                testCaseName = scenarioContext.ScenarioInfo.Title;

                var fieldMappings = GetCharTypeFieldMappings();

                await WebUIHelper.FillFieldsFromTestDataAsync(page, scenarioContext, featureContext, testCaseName, fieldMappings, randomFieldKey: "TypeNameTextBox", randomContextKey: "RandomTypeName");

                await ValidateBusinessPriorityDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed while creating characteristic Type.\nTestCase: {testCaseName}\nError: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw ExceptionHandler.BuildCustomException("create Characteristic Type", ex);
            }
        }

        /// <summary>
        /// Creates Characteristic Type without mandatory details to generate error messages
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task CreateCharTypeErrorMsgsAsync()
        {
            try
            {
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Standard}.NewTypeTab", TimeConstants.MediumWait, WaitForSelectorState.Visible);
                await ClickOnNewTypeButtonAsync();
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Standard}.CharStandardSaveButton", TimeConstants.MediumWait, WaitForSelectorState.Visible);
                await ClickCharStandardSaveButtonAsync();
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("generate Characteristic Type error messages", ex);
            }
        }



        /// <summary>
        /// Manages Characteristic entity (Kind or Type) operations
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task ManageCharacteristicEntityAsync(string entityType, string actionType)
        {
            try
            {
                if (entityType.Equals("kind", StringComparison.OrdinalIgnoreCase))
                {
                    await ManageCharacteristicKindAsync(actionType, entityType);
                }
                else if (entityType.Equals("type", StringComparison.OrdinalIgnoreCase))
                {
                    await ManageCharacteristicTypeAsync(actionType, entityType);
                }
                else
                {
                    throw new ArgumentException($"Unsupported entity '{entityType}'. Use 'kind' or 'type'.");
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException($"Failed to {actionType} Characteristic {entityType}", ex);
            }
        }

        /// <summary>
        /// Manages Characteristic Kind operations (edit, activate, inactivate)
        /// </summary>
        private async Task ManageCharacteristicKindAsync(string actionType, string entityType)
        {
            switch (actionType.ToLower())
            {
                case "edits":
                case "activates":
                case "inactivates":
                    await EditCharacteristicKindAsync(entityType);
                    break;

                default:
                    throw new ArgumentException($"Unsupported action '{actionType}' for Characteristic Kind.");
            }
        }

        /// <summary>
        /// Edits Characteristic Kind
        /// </summary>
        private async Task EditCharacteristicKindAsync(string entityType)
        {
            try
            {
                await ClickStandardEditButtonAsync(entityType);
                await EnterKindDetailsAsync();
                await ClickStandardUpdateButtonAsync(entityType);
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("edit Characteristic Kind", ex);
            }
        }

        /// <summary>
        /// Manages Characteristic Type operations (edit, activate, inactivate)
        /// </summary>
        private async Task ManageCharacteristicTypeAsync(string actionType, string entityType)
        {
            switch (actionType.ToLower())
            {
                case "edits":
                case "activates":
                case "inactivates":
                    await EditCharacteristicTypeAsync(entityType);
                    break;

                default:
                    throw new ArgumentException($"Unsupported action '{actionType}' for Characteristic Type.");
            }
        }

        /// <summary>
        /// Edits Characteristic Type
        /// </summary>
        private async Task EditCharacteristicTypeAsync(string entityType)
        {
            try
            {
                await ClickStandardEditButtonAsync(entityType);
                await EnterTypeDetailsAsync();
                await ClickStandardUpdateButtonAsync(entityType);
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("edit Characteristic Type", ex);
            }
        }



        /// <summary>
        /// Clicks the New Characteristic Kind button
        /// </summary>
        private async Task ClickOnNewCharKindButtonAsync()
        {
            try
            {
                await WebUIHelper.ClickAsync(scenarioContext, page, $"{Standard}.NewKindButton");
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Standard}.NewKindTab", TimeConstants.MediumWait, WaitForSelectorState.Visible);
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("click on New Kind button", ex);
            }
        }

        /// <summary>
        /// Clicks the New Type button
        /// </summary>
        private async Task ClickOnNewTypeButtonAsync()
        {
            try
            {
                await WebUIHelper.ClickAsync(scenarioContext, page, $"{Standard}.NewTypeTab");
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Standard}.TypeNameTextBox", TimeConstants.MediumWait, WaitForSelectorState.Visible);
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("click on New Type Button", ex);
            }
        }

        /// <summary>
        /// Clicks the Characteristic Standard Save button
        /// </summary>
        private async Task ClickCharStandardSaveButtonAsync()
        {
            try
            {
                await WebUIHelper.ClickAsync(scenarioContext, page, $"{Standard}.CharStandardSaveButton");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Playwright failure: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw ExceptionHandler.BuildCustomException("click Characteristic Standard Save button", ex);
            }
        }

        /// <summary>
        /// Clicks the Characteristic Standard Save button (with popup wait)
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task ClickCharacteristicStandardSaveButtonAsync()
        {
            try
            {
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Standard}.PopUpTitle", TimeConstants.MediumWait, WaitForSelectorState.Visible);
                await WebUIHelper.ClickAsync(scenarioContext, page, $"{Standard}.CharStandardSaveButton");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Playwright failure: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw ExceptionHandler.BuildCustomException("click Characteristic Standard Save button", ex);
            }
        }

        /// <summary>
        /// Clicks the Edit button for the specified entity type
        /// </summary>
        private async Task ClickStandardEditButtonAsync(string entityType)
        {
            try
            {
                switch (entityType.ToLowerInvariant())
                {
                    case "kind":
                        await WebUIHelper.ClickAsync(scenarioContext, page, $"{Standard}.CharactristicKindTab");
                        await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Standard}.CharacteristicEditButton", TimeConstants.MediumWait, WaitForSelectorState.Visible);
                        await WebUIHelper.ClickAsync(scenarioContext, page, $"{Standard}.CharacteristicEditButton");
                        break;

                    case "type":
                        await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Standard}.CharTypeTab", TimeConstants.MediumWait, WaitForSelectorState.Visible);
                        var typeTab = await WebUIHelper.IsVisibleAsync(scenarioContext, page, $"{Standard}.CharTypeTab");

                        if (typeTab)
                        {
                            await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Standard}.CharacteristicEditButton", TimeConstants.MediumWait, WaitForSelectorState.Visible);
                            await WebUIHelper.ClickAsync(scenarioContext, page, $"{Standard}.CharacteristicEditButton");
                        }
                        else
                        {
                            await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Standard}.CharacteristicTypeTab", TimeConstants.MediumWait, WaitForSelectorState.Visible);
                            await WebUIHelper.ClickAsync(scenarioContext, page, $"{Standard}.CharacteristicTypeTab");
                            await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Standard}.CharacteristicEditButton", TimeConstants.MediumWait, WaitForSelectorState.Visible);
                            await WebUIHelper.ClickAsync(scenarioContext, page, $"{Standard}.CharacteristicEditButton");
                        }

                        break;

                    default:
                        throw new ArgumentException($"Unsupported entity type: {entityType}");
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException($"click on Edit button for {entityType} tab", ex);
            }
        }

        /// <summary>
        /// Clicks the Update button for the specified entity type
        /// </summary>
        private async Task ClickStandardUpdateButtonAsync(string entityType)
        {
            try
            {
                string tabLocator = entityType.Equals("Kind", StringComparison.OrdinalIgnoreCase) ? $"{Standard}.CharKindTab" : $"{Standard}.CharTypeTab";

                await WebUIHelper.WaitForAsync(scenarioContext, page, tabLocator, TimeConstants.MediumWait, WaitForSelectorState.Visible);
                await WebUIHelper.IsVisibleAsync(scenarioContext, page, tabLocator);

                await WebUIHelper.ClickAsync(scenarioContext, page, $"{Standard}.CharacteristicUpdateButton");
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException($"click on Update button for {entityType} tab", ex);
            }
        }



        /// <summary>
        /// Ensures the Kind tab is expanded
        /// </summary>
        private async Task EnsureKindTabExpandedAsync()
        {
            try
            {
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Standard}.DefaultCharKindOptionSelected", TimeConstants.MediumWait, WaitForSelectorState.Visible);
                await WebUIHelper.ToHaveAttributeAsync(scenarioContext, page, $"{Standard}.DefaultCharKindOptionSelected", "aria-expanded", "true");
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("validate the Characteristic Kind is expanded", ex);
            }
        }

        /// <summary>
        /// Extracts the default Kind name
        /// </summary>
        private async Task<string> ExtractDefaultKindNameAsync()
        {
            try
            {
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Standard}.DefaultCharKindOption", TimeConstants.MediumWait, WaitForSelectorState.Visible);
                string displayedKindOption = await WebUIHelper.GetInnerTextAsync(scenarioContext, page, $"{Standard}.DefaultCharKindOption");
                Console.WriteLine($"[INFO] Extracted Characteristic Kind Option: {displayedKindOption}");
                return displayedKindOption;
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("extract default Characteristic Kind option", ex);
            }
        }

        /// <summary>
        /// Validates that the Kind tab is visible
        /// </summary>
        private async Task ValidateKindTabVisibilityAsync()
        {
            try
            {
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Standard}.KindTab", TimeConstants.MediumWait, WaitForSelectorState.Visible);
                await WebUIHelper.IsVisibleAsync(scenarioContext, page, $"{Standard}.KindTab");
                await WebUIHelper.WaitForInputValueAsync(scenarioContext, page, $"{Standard}.KindNameTextBox");
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("validate the Characteristic Kind tab is visible and stable", ex);
            }
        }

        /// <summary>
        /// Validates a characteristic field value
        /// </summary>
        private async Task ValidateCharacteristicFieldAsync(string fieldName, string locator, string expectedValue = null)
        {
            try
            {
                string actualValue = await WebUIHelper.GetInputValueAsync(scenarioContext, page, locator);
                Console.WriteLine($"[INFO] Extracted {fieldName}: {actualValue}");

                if (!string.IsNullOrWhiteSpace(expectedValue))
                {
                    ValidationHelper.AssertEqual(locator, expectedValue, actualValue, $"The {fieldName} does not match the expected value.");
                }
                else
                {
                    ValidationHelper.FailIfNullOrEmpty(locator, actualValue, $"{fieldName} should not be null or empty");
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("validate the Characteristic Kind or Characteristic Type", ex);
            }
        }

        /// <summary>
        /// Validates Characteristic Kind details
        /// </summary>
        private async Task ValidateCharKindDetailsAsync(bool isSearch, Func<string, string, bool> shouldBreakLoop)
        {
            try
            {
                await EnsureKindTabExpandedAsync();

                string displayedKindOption = await ExtractDefaultKindNameAsync();

                await ValidateKindTabVisibilityAsync();

                string actualKindName = string.Empty;

                for (int attempt = 1; attempt <= 5; attempt++)
                {
                    actualKindName = await WebUIHelper.GetInputValueAsync(scenarioContext, page, $"{Standard}.KindNameTextBox");
                    Console.WriteLine($"[DEBUG] Attempt {attempt}: KindNameTextBox value = {actualKindName}");

                    if (!string.IsNullOrWhiteSpace(actualKindName) && shouldBreakLoop(actualKindName, displayedKindOption))
                    {
                        break;
                    }

                    await WebUIHelper.WaitAsync(TimeConstants.DefaultPollingWait);
                }

                Console.WriteLine($"[INFO] Final stabilized Kind Name from Kind Tab: {actualKindName}");

                string expectedKindName = (isSearch && featureContext.ContainsKey("RandomKindName")) ? featureContext["RandomKindName"].ToString() : displayedKindOption;

                Console.WriteLine($"[INFO] Validating using {(isSearch ? "RandomKindName" : "DefaultKindOption")}: {expectedKindName}");

                featureContext["ValidatedKindOption"] = displayedKindOption;
                featureContext["ValidatedKindName"] = actualKindName;

                var kindFields = new List<(string Field, string Locator, Func<string> Expected)>
                {
     ("Kind Name", $"{Standard}.KindNameTextBox", () => expectedKindName),
     ("Kind Rank", $"{Standard}.KindRankTextBox", null),
     ("Kind Rank Memo", $"{Standard}.KindRankMemoTextBox", null),
     ("Kind Description", $"{Standard}.KindDescriptionTextBox", null),
     ("Translation Action DropDown", $"{Standard}.TranslationActionDropDown", null)
                };

                foreach (var (field, locator, expectedProvider) in kindFields)
                {
                    string expected = expectedProvider?.Invoke();
                    await ValidateCharacteristicFieldAsync(field, locator, expected);
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("Failed to validate Characteristic Kind details on the Kind tab", ex);
            }
        }



        /// <summary>
        /// Validates the default Business Priority value
        /// </summary>
        private async Task ValidateBusinessPriorityDefaultAsync()
        {
            try
            {
                testCaseName = scenarioContext.ScenarioInfo.Title;

                var (isRestricted, isItemLinked, isLinkedLOB) = await ReadCheckboxStatesFromJsonAsync();

                string expectedValue = CalculateExpectedBusinessPriority(isRestricted, isItemLinked, isLinkedLOB);

                await AssertBusinessPriorityAsync(expectedValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed while validating Business Priority default.\nTestCase: {testCaseName}\nError: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw ExceptionHandler.BuildCustomException("validate default Business Priority value", ex);
            }
        }

        /// <summary>
        /// Reads checkbox states from JSON test data
        /// </summary>
        private async Task<(bool isRestricted, bool isItemLinked, bool isLinkedLOB)> ReadCheckboxStatesFromJsonAsync()
        {
            try
            {
                var businessPriorityMappings = new Dictionary<string, (string locatorKey, string controlType)>
      {
  { "RestrictedCheckBox", ($"{Standard}.RestrictedCheckBox", "checkbox") },
  { "ItemLinkedToTypeCheckbox", ($"{Standard}.ItemLinkedToTypeCheckbox", "checkbox") },
  { "LinkedToLOBCheckBox", ($"{Standard}.LinkedToLOBCheckBox", "checkbox") }
      };

                bool isRestricted = false, isItemLinked = false, isLinkedLOB = false;

                foreach (var kvp in businessPriorityMappings)
                {
                    string fieldName = kvp.Key;
                    string keyPath = $"testCases.{testCaseName}.{fieldName}";

                    string fieldValue;
                    try
                    {
                        fieldValue = Utility.ReadDataFromJsonString(scenarioContext, keyPath);
                    }
                    catch (KeyNotFoundException)
                    {
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(fieldValue))
                    {
                        continue;
                    }

                    bool isChecked = fieldValue.Equals("true", StringComparison.OrdinalIgnoreCase);

                    switch (fieldName)
                    {
                        case "RestrictedCheckBox":
                            isRestricted = isChecked;
                            break;
                        case "ItemLinkedToTypeCheckbox":
                            isItemLinked = isChecked;
                            break;
                        case "LinkedToLOBCheckBox":
                            isLinkedLOB = isChecked;
                            break;
                    }
                }

                return await Task.FromResult((isRestricted, isItemLinked, isLinkedLOB));
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("read checkbox states", ex);
            }
        }

        /// <summary>
        /// Calculates the expected Business Priority based on checkbox states
        /// </summary>
        private string CalculateExpectedBusinessPriority(bool isRestricted, bool isItemLinked, bool isLinkedLOB)
        {
            try
            {
                if (isRestricted && isItemLinked && isLinkedLOB)
                {
                    return AdminConstants.IsNecessary;
                }
                else if (isRestricted)
                {
                    return AdminConstants.IsOptional;
                }
                else if (isItemLinked && isLinkedLOB)
                {
                    return AdminConstants.IsStandard;
                }
                else
                {
                    return AdminConstants.IsOptional;
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("calculate expected Business Priority", ex);
            }
        }

        /// <summary>
        /// Asserts the Business Priority value matches expected
        /// </summary>
        private async Task AssertBusinessPriorityAsync(string expectedValue)
        {
            try
            {
                string actualValue = await WebUIHelper.GetSelectedDropDownValueAsync(scenarioContext, page, $"{Standard}.BusinessPriorityDropDown");

                ValidationHelper.AssertEqual($"{Standard}.BusinessPriorityDropDown", expectedValue, actualValue, $"[ASSERT FAILED] Expected Business Priority default: '{expectedValue}', but found: '{actualValue}'.");

                Console.WriteLine($"[INFO] Validated Business Priority default value: '{expectedValue}'");
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("validate the Business Priority", ex);
            }
        }
    }
}

using Microsoft.Playwright;
using NUnit.Framework;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Utilities;
using TechTalk.SpecFlow;
using Utilities;
namespace PIMQAAutomation.Modules.Items.Actions
{
   

    /// <summary>
    /// Contains business logic for Admin Value (LOV) operations
    /// </summary>
    public class AdminValueActions
    {
        private readonly IPage page;
        private readonly ScenarioContext scenarioContext;
        private readonly FeatureContext featureContext;
        private const string Value = "AdminValue";
        private const string Standard = "AdminStandardCharacteristics";
        private string testCaseName = string.Empty;
        private string defaultKindOption;
        private string defaultTypeOption;

        public AdminValueActions(IPage page, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            this.page = page;
            this.scenarioContext = scenarioContext;
            this.featureContext = featureContext;
        }


        /// <summary>
        /// Returns field mappings for Characteristic LOV
        /// </summary>
        private Dictionary<string, (string locatorKey, string controlType)> GetCharLOVFieldMappings()
        {
            var fieldMappings = new Dictionary<string, (string locatorKey, string controlType)>
    {
    { "LOVNameTextBox", ($"{Value}.LOVNameTextBox", "textbox") },
    { "LOVNameDescriptionTextBox", ($"{Value}.LOVNameDescriptionTextBox", "textbox") },
    { "LOVActiveCheckBox", ($"{Value}.LOVActiveCheckBox", "checkbox") },
    { "LOVInternalNotes", ($"{Value}.LOVInternalNotes", "textbox") }
    };
            return fieldMappings;
        }



        /// <summary>
        /// Validates that the Value Characteristics page header is visible
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task ValidateValuePageHeaderAsync()
        {
            try
            {
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Value}.ValuePageHeader", TimeConstants.MediumWait, WaitForSelectorState.Visible);
                var isValuePageHeaderVisible = await WebUIHelper.IsVisibleAsync(scenarioContext, page, $"{Value}.ValuePageHeader");
                Assert.IsTrue(isValuePageHeaderVisible, "Value Characteristics page header is not visible.");
            }
            catch (Exception ex)
            {
                throw new CustomException("validate the Value Characteristics page header. The header was not found or not visible within the timeout.", ex);
            }
        }

        /// <summary>
        /// Verifies Characteristic Kind and LOV details
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task VerifyCharKindAndLOVDetailsAsync()
        {
            try
            {
                await WebUIHelper.ToHaveAttributeAsync(scenarioContext, page, $"{Standard}.DefaultCharKindOptionSelected", "aria-expanded", "true");
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Standard}.DefaultCharTypeOption", TimeConstants.MediumWait, WaitForSelectorState.Visible);
                await WebUIHelper.IsVisibleAsync(scenarioContext, page, $"{Standard}.DefaultCharTypeOptionSelected");

                defaultKindOption = await WebUIHelper.GetInnerTextAsync(scenarioContext, page, $"{Standard}.DefaultCharKindOption");
                Console.WriteLine($"The extracted Characteristic Kind Option is: {defaultKindOption}");

                defaultTypeOption = await WebUIHelper.GetInnerTextAsync(scenarioContext, page, $"{Standard}.DefaultCharTypeOption");
                Console.WriteLine($"[INFO] Extracted default Characteristic Type Option: {defaultTypeOption}");

                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Value}.DefaultSelectedCharValue", TimeConstants.MediumWait, WaitForSelectorState.Visible);

                string defaultLOVOption = await WebUIHelper.GetInnerTextAsync(scenarioContext, page, $"{Value}.CharValueOption");
                Console.WriteLine($"[INFO] The extracted Characteristic LOV Option is: {defaultLOVOption}");

                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Value}.LovDetailsTab", TimeConstants.MediumWait, WaitForSelectorState.Visible);

                string actualLOVName = await WebUIHelper.GetInputValueAsync(scenarioContext, page, $"{Value}.LOVNameTextBox");
                Console.WriteLine($"The extracted LOV Name is: {actualLOVName}");

                string lOVDescription = await WebUIHelper.GetInputValueAsync(scenarioContext, page, $"{Value}.LOVNameDescriptionTextBox");
                Console.WriteLine($"The extracted Characteristic LOV Description is: {lOVDescription}");

                string lOVInternalNotes = await WebUIHelper.GetInputValueAsync(scenarioContext, page, $"{Value}.LOVInternalNotes");
                Console.WriteLine($"The extracted Characteristic LOV Internal Notes is: {lOVInternalNotes}");

                ValidationHelper.AssertEqual($"{Value}.LOVNameTextBox", defaultLOVOption, actualLOVName, "The selected LOV option does not match the LOV Name.");
                ValidationHelper.FailIfNullOrEmpty($"{Value}.LOVNameDescriptionTextBox", lOVDescription, "LOV Description should not be null");
                ValidationHelper.FailIfNullOrEmpty($"{Value}.LOVInternalNotes", lOVInternalNotes, "LOV Internal Notes should not be null");
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("validate default Characteristic Kind and LOV details", ex);
            }
        }



        /// <summary>
        /// Creates a new LOV Value
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task CreateNewLOVValueAsync()
        {
            try
            {
                testCaseName = scenarioContext.ScenarioInfo.Title;

                await page.WaitForTimeoutAsync(TimeConstants.MinimumWait);
                await WebUIHelper.ClickAsync(scenarioContext, page, $"{Value}.NewLOVButton");
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Value}.LovDetailsTab", TimeConstants.MediumWait, WaitForSelectorState.Visible);

                var fieldMappings = GetCharLOVFieldMappings();
                await WebUIHelper.FillFieldsFromTestDataAsync(page, scenarioContext, featureContext, testCaseName, fieldMappings, randomFieldKey: "LOVNameTextBox", randomContextKey: "RandomLOVName");

                await ClickSaveButtonAsync();
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("create new LOV Value", ex);
            }
        }

        /// <summary>
        /// Creates new LOV Value without mandatory details to generate error messages
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task CreateNewValueErrorMsgsAsync()
        {
            try
            {
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Value}.NewLOVButton", TimeConstants.MinimumWait, WaitForSelectorState.Visible);
                await ClickNewLOVButtonAsync();
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Value}.ValueSaveButton", TimeConstants.MinimumWait, WaitForSelectorState.Visible);
                await ClickLOVSaveButtonAsync();
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("generate LOV error messages", ex);
            }
        }

        /// <summary>
        /// Searches for Characteristic Kind
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task SearchKindAsync(Table fieldNames)
        {
            try
            {
                testCaseName = scenarioContext.ScenarioInfo.Title;

                if (!featureContext.ContainsKey("RandomKindName"))
                {
                    throw new CustomException("[ERROR] RandomKindName is missing in FeatureContext. Ensure Kind creation step stored it.");
                }

                string randomKindName = featureContext["RandomKindName"].ToString();
                Console.WriteLine($"[INFO] Using RandomKindName from FeatureContext: {randomKindName}");

                foreach (var row in fieldNames.Rows)
                {
                    string fieldName = row["FieldName"].Trim();

                    switch (fieldName)
                    {
                        case "LOVCharKindSearchTextBox":
                            await WebUIHelper.FillCharByCharAndPressEnterAsync(scenarioContext, page, $"{Value}.LOVCharKindSearchTextBox", randomKindName);
                            await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Standard}.DefaultCharKindOptionSelected", TimeConstants.MediumWait, WaitForSelectorState.Visible);
                            break;

                        case "CharKindSearchTextBox":
                            Console.WriteLine($"[INFO] Searching Characteristic Kind: {randomKindName}");
                            await WebUIHelper.FillAndPressEnterAsync(scenarioContext, page, $"{Standard}.CharKindSearchTextBox", randomKindName);
                            await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Standard}.DefaultCharKindOptionSelected", TimeConstants.MediumWait, WaitForSelectorState.Visible);
                            break;

                        default:
                            Console.WriteLine($"[WARN] No validation rule defined for field '{fieldName}'. Skipping.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Playwright failure: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw ExceptionHandler.BuildCustomException("validate Characteristic Kind details", ex, testCaseName);
            }
        }



        /// <summary>
        /// Clicks the Save button
        /// </summary>
        private async Task ClickSaveButtonAsync()
        {
            try
            {
                await page.WaitForTimeoutAsync(TimeConstants.MinimumWait);
                await WebUIHelper.ClickAsync(scenarioContext, page, $"{Value}.ValueSaveButton");
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Value}.PopUpYesButton", TimeConstants.MediumWait, WaitForSelectorState.Visible);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed while clicking on the save button. Error: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw ExceptionHandler.BuildCustomException("click on the Save button", ex);
            }
        }

        /// <summary>
        /// Clicks the New LOV button
        /// </summary>
        private async Task ClickNewLOVButtonAsync()
        {
            try
            {
                await WebUIHelper.ClickAsync(scenarioContext, page, $"{Value}.NewLOVButton");
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Value}.LovDetailsTab", TimeConstants.MinimumWait, WaitForSelectorState.Visible);
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("click on New LOV button", ex);
            }
        }

        /// <summary>
        /// Clicks the LOV Save button
        /// </summary>
        private async Task ClickLOVSaveButtonAsync()
        {
            try
            {
                await WebUIHelper.ClickAsync(scenarioContext, page, $"{Value}.ValueSaveButton");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Playwright failure: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw ExceptionHandler.BuildCustomException("click Characteristic Standard Save button", ex);
            }
        }
    }
}

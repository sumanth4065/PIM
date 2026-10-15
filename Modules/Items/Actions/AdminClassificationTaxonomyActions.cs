using global::PIMQAAutomation.Modules.Common.Constants;
using global::PIMQAAutomation.Modules.Items.Constants;
using global::Utilities;
using Microsoft.Playwright;
using PIMQAAutomation.Utilities;
using TechTalk.SpecFlow;
using Utilities;
namespace PIMQAAutomation.Modules.Items.Actions
    {


    /// <summary>
    /// Contains business logic for Admin Classification Taxonomy operations
    /// </summary>
    public class AdminClassificationTaxonomyActions
        {
        private const string Common = "AdminCommonActions";
        private const string Standard = "AdminStandardCharacteristics";
        private const string Taxonomy = "AdminClassificationTaxonomy";
        private readonly FeatureContext featureContext;
        private readonly IPage page;
        private readonly ScenarioContext scenarioContext;
        private string testCaseName = string.Empty;

        public AdminClassificationTaxonomyActions(IPage page, ScenarioContext scenarioContext, FeatureContext featureContext)
            {
            this.page = page;
            this.scenarioContext = scenarioContext;
            this.featureContext = featureContext;
            }


        /// <summary>
        /// Creates a new taxonomy
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task CreateTaxonomyAsync(Table fieldname)
            {
            try
                {
                await ClickTaxonomyNewButtonAsync();
                await EnterTaxonomyDetailsAsync(fieldname);
                await ClickTaxonomySaveButtonAsync();
                }
            catch (Exception ex)
                {
                throw ExceptionHandler.BuildCustomException("create taxonomy", ex);
                }
            }

        /// <summary>
        /// Creates taxonomy without mandatory fields to generate error messages
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task CreateTaxonomyErrorMsgsAsync()
            {
            try
                {
                await ClickTaxonomyNewButtonAsync();
                await ClickTaxonomySaveButtonAsync();
                }
            catch (Exception ex)
                {
                throw ExceptionHandler.BuildCustomException("generate mandatory field error messages", ex);
                }
            }

        /// <summary>
        /// Creates taxonomy with warning message (for cancel scenarios)
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task CreateTaxonomyWarningMsgAsync(Table fieldname)
            {
            try
                {
                await EnterTaxonomyDetailsAsync(fieldname);
                await ClickTaxonomyCancelButtonAsync();
                }
            catch (Exception ex)
                {
                throw ExceptionHandler.BuildCustomException("create taxonomy warning message", ex);
                }
            }

        /// <summary>
        /// Edits an existing taxonomy
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task EditTaxonomyAsync(Table fieldname)
            {
            try
                {
                await ClickEditTaxonomyButtonAsync();
                await EnterTaxonomyDetailsAsync(fieldname);
                await ClickUpdateTaxonomyButtonAsync();
                }
            catch (Exception ex)
                {
                throw ExceptionHandler.BuildCustomException("edit taxonomy", ex);
                }
            }

        /// <summary>
        /// Extracts default Kind name
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task<string> ExtractDefaultKindNameAsync()
            {
            try
                {
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Standard}.DefaultCharKindOption", TimeConstants.MinimumWait, WaitForSelectorState.Visible);
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
        /// Extracts searchbox header
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task ExtractSearchboxHeaderAsync(Table fieldNames, string expectedSearchTitle)
            {
            string actualSearchTitle = string.Empty;

            try
                {
                foreach (var row in fieldNames.Rows)
                    {
                    await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Taxonomy}.LovSearchBoxHeading", TimeConstants.MinimumWait, WaitForSelectorState.Visible);
                    actualSearchTitle = await WebUIHelper.GetInnerTextAsync(scenarioContext, page, $"{Taxonomy}.LovSearchBoxHeading");
                    if (actualSearchTitle != null)
                        {
                        ValidationHelper.AssertEqual($"{Taxonomy}.LovSearchBoxHeading", expectedSearchTitle, actualSearchTitle);
                        }
                    }
                }
            catch (Exception ex)
                {
                throw ExceptionHandler.BuildCustomException($"validate the {expectedSearchTitle}. Expected to be {expectedSearchTitle} but found {actualSearchTitle}", ex);
                }
            }

        /// <summary>
        /// Navigates away without saving
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task NavigateAwayWithoutSavingAsync(Table fieldname)
            {
            try
                {
                await ClickTaxonomyNewButtonAsync();
                await EnterTaxonomyDetailsAsync(fieldname);
                }
            catch (Exception ex)
                {
                throw ExceptionHandler.BuildCustomException("create taxonomy without saving", ex);
                }
            }

        /// <summary>
        /// Reactivates a taxonomy by checking the Active checkbox
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task ReactivateTaxonomyAsync()
            {
            try
                {
                await ClickEditTaxonomyButtonAsync();
                await WebUIHelper.HandleCheckboxAsync(scenarioContext, page, $"{Taxonomy}.LovActiveCheckBox", "true", "Active Checkbox");
                await ClickUpdateTaxonomyButtonAsync();
                }
            catch (Exception ex)
                {
                throw ExceptionHandler.BuildCustomException("reactivate the Active Checkbox", ex);
                }
            }

        /// <summary>
        /// Searches for entity
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task SearchForEntityAsync(Table fieldNames)
            {
            try
                {
                testCaseName = scenarioContext.ScenarioInfo.Title;

                foreach (var row in fieldNames.Rows)
                    {
                    string fieldName = row["FieldName"].Trim();
                    string fieldValue = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.{fieldName}");

                    switch (fieldName)
                        {
                        case SwitchcaseValues.Oem:
                            await WebUIHelper.FillAndPressEnterAsync(scenarioContext, page, $"{Taxonomy}.", fieldValue);
                            break;

                        case SwitchcaseValues.Family:
                            await WebUIHelper.FillAndPressEnterAsync(scenarioContext, page, $"{Taxonomy}.", fieldValue);
                            break;
                        }
                    }

                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Taxonomy}.serachBox", TimeConstants.MediumWait, WaitForSelectorState.Visible);
                }
            catch (Exception ex)
                {
                throw ExceptionHandler.BuildCustomException("Search Entity", ex);
                }
            }

        /// <summary>
        /// Searches taxonomy by hierarchy
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task SearchTaxonomyByHierarchyAsync(Table fieldNames)
            {
            try
                {
                testCaseName = scenarioContext.ScenarioInfo.Title;

                if (fieldNames?.Rows == null || !fieldNames.Rows.Any())
                    {
                    throw new ArgumentException("The provided Table has no rows to process.");
                    }

                Console.WriteLine($"[INFO] Executing SearchTaxonomyByHierarchy for test case: {testCaseName}");

                foreach (var row in fieldNames.Rows)
                    {
                    string fieldName = row["FieldName"].Trim();
                    string fieldValue = GetFieldValue(fieldName);

                    Console.WriteLine($"[INFO] Searching taxonomy '{fieldName}' with value '{fieldValue}'");

                    await WebUIHelper.WaitForElementToDisappearAsync(scenarioContext, page, $"{Common}.LoadSpinner", 80);

                    await SearchAndSelectLOVAsync(fieldValue);
                    await ValidateLOVAsync(fieldValue);

                    await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Taxonomy}.LovItemRightIcon",
                   TimeConstants.MinimumWait, WaitForSelectorState.Visible);

                    if (fieldName != "EditClassSearchBox")
                        {
                        await WebUIHelper.ClickAsync(scenarioContext, page, $"{Taxonomy}.LovItemRightIcon");
                        }

                    Console.WriteLine($"[INFO] Successfully validated and expanded taxonomy '{fieldName}' ? '{fieldValue}'");
                    }
                }
            catch (Exception ex)
                {
                Console.WriteLine($"[ERROR] Taxonomy hierarchy search failed: {ex.Message}");
                throw ExceptionHandler.BuildCustomException("Error occurred during taxonomy hierarchy search", ex, testCaseName);
                }
            }

        /// <summary>
        /// Selects a taxonomy type
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task SelectTaxonomyAsync(string taxonomyType)
            {
            try
                {
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Taxonomy}.TaxonomyType", TimeConstants.MinimumWait, taxonomyType, WaitForSelectorState.Visible);
                await WebUIHelper.ClickAsync(scenarioContext, page, $"{Taxonomy}.TaxonomyType", taxonomyType);
                Console.WriteLine($"[INFO] Successfully selected taxonomy: {taxonomyType}");
                }
            catch (Exception ex)
                {
                throw ExceptionHandler.BuildCustomException("select Taxonomy Type", ex);
                }
            }

        /// <summary>
        /// Validates the active taxonomy tab
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task ValidateActiveTaxonomyTabAsync(string expectedValue)
            {
            try
                {
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Taxonomy}.LovActiveTab", TimeConstants.MinimumWait, WaitForSelectorState.Visible);
                string actualTab = await WebUIHelper.GetInnerTextAsync(scenarioContext, page, $"{Taxonomy}.LovActiveTab");
                ValidationHelper.AssertEqual($"{Taxonomy}.LovActiveTab", expectedValue, actualTab, "The expected Tab Name is not visible");
                }
            catch (Exception ex)
                {
                throw ExceptionHandler.BuildCustomException("validate active taxonomy tab", ex);
                }
            }

        /// <summary>
        /// Validates selected taxonomy option
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task ValidateSelectedTaxonomyOptionAsync()
            {
            try
                {
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Taxonomy}.DefaultSelctedTaxonomy", TimeConstants.MinimumWait, WaitForSelectorState.Visible);
                string taxonomyOption = (await WebUIHelper.GetInnerTextAsync(scenarioContext, page, $"{Taxonomy}.DefaultSelctedTaxonomy")).Trim();

                if (string.Equals(taxonomyOption, TaxonomyCharKindOptions.ProductTaxonomy, StringComparison.OrdinalIgnoreCase))
                    {
                    ValidationHelper.AssertEqual($"{Taxonomy}.DefaultSelctedTaxonomy", TaxonomyCharKindOptions.ProductTaxonomy, taxonomyOption);
                    }
                else if (string.Equals(taxonomyOption, TaxonomyCharKindOptions.ServiceTaxonomy, StringComparison.OrdinalIgnoreCase))
                    {
                    ValidationHelper.AssertEqual($"{Taxonomy}.DefaultSelctedTaxonomy", TaxonomyCharKindOptions.ServiceTaxonomy, taxonomyOption);
                    }
                else
                    {
                    throw new Exception($"Unexpected taxonomy selection: {taxonomyOption}");
                    }
                }
            catch (Exception ex)
                {
                throw ExceptionHandler.BuildCustomException("validate selected Taxonomy option", ex);
                }
            }

        /// <summary>
        /// Verifies default selected taxonomy
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task VerifyDefaultSelectedTaxonomyAsync()
            {
            try
                {
                string selectedTaxonomy = await ExtractDefaultTaxonomyNameAsync();
                Console.WriteLine($"[INFO] Selected Taxonomy: {selectedTaxonomy}");

                if (string.Equals(selectedTaxonomy, TaxonomyCharKindOptions.ProductTaxonomy, StringComparison.OrdinalIgnoreCase))
                    {
                    await VerifySearchBoxTitleAsync(TaxonomyCharKindOptions.TaxonomySearchBoxTitle.Oem, "Product Taxonomy");
                    }
                else if (string.Equals(selectedTaxonomy, TaxonomyCharKindOptions.ServiceTaxonomy, StringComparison.OrdinalIgnoreCase))
                    {
                    await VerifySearchBoxTitleAsync(TaxonomyCharKindOptions.TaxonomySearchBoxTitle.Class, "Service Taxonomy");
                    }
                else
                    {
                    throw new Exception($"Unexpected taxonomy selection: {selectedTaxonomy}");
                    }
                }
            catch (Exception ex)
                {
                throw ExceptionHandler.BuildCustomException("Validate Product or Service Taxonomy and the corresponding Search Box title (OEM/Class)", ex);
                }
            }

        /// <summary>
        /// Verifies page header is visible
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task VerifyPageHeaderAsync()
            {
            try
                {
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Taxonomy}.LovKindHeading", TimeConstants.MinimumWait, WaitForSelectorState.Visible);

                bool isVisible = await WebUIHelper.IsVisibleAsync(scenarioContext, page, $"{Taxonomy}.LovKindHeading");

                if (!isVisible)
                    {
                    ValidationHelper.Fail("Expected Characteristic Kinds title to be visible. But was not visible.");
                    }
                }
            catch (Exception ex)
                {
                throw ExceptionHandler.BuildCustomException("validate the Characteristic Kinds title", ex);
                }
            }

        /// <summary>
        /// Verifies Product and Service Taxonomy are visible
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task VerifyProductAndServiceTaxonomyAsync()
            {
            try
                {
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Taxonomy}.ProductTaxonomy", TimeConstants.MinimumWait, WaitForSelectorState.Visible);

                bool isVisible = (await WebUIHelper.IsVisibleAsync(scenarioContext, page, $"{Taxonomy}.ProductTaxonomy")) &&
                 (await WebUIHelper.IsVisibleAsync(scenarioContext, page, $"{Taxonomy}.ServiceTaxonomy"));
                if (!isVisible)
                    {
                    ValidationHelper.Fail("The Product Taxonomy and Service Taxonomy are expected to be visible. But not visible");
                    }
                }
            catch (Exception ex)
                {
                throw ExceptionHandler.BuildCustomException("validate the Product Taxonomy and Service Taxonomy", ex);
                }
            }

        private async Task ClickEditTaxonomyButtonAsync()
            {
            try
                {
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Taxonomy}.LovEditButton", TimeConstants.MinimumWait, WaitForSelectorState.Visible);
                await WebUIHelper.ClickAsync(scenarioContext, page, $"{Taxonomy}.LovEditButton");
                }
            catch (Exception ex)
                {
                Console.WriteLine($"[ERROR] Playwright failure: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw ExceptionHandler.BuildCustomException("Click Taxonomy Edit button", ex);
                }
            }

        private async Task ClickTaxonomyCancelButtonAsync()
            {
            try
                {
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Taxonomy}.LovCancelButton", TimeConstants.MinimumWait, WaitForSelectorState.Visible);
                await WebUIHelper.ClickAsync(scenarioContext, page, $"{Taxonomy}.LovCancelButton");
                }
            catch (Exception ex)
                {
                Console.WriteLine($"[ERROR] Playwright failure: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw ExceptionHandler.BuildCustomException("Click Taxonomy Cancel button", ex);
                }
            }

        private async Task ClickTaxonomyNewButtonAsync()
            {
            try
                {
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Taxonomy}.AddNewLovButton", TimeConstants.MinimumWait, WaitForSelectorState.Visible);
                await WebUIHelper.ClickAsync(scenarioContext, page, $"{Taxonomy}.AddNewLovButton");
                }
            catch (Exception ex)
                {
                Console.WriteLine($"[ERROR] Playwright failure: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw ExceptionHandler.BuildCustomException("Click Taxonomy Standard New button", ex);
                }
            }

        private async Task ClickTaxonomySaveButtonAsync()
            {
            try
                {
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Taxonomy}.LovSaveButton", TimeConstants.MinimumWait, WaitForSelectorState.Visible);
                await WebUIHelper.ClickAsync(scenarioContext, page, $"{Taxonomy}.LovSaveButton");
                }
            catch (Exception ex)
                {
                Console.WriteLine($"[ERROR] Playwright failure: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw ExceptionHandler.BuildCustomException("Click Taxonomy Standard Save button", ex);
                }
            }

        private async Task ClickUpdateTaxonomyButtonAsync()
            {
            try
                {
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Taxonomy}.LovUpdateButton", TimeConstants.MinimumWait, WaitForSelectorState.Visible);
                await WebUIHelper.ClickAsync(scenarioContext, page, $"{Taxonomy}.LovUpdateButton");
                }
            catch (Exception ex)
                {
                Console.WriteLine($"[ERROR] Playwright failure: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw ExceptionHandler.BuildCustomException("Click Taxonomy Update button", ex);
                }
            }

        /// <summary>
        /// Enters taxonomy details based on table
        /// </summary>
        private async Task EnterTaxonomyDetailsAsync(Table fieldname)
            {
            try
                {
                testCaseName = scenarioContext.ScenarioInfo.Title;

                if (fieldname?.Rows == null || !fieldname.Rows.Any())
                    {
                    throw new ArgumentException("The provided Table has no rows to process.");
                    }

                var fieldMappings = GetTaxonomyFieldMapping();

                foreach (var row in fieldname.Rows)
                    {
                    string tabType = row[0].Trim().ToLowerInvariant();

                    string randomContextKey = tabType switch
                        {
                            "oem" => "RandomOemName",
                            "family" => "RandomFamilyName",
                            "line" => "RandomLineName",
                            "group" => "RandomGroupName",
                            "class" => "RandomClassName",
                            "domain" => "RandomDomainName",
                            "type" => "RandomTypeName",
                            "item" => "RandomItemName",
                            _ => throw new ArgumentException($"Unknown taxonomy type: {tabType}")
                            };

                    Console.WriteLine($"[INFO] Creating taxonomy for tab: {tabType}");

                    await ValidateActiveTaxonomyTabAsync(tabType);

                    await WebUIHelper.FillFieldsFromTestDataAsync(
                    page,
                    scenarioContext,
                    featureContext,
                    testCaseName,
                    fieldMappings,
                    randomFieldKey: "LovNameTextBox",
                    randomContextKey: randomContextKey);

                    Console.WriteLine($"[INFO] Successfully created {tabType} taxonomy and stored in FeatureContext as '{randomContextKey}'.");
                    }
                }
            catch (Exception ex)
                {
                Console.WriteLine($"[ERROR] Error occurred while creating taxonomy: {ex.Message}");
                throw ExceptionHandler.BuildCustomException("Error occurred while creating taxonomy", ex);
                }
            }

        /// <summary>
        /// Extracts default taxonomy name
        /// </summary>
        private async Task<string> ExtractDefaultTaxonomyNameAsync()
            {
            try
                {
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Standard}.DefaultSelctedTaxonomy", TimeConstants.MinimumWait, WaitForSelectorState.Visible);
                string displayedTaxonomyOption = (await WebUIHelper.GetInnerTextAsync(scenarioContext, page, $"{Standard}.DefaultSelctedTaxonomy")).Trim();
                Console.WriteLine($"[INFO] Extracted Taxonomy Option: {displayedTaxonomyOption}");
                return displayedTaxonomyOption;
                }
            catch (Exception ex)
                {
                throw ExceptionHandler.BuildCustomException("validate extracted default Taxonomy option", ex);
                }
            }

        /// <summary>
        /// Gets field value from context or test data
        /// </summary>
        private string GetFieldValue(string fieldName)
            {
            switch (fieldName)
                {
                case "OemSearchBox":
                    return Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.OemSearchBox");

                default:
                    var searchKeyMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
      {
        { "FamilySearchBox", "RandomFamilyName" },
        { "LineSearchBox", "RandomLineName" },
        { "GroupSearchBox", "RandomGroupName" },
        { "ClassSearchBox", "RandomClassName" },
        { "DomainSearchBox", "RandomDomainName" },
        { "TypeSearchBox", "RandomTypeName" },
        { "ItemSearchBox", "RandomItemName" },
        { "EditClassSearchBox", "RandomClassName" }
      };

                    if (!searchKeyMap.TryGetValue(fieldName, out string randomKey))
                        {
                        throw new ArgumentException($"Unknown taxonomy field: {fieldName}");
                        }

                    if (!featureContext.ContainsKey(randomKey))
                        {
                        throw new KeyNotFoundException($"{randomKey} not found in FeatureContext.");
                        }

                    return featureContext[randomKey]?.ToString();
                }
            }

        /// <summary>
        /// Returns field mappings for Taxonomy
        /// </summary>
        private Dictionary<string, (string locatorKey, string controlType)> GetTaxonomyFieldMapping()
            {
            var fieldMappings = new Dictionary<string, (string locatorKey, string controlType)>
        {
      { "LovNameTextBox", ($"{Taxonomy}.LovNameTextBox", "textbox") },
      { "LovDescriptionTextBox", ($"{Taxonomy}.LovDescriptionTextBox", "textbox") },
      { "LovInternalNotesTextBox", ($"{Taxonomy}.LovInternalNotesTextBox", "textbox") },
      { "LovActiveCheckBox", ($"{Taxonomy}.LovActiveCheckBox", "checkbox") },
      { "LovInActiveCheckBox", ($"{Taxonomy}.LovInActiveCheckBox", "checkbox") },
      { "CodeTextBox", ($"{Taxonomy}.CodeTextBox", "textbox") }
        };
            return fieldMappings;
            }
        /// <summary>
        /// Searches and selects LOV
        /// </summary>
        private async Task SearchAndSelectLOVAsync(string fieldValue)
            {
            await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Taxonomy}.LovsTypeSearchBox",
                  TimeConstants.MinimumWait, WaitForSelectorState.Visible);

            await WebUIHelper.ClickAndPressSequenceAsync(scenarioContext, page,
          $"{Taxonomy}.LovsTypeSearchBox", fieldValue);

            await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Taxonomy}.LovDefaultOption",
             TimeConstants.MinimumWait, fieldValue, WaitForSelectorState.Visible);

            await WebUIHelper.ClickAsync(scenarioContext, page,
                   $"{Taxonomy}.LovDefaultOption", fieldValue);
            }

        /// <summary>
        /// Validates LOV details
        /// </summary>
        private async Task ValidateLOVAsync(string expectedValue)
            {
            await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Taxonomy}.LovNameTextBox",
      TimeConstants.MinimumWait, WaitForSelectorState.Visible);

            await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Taxonomy}.LovDescriptionTextBox",
        TimeConstants.MinimumWait, WaitForSelectorState.Visible);

            string lovName = await WebUIHelper.GetInputValueAsync(scenarioContext, page,
          $"{Taxonomy}.LovNameTextBox");

            string lovDescription = await WebUIHelper.GetInputValueAsync(scenarioContext, page,
            $"{Taxonomy}.LovDescriptionTextBox");

            Console.WriteLine($"[INFO] Validating LOV Name: {lovName}, Description: {lovDescription}");

            ValidationHelper.AssertEqual($"{Taxonomy}.LovNameTextBox", expectedValue, lovName,
   $"Expected LOV Name '{expectedValue}', but found '{lovName}'.");

            ValidationHelper.FailIfNullOrEmpty($"{Taxonomy}.LovDescriptionTextBox", lovDescription,
             "LOV Description should not be null or empty.");
            }

        /// <summary>
        /// Verifies search box title
        /// </summary>
        private async Task VerifySearchBoxTitleAsync(string expectedTitle, string taxonomyType)
            {
            try
                {
                Console.WriteLine($"[INFO] Verifying Search Box Title for {taxonomyType}...");

                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{Taxonomy}.LovSearchBoxHeading", TimeConstants.MinimumWait, WaitForSelectorState.Visible);
                string actualTitle = (await WebUIHelper.GetInnerTextAsync(scenarioContext, page, $"{Taxonomy}.LovSearchBoxHeading")).Trim();

                ValidationHelper.AssertEqual($"{Taxonomy}.LovSearchBoxHeading", expectedTitle, actualTitle);

                Console.WriteLine($"[INFO] Verified that Search Box Title '{actualTitle}' is correct for {taxonomyType}.");
                }
            catch (Exception ex)
                {
                throw ExceptionHandler.BuildCustomException($"verify the Search Box title for {taxonomyType}", ex);
                }
            }
        }
    }

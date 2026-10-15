using Microsoft.Playwright;
using NUnit.Framework;
using PIMQAAutomation.Common.Pages;
using PIMQAAutomation.Modules.Bom.Constants;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Utilities;
using TechTalk.SpecFlow;

namespace PIMQAAutomation.Modules.Bom.Steps
{
    public partial class BOM
    {
        [Then(@"Verify ""(.*)"" tab is displayed")]
        public async Task ThenUserIsOnTabByDefault(string pageTab)
        {
            try
            {
                await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
                var tabLocator = bomPage.GetPageTab(pageTab);
                await tabLocator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
                bool isAssetSelected = await tabLocator.EvaluateAsync<bool>($"el => el.classList.contains('{AttributeConstants.Active}')");
                Assert.True(isAssetSelected, $"{pageTab} tab should be selected by default.");
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to verify that '{pageTab}' tab is displayed", ex);
            }
        }

        [When(@"User selects ""(.*)"" Tab")]
        public async Task ThenUserClickOnTabInBOMScreen(string pageTab)
        {
            try
            {
                await bomPage.GetPageTab(pageTab).ClickAsync();
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to select '{pageTab}' tab in BOM screen", ex);
            }
        }

        [Then(@"Verify ""(.*)"" tab ""(.*)"" field is enabled")]
        public async Task ThenVerifyBomKindFieldIsEnabled(string tabName, string fieldName)
        {
            try
            {
                await BasePage.VerifyFieldIsEnabled(bomPage.GetBomInputField(tabName, fieldName), "BOM Kind");
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to verify that '{fieldName}' field is enabled in '{tabName}' tab", ex);
            }
        }

        [Then(@"Verify ""(.*)"" tab ""(.*)"" button is enabled")]
        public async Task WhenUserViewsAndButtonsAreEnabled(string tabName, string buttonName)
        {
            try
            {
                await BasePage.VerifyFieldIsEnabled(bomPage.GetButtonInBOMHeader(tabName, buttonName), $"{buttonName} button");
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to verify that '{buttonName}' button is enabled in '{tabName}' tab", ex);
            }
        }

        [When(@"User clicks on ""(.*)"" tab ""(.*)"" button in BOM header panel")]
        public async Task WhenUserClicksOnButtonInBOMHeaderPanel(string tabName, string buttonName)
        {
            try
            {
                await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
                var buttonLocator = bomPage.GetButtonInBOMHeader(tabName, buttonName);
                await buttonLocator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
                await BasePage.VerifyFieldIsEnabled(buttonLocator, buttonName);
                await buttonLocator.ClickAsync();
                await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to click on '{buttonName}' button in '{tabName}' tab BOM header panel", ex);
            }
        }

        [Then("User verifies success message for BOM creation")]
        public async Task ThenUserVerifiesSuccessMessageForBOMCreation()
        {
            try
            {
                var expectedMessage = BomConstants.ToastMessages.BOMHeaderSuccess;
                var actualToastMessage = await BasePage.ValidateToastMessage(page, commonLocators.GetToastMessage(), expectedMessage);
                ValidationHelper.AssertEqual("Creating BOM Header", expectedMessage, actualToastMessage);
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to create bom header creation", ex);
            }
        }

        [Then(@"User recalls ""(.*)"" in ""(.*)"" tab from the Admin Tool")]
        public async Task ThenUserRecallsFromTheAdminTool(string bomKind, string tabName)
        {
            try
            {
                if (!featureContext.ContainsKey(BomConstants.FeatureContextValue.BOMKind))
                {
                    ValidationHelper.Fail($"BOMKind not found in feature context for recalling '{bomKind}' in '{tabName}' tab");
                }
                var bomKindValue = featureContext[BomConstants.FeatureContextValue.BOMKind].ToString();
                ValidationHelper.FailIfNullOrEmpty("bomKindValue", bomKindValue, $"value is null or empty for recalling '{bomKind}' in '{tabName}' tab");
                await BasePage.TypeTextAsync(bomPage.GetBomInputField(tabName, bomKind), bomKindValue);
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to recall '{bomKind}' in '{tabName}' tab from the Admin Tool", ex);
            }
        }

        [Then(@"User searches and selects data for the below fields from dropdown in ""(.*)"" tab")]
        public async Task ThenUserSearchesAndSelectsTheBelowFieldsFromDropdownInTab(string tabName, Table inputFields)
        {
            try
            {
                await page.WaitForLoadStateAsync();
                foreach (var fieldName in inputFields.Rows)
                {
                    testCaseName = scenarioContext.ScenarioInfo.Title;
                    var fieldLocator = fieldName[0].ToLower();
                    var tabLocator = tabName.ToLower();

                    if (fieldLocator.Contains(BomConstants.BOMKind))
                    {
                        fieldLocator = BomConstants.BOMKind;
                    }
                    var inputValue = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.{fieldName[0]}");
                    ValidationHelper.FailIfNullOrEmpty($"inputValue for field {fieldName[0]}", inputValue, $"Failed to read input value for field '{fieldName[0]}' from test case '{testCaseName}'");
                    var bonInputField = bomPage.GetCreateBomInputField(tabLocator, fieldLocator);
                    await bonInputField.ClickAsync();
                    await BasePage.TypeTextAsync(bonInputField, inputValue);
                    var isDropdownOpen = await commonLocators.GetDropdownListBox().IsVisibleAsync();
                    Console.WriteLine($"isDropdownOpen {isDropdownOpen}");
                    if (isDropdownOpen)
                    {
                        await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);
                        var dropdownOptions = commonLocators.GetDropDownOption(inputValue);
                        var optionCount = await dropdownOptions.CountAsync();
                        if (optionCount > 0)
                        {
                            await commonLocators.GetSelectDropdownOption(inputValue).EvaluateAsync("element => element.click()");
                            Console.WriteLine($"dropdown option {inputValue} is selected");
                        }
                    }
                }
                featureContext[BomConstants.FeatureContextValue.BOMKind] = await bomPage.GetBomInputField(tabName, BomConstants.BOMKind).InputValueAsync();
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to search and select data for fields from dropdown in '{tabName}' tab", ex);
            }
        }

        [Then(@"Verify Taxonomy field description is not empty in ""(.*)"" tab")]
        public async Task ThenVerifyTaxonomyFieldDescriptionIsNotEmptyInTab(string tabName, Table taxonomydescription)
        {
            try
            {
                foreach (var menu in taxonomydescription.Rows)
                {
                    var fieldName = menu[0];
                    var fieldValue = await bomPage.GetTaxonomyDescription(tabName, fieldName).InputValueAsync();

                    ValidationHelper.FailIfNullOrEmpty($"Taxonomy field {fieldName}", fieldValue, $"is displaying as empty in '{tabName}' tab");
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to verify taxonomy field descriptions are not empty in '{tabName}' tab", ex);
            }
        }

        [Then(@"Verify created and modified username and date in right corner of BOM header panel in ""(.*)"" tab")]
        public async Task ThenVerifyCreatedAndModifiedUsernameAndDateInRightCornerOfBomHeaderPanelInTab(string tabName, Table dateFields)
        {
            try
            {
                await bomPage.GetHistoryButton().ClickAsync();
                foreach (var fieldName in dateFields.Rows)
                {
                    await bomPage.GetCreatedModifiedUsernameAndDateBomHeader(fieldName[0]).IsVisibleAsync();
                }
                await bomPage.GetCloseHistoryIcon().ClickAsync();
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to verify created and modified username and date in right corner of BOM header panel in '{tabName}' tab", ex);
            }
        }

        [Then(@"Verify Header fields are not empty in ""(.*)"" tab")]
        public async Task ThenVerifyHeaderFields(string tabName, Table bomHeaderFields)
        {
            try
            {
                await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
                await ToggleSectionAsync(expandOnly: true);
                foreach (var menu in bomHeaderFields.Rows)
                {
                    var fieldName = menu[0];
                    var fieldValue = await bomPage.GetBomInputField(tabName, fieldName).InputValueAsync();
                    ValidationHelper.FailIfNullOrEmpty($"Header field {fieldName}", fieldValue, $"is displaying as empty in '{tabName}' tab");
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to verify header fields are not empty in '{tabName}' tab", ex);
            }
        }

        [Then(@"verify BOM internal Notes field is displayed in ""(.*)"" tab")]
        public async Task ThenUserViewsBomInternalNotesAsync(string tabName)
        {
            try
            {
                await bomPage.GetBomHeaderInternalNotes(tabName).IsVisibleAsync();
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to verify BOM internal Notes field is displayed in '{tabName}' tab", ex);
            }
        }

        [Then(@"User recalls ""(.*)"" header ""(.*)"" and ""(.*)""")]
        public async Task ThenUserRecallsAssetHeaderBomKindAndSerialNumber(string tabName, string bomKind, string serialNo)
        {
            try
            {
                await bomPage.GetBomInputField(tabName, bomKind).WaitForAsync(new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible
                });

                if (!scenarioContext.ContainsKey(BomConstants.FeatureContextValue.BOMKind))
                {
                    ValidationHelper.Fail($"BOMKind not found in scenario context for recalling header '{bomKind}' in '{tabName}' tab");
                }

                if (!scenarioContext.ContainsKey(BomConstants.FeatureContextValue.SerialNumber))
                {
                    ValidationHelper.Fail($"SerialNumber not found in scenario context for recalling header '{serialNo}' in '{tabName}' tab");
                }

                var bomKindValue = scenarioContext[BomConstants.FeatureContextValue.BOMKind].ToString();
                var serialNumber = scenarioContext[BomConstants.FeatureContextValue.SerialNumber].ToString();

                await bomPage.GetBomInputField(tabName, bomKind).ClickAsync();
                await BasePage.TypeTextAsync(bomPage.GetBomInputField(tabName, bomKind), bomKindValue);
                await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
                var isDropdownOpen = await commonLocators.GetDropdownListBox().IsVisibleAsync();
                Console.WriteLine($"isDropdownOpen {isDropdownOpen}");
                if (isDropdownOpen)
                {
                    await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);
                    var dropdownOptions = commonLocators.GetDropDownOption(bomKind);
                    var optionCount = await dropdownOptions.CountAsync();
                    if (optionCount > 0)
                    {
                        await commonLocators.GetSelectDropdownOption(bomKind).EvaluateAsync("element => element.click()");
                        Console.WriteLine($"dropdown option {bomKind} is selected");
                    }
                }
                await bomPage.GetBomInputField(tabName, serialNo).ClickAsync();
                await BasePage.TypeTextAsync(bomPage.GetBomInputField(tabName, serialNo), serialNumber);
                await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
                var dropdownOpen = await commonLocators.GetDropdownListBox().IsVisibleAsync();
                Console.WriteLine($"isDropdownOpen {dropdownOpen}");
                if (dropdownOpen)
                {
                    await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);
                    var dropdownOptions = commonLocators.GetDropDownOption(serialNo);
                    var optionCount = await dropdownOptions.CountAsync();
                    if (optionCount > 0)
                    {
                        await commonLocators.GetSelectDropdownOption(serialNo).EvaluateAsync("element => element.click()");
                        Console.WriteLine($"dropdown option {serialNo} is selected");
                    }
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to recall '{bomKind}' and '{serialNo}' headers in '{tabName}' tab", ex);
            }
        }

        [Then(@"User recalls below fields from ""(.*)"" tab")]
        public async Task ThenUserRecallBelowFieldsFromTab(string tabName, Table fields)
        {
            try
            {
                var tabLocator = tabName.ToLower();
                foreach (var fieldName in fields.Rows)
                {
                    var field = fieldName[0];
                    if (!scenarioContext.ContainsKey(field))
                    {
                        ValidationHelper.Fail($"Field '{field}' not found in scenario context for recalling from '{tabName}' tab");
                    }
                    var fieldValue = scenarioContext[field].ToString();
                    ValidationHelper.FailIfNullOrEmpty($"Field value for {field}", fieldValue, $"is null or empty in scenario context");
                    await bomPage.GetBomInputField(tabLocator, fieldName[0]).ClickAsync();
                    await BasePage.TypeTextAsync(bomPage.GetBomInputField(tabLocator, fieldName[0]), fieldValue);
                    await page.WaitForLoadStateAsync();
                    await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);
                    await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
                    var isDropdownOpen = await commonLocators.GetDropdownListBox().IsVisibleAsync();
                    Console.WriteLine($"isDropdownOpen {isDropdownOpen}");
                    if (isDropdownOpen)
                    {
                        await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);
                        var dropdownOptions = commonLocators.GetDropDownOption(fieldValue);
                        var optionCount = await dropdownOptions.CountAsync();
                        if (optionCount > 0)
                        {
                            await commonLocators.GetSelectDropdownOption(fieldValue).EvaluateAsync("element => element.click()");
                            Console.WriteLine($"dropdown option {fieldValue} is selected");
                        }
                    }
                }
                featureContext[BomConstants.FeatureContextValue.BOMKind] = await bomPage.GetBomInputField(tabLocator, BomConstants.BOMKind).InputValueAsync();
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to recall fields from '{tabName}' tab", ex);
            }
        }

        [Then(@"User clicks and enters BOM Internal Notes field in ""(.*)"" tab")]
        public async Task ThenUserClicksAndEntersBomTextbox(string tabName)
        {
            try
            {
                testCaseName = scenarioContext.ScenarioInfo.Title;
                //Build the JSON key dynamically based on the test case name
                var internalNotes = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.Internal-Notes");
                ValidationHelper.FailIfNullOrEmpty("internalNotes", internalNotes, $"Failed to read Internal-Notes data from test case '{testCaseName}'");
                await bomPage.GetBomHeaderInternalNotes(tabName).FillAsync(internalNotes);
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to click and enter BOM Internal Notes field in '{tabName}' tab", ex);
            }
        }

        [Then(@"User gets the values of below fields from ""(.*)"" tab")]
        public async Task ThenUserGetsTheValuesOfBelowFieldsFromTab(string tabName, Table fields)
        {
            try
            {
                await ToggleSectionAsync(expandOnly: true);
                var fieldValues = new Dictionary<string, string>();
                foreach (var field in fields.Rows)
                {
                    var fieldName = field[0];
                    var inputValue = await bomPage.GetBomInputField(tabName, fieldName).InputValueAsync();
                    fieldValues[fieldName] = inputValue;
                }
                featureContext[BomConstants.FeatureContextValue.BOMHeaderDetails] = fieldValues;
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to get values of fields from '{tabName}' tab", ex);
            }
        }
        [Then(@"User verifies the created data matches ""(.*)"" BOM header page")]
        public async Task ThenUserVerifiesTheCreatedDataMatchesBOMHeaderPage(string tabName, Table bomHeaderFields)
        {
            try
            {
                var bomHeaderCreationValue = featureContext[BomConstants.FeatureContextValue.BOMCreationValues] as Dictionary<string, string>;
                ValidationHelper.FailIfNullOrEmpty("BOMCreationFields", bomHeaderCreationValue, $"is null or empty in feature context for verifying bomLineCreationValues");
                var bomHeaderDetails = new Dictionary<string, string>();
                foreach (var bomHeaderField in bomHeaderFields.Rows)
                {
                    var fieldName = bomHeaderField[0];
                    await page.WaitForLoadStateAsync();
                    await bomPage.GetBomInputField(tabName, fieldName).IsVisibleAsync();
                    await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
                    bomHeaderDetails[fieldName] = (await bomPage.GetBomInputField(tabName, fieldName).InputValueAsync()).Trim();
                }
                ValidationHelper.AssertEqual("BOM Header Creation Value", bomHeaderCreationValue, bomHeaderDetails, $"Data mismatch for Bom creation: '{bomHeaderCreationValue}' and in BOM header page: '{bomHeaderDetails}'");
                featureContext[BomConstants.FeatureContextValue.BOMHeaderDetails] = bomHeaderDetails;
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to validate BOM header creation data matches BOM header panel", ex);
            }
        }


        [Then(@"User searches and selects data for the below fields from dropdown in ""(.*)"" tab using Asset BOM Line data")]
        public async Task WhenUserSearchesAndSelectsDataForFieldsFromDropdownInTabUsingAssetBomLineTab(string tabName, Table bomHeaderFields)
        {
            try
            {
                // Get the values previously stored in the scenario context
                if (!scenarioContext.ContainsKey(BomConstants.FeatureContextValue.BOMLineValues))
                {
                    ValidationHelper.Fail($"BOMLineValues not found in scenario context for searching fields in '{tabName}' tab");
                }
                var bomLineValues = scenarioContext[BomConstants.FeatureContextValue.BOMLineValues] as Dictionary<string, string>;
                ValidationHelper.FailIfNullOrEmpty("BOMLineValues", bomLineValues, $"is null or empty in scenario context for searching fields in '{tabName}' tab");

                foreach (var bomHeaderField in bomHeaderFields.Rows)
                {
                    var fieldName = bomHeaderField[0];
                    if (fieldName == BomConstants.BOMHeaderItemId)
                    {
                        fieldName = BomConstants.BomLineGridFields.ItemId;
                    }
                    if (!bomLineValues.ContainsKey(fieldName))
                    {
                        ValidationHelper.Fail($"Field '{fieldName}' not found in BOMLineValues for searching in '{tabName}' tab");
                    }
                    var inputValue = bomLineValues[fieldName];

                    await bomPage.GetCreateBomInputField(tabName, fieldName).ClickAsync();
                    await BasePage.TypeTextAsync(bomPage.GetCreateBomInputField(tabName, fieldName), inputValue);
                    await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
                    var isDropdownOpen = await commonLocators.GetDropdownListBox().IsVisibleAsync();
                    Console.WriteLine($"isDropdownOpen {isDropdownOpen}");
                    if (isDropdownOpen)
                    {
                        await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);
                        var dropdownOptions = commonLocators.GetDropDownOption(inputValue);
                        var optionCount = await dropdownOptions.CountAsync();
                        if (optionCount > 0)
                        {
                            await commonLocators.GetSelectDropdownOption(inputValue).EvaluateAsync("element => element.click()");
                            Console.WriteLine($"dropdown option {inputValue} is selected");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to search and select data for fields from dropdown in '{tabName}' tab using Asset BOM Line data", ex);
            }
        }

        [Then(@"User searches and selects data for the below fields from dropdown in ""(.*)"" tab using Asset data")]
        public async Task WhenUserSearchesAndSelectsDataForFieldsFromDropdownInTabUsingAssetTab(string tabName, Table bomHeaderFields)
        {
            try
            {
                // Get the values previously stored in the scenario context
                if (!featureContext.ContainsKey(BomConstants.FeatureContextValue.BOMHeaderDetails))
                {
                    ValidationHelper.Fail($"BOMHeaderDetails not found in feature context for searching fields in '{tabName}' tab");
                }
                var assetFieldValues = featureContext[BomConstants.FeatureContextValue.BOMHeaderDetails] as Dictionary<string, string>;
                ValidationHelper.FailIfNullOrEmpty("assetFieldValues", assetFieldValues, $"is null or empty in feature context for searching fields in '{tabName}' tab");

                foreach (var bomHeaderField in bomHeaderFields.Rows)
                {
                    var fieldName = bomHeaderField[0];

                    if (!assetFieldValues.ContainsKey(fieldName))
                    {
                        ValidationHelper.Fail($"Field '{fieldName}' not found in AssetFields for searching in '{tabName}' tab");
                    }
                    var inputValue = assetFieldValues[fieldName];
                    var bomInpitField = bomPage.GetCreateBomInputField(tabName, fieldName);
                    await bomInpitField.ClickAsync();
                    await BasePage.TypeTextAsync(bomInpitField, inputValue);
                    var isDropdownOpen = await commonLocators.GetDropdownListBox().IsVisibleAsync();
                    Console.WriteLine($"isDropdownOpen {isDropdownOpen}");
                    if (isDropdownOpen)
                    {
                        await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);
                        var dropdownOptions = commonLocators.GetDropDownOption(inputValue);
                        var optionCount = await dropdownOptions.CountAsync();
                        if (optionCount > 0)
                        {
                            await commonLocators.GetSelectDropdownOption(inputValue).EvaluateAsync("element => element.click()");
                            Console.WriteLine($"dropdown option {inputValue} is selected");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to search and select data for fields from dropdown in '{tabName}' tab using Asset data", ex);
            }
        }
        [Then(@"User captures the entered values in the ""(.*)"" tab")]
        public async Task ThenUseraGetsTheValues(string tabName,Table bomHeaderFields)
        {
            try
            {
                await ToggleSectionAsync(expandOnly: true);
                var fieldValues = new Dictionary<string, string>();
                foreach (var menu in bomHeaderFields.Rows)
                {
                    var menuLocator = menu[0];
                    var inputValue = await bomPage.GetBomInputField(tabName, menuLocator).InputValueAsync();
                    fieldValues[menu[0]] = inputValue;
                }
                featureContext[BomConstants.FeatureContextValue.BOMCreationValues] = fieldValues;
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to get values of fields from '{tabName}' tab", ex);
            }
        }
        [Then(@"User recalls below field values in ""(.*)"" tab")]
        public async Task ThenUserRecallsBelowFieldValues(string tabName, Table bomHeaderFields)
        {
            try
            {
                // Get the values previously stored in the scenario context
                if (!featureContext.ContainsKey(BomConstants.FeatureContextValue.BOMCreationValues))
                {
                    ValidationHelper.Fail($"BOMCreationFields not found in feature context for searching fields in '{tabName}' tab");
                }
                var bomCreationValues = featureContext[BomConstants.FeatureContextValue.BOMCreationValues] as Dictionary<string, string>;
                ValidationHelper.FailIfNullOrEmpty("BOMCreationFields", bomCreationValues, $"is null or empty in feature context for searching fields in '{tabName}' tab");

                foreach (var bomHeaderField in bomHeaderFields.Rows)
                {
                    var fieldName = bomHeaderField[0];
                    if (!bomCreationValues.ContainsKey(fieldName))
                    {
                        ValidationHelper.Fail($"Field '{fieldName}' not found in AssetFields for searching in '{tabName}' tab");
                    }
                    var inputValue = bomCreationValues[fieldName];
                    var bomInputField = bomPage.GetBomInputField(tabName, fieldName);
                    await bomInputField.ClickAsync();
                    await BasePage.TypeTextAsync(bomInputField, inputValue);
                    await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
                    var isDropdownOpen = await commonLocators.GetDropdownListBox().IsVisibleAsync();
                    Console.WriteLine($"isDropdownOpen {isDropdownOpen}");
                    if (isDropdownOpen)
                    {
                        await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);
                        var dropdownOptions = commonLocators.GetDropDownOption(inputValue);
                        var optionCount = await dropdownOptions.CountAsync();
                        if (optionCount > 0)
                        {
                            await commonLocators.GetSelectDropdownOption(inputValue).EvaluateAsync("element => element.click()");
                            Console.WriteLine($"dropdown option {inputValue} is selected");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to search and select data for fields from dropdown in '{tabName}' tab using Asset data", ex);
            }
        }
        [Then(@"User enters the data in the BOM internal notes textbox under the ""(.*)"" tab in the BOM header")]
        public async Task ThenUserEntersTheDataInTheBomInternalNotesTextboxUnderTheTabInTheBomHeader(string tabName)
        {
            try
            {
                var scenarioTitle = scenarioContext.ScenarioInfo.Title;
                var internalNotesText = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{scenarioTitle}.BOMInternalNotes");
                ValidationHelper.FailIfNullOrEmpty("internalNotesText", internalNotesText, $"Failed to read BOMInternalNotes data from test case '{scenarioTitle}'");
                var bomInternalNotes = bomPage.GetBomHeaderInternalNotes(tabName);
                await bomInternalNotes.ClickAsync();
                await bomInternalNotes.FillAsync(internalNotesText);
                var internalNotes = bomInternalNotes.InputValueAsync();
                scenarioContext[BomConstants.FeatureContextValue.BOMInternalNotes] = internalNotes;
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to enter data in the BOM internal notes textbox under the '{tabName}' tab in the BOM header", ex);
            }
        }

        [Then(@"User verifies duplicate ""(.*)"" BOM creation error message")]
        public async Task ThenUserVerifiesTheErrorMessage(string tabName)
        {
            try
            {
                var expectedMessage = BomConstants.DuplicateErrorMessages[tabName];
                var actualToastMessage = await BasePage.ValidateToastMessage(page,commonLocators.GetToastMessage(), expectedMessage);
                ValidationHelper.AssertEqual($"{tabName}", expectedMessage, actualToastMessage);
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to verify duplicate error message for {tabName} data creation", ex);
            }
        }
        public async Task ToggleSectionAsync(bool expandOnly = true)
        {
            try
            {
                await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
                var toggleButton = bomPage.GetToggleButton();
                // Check if toggle button exists on the page
                if (await toggleButton.CountAsync() == 0)
                {
                    Console.WriteLine("Toggle button not found on this page. Skipping toggle action.");
                    return;
                }
                // Check if it’s visible
                if (!await toggleButton.IsVisibleAsync())
                {
                    Console.WriteLine("Toggle button is not visible. Skipping toggle action.");
                    return;
                }
                // Get the class attribute
                var iconClass = await toggleButton.GetAttributeAsync("class");
                if (iconClass == null)
                {
                    Console.WriteLine("Toggle button has no class attribute. Skipping toggle action.");
                    return;
                }
                // Identify collapse/expand state
                bool isCollapsed = iconClass.Contains("arrow-down");

                // - If expandOnly = true → click only if collapsed
                // - If expandOnly = false → always click (toggle both ways)
                if ((expandOnly && isCollapsed) || !expandOnly)
                {
                    await toggleButton.ClickAsync();
                    Console.WriteLine($"Toggle button clicked. (expandOnly={expandOnly}, collapsed={isCollapsed})");
                }
                else
                {
                    Console.WriteLine("Section already expanded — no click performed.");
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Toggle button is not clicked",ex);
            }
        }
    }
}
using PIMQAAutomation.Common.Pages;
using PIMQAAutomation.Modules.Bom.Constants;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Utilities;
using TechTalk.SpecFlow;

namespace PIMQAAutomation.Modules.Bom.Steps
{
    public partial class BOM
    {
        [Then(@"Verify ""(.*)"" and ""(.*)"" buttons are enabled in BOM Line creation panel")]
        public async Task ThenVerifyButtonIsEnabledInBomLineCreationPanel(string button1, string button2)
        {
            try
            {
                await BasePage.VerifyFieldIsEnabled(bomPage.GetButtonInBOMLineCreation(button1), $"{button1} button");
                await BasePage.VerifyFieldIsEnabled(bomPage.GetButtonInBOMLineCreation(button2), $"{button2} button");
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to verify that buttons '{button1}' and '{button2}' are enabled in BOM Line creation panel", ex);
            }
        }

        [When(@"User clicks on row ""(.*)"" Edit button in BOM Line creation panel")]
        public async Task WhenUserClicksOnRowEditButtonInBOMLineCreationPanel(int rowNumber)
        {
            try
            {
                await bomPage.GetBomLineEditButton(rowNumber).ClickAsync();
            }
            catch (Exception e)
            {
                ValidationHelper.Fail("Unable to click edit button", e);
            }
        }

        [When(@"User clicks on ""(.*)"" button in BOM Line creation panel")]
        public async Task WhenUserClicksOnTheButtonInBOMLineCreationPanel(string buttonName)
        {
            try
            {
                var buttonLocator = bomPage.GetButtonInBOMLineCreation(buttonName);
                await BasePage.VerifyFieldIsEnabled(buttonLocator, buttonName);
                await buttonLocator.ClickAsync();
                await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to click on '{buttonName}' button in BOM Line creation panel", ex);
            }
        }
        [Then("User verifies success message for BOM Line addition")]
        public async Task ThenUserVerifiesSuccessMessageForBOMLineAddition()
        {
            try
            {
                var expectedMessage = BomConstants.ToastMessages.BOMLineSuccess;
                var actualToastMessage = await BasePage.ValidateToastMessage(page, commonLocators.GetToastMessage(), expectedMessage);
                ValidationHelper.AssertEqual("Adding BOM Line ", expectedMessage, actualToastMessage);
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to verify Toast message of BOM Line addition", ex);
            }
        }

        [Then(@"Verify grid row ""(.*)"" not empty in BOM line grid with below headers")]
        public async Task ThenVerifyGridRowNotEmptyInBomLineGridWithBelowHeaders(int rowNumber, Table table)
        {
            try
            {
                foreach (var menu in table.Rows)
                {
                    var fieldName = menu[0];
                    var bomLineGridValue = (await bomPage.GetBomLineTableValue(fieldName, rowNumber).TextContentAsync()).Trim();
                    ValidationHelper.FailIfNullOrEmpty("bomLineGridValue", bomLineGridValue, $"The field '{fieldName}' in row {rowNumber} is null or empty while verifying BOM line grid values");
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to verify grid row {rowNumber} is not empty in BOM line grid", ex);
            }
        }

        [Then("User gets the values of below fields from BOM line creation panel")]
        public async Task ThenUserGetsTheValuesOfBelowFieldsFromBOMLineCreationPanel(Table bomLineFields)
        {
            try
            {
                var bomLineCreationValue = new Dictionary<string, string>();
                foreach (var bomLineField in bomLineFields.Rows)
                {
                    var fieldName = bomLineField[0];
                    await page.WaitForLoadStateAsync();
                    if (fieldName.Equals(BomConstants.LocatorProperties.BOMObsolete, StringComparison.OrdinalIgnoreCase))
                    {
                        var isChecked = await bomPage.GetBomLineCreationObsoleteCheckbox().IsCheckedAsync();
                        bomLineCreationValue[fieldName] = isChecked ? BomConstants.True : BomConstants.False;
                    }
                    else
                    {
                        bomLineCreationValue[fieldName] = await bomPage.GetBomLineCreationValue(fieldName).InputValueAsync();
                    }
                    await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
                }
                featureContext[BomConstants.FeatureContextValue.BOMLineCreationValues] = bomLineCreationValue;
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to get the values of below fields from BOM line creation panel", ex);
            }
        }

        [Then(@"Validate BOM line grid row ""(.*)"" data matches BOM line creation panel")]
        public async Task ThenUserViewSelectedValuesInBomLinePanelAreDisplayedInCategoriesPanelAsync(int rowNumber, Table bomLineFields)
        {
            try
            {
                var bomLineCreationValue = featureContext[BomConstants.FeatureContextValue.BOMLineCreationValues] as Dictionary<string, string>;
                ValidationHelper.FailIfNullOrEmpty("bomLineCreationValue", bomLineCreationValue, $"is null or empty in feature context for verifying bomLineCreationValues");
                var bomLineGrid = new Dictionary<string, string>();
                foreach (var bomLineField in bomLineFields.Rows)
                {
                    var fieldName = bomLineField[0];
                    await page.WaitForLoadStateAsync();
                    await bomPage.GetBomLineTableValue(fieldName, rowNumber).ScrollIntoViewIfNeededAsync();
                    await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
                    bomLineGrid[fieldName] = (await bomPage.GetBomLineTableValue(fieldName, rowNumber).TextContentAsync()).Trim();
                }
                ValidationHelper.AssertEqual("BOM Line Creation Value", bomLineCreationValue, bomLineGrid, $"Data mismatch in Grid: '{bomLineGrid}' and in BOM line creation panel: '{bomLineCreationValue}'");
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to validate BOM line grid row {rowNumber} data matches BOM line creation panel. Error", ex);
            }
        }

        [Then(@"Verify created and modified username and date of BOM Line ""(.*)""")]
        public async Task ThenVerifyCreatedAndModifiedUsernameAndDateOfBOMLine(int rowNumber, Table dateFields)
        {
            try
            {
                await bomPage.GetHistoryButton().ClickAsync();
                foreach (var fieldName in dateFields.Rows)
                {
                    await bomPage.GetCreatedDateInItemToBomRelationship(fieldName[0], rowNumber).IsVisibleAsync();
                }
                await bomPage.GetCloseHistoryIcon().ClickAsync();
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to verify created and modified username and date in right corner of BOM header panel in '{rowNumber}' tab. Error", ex);
            }
        }
        [Then(@"User get the value of BOM Line grid row ""(.*)"" data from Asset tab")]
        public async Task ThenUserGetTheValueOfBomLineFieldFromAssetTab(int rowNumber, Table bomLineGridFields)
        {
            try
            {
                var fieldValues = new Dictionary<string, string>();
                foreach (var fieldName in bomLineGridFields.Rows)
                {
                    var fieldLocator = fieldName[0];
                    var inputValue = (await bomPage.GetBomLineTableValue(fieldLocator, rowNumber).TextContentAsync()).Trim();
                    fieldValues[fieldLocator] = inputValue;
                }
                scenarioContext[BomConstants.FeatureContextValue.BOMLineValues] = fieldValues;
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to get value of BOM Line field from Asset tab. Error", ex);
            }
        }

        [Then(@"User searches and selects the ""(.*)"" from dropdown")]
        public async Task ThenUserSearchesAndSelectsTheItemIdFromDropdownAsync(string fieldName)
        {
            try
            {
                var itemIdLocator = fieldName;
                if (itemIdLocator.Contains(BomConstants.LocatorProperties.BOMItemId, StringComparison.OrdinalIgnoreCase))
                {
                    itemIdLocator = BomConstants.LocatorProperties.BOMItemId;
                }
                testCaseName = scenarioContext.ScenarioInfo.Title;
                // Build the JSON key dynamically based on the test case name
                var itemId = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.{fieldName}");
                ValidationHelper.FailIfNullOrEmpty($"itemId for field {fieldName}", itemId, $"Failed to read item ID data for field '{fieldName}' from test case '{testCaseName}'");
                await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
                await bomPage.GetBomLineCreationValue(itemIdLocator).ClickAsync();
                await BasePage.TypeTextAsync(bomPage.GetBomLineCreationValue(itemIdLocator), itemId);
                await bomPage.GetBomLineCreationValue(itemIdLocator).InputValueAsync();
                await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
                var isDropdownOpen = await commonLocators.GetDropdownListBox().IsVisibleAsync();
                Console.WriteLine($"isDropdownOpen {isDropdownOpen}");
                if (isDropdownOpen)
                {
                    await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);
                    var dropdownOptions = commonLocators.GetDropDownOption(itemId);
                    var optionCount = await dropdownOptions.CountAsync();
                    if (optionCount > 0)
                    {
                        await commonLocators.GetSelectDropdownOption(itemId).EvaluateAsync("element => element.click()");
                        Console.WriteLine($"dropdown option {itemId} is selected");
                    }
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to search and select '{fieldName}' from dropdown. Error", ex);
            }
        }

        [Then(@"User clicks and enters BOM Line ""(.*)"" field")]
        public async Task ThenUserClicksAndEntersBomLineInstallBaseFieldAsync(string fieldName)
        {
            try
            {
                await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
                testCaseName = scenarioContext.ScenarioInfo.Title;
                var fieldLocator = fieldName;
                if (fieldLocator.Contains(BomConstants.LocatorProperties.BOMQuantity, StringComparison.OrdinalIgnoreCase))
                {
                    fieldLocator = BomConstants.LocatorProperties.BOMQuantity;
                }
                var fieldValue = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.{fieldName}");
                ValidationHelper.FailIfNullOrEmpty($"fieldValue for {fieldName}", fieldValue, $"Failed to read field value for '{fieldName}' from test case '{testCaseName}'");
                await bomPage.GetBomLineCreationValue(fieldLocator).FillAsync(fieldValue);
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to click and enter BOM Line '{fieldName}' field. Error", ex);
            }
        }

        [Then(@"User clicks and enters BOM Line ""(.*)"" textbox")]
        public async Task ThenUserClickAndEnterBomLineTextbox(string itemToBomNotes)
        {
            try
            {
                testCaseName = scenarioContext.ScenarioInfo.Title;
                var notesValue = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.{itemToBomNotes}");
                ValidationHelper.FailIfNullOrEmpty($"notesValue for {itemToBomNotes}", notesValue, $"Failed to read notes value for '{itemToBomNotes}' from test case '{testCaseName}'");
                await bomPage.GetItemToBomNotes().FillAsync(notesValue);
                await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to click and enter BOM Line '{itemToBomNotes}' textbox. Error", ex);
            }
        }

        [Then(@"User verifies the BOM Line ""(.*)"" defaults to ""(.*)""")]
        public async Task ThenUserVerifiesTheBomLineWeightDefaultsTo(string quantity, string actualQuantity)
        {
            try
            {
                var currentQuantity = await bomPage.GetBomLineCreationValue(quantity).InputValueAsync();
                ValidationHelper.AssertEqual("BOM Line Quantity", currentQuantity, actualQuantity, $"BOM Line Quantity should default to '{actualQuantity}' but found '{currentQuantity}'");
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to verify BOM Line '{quantity}' defaults to '{actualQuantity}'", ex);
            }
        }

        [Then(@"User verifies ""(.*)"" defaults to ""(.*)"" in the BOM Line creation section")]
        public async Task ThenUserVerifiesDefaultsToInTheBomLineCreationSection(string quantity, string actualquantity)
        {
            try
            {
                var quantityBomLineCreation = await bomPage.GetBomLineCreationValue(quantity).InputValueAsync();
                ValidationHelper.AssertEqual("BOM Line creation Quantity", quantityBomLineCreation, actualquantity, $"BOM Line creation Quantity should default to '{actualquantity}', but found '{quantityBomLineCreation}'");
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to verify '{quantity}' defaults to '{actualquantity}' in the BOM Line Grid and BOM Line creation section. Error", ex);
            }
        }
        [Then(@"User verifies ""(.*)"" defaults to ""(.*)"" in the BOM Line Grid row ""(.*)""")]
        public async Task ThenUserVerifiesDefaultsToInTheBomLineGrid(string quantity, string actualquantity, int rowNumber)
        {
            try
            {
                var quantityBomLineGrid = await bomPage.GetBomLineTableValue(quantity, rowNumber).TextContentAsync();
                ValidationHelper.AssertEqual("BOM Line Grid Quantity", quantityBomLineGrid.Trim(), actualquantity, $"BOM Line Grid Quantity should default to '{actualquantity}', but found '{quantityBomLineGrid}'");
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to verify '{quantity}' defaults to '{actualquantity}' in the BOM Line Grid and BOM Line creation section. Error", ex);
            }
        }

        [Then(@"User checks BOM Line ""(.*)"" checkbox")]
        public async Task ThenUserClickAndBomLineCheckbox(string obsolete)
        {
            try
            {
                var isChecked = await bomPage.GetBomLineCreationValue(obsolete).IsCheckedAsync();
                if (isChecked)
                {
                    await bomPage.GetBomLineCreationValue(obsolete).ClickAsync(); //uncheck
                    await bomPage.GetBomLineCreationValue(obsolete).ClickAsync(); //check
                }
                else
                {
                    await bomPage.GetBomLineCreationValue(obsolete).ClickAsync(); //check
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to check BOM Line '{obsolete}' checkbox. Error", ex);
            }
        }

        [Then(@"User gets the ""(.*)"" from BOM line grid row ""(.*)"" in the ""(.*)"" tab")]
        public async Task ThenUserGetsTheBomLineQuantityFromTab(string fieldName, int rowNumber, string tabName)
        {
            try
            {
                var quantityValue = (await bomPage.GetBomLineTableValue(fieldName, rowNumber).TextContentAsync()).Trim();
                ValidationHelper.FailIfNullOrEmpty($"Quantity value for field {fieldName}", quantityValue, $"is null or empty for field '{fieldName}' in tab '{tabName}' at row {rowNumber}.");

                if (double.TryParse(quantityValue, out var quantity))
                {
                    scenarioContext[$"{tabName}Quantity"] = quantity;
                }
                else
                {
                    ValidationHelper.Fail($"Failed to parse quantity {quantityValue} from tab '{tabName}'");
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to get Quantity from {tabName}", ex);
            }
        }

        [Then("User verifies error message for quantity")]
        public async Task ThenUserVerifiesErrorMessageForQuantity()
        {
            try
            {
                var popupMessage = bomPage.GetQuantityErrorMessage();
                var actualText = (await popupMessage.TextContentAsync())?.Trim();
                ValidationHelper.AssertEqual("Quantity", BomConstants.QuantityErrorMessage, actualText);
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to verify error message when Quantity is zero", ex);
            }
        }

        [Then(@"User gets the ""(.*)"" from the BOM line grid row ""(.*)"" in the Part tab")]
        public async Task ThenUserGetsTheFromTheBOMLineGridRowInThePartTab(string fieldName, int rowNumber)
        {
            try
            {
                var quantityValue = (await bomPage.GetBomLineTableValue(fieldName, rowNumber).TextContentAsync()).Trim();
                ValidationHelper.FailIfNullOrEmpty($"Quantity value for field {fieldName}", quantityValue, $"is null or empty for field '{fieldName}' in tab Part at row {rowNumber}.");

                if (double.TryParse(quantityValue, out var quantity))
                {
                    quantityList.Add(quantity);
                }
                else
                {
                    ValidationHelper.Fail($"Failed to parse quantity {quantityValue} from tab Part");
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to get Quantity from Part", ex);
            }
        }
    }
}
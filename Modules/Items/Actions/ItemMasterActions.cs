using Microsoft.Playwright;
using NUnit.Framework;
using OfficeOpenXml;
using PIMQAAutomation.Common.Pages;
using PIMQAAutomation.Modules.Bom.Constants;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Modules.Items.Constants;
using PIMQAAutomation.Modules.Items.Pages;
using PIMQAAutomation.Modules.Lob.Constants;
using PIMQAAutomation.Utilities;
using TechTalk.SpecFlow;
namespace PIMQAAutomation.Modules.Items.Actions
{
   

    /// <summary>
    /// Contains business logic and action methods for Item Master functionality
    /// </summary>
    public class ItemMasterActions
    {
        private readonly IPage page;
        private readonly ScenarioContext scenarioContext;
        private readonly FeatureContext featureContext;
        private readonly ItemMasterPage itemMasterPage;
        private readonly CommonLocators commonLocators;

        public ItemMasterActions(IPage page, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            this.page = page;
            this.scenarioContext = scenarioContext;
            this.featureContext = featureContext;
            itemMasterPage = new ItemMasterPage(page);
            commonLocators = new CommonLocators(page);
        }

        public async Task VerifyTabIsDisplayedAsync(string tabName)
        {
            await itemMasterPage.GetTab(tabName.ToLower()).IsVisibleAsync();
        }

        public async Task ClickTabAsync(string tabName)
        {
            await itemMasterPage.GetTab(tabName.ToLower()).IsVisibleAsync();
            await itemMasterPage.GetTab(tabName.ToLower()).ClickAsync();
            await Task.Delay(500);
        }

        public async Task ClickButtonAsync(string buttonName)
        {
            var buttonLocator = itemMasterPage.GetButtonName(buttonName.ToLower());
            await buttonLocator.ScrollIntoViewIfNeededAsync();

            if (await buttonLocator.IsVisibleAsync() && await buttonLocator.IsEnabledAsync())
            {
                await buttonLocator.ClickAsync();
            }
            else
            {
                ValidationHelper.Fail($"Button '{buttonName}' is not interactable");
            }
        }

        public async Task ClickTaxonomyButtonAsync(string buttonName)
        {
            await itemMasterPage.GetTaxonomyButtonName(buttonName.ToLower()).ClickAsync();
        }

        public async Task VerifyRadioButtonFieldsAreDisplayedAsync(Table table)
        {
            foreach (var row in table.Rows)
            {
                await itemMasterPage.GetRadioButton(row[0].ToLower()).IsVisibleAsync();
            }
        }

        public async Task VerifyTextBoxFieldsAreDisplayedAsync(Table textBoxFields)
        {
            foreach (var row in textBoxFields.Rows)
            {
                await itemMasterPage.GetTextBox(row[0].ToLower()).IsVisibleAsync();
            }
        }

        public async Task VerifyRadioButtonIsDisplayedAsync(Table table)
        {
            foreach (var row in table.Rows)
            {
                await itemMasterPage.GetRadioButton(row[0].ToLower()).IsVisibleAsync();
            }
        }

        public async Task VerifyCheckboxIsDisplayedAsync(string checkbox)
        {
            await itemMasterPage.GetDropdownInputField(checkbox.ToLower()).IsVisibleAsync();
        }

        public async Task VerifyFieldsCountAsync(string expectedCount, Table table)
        {
            await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);
            await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());

            foreach (var row in table.Rows)
            {
                var actualCount = await itemMasterPage.GetCountBox(row[0].ToLower()).InnerTextAsync();
                ValidationHelper.AssertEqual(row[0], expectedCount, actualCount, $"{row[0]} count box should {expectedCount}");
            }
        }

        public async Task VerifyFieldsCountIsGreaterThanAsync(int expectedCount, Table table)
        {
            foreach (var row in table.Rows)
            {
                await itemMasterPage.GetCountBox(row[0].ToLower()).IsVisibleAsync();
                var countText = await itemMasterPage.GetCountBox(row[0].ToLower()).TextContentAsync();

                if (!int.TryParse(countText, out var count) || count <= expectedCount)
                {
                    ValidationHelper.Fail($"Count for {row[0]} is not greater than {expectedCount} but found: {countText}");
                }
            }
        }

        public async Task VerifyFieldsAreNotEmptyAsync(Table table)
        {
            foreach (var row in table.Rows)
            {
                var value = await itemMasterPage.GetDropdownInputField(row[0].ToLower()).InputValueAsync();
                Assert.False(value == null, $"{row[0]} displaying as empty.");
            }
        }

        public async Task VerifyFieldsAreEmptyAsync(Table fieldNames)
        {
            foreach (var row in fieldNames.Rows)
            {
                var field = row[0];
                var value = await itemMasterPage.GetRightCornerInputField(field.ToLower()).InputValueAsync();
                Assert.IsTrue(string.IsNullOrWhiteSpace(value), $"Expected field '{field}' to be empty, but found '{value}'.");
            }
        }

        public async Task VerifyFieldValuesAreDisplayedAsync(Table fieldNames)
        {
            foreach (var row in fieldNames.Rows)
            {
                var field = row[0];
                var value = await itemMasterPage.GetRightCornerInputField(field.ToLower()).InputValueAsync();
                Assert.IsFalse(string.IsNullOrWhiteSpace(value), $"Expected field '{field}' to be empty but found '{value}'.");
            }
        }

        public async Task VerifyFieldValueDisplayedForItemIdAsync(Table table)
        {
            foreach (var row in table.Rows)
            {
                var value = await itemMasterPage.GetDropdownInputField(row[0].ToLower()).InputValueAsync();
                Assert.IsTrue(value == null, $"{row[0]} displaying as empty.");
            }
        }

        public async Task VerifyTextIsDisplayedForFieldsAsync(Table table)
        {
            foreach (var row in table.Rows)
            {
                await itemMasterPage.GetInputTab(row[0].ToLower()).ClickAsync();
                var value = await itemMasterPage.GetInputField(row[0].ToLower()).InputValueAsync();
                Assert.False(value == null, $"{row[0]} displaying as empty.");
            }
        }

        public async Task VerifyFieldStatusAsync(string fieldName, string expectedStatus)
        {
            var actualStatus = await itemMasterPage.GetRightCornerInputField(fieldName.ToLower()).InputValueAsync();
            ValidationHelper.AssertEqual(fieldName, expectedStatus, actualStatus, $"Item {fieldName} should be {expectedStatus}");
        }

        public async Task EnterHeaderFieldsAsync(Table table)
        {
            await page.WaitForLoadStateAsync();
            var testCaseName = scenarioContext.ScenarioInfo.Title;

            foreach (var row in table.Rows)
            {
                var fieldLocator = row[0].ToLower();
                var getValue = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.{row[0]}");

                if (row[0] == ItemConstants.ItemId)
                {
                    await EnterItemIdAsync(fieldLocator, getValue);
                }
                else
                {
                    await EnterDropdownFieldAsync(fieldLocator, getValue);
                }
            }
        }

        private async Task EnterItemIdAsync(string fieldLocator, string baseValue)
        {
            var itemIdValue = $"{baseValue}-{Guid.NewGuid().ToString().Substring(0, 4).ToUpper()}";
            var field = itemMasterPage.GetDropdownInputField(fieldLocator);

            await field.ClickAsync();
            await BasePage.TypeTextAsync(field, itemIdValue);
            featureContext["itemIdValue"] = itemIdValue;
        }

        private async Task EnterDropdownFieldAsync(string fieldLocator, string value)
        {
            var field = itemMasterPage.GetDropdownInputField(fieldLocator);

            await field.ClickAsync();
            await BasePage.ClearInputIfNotEmptyAsync(field);
            await BasePage.TypeTextAsync(field, value);
            await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
            await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);

            var isDropdownOpen = await commonLocators.GetDropdownListBox().IsVisibleAsync();
            if (isDropdownOpen)
            {
                await commonLocators.GetSelectDropdownOption(value).ClickAsync();
            }
        }

        public async Task EnterTextIntoFieldAsync(string textField)
        {
            try
            {
                await itemMasterPage.GetInternalExternalNotesTab(textField).ClickAsync();

                var testCaseName = scenarioContext.ScenarioInfo.Title;
                var getValue = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.{textField}");

                if (textField.Contains(ItemConstants.ItemMasterExternalNotes, StringComparison.OrdinalIgnoreCase))
                {
                    scenarioContext["ExternalNotes"] = getValue;
                }
                else if (textField.Contains(ItemConstants.ItemMasterInternalNotes, StringComparison.OrdinalIgnoreCase))
                {
                    scenarioContext["InternalNotes"] = getValue;
                }

                var inputField = itemMasterPage.GetInputField(textField);
                await BasePage.ClearInputIfNotEmptyAsync(inputField);
                await inputField.FillAsync(getValue);
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to enter text into the '{textField}' field in Item Master. Error: {ex.Message}", ex);
            }
        }

        public async Task SelectFromDropdownAsync(string value, string fieldName)
        {
            await itemMasterPage.GetRightCornerInputField(fieldName.ToLower()).ClickAsync();
            await BasePage.ClearInputIfNotEmptyAsync(itemMasterPage.GetRightCornerInputField(fieldName.ToLower()));
            await itemMasterPage.GetRightCornerInputField(fieldName.ToLower()).IsEnabledAsync();
            await itemMasterPage.GetRightCornerInputField(fieldName.ToLower()).FillAsync(value);
            await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
            await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);

            var isDropdownOpen = await commonLocators.GetDropdownListBox().IsVisibleAsync();
            if (isDropdownOpen)
            {
                await commonLocators.GetSelectDropdownOption(value).ClickAsync();
            }
        }

        public async Task EnterProductFieldsAsync(Table fieldNames)
        {
            var testCaseName = scenarioContext.ScenarioInfo.Title;

            foreach (var row in fieldNames.Rows)
            {
                var fieldLocator = row[0].ToLower();
                var inputValue = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.{row[0]}");

                await itemMasterPage.GetRightCornerInputField(fieldLocator).ClickAsync();
                await itemMasterPage.GetRightCornerInputField(fieldLocator).FillAsync(inputValue);
                await page.WaitForLoadStateAsync();
                await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);

                var isDropdownOpen = await commonLocators.GetDropdownListBox().IsVisibleAsync();
                if (isDropdownOpen)
                {
                    await commonLocators.GetSelectDropdownOption(inputValue).ClickAsync();
                }
            }
        }

        public async Task EnterProductFieldsWithoutDropdownAsync(Table fieldNames)
        {
            var testCaseName = scenarioContext.ScenarioInfo.Title;

            foreach (var row in fieldNames.Rows)
            {
                var fieldLocator = row[0].ToLower();
                var inputValue = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.{row[0]}");

                await itemMasterPage.GetRightCornerInputField(fieldLocator).ClickAsync();
                await itemMasterPage.GetRightCornerInputField(fieldLocator).FillAsync(inputValue);
                await page.WaitForLoadStateAsync();
                await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);
            }
        }

        public async Task VerifyRadioButtonIsSelectedAsync(string radioButton)
        {
            var isChecked = await itemMasterPage.GetRadioButton(radioButton.ToLower()).IsCheckedAsync();
            Assert.True(isChecked, $"{radioButton} radio button should be selected");
        }

        public async Task VerifyRadioButtonIsUnselectedAsync(string radioButton)
        {
            var isChecked = await itemMasterPage.GetRadioButton(radioButton.ToLower()).IsCheckedAsync();
            Assert.False(isChecked, $"{radioButton} radio button should be unselected");
        }

        public async Task VerifyCheckboxesAreUncheckedAsync(Table fieldNames)
        {
            foreach (var row in fieldNames.Rows)
            {
                var isChecked = await itemMasterPage.GetCheckbox(row[0].ToLower()).IsCheckedAsync();
                Assert.False(isChecked, $"{row[0]} checkbox should be unchecked");
            }
        }

        public async Task SelectRadioButtonAsync(string fieldName)
        {
            try
            {
                await itemMasterPage.GetRadioButton(fieldName.ToLower()).ClickAsync();
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"[FAIL] Failed to select radio button '{fieldName}'.", e);
            }
        }

        public async Task SetCheckboxStateAsync(string checkboxName, string shouldBeChecked)
        {
            try
            {
                var checkboxLocator = itemMasterPage.GetDropdownInputField(checkboxName.ToLower());
                bool isChecked = await checkboxLocator.IsCheckedAsync();
                bool desiredState = bool.Parse(shouldBeChecked);

                if (isChecked != desiredState)
                {
                    await checkboxLocator.ClickAsync();
                }
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"Failed to set '{checkboxName}' checkbox to '{shouldBeChecked}'.", e);
            }
        }

        public async Task CheckDeffCheckboxAsync(string checkboxName)
        {
            var checkboxLocator = itemMasterPage.GetDropdownInputField(checkboxName.ToLower());
            var isChecked = await checkboxLocator.IsCheckedAsync();

            if (isChecked)
            {
                await checkboxLocator.ClickAsync(); //uncheck
                await checkboxLocator.ClickAsync(); //check
            }
            else
            {
                await checkboxLocator.ClickAsync(); //check
            }
        }

        public async Task SelectExplicitTabCheckboxesAsync(Table fieldsToCheck)
        {
            foreach (var row in fieldsToCheck.Rows)
            {
                await itemMasterPage.GetExplicitCheckBox(row[0]).ClickAsync();
            }
        }

        public async Task ValidateCheckboxStatesFromExcelAsync(Table table)
        {
            try
            {
                foreach (var row in table.Rows)
                {
                    var fieldName = row[0].Trim();
                    var expected = featureContext[fieldName]?.ToString()?.Trim().ToUpper();
                    bool expectedChecked = expected == "Y";
                    var isChecked = await itemMasterPage.GetRadioButton(fieldName.ToLower()).IsCheckedAsync();
                    ValidationHelper.AssertEqual(fieldName, expectedChecked, isChecked, $"Mismatch in checkbox state for '{fieldName}'.");
                }
            }
            catch (Exception e)
            {
                ValidationHelper.Fail("Fail to validate isProduct isComponent in the Item Creation Tab", e);
            }
        }

        public async Task RecallCreatedItemIdAsync(string itemId)
        {
            await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);
            var itemIdField = itemMasterPage.GetDropdownInputField(itemId.ToLower());
            await itemIdField.ClickAsync();

            var inputValue = featureContext["itemIdValue"].ToString() ?? string.Empty;
            await itemIdField.IsEnabledAsync();
            await BasePage.TypeTextAsync(itemIdField, inputValue);
            await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
            await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);

            var isDropdownOpen = await commonLocators.GetDropdownListBox().IsVisibleAsync();
            if (isDropdownOpen)
            {
                await commonLocators.GetSelectDropdownOption(inputValue).ClickAsync();
            }
        }

        public async Task RecallFromBaselineScreenAsync(string fieldName)
        {
            var fieldLocator = fieldName.ToLower();
            var inputValue = featureContext["ItemId"].ToString();

            await itemMasterPage.GetDropdownInputField(fieldLocator).ClickAsync();
            await BasePage.TypeTextAsync(itemMasterPage.GetDropdownInputField(fieldLocator), inputValue);
            await page.WaitForLoadStateAsync();
            await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);

            var isDropdownOpen = await commonLocators.GetDropdownListBox().IsVisibleAsync();
            if (isDropdownOpen)
            {
                await commonLocators.GetSelectDropdownOption(inputValue).ClickAsync();
            }
        }

        public async Task GetFieldValueAsync(string fieldName)
        {
            featureContext["ItemId"] = await itemMasterPage.GetDropdownInputField(fieldName.ToLower()).InputValueAsync();
        }

        public async Task RecallLOBInExplicitTabAsync(string itemId)
        {
            var lobName = featureContext["ItemId"].ToString();
            await itemMasterPage.GetDropdownInputField(itemId.ToLower()).FillAsync(lobName);
        }

        public async Task GetMultipleFieldValuesAsync(string field1, string field2, string field3)
        {
            featureContext["LOB"] = await itemMasterPage.GetDropdownInputField(field1.ToLower()).InputValueAsync();
            featureContext["OEM"] = await itemMasterPage.GetDropdownInputField(field2.ToLower()).InputValueAsync();
            featureContext["Description"] = await itemMasterPage.GetDropdownInputField(field3.ToLower()).InputValueAsync();
        }

        public async Task RecallItemIdFromBOMAsync(string itemId)
        {
            await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);
            var itemIdField = itemMasterPage.GetDropdownInputField(itemId.ToLower());
            await itemIdField.ClickAsync();

            var fieldName = scenarioContext[BomConstants.FeatureContextValue.BOMLineValues] as Dictionary<string, string>;
            await itemIdField.IsEnabledAsync();
            var inputValue = fieldName["Sku"];

            await BasePage.TypeTextAsync(itemIdField, inputValue);
            await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
            await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);

            var isDropdownOpen = await commonLocators.GetDropdownListBox().IsVisibleAsync();
            if (isDropdownOpen)
            {
                await commonLocators.GetSelectDropdownOption(inputValue).ClickAsync();
            }
        }

        public async Task VerifyButtonsAreEnabledAsync(string button1, string button2)
        {
            await BasePage.VerifyFieldIsEnabled(itemMasterPage.GetButtonName(button1.ToLower()), $"{button1}");
            await BasePage.VerifyFieldIsEnabled(itemMasterPage.GetButtonName(button2.ToLower()), $"{button2}");
        }

        public async Task VerifyButtonsAreDisabledAsync(string button1, string button2)
        {
            await BasePage.VerifyFieldIsDisabled(itemMasterPage.GetButtonName(button1.ToLower()), $"{button1}");
            await BasePage.VerifyFieldIsDisabled(itemMasterPage.GetButtonName(button2.ToLower()), $"{button2}");
        }

        public async Task HandleOKPopupAsync()
        {
            var testCaseName = scenarioContext.ScenarioInfo.Title;
            var inputValue = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.Type");
            var errorTitle = await commonLocators.GetErrorTitlePopupMessage().IsVisibleAsync();

            if (errorTitle)
            {
                await commonLocators.GetPopupOkButton().ClickAsync();
                await itemMasterPage.GetCharacteristicButton(ButtonNames.UndoButton).ClickAsync();
                await commonLocators.GetSuccessTitlePopup().IsVisibleAsync();
                await commonLocators.GetPopupYesButton().ClickAsync();
                await itemMasterPage.GetCharacteristicKindOrType(inputValue).ClickAsync();
            }
            else
            {
                await commonLocators.GetSavePopupMessage().IsVisibleAsync();
                await commonLocators.GetPopupYesButton().IsVisibleAsync();
                await commonLocators.GetPopupYesButton().ClickAsync();
                await commonLocators.GetSuccessTitlePopup().IsVisibleAsync();
                await commonLocators.GetPopupOkButton().ClickAsync();
            }
        }

        public async Task VerifyNoDataFoundAsync()
        {
            await itemMasterPage.GetNoDataFound().IsVisibleAsync();
        }

        public async Task RecallAndVerifyOemAsync(string oemDropdown, string expectedStatus)
        {
            var oemName = string.Empty;
            try
            {
                oemName = featureContext[Constants.AdminConstants.FeatureContextValues.OemName].ToString();
                var oemField = itemMasterPage.GetDropdownInputField(oemDropdown);
                await VerifyOemDropdownAsync(oemField, oemName, expectedStatus);
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"Validate oem {oemName} which is in {expectedStatus} state", e);
            }
        }

        public async Task VerifyOemDropdownAsync(ILocator oemField, string oemName, string expectedStatus)
        {
            try
            {
                await BasePage.TypeTextAsync(oemField, oemName);
                await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
                await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);

                var isDropdownOpen = await commonLocators.GetDropdownListBox().IsVisibleAsync();

                if (expectedStatus.Equals(Constants.AdminConstants.ActiveState, StringComparison.OrdinalIgnoreCase))
                {
                    if (!isDropdownOpen)
                    {
                        ValidationHelper.Fail($"[FAIL] Active OEM '{oemName}' was NOT found in dropdown.");
                    }
                    else
                    {
                        await commonLocators.GetSelectDropdownOption(oemName).ClickAsync();
                    }
                }
                else if (expectedStatus.Equals(Constants.AdminConstants.InactiveState, StringComparison.OrdinalIgnoreCase))
                {
                    var expectedMessage = Constants.AdminConstants.ErrorToastMessages.OemInactive;
                    var actualPopupMessage = itemMasterPage.GetToastMessage(expectedMessage);

                    if (!await actualPopupMessage.IsVisibleAsync())
                    {
                        ValidationHelper.Fail($"[FAIL] Expected error message for inactive {oemField} '{oemName}' was NOT shown.");
                    }
                }
                else
                {
                    throw new ArgumentException($"Invalid status '{expectedStatus}'. Use 'Active' or 'Inactive'.");
                }
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"Verification of OEM dropdown failed for OEM '{oemName}' with expected status '{expectedStatus}'", e);
            }
        }
    }
}

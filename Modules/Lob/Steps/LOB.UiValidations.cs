using Microsoft.Playwright;
using NUnit.Framework;
using PIMQAAutomation.Common.Pages;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Modules.Lob.Constants;
using PIMQAAutomation.Utilities;
using TechTalk.SpecFlow;

namespace PIMQAAutomation.Modules.Lob.Steps
{
    public partial class LOB
    {
        [Then("User navigates to LOB Page")]
        public async Task ThenUserNavigatesToLOBPage()
        {
            try
            {
                await lobPage.NavigateToLineOfBusinessAsync();
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"User is not navigated to LOB Page.", e);
            }
        }

        [Then(@"User enters ""(.*)"" in ""(.*)"" subtab")]
        public async Task ThenUserEntersInSubtab(string rank, string subTab)
        {
            try
            {
                int priorityRank = new Random().Next(LOBConstants.MinPriorityRank, LOBConstants.MaxPriorityRank); // Generate a random number between 1000 and 9999
                var priorityRankFieldlocator = lobPage.GetPrimaryLOVsFiled(subTab, rank);
                await priorityRankFieldlocator.ClickAsync();
                await priorityRankFieldlocator.FillAsync(priorityRank.ToString());
                featureContext[LOBConstants.FeatureContextValues.PriorityRank] = priorityRank;
            }
            catch (Exception e)
            {
                var errorMessage = $"Failed to enter rank '{rank}' in subtab '{subTab}'. Error: {e.Message}";
                ValidationHelper.Fail(errorMessage, e);
            }
        }

        [Then(@"Verify created item id is not displayed in ""(.*)"" from baseline screen in ""(.*)"" subtab")]
        public async Task ThenVerifyCreatedItemIdIsNotDisplayedInFromBaselineScreenInSubtab(string fieldName, string subTab)
        {
            try
            {
                string LOBName = featureContext["ItemId"].ToString();
                ValidationHelper.FailIfNullOrEmpty("ItemId", LOBName, "is null or empty in feature context");
                var primaryLovsFiledLocator = lobPage.GetPrimaryLOVsFiled(subTab, fieldName);
                await primaryLovsFiledLocator.ClickAsync();
                await BasePage.TypeTextAsync(primaryLovsFiledLocator, LOBName);
                var dropdownText = await commonLocators.GetEmptyDropdown().TextContentAsync();
                Assert.IsFalse(dropdownText.Equals(LOBName), $"LOB item '{LOBName}' should not be visible in the dropdown.");
            }
            catch (Exception e)
            {
                var errorMessage = $"Failed to verify that item ID '{featureContext["ItemId"]}' is not shown in field '{fieldName}' under '{subTab}' subtab. Error: {e.Message}";
                ValidationHelper.Fail(errorMessage, e);
            }
        }

        [Then(@"User enters data for below fields in ""(.*)"" subtab")]
        public async Task ThenUserEntersDataForBelowFieldsInSubtab(string subTab, Table fieldNames)
        {
            try
            {
                var uiGeneralLedgerValues = new Dictionary<string, string>();
                foreach (var fieldName in fieldNames.Rows)
                {
                    testCaseName = scenarioContext.ScenarioInfo.Title;
                    string fieldLocator = fieldName[0].ToLower();
                    string inputValue = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.{fieldName[0]}");
                    await FillAndSelectLOVAsync(subTab, fieldLocator, inputValue);
                    uiGeneralLedgerValues[fieldName[0]] = inputValue;
                }
                featureContext[LOBConstants.FeatureContextValues.GeneralLedgerCodeDetails] = uiGeneralLedgerValues;
            }
            catch (Exception e)
            {
                var errorMessage = $"Failed to enter field data in subtab '{subTab}'. Error: {e.Message}";
                ValidationHelper.Fail(errorMessage, e);
            }
        }

        [Then(@"User recalls Line of Business ""(.*)"" from baseline screen in ""(.*)"" subtab")]
        public async Task ThenUserRecallsLineOfBusinessFromBaselineScreenInSubtab(string fieldName, string subTab)
        {
            try
            {
                string fieldLocator = fieldName.ToLower();
                string inputValue = featureContext["ItemId"].ToString();
                await FillAndSelectLOVAsync(subTab, fieldLocator, inputValue);
            }
            catch (Exception e)
            {
                var errorMessage = $"Failed to recall Line of Business '{fieldName}' from baseline screen in '{subTab}' subtab. Error: {e.Message}";
                ValidationHelper.Fail(errorMessage, e);
            }
        }

        [Then(@"User enters existing ""(.*)"" in ""(.*)"" subtab")]
        public async Task ThenUserEntersExistingInSubtab(string rank, string tabName)
        {
            try
            {
                var priorityRank = lobPage.GetPrimaryLOVsFiled(tabName, rank);
                await priorityRank.ClickAsync();
                await priorityRank.FillAsync(featureContext[LOBConstants.FeatureContextValues.PriorityRank].ToString());
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"Failed to enter existing value for Rank '{rank}' in tab '{tabName}'", e);
            }
        }

        public async Task FillAndSelectLOVAsync(string subTab, string fieldName, string inputValue)
        {
            try
            {
                var fieldLocator = lobPage.GetPrimaryLOVsFiled(subTab, fieldName);
                await fieldLocator.WaitForAsync(new() { State = WaitForSelectorState.Visible });
                await fieldLocator.ClickAsync();
                await BasePage.ClearInputIfNotEmptyAsync(fieldLocator);
                await BasePage.TypeTextAsync(fieldLocator, inputValue);
                await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);
                var dropdownList = commonLocators.GetDropdownListBox();
                if (await dropdownList.IsVisibleAsync())
                {
                    var dropdownOption = commonLocators.GetSelectDropdownOption(inputValue);
                    await page.WaitForLoadStateAsync();
                    await dropdownOption.ClickAsync();
                }
                else
                {
                    throw new InvalidOperationException($"Dropdown did not open or '{inputValue}' was not found in list for field '{fieldName}' in subtab '{subTab}'.");
                }
            }
            catch (Exception e)
            {
                var errorMessage = $"Failed to select '{inputValue}' for field '{fieldName}' in subtab '{subTab}'. Error: {e.Message}";
                ValidationHelper.Fail(errorMessage, e);
            }
        }

        [Then(@"User sets ""(.*)"" checkbox to ""(.*)""")]
        public async Task ThenUserSetsDeffCheckboxTo(string checkboxName, string shouldBeChecked)
        {
            var checkboxLocator = lobPage.GetPrimaryLOVsFiled(LOBConstants.LocatorProperties.LovToGl, checkboxName);
            bool isChecked = await checkboxLocator.IsCheckedAsync();
            bool desiredState = bool.Parse(shouldBeChecked); // expects "true" or "false"

            if (isChecked != desiredState)
            {
                await checkboxLocator.ClickAsync();
            }
        }

        [Then(@"User verifies the popup displays the expected message ""(.*)""")]
        public async Task ThenUserVerifiesThePopupDisplaysTheExpectedMessage(string errorKeyOrMessage)
        {
            try
            {
                var expectedMessage = LOBConstants.ErrorPopUpMessages.ContainsKey(errorKeyOrMessage)
                    ? LOBConstants.ErrorPopUpMessages[errorKeyOrMessage]
                    : errorKeyOrMessage;

                var actualPopupMessage = commonLocators.GetpopupMessage();
                await Assertions.Expect(actualPopupMessage).ToHaveTextAsync(expectedMessage);
                var isVisible = await commonLocators.GetPopupOkButton().IsVisibleAsync();
                if (isVisible)
                {
                    await commonLocators.GetPopupOkButton().ClickAsync();
                }
            }
            catch (Exception ex)
            {
                var errorMsg = $"Popup validation failed. Error key or message: '{errorKeyOrMessage}', Exception: {ex.Message}";
                ValidationHelper.Fail(errorMsg, ex);
            }
        }

        [Then("Verify below service taxonomy details are auto populated and not editable")]
        public async Task ThenVerifyBelowServiceTaxonomyDetailsNotEditable(Table fieldsToCheck)
        {
            foreach (var field in fieldsToCheck.Rows)
            {
                var fieldName = field[0];
                try
                {
                    var locator = lobPage.GetServiceTaxonomyName(fieldName);
                    Assert.IsTrue(await locator.IsDisabledAsync(), $"{fieldName} is editable but should be read-only.");
                    // Check if field is not empty
                    var fieldValue = (await locator.InputValueAsync()).Trim();
                    ValidationHelper.FailIfNullOrEmpty(fieldName, fieldValue, $"{fieldName} is empty but should contain a value.");
                }
                catch (Exception e)
                {
                    var errorMessage = $"Error validating '{fieldName}': {e.Message}";
                    ValidationHelper.Fail(errorMessage, e);
                }
            }
        }

        [Then("User verifies the following fields are enabled")]
        public async Task ThenUserVerifiesTheFollowingFieldsAreEnabled(Table fieldsToCheck)
        {
            foreach (var field in fieldsToCheck.Rows)
            {
                var fieldName = field[0];
                try
                {
                    await BasePage.VerifyFieldIsEnabled(lobPage.GetPrimaryLOVsFiled(LOBConstants.LocatorProperties.LovToGl, fieldName), fieldName);
                }
                catch (Exception e)
                {
                    var errorMessage = $"Error verifying '{fieldName}' is enabled: {e.Message}";
                    ValidationHelper.Fail(errorMessage, e);
                }
            }
        }

        public async Task<Dictionary<string, string>> GetUiGeneralLedgerCodeDescriptionValuesAsync(Table fieldsToCheck)
        {
            try
            {
                var uiGeneralLedgerDescriptionValues = new Dictionary<string, string>();
                foreach (var field in fieldsToCheck.Rows)
                {
                    var fieldName = field[0];
                    uiGeneralLedgerDescriptionValues[fieldName] = (await lobPage.GetGeneralLedgerDescription(fieldName).TextContentAsync()).Trim();
                }
                return uiGeneralLedgerDescriptionValues;
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"Failed to get values of fields", e);
                return null;
            }
        }
    }
}
using Microsoft.Playwright;
using NUnit.Framework;
using PIMQAAutomation.Common.Pages;
using PIMQAAutomation.Modules.Alternates.Pages;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Modules.Items.Constants;
using PIMQAAutomation.Utilities;
using TechTalk.SpecFlow;

namespace PIMQAAutomation.Modules.Alternates.Steps
{
    public partial class Alternate
    {
        [When(@"User selects ""(.*)"" tab from cluster mode")]
        public async Task WhenUserSelectsTabFromClusterMode(string pageTab)
        {
            await alternateClusterModeActions.SelectTabAsync(pageTab);
        }

        [Then(@"Verify the below text fields are empty in ""(.*)"" panel")]
        public async Task ThenVerifyBelowTextFieldsAreEmpty(string panelName, Table table)
        {
            await alternateClusterModeActions.VerifyTextFieldsEmptyAsync(panelName, table);
        }

        [Then(@"Verify the below buttons are disabled in ""(.*)"" panel")]
        public async Task ThenVerifyBelowButtonsAreDisabled(string panelName, Table table)
        {
            await alternateClusterModeActions.VerifyButtonsDisabledAsync(panelName, table);
        }

        [When(@"User enters ""(.*)"" data for the following fields in ""(.*)"" panel")]
        public async Task WhenEntersActiveItemId(string item, string panelName, Table table)
        {
            await alternateClusterModeActions.EnterItemDataAsync(item, panelName, table);
        }

        [Then(@"Verify the below buttons are enabled in ""(.*)"" panel")]
        public async Task ThenVerifyBelowButtonsAreEnabled(string panelName, Table table)
        {
            await alternateClusterModeActions.VerifyButtonsEnabledAsync(panelName, table);
        }

        [Then(@"Verify the details of below fields in ""(.*)"" panel")]
        public async Task ThenVerifyTheTotalAndTotalActiveFeilds(string panelName, Table table)
        {
            await alternateClusterModeActions.VerifyFieldDetailsAsync(panelName, table);
        }

        [Then(@"Verify the below details of ""(.*)"" in ""(.*)"" interchange master table")]
        public async Task ThenVerifyItemDetailsInMasterInterChangeTable(string item, string panelName, Table table)
        {
            await alternateClusterModeActions.VerifyItemDetailsInMasterTableAsync(item, panelName, table);
        }

        [Then(@"Verify the below cluster details of ""(.*)"" in ""(.*)"" table")]
        public async Task ThenVerifyMultipleClusterItemsInTable(string itemsCsv, string panelName, Table table)
        {
            await alternateClusterModeActions.VerifyClusterDetailsInTableAsync(itemsCsv, panelName, table);
        }

        [Then(@"Verify created and modified username and date in ""(.*)"" panel")]
        public async Task ThenVerifyCreatedAndModifiedUsernameAndDateInRightCornerOfBomHeaderPanelInTab(string panelName, Table table)
        {
            await alternateClusterModeActions.VerifyCreatedModifiedInfoAsync(panelName, table);
        }

        [Then(@"Verify the Display InActive toggle switch is ""(.*)""")]
        public async Task ThenVerifyToggleSwitchIsOff(string state)
        {
            await alternateClusterModeActions.VerifyToggleSwitchStateAsync(state);
        }

        [When(@"User selects the ""(.*)"" tab")]
        public async Task WhenUserSelectsTheTab(string tabName)
        {
            await alternateClusterModeActions.SelectTabAsync(tabName);
        }

        [Then(@"Verify the item ""(.*)"" details of items populated in ""(.*)"" panel")]
        public async Task ThenVerifytheCheckBoxState(string itemId, string panelname)
        {
            await alternateClusterModeActions.VerifyItemDetailsPopulatedAsync(itemId, panelname);
        }

        [Then(@"Verify the ""(.*)"" tab is displayed")]
        public async Task ThenVerifytheTabIsDisplayed(string tabName)
        {
            await alternateClusterModeActions.VerifyTabDisplayedAsync(tabName);
        }

        [Then(@"Verify the same ""(.*)"" is populated in the ""(.*)"" panel")]
        public async Task TheVerifyTheSameItemIsPopulatedInThePanel(string item, string panelName, Table table)
        {
            await alternateClusterModeActions.VerifySameItemPopulatedAsync(item, panelName, table);
        }

        [When(@"User clicks on the ""(.*)"" button from the ""(.*)"" panel")]
        public async Task WhenUserClicksOnTheButton(string buttonName, string panelName)
        {
            await alternateClusterModeActions.ClickButtonAsync(buttonName, panelName);
        }

        [Then(@"Verify the ""(.*)"" pop up message is displayed")]
        public async Task ThenVerifyTheMessagePopUpIsDisplayed(string messageTitle)
        {
            await alternateClusterModeActions.VerifyPopupMessageAsync(messageTitle);
        }

        [Then(@"Verify the ""(.*)"" pop up is displayed in the ""(.*)"" panel")]
        public async Task ThenVerifyThePopUpTiTleIsDisplayed(string expectedTitle, string panelName)
        {
            await alternateClusterModeActions.VerifyPopupTitleAsync(expectedTitle, panelName);
        }

        [Then(@"Verify the ""(.*)"" PopUp is displayed")]
        public async Task ThenVerifyThePopUpIsDisplayed(string title)
        {
            await alternateClusterModeActions.VerifyPopupDisplayedAsync(title);
        }

        [When(@"User selects the ""(.*)"" option")]
        public async Task WhenUserSelectsTheItemOption(string item)
        {
            await alternateClusterModeActions.SelectItemOptionAsync(item);
        }

        [Then(@"Verify the Inactive Clusters grid is displayed")]
        public async Task ThenVerifyTheInactiveClustersGridIsDisplayed()
        {
            await alternateClusterModeActions.VerifyInactiveClustersGridDisplayedAsync();
        }

        [Then(@"Verify the following details in pop up for the ""(.*)"" panel")]
        public async Task ThenVerifyTotalItemsAndActiveItems(string panelName, Table table)
        {
            await alternateClusterModeActions.VerifyPopupDetailsAsync(panelName, table);
        }

        [Then(@"Verify the same item is populated in the pop up ""(.*)"" panel")]
        public async Task TheVerifyTheSameItemIsPopulatedInThePopUpPannel(string panelName, Table table)
        {
            await alternateClusterModeActions.VerifySameItemInPopupAsync(panelName, table);
        }

        [When(@"User from the ""(.*)"" panel selects the ""(.*)"" tab")]
        public async Task WhenUserClicksOnTheTabFromThePanel(string panelName, string tabName)
        {
            await alternateClusterModeActions.SelectPanelTabAsync(panelName, tabName);
        }

        [Then(@"Verify the entered ""(.*)"" is selected automatically in the grid in the ""(.*)"" panel")]
        public async Task ThenVerifyTheEnteredIsSelectedAutomaticallyInTheGrid(string item, string panelName)
        {
            await alternateClusterModeActions.VerifyItemAutoSelectedAsync(item, panelName);
        }

        [Then(@"Verify the entered ""(.*)"" from the cluster is selected automatically in the grid in the ""(.*)"" panel")]
        public async Task ThenVerifyTheEnteredItemFromClusterIsSelectedAutomaticallyInTheGrid(string item, string panelName)
        {
            await alternateClusterModeActions.VerifyClusterItemAutoSelectedAsync(item, panelName);
        }

        [When(@"User selects ""(.*)"" from ""(.*)"" interchange master table")]
        public async Task WhenUserSelectsItemFromMasterInterchangetable(string item, string panelName)
        {
            await alternateClusterModeActions.SelectItemFromMasterTableAsync(item, panelName);
        }

        [Then(@"User verifies ""(.*)"" is not visible on ""(.*)"" interchange master table")]
        public async Task ThenUserVerifiesItemOnMasterInterchangetable(string item, string panelName)
        {
            await alternateClusterModeActions.VerifyItemNotVisibleAsync(item, panelName);
        }

        [When(@"User selects Display Inactive toggle button which is in ""(.*)"" state")]
        public async Task WhenUserSelectsDisplayInactiveButton(string state)
        {
            await alternateClusterModeActions.SelectDisplayInactiveToggleAsync(state);
        }

        [Then(@"Verify the Clusters grid is displayed")]
        public async Task TheVerifyTheClustersGridIsDisplayed()
        {
            await alternateClusterModeActions.VerifyClustersGridDisplayedAsync();
        }

        [Then(@"Verify created and modified date and username for ""(.*)"" panel in ""(.*)"" tab")]
        public async Task ThenVerifyCreatedAndModifiedDateUsernameInTabContainer(string panelName, string tabName, Table table)
        {
            await alternateClusterModeActions.VerifyCreatedModifiedDateInTabAsync(panelName, tabName, table);
        }

        [Then(@"Verify the entered cluster item ""(.*)"" is selected automatically in the grid in the ""(.*)"" panel")]
        public async Task ThenVerifyTheEnteredItemIsSelectedAutomaticallyInTheGrid(string item, string panelName)
        {
            await alternateClusterModeActions.VerifyClusterItemAutoSelectedAsync(item, panelName);
        }

        [Then(@"User verifies and closes the ""(.*)"" pop-up in ""(.*)"" panel, then enters ""(.*)"" data in ""(.*)"" panel for the following fields")]
        public async Task HandlePopupThenEnterData(string expectedTitle, string popupPanelName, string item, string textFieldPanelName, Table inputParametersList)
        {
            await alternateClusterModeActions.HandlePopupThenEnterDataAsync(expectedTitle, popupPanelName, item, textFieldPanelName, inputParametersList);
        }

        [When(@"User confirms the ""(.*)"" by clicking on OK button in Alternates")]
        public async Task WhenUserConfirmsTheSaveChangesByClickingOkButton(string popupTitle)
        {
            await alternateClusterModeActions.ConfirmActionAsync(popupTitle);
        }

        [Then(@"Verify the below cluster details of ""(.*)"" in ""(.*)"" table for ""(.*)"" panel")]
        public async Task ThenVerifyPopUpMultipleClusterItemsInTable(string itemsCsv, string tableName, string panelName, Table inputParametersList)
        {
            await alternateClusterModeActions.VerifyClusterDetailsInPopupTableAsync(itemsCsv, tableName, panelName, inputParametersList);
        }
    }
}
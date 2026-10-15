using NUnit.Framework;
using TechTalk.SpecFlow;

namespace PIMQAAutomation.Modules.Alternates.Steps
{
    public partial class Alternate
    {
        [Then(@"Verify the below text fields are enabled in the ""(.*)"" panel")]
        public async Task ThenVerifyTheBelowTextFieldsAreEnabled(string panelName, Table table)
        {
            await alternateSupersessionActions.VerifyTextFieldsEnabledAsync(panelName, table);
        }

        [Then(@"Verify the Display InActive toggle switch is ""(.*)"" in Supersession Mode")]
        public async Task ThenVerifyToggleSwitchIsOnInSupersessionMode(string state)
        {
            await alternateSupersessionActions.VerifyToggleSwitchStateAsync(state);
        }

        [Then(@"Verify the ""(.*)"" checkbox unchecked state from the ""(.*)"" panel")]
        public async Task ThenVerifyTheClusterToClusterRelationshipUnCheckedCheckboxState(string checkboxName, string panelName)
        {
            await alternateSupersessionActions.VerifyCheckboxUncheckedAsync(checkboxName, panelName);
        }

        [Then(@"Verify the ""(.*)"" checkbox checked state from the ""(.*)"" panel")]
        public async Task ThenVerifyTheClusterToClusterRelationshipCheckedCheckboxState(string checkboxName, string panelName)
        {
            await alternateSupersessionActions.VerifyCheckboxCheckedAsync(checkboxName, panelName);
        }

        [Then(@"Verify the close button in the ""(.*)"" panel")]
        public async Task ThenVerifyCloseButtonSupersession(string panelName)
        {
            await alternateSupersessionActions.VerifyCloseButtonEnabledAsync(panelName);
        }

        [When(@"User clicks on the close button from the ""(.*)"" panel")]
        public async Task WhenUserClicksOnCloseButton(string panelName)
        {
            await alternateSupersessionActions.ClickCloseButtonAsync(panelName);
        }
    }
}
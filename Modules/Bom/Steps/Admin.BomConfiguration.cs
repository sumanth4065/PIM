using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace PIMQAAutomation.Modules.Bom.Steps
{
    public partial class Admin
    {
        [Then("Verify User is on BOM Configuration Screen")]
        public async Task ThenVerifyUserIsOnBOMConfigurationScreen()
        {
            await bomConfiguration.VerifyBomConfigurationTitle();
        }

        [When("User clicks on the New Kind button")]
        public async Task WhenUserClicksOnTheNewKindButton()
        {
            await bomConfiguration.ClickNewKindButton();
        }

        [When(@"User selects ""(.*)"" from the popup menu")]
        public async Task WhenUserSelectsFromThePopupMenu(string option)
        {
            await bomConfiguration.SelectBomStyle(option);
        }

        [When(@"User clicks on the ""(.*)"" button")]
        public async Task WhenUserClicksOnTheButton(string buttonName)
        {
            await bomConfiguration.ClickBomButton(buttonName);
        }

        [Then("Verify the below fields error messages are displayed")]
        public async Task ThenVerifyTheBelowFieldsErrorMessagesAreDisplayed(Table table)
        {
            foreach (var row in table.Rows)
            {
                string field = row[0];
                await bomConfiguration.VerifyBomFieldError(field);
            }
        }

        [Then("Verify character limit error messages are displayed for the below fields with exceeding the data")]
        public async Task ThenVerifyCharacterLimitErrorMessagesAreDisplayedForTheBelowFieldsWithExceedingTheData(Table table)
        {
            foreach (var row in table.Rows)
            {
                string field = row[0];
                await bomConfiguration.ValidateCharLimit(field);
            }
        }

        [When("User enters BOM Configuration details")]
        public async Task WhenUserEntersBOMConfigurationDetails()
        {
            await bomConfiguration.EnterBomConfigurationDetails();
        }

        [Then(@"User Clicks on the ""(.*)"" button")]
        public async Task ThenUserClicksOnTheButton(string buttonName)
        {
            await bomConfiguration.ClickConfirmationButton(buttonName);
        }

        [Then("Verify Created and Modifyed details are displayed")]
        public async Task ThenVerifyCreatedAndModifyedDetailsAreDisplayed()
        {
            await bomConfiguration.ValidateCreateModify();
        }

        [When("User enters numeric values exceeding the limit in the following fields")]
        public async Task WhenUserEntersNumericValuesExceedingTheLimitInTheFollowingFields(Table table)
        {
            foreach (var row in table.Rows)
            {
                string field = row[0];
                await bomConfiguration.ValidateNumberFieldCharLimit(field);
            }
        }
    }
}

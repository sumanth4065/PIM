namespace PIMQAAutomation.Modules.Items.Steps
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using PIMQAAutomation.Modules.Items.Pages;
    using TechTalk.SpecFlow;

    public partial class Admin
    {
        [Then("Verify User is on Product Class Screen")]
        public async Task ThenVerifyUserIsOnProductClassScreen()
        {
            await adminClassActions.VerifyProductClassTitleAsync();
        }

        [When(@"User clicks on the Product Class ""(.*)"" button")]
        public async Task WhenUserClicksOnTheProductClassButton(string buttonName)
        {
            await adminClassActions.ClickProductClassButtonAsync(buttonName);
        }

        [Then("Verify the below product class fields error messages")]
        public async Task ThenVerifyTheBelowProductClassFieldsErrorMessages(Table table)
        {
            foreach (var row in table.Rows)
            {
                string fieldName = row[0];
                await adminClassActions.VerifyProductClassFieldErrorAsync(fieldName);
            }
        }

        [Then("Verify the character limit error message for the below product class fields")]
        public async Task ThenVerifyTheCharacterLimitErrorMessageForTheBelowProductClassFields(Table table)
        {
            foreach (var row in table.Rows)
            {
                string fieldName = row[0];
                await adminClassActions.VerifyMaxCharLimitAsync(fieldName);
            }
        }

        [When("User enters product class details")]
        public async Task WhenUserEntersProductClassDetails()
        {
            await adminClassActions.EnterProductClassDetailsAsync();
        }

        [When("User Searches and selects existing Product Class")]
        public async Task WhenUserSearchesAndSelectsExistingProductClass()
        {
            await adminClassActions.SearchExistingProductClassAsync();
        }

        [When("User unchecks the product class active checkbox")]
        public async Task WhenUserUnchecksTheProductClassActiveCheckbox()
        {
            await adminClassActions.UncheckActiveCheckboxAsync();
        }

        [When(@"User enters valid data into the product class Internal Notes field")]
        public async Task WhenUserEntersValidDataIntoTheProductClassInternalNotesField()
        {
            await adminClassActions.EnterDataIntoInternalNotesAsync();
        }

        [When("User checks the product class active checkbox")]
        public async Task WhenUserChecksTheProductClassActiveCheckbox()
        {
            await adminClassActions.CheckActiveCheckboxAsync();
        }
    }
}

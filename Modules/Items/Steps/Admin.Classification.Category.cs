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
        [Then("Verify User is on Product Category Screen")]
        public async Task ThenVerifyUserIsOnProductCategoryScreen()
        {
            await adminCategoryActions.VerifyProductCategoryTitleAsync();
        }

        [When(@"User clicks on the Product Category ""(.*)"" button")]
        public async Task WhenUserClicksOnTheProductCategoryButton(string buttonName)
        {
            await adminCategoryActions.ClickProductCategoryButtonAsync(buttonName);
        }

        [Then("Verify the below Product Category fields error messages")]
        public async Task ThenVerifyTheBelowProductCategoryFieldsErrorMessages(Table table)
        {
            foreach (var row in table.Rows)
            {
                string fieldName = row[0];
                await adminCategoryActions.VerifyProductCategoryFieldErrorAsync(fieldName);
            }
        }

        [Then("Verify the character limit error message for the below product Category fields")]
        public async Task ThenVerifyTheCharacterLimitErrorMessageForTheBelowProductCategoryFields(Table table)
        {
            foreach (var row in table.Rows)
            {
                string fieldName = row[0];
                await adminCategoryActions.VerifyMaxCharLimitAsync(fieldName);
            }
        }

        [When("User enters Product Category details")]
        public async Task WhenUserEntersProductCategoryDetails()
        {
            await adminCategoryActions.EnterProductCategoryDetailsAsync();
        }

        [When("User Searches and selects existing Product Category")]
        public async Task WhenUserSearchesAndSelectsExistingProductCategory()
        {
            await adminCategoryActions.SearchExistingProductCategoryAsync();
        }

        [When("User unchecks the product Category active checkbox")]
        public async Task WhenUserUnchecksTheProductCategoryActiveCheckbox()
        {
            await adminCategoryActions.UncheckActiveCheckboxAsync();
        }

        [When("User enters valid data into the Product Category Internal Notes field")]
        public async Task WhenUserEntersValidDataIntoTheProductCategoryInternalNotesField()
        {
            await adminCategoryActions.EnterDataIntoInternalNotesAsync();
        }

        [When("User checks the product Category active check box")]
        public async Task WhenUserChecksTheProductCategoryActiveCheckBox()
        {
            await adminCategoryActions.CheckActiveCheckboxAsync();
        }
    }
}

namespace PIMQAAutomation.Modules.Items.Steps
{
    using TechTalk.SpecFlow;

    public partial class Admin
    {
        [When(@"User creates a new Oem with ""(.*)""")]
        public async Task ThenUserCreatesANewOemWithAsync(string newOrExistingGroup)
        {
            await adminOemActions.CreateOem(newOrExistingGroup);
        }

        [Then("User selects existing Oem and change Active status")]
        public async Task ThenUserSelectsExistingOemAndChangeActiveStatus()
        {
            await adminOemActions.UpdateExistingOemActiveStatus();
        }

        [Then(@"User validates below fields with rules")]
        public async Task ThenUserValidatesFieldsWithRules(Table fieldsToCheck)
        {
            await adminOemActions.ValidateFieldsAsync(fieldsToCheck);
        }

        [Then("User modify existing Oem details")]
        public async Task ThenUserModifyExistingOemDetails()
        {
            await adminOemActions.UpdateExistingOemDetails();
        }

        [Then("User verifies modified details")]
        public async Task ThenUserVerifiesModifiedDetails()
        {
            await adminOemActions.VerifyModifiedOemDetails();
        }
    }
}

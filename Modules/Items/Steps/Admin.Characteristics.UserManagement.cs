namespace PIMQAAutomation.Modules.Items.Steps
{
    using TechTalk.SpecFlow;

    public partial class Admin
    {
        [Then(@"User validates the Authorized Users page header")]
        public async Task UserValidatesAuthorizedUsersPageHeader()
        {
            await adminUserManagementActions.VerifyUserManagementPageHeaderAsync();
        }

        [Then(@"User validates the default Characteristic Kind and User details")]
        public async Task UserValidatesDefaultCharacteristicKindAndUserDetails()
        {
            await adminUserManagementActions.VerifyCharKindUserDetailsAsync();
        }

        [When("User modifies the following User Details")]
        public async Task WhenUserModifiesTheUserDetails(Table fieldNames)
        {
            await adminUserManagementActions.ModifyUserDetailsAsync(fieldNames);
        }

        [When(@"User clicks on Update button in User Management")]
        public async Task WhenUserClicksOnUpdateButton()
        {
            // This method is now part of ModifyUserDetailsAsync
            // If needed separately, we can extract it from AdminUserManagementActions
            // For now, this is intentionally empty as the Update button click is part of ModifyUserDetails
        }

        [When(@"User searches for a Characteristic Kind selects a User and validates the user details")]
        public async Task WhenUserSearchesAndSelectsCharKindAndUserAndValidateUserDetails(Table table)
        {
            await adminUserManagementActions.ValidateUsersDetailsForSelectedCharKindAndUserAsync(table);
        }
    }
}

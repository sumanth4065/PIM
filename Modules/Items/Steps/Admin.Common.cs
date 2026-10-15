namespace PIMQAAutomation.Modules.Items.Steps
{
    using AventStack.ExtentReports.Gherkin.Model;
    using TechTalk.SpecFlow;

    public partial class Admin
    {
        [Then(@"User validates the Save Changes pop up and clicks on Yes button")]
        public async Task ThenUserValidatesSaveChangesPopUpAndClickYesButton()
        {
            await adminCommonActions.ValidateSaveChangesPopupAsync();
        }

        [Then(@"User validates the toast message")]
        public async Task ThenUserValidatesTheToastMessage(Table fieldName)
        {
            await adminCommonActions.ValidateToastMessageAsync(fieldName);
        }

        [Then(@"User validates mandatory field error messages for ""(.*)""")]
        public async Task ThenUserValidatesMandatoryFieldErrorMessagesFor(string moduleName)
        {
            await adminCommonActions.CheckFieldErrors(moduleName);
        }

        [When(@"User creates a new ""(.*)"" with the fields and validates the error messages")]
        public async Task WhenUserCreatesANewCategoryWithFields(string moduleName, Table fieldName)
        {
            await adminCommonActions.EnterLongTextAndVerifyErrorAsync(fieldName);
        }

        [When("User clicks on the Log Out button")]
        public async Task WhenUserClicksOnLogOutButton()
        {
            await adminCommonActions.Logout();
        }

        [Then(@"User validates the Warning pop up and clicks on ""(.*)"" button")]
        public async Task ThenUserValidatesWarningPopUpAndClickYesButton(string buttonType)
        {
            await adminCommonActions.ValidateWarningPopupAsync(buttonType);
        }
    }
}

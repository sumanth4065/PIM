namespace PIMQAAutomation.Modules.Items.Steps
{
    using AventStack.ExtentReports.Gherkin.Model;
    using PIMQAAutomation.Modules.Items.Pages;
    using TechTalk.SpecFlow;

    public partial class Admin
    {
        [When(@"User creates a new Characteristic Kind")]
        public async Task WhenUserCreatesCharacteristicKind()
        {
            await adminStandardCharacteristicsActions.CreateCharKindAsync();
        }

        [When(@"User creates a new Characteristic Type")]
        public async Task WhenUserCreatesCharacteristicType()
        {
            await adminStandardCharacteristicsActions.CreateCharTypeAsync();
        }

        [Then(@"User validates the Standard Characteristics page header")]
        public async Task ThenUserValidatesStandardPageHeader()
        {
            await adminStandardCharacteristicsActions.ValidateStandardPageHeaderAsync();
        }

        [Then(@"User validates the selected default Characteristic Type details")]
        public async Task ThenUserValidatesDefaultCharacteristicType()
        {
            await adminStandardCharacteristicsActions.ValidateDefaultCharacteristicTypeAsync();
        }

        [When(@"User clicks on Save button in Characteristics")]
        public async Task WhenUserClicksOnSaveButtonInCharacteristicKind()
        {
            await adminStandardCharacteristicsActions.ClickCharacteristicStandardSaveButtonAsync();
        }

        [Then(@"User validates the default Characteristic Kind details")]
        public async Task ThenUserValidatesDefaultCharacteristicKind()
        {
            await adminStandardCharacteristicsActions.ValidateDefaultCharacteristicKindAsync();
        }

        [Then(@"User validates the searched Characteristic Kind details")]
        public async Task ThenUserValidatesSearchedCharacteristicKind()
        {
            await adminStandardCharacteristicsActions.ValidateCharacteristicKindAsync();
        }

        [When(@"User ""(.*)"" the Characteristic ""(.*)"" details")]
        public async Task ThenUserEditsTheCharacteristicKind(string actionType, string entityType)
        {
            await adminStandardCharacteristicsActions.ManageCharacteristicEntityAsync(entityType, actionType);
        }

        [When("User creates a new Characteristic Kind without mandatory details")]
        public async Task WhenUserCreatesANewCharacteristicKindWithoutMandatoryDetails()
        {
            await adminStandardCharacteristicsActions.CreateCharKindErrorMsgsAsync();
        }

        [When("User creates a new Characteristic Type without mandatory details")]
        public async Task WhenUserCreatesANewCharacteristicTypeWithoutMandatoryDetails()
        {
            await adminStandardCharacteristicsActions.CreateCharTypeErrorMsgsAsync();
        }
    }
}

namespace PIMQAAutomation.Modules.Items.Steps
{
    using TechTalk.SpecFlow;

    public partial class Admin
    {
        [Then("User Validates the Value page header")]
        public async Task ThenUserValidatesTheValuePageHeader()
        {
            await adminValueActions.ValidateValuePageHeaderAsync();
        }

        [Then("User Validates the selected default Characteristic Kind and Characteristic LOV details in Value")]
        public async Task ThenUserValidatesTheSelectedDefaultCharacteristicKindAndLOVInValue()
        {
            await adminValueActions.VerifyCharKindAndLOVDetailsAsync();
        }

        [When("User creates New LOV value")]
        public async Task WhenUserCreatesNewLOVValueWithTheFollowingDetails()
        {
            await adminValueActions.CreateNewLOVValueAsync();
        }

        [When("User searches existing Characteristic Kind")]
        public async Task WhenUserSearchesCharKindInValue(Table fieldNames)
        {
            await adminValueActions.SearchKindAsync(fieldNames);
        }

        [When("User creates a new Value without mandatory details")]
        public async Task WhenUserCreatesANewValueWithoutMandatoryDetails()
        {
            await adminValueActions.CreateNewValueErrorMsgsAsync();
        }
    }
}

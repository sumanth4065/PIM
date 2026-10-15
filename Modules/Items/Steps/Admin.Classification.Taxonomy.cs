namespace PIMQAAutomation.Modules.Items.Steps
{
    using AventStack.ExtentReports.Gherkin.Model;
    using TechTalk.SpecFlow;

    public partial class Admin
    {
        [Then("User validates the Product Taxonomy and Service Taxonomy")]
        public async Task ThenUserValidatesProductTaxonomyAndServiceTaxonomy()
        {
            await adminClassificationTaxonomyActions.VerifyProductAndServiceTaxonomyAsync();
        }

        [Then("User validates the Taxonomy page header")]
        public async Task ThenUserValidatesTaxonomyPageHeader()
        {
            await adminClassificationTaxonomyActions.VerifyPageHeaderAsync();
        }

        [Then("User validates the selected default Taxonomy Type And Oem details")]
        public async Task ThenUserValidatesDefaultTaxonomyTypeAndOemDetails()
        {
            await adminClassificationTaxonomyActions.VerifyDefaultSelectedTaxonomyAsync();
        }

        [When("User creates Product Taxonomy")]
        public async Task WhenUserCreatesProductTaxonomy(Table fieldName)
        {
            await adminClassificationTaxonomyActions.CreateTaxonomyAsync(fieldName);
        }

        [When("User creates Service Taxonomy")]
        public async Task WhenUserCreatesServiceTaxonomy(Table fieldname)
        {
            await adminClassificationTaxonomyActions.CreateTaxonomyAsync(fieldname);
        }

        [When("User Edits the Product Taxonomy")]
        public async Task WhenUserEditsProductTaxonomy(Table fieldName)
        {
            await adminClassificationTaxonomyActions.EditTaxonomyAsync(fieldName);
        }

        [When("User Edits the Product Taxonomy")]
        public async Task WhenUserEditsServiceTaxonomy(Table fieldName)
        {
            await adminClassificationTaxonomyActions.EditTaxonomyAsync(fieldName);
        }

        [Then("User validates the selected default Taxonomy type option")]
        public async Task ThenUserValidatesSelectedTaxonomyOption()
        {
            await adminClassificationTaxonomyActions.ValidateSelectedTaxonomyOptionAsync();
        }

        [When("User searches for the value in the below field")]
        public async Task WhenUserSearchesForExistingEntity(Table fieldName)
        {
            await adminClassificationTaxonomyActions.SearchTaxonomyByHierarchyAsync(fieldName);
        }

        [When("User creates Taxonomy for below entity")]
        public async Task WhenUserCreates(Table fieldName)
        {
            await adminClassificationTaxonomyActions.CreateTaxonomyAsync(fieldName);
        }

        [When("User creates Taxonomy to validate warning pop up for below entity")]
        public async Task When(Table fieldName)
        {
            await adminClassificationTaxonomyActions.CreateTaxonomyWarningMsgAsync(fieldName);
        }

        [When("User creates a Service Taxonomy with the following")]
        public async Task WhenUserCreatesService(Table fieldName)
        {
            await adminClassificationTaxonomyActions.CreateTaxonomyAsync(fieldName);
        }

        [When("User edits the existing Taxonomy")]
        public async Task WhenEdits(Table fieldName)
        {
            await adminClassificationTaxonomyActions.EditTaxonomyAsync(fieldName);
        }

        [When("User creates Taxonomy without mandatory details")]
        public async Task WhenUserCreatesTaxonomyWithoutMandatoryDetails()
        {
            await adminClassificationTaxonomyActions.CreateTaxonomyErrorMsgsAsync();
        }

        [When("User reactivates the existing Taxonomy")]
        public async Task WhenUserReactivatesTheExistingTaxonomy()
        {
            await adminClassificationTaxonomyActions.ReactivateTaxonomyAsync();
        }

        [When(@"User selects option ""(.*)"" Taxonomy")]
        public async Task WhenUserSelectsOption(string taxonomyType)
        {
            await adminClassificationTaxonomyActions.SelectTaxonomyAsync(taxonomyType);
        }

        [When("User attempts to navigate away from the current page by selecting the below tab")]
        public async Task WhenUserAttemptsToNavigateAwayFromTheCurrentPage(Table fieldName)
        {
            await adminClassificationTaxonomyActions.NavigateAwayWithoutSavingAsync(fieldName);
        }
    }
}

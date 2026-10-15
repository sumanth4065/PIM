using Microsoft.Playwright;
using PIMQAAutomation.Modules.Items.Actions;
using TechTalk.SpecFlow;
namespace PIMQAAutomation.Modules.Items.Steps
{
    

    [Binding]
    public partial class Item
    {
        private readonly IPage page;
        private readonly FeatureContext featureContext;
        private readonly ScenarioContext scenarioContext;

        // Action class instances - following POM pattern
        private readonly ItemCommonActions itemCommonActions;
        private readonly ItemMasterActions itemMasterActions;
        private readonly ItemTaxonomyActions itemTaxonomyActions;
        private readonly ItemExcelActions itemExcelActions;
        private readonly ItemCharacteristicActions itemCharacteristicActions;
        private readonly ItemIDHActions itemIDHActions;
        private readonly ItemPIMDefaultActions itemPIMDefaultActions;
        private readonly ItemPIMPoolActions itemPIMPoolActions;
        private readonly ItemPIMServiceDefaultActions itemPIMServiceDefaultActions;

        public Item(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            page = featureContext["Page"] as IPage;
            this.featureContext = featureContext;
            this.scenarioContext = scenarioContext;

            // Initialize Common Actions first (shared helpers)
            itemCommonActions = new ItemCommonActions(page, scenarioContext, featureContext);

            // Initialize Action classes - POM pattern
            itemMasterActions = new ItemMasterActions(page, scenarioContext, featureContext);
            itemTaxonomyActions = new ItemTaxonomyActions(page, scenarioContext, featureContext);
            itemExcelActions = new ItemExcelActions(page, scenarioContext, featureContext);
            itemCharacteristicActions = new ItemCharacteristicActions(page, scenarioContext, featureContext);
            itemIDHActions = new ItemIDHActions(scenarioContext, featureContext);
            itemPIMDefaultActions = new ItemPIMDefaultActions(page, scenarioContext, featureContext);
            itemPIMPoolActions = new ItemPIMPoolActions(page, scenarioContext, featureContext);
            itemPIMServiceDefaultActions = new ItemPIMServiceDefaultActions(page, scenarioContext, featureContext);
        }

        [Then(@"User verifies product class and category updated in ""(.*)"" document")]
        public async Task ThenUserVerifiesProductClassAndCategoryUpdated(string documentType, Table inputValues)
        {
            await itemCommonActions.VerifyProductClassAndCategoryAsync(documentType, inputValues);
        }
    }
}
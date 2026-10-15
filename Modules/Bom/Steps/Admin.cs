using Microsoft.Playwright;
using PIMQAAutomation.Modules.Bom.Pages;
using PIMQAAutomation.Modules.Items.Actions;
using TechTalk.SpecFlow;

namespace PIMQAAutomation.Modules.Bom.Steps
{
    [Binding]
    public partial class Admin
    {
        private readonly IPage page;
        private readonly AdminCommonActions adminCommonActions;
        private readonly AdminValueActions adminValueActions;
        private readonly ScenarioContext scenarioContext;
        private readonly FeatureContext featureContext;
        private readonly BomConfiguration bomConfiguration;

        public Admin(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            page = featureContext["Page"] as IPage;
            this.scenarioContext = scenarioContext;
            this.featureContext = featureContext;
            adminCommonActions = new AdminCommonActions(page, scenarioContext, featureContext);
            adminValueActions = new AdminValueActions(page, scenarioContext, featureContext);
            bomConfiguration = new BomConfiguration(page, scenarioContext, featureContext);
        }
    }
}

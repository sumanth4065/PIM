using Microsoft.Playwright;
using PIMQAAutomation.Modules.Items.Actions;
using TechTalk.SpecFlow;

namespace PIMQAAutomation.Modules.Items.Steps
{
    [Binding]
    public partial class Admin
    {
        private readonly IPage page;
        private readonly ScenarioContext scenarioContext;
        private readonly FeatureContext featureContext;

        // Action class instances - following POM pattern
        private readonly AdminCommonActions adminCommonActions;
        private readonly AdminOemActions adminOemActions;
        private readonly AdminNavigationActions adminNavigationActions;
        private readonly AdminCategoryActions adminCategoryActions;
        private readonly AdminClassActions adminClassActions;
        private readonly AdminStandardCharacteristicsActions adminStandardCharacteristicsActions;
        private readonly AdminUserManagementActions adminUserManagementActions;
        private readonly AdminValueActions adminValueActions;
        private readonly AdminClassificationTaxonomyActions adminClassificationTaxonomyActions;

        public Admin(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            page = featureContext["Page"] as IPage;
            this.scenarioContext = scenarioContext;
            this.featureContext = featureContext;

            // Initialize Action classes - POM pattern
            adminCommonActions = new AdminCommonActions(page, scenarioContext, featureContext);
            adminOemActions = new AdminOemActions(page, scenarioContext, featureContext);
            adminNavigationActions = new AdminNavigationActions(page, scenarioContext, featureContext);
            adminCategoryActions = new AdminCategoryActions(page, scenarioContext, featureContext);
            adminClassActions = new AdminClassActions(page, scenarioContext, featureContext);
            adminStandardCharacteristicsActions = new AdminStandardCharacteristicsActions(page, scenarioContext, featureContext);
            adminUserManagementActions = new AdminUserManagementActions(page, scenarioContext, featureContext);
            adminValueActions = new AdminValueActions(page, scenarioContext, featureContext);
            adminClassificationTaxonomyActions = new AdminClassificationTaxonomyActions(page, scenarioContext, featureContext);
        }
    }
}
using Microsoft.Playwright;
using NUnit.Framework;
using PIMQAAutomation.Common.Pages;
using PIMQAAutomation.Modules.Bom.Pages;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Utilities;
using TechTalk.SpecFlow;

namespace PIMQAAutomation.Modules.Bom.Steps
{
    [Binding]
    public partial class BOM
    {
        private readonly IPage page;
        private readonly BomPage bomPage;
        private readonly CommonLocators commonLocators;
        private readonly CosmosDbHelper cosmosDb;
        private readonly FeatureContext featureContext;
        private readonly ScenarioContext scenarioContext;
        private string testCaseName = string.Empty;
        private List<double> quantityList = new();

        public BOM(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            page = featureContext["Page"] as IPage;
            this.featureContext = featureContext;
            this.scenarioContext = scenarioContext;
            bomPage = new BomPage(page);
            commonLocators = new CommonLocators(page);
            cosmosDb = new CosmosDbHelper();
        }

        private bool IsIdhValidationEnabled()
        {
            return string.IsNullOrEmpty(TestContext.Parameters[RunSettingsConstants.EnableIDHVerification]) || Convert.ToBoolean(TestContext.Parameters[RunSettingsConstants.EnableIDHVerification]);
        }
    }
}
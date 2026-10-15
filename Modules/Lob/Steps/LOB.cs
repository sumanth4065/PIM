using Microsoft.Playwright;
using NUnit.Framework;
using PIMQAAutomation.Common.Pages;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Modules.Lob.Pages;
using PIMQAAutomation.Utilities;
using TechTalk.SpecFlow;

namespace PIMQAAutomation.Modules.Lob.Steps
{
    [Binding]
    public partial class LOB
    {
        private readonly IPage page;
        private readonly CommonLocators commonLocators;
        private readonly LobPage lobPage;
        private readonly CosmosDbHelper cosmosDb;
        private readonly FeatureContext featureContext;
        private readonly ScenarioContext scenarioContext;
        private string testCaseName = string.Empty;

        public LOB(FeatureContext featureContext, ScenarioContext scenarioContext, CosmosDbHelper cosmosDbHelper)
        {
            this.featureContext = featureContext;
            this.scenarioContext = scenarioContext;
            this.cosmosDb = cosmosDbHelper;
            page = featureContext["Page"] as IPage;
            commonLocators = new CommonLocators(page);
            lobPage = new LobPage(page);
        }
        private bool IsIdhValidationEnabled()
        {
            return string.IsNullOrEmpty(TestContext.Parameters[RunSettingsConstants.EnableIDHVerification]) || Convert.ToBoolean(TestContext.Parameters[RunSettingsConstants.EnableIDHVerification]);
        }
    }
}
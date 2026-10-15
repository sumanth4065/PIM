using Microsoft.Playwright;
using PIMQAAutomation.Common.Pages;
using NUnit.Framework;
using PIMQAAutomation.Modules.Alternates.Pages;
using PIMQAAutomation.Modules.Alternates.Actions;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Modules.Items.Constants;
using PIMQAAutomation.Utilities;
using System.Text.Json;
using TechTalk.SpecFlow;
using PIMQAAutomation.Modules.Alternates.Constants;

namespace PIMQAAutomation.Modules.Alternates.Steps
{
    [Binding]
    public partial class Alternate
    {
        private readonly ScenarioContext scenarioContext;
        private readonly FeatureContext featureContext;
        private readonly IPage page;
        private readonly CommonLocators commonLocators;
        private readonly AlternatePage alternatePage;
        private readonly CosmosDbHelper cosmosDb;

        // Action class instances - following POM pattern
        private readonly AlternateCommonActions alternateCommonActions;
        private readonly AlternatePIMActions alternatePIMActions;
        private readonly AlternateIDHActions alternateIDHActions;
        private readonly AlternateClusterModeActions alternateClusterModeActions;
        private readonly AlternateSupersessionActions alternateSupersessionActions;
        private readonly AlternateImportActions alternateImportActions;

        public Alternate(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            page = featureContext["Page"] as IPage;
            this.scenarioContext = scenarioContext;
            this.featureContext = featureContext;

            // Initialize Page Objects
            alternatePage = new AlternatePage(page);
            commonLocators = new CommonLocators(page);
            cosmosDb = new CosmosDbHelper();

            // Initialize Common Actions first (shared helpers)
            alternateCommonActions = new AlternateCommonActions(page, scenarioContext, featureContext);

            // Initialize Action classes - POM pattern
            alternatePIMActions = new AlternatePIMActions(page, scenarioContext, featureContext, alternateCommonActions);
            alternateIDHActions = new AlternateIDHActions(scenarioContext, featureContext, alternateCommonActions);
            alternateClusterModeActions = new AlternateClusterModeActions(page, scenarioContext, featureContext, alternateCommonActions);
            alternateSupersessionActions = new AlternateSupersessionActions(page);
            alternateImportActions = new AlternateImportActions(page, scenarioContext, featureContext, alternateCommonActions);
        }

        

        [Then(@"User saves the item details as ""(.*)""")]
        public async Task ThenUserSavesTheItemDetails(string item)
        {
            await alternateCommonActions.SaveItemDetailsAsync(item);
        }

        [Then(@"User loads the cluster item ""(.*)""")]
        public async Task UserLoadsTheClusterItem(string item)
        {
            await alternateCommonActions.LoadClusterItemAsync(item);
        }

        [Given(@"trigger wait is not required")]
        public void GivenTriggerWaitIsNotRequired()
        {
            alternateCommonActions.SetTriggerWaitNotRequired();
        }

        [Given(@"trigger wait is required")]
        public void GivenTriggerWaitIsRequired()
        {
            alternateCommonActions.SetTriggerWaitRequired();
        }

    }
}
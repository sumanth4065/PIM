using NUnit.Framework;
using PIMQAAutomation.Modules.Alternates.Constants;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Modules.Items.Constants;
using PIMQAAutomation.Utilities;
using PPT.ItemMaster.Data.Cosmos.Features.AlternatePartPools;
using PPT.ItemMaster.Data.Cosmos.Features.Alternates;
using PPT.ItemMaster.Data.Cosmos.Features.ItemPools;
using PPT.ItemMaster.Domain.Features.AlternatePartPools;
using TechTalk.SpecFlow;

namespace PIMQAAutomation.Modules.Alternates.Steps
{
    public partial class Alternate
    {
        [Then(@"Verify the below field values for ""(.*)"" in Cosmos DB using ""(.*)"" database and ""(.*)"" container")]
        public async Task ThenVerifyClusterDetails(string item, string dataBase, string container, Table table)
        {
            await alternatePIMActions.VerifyClusterDetailsAsync(item, dataBase, container, table);
        }

        [Then(@"Verify document is created for ""(.*)"" in Cosmos DB using ""(.*)"" database and ""(.*)"" container")]
        public async Task ThenVerifyDocumentIsCreatedForItemInCosmosDbUsingDatabaseAndContainerInDocument(string item, string dataBase, string container)
        {
            await alternatePIMActions.VerifyDocumentCreatedAsync(item, dataBase, container);
        }

        [Then(@"Verify the following details are updated in the ""(.*)"" container of the ""(.*)"" database for ""(.*)""")]
        public async Task ThenVerifyTheFollowingDetailsAreUpdatedInTheContainerOfTheDatabaseForItem(string containerName, string databaseName, string item, Table table)
        {
            await alternatePIMActions.VerifyAlternatePartPoolDetailsAsync(containerName, databaseName, item, table);
        }

        [Then(@"Verify the cluster replacement field values for ""(.*)"" in Cosmos DB using ""(.*)"" database and ""(.*)"" container")]
        public async Task ThenVerifyClusterReplacementDetails(string item, string dataBase, string container, Table table)
        {
            await alternatePIMActions.VerifyClusterReplacementDetailsAsync(item, dataBase, container, table);
        }

        [Then(@"Verify AlternatePartPool document is not created for ""(.*)"" in Pim")]
        public async Task ThenVerifyAlternatePartPoolDocumnentIsNotCreatedForItemInPim(string item)
        {
            await alternatePIMActions.VerifyAlternatePartPoolDocumentNotCreatedAsync(item);
        }
    }
}
using NUnit.Framework;
using PIMQAAutomation.Helpers.DocumentFetcher;
using PIMQAAutomation.Modules.Items.Constants;
using PIMQAAutomation.Utilities;
using PPT.Integration.CommonDataModel.Models.Alternates;
using PPT.Integration.CommonDataModel.Models.ItemCatalogs;
using TechTalk.SpecFlow;

namespace PIMQAAutomation.Modules.Alternates.Steps
{
    public partial class Alternate
    {
        [Then(@"Verify below documents are created for ""(.*)"" in ""(.*)"" database and ""(.*)"" container")]
        public async Task ThenVerifyBelowDocumentAreCreatedForItemInCosmosDbUsingDatabaseAndContainer(string item, string dataBase, string container, Table table)
        {
            await alternateIDHActions.VerifyDocumentsCreatedAsync(item, dataBase, container, table);
        }

        [Then(@"Verify the ""(.*)"" document is updated with the below details for ""(.*)"" in ""(.*)""")]
        public async Task ThenVerifyTheDocumentIsUpdatedWithTheBelowDetailsForItemInContainer(string container, string item, string database, Table table)
        {
            await alternateIDHActions.VerifyAlternateDocumentDetailsAsync(container, item, database, table);
        }

        [Then(@"Verify AliasSku is added to ""(.*)"" in IDH Item document with the following details")]
        public async Task ThenVerifyAliasSkuIsAddedToIdhItemDocumentWithTheFollowingDetails(string item, Table table)
        {
            await alternateIDHActions.VerifyAliasSkuInItemDocumentAsync(item, table);
        }
    }
}
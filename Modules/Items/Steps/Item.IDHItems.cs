using System.Reflection;
using NUnit.Framework;
using PIMQAAutomation.Helpers;
using PIMQAAutomation.Helpers.DocumentFetcher;
using PIMQAAutomation.Helpers.IDH;
using PIMQAAutomation.Modules.Items.Actions;
using PIMQAAutomation.Modules.Items.Constants;
using PIMQAAutomation.Utilities;
using PPT.Integration.CommonDataModel.Models.ItemCatalogs;
using TechTalk.SpecFlow;
namespace PIMQAAutomation.Modules.Items.Steps
{
    

    public partial class Item
    {
        // No field declarations here - they're in Item.cs (main partial class file)
        [Then(@"Verify the following characteristic Values are displayed in ""(.*)"" database and ""(.*)"" container under ""(.*)"" document")]
        public async Task ThenVerifyTheFollowingCharacteristicValuesAreDisplayedInDatabaseAndContainerUnderDocument(string database, string container, string documentType, Table characteristicTypes)
        {
            if (!itemCommonActions.IsIdhValidationEnabled())
            {
                return;
            }

            await itemIDHActions.VerifyCharacteristicValuesInDocumentAsync(database, container, documentType, characteristicTypes);
        }

        [Then(@"Verify below documents are created in ""(.*)"" database and ""(.*)"" container")]
        public async Task ThenVerifyBelowDocumentAreCreatedInCosmosDBUsingDatabaseAndContainer(string dataBase, string container, Table table)
        {
            if (!itemCommonActions.IsIdhValidationEnabled())
            {
                return;
            }

            await itemIDHActions.VerifyDocumentsAreCreatedAsync(dataBase, container, table);
        }

        [Then(@"Verify below field values are created in ""(.*)"" database and ""(.*)"" container for ""(.*)"" type")]
        public async Task ThenVerifyBelowFieldValuesAreCreatedInDatabaseAndContainerForType(string dataBase, string container, string type, Table table)
        {
            if (!itemCommonActions.IsIdhValidationEnabled())
            {
                return;
            }

            await itemIDHActions.VerifyFieldValuesInDocumentAsync(dataBase, container, type, table);
        }

        [Then(@"the user verifies the following characteristics under ""(.*)"" database and ""(.*)"" container in ""(.*)"" document")]
        public async Task ThenVerifyCharacteristicsInKind(string database, string container, string documentType, Table table)
        {
            if (!itemCommonActions.IsIdhValidationEnabled())
            {
                return;
            }

            await itemIDHActions.VerifyCharacteristicsInKindAsync(database, container, documentType, table);
        }

        [Then("The characteristic values are validated in the ItemCatalog document within IDH")]
        public async Task ThenTheCharacteristicValuesAreValidatedInTheItemCatalogDocumentWithinIDH(Table characteristicKind)
        {
            if (!itemCommonActions.IsIdhValidationEnabled())
            {
                return;
            }

            await itemIDHActions.ValidateCharacteristicValuesInItemCatalogAsync(characteristicKind);
        }

        [Then("Verify characteristic value in Item document in IDH")]
        public async Task ThenVerifyCharacteristicValueInIdhItemDocument(Table characteristicTypes)
        {
            if (!itemCommonActions.IsIdhValidationEnabled())
            {
                return;
            }

            await itemIDHActions.VerifyCharacteristicValueInItemDocumentAsync(characteristicTypes);
        }

        [Then("Verify characteristic value in Item Catalog document Service node in IDH")]
        public async Task ThenVerifyCharacteristicValueInTheItemCatalogServiceDocument(Table characteristicTypes)
        {
            if (!itemCommonActions.IsIdhValidationEnabled())
            {
                Console.WriteLine("IDH validation is disabled. Skipping validation logic.");
                return;
            }

            await itemIDHActions.VerifyCharacteristicValueInItemCatalogServiceNodeAsync(characteristicTypes);
        }

        [Then("Verify characteristic value in Item Catalog document Item node in IDH")]
        public async Task ThenVerifyCharacteristicValueInTheItemCatalogItemDocument(Table characteristicTypes)
        {
            if (!itemCommonActions.IsIdhValidationEnabled())
            {
                return;
            }

            await itemIDHActions.VerifyCharacteristicValueInItemCatalogItemNodeAsync(characteristicTypes);
        }

        [Then("Validate Taxonomy details on ItemCatalog Document in Integration")]
        public async Task ValidateTaxonomyDetailsOnItemCatalogDocumentInIntegration(Table taxonomyDetails)
        {
            if (!itemCommonActions.IsIdhValidationEnabled())
            {
                return;
            }

            await itemIDHActions.ValidateTaxonomyDetailsOnItemCatalogAsync(taxonomyDetails);
        }

        [Then("Validate Taxonomy details on Item Document in Integration")]
        public async Task ValidateTaxonomyDetailsOnItemDocumentInIntegration(Table taxonomyDetails)
        {
            if (!itemCommonActions.IsIdhValidationEnabled())
            {
                return;
            }

            await itemIDHActions.ValidateTaxonomyDetailsOnItemAsync(taxonomyDetails);
        }

        [Then("Verify the following general flags in the Integration in Item document")]
        public async Task ThenVerifyTheFollowingGeneralFlagsInTheIntegrationInItemDocument(Table generalFields)
        {
            if (!itemCommonActions.IsIdhValidationEnabled())
            {
                Console.WriteLine("IDH Validation is disabled, skipping execution.");
                return;
            }

            await itemIDHActions.VerifyGeneralFlagsInItemDocumentAsync(generalFields);
        }

        [Then("Verify the following general flags in the Integration in ItemCatalog document")]
        public async Task ThenVerifyTheFollowingGeneralFlagsInTheIntegrationInItemCatalogDocument(Table generalFields)
        {
            if (!itemCommonActions.IsIdhValidationEnabled())
            {
                Console.WriteLine("IDH Validation is disabled, skipping execution.");
                return;
            }

            await itemIDHActions.VerifyGeneralFlagsInItemCatalogDocumentAsync(generalFields);
        }

        [Then("User verify the following characteristics in the Integration in ItemCatalog document")]
        public async Task ThenUserVerifyTheFollowingCharacteristicsInTheIntegrationInItemCatalogDocument(Table characteristics)
        {
            if (!itemCommonActions.IsIdhValidationEnabled())
            {
                return;
            }

            await itemIDHActions.VerifyCharacteristicsInItemCatalogServicesAsync(characteristics);
        }
    }
}

namespace PIMQAAutomation.Modules.Items.Steps
{
    using NUnit.Framework;
    using OfficeOpenXml;
    using PIMQAAutomation.Helpers;
    using PIMQAAutomation.Modules.Common.Constants;
    using PIMQAAutomation.Modules.Common.Helpers;
    using PIMQAAutomation.Modules.Items.Constants;
    using PIMQAAutomation.Utilities;
    using PPT.ItemMaster.Data.Cosmos.Features.Items;
    using PPT.ItemMaster.Domain.Features.ItemPools;
    using TechTalk.SpecFlow;

    public partial class Item
    {
        [Then(@"Verify the below field values in Cosmos DB using ""(.*)"" database and ""(.*)"" container in ""(.*)"" document")]
        public async Task ThenVerifyTheBelowFieldValuesInCosmosDBUsingDatabaseAndContainerInDocument(string database, string container, string documentType, Table fieldsToCheck)
        {
            await itemPIMDefaultActions.VerifyFieldValuesInCosmosDBAsync(database, container, documentType, fieldsToCheck);
        }

        [Then(@"Verify the created and modified details in Cosmos DB using ""(.*)"" database and ""(.*)"" container in ""(.*)"" document")]
        public async Task ThenVerifyTheCreatedAndModifiedDetailsInCosmosDBUsingDatabaseAndContainerInDocument(string database, string container, string documentType, Table fieldsToCheck)
        {
            await itemPIMDefaultActions.VerifyCreatedAndModifiedDetailsAsync(database, container, documentType, fieldsToCheck);
        }

        [Then(@"User verifies the Item Master ""(.*)"" and Source Type as ""(.*)"" with Cosmos DB using the ""(.*)"" database and ""(.*)"" container in ""(.*)"" document")]
        public async Task ThenUserVerifiesTheItemMasterAndSourceTypeAsWithCosmosDBUsingTheDatabaseAndContainer(string modifiedDate, string sourceType, string database, string container, string documentType)
        {
            await itemPIMDefaultActions.VerifyDocumentModifiedSourceAsync(modifiedDate, sourceType, database, container, documentType);
        }

        [Then(@"Verify ""(.*)"" Taxonomy details in Cosmos DB using ""(.*)"" database and ""(.*)"" container in ""(.*)"" document")]
        public async Task ThenVerifyTaxonomyDetailsInCosmosDBUsingDatabaseAndContainerInDocument(string taxonomy, string database, string container, string documentType, Table fieldsToCheck)
        {
            await itemPIMDefaultActions.VerifyProductTaxonomyDetailsAsync(taxonomy, database, container, documentType, fieldsToCheck);
        }

        [Then(@"Verify the below ""(.*)"" Taxonomy field values in Cosmos DB using ""(.*)"" database and ""(.*)"" container in ""(.*)"" document")]
        public async Task ThenVerifyTheBelowTaxonomyFieldValuesInCosmosDBUsingDatabaseAndContainerInDocument(string taxonomy, string database, string container, string documentType, Table fieldsToCheck)
        {
            await itemPIMDefaultActions.VerifyServiceTaxonomyDetailsAsync(taxonomy, database, container, documentType, fieldsToCheck);
        }

        [Then(@"Verify below characteristic values displayed in ""(.*)"" database and ""(.*)"" container in ""(.*)"" document")]
        public async Task ThenVerifyBelowCharacteristicValuesDisplayedInDatabaseAndContainer(string database, string container, string documentType, Table table)
        {
            await itemPIMDefaultActions.VerifyCharacteristicValuesAsync(database, container, documentType, table);
        }

        [Then(@"Verify the ""(.*)"" tab below ""(.*)"" excel field values in Cosmos DB using ""(.*)"" database and ""(.*)"" container in ""(.*)"" document")]
        public async Task ThenVerifyTheTabBelowExcelFieldValuesInCosmosDBUsingDatabaseAndContainerInDocument(string tabName, string fileName, string database, string container, string documentType, Table fields)
        {
            await itemPIMDefaultActions.VerifyExcelCharacteristicValuesAsync(tabName, fileName, database, container, documentType, fields);
        }

        [Then(@"Verify Characteristic type ""(.*)"" value should be updated with in the ""(.*)"" database and ""(.*)"" container in ""(.*)"" document")]
        public async Task ThenVerifyCharacteristicTypeValueShouldBeUpdatedWithInTheDatabaseAndContainerInDocument(string locatorType, string database, string container, string documentType)
        {
            await itemPIMDefaultActions.VerifyCharacteristicTypeValueAsync(locatorType, database, container, documentType);
        }

        [Then(@"Verify the below ""(.*)"" Taxonomy details in Cosmos DB using ""(.*)"" database and ""(.*)"" container in ""(.*)"" document")]
        public async Task ThenVerifyTheBelowTaxonomyDetailsInCosmosDBUsingDatabaseAndContainerInDocument(string taxonomyName, string database, string container, string documentType, Table fields)
        {
            await itemPIMDefaultActions.VerifyTaxonomyDetailsInCosmosDBAsync(taxonomyName, database, container, documentType, fields);
        }

        [Then(@"Verify the characteristic value in ""(.*)"" document in Pim DB")]
        public async Task ThenVerifyTheCharacteristicValueInPim(string documentType, Table characteristicValues)
        {
            await itemPIMDefaultActions.VerifyCharacteristicValueInDocumentAsync(documentType, characteristicValues);
        }

        [Then(@"Verify the following general flags in the ""(.*)"" document from ""(.*)"" database and ""(.*)"" container")]
        public async Task ThenVerifyGeneralFlagsInDocument(string documentType, string database, string container, Table generalDetails)
        {
            await itemPIMDefaultActions.VerifyGeneralFlagsAsync(documentType, database, container, generalDetails);
        }
    }
}
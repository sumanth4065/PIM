namespace PIMQAAutomation.Modules.Items.Steps
{
    using PIMQAAutomation.Modules.Common.Constants;
    using PIMQAAutomation.Modules.Items.Actions;
    using TechTalk.SpecFlow;

    public partial class Item
    {
        // No field declarations here - they're in Item.cs (main partial class file)
        [Then(@"Verify the ""(.*)"" tab below ""(.*)"" excel characteristic field ""(.*)"" in Cosmos DB using ""(.*)"" database and ""(.*)"" container in ""(.*)"" document")]
        public async Task ThenVerifyTheTabBelowExcelCharacteristicFieldInCosmosDBUsingDatabaseAndContainerInDocument(string tabName, string fileName, string fieldName, string database, string container, string documentType)
        {
            await itemPIMPoolActions.VerifyExcelCharacteristicFieldAsync(tabName, fileName, fieldName, database, container, documentType);
        }

        [Then(@"Verify the below fields in ""(.*)"" database and ""(.*)"" container in ""(.*)"" document")]
        public async Task ThenVerifyTheBelowFieldsInDatabaseAndContainerInDocument(string database, string container, string documentType, Table fieldsToCheck)
        {
            await itemPIMPoolActions.VerifyFieldsInDocumentAsync(database, container, documentType, fieldsToCheck);
        }

        [Then(@"Verify ""(.*)"" taxonomy fields in ""(.*)"" database and ""(.*)"" container in ""(.*)"" document")]
        public async Task ThenVerifyTaxonomyFieldsInDatabaseAndContainerInDocument(string taxonomy, string database, string container, string documentType, Table fieldsToCheck)
        {
            await itemPIMPoolActions.VerifyTaxonomyFieldsAsync(taxonomy, database, container, documentType, fieldsToCheck);
        }

        [Then(@"Verify Event History should be updated with in the ""(.*)"" container of the ""(.*)"" database in ""(.*)"" document")]
        public async Task ThenVerifyEventHistoryShouldBeUpdatedWithInTheContainerOfTheDatabaseInItemPoolsDocument(string container, string database, string documentType, Table eventHistories)
        {
            await itemPIMPoolActions.VerifyEventHistoryAsync(container, database, documentType, eventHistories);
        }

        [Then(@"Verify Language Translation value should be updated with in the ""(.*)"" database and ""(.*)"" container in ""(.*)"" document")]
        public async Task ThenVerifyLanguageTranslationValueShouldBeUpdatedWithInTheDatabase(string database, string container, string documentType, Table valueToCheck)
        {
            await itemPIMPoolActions.VerifyLanguageTranslationAsync(database, container, documentType, valueToCheck);
        }

        [Then(@"User verifies that the general item details using ""(.*)"" database and ""(.*)"" container in ""(.*)"" document")]
        public async Task ThenUserVerifiesGeneralItemDetailsUsingDB(string database, string container, string documentType, Table fieldsToCheck)
        {
            await itemPIMPoolActions.VerifyGeneralItemDetailsAsync(database, container, documentType, fieldsToCheck);
        }

        [Then(@"User verifies that the general item details are displayed in ""(.*)"" database and ""(.*)"" container using ""(.*)"" document")]
        public async Task ThenUserVerifiesTheGeneralItemDetails(string database, string container, string documentType, Table fieldsToCheck)
        {
            await itemPIMPoolActions.VerifyGeneralItemDetailsDisplayedAsync(database, container, documentType, fieldsToCheck);
        }

        [Then(@"User verifies the service class details with processing type ""(.*)"" with ""(.*)"" Id , ""(.*)"" and ""(.*)"" values in ""(.*)"" database and ""(.*)"" container using ""(.*)"" document")]
        public async Task ThenUserVerifiesServiceClassDetailsWithUiCheck(string processingType, string sku, string oem, string description, string database, string container, string documentType)
        {
            await itemPIMPoolActions.VerifyServiceClassDetailsAsync(processingType, sku, oem, description, database, container, documentType);
        }

        [Then(@"User verifies that the ""(.*)"" service attribute for characteristic kind and name is updated and derived from ""(.*)"" with the following updated values in ""(.*)"" database and ""(.*)"" container using ""(.*)"" document")]
        public async Task ThenVerifyServiceAttributeWithDictionaries(string processingType, string derivedFrom, string database, string container, string documentType, Table fields)
        {
            await itemPIMPoolActions.VerifyServiceAttributeWithDictionariesAsync(processingType, derivedFrom, database, container, documentType, fields);
        }

        [Then(@"User verifies the relatedItem service class details with processing type ""(.*)"" with itemId as ""(.*)"" , ""(.*)"" and ""(.*)"" values in ""(.*)"" database and ""(.*)"" container using ""(.*)"" document")]
        public async Task ThenUserVerifiesTheRelatedItemServiceDetails(string processingType, string lob, string oem, string description, string database, string container, string documentType)
        {
            await itemPIMPoolActions.VerifyRelatedItemServiceDetailsAsync(processingType, lob, oem, description, database, container, documentType);
        }

        [Then(@"User verifies that the Characteristic value is getting updated under ""(.*)"" service attribute in ""(.*)"" database and ""(.*)"" container")]
        public async Task ThenUserVerifiesThatTheCharacteristicValueIsGettingUpdateUnderServiceAttributeInDatabaseAndContainer(string processingType, string database, string container)
        {
            await itemPIMPoolActions.VerifyCharacteristicValueUnderServiceAttributeAsync(processingType, database, container);
        }

        [Then(@"Verify the characteristic field ""(.*)"" in Cosmos DB using ""(.*)"" database and ""(.*)"" container in ""(.*)"" document")]
        public async Task ThenVerifyTheCharacteristicFieldInCosmosDBUsingDatabaseAndContainerInDocument(string value, string database, string container, string documentType)
        {
            await itemPIMPoolActions.VerifyCharacteristicFieldAsync(value, database, container, documentType);
        }

        [Then(@"Item LineOfBusiness array should have LOB2 as Primary and LOB1 as Secondary in the ""(.*)"" database ""(.*)"" container")]
        public async Task ThenVerifyItemLineOfBusinessPrimarySecondary(string database, string container)
        {
            await itemPIMPoolActions.VerifyItemLineOfBusinessPrimarySecondaryAsync(database, container);
        }

        [Then(@"Verify the characteristic value in ItemPools document in Pim database")]
        public async Task ThenVerifyTheCharacteristicValueInItemPool(Table valuesToCheck)
        {
            await itemPIMPoolActions.VerifyCharacteristicValueInItemPoolAsync(valuesToCheck);
        }

        [Then("verify the document should not be created in ItemPool")]
        public async Task ThenVerifyTheDocumentShouldNotBeCreatedInItemPool()
        {
            await itemPIMPoolActions.VerifyDocumentShouldNotBeCreatedAsync();
        }

        [Then(@"User verifies that the general item details are getting updated under ""(.*)"" service attribute in ItemPool")]
        public async Task VerifiesTheGeneralItemDetailsInItemPool(string processingType, Table valuesToCheck)
        {
            await itemPIMPoolActions.VerifyGeneralItemDetailsUnderServiceAttributeAsync(processingType, DatabaseConstants.DatabaseName, DatabaseConstants.ItemPoolsContainer, valuesToCheck);
        }

        [Then("Verify the characteristic and levelLineage value under skuAttributes in ItemPools document in Pim database")]
        public async Task ThenVerifyTheCharacteristicAndLevelLineageValueUnderSkuAttributesInItemPoolsDocumentInPimDatabase(Table valuesToCheck)
        {
            await itemPIMPoolActions.VerifyCharacteristicAndLevelLineageValueAsync(valuesToCheck);
        }

        [Then(@"Verify the characteristic value in ItemPools document in Pim database for ""(.*)""")]
        public async Task ThenVerifyTheCharacteristicValueInItemPoolsDocumentInPimDatabaseFor(string oemKey, Table eventHistories)
        {
            await itemPIMPoolActions.VerifyEventHistoryForOemAsync(oemKey, DatabaseConstants.DatabaseName, DatabaseConstants.ItemPoolsContainer, eventHistories);
        }

        [Then(@"Verify the following general flags in the ItemPool document from Pim database")]
        public async Task ThenVerifyGeneralFlagsInItemPoolDocument(Table generalDetails)
        {
            await itemPIMPoolActions.VerifyGeneralFlagsAsync(generalDetails);
        }
    }
}
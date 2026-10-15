namespace PIMQAAutomation.Modules.Items.Steps
{
    using PIMQAAutomation.Modules.Items.Actions;
    using TechTalk.SpecFlow;

    public partial class Item
    {
        // No field declarations here - they're in Item.cs (main partial class file)
        [Then(@"Verify the below service field values in Cosmos DB using ""(.*)"" database and ""(.*)"" container in ""(.*)"" document")]
        public async Task ThenVerifyTheBelowServiceFieldValuesInCosmosDBUsingDatabaseAndContainerInDocument(string database, string container, string documentType, Table fieldsNames)
        {
            await itemPIMServiceDefaultActions.VerifyServiceFieldValuesAsync(database, container, documentType, fieldsNames);
        }

        [Then(@"Verify the service created and modified details in Cosmos DB using ""(.*)"" database and ""(.*)"" container in ""(.*)"" document")]
        public async Task ThenVerifyTheServiceCreatedAndModifiedDetailsInCosmosDBUsingDatabaseAndContainerInDocument(string database, string container, string documentType, Table fieldsToCheck)
        {
            await itemPIMServiceDefaultActions.VerifyServiceCreatedAndModifiedDetailsAsync(database, container, documentType, fieldsToCheck);
        }

        [Then(@"User verifies the Item Master service ""(.*)"" and Source Type as ""(.*)"" with Cosmos DB using the ""(.*)"" database and ""(.*)"" container in ""(.*)"" document")]
        public async Task ThenUserVerifiesTheItemMasterServiceAndSourceTypeAsWithCosmosDBUsingTheDatabaseAndContainer(string modifiedDate, string sourceType, string database, string container, string documentType)
        {
            await itemPIMServiceDefaultActions.VerifyServiceDocumentModifiedSourceAsync(modifiedDate, sourceType, database, container, documentType);
        }

        [Then(@"Verify below checkboxes status in ""(.*)"" document")]
        public async Task ThenVerifyBelowCheckboxesStatusInServiceDefault(string documentType, Table checkboxStatus)
        {
            await itemPIMServiceDefaultActions.VerifyCheckboxStatusAsync(documentType, checkboxStatus);
        }
    }
}
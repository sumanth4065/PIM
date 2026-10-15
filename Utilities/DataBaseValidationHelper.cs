using TechTalk.SpecFlow;
using PIMQAAutomation.Modules.Items.Pages;
using PIMQAAutomation.Helpers;
using PIMQAAutomation.Modules.Common.Constants;         // for ICosmosDbService

namespace PIMQAAutomation.Utilities
{
    public static class DatabaseValidationhelper
    {
        public static async Task VerifyGeneralFlagsAsync<T>(
        FeatureContext featureContext,
        ItemMasterPage itemMasterPage,
        CosmosDbHelper cosmosDb,
        string documentType,
        string database,
        string container,
        Table table)
        {
            try
            {
                var itemId = ContextHelper.GetContextValue(featureContext, "itemIdValue");
                var query = CosmosDBQueries.GetDataUsingItemIdAndDocumentType(itemId, documentType);

                var dbRecord = await cosmosDb.GetDataFromCosmosDB<T>(database, container, query);
                ValidationHelper.FailIfNullOrEmpty(nameof(dbRecord), dbRecord, $"No {documentType} document found for itemId: {itemId}");

                // Using dynamic to access IsProduct / IsComponent from the model
                dynamic record = dbRecord;
                bool dbIsProduct = record.Item.General.IsProduct;
                bool dbIsComponent = record.Item.General.IsComponent;

                var dbFlags = new Dictionary<string, bool>
                {
                    { "product", dbIsProduct },
                    { "component", dbIsComponent }
                };

                var uiFlags = new Dictionary<string, bool>();
                foreach (var row in table.Rows)
                {
                    var field = row[0].Trim().ToLower();
                    uiFlags[field] = await itemMasterPage.GetRadioButton(field).IsCheckedAsync();
                }

                foreach (var field in dbFlags.Keys)
                {
                    if (uiFlags.ContainsKey(field))
                    {
                        ValidationHelper.AssertEqual(
                            field,
                            dbFlags[field],
                            uiFlags[field],
                            $"Mismatch in '{field}' between UI and DB for itemId: {itemId}."
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to validate Product/Component flags between UI and DB.", ex);
            }
        }
    }
}
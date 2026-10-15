namespace PIMQAAutomation.Modules.Alternates.Actions
{
    using NUnit.Framework;
    using PIMQAAutomation.Helpers.DocumentFetcher;
    using PIMQAAutomation.Modules.Alternates.Constants;
    using PIMQAAutomation.Modules.Common.Constants;
    using PIMQAAutomation.Modules.Items.Constants;
    using PIMQAAutomation.Utilities;
    using PPT.Integration.CommonDataModel.Models.Alternates;
    using PPT.Integration.CommonDataModel.Models.ItemCatalogs;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Contains business logic and action methods for IDH Alternate document validation
    /// </summary>
    public class AlternateIDHActions
    {
        private readonly ScenarioContext scenarioContext;
        private readonly FeatureContext featureContext;
        private readonly AlternateCommonActions commonActions;

        public AlternateIDHActions(ScenarioContext scenarioContext, FeatureContext featureContext, AlternateCommonActions commonActions)
        {
            this.scenarioContext = scenarioContext;
            this.featureContext = featureContext;
            this.commonActions = commonActions;
        }

        #region Document Creation Verification

        /// <summary>
        /// Verifies that documents are created in IDH for specified item
        /// </summary>
        public async Task VerifyDocumentsCreatedAsync(string item, string dataBase, string container, Table table)
        {
            try
            {
                if (!commonActions.IsIdhValidationEnabled())
                {
                    return;
                }

                var itemId = commonActions.GetFieldValue(item, ItemConstants.ItemId);
                var contextItemCatalog = new ValidationContext(itemId, ItemConstants.ItemCatalog);
                var contextItem = new ValidationContext(itemId, ItemConstants.Item);

                foreach (var fieldName in table.Rows)
                {
                    var docTypeKey = fieldName[0];
                    var recordType = ItemConstants.DocumentTypeMapping.ContainsKey(docTypeKey) ? ItemConstants.DocumentTypeMapping[docTypeKey] : docTypeKey;

                    if (recordType == "ItemCatalog")
                    {
                        var catalogDocument = await DocumentFetcherFactory.FetchDocumentAsync<ItemCatalog>(contextItemCatalog);
                        ValidationHelper.FailIfNullOrEmpty("IDHCatalogDocument", catalogDocument, $"Item Catalog document is not found in IDH for '{itemId}' ");
                    }
                    else
                    {
                        var itemDocument = await DocumentFetcherFactory.FetchDocumentAsync<PPT.Integration.CommonDataModel.Models.Items.Item>(contextItem);
                        ValidationHelper.FailIfNullOrEmpty("IDHItemDocument", itemDocument, $"Item document is not found in IDH for '{itemId}' ");
                    }
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Document verification failed in IDH. Error: {ex.Message}", ex);
            }
        }

        #endregion

        #region Alternate Document Validation

        /// <summary>
        /// Verifies the Alternate document is updated with correct details in IDH
        /// </summary>
        public async Task VerifyAlternateDocumentDetailsAsync(string container, string item, string database, Table table)
        {
            try
            {
                if (!commonActions.IsIdhValidationEnabled())
                {
                    return;
                }

                var itemId = commonActions.GetFieldValue(item, ItemConstants.ItemId);
                var context = new ValidationContext(itemId);
                var document = await DocumentFetcherFactory.FetchDocumentAsync<PPT.Integration.CommonDataModel.Models.Alternates.Alternate>(context);
                ValidationHelper.FailIfNullOrEmpty("Alternate Document", document, $" Document for Item Id '{itemId}' not found in IDH container - '{container}'.");

                var alternateItemType = string.Empty;
                var expectedItemId = string.Empty;

                foreach (var row in table.Rows)
                {
                    alternateItemType = row["AlternateItem"];
                    expectedItemId = commonActions.GetFieldValue(row["Item"], ItemConstants.ItemId);

                    IEnumerable<AlternateItem> alternateItems = alternateItemType.ToLower() switch
                    {
                        "interchangeableitems" => document.InterchangeableItems,
                        "interchangeablesupersessionitems" => document.InterchangeableSupersessionItems,
                        "unidirectionalitems" => document.UnidirectionalItems,
                        "unidirectionalsupersessionitems" => document.UnidirectionalSupersessionItems,
                        _ => throw new ArgumentException($"Invalid AlternateItem type '{alternateItemType}' provided in table.")
                    };

                    ValidationHelper.FailIfNullOrEmpty("Alternate Items", alternateItems, $"No items found for AlternateItem type '{alternateItemType}' under item '{itemId}'.");

                    var matchedItem = alternateItems.FirstOrDefault(ai => ai.Item.Sku == expectedItemId);
                    ValidationHelper.FailIfNullOrEmpty("Matched Alternate Item", matchedItem, $"Alternate item with SKU '{expectedItemId}' not found under '{alternateItemType}' for item '{itemId}'.");

                    Assert.Multiple(() =>
                    {
                        ValidationHelper.AssertEqual("IsActiveRelationship", row["IsActiveRelationship"], matchedItem.IsActiveRelationship.ToString());
                        ValidationHelper.AssertEqual("IsPrimary", row["IsPrimary"], matchedItem.IsPrimary.ToString());
                    });
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Error during IDH Alternate document validation: {ex.Message}", ex);
            }
        }

        #endregion

        #region Alias SKU Validation

        /// <summary>
        /// Verifies that AliasSku is added to IDH Item document with correct details
        /// </summary>
        public async Task VerifyAliasSkuInItemDocumentAsync(string item, Table table)
        {
            try
            {
                if (!commonActions.IsIdhValidationEnabled())
                {
                    return;
                }

                await commonActions.WaitIfTriggerToWaitAsync();
                var itemId = commonActions.GetFieldValue(item, ItemConstants.ItemId);
                var context = new ValidationContext(itemId, ItemConstants.Item);
                var itemDocument = await DocumentFetcherFactory.FetchDocumentAsync<PPT.Integration.CommonDataModel.Models.Items.Item>(context);
                ValidationHelper.FailIfNullOrEmpty("Item Document", itemDocument, $"Item document is not found in IDH for '{itemId}' ");

                var expectedRow = table.Rows[0];
                var expectedIsPrimary = bool.Parse(expectedRow["IsPrimary"]);
                var expectedIsValid = bool.Parse(expectedRow["IsValid"]);
                var expectedSku = commonActions.GetFieldValue(expectedRow["Sku"], "Item-Id");

                Assert.Multiple(() =>
                {
                    ValidationHelper.FailIfNullOrEmpty("Skus collection", itemDocument.Skus, "Skus collection should not be null.");

                    Assert.Greater(itemDocument.Skus.Count(), 1, "There should be at least one AliasSku starting from index 1.");

                    var aliasSkus = itemDocument.Skus.Skip(1).ToList();

                    var matchingSku = aliasSkus.FirstOrDefault(sku =>
                        sku.IsPrimary == expectedIsPrimary &&
                        sku.IsValid == expectedIsValid &&
                        sku.Sku == expectedSku);

                    ValidationHelper.FailIfNullOrEmpty("Expected AliasSku", matchingSku, $"Expected AliasSku with IsPrimary={expectedIsPrimary}, IsValid={expectedIsValid}, Sku={expectedSku} should be present from index 1 onwards.");

                    if (matchingSku != null)
                    {
                        ValidationHelper.AssertEqual("IsPrimary", expectedIsPrimary, matchingSku.IsPrimary);
                        ValidationHelper.AssertEqual("IsValid", expectedIsValid, matchingSku.IsValid);
                        ValidationHelper.AssertEqual("Sku", expectedSku, matchingSku.Sku);
                    }
                });
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Document verification failed in IDH. Error: {ex.Message}", ex);
            }
        }

        #endregion
    }
}

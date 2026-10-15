using System.Reflection;
using NUnit.Framework;
using PIMQAAutomation.Helpers;
using PIMQAAutomation.Helpers.DocumentFetcher;
using PIMQAAutomation.Helpers.IDH;
using PIMQAAutomation.Modules.Items.Constants;
using PIMQAAutomation.Utilities;
using PPT.Integration.CommonDataModel.Models.ItemCatalogs;
using TechTalk.SpecFlow;
namespace PIMQAAutomation.Modules.Items.Actions
{
    

    /// <summary>
    /// Contains business logic and action methods for Item IDH (Integration Data Hub) validation functionality
    /// </summary>
    public class ItemIDHActions
    {
        private readonly ScenarioContext scenarioContext;
        private readonly FeatureContext featureContext;

        public ItemIDHActions(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            this.scenarioContext = scenarioContext;
            this.featureContext = featureContext;
        }

        public async Task VerifyCharacteristicValuesInDocumentAsync(string database, string container, string documentType, Table characteristicTypes)
        {
            var itemId = string.Empty;
            try
            {
                itemId = ContextHelper.GetContextValue(featureContext, "itemIdValue");
                var context = new ValidationContext(itemId, ItemConstants.ItemCatalog);
                var itemCatalog = await DocumentFetcherFactory.FetchDocumentAsync<ItemCatalog>(context);

                ValidationHelper.FailIfNullOrEmpty("ItemCatalog", itemCatalog, $"for itemId: {itemId}");
                var service = itemCatalog.Services.FirstOrDefault();
                ValidationHelper.FailIfNullOrEmpty("Service", service, "in ItemCatalog");

                var uiCharacteristicValue = ContextHelper.GetContextValue(featureContext, "Value");

                foreach (var row in characteristicTypes.Rows)
                {
                    var characteristicType = row[ItemConstants.CharacteristicValue];

                    if (characteristicType.Equals(ItemConstants.CharacteristicTypes.PPTSupportability))
                    {
                        var supportabilityDBValue = service.Support?.Rule;
                        ValidationHelper.AssertEqual("Supportability", uiCharacteristicValue, supportabilityDBValue);
                    }
                    else
                    {
                        await ValidateServiceCharacteristicAsync(service, characteristicType, uiCharacteristicValue);
                    }
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Characteristic value not found for Item Id {itemId} in {documentType} document", ex);
            }
        }

        public async Task VerifyCharacteristicsInKindAsync(string database, string container, string documentType, Table table)
        {
            var itemId = string.Empty;
            try
            {
                itemId = ContextHelper.GetContextValue(featureContext, "itemIdValue");
                var context = new ValidationContext(itemId, ItemConstants.ItemCatalog);
                var itemCatalog = await DocumentFetcherFactory.FetchDocumentAsync<ItemCatalog>(context);

                ValidationHelper.FailIfNullOrEmpty("itemCatalog", itemCatalog, "in ItemCatalog");

                var serviceDescription = ContextHelper.GetContextValue(featureContext, "LOB");
                var service = itemCatalog.Services
                  .FirstOrDefault(s => s.Description.Equals(serviceDescription, StringComparison.OrdinalIgnoreCase));

                ValidationHelper.FailIfNullOrEmpty("Service", service, "in ItemCatalog");

                foreach (var row in table.Rows)
                {
                    var characteristicType = row["CharacteristicType"];
                    var kind = row.ContainsKey("KindName") ? row["KindName"] : null;

                    var kindNode = ServiceKindMapper.GetKindSection(kind, service);
                    var expectedValue = ContextHelper.GetContextValue(featureContext, "Value");

                    await ValidateKindCharacteristicAsync(kindNode, characteristicType, kind, expectedValue);
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Characteristic Value is not displaying in Integration for {itemId}.", ex);
            }
        }

        public async Task ValidateCharacteristicValuesInItemCatalogAsync(Table characteristicKind)
        {
            var itemId = string.Empty;
            try
            {
                itemId = ContextHelper.GetContextValue(featureContext, "itemIdValue");
                var context = new ValidationContext(itemId, ItemConstants.ItemCatalog);
                var itemCatalog = await DocumentFetcherFactory.FetchDocumentAsync<ItemCatalog>(context);

                var serviceDescription = ContextHelper.GetContextValue(featureContext, "LOB");
                var service = itemCatalog.Services.FirstOrDefault(s => s.Description.Equals(serviceDescription, StringComparison.OrdinalIgnoreCase));

                ValidationHelper.FailIfNullOrEmpty("Service", service, "in ItemCatalog");

                var expectedValue = featureContext.ContainsKey("Value")
                ? featureContext["Value"].ToString()
                   : featureContext["BooleanValue"].ToString();

                foreach (var row in characteristicKind.Rows)
                {
                    var kindName = row["KindName"];
                    var characteristicType = row["CharacteristicType"];

                    await ValidateListPriceCharacteristicAsync(service, kindName, characteristicType, expectedValue);
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Document verification failed in Integration database in the Item Catalog document for {itemId}.", ex);
            }
        }

        public async Task VerifyCharacteristicValueInItemDocumentAsync(Table characteristicTypes)
        {
            var itemId = string.Empty;
            try
            {
                itemId = ContextHelper.GetContextValue(featureContext, "itemIdValue");
                var context = new ValidationContext(itemId, ItemConstants.Item);
                var itemDoc = await DocumentFetcherFactory.FetchDocumentAsync<PPT.Integration.CommonDataModel.Models.Items.Item>(context);

                ValidationHelper.FailIfNullOrEmpty("Item", itemDoc, $"for itemId: {itemId}");

                foreach (var row in characteristicTypes.Rows)
                {
                    var characteristicType = row[0].Trim();
                    var expectedValue = row[1].Trim();
                    var actualValue = ServiceKindMapper.GetIdhItemValue(itemDoc, characteristicType)?.ToString();

                    ValidationHelper.AssertEqual($"Characteristic {characteristicType}", expectedValue, actualValue);
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Item Characteristic value not found for Item Id {itemId} in Item document", ex);
            }
        }

        public async Task VerifyCharacteristicValueInItemCatalogServiceNodeAsync(Table characteristicTypes)
        {
            var itemId = string.Empty;
            try
            {
                itemId = ContextHelper.GetContextValue(featureContext, "itemIdValue");
                var additionalInfo = IDHInfoBuilder.BuildItemInfo(itemId);
                var itemCatalogDoc = await IDHDataFetcherService.GetIdhDocumentAsync<ItemCatalog>(additionalInfo);

                var serviceDescription = ContextHelper.GetContextValue(featureContext, "LOB");
                ValidationHelper.FailIfNullOrEmpty("Item", itemCatalogDoc, $"for itemId: {itemId}");

                var service = itemCatalogDoc.Services
                  .FirstOrDefault(s => s.Description.Equals(serviceDescription, StringComparison.OrdinalIgnoreCase));

                foreach (var row in characteristicTypes.Rows)
                {
                    var characteristicType = row[0].Trim();
                    var expectedValue = row[1].Trim();
                    var actualValue = ServiceKindMapper.GetIdhItemCatalogServiceValue(service, characteristicType)?.ToString();

                    ValidationHelper.AssertEqual($"Characteristic {characteristicType}", expectedValue, actualValue);
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Item Characteristic value not found for Item Id {itemId} in Item Catalog document under service node", ex);
            }
        }

        public async Task VerifyCharacteristicValueInItemCatalogItemNodeAsync(Table characteristicTypes)
        {
            var itemId = string.Empty;
            try
            {
                itemId = ContextHelper.GetContextValue(featureContext, "itemIdValue");
                var context = new ValidationContext(itemId, ItemConstants.ItemCatalog);
                var itemCatalogDoc = await DocumentFetcherFactory.FetchDocumentAsync<ItemCatalog>(context);

                ValidationHelper.FailIfNullOrEmpty("Item", itemCatalogDoc, $"for itemId: {itemId}");

                foreach (var row in characteristicTypes.Rows)
                {
                    var characteristicType = row[0].Trim();
                    var expectedValue = row[1].Trim();
                    var actualValue = ServiceKindMapper.GetIdhItemCatalogValue(itemCatalogDoc.Item, characteristicType)?.ToString()?.ToLower();

                    ValidationHelper.AssertEqual($"Characteristic {characteristicType}", expectedValue, actualValue);
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Item Characteristic value not found for Item Id {itemId} in Item Catalog document under Item Node", ex);
            }
        }

        public async Task VerifyDocumentsAreCreatedAsync(string dataBase, string container, Table table)
        {
            var itemId = string.Empty;
            try
            {
                itemId = ContextHelper.GetContextValue(featureContext, "itemIdValue");

                var contextItemCatalog = new ValidationContext(itemId, ItemConstants.ItemCatalog);
                var contextItem = new ValidationContext(itemId, ItemConstants.Item);

                foreach (var row in table.Rows)
                {
                    var documentName = row[0];
                    var recordType = ItemConstants.DocumentTypeMapping.ContainsKey(documentName)
                ? ItemConstants.DocumentTypeMapping[documentName]
                          : documentName;

                    if (documentName.Equals(ItemConstants.Item, StringComparison.OrdinalIgnoreCase))
                    {
                        var itemDocument = await DocumentFetcherFactory.FetchDocumentAsync<PPT.Integration.CommonDataModel.Models.Items.Item>(contextItem);
                        ValidationHelper.FailIfNullOrEmpty($"{recordType} document", itemDocument, $"in Integration for '{itemId}'");
                    }
                    else if (documentName.Equals(ItemConstants.ItemCatalog, StringComparison.OrdinalIgnoreCase))
                    {
                        var itemCatalogDocument = await DocumentFetcherFactory.FetchDocumentAsync<ItemCatalog>(contextItemCatalog);
                        ValidationHelper.FailIfNullOrEmpty($"{recordType} document", itemCatalogDocument, $"in Integration for '{itemId}'");
                    }
                    else
                    {
                        Assert.Fail($"document type not found {documentName}");
                    }
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Document verification failed in Integration for Item Id {itemId}", ex);
            }
        }

        public async Task VerifyFieldValuesInDocumentAsync(string dataBase, string container, string type, Table table)
        {
            try
            {
                var itemId = ContextHelper.GetContextValue(featureContext, "itemIdValue");
                var context = new ValidationContext(itemId, ItemConstants.Item);
                var itemOrCatalogDocument = await DocumentFetcherFactory.FetchDocumentAsync<PPT.Integration.CommonDataModel.Models.Items.Item>(context);

                foreach (var row in table.Rows)
                {
                    var fieldName = row[0];

                    switch (fieldName)
                    {
                        case "isMaintenanceEligible":
                            ValidationHelper.AssertEqual("isMaintenanceEligible", true, itemOrCatalogDocument.IsMaintenanceEligible);
                            break;

                        case "isServiceProduct":
                            ValidationHelper.AssertEqual("isServiceProduct", true, itemOrCatalogDocument.IsServiceProduct);
                            break;

                        default:
                            ValidationHelper.Fail($"Unsupported field '{fieldName}' for validation.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Document verification failed in Integration.", ex);
            }
        }

        public async Task ValidateTaxonomyDetailsOnItemCatalogAsync(Table taxonomyDetails)
        {
            var itemId = string.Empty;
            try
            {
                itemId = ContextHelper.GetContextValue(featureContext, "itemIdValue");
                var context = new ValidationContext(itemId, ItemConstants.ItemCatalog);
                var itemCatalogDoc = await DocumentFetcherFactory.FetchDocumentAsync<ItemCatalog>(context);

                ValidationHelper.FailIfNullOrEmpty("ItemCatalog", itemCatalogDoc, $"for itemId: {itemId}");

                var taxonomy = itemCatalogDoc?.Item?.Taxonomy;

                foreach (var row in taxonomyDetails.Rows)
                {
                    string node = row["Node"].Trim();
                    string property = row["Property"].Trim();
                    string expectedValue = row["Value"].Trim();
                    var actualValue = ServiceKindMapper.GetTaxonomyValue(taxonomy, node, property);

                    ValidationHelper.AssertEqual($"Taxonomy {node}", expectedValue, actualValue);
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Taxonomy value not found for Item Id {itemId} in Item Catalog document", ex);
            }
        }

        public async Task ValidateTaxonomyDetailsOnItemAsync(Table taxonomyDetails)
        {
            var itemId = string.Empty;
            try
            {
                itemId = ContextHelper.GetContextValue(featureContext, "itemIdValue");
                var context = new ValidationContext(itemId, ItemConstants.Item);
                var itemDoc = await DocumentFetcherFactory.FetchDocumentAsync<PPT.Integration.CommonDataModel.Models.Items.Item>(context);

                ValidationHelper.FailIfNullOrEmpty("Item", itemDoc, $"for itemId: {itemId}");

                var taxonomy = itemDoc?.Taxonomy;

                foreach (var row in taxonomyDetails.Rows)
                {
                    string node = row["Node"].Trim();
                    string property = row["Property"].Trim();
                    string expectedValue = row["Value"].Trim();
                    var actualValue = ServiceKindMapper.GetTaxonomyValue(taxonomy, node, property);

                    ValidationHelper.AssertEqual($"Taxonomy {node}", expectedValue, actualValue);
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Taxonomy value not found for Item Id {itemId} in Item document", ex);
            }
        }

        public async Task VerifyGeneralFlagsInItemDocumentAsync(Table generalFields)
        {
            var itemId = string.Empty;
            try
            {
                itemId = ContextHelper.GetContextValue(featureContext, "itemIdValue");
                var context = new ValidationContext(itemId, ItemConstants.Item);
                var item = await DocumentFetcherFactory.FetchDocumentAsync<PPT.Integration.CommonDataModel.Models.Items.Item>(context);

                ValidationHelper.FailIfNullOrEmpty("Item", item, $"for itemId: {itemId}");

                var actualValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
   {
         { "isProduct", item.IsProduct.ToString().ToLower() },
         { "isComponent", item.IsComponent.ToString().ToLower() }
   };

                foreach (var row in generalFields.Rows)
                {
                    var fieldName = row[0].Trim();
                    var expectedValue = row[1].Trim().ToLower();

                    if (!actualValues.TryGetValue(fieldName, out var actualValue))
                    {
                        ValidationHelper.Fail($"Field '{fieldName}' not found in Item document for ItemId {itemId}.");
                        continue;
                    }

                    ValidationHelper.AssertEqual($"Characteristic {fieldName}", expectedValue, actualValue);
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed verifying isProduct/isComponent flags for ItemId {itemId} in Item document.", ex);
            }
        }

        public async Task VerifyGeneralFlagsInItemCatalogDocumentAsync(Table generalFields)
        {
            var itemId = string.Empty;
            try
            {
                itemId = ContextHelper.GetContextValue(featureContext, "itemIdValue");
                var context = new ValidationContext(itemId, ItemConstants.ItemCatalog);
                var itemCatalog = await DocumentFetcherFactory.FetchDocumentAsync<ItemCatalog>(context);

                ValidationHelper.FailIfNullOrEmpty("Item", itemCatalog, $"for itemId: {itemId}");

                var actualValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
              { "isProduct", itemCatalog.Item.IsProduct.ToString().ToLower() },
              { "isComponent", itemCatalog.Item.IsComponent.ToString().ToLower() }
    };

                foreach (var row in generalFields.Rows)
                {
                    var fieldName = row[0].Trim();
                    var expectedValue = row[1].Trim().ToLower();

                    if (!actualValues.TryGetValue(fieldName, out var actualValue))
                    {
                        ValidationHelper.Fail($"Field '{fieldName}' not found in ItemCatalog document for ItemId {itemId}.");
                        continue;
                    }

                    ValidationHelper.AssertEqual($"Characteristic {fieldName}", expectedValue, actualValue);
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed verifying isProduct/isComponent flags for ItemId {itemId} in ItemCatalog document.", ex);
            }
        }

        public async Task VerifyCharacteristicsInItemCatalogServicesAsync(Table characteristics)
        {
            var itemId = string.Empty;
            try
            {
                itemId = ContextHelper.GetContextValue(featureContext, "itemIdValue");
                var context = new ValidationContext(itemId, ItemConstants.ItemCatalog);
                var itemCatalog = await DocumentFetcherFactory.FetchDocumentAsync<ItemCatalog>(context);

                ValidationHelper.FailIfNullOrEmpty("Item", itemCatalog, $"for itemId: {itemId}");

                foreach (var service in itemCatalog.Services)
                {
                    if (service.Finance == null)
                    {
                        continue;
                    }

                    var actualValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
      {
          { "costCenter", service.Finance.CostCenter }
      };

                    foreach (var row in characteristics.Rows)
                    {
                        var fieldName = row[0].Trim();
                        var expectedValue = row[1].Trim();

                        if (!actualValues.TryGetValue(fieldName, out var actualValue))
                        {
                            ValidationHelper.Fail($"Field '{fieldName}' not found in service '{service.Description}' for ItemId {itemId}.");
                            continue;
                        }

                        ValidationHelper.AssertEqual($"Field '{fieldName}' for service '{service.Description}'", expectedValue, actualValue);
                    }
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed verifying characteristics in services for ItemId {itemId} in ItemCatalog document.", ex);
            }
        }

        private async Task ValidateServiceCharacteristicAsync(dynamic service, string characteristicType, string expectedValue)
        {
            var propInfo = service.GetType().GetProperty(characteristicType, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            ValidationHelper.FailIfNullOrEmpty("CharacteristicType", propInfo, $"{characteristicType} not found on Service object");

            var dbCharacteristicValue = propInfo.GetValue(service)?.ToString();
            ValidationHelper.AssertEqual($"Characteristic {characteristicType}", expectedValue, dbCharacteristicValue);
        }

        private async Task ValidateKindCharacteristicAsync(object kindNode, string characteristicType, string kind, string expectedValue)
        {
            var propertyName = ServiceKindMapper.GetServicePropertyName(characteristicType);
            var propInfo = kindNode.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

            ValidationHelper.FailIfNullOrEmpty("Characteristic", propInfo, $"Property {propertyName} not found under {kind} node");

            var dbValue = propInfo.GetValue(kindNode).ToString();
            ValidationHelper.FailIfNullOrEmpty("dbValue", dbValue, "in ItemCatalog");
            ValidationHelper.AssertEqual("Characteristic Value", expectedValue, dbValue);
        }

        private async Task ValidateListPriceCharacteristicAsync(dynamic service, string kindName, string characteristicType, string expectedValue)
        {
            // Use helper variable to avoid CS1977 error with lambda expressions on dynamic types
            var industryPricing = service.IndustryPricing;
            var listPrices = (IEnumerable<dynamic>)industryPricing.ListPrices;

            dynamic listPrice = null;
            foreach (var lp in listPrices)
            {
                if (lp.Name.Equals(kindName, StringComparison.OrdinalIgnoreCase))
                {
                    listPrice = lp;
                    break;
                }
            }

            ValidationHelper.FailIfNullOrEmpty($"{kindName} ListPrice", listPrice, "in IndustryPricing");

            var dbValue = ServiceKindMapper.GetListPriceValue(listPrice, characteristicType)?.ToString();
            if (dbValue == null)
            {
                ValidationHelper.FailIfNullOrEmpty("DB Value", dbValue, "DB value is coming as null from the Integration database in the Item Catalog document.");
            }

            ValidationHelper.AssertEqual($"{kindName} {characteristicType}", expectedValue, dbValue);
        }
    }
}

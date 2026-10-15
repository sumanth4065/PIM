using Microsoft.Playwright;
using NUnit.Framework;
using OfficeOpenXml;
using PIMQAAutomation.Helpers;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Modules.Common.Helpers;
using PIMQAAutomation.Modules.Items.Constants;
using PIMQAAutomation.Modules.Items.Pages;
using PIMQAAutomation.Utilities;
using PPT.ItemMaster.Data.Cosmos.Features.ItemPools;
using PPT.ItemMaster.Domain.Features.ItemPools;
using TechTalk.SpecFlow;
namespace PIMQAAutomation.Modules.Items.Actions
{
   

    /// <summary>
    /// Contains business logic and action methods for PIM Item Pool document validation in Cosmos DB
    /// </summary>
    public class ItemPIMPoolActions
    {
        private readonly IPage page;
        private readonly ScenarioContext scenarioContext;
        private readonly FeatureContext featureContext;
        private readonly ItemMasterPage itemMasterPage;
        private readonly CosmosDbHelper cosmosDb;

        public ItemPIMPoolActions(IPage page, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            this.page = page;
            this.scenarioContext = scenarioContext;
            this.featureContext = featureContext;
            itemMasterPage = new ItemMasterPage(page);
            cosmosDb = new CosmosDbHelper();
        }

        public async Task VerifyExcelCharacteristicFieldAsync(string tabName, string fileName, string fieldName, string database, string container, string documentType)
        {
            var itemId = string.Empty;
            try
            {
                itemId = featureContext["itemIdValue"].ToString();
                var defaultQuery = CosmosDBQueries.GetDataUsingItemIdAndDocumentType(itemId, documentType);
                var dbDefaultData = await cosmosDb.GetDataFromCosmosDB<ItemPoolRecord>(database.ToLower(), container, defaultQuery);

                var dbValueDict = dbDefaultData.Item.ItemLineOfBusinesses?
                       .SelectMany(lob => lob.ServiceAttributes.Kinds)
                      .SelectMany(kind => kind.Characteristics.Select(ch => new
                      {
                          Kind = kind.Name.Trim(),
                          Type = ch.Name.Trim(),
                          Value = ch.Value?.Trim()
                      }))
        .ToDictionary(x => (x.Kind.ToLower(), x.Type.ToLower()), x => x.Value);

                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                var excelData = Utility.ReadExcel(FileUtil.GetImportExcelFilePath(scenarioContext, fileName));

                foreach (var row in excelData)
                {
                    var kind = row[ItemConstants.ImportCharacteristicKind].Trim();
                    var type = row[ItemConstants.ImportCharacteristicType].Trim();

                    await itemMasterPage.GetCharacteristicKind(kind).ScrollIntoViewIfNeededAsync();
                    await itemMasterPage.GetCharacteristicKind(kind).ClickAsync();
                    await itemMasterPage.GetCharacteristicType(type).ScrollIntoViewIfNeededAsync();
                    await itemMasterPage.GetCharacteristicType(type).ClickAsync();

                    var uiValue = (await itemMasterPage.GetDefaultCharacteristicsFields(tabName.ToLower(), fieldName.ToLower()).TextContentAsync()).Trim();
                    var key = (kind.ToLower(), type.ToLower());

                    Assert.IsTrue(dbValueDict.ContainsKey(key), $"No DB entry found for Kind: '{kind}', Type: '{type}'");
                    var dbValue = dbValueDict[key];

                    ValidationHelper.AssertEqual(fieldName, uiValue, dbValue,
                                $"Mismatch for Kind: '{kind}', Type: '{type}' of characteristic {fieldName}. Expected: '{uiValue}' but found: '{dbValue}'");
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Characteristic value validation failed for {documentType} document with ItemId: {itemId}.", ex);
            }
        }

        public async Task VerifyFieldsInDocumentAsync(string database, string container, string documentType, Table fieldsToCheck)
        {
            var itemId = string.Empty;
            try
            {
                itemId = featureContext["itemIdValue"].ToString();
                var defaultQuery = CosmosDBQueries.GetDataUsingItemIdAndDocumentType(itemId, documentType);
                var dbDefaultData = await cosmosDb.GetDataFromCosmosDB<ItemPoolRecord>(database.ToLower(), container, defaultQuery);
                var dbItem = dbDefaultData.Item;

                var actualUiValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                var expectedDbValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                foreach (var row in fieldsToCheck.Rows)
                {
                    var uiField = row[0].Trim();
                    var dbField = row[1].Trim();
                    var locator = uiField.ToLower();

                    string uiValue = locator switch
                    {
                        _ when locator.Equals(ItemConstants.Status, StringComparison.OrdinalIgnoreCase) =>
                   (await itemMasterPage.GetRightCornerInputField(locator).InputValueAsync()).Trim()
                          .Equals("00", StringComparison.OrdinalIgnoreCase) ? "True" : "False",
                        _ when locator.Equals(ItemConstants.ProductClass, StringComparison.OrdinalIgnoreCase) ||
                               locator.Equals(ItemConstants.ProductCategory, StringComparison.OrdinalIgnoreCase) =>
                           (await itemMasterPage.GetRightCornerInputField(locator).InputValueAsync()).Trim(),
                        _ when locator.Equals(ItemConstants.ItemMasterInternalNotes, StringComparison.OrdinalIgnoreCase) =>
                         (await itemMasterPage.GetInputField(locator).InputValueAsync()).Trim(),
                        _ when locator.Equals(ItemConstants.Item, StringComparison.OrdinalIgnoreCase) =>
                               featureContext["itemIdValue"].ToString(),
                        _ => featureContext["OEM"].ToString()
                    };

                    actualUiValues[uiField] = uiValue;

                    if (!ItemPoolFieldMap.DBItemPoolFieldMapping.TryGetValue(dbField, out var dbFunc))
                    {
                        throw new ArgumentException($"Unsupported DB field: '{dbField}'");
                    }

                    expectedDbValues[uiField] = dbFunc(dbItem)?.Trim();
                }

                ValidationHelper.AssertDictionariesMatch(actualUiValues, expectedDbValues, documentType);
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Field validation failed for document '{documentType}' with itemId: {itemId}.", ex);
            }
        }

        public async Task VerifyTaxonomyFieldsAsync(string taxonomy, string database, string container, string documentType, Table fieldsToCheck)
        {
            var itemId = string.Empty;
            try
            {
                itemId = featureContext["itemIdValue"].ToString();
                var query = CosmosDBQueries.GetDataUsingItemIdAndDocumentType(itemId, documentType);
                var dbItem = (await cosmosDb.GetDataFromCosmosDB<ItemPoolRecord>(database, container, query)).Item;

                var actualUiValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                var expectedDbValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                foreach (var row in fieldsToCheck.Rows)
                {
                    var uiField = row[0].Trim();
                    var dbField = row[1].Trim();
                    var locator = uiField.ToLower();

                    actualUiValues[uiField] = (await itemMasterPage.GetTaxonomyField(taxonomy.ToLower(), locator).InputValueAsync()).Trim();

                    if (!ItemPoolFieldMap.DBItemPoolFieldMapping.TryGetValue(dbField, out var dbFunc))
                    {
                        throw new ArgumentException($"Unsupported DB field: '{dbField}'");
                    }

                    expectedDbValues[uiField] = dbFunc(dbItem)?.Trim();
                }

                ValidationHelper.AssertDictionariesMatch(actualUiValues, expectedDbValues, documentType);
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Taxonomy field validation failed for document '{documentType}' with itemId: {itemId}.", ex);
            }
        }

        public async Task VerifyEventHistoryAsync(string container, string database, string documentType, Table eventHistories)
        {
            string eventName = string.Empty;
            var itemId = string.Empty;
            try
            {
                itemId = featureContext["itemIdValue"].ToString();
                await page.WaitForTimeoutAsync(TimeConstants.ItemOrAlternatesPoolTriggerTimeMs);

                var itemPoolQuery = CosmosDBQueries.GetDataUsingItemIdAndDocumentType(itemId, documentType);
                var itemPoolData = await cosmosDb.GetDataFromCosmosDB<ItemPoolRecord>(database, container, itemPoolQuery);

                ValidationHelper.FailIfNullOrEmpty("Item Pool Document", itemPoolData,
                        $"{documentType} document after waiting {TimeConstants.ItemOrAlternatesPoolTriggerTimeMs / 60000} minutes for {itemId}.");

                var eventHistoryList = itemPoolData?.EventHistories ?? new List<EventHistory>();

                foreach (var eventHistory in eventHistories.Rows)
                {
                    eventName = ItemConstants.EventHistory[eventHistory[0]];
                    var matchingEvent = eventHistoryList.FirstOrDefault(e => e.Name == eventName);

                    Assert.IsNotNull(matchingEvent, $"Expected event '{eventName}' was not found in Event History.");
                    Assert.IsFalse(string.IsNullOrWhiteSpace(matchingEvent.MessageId), $"Message ID for event '{eventName}' is missing or empty.");
                }
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"Event History validation failed: Expected event '{eventName}' not found in {database}>>{container} for {documentType} document after waiting {TimeConstants.ItemOrAlternatesPoolTriggerTimeMs / 60000} minutes for {itemId}.", e);
            }
        }

        public async Task VerifyEventHistoryForOemAsync(string oemKey, string database, string container, Table eventHistories)
        {
            string eventName = string.Empty;
            string itemId = string.Empty;

            try
            {
                itemId = featureContext["itemIdValue"].ToString();

                if (!featureContext.ContainsKey(oemKey))
                {
                    throw new ArgumentException($"OEM key '{oemKey}' not found in feature context.");
                }

                var oem = featureContext[oemKey]?.ToString();

                await page.WaitForTimeoutAsync(TimeConstants.ItemOrAlternatesPoolTriggerTimeMs);

                var itemPoolQuery = CosmosDBQueries.GetDataUsingItemIdAndDocumentTypeMultipleOems(itemId, oem);
                var itemPoolData = await cosmosDb.GetDataFromCosmosDB<ItemPoolRecord>(database.ToLower(), container, itemPoolQuery);

                ValidationHelper.FailIfNullOrEmpty("Item Pool data", itemPoolData, "Item pool Data is found null");

                var eventHistoryList = itemPoolData?.EventHistories ?? new List<EventHistory>();
                ValidationHelper.FailIfNullOrEmpty("Event History List", eventHistoryList, "Event History Data is found null");

                foreach (var eventHistory in eventHistories.Rows)
                {
                    eventName = ItemConstants.EventHistory[eventHistory[0]];
                    var matchingEvent = eventHistoryList.FirstOrDefault(e => e.Name == eventName);

                    ValidationHelper.FailIfNullOrEmpty("Event Object", matchingEvent, $"Expected event '{eventName}' was not found in Event History.");
                    ValidationHelper.FailIfNullOrEmpty("Message ID", matchingEvent?.MessageId, $"Message ID for event '{eventName}' is missing or empty.");
                }
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"Event History validation failed: Expected event '{eventName}' not found in {database}>>{container} for {oemKey} after waiting {TimeConstants.ItemOrAlternatesPoolTriggerTimeMs / 60000} minutes for {itemId}.", e);
            }
        }

        public async Task VerifyLanguageTranslationAsync(string database, string container, string documentType, Table valueToCheck)
        {
            var itemId = string.Empty;
            try
            {
                itemId = featureContext["itemIdValue"].ToString();
                var query = CosmosDBQueries.GetDataUsingItemIdAndDocumentType(itemId, documentType);
                var itemPoolData = await cosmosDb.GetDataFromCosmosDB<ItemPoolRecord>(database.ToLower(), container, query);
                var item = itemPoolData.Item;

                foreach (var value in valueToCheck.Rows)
                {
                    var kindName = value[ItemConstants.CharacteristicKind];
                    var characteristicName = value[ItemConstants.CharacteristicType];

                    var kind = item.ItemLineOfBusinesses
                             .First(lob => lob.ServiceProcessingType == ItemConstants.ServiceType)
                         .ServiceAttributes.Kinds
                             .First(k => k.Name.Equals(kindName, StringComparison.OrdinalIgnoreCase));

                    var characteristic = kind.Characteristics
            .First(c => c.Name.Equals(characteristicName, StringComparison.OrdinalIgnoreCase));

                    var dbValue = characteristic.Value.Trim();
                    var uiValue = ContextHelper.GetContextValue(featureContext, "Value");

                    ValidationHelper.AssertEqual(characteristicName, uiValue, dbValue);
                }
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"Language Translation validation failed for {itemId}.", e);
            }
        }

        public async Task VerifyGeneralItemDetailsAsync(string database, string container, string documentType, Table fieldsToCheck)
        {
            var itemId = string.Empty;
            try
            {
                itemId = featureContext["itemIdValue"].ToString();
                var query = CosmosDBQueries.GetDataUsingItemIdAndDocumentType(itemId, documentType);
                var dbItem = (await cosmosDb.GetDataFromCosmosDB<ItemPoolRecord>(database.ToLower(), container, query)).Item;

                var actualUiValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                var expectedDbValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                foreach (var row in fieldsToCheck.Rows)
                {
                    var uiFieldName = row["UIFieldName"].Trim();
                    var dbFieldName = row["DBFieldName"].Trim();
                    var locatorFieldKey = uiFieldName.ToLower();

                    string uiValue;
                    if (uiFieldName.Equals(ItemConstants.ItemCreationValues.ItemStatus))
                    {
                        var value = (await itemMasterPage.GetRightCornerInputField(locatorFieldKey).InputValueAsync()).Trim();
                        uiValue = value.Equals(ItemConstants.IsActiveCheckbox, StringComparison.OrdinalIgnoreCase) ? "True" : "False";
                    }
                    else if (uiFieldName.Equals(ItemConstants.ItemCreationValues.IsProduct) || uiFieldName.Equals(ItemConstants.ItemCreationValues.IsComponent))
                    {
                        uiValue = (await itemMasterPage.GetRadioButton(locatorFieldKey).IsCheckedAsync()) ? "True" : "False";
                    }
                    else if (uiFieldName.Equals(ItemConstants.ProductTaxonomyFields.IsAlias) || uiFieldName.Equals(ItemConstants.ProductTaxonomyFields.IsTest))
                    {
                        uiValue = (await itemMasterPage.GetCheckbox(locatorFieldKey).IsCheckedAsync()) ? "True" : "False";
                    }
                    else
                    {
                        ValidationHelper.Fail($"UI field not found for '{uiFieldName}'");
                        continue;
                    }

                    actualUiValues[uiFieldName] = uiValue;

                    if (!ItemPoolFieldMap.DBItemPoolFieldMapping.TryGetValue(dbFieldName, out var dbFunc))
                    {
                        throw new ArgumentException($"Unsupported DB field: '{dbFieldName}'");
                    }

                    expectedDbValues[uiFieldName] = dbFunc(dbItem)?.Trim();
                }

                ValidationHelper.AssertDictionariesMatch(actualUiValues, expectedDbValues);
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"General item details validation failed for ItemId '{itemId}' in {database}>{container}.", e);
            }
        }

        public async Task VerifyGeneralItemDetailsDisplayedAsync(string database, string container, string documentType, Table fieldsToCheck)
        {
            var itemId = string.Empty;
            try
            {
                itemId = featureContext["itemIdValue"].ToString();
                var query = CosmosDBQueries.GetDataUsingItemIdAndDocumentType(itemId, documentType);
                var dbItem = (await cosmosDb.GetDataFromCosmosDB<ItemPoolRecord>(database.ToLower(), container, query)).Item;

                foreach (var field in fieldsToCheck.Rows)
                {
                    var fieldName = field["FieldName"].Trim();

                    if (!ItemPoolFieldMap.DBItemPoolFieldMapping.TryGetValue(fieldName, out var dbFunc))
                    {
                        ValidationHelper.Fail($"Field '{fieldName}' is not mapped in ItemPoolFieldMap.");
                    }

                    var dbValue = dbFunc(dbItem);
                    Assert.IsFalse(string.IsNullOrEmpty(dbValue), $"Field '{fieldName}' is null or empty in the DB.");
                }
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"General item details for ItemId '{itemId}' not displaying in {database}>{container}", e);
            }
        }

        public async Task VerifyGeneralItemDetailsUnderServiceAttributeAsync(string processingType, string database, string container, Table valuesToCheck)
        {
            var itemId = string.Empty;
            var lobName = string.Empty;
            try
            {
                itemId = ContextHelper.GetContextValue(featureContext, "itemIdValue");
                lobName = ContextHelper.GetContextValue(featureContext, "LOB");
                var query = CosmosDBQueries.GetItemPoolQuery(itemId);

                var dbItem = (await cosmosDb.GetDataFromCosmosDB<ItemPoolRecord>(database, container, query)).Item;

                var targetService = dbItem.ItemLineOfBusinesses?.FirstOrDefault(s =>
                string.Equals(s.ServiceProcessingType, processingType, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(s.Name, lobName, StringComparison.OrdinalIgnoreCase));

                ValidationHelper.FailIfNullOrEmpty("targetService", targetService, $"No LOB {lobName} found with processing type: {processingType}");

                var uiGeneralItemDetails = new Dictionary<string, string>();
                foreach (var fieldName in valuesToCheck.Rows)
                {
                    var field = fieldName[0];
                    var menuLocator = field.ToLower();

                    if (menuLocator.Equals(ItemConstants.Status, StringComparison.OrdinalIgnoreCase))
                    {
                        var value = (await itemMasterPage.GetRightCornerInputField(menuLocator).InputValueAsync()).Trim();
                        uiGeneralItemDetails[field] = value.Equals(ItemConstants.IsActiveCheckbox, StringComparison.OrdinalIgnoreCase) ? "True" : "False";
                    }
                    else if (menuLocator.Equals(ItemConstants.ItemMasterInternalNotes, StringComparison.OrdinalIgnoreCase))
                    {
                        uiGeneralItemDetails[field] = (await itemMasterPage.GetInputField(menuLocator).InputValueAsync()).Trim();
                    }
                    else if (menuLocator.Equals(ItemConstants.Description))
                    {
                        uiGeneralItemDetails[field] = (await itemMasterPage.GetDropdownInputField(menuLocator).InputValueAsync()).Trim();
                    }
                    else if (menuLocator.Equals(ItemConstants.ProductTaxonomyFields.IsAlias, StringComparison.OrdinalIgnoreCase) ||
                    menuLocator.Equals(ItemConstants.ProductTaxonomyFields.IsTest, StringComparison.OrdinalIgnoreCase))
                    {
                        uiGeneralItemDetails[field] = (await itemMasterPage.GetCheckbox(menuLocator).IsCheckedAsync()) ? "True" : "False";
                    }
                    else
                    {
                        ValidationHelper.Fail($"UI field not found for '{field}'");
                        continue;
                    }
                }

                var dbGeneralItemDetails = new Dictionary<string, string>
    {
       { ItemConstants.ItemCreationValues.Description, targetService.Description },
       { ItemConstants.ItemCreationValues.InternalNotes, targetService.InternalNotes },
       { ItemConstants.ItemCreationValues.ItemStatus, targetService.General.IsActive.ToString() },
       { ItemConstants.ItemCreationValues.IsAlias, targetService.General.IsAlias.ToString() },
       { ItemConstants.ItemCreationValues.IsTest, targetService.General.IsTest.ToString() }
    };

                ValidationHelper.AssertDictionariesMatch(uiGeneralItemDetails, dbGeneralItemDetails, $"UI Service field values vs Database for {itemId}");
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"General Item details not matching in db for {lobName} under {processingType} in {database}>{container} for {itemId}", e);
            }
        }

        public async Task VerifyServiceClassDetailsAsync(string processingType, string sku, string oem, string description, string database, string container, string documentType)
        {
            var itemId = string.Empty;
            try
            {
                itemId = featureContext["itemIdValue"].ToString();
                var query = CosmosDBQueries.GetDataUsingItemIdAndDocumentType(itemId, documentType);
                var dbItem = (await cosmosDb.GetDataFromCosmosDB<ItemPoolRecord>(database.ToLower(), container, query)).Item;

                var targetService = dbItem.ItemLineOfBusinesses.FirstOrDefault(s =>
                     s.ServiceProcessingType.Equals(processingType, StringComparison.OrdinalIgnoreCase));

                Assert.IsNotNull(targetService, $"Service with processingType = '{processingType}' not found.");

                var uiOem = featureContext["OEM"].ToString();
                var uiDescription = featureContext["Description"].ToString();

                Assert.Multiple(() =>
                {
                    ValidationHelper.AssertEqual(sku, itemId, targetService.Sku.ItemId);
                    ValidationHelper.AssertEqual(oem, uiOem, targetService.Sku.Oem.Name);
                    ValidationHelper.AssertEqual(description, uiDescription, targetService.Description);
                });
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"Service class details for ItemId '{itemId}' not displaying in {database}>{container}", e);
            }
        }

        public async Task VerifyRelatedItemServiceDetailsAsync(string processingType, string lob, string oem, string description, string database, string container, string documentType)
        {
            var itemId = string.Empty;
            try
            {
                itemId = featureContext["itemIdValue"].ToString();
                var query = CosmosDBQueries.GetDataUsingItemIdAndDocumentType(itemId, documentType);
                var dbItem = (await cosmosDb.GetDataFromCosmosDB<ItemPoolRecord>(database.ToLower(), container, query)).Item;

                var service = dbItem.ItemLineOfBusinesses?
                       .FirstOrDefault(s => s.ServiceProcessingType.Equals(processingType, StringComparison.OrdinalIgnoreCase));

                Assert.IsNotNull(service, $"No service found with processing type '{processingType}'");

                var relatedItem = service.RelatedItem;
                Assert.IsNotNull(relatedItem, "RelatedItem data is missing");

                var uiLob = featureContext["LOB"].ToString();
                var uiOem = featureContext["OEM"].ToString();

                Assert.Multiple(() =>
                            {
                                ValidationHelper.AssertEqual(lob, uiLob, relatedItem.ItemId);
                                ValidationHelper.AssertEqual(oem, uiOem, relatedItem.Oem?.Name);
                            });
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"Related Item service class details for ItemId '{itemId}' not displaying in {database}>{container}", e);
            }
        }

        public async Task VerifyServiceAttributeWithDictionariesAsync(string processingType, string derivedFrom, string database, string container, string documentType, Table fields)
        {
            var itemId = string.Empty;
            var characteristicKind = string.Empty;
            var characteristicName = string.Empty;
            try
            {
                itemId = featureContext["itemIdValue"].ToString();
                characteristicKind = featureContext["CharacteristicKind"].ToString();
                characteristicName = featureContext["CharacteristicType"].ToString();

                await itemMasterPage.GetCharacteristicKindOrType(characteristicName).ScrollIntoViewIfNeededAsync();

                var query = CosmosDBQueries.GetDataUsingItemIdAndDocumentType(itemId, documentType);
                var dbItem = (await cosmosDb.GetDataFromCosmosDB<ItemPoolRecord>(database.ToLower(), container, query)).Item;

                var service = dbItem.ItemLineOfBusinesses.FirstOrDefault(s =>
                         s.ServiceProcessingType.Equals(processingType, StringComparison.OrdinalIgnoreCase));

                Assert.IsNotNull(service, $"Service with processing type '{processingType}' not found.");

                var matchedKind = service.ServiceAttributes?.Kinds
                 .FirstOrDefault(k => k.Name.Equals(characteristicKind, StringComparison.OrdinalIgnoreCase));

                var attribute = matchedKind?.Characteristics
                 .FirstOrDefault(c => c.Name.Equals(characteristicName, StringComparison.OrdinalIgnoreCase));

                Assert.IsNotNull(attribute, $"Attribute with kind '{characteristicKind}' and name '{characteristicName}' not found.");

                var levelLineage = attribute.LevelLineage;

                ValidationHelper.AssertEqual("ValueDerivedFrom", derivedFrom, levelLineage.ValueDerivedFrom,
             $"Expected derived from '{derivedFrom}' for this itemid {itemId} under {processingType} processingType.");

                var levelValue = derivedFrom switch
                {
                    ItemConstants.LevelLineageValue.Service => levelLineage?.Service,
                    ItemConstants.LevelLineageValue.Taxonomy => levelLineage?.Taxonomy,
                    ItemConstants.LevelLineageValue.Item => levelLineage?.Item,
                    _ => throw new ArgumentException($"Unknown level value: {levelLineage}")
                };

                var dbValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
{ ItemConstants.CharacteristicValues.Value, levelValue.Value },
{ ItemConstants.CharacteristicValues.IsActive, levelValue.IsActive.ToString() },
{ ItemConstants.CharacteristicValues.ScopeObsolete, levelValue.ScopeObsolete.ToString() }
    };

                var uiValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                foreach (var fieldName in fields.Rows)
                {
                    var field = fieldName["Field"];
                    var fieldLocator = field.ToLower();

                    var value = fieldLocator switch
                    {
                        _ when fieldLocator.Equals(ItemConstants.IsActiveCheckbox, StringComparison.OrdinalIgnoreCase) ||
                           fieldLocator.Equals(ItemConstants.ScopeObsolete, StringComparison.OrdinalIgnoreCase) =>
                (await itemMasterPage.GetCharacteristicCheckbox(fieldLocator).IsCheckedAsync()).ToString(),
                        _ => (await itemMasterPage.GetCharacteristicField(fieldLocator).InputValueAsync()).Trim()
                    };

                    uiValues[field] = value;
                }

                ValidationHelper.AssertDictionariesMatch(uiValues, dbValues);
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"Service Attribute values for {characteristicKind}->{characteristicName} ItemId '{itemId}' not displaying in {database}>{container}", e);
            }
        }

        public async Task VerifyCharacteristicValueUnderServiceAttributeAsync(string processingType, string database, string container)
        {
            var itemId = string.Empty;
            var characteristicKind = string.Empty;
            var characteristicName = string.Empty;
            var lobName = string.Empty;
            try
            {
                itemId = ContextHelper.GetContextValue(featureContext, "itemIdValue");
                characteristicKind = ContextHelper.GetContextValue(featureContext, "CharacteristicKind");
                characteristicName = ContextHelper.GetContextValue(featureContext, "CharacteristicType");
                lobName = ContextHelper.GetContextValue(featureContext, "LOB");

                var query = CosmosDBQueries.GetItemPoolQuery(itemId);
                var dbItem = (await cosmosDb.GetDataFromCosmosDB<ItemPoolRecord>(database.ToLower(), container, query)).Item;

                var targetService = dbItem.ItemLineOfBusinesses?.FirstOrDefault(s =>
      string.Equals(s.ServiceProcessingType, processingType, StringComparison.OrdinalIgnoreCase) &&
   string.Equals(s.Name, lobName, StringComparison.OrdinalIgnoreCase));

                ValidationHelper.FailIfNullOrEmpty("targetService", targetService, $"No LOB {lobName} found with processing type: {processingType}");

                var kind = targetService.ServiceAttributes?.Kinds
                         ?.FirstOrDefault(k => k.Name.Equals(characteristicKind, StringComparison.OrdinalIgnoreCase));

                ValidationHelper.FailIfNullOrEmpty("Characteristic Kind", kind, $"{characteristicKind} not found under the {lobName} LOB.");

                var pptCharacteristic = kind.Characteristics
                      ?.FirstOrDefault(c => c.Name.Equals(characteristicName, StringComparison.OrdinalIgnoreCase));

                ValidationHelper.FailIfNullOrEmpty("pptCharacteristic", pptCharacteristic, $"Characteristic '{characteristicName}' not found under {characteristicKind}.");

                var uiCharacteristicValue = ContextHelper.GetContextValue(featureContext, "Value");
                ValidationHelper.AssertEqual("Characteristic Value", pptCharacteristic.Value, uiCharacteristicValue, "Mismatch between UI and DB characteristic values.");
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"Characteristic Value not matching in db for {lobName} under {processingType} in {database}>{container} for {itemId}", e);
            }
        }

        public async Task VerifyCharacteristicFieldAsync(string value, string database, string container, string documentType)
        {
            var itemId = string.Empty;
            try
            {
                itemId = ContextHelper.GetContextValue(featureContext, "itemIdValue");
                var defaultQuery = CosmosDBQueries.GetDataUsingItemIdAndDocumentType(itemId, documentType);
                var dbDefaultData = await cosmosDb.GetDataFromCosmosDB<ItemPoolRecord>(database.ToLower(), container, defaultQuery);

                var dbValueDict = dbDefaultData.Item.SkuAttributes?.Kinds?
                      .SelectMany(kind => kind.Characteristics.Select(ch => new
                      {
                          Kind = kind.Name.Trim(),
                          Type = ch.Name.Trim(),
                          Value = ch.Value?.Trim()
                      }))
             .ToDictionary(x => (x.Kind.ToLower(), x.Type.ToLower()), x => x.Value);

                var kind = ContextHelper.GetContextValue(featureContext, "CharacteristicKind");
                var type = ContextHelper.GetContextValue(featureContext, "CharacteristicType");

                await itemMasterPage.GetCharacteristicKind(kind).ScrollIntoViewIfNeededAsync();
                await itemMasterPage.GetCharacteristicKind(kind).ClickAsync();
                await itemMasterPage.GetCharacteristicType(type).ScrollIntoViewIfNeededAsync();
                await itemMasterPage.GetCharacteristicType(type).ClickAsync();

                var uiValue = ContextHelper.GetContextValue(featureContext, "Value");
                var key = (kind.ToLower(), type.ToLower());

                ValidationHelper.FailIfNullOrEmpty("Characteristic Kind", dbValueDict.ContainsKey(key),
                 $"No DB entry found for Kind: '{kind}', Type: '{type}'");

                var dbValue = dbValueDict[key];

                ValidationHelper.AssertEqual(value, uiValue, dbValue,
                    $"Mismatch for Kind: '{kind}', Type: '{type}' of characteristic {value}. Expected: '{uiValue}' but found: '{dbValue}'");
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Characteristic value validation failed for {documentType} document with ItemId: {itemId}.", ex);
            }
        }

        public async Task VerifyCharacteristicValueInItemPoolAsync(Table valuesToCheck)
        {
            var itemId = string.Empty;
            try
            {
                var database = DatabaseConstants.DatabaseName;
                var container = DatabaseConstants.ItemPoolsContainer;
                itemId = ContextHelper.GetContextValue(featureContext, "itemIdValue");
                var query = CosmosDBQueries.GetItemPoolQuery(itemId);

                var dbData = await cosmosDb.GetDataFromCosmosDB<ItemPoolRecord>(database, container, query);
                ValidationHelper.FailIfNullOrEmpty("ItemPoolRecord", dbData, $"for itemId: {itemId}");

                var characteristics = dbData.Item.ItemLineOfBusinesses
                          .SelectMany(lob => lob.ServiceAttributes?.Kinds ?? new List<ServiceAttributes.Kind>())
                    .SelectMany(
                        kind => kind.Characteristics ?? new List<ServiceAttributes.Characteristic>(),
                        (kind, c) => (Kind: kind.Name?.Trim(), Type: c.Name?.Trim(), Value: c.Value?.ToString().Trim().ToLower()))
                 .ToList();

                ValidationHelper.FailIfNullOrEmpty("characteristics", characteristics, $"characteristic kind and type not found for itemId: {itemId}");

                foreach (var value in valuesToCheck.Rows)
                {
                    var expectedKind = value[0]?.Trim();
                    var expectedType = value[1]?.Trim();
                    var expectedValue = value[2]?.Trim();

                    var matches = characteristics.Where(c =>
                    c.Kind.Equals(expectedKind, StringComparison.OrdinalIgnoreCase) &&
                      c.Type.Equals(expectedType, StringComparison.OrdinalIgnoreCase)).ToList();

                    ValidationHelper.FailIfNullOrEmpty($"{expectedKind} - {expectedType}", matches, $"not found for itemId: {itemId}");

                    foreach (var match in matches)
                    {
                        ValidationHelper.AssertEqual($"{expectedKind} - {expectedType}", expectedValue, match.Value, $"Mismatch for itemId: {itemId}");
                    }
                }
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"UI characteristic value not matching with db for itemId '{itemId}'", e);
            }
        }

        public async Task VerifyCharacteristicAndLevelLineageValueAsync(Table valuesToCheck)
        {
            var itemId = string.Empty;
            try
            {
                var database = DatabaseConstants.DatabaseName;
                var container = DatabaseConstants.ItemPoolsContainer;
                itemId = ContextHelper.GetContextValue(featureContext, "itemIdValue");
                var query = CosmosDBQueries.GetItemPoolQuery(itemId);

                var dbData = await cosmosDb.GetDataFromCosmosDB<ItemPoolRecord>(database, container, query);
                ValidationHelper.FailIfNullOrEmpty("ItemPoolRecord", dbData, $"for itemId: {itemId}");

                var skuAttributes = dbData.Item.SkuAttributes;
                ValidationHelper.FailIfNullOrEmpty("SkuAttributes", skuAttributes, $"for itemId: {itemId}");

                var skuCharacteristics = skuAttributes?.Kinds?
             .SelectMany(
                 kind => kind.Characteristics ?? new List<SkuAttributes.Characteristic>(),
                 (kind, characteristic) => new
                  {
                      Kind = kind.Name?.Trim(),
                      Type = characteristic.Name?.Trim(),
                      Value = characteristic.Value?.Trim(),
                      LevelLineage = characteristic.LevelLineage
                  })
               .ToList();

                ValidationHelper.FailIfNullOrEmpty("characteristics", skuCharacteristics, $"characteristic kind and type not found for itemId: {itemId}");

                foreach (var value in valuesToCheck.Rows)
                {
                    var expectedKind = value[0]?.Trim();
                    var expectedType = value[1]?.Trim();
                    var expectedValue = value[2]?.Trim();
                    var expectedDerivedFrom = value[3]?.Trim();
                    var expectedItemValue = value[4]?.Trim();

                    var match = skuCharacteristics.FirstOrDefault(c =>
                       c.Kind.Equals(expectedKind, StringComparison.OrdinalIgnoreCase) &&
                    c.Type.Equals(expectedType, StringComparison.OrdinalIgnoreCase));

                    Assert.Multiple(() =>
                      {
                          ValidationHelper.FailIfNullOrEmpty($"{expectedKind} - {expectedType}", match, $"not found for itemId: {itemId}");
                          ValidationHelper.AssertEqual($"{expectedKind} - {expectedType} Value", expectedValue, match.Value, $"Mismatch for itemId: {itemId}");
                          ValidationHelper.AssertEqual($"{expectedKind} - {expectedType} LevelLineage DerivedFrom", expectedDerivedFrom, match.LevelLineage?.ValueDerivedFrom, $"Mismatch for itemId: {itemId}");
                          ValidationHelper.AssertEqual($"{expectedKind} - {expectedType} LevelLineage Item Value", expectedItemValue, match.LevelLineage?.Item?.Value, $"Mismatch for itemId: {itemId}");
                      });
                }
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"UI characteristic value not matching with DB for itemId '{itemId}'", e);
            }
        }

        public async Task VerifyItemLineOfBusinessPrimarySecondaryAsync(string database, string container)
        {
            var itemId = string.Empty;
            try
            {
                itemId = ContextHelper.GetContextValue(featureContext, "itemIdValue");
                await page.WaitForTimeoutAsync(TimeConstants.ItemOrAlternatesPoolTriggerTimeMs);

                var query = CosmosDBQueries.GetItemPoolQuery(itemId);
                var itemPoolRecord = (await cosmosDb.GetDataFromCosmosDB<ItemPoolRecord>(database.ToLower(), container, query)).Item;

                ValidationHelper.FailIfNullOrEmpty("itemPoolRecord", itemPoolRecord, $"No document found for ItemId '{itemId}'.");
                ValidationHelper.FailIfNullOrEmpty("ItemLineOfBusinesses", itemPoolRecord.ItemLineOfBusinesses, "ItemLineOfBusinesses array is null.");

                string lob1Name = ContextHelper.GetContextValue(featureContext, "LOB1");
                string lob2Name = ContextHelper.GetContextValue(featureContext, "LOB2");

                var primaryLob = itemPoolRecord.ItemLineOfBusinesses
                  .FirstOrDefault(lob => lob.Name.Equals(lob1Name, StringComparison.OrdinalIgnoreCase));

                ValidationHelper.FailIfNullOrEmpty("PrimaryLOB", primaryLob, $"PrimaryLob '{lob1Name}' not found in ItemLineOfBusinesses.");

                var secondaryLob = itemPoolRecord.ItemLineOfBusinesses
             .FirstOrDefault(lob => lob.Name.Equals(lob2Name, StringComparison.OrdinalIgnoreCase));

                ValidationHelper.FailIfNullOrEmpty("secondaryLob", secondaryLob, $"SecondaryLob '{lob2Name}' not found in ItemLineOfBusinesses.");

                if (secondaryLob.ServiceProcessingType.Equals("Primary", StringComparison.OrdinalIgnoreCase))
                {
                    ValidationHelper.AssertEqual("ServiceProcessingType", "Secondary", primaryLob.ServiceProcessingType,
                 $"LOB1 '{lob1Name}' should be 'Secondary' when LOB2 '{lob2Name}' is 'Primary'.");
                }
                else
                {
                    ValidationHelper.Fail($"LOB2 '{lob2Name}' is not marked as 'Primary'.");
                }
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"Verification failed for ItemId '{featureContext["itemIdValue"]}'", e);
            }
        }

        public async Task VerifyDocumentShouldNotBeCreatedAsync()
        {
            var itemId = string.Empty;
            try
            {
                var database = DatabaseConstants.DatabaseName;
                var container = DatabaseConstants.ItemPoolsContainer;
                itemId = ContextHelper.GetContextValue(featureContext, "itemIdValue");
                var query = CosmosDBQueries.GetItemPoolQuery(itemId);

                var itemPoolData = await cosmosDb.GetDataFromCosmosDB<ItemPoolRecord>(database, container, query);

                ValidationHelper.FailIfNotNullOrNotEmpty("itemPoolData", itemPoolData,
               $"Expected no document created in {container}', but found some data in the {container}.");
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"Failed to validate absence of itemPools document in DB. Details for {itemId}", e);
            }
        }

        public async Task VerifyGeneralFlagsAsync(Table generalDetails)
        {
            try
            {
                await DatabaseValidationhelper.VerifyGeneralFlagsAsync<ItemPoolRecord>(
             featureContext,
             itemMasterPage,
             cosmosDb,
             ItemConstants.ItemPoolDocType,
             DatabaseConstants.DatabaseName,
             DatabaseConstants.ItemPoolsContainer,
             generalDetails);
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail("ItemPool general flag validation failed.", ex);
            }
        }
    }
}

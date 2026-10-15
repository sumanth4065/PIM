using PIMQAAutomation.Modules.Bom.Constants;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Modules.Common.Helpers;
using PIMQAAutomation.Utilities;
using PPT.ItemMaster.Data.Cosmos.Features.BOMs;
using PPT.ItemMaster.Domain.Features.DocumentSources;
using TechTalk.SpecFlow;

namespace PIMQAAutomation.Modules.Bom.Steps
{
    public partial class BOM
    {
        [Then(@"User retrieves the PartitionKey for Asset SerialNumber from Cosmos DB using ""(.*)"" database and ""(.*)"" container")]
        public async Task ThenUserRetrievesThePartitionKeyForAssetSerialNumberFromCosmosDbUsingDatabaseAndContainer(string database, string container)
        {
            try
            {
                testCaseName = scenarioContext.ScenarioInfo.Title;
                var partitionKey = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.PartitionKey");
                var contextName = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.Asset-BOM-Kind");

                ValidationHelper.FailIfNullOrEmpty("partitionKey", partitionKey, $"not found or empty in test case '{testCaseName}'");
                ValidationHelper.FailIfNullOrEmpty("contextName", contextName, $"not found or empty in test case '{testCaseName}'");

                var pimBomQuery = CosmosDBQueries.GetPimBomDataUsingPartitionKey(contextName, partitionKey);
                var dbBomData = await cosmosDb.GetDataFromCosmosDB<BOMRecord>(database, container, pimBomQuery);
                ValidationHelper.FailIfNullOrEmpty("dbBomData", dbBomData, $"No BOM data found in database '{database}' container '{container}' for PimBomQuery {pimBomQuery}");

                featureContext["PartOrProductPartitionKey"] = dbBomData.PartitionKey;
                featureContext["OemId"] = dbBomData.Filter.Oem.Ref.Id;
                featureContext["FamilyId"] = dbBomData.Filter.Family.Ref.Id;
                featureContext["ItemId"] = dbBomData.Filter.Item.Sku;
                featureContext["BestBOMPartitionKey"] = partitionKey;
            }
            catch (Exception ex)
            {
                var errorMessage = $"Failed to retrieve PartitionKey for Asset SerialNumber from Cosmos DB using database '{database}' and container '{container}'. Error: {ex.Message}";

                ValidationHelper.Fail(errorMessage, ex);
            }
        }

        [Then(@"Verify BOM UI data in ""(.*)"" tab with Cosmos DB using ""(.*)"" database and ""(.*)"" container using ""(.*)"" reference")]
        public async Task ThenVerifyBomUiDataInTabWithCosmosDbUsingDatabaseAndContainer(string tabName, string databaseName, string containerName, string bomKind, Table bomHeaderValues)
        {
            try
            {
                testCaseName = scenarioContext.ScenarioInfo.Title;
                var errorMessage = string.Empty;
                if (!featureContext.ContainsKey(BomConstants.FeatureContextValue.BOMHeaderDetails))
                {
                    ValidationHelper.Fail($"AssetFields not found in feature context for verifying BOM UI data in '{tabName}' tab");
                }
                var assetFieldValues = featureContext[BomConstants.FeatureContextValue.BOMHeaderDetails] as Dictionary<string, string>;
                ValidationHelper.FailIfNullOrEmpty("assetFieldValues", assetFieldValues, $"is null or empty in feature context for verifying BOM UI data in '{tabName}' tab");
                var contextName = await bomPage.GetBomInputField(tabName, BomConstants.BOMKind).InputValueAsync();
                BOMRecord dbBomData = null;
                var assetOrOemId = string.Empty;

                switch (tabName)
                {
                    case BomConstants.BOMAsset:
                        {
                            if (!featureContext.ContainsKey("PartOrProductPartitionKey"))
                            {
                                errorMessage = $"AssetPartitionKey not found in feature context for BOM Asset tab";

                                ValidationHelper.Fail(errorMessage);
                            }

                            var assetPartitionKey = featureContext["PartOrProductPartitionKey"].ToString();
                            var assetBomQuery = CosmosDBQueries.GetPimBomDataUsingPartitionKey(contextName, assetPartitionKey);
                            dbBomData = await cosmosDb.GetDataFromCosmosDB<BOMRecord>(databaseName, containerName, assetBomQuery);

                            ValidationHelper.FailIfNullOrEmpty("dbBomData", dbBomData, $"NoBOM data found for assetBomQuery '{assetBomQuery}' in database '{databaseName}' container '{containerName}'");

                            assetOrOemId = dbBomData.Filter.Asset.Ref.Id.ToString();
                            break;
                        }

                    case BomConstants.BOMProduct:
                        {
                            var familyId = featureContext["FamilyId"].ToString();
                            if (!featureContext.ContainsKey("FamilyId"))
                            {
                                errorMessage = $"FamilyId not found in feature context";

                                ValidationHelper.Fail(errorMessage);
                            }

                            var productBomQuery = CosmosDBQueries.GetPimBomDataUsingPartitionKey(contextName, familyId);
                            dbBomData = await cosmosDb.GetDataFromCosmosDB<BOMRecord>(databaseName, containerName, productBomQuery);

                            ValidationHelper.FailIfNullOrEmpty("dbBomData", dbBomData, $"NoBOM data found for product reference '{contextName}' in database '{databaseName}' container '{containerName}'");

                            assetOrOemId = dbBomData.Filter.Oem.Ref.Id.ToString();
                            scenarioContext["ProductBOMQuery"] = productBomQuery;
                            break;
                        }

                    case BomConstants.BOMPart:
                        {
                            if (!assetFieldValues.ContainsKey("Oem") || !assetFieldValues.ContainsKey("Sku"))
                            {
                                errorMessage = $"Required fields (Oem, Sku) not found in AssetFields for BOM Part tab";

                                ValidationHelper.Fail(errorMessage);
                            }

                            var itemId = assetFieldValues["Sku"];
                            var oemId = featureContext["OemId"].ToString();
                            var partPartitionKey = $"{oemId}|{itemId}";
                            var partBomQuery = CosmosDBQueries.GetPimBomDataUsingPartitionKey(contextName, partPartitionKey);
                            dbBomData = await cosmosDb.GetDataFromCosmosDB<BOMRecord>(databaseName, containerName, partBomQuery);

                            ValidationHelper.FailIfNullOrEmpty("dbBomData", dbBomData, $"NoBOM data found for partBomQuery '{partBomQuery}' in database '{databaseName}' container '{containerName}'");

                            assetOrOemId = dbBomData.Filter.Oem.Ref.Id.ToString();
                            break;
                        }

                    default:
                        ValidationHelper.Fail($"Unsupported tab name: {tabName}");
                        break;
                }

                scenarioContext["AssetOrOemId"] = assetOrOemId;

                //get partition key to use further queries
                var partitionKey = dbBomData.PartitionKey;
                scenarioContext["PartitionKey"] = partitionKey;
                var uiBomHeaderValues = new Dictionary<string, string>();
                await ToggleSectionAsync(expandOnly: true);
                foreach (var bomHeaderValue in bomHeaderValues.Rows)
                {
                    var menuLocator = bomHeaderValue[0].ToLower();
                    var inputValue = await bomPage.GetBomInputField(tabName, menuLocator).InputValueAsync();
                    uiBomHeaderValues[bomHeaderValue[0]] = inputValue;
                }

                var dbBomHeaderValues = new Dictionary<string, string>();
                foreach (var bomHeaderValue in bomHeaderValues.Rows)
                {
                    var menuLocator = bomHeaderValue[0];
                    var value = dbBomData.Filter.GetType().GetProperty(menuLocator).GetValue(dbBomData.Filter);
                    var name = value.GetType().GetProperty(ApplicationConstants.PropertyName).GetValue(value).ToString();
                    dbBomHeaderValues[menuLocator] = name;
                }

                foreach (var field in uiBomHeaderValues.Keys)
                {
                    if (!uiBomHeaderValues.ContainsKey(field))
                    {
                        errorMessage = $"UI does not contain expected field: {field}";

                        ValidationHelper.Fail(errorMessage);
                    }

                    ValidationHelper.AssertEqual("BOM Header Value", uiBomHeaderValues[field], dbBomHeaderValues[field], $"Mismatch for field '{field}': Expected '{uiBomHeaderValues[field]}' but found '{dbBomHeaderValues[field]}'");
                }
            }
            catch (Exception ex)
            {
                var errorMessage = $"BOM UI data not matching with {databaseName}>>{containerName} with error: {ex.Message}";

                ValidationHelper.Fail(errorMessage, ex);
            }
        }

        [Then(@"Verify BOM Header ""(.*)"" in ""(.*)"" tab with Cosmos DB using ""(.*)"" database and ""(.*)"" container")]
        public async Task ThenVerifyBomHeaderInTabAndCosmosDbUsingDatabaseAndContainer(string filedName, string tabName, string database, string container)
        {
            try
            {
                var errorMessage = string.Empty;
                if (!scenarioContext.ContainsKey("PartitionKey"))
                {
                    errorMessage = $"PartitionKey not found in scenario context for verifying BOM Header '{filedName}' in '{tabName}' tab";

                    ValidationHelper.Fail(errorMessage);
                }

                var partitionKey = scenarioContext["PartitionKey"].ToString() ?? string.Empty;

                var bomQuery = CosmosDBQueries.GetDataUsingPartitionKey(partitionKey);
                var dbBomData = await cosmosDb.GetDataFromCosmosDB<BOMRecord>(database, container, bomQuery);

                ValidationHelper.FailIfNullOrEmpty("dbBomData", dbBomData, $"No BOM data found for partition key '{partitionKey}' in database '{database}' container '{container}'");
                await ToggleSectionAsync(expandOnly: true);
                switch (filedName)
                {
                    case BomConstants.BOMInternalNotes:
                        {
                            var internalNotesDb = dbBomData.Notes;
                            var internalNotesUi = await bomPage.GetBomHeaderInternalNotes(tabName).InputValueAsync();

                            ValidationHelper.AssertEqual("Internal Notes", internalNotesUi?.Trim() ?? string.Empty, internalNotesDb?.Trim() ?? string.Empty, $"Internal Notes do not match. UI: '{internalNotesUi?.Trim() ?? string.Empty}', DB: '{internalNotesDb?.Trim() ?? string.Empty}'");

                            break;
                        }

                    case BomConstants.BOMHeaderItemId:
                        {
                            var dbItemId = dbBomData.Filter.Item.Sku;
                            var uiItemId = await bomPage.GetBomInputField(tabName, filedName).InputValueAsync();

                            ValidationHelper.AssertEqual("Item ID", uiItemId, dbItemId, $"Item ID does not match. UI: '{uiItemId}', DB: '{dbItemId}'");

                            break;
                        }

                    default:
                        ValidationHelper.Fail($"Unhandled field name: {filedName}");
                        break;
                }
            }
            catch (Exception ex)
            {
                var errorMessage = $"BOM Header data not matching with {database}>>{container} with error: {ex.Message}";

                ValidationHelper.Fail(errorMessage, ex);
            }
        }

        [Then(@"Verify the UI ""(.*)"" and Source Type as ""(.*)"" with Cosmos DB using the ""(.*)"" database and ""(.*)"" container for ""(.*)"" tab")]
        public async Task ThenVerifyTheUiAndSourceTypeWithCosmosDbUsingTheDatabaseAndContainer(string ModifiedDate, string source_Type, string database, string container, string tabName)
        {
            try
            {
                var bomQuery = string.Empty;
                var errorMessage = string.Empty;
                switch (tabName)
                {
                    case BomConstants.BOMAsset or BomConstants.BOMPart:
                        if (!scenarioContext.ContainsKey("PartitionKey"))
                        {
                            errorMessage = $"PartitionKey not found in scenario context for '{tabName}' tab";

                            ValidationHelper.Fail(errorMessage);
                        }

                        bomQuery = CosmosDBQueries.GetDataUsingPartitionKey(scenarioContext["PartitionKey"].ToString());
                        break;

                    case BomConstants.BOMProduct:
                        if (!scenarioContext.ContainsKey("ProductBOMQuery"))
                        {
                            errorMessage = $"ProductBOMQuery not found in scenario context for '{tabName}' tab";

                            ValidationHelper.Fail(errorMessage);
                        }

                        bomQuery = scenarioContext["ProductBOMQuery"].ToString();
                        break;

                    default:
                        ValidationHelper.Fail($"Unsupported tab name: {tabName}");
                        break;
                }

                var dbBomData = await cosmosDb.GetDataFromCosmosDB<BOMRecord>(database, container, bomQuery);

                ValidationHelper.FailIfNullOrEmpty("dbBomData", dbBomData, $"No BOM data found in database '{database}' container '{container}' for query");

                List<DocumentModifiedSource> documentModifiedSource = dbBomData.DocumentModifiedSource;
                foreach (var documentModifiedSour in documentModifiedSource)
                {
                    Console.WriteLine($"documentModifiedSour is {documentModifiedSour}");
                }

                ValidationHelper.FailIfNullOrEmpty("documentModifiedSource", documentModifiedSource, $"DocumentModifiedSource is null or empty for source type '{source_Type}'");

                var sourceType = documentModifiedSource.FirstOrDefault(item => item.SourceType == source_Type);

                ValidationHelper.FailIfNullOrEmpty("sourceType", sourceType, $"No document modified source found with source type '{source_Type}'");

                var sourceModifiedDate = sourceType.ModifiedDate.ToString();
                var dbLocalDate = DateTimeHelper.ConvertDbUtcToLocal(sourceModifiedDate);
                var uiDocumentModifiedDate = (await commonLocators.GetDocumentModifiedUserDate(ModifiedDate).TextContentAsync()).Trim();
                ValidationHelper.AssertEqual("Validate DB and UI Date", dbLocalDate, uiDocumentModifiedDate, $"Mismatch: db Date '{dbLocalDate}' Ui date '{uiDocumentModifiedDate}'");
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Error validating modified date for Tab: {tabName}, Source Type: {source_Type}, Database: {database}, Container: {container}, Message: {ex.Message}, StackTrace: {ex.StackTrace}", ex);
            }
        }

        [Then(@"Verify ""(.*)"" BOM line grid row ""(.*)"" and Cosmos DB using ""(.*)"" database and ""(.*)"" container for ""(.*)"" tab")]
        public async Task ThenVerifyBomLineGridRowAndCosmosDbUsingDatabaseAndContainerForTab(string sourceType, int rowNumber, string database, string container, string tabName, Table bomLineGridFileds)
        {
            try
            {
                var bomQuery = string.Empty;
                var errorMessage = string.Empty;
                switch (tabName)
                {
                    case BomConstants.BOMAsset or BomConstants.BOMPart:
                        if (!scenarioContext.ContainsKey("PartitionKey"))
                        {
                            errorMessage = $"PartitionKey not found in scenario context for '{tabName}' tab";

                            ValidationHelper.Fail(errorMessage);
                        }

                        bomQuery = CosmosDBQueries.GetDataUsingPartitionKey(scenarioContext["PartitionKey"].ToString());
                        break;

                    case BomConstants.BOMProduct:
                        if (!scenarioContext.ContainsKey("ProductBOMQuery"))
                        {
                            errorMessage = $"ProductBOMQuery not found in scenario context for '{tabName}' tab";

                            ValidationHelper.Fail(errorMessage);
                        }

                        bomQuery = scenarioContext["ProductBOMQuery"].ToString();
                        break;

                    default:
                        ValidationHelper.Fail($"Unsupported tab name: {tabName}");
                        break;
                }

                var dbBomData = await cosmosDb.GetDataFromCosmosDB<BOMRecord>(database, container, bomQuery);

                ValidationHelper.FailIfNullOrEmpty("dbBomData", dbBomData, $"No BOM data found in database '{database}' container '{container}' for query");

                var assetOrOemId = string.Empty;
                if (sourceType == BomConstants.ImportSourceType)
                {
                    switch (tabName)
                    {
                        case BomConstants.BOMAsset:
                            assetOrOemId = dbBomData.Filter.Asset.Ref.Id.ToString();
                            break;

                        case BomConstants.BOMProduct or BomConstants.BOMPart:
                            assetOrOemId = dbBomData.Filter.Oem.Ref.Id.ToString();
                            break;

                        default:
                            ValidationHelper.Fail($"Unsupported tab name for source type: {tabName}");
                            break;
                    }
                }

                scenarioContext["AssetOrOemId"] = assetOrOemId;
                var bomLineGridValues = new Dictionary<string, string>();

                foreach (var field in bomLineGridFileds.Rows)
                {
                    var columnName = field[0];
                    var menuLocator = columnName.ToLower();
                    var value = (await bomPage.GetBomLineTableValue(menuLocator, rowNumber).TextContentAsync()).Trim();
                    if (menuLocator == BomConstants.LocatorProperties.BOMObsolete)
                    {
                        value = value == "N" ? "False" : value == "Y" ? "True" : value;
                    }

                    bomLineGridValues[columnName] = value;
                }

                var itemIdFromUi = (await bomPage.GetBomLineTableValue(BomConstants.BomLineGridFields.ItemId, rowNumber).TextContentAsync()).Trim();
                var dbbomLineRecord = dbBomData.Components.FirstOrDefault(c => c.Item.Sku == itemIdFromUi);

                ValidationHelper.FailIfNullOrEmpty("dbbomLineRecord", dbbomLineRecord, $"No DB component found with SKU '{itemIdFromUi}' for UI row #{rowNumber}");

                var dbRow = new Dictionary<string, string>
                {
                    { BomConstants.BomLineGridFields.ItemId, dbbomLineRecord.Item.Sku },
                    { BomConstants.BomLineGridFields.Oem, dbbomLineRecord.Item.Oem.Name },
                    { BomConstants.BomLineGridFields.Quantity, dbbomLineRecord.Line.Quantity.ToString() },
                    { BomConstants.BomLineGridFields.Weight, dbbomLineRecord.Line.Weight.ToString() },
                    { BomConstants.BomLineGridFields.Obsolete, dbbomLineRecord.IsObsolete.ToString() }
                };

                var matchFound = bomLineGridValues.All(kvp =>
                dbRow.ContainsKey(kvp.Key) &&
                dbRow[kvp.Key].Equals(kvp.Value, StringComparison.OrdinalIgnoreCase));

                if (!matchFound)
                {
                    ValidationHelper.Fail($"No matching DB component found for UI row #{rowNumber}. UI values: {string.Join(", ", bomLineGridValues.Select(kvp => $"{kvp.Key}='{kvp.Value}'"))}");
                }
            }
            catch (Exception ex)
            {
                var errorMessage = $"BOM Line grid values are not displayed in {database}>>{container} with error: {ex.Message}";

                ValidationHelper.Fail(errorMessage, ex);
            }
        }

        [Then(@"User Verifies the imported data is reflected in the BOM Line grid row ""(.*)""")]
        public async Task ThenUserVerifiesTheImportedDataIsReflectedInBomPage(int rowNumber)
        {
            try
            {
                var errorMessage = string.Empty;
                if (!scenarioContext.ContainsKey("PartitionKey"))
                {
                    errorMessage = $"PartitionKey not found in scenario context for verifying imported data";

                    ValidationHelper.Fail(errorMessage);
                }

                var partitionKey = scenarioContext["PartitionKey"].ToString() ?? string.Empty;
                ValidationHelper.FailIfNullOrEmpty("PartitionKey", partitionKey, $"PartitionKey is null or empty in scenario context for verifying imported data");
                var bomQuery = CosmosDBQueries.GetDataUsingPartitionKey(partitionKey);
                var dbBomData = await cosmosDb.GetDataFromCosmosDB<BOMRecord>(DatabaseConstants.DatabaseName, DatabaseConstants.BOMsContainer, bomQuery);

                ValidationHelper.FailIfNullOrEmpty("dbBomData", dbBomData, $"No BOM data found for partition key '{partitionKey}' in database '{DatabaseConstants.DatabaseName}' container '{DatabaseConstants.BOMsContainer}'");

                ValidationHelper.FailIfNullOrEmpty("dbBomData.Components", dbBomData.Components, $"No components found in BOM data for partition key '{partitionKey}'");

                await bomPage.GetBomLineTableValue(BomConstants.LocatorProperties.BOMQuantity, rowNumber).ScrollIntoViewIfNeededAsync();

                // Fetch UI values
                var quantityUi = (await bomPage.GetBomLineTableValue(BomConstants.LocatorProperties.BOMQuantity, rowNumber).TextContentAsync()).Trim();
                var oemUi = (await bomPage.GetBomLineTableValue(BomConstants.LocatorProperties.BOMOem, rowNumber).TextContentAsync()).Trim();
                var itemIdUi = (await bomPage.GetBomLineTableValue(BomConstants.LocatorProperties.BOMItemId, rowNumber).TextContentAsync()).Trim();

                // Fetch DB values
                var oemDb = dbBomData.Components[0].Item.Oem.Name;
                var quantityDb = dbBomData.Components[0].Line.Quantity.ToString();
                var itemIdDb = dbBomData.Components[0].Item.Sku;

                ValidationHelper.AssertEqual("Quantity", quantityUi, quantityDb, $"Mismatch in Quantity. UI value: {quantityUi}, DB value: {quantityDb}");
                ValidationHelper.AssertEqual("OEM", oemUi, oemDb, $"Mismatch in OEM. UI value: {oemUi}, DB value: {oemDb}");
                ValidationHelper.AssertEqual("ItemId", itemIdUi, itemIdDb, $"Mismatch in ItemId. UI value: {itemIdUi}, DB value: {itemIdDb}");
            }
            catch (Exception ex)
            {
                var errorMessage = $"Failed to verify imported data is reflected in BOM page. Error: {ex.Message}";

                ValidationHelper.Fail(errorMessage, ex);
            }
        }

        [Then(@"Verify Bom internal notes in ""(.*)"" tab and Cosmos DB using ""(.*)"" database and ""(.*)"" container")]
        public async Task ThenVerifyBomInternalNotesInTabAndCosmosDbUsingDatabaseAndContainer(string tabName, string databaseName, string containerName)
        {
            try
            {
                var errorMessage = string.Empty;
                var contextName = await bomPage.GetBomInputField(tabName, BomConstants.BOMKind).InputValueAsync();

                if (!scenarioContext.ContainsKey(BomConstants.FeatureContextValue.BOMInternalNotes))
                {
                    ValidationHelper.Fail($"internalNotes not found in scenario context for verifying BOM internal notes in '{tabName}' tab");
                }

                var assetPartitionKey = featureContext["PartOrProductPartitionKey"].ToString();
                var dbBomData = await cosmosDb.GetDataFromCosmosDB<BOMRecord>(databaseName, containerName, CosmosDBQueries.GetPimBomDataUsingPartitionKey(contextName, assetPartitionKey));

                ValidationHelper.FailIfNullOrEmpty("dbBomData", dbBomData, $"No BOM data found for containerName '{containerName}' in database '{databaseName}' container '{containerName}'");

                var internalNotes = scenarioContext[BomConstants.FeatureContextValue.BOMInternalNotes].ToString();

                if (!string.IsNullOrWhiteSpace(dbBomData.Notes))
                {
                    ValidationHelper.AssertEqual("Internal Notes", internalNotes.Trim(), dbBomData.Notes.Trim(), $"Mismatch between UI and DB 'notes' field. UI: '{internalNotes.Trim()}', DB: '{dbBomData.Notes.Trim()}'");
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to verify BOM internal notes in '{tabName}' tab and Cosmos DB using database '{databaseName}' and container '{containerName}'", ex);
            }
        }

        [Then(@"Verify BOM UI data ""(.*)"" tab with Cosmos DB using ""(.*)"" database and ""(.*)"" container")]
        public async Task ThenVerifyBomUiDataTabWithCosmosDbUsingDatabaseAndContainer(string tabName, string databaseName, string containerName, Table table)
        {
            try
            {
                testCaseName = scenarioContext.ScenarioInfo.Title;
                var errorMessage = string.Empty;

                if (!scenarioContext.ContainsKey("BOMKind"))
                {
                    errorMessage = $"BOMKind not found in scenario context for verifying BOM UI data in '{tabName}' tab";

                    ValidationHelper.Fail(errorMessage);
                }

                var bomMKind = scenarioContext["BOMKind"].ToString();
                var assetpartitionKey = scenarioContext["PartitionKey"].ToString();
                ValidationHelper.FailIfNullOrEmpty("bomMKind", bomMKind, $"is null or empty in scenario context for verifying BOM UI data in '{tabName}' tab");

                await page.WaitForLoadStateAsync();
                var bomQuery = CosmosDBQueries.GetPimBomDataUsingPartitionKey(bomMKind, assetpartitionKey);
                var dbBomData = await cosmosDb.GetDataFromCosmosDB<BOMRecord>(databaseName, containerName, bomQuery);

                ValidationHelper.FailIfNullOrEmpty("dbBomData", dbBomData, $"No BOM data found for bomQuery '{bomQuery}' in database '{databaseName}' container '{containerName}'");

                var assetOrOemId = string.Empty;
                switch (tabName)
                {
                    case BomConstants.BOMAsset:
                        assetOrOemId = dbBomData.Filter.Asset.Ref.Id.ToString();
                        break;

                    case BomConstants.BOMProduct or BomConstants.BOMPart:
                        assetOrOemId = dbBomData.Filter.Oem.Ref.Id.ToString();
                        break;

                    default:
                        ValidationHelper.Fail($"Unsupported tab name: {tabName}");
                        break;
                }

                scenarioContext["AssetOrOemId"] = assetOrOemId;

                //get partition key to use further queries
                var partitionKey = dbBomData.PartitionKey;
                scenarioContext["PartitionKey"] = partitionKey;
                var uiBomHeaderValues = new Dictionary<string, string>();

                foreach (var menu in table.Rows)
                {
                    var menuLocator = menu[0].ToLower();
                    var inputValue = await bomPage.GetBomInputField(tabName, menuLocator).InputValueAsync();
                    uiBomHeaderValues[menu[0]] = inputValue;
                }

                var dbBomHeaderValues = new Dictionary<string, string>();
                foreach (var menu in table.Rows)
                {
                    var menuLocator = menu[0];
                    var value = dbBomData.Filter.GetType().GetProperty(menuLocator).GetValue(dbBomData.Filter);
                    var name = value.GetType().GetProperty(BomConstants.PropertyName).GetValue(value).ToString();
                    dbBomHeaderValues[menuLocator] = name;
                }

                foreach (var field in uiBomHeaderValues.Keys)
                {
                    if (!uiBomHeaderValues.ContainsKey(field))
                    {
                        errorMessage = $"UI does not contain expected field: {field}";

                        ValidationHelper.Fail(errorMessage);
                    }

                    ValidationHelper.AssertEqual("BOM Header Value", uiBomHeaderValues[field], dbBomHeaderValues[field], $"Mismatch for field '{field}': Expected '{uiBomHeaderValues[field]}' but found '{dbBomHeaderValues[field]}'");
                }
            }
            catch (Exception ex)
            {
                var errorMessage = $"BOM UI data not matching with {databaseName}>>{containerName} with error: {ex.Message}";

                ValidationHelper.Fail(errorMessage, ex);
            }
        }
    }
}
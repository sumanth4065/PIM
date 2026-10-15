using PIMQAAutomation.Modules.Bom.Constants;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Utilities;
using PPT.ItemMaster.Data.Cosmos.Features.BestBomPools;
using PPT.ItemMaster.Data.Cosmos.Features.Customers;
using PPT.ItemMaster.Domain.Features.BestBomPools;
using TechTalk.SpecFlow;

namespace PIMQAAutomation.Modules.Bom.Steps
{
    public partial class BOM
    {
        [Then(@"Verify ""(.*)"" and Item ""(.*)"" are not empty in the ""(.*)"" container and verify customer ""(.*)"" in the ""(.*)"" container of the ""(.*)"" database for ""(.*)"" tab")]
        public async Task ThenVerifyAndItemAreNotEmptyInTheContainerForCreationAndVerifyCustomerInTheContainerOfTheDatabase(string noOfValidContracts, string itemID, string bestBomContainerName, string type, string customerContainer, string dataBaseName, string tabName)
        {
            try
            {
                testCaseName = scenarioContext.ScenarioInfo.Title;
                var oemName = string.Empty;
                var item = string.Empty;
                var partitionKey = string.Empty;
                var errorMessage = string.Empty;

                switch (tabName)
                {
                    case BomConstants.BOMPart:
                        oemName = await bomPage.GetBomInputField(BomConstants.LocatorProperties.BOMPart, BomConstants.LocatorProperties.BOMOem).InputValueAsync();
                        item = await bomPage.GetBomInputField(BomConstants.LocatorProperties.BOMPart, BomConstants.LocatorProperties.BOMItemId).InputValueAsync();

                        if (!featureContext.ContainsKey("PartOrProductPartitionKey"))
                        {
                            errorMessage = $"PartOrProductPartitionKey not found in feature context for '{tabName}' tab";

                            ValidationHelper.Fail(errorMessage);
                        }

                        partitionKey = featureContext["PartOrProductPartitionKey"].ToString();
                        break;

                    case BomConstants.BOMProduct:
                        oemName = await bomPage.GetBomInputField(BomConstants.LocatorProperties.BOMProduct, BomConstants.LocatorProperties.BOMOem).InputValueAsync();

                        if (!featureContext.ContainsKey("PartOrProductPartitionKey"))
                        {
                            errorMessage = $"PartOrProductPartitionKey not found in feature context for '{tabName}' tab";

                            ValidationHelper.Fail(errorMessage);
                        }

                        partitionKey = featureContext["PartOrProductPartitionKey"].ToString();
                        break;

                    case BomConstants.BOMAsset:
                        oemName = await bomPage.GetBomInputField(BomConstants.LocatorProperties.BOMAsset, BomConstants.LocatorProperties.BOMOem).InputValueAsync();
                        item = await bomPage.GetBomInputField(BomConstants.LocatorProperties.BOMAsset, BomConstants.LocatorProperties.BOMItemId).InputValueAsync();

                        if (!scenarioContext.ContainsKey("PartitionKey"))
                        {
                            errorMessage = $"PartitionKey not found in scenario context for '{tabName}' tab";

                            ValidationHelper.Fail(errorMessage);
                        }

                        partitionKey = scenarioContext["PartitionKey"] as string;
                        break;

                    default:
                        ValidationHelper.Fail($"Unsupported tab name: {tabName}");
                        break;
                }

                ValidationHelper.FailIfNullOrEmpty("partitionKey", partitionKey, $"is null or empty for '{tabName}' tab");

                var bestBomQuery = CosmosDBQueries.GetDataUsingPartitionKey(partitionKey);
                var bestBomData = await cosmosDb.GetDataFromCosmosDB<BestBomPoolRecord>(dataBaseName, bestBomContainerName, bestBomQuery);

                ValidationHelper.FailIfNullOrEmpty("bestBomData", bestBomData, $"No BestBOM data found for partition key '{partitionKey}' in database '{dataBaseName}' container '{bestBomContainerName}'");

                var numberOfValidContract = bestBomData.NumberOfValidContracts.ToString();
                ValidationHelper.FailIfNullOrEmpty("numberOfValidContract", numberOfValidContract, $"numberOfValidContracts is displaying as empty: {numberOfValidContract}");

                var itemId = bestBomData.Id.ToString();
                ValidationHelper.FailIfNullOrEmpty("itemId", itemId, $"itemId is displaying as empty: {itemId}");

                //get customer account id to fetch data in pim>> Customer
                ValidationHelper.FailIfNullOrEmpty("bestBomData.CustomerAccount?.Ref", bestBomData.CustomerAccount?.Ref, $"CustomerAccount or Ref is null in BestBOM data");

                var customerAccountId = bestBomData.CustomerAccount.Ref.Id.ToString();
                var customerQuery = CosmosDBQueries.GetId(customerAccountId);
                var customerData = await cosmosDb.GetDataFromCosmosDB<CustomerAccountRecord>(dataBaseName, customerContainer, customerQuery);

                ValidationHelper.FailIfNullOrEmpty("customerData", customerData, $"No customer data found for customer account ID '{customerAccountId}' in database '{dataBaseName}' container '{customerContainer}'");

                var customerType = customerData.Type;
                ValidationHelper.FailIfNullOrEmpty("customerType", customerType, $"Customer account details are displaying as empty: {customerType}");

                var itemSku = bestBomData.Item.Sku;
                ValidationHelper.AssertEqual("OEM Name", oemName, bestBomData.Oem.Name, $"OEM does not match. UI: '{oemName}', DB:{bestBomData.Oem.Name}'");

                if (tabName != BomConstants.BOMProduct)
                {
                    ValidationHelper.AssertEqual("Item Id", item, itemSku, $"Item Id does not match. UI: '{item}', DB: '{itemSku}'");
                }
            }
            catch (Exception ex)
            {
                var errorMessage = $"In {bestBomContainerName} data not reflecting with error: {ex.Message}";

                ValidationHelper.Fail(errorMessage, ex);
            }
        }

        [Then(@"Verify Source Kind and BOMLines grid row ""(.*)"" displayed in ""(.*)"" container of the ""(.*)"" database for ""(.*)"" tab through ""(.*)""")]
        public async Task ThenVerifySourceKindAndBomLinesGridRowDisplayedInContainerOfTheDatabaseForTab(int rowNumber, string container, string database, string tabName, string sourceKind, Table bomLineFields)
        {
            try
            {
                testCaseName = scenarioContext.ScenarioInfo.Title;
                var partitionKey = string.Empty;
                var errorMessage = string.Empty;

                switch (tabName)
                {
                    case BomConstants.BOMPart or BomConstants.BOMProduct:
                        if (!featureContext.ContainsKey("PartOrProductPartitionKey"))
                        {
                            errorMessage = $"PartOrProductPartitionKey not found in feature context for '{tabName}' tab";

                            ValidationHelper.Fail(errorMessage);
                        }

                        partitionKey = featureContext["PartOrProductPartitionKey"].ToString();
                        break;

                    case BomConstants.BOMAsset:
                        if (!scenarioContext.ContainsKey("PartitionKey"))
                        {
                            errorMessage = $"PartitionKey not found in scenario context for '{tabName}' tab";

                            ValidationHelper.Fail(errorMessage);
                        }

                        partitionKey = scenarioContext["PartitionKey"] as string;
                        break;

                    default:
                        ValidationHelper.Fail($"Unsupported tab name: {tabName}");
                        break;
                }

                ValidationHelper.FailIfNullOrEmpty("partitionKey", partitionKey, $"is null or empty for '{tabName}' tab");

                var bestBomQuery = CosmosDBQueries.GetDataUsingPartitionKey(partitionKey);
                var bestBomData = await cosmosDb.GetDataFromCosmosDB<BestBomPoolRecord>(database, container, bestBomQuery);

                ValidationHelper.FailIfNullOrEmpty("bestBomData", bestBomData, $"No BestBOM data found for partition key '{partitionKey}' in database '{database}' container '{container}'");

                var bomLineGridValues = new Dictionary<string, string>();
                foreach (var bomLineField in bomLineFields.Rows)
                {
                    var columnName = bomLineField[0];
                    var menuLocator = columnName.ToLower();
                    if (menuLocator == BomConstants.LocatorProperties.BOMObsolete)
                    {
                        var value = (await bomPage.GetBomLineTableValue(menuLocator, rowNumber).TextContentAsync()).Trim();
                        var obsoleteStatus = value == BomConstants.True ? "yes" : "no";
                        bomLineGridValues[columnName] = obsoleteStatus;
                    }
                    else
                    {
                        bomLineGridValues[columnName] = (await bomPage.GetBomLineTableValue(menuLocator, rowNumber).TextContentAsync()).Trim();
                    }
                }

                var itemIdFromUi = (await bomPage.GetBomLineTableValue(BomConstants.BomLineGridFields.ItemId, rowNumber).TextContentAsync()).Trim();
                var bestBomPoolBomLine = bestBomData.BomLines
                .FirstOrDefault(b => b.Item.Sku.Equals(itemIdFromUi, StringComparison.OrdinalIgnoreCase));

                ValidationHelper.FailIfNullOrEmpty("bestBomPoolBomLine", bestBomPoolBomLine, $"No matching DB BOM line found for SKU: '{itemIdFromUi}'");

                var dbBomLineGridValues = new Dictionary<string, string>
                {
                    { BomConstants.BomLineGridFields.ItemId, bestBomPoolBomLine.Item.Sku },
                    { BomConstants.BomLineGridFields.Oem, bestBomPoolBomLine.Oem.Name },
                    { BomConstants.BomLineGridFields.Quantity, bestBomPoolBomLine.Quantity.ToString() },
                    { BomConstants.BomLineGridFields.Obsolete, bestBomPoolBomLine.Obsolete }
                };

                foreach (var field in bomLineGridValues.Keys)
                {
                    if (!bomLineGridValues.ContainsKey(field))
                    {
                        errorMessage = $"UI does not contain expected field: {field}";

                        ValidationHelper.Fail(errorMessage);
                    }

                    ValidationHelper.AssertEqual("BOM Line Grid Value", bomLineGridValues[field], dbBomLineGridValues[field], $"Mismatch for field '{field}': Expected '{bomLineGridValues[field]}' but found '{dbBomLineGridValues[field]}'");
                }

                if (!featureContext.ContainsKey("BOMKind"))
                {
                    errorMessage = $"BOMKind not found in feature context";

                    ValidationHelper.Fail(errorMessage);
                }

                var bomKind = featureContext["BOMKind"].ToString();
                ValidationHelper.AssertEqual("BOM Kind", bomKind, bestBomPoolBomLine.SourceBomKind.Name, $"BOM Kind mismatch. Expected: '{bomKind}', Actual: '{bestBomPoolBomLine.SourceBomKind.Name}'");

                scenarioContext["PartitionKey"] = bestBomData.PartitionKey;
            }
            catch (Exception ex)
            {
                var errorMessage = $"Source BOM Kind {sourceKind} and BOM lines are not updated due to error: {ex.Message}";

                ValidationHelper.Fail(errorMessage, ex);
            }
        }

        [Then(@"Verify Event History should be updated with in the ""(.*)"" container of the ""(.*)"" database for ""(.*)"" tab")]
        public async Task ThenVerifyEventHistoryShouldBeUpdatedWithInTheContainerOfTheDatabase(string container, string database, string tabName, Table eventHistories)
        {
            var eventName = string.Empty;
            var triggerTime = 0;
            try
            {
                var partitionKey = string.Empty;
                var errorMessage = string.Empty;

                switch (tabName)
                {
                    case BomConstants.BOMProduct:
                        // Taking more than 13 min to load data in BestBOM Container
                        triggerTime = BomConstants.ProductBestBOMPoolTriggerTimeMs;
                        await page.WaitForTimeoutAsync(triggerTime);

                        if (!featureContext.ContainsKey("PartOrProductPartitionKey"))
                        {
                            errorMessage = $"PartOrProductPartitionKey not found in feature context for '{tabName}' tab";

                            ValidationHelper.Fail(errorMessage);
                        }

                        partitionKey = featureContext["PartOrProductPartitionKey"].ToString();
                        break;

                    case BomConstants.BOMPart:
                        // Taking more than 11 min to load data in BestBOM Container
                        triggerTime = BomConstants.PartBestBOMPoolTriggerTimeMs;
                        await page.WaitForTimeoutAsync(triggerTime);

                        if (!featureContext.ContainsKey("PartOrProductPartitionKey"))
                        {
                            errorMessage = $"PartOrProductPartitionKey not found in feature context for '{tabName}' tab";

                            ValidationHelper.Fail(errorMessage);
                        }

                        partitionKey = featureContext["PartOrProductPartitionKey"].ToString();
                        break;

                    case BomConstants.BOMAsset:
                        // Taking more than 10 min to load data in BestBOM Container
                        triggerTime = BomConstants.AssetBestBOMPoolTriggerTimeMs;
                        await page.WaitForTimeoutAsync(triggerTime);

                        if (!scenarioContext.ContainsKey("PartitionKey"))
                        {
                            errorMessage = $"PartitionKey not found in scenario context for '{tabName}' tab";

                            ValidationHelper.Fail(errorMessage);
                        }

                        partitionKey = scenarioContext["PartitionKey"] as string;
                        break;

                    default:
                        ValidationHelper.Fail($"Unsupported tab name: {tabName}");
                        break;
                }

                ValidationHelper.FailIfNullOrEmpty("partitionKey", partitionKey, $"is null or empty for '{tabName}' tab");

                var bestBomQuery = CosmosDBQueries.GetDataUsingPartitionKey(partitionKey);
                var bestBomData = await cosmosDb.GetDataFromCosmosDB<BestBomPoolRecord>(database, container, bestBomQuery);

                ValidationHelper.FailIfNullOrEmpty("bestBomData", bestBomData, $"No BestBOM data found for partition key '{partitionKey}' in database '{database}' container '{container}'");

                var eventHistoryList = bestBomData?.EventHistories ?? Enumerable.Empty<EventHistory>();

                foreach (var eventHistory in eventHistories.Rows)
                {
                    if (!BomConstants.EventHistory.ContainsKey(eventHistory[0]))
                    {
                        errorMessage = $"Event history key '{eventHistory[0]}' not found in BOMConstants.EventHistory";

                        ValidationHelper.Fail(errorMessage);
                    }

                    eventName = BomConstants.EventHistory[eventHistory[0]];

                    switch (eventName)
                    {
                        case var name when name.Contains(BomConstants.EventHistoryPlanningBOM):
                            ValidationHelper.FailIfNullOrEmpty("bestBomData.IdhReferences.PlanningBom", bestBomData.IdhReferences?.PlanningBom, $"PlanningBom reference is null in BestBOM data");

                            featureContext[BomConstants.PlanningBOMId] = bestBomData.IdhReferences.PlanningBom.Id.ToString();
                            featureContext[BomConstants.PlanningBOMPartitionKey] = bestBomData.IdhReferences.PlanningBom.PartitionKey;
                            break;

                        case var name when name.Contains(BomConstants.EventHistoryActualBOM):
                            ValidationHelper.FailIfNullOrEmpty("bestBomData.IdhReferences.ActualBom", bestBomData.IdhReferences?.ActualBom, $"ActualBom reference is null in BestBOM data");

                            featureContext[BomConstants.ActualBOMId] = bestBomData.IdhReferences.ActualBom.Id.ToString();
                            featureContext[BomConstants.ActualBOMPartitionKey] = bestBomData.IdhReferences.ActualBom.PartitionKey;
                            break;

                        default:
                            ValidationHelper.Fail($"Event history '{eventName}' is not updated in the DB");
                            break;
                    }

                    var found = eventHistoryList.Any(e => e.Name == eventName);
                    if (!found)
                    {
                        ValidationHelper.Fail($"Expected event '{eventName}' was not found in Event History");
                    }
                }
            }
            catch (Exception ex)
            {
                var errorMessage = $"Event History: '{eventName}' not updated in {database}>>{container} for {tabName} tab after waiting Trigger time {triggerTime / 60000} minutes due to error: {ex.Message}";

                ValidationHelper.Fail(errorMessage, ex);
            }
        }

        [Then(@"Verify Part BOMLine grid row ""(.*)"" displayed in ""(.*)"" container of the ""(.*)"" database for ""(.*)"" tab")]
        public async Task ThenVerifyPartBomLineGridRowDisplayedInContainerOfTheDatabaseForTabThrough(int rowNumber, string container, string database, string tabName, Table fieldValuesToCheck)
        {
            try
            {
                testCaseName = scenarioContext.ScenarioInfo.Title;
                var partitionKey = string.Empty;
                var errorMessage = string.Empty;

                switch (tabName)
                {
                    case BomConstants.BOMPart or BomConstants.BOMProduct:
                        if (!featureContext.ContainsKey("PartOrProductPartitionKey"))
                        {
                            errorMessage = $"PartOrProductPartitionKey not found in feature context for '{tabName}' tab";

                            ValidationHelper.Fail(errorMessage);
                        }

                        partitionKey = featureContext["PartOrProductPartitionKey"].ToString();
                        break;

                    case BomConstants.BOMAsset:
                        if (!scenarioContext.ContainsKey("PartitionKey"))
                        {
                            errorMessage = $"PartitionKey not found in scenario context for '{tabName}' tab";

                            ValidationHelper.Fail(errorMessage);
                        }

                        partitionKey = scenarioContext["PartitionKey"] as string;
                        break;

                    default:
                        ValidationHelper.Fail($"Unsupported tab name: {tabName}");
                        break;
                }

                ValidationHelper.FailIfNullOrEmpty("partitionKey", partitionKey, $"is null or empty for '{tabName}' tab");

                var bestBomQuery = CosmosDBQueries.GetDataUsingPartitionKey(partitionKey);
                var bestBomData = await cosmosDb.GetDataFromCosmosDB<BestBomPoolRecord>(database, container, bestBomQuery);

                ValidationHelper.FailIfNullOrEmpty("bestBomData", bestBomData, $"No BestBOM data found for partition key '{partitionKey}' in database '{database}' container '{container}'");

                var bomLineGridValues = new Dictionary<string, string>();
                foreach (var bomLineField in fieldValuesToCheck.Rows)
                {
                    var columnName = bomLineField[0];
                    var menuLocator = columnName.ToLower();
                    if (menuLocator == BomConstants.LocatorProperties.BOMObsolete)
                    {
                        var value = (await bomPage.GetBomLineTableValue(menuLocator, rowNumber).TextContentAsync()).Trim();
                        var obsoleteStatus = value == "Y" ? "yes" : "no";
                        bomLineGridValues[columnName] = obsoleteStatus;
                    }
                    else
                    {
                        bomLineGridValues[columnName] = (await bomPage.GetBomLineTableValue(menuLocator, rowNumber).TextContentAsync()).Trim();
                    }
                }

                var itemIdFromUi = (await bomPage.GetBomLineTableValue(BomConstants.BomLineGridFields.ItemId, rowNumber).TextContentAsync()).Trim();
                var bestBomPoolBomLine = bestBomData.BomLines
                .FirstOrDefault(b => b.Item.Sku.Equals(itemIdFromUi, StringComparison.OrdinalIgnoreCase));

                ValidationHelper.FailIfNullOrEmpty("bestBomPoolBomLine", bestBomPoolBomLine, $"No matching DB BOM line found for SKU: '{itemIdFromUi}'");

                var dbBomLineGridValues = new Dictionary<string, string>
                {
                    { BomConstants.BomLineGridFields.ItemId, bestBomPoolBomLine.Item.Sku },
                    { BomConstants.BomLineGridFields.Oem, bestBomPoolBomLine.Oem.Name },
                    { BomConstants.BomLineGridFields.Obsolete, bestBomPoolBomLine.Obsolete }
                };

                foreach (var field in bomLineGridValues.Keys)
                {
                    if (!bomLineGridValues.ContainsKey(field))
                    {
                        errorMessage = $"UI does not contain expected field: {field}";

                        ValidationHelper.Fail(errorMessage);
                    }

                    ValidationHelper.AssertEqual("BOM Line Grid Value", bomLineGridValues[field], dbBomLineGridValues[field], $"Mismatch for field '{field}': Expected '{bomLineGridValues[field]}' but found '{dbBomLineGridValues[field]}'");
                }
            }
            catch (Exception ex)
            {
                var errorMessage = $"Part BOM lines are not updated in {database}>>{container} container due to error: {ex.Message}";

                ValidationHelper.Fail(errorMessage, ex);
            }
        }

        [Then(@"User verifies BOMLine grid row ""(.*)"" quantity in ""(.*)"" database and ""(.*)"" container equals total from three tabs")]
        public async Task ThenUserVerifiesBomRowQuantityInDatabaseAndContainerEqualsTotalFromThreeTabs(int rowNumber, string database, string container)
        {
            double totalBomLineQuantity = 0;
            double bestBomPoolQuantity = 0;
            try
            {
                var partitionKey = string.Empty;
                var errorMessage = string.Empty;
                totalBomLineQuantity = Convert.ToDouble(scenarioContext["AssetQuantity"]) + Convert.ToDouble(scenarioContext["PartQuantity"]) + Convert.ToDouble(scenarioContext["ProductQuantity"]);

                if (!featureContext.ContainsKey("PartOrProductPartitionKey"))
                {
                    errorMessage = $"PartOrProductPartitionKey not found in feature context";
                    ValidationHelper.Fail(errorMessage);
                }

                partitionKey = featureContext["PartOrProductPartitionKey"].ToString();
                var bestBomQuery = CosmosDBQueries.GetDataUsingPartitionKey(partitionKey);
                var bestBomData = await cosmosDb.GetDataFromCosmosDB<BestBomPoolRecord>(database, container, bestBomQuery);

                ValidationHelper.FailIfNullOrEmpty("bestBomData", bestBomData, $"No BestBOM data found for partition key '{partitionKey}' in database '{database}' container '{container}'");

                var itemIdFromUi = (await bomPage.GetBomLineTableValue(BomConstants.BomLineGridFields.ItemId, rowNumber).TextContentAsync()).Trim();
                var bestBomPoolBomLine = bestBomData.BomLines
                .FirstOrDefault(b => b.Item.Sku.Equals(itemIdFromUi, StringComparison.OrdinalIgnoreCase));

                ValidationHelper.FailIfNullOrEmpty("bestBomPoolBomLine", bestBomPoolBomLine, $"No matching DB BOM line found for SKU: '{itemIdFromUi}'");

                bestBomPoolQuantity = bestBomPoolBomLine.Quantity;
                ValidationHelper.AssertEqual("QuantityMismatch", totalBomLineQuantity, bestBomPoolQuantity, $"Expected Quantity: '{totalBomLineQuantity}' but found: '{bestBomPoolQuantity}'");
            }
            catch (Exception ex)
            {
                ValidationHelper.AssertEqual("QuantityMismatch", totalBomLineQuantity, bestBomPoolQuantity, $"Expected Quantity: '{totalBomLineQuantity}' but found: '{bestBomPoolQuantity}'");
            }
        }

        [Then(@"User verifies the Aggregated BOMLine grid row ""(.*)"" quantity for replaced part BOMs in ""(.*)"" database and ""(.*)"" container")]
        public async Task ThenUserVerifiesTheAggregatedBOMLineGridRowQuantityForReplacedPartBOMsInDatabaseAndContainer(int rowNumber, string database, string container)
        {
            double totalBomLineQuantity = 0;
            double bestBomPoolQuantity = 0;
            try
            {
                var partitionKey = string.Empty;
                var errorMessage = string.Empty;
                totalBomLineQuantity = quantityList.Sum();
                if (!featureContext.ContainsKey("PartOrProductPartitionKey"))
                {
                    errorMessage = $"PartOrProductPartitionKey not found in feature context";
                    ValidationHelper.Fail(errorMessage);
                }

                partitionKey = featureContext["PartOrProductPartitionKey"].ToString();
                var bestBomQuery = CosmosDBQueries.GetDataUsingPartitionKey(partitionKey);
                var bestBomData = await cosmosDb.GetDataFromCosmosDB<BestBomPoolRecord>(database, container, bestBomQuery);

                ValidationHelper.FailIfNullOrEmpty("bestBomData", bestBomData, $"No BestBOM data found for partition key '{partitionKey}' in database '{database}' container '{container}'");

                var itemIdFromUi = (await bomPage.GetBomLineTableValue(BomConstants.BomLineGridFields.ItemId, rowNumber).TextContentAsync()).Trim();
                var bestBomPoolBomLine = bestBomData.BomLines
                .FirstOrDefault(b => b.Item.Sku.Equals(itemIdFromUi, StringComparison.OrdinalIgnoreCase));

                ValidationHelper.FailIfNullOrEmpty("bestBomPoolBomLine", bestBomPoolBomLine, $"No matching DB BOM line found for SKU: '{itemIdFromUi}'");

                bestBomPoolQuantity = bestBomPoolBomLine.Quantity;
                if (Math.Abs(totalBomLineQuantity - bestBomPoolQuantity) > ValidationConstants.QuantityTolerance)
                {
                    ValidationHelper.AssertEqual("QuantityMismatch", totalBomLineQuantity, bestBomPoolQuantity, $"Expected Quantity: '{totalBomLineQuantity}' but found: '{bestBomPoolQuantity}'");
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.AssertEqual("QuantityMismatch", totalBomLineQuantity, bestBomPoolQuantity, $"Expected Quantity: '{totalBomLineQuantity}' but found: '{bestBomPoolQuantity}'");
            }
        }
    }
}
using PIMQAAutomation.Helpers.DocumentFetcher;
using PIMQAAutomation.Modules.Bom.Constants;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Utilities;
using PPT.ItemMaster.Data.Cosmos.Features.BestBomPools;
using TechTalk.SpecFlow;

namespace PIMQAAutomation.Modules.Bom.Steps
{
    public partial class BOM
    {
        [Then(@"Verify Actual and Planning BOM documents are saved in ""(.*)"" database using ""(.*)"" container")]
        public async Task ThenVerifyActualAndPlanningBomDocumentsAreSavedInDatabaseUsingContainer(string dataBase, string containerName)
        {
            try
            {
                if (!IsIdhValidationEnabled())
                {
                    return;
                }
                var partitionKey = featureContext["BestBOMPartitionKey"] as string;
                var context = new ValidationContext(partitionKey, BomConstants.ActualBom);
                var idhActualBomData = await DocumentFetcherFactory.FetchDocumentAsync<PPT.Integration.CommonDataModel.Models.Boms.Bom>(context);
                ValidationHelper.FailIfNullOrEmpty("idhActualBomData", idhActualBomData, $"No actual BOM data found for Partitionkey '{partitionKey}' in database '{dataBase}' container '{containerName}'");
                var actualBomName = idhActualBomData.Name;
                scenarioContext["actualBOMName"] = actualBomName;
                ValidationHelper.FailIfNullOrEmpty("actualBomName", actualBomName, $"ActualBOM Name is empty or null: {actualBomName}");
                context = new ValidationContext(partitionKey, BomConstants.PlanningBom);
                var idhPlanningBomData = await DocumentFetcherFactory.FetchDocumentAsync<PPT.Integration.CommonDataModel.Models.Boms.Bom>(context);
                ValidationHelper.FailIfNullOrEmpty("idhPlanningBomData", idhPlanningBomData, $"No planning BOM data found for and PartitionKey {partitionKey}' in database '{dataBase}' container '{containerName}'");
                var planningBomName = idhPlanningBomData.Name;
                scenarioContext["planningBOMName"] = planningBomName;
                ValidationHelper.FailIfNullOrEmpty("planningBomName", planningBomName, $"PlanningBOM Name is empty or null: {planningBomName}");
            }
            catch (Exception ex)
            {
                var errorMessage = $"Actual and Planning BOM documents are not displayed in {dataBase}>>{containerName} container with error: {ex.Message}";
                ValidationHelper.Fail(errorMessage, ex);
            }
        }

        [Then(@"Verify Planning BOM document is saved in ""(.*)"" database using ""(.*)"" container")]
        public async Task ThenVerifyPlanningBomDocumentIsSavedInDatabaseUsingContainer(string dataBase, string containerName)
        {
            try
            {
                if (!IsIdhValidationEnabled())
                {
                    return;
                }
                var partitionKey = featureContext["BestBOMPartitionKey"] as string;
                var context = new ValidationContext(partitionKey, BomConstants.PlanningBom);
                var idhPlanningBomData = await DocumentFetcherFactory.FetchDocumentAsync<PPT.Integration.CommonDataModel.Models.Boms.Bom>(context);
                ValidationHelper.FailIfNullOrEmpty("idhPlanningBomData", idhPlanningBomData, $"No planning BOM data found for PartitionKey {partitionKey} in database '{dataBase}' container '{containerName}'");
                var planningBomName = idhPlanningBomData.Name;
                scenarioContext["planningBOMName"] = planningBomName;
                ValidationHelper.FailIfNullOrEmpty("planningBomName", planningBomName, $"PlanningBOM Name is empty or null: {planningBomName}");
            }
            catch (Exception ex)
            {
                var errorMessage = $"Planning BOM document is not displayed in {dataBase}>>{containerName} container with error: {ex.Message}";
                ValidationHelper.Fail(errorMessage, ex);
            }
        }

        [Then(@"Verify ""(.*)"" data in ""(.*)"" using ""(.*)"" container")]
        public async Task ThenVerifyPlanningBomDataInUsingContainer(string actualOrPlanningBom, string dataBase, string containerName)
        {
            try
            {
                if (!IsIdhValidationEnabled())
                {
                    return;
                }

                var partitionKey = featureContext["BestBOMPartitionKey"] as string;
                var context = new ValidationContext(partitionKey, actualOrPlanningBom);
                ValidationHelper.FailIfNullOrEmpty("PartitionKey", partitionKey, "is null or empty in scenario context");
                var idhBomActualOrPlanningBomData = await DocumentFetcherFactory.FetchDocumentAsync<PPT.Integration.CommonDataModel.Models.Boms.Bom>(context);
                ValidationHelper.FailIfNullOrEmpty("idhBomActualOrPlanningBomData", idhBomActualOrPlanningBomData, $"No {actualOrPlanningBom} BOM data found for partition key '{partitionKey}' in database '{dataBase}' container '{containerName}'");

                if (idhBomActualOrPlanningBomData.Asset?.Item?.Oem == null)
                {
                    var errorMessage = $"Asset or Item or OEM data is null in {actualOrPlanningBom} BOM data";
                    ValidationHelper.Fail(errorMessage);
                }
            }
            catch (Exception ex)
            {
                var errorMessage = $"{actualOrPlanningBom} data is not displayed in {dataBase}>>{containerName} container with error: {ex.Message}";
                ValidationHelper.Fail(errorMessage, ex);
            }
        }

        [Then(@"Verify ID and Partition Key generating from PIM match with ID and Partition Key in ""(.*)"" database in ""(.*)"" container")]
        public async Task ThenVerifyIdAndPartitionKeyGeneratingFromPimMatchWithIdAndPartitionKeyInDatabaseInContainer(string dataBase, string container)
        {
            try
            {
                if (!IsIdhValidationEnabled())
                {
                    return;
                }
                if (!scenarioContext.ContainsKey("PartitionKey"))
                {
                    var errorMessage = $"PartitionKey not found in scenario context for verifying ID and Partition Key in database '{dataBase}' container '{container}'";
                    ValidationHelper.Fail(errorMessage);
                }
                var bomPartitionKey = featureContext["BestBOMPartitionKey"] as string;
                var context = new ValidationContext(bomPartitionKey, BomConstants.PlanningBom);
                var partitionKey = scenarioContext["PartitionKey"] as string;

                var dbBomData = await DocumentFetcherFactory.FetchDocumentAsync<PPT.Integration.CommonDataModel.Models.Boms.Bom>(context);
                ValidationHelper.FailIfNullOrEmpty("dbBomData", dbBomData, $"No BOM data found for partition key '{bomPartitionKey}' in database '{dataBase}' container '{container}'");

                if (dbBomData.References == null || !dbBomData.References.Any())
                {
                    var errorMessage = $"No references found in BOM data for partition key '{bomPartitionKey}'";
                    ValidationHelper.Fail(errorMessage);
                }

                var idhBomPartitionKey = dbBomData.References.ToList()[0].Extension["partitionKey"].ToString();
                ValidationHelper.AssertEqual("Partition Key", partitionKey, idhBomPartitionKey, $"Partition Key mismatch. PIM Partition Key: '{partitionKey}', IDH Partition Key: '{idhBomPartitionKey}'");
            }
            catch (Exception ex)
            {
                var errorMessage = $"Partition Key in IDH>BOMS are not matched with PIM>BOMs with error: {ex.Message}";
                ValidationHelper.Fail(errorMessage, ex);
            }
        }

        [Then(@"Verify ID and Partition Key generating from PIM BestBOMPool match with ID and Partition Key in ""(.*)"" database in ""(.*)"" container")]
        public async Task ThenVerifyIdAndPartitionKeyGeneratingFromPimBestBomPoolMatchWithIdAndPartitionKeyInDatabaseInContainer(string dataBase, string container)
        {
            try
            {
                if (!IsIdhValidationEnabled())
                {
                    return;
                }
                if (!featureContext.ContainsKey("PartOrProductPartitionKey"))
                {
                    var errorMessage = $"PartOrProductPartitionKey not found in feature context for verifying ID and Partition Key in database '{dataBase}' container '{container}'";
                    ValidationHelper.Fail(errorMessage);
                }

                var partitionKeyPimBestBomPool = featureContext["PartOrProductPartitionKey"].ToString();

                ValidationHelper.FailIfNullOrEmpty("PartOrProductPartitionKey", partitionKeyPimBestBomPool, "is null or empty in feature context");
                var bomPartitionKey = featureContext["BestBOMPartitionKey"] as string;
                var context = new ValidationContext(bomPartitionKey, BomConstants.PlanningBom);
                var dbBomData = await DocumentFetcherFactory.FetchDocumentAsync<PPT.Integration.CommonDataModel.Models.Boms.Bom>(context);
                ValidationHelper.FailIfNullOrEmpty("dbBomData", dbBomData, $"No BOM data found for partition key '{bomPartitionKey}' in database '{dataBase}' container '{container}'");

                if (dbBomData.References == null || !dbBomData.References.Any())
                {
                    var errorMessage = $"No references found in BOM data for partition key '{bomPartitionKey}'";
                    ValidationHelper.Fail(errorMessage);
                }

                var idhBomPartitionKey = dbBomData.References.ToList()[0].Extension["partitionKey"].ToString();
                ValidationHelper.AssertEqual("Partition Key", partitionKeyPimBestBomPool, idhBomPartitionKey, $"Partition Key mismatch. PIM BestBOMPool Partition Key: '{partitionKeyPimBestBomPool}', IDH Partition Key: '{idhBomPartitionKey}'");
            }
            catch (Exception ex)
            {
                var errorMessage = $"Partition Key in IDH>BOMs are not matched with PIM>BestBOMPools with error: {ex.Message}";
                ValidationHelper.Fail(errorMessage, ex);
            }
        }

        [Then(@"Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as ""(.*)"",BOMLines grid row ""(.*)"" of Sku,Oem and Quantity in ""(.*)"" record with ""(.*)"" database of ""(.*)"" container match with ""(.*)"" database of ""(.*)"" container for ""(.*)"" tab")]
        public async Task ThenVerifyAssetSerialNumberBomHeaderSkuAndOemBomtypeAsBomLinesGridRowOfSkuOemAndQuantityInDatabaseOfContainerMatchWithDatabaseOfContainer(int bomType, int rowNumber, string actualOrPlanningBom, string idhDatabase, string boMsContainer, string pimDatabase, string bestBomContainer, string tabName)
        {
            try
            {
                if (!IsIdhValidationEnabled())
                {
                    return;
                }
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

                        partitionKey = scenarioContext["PartitionKey"].ToString();
                        break;

                    default:
                        errorMessage = $"Unsupported tab name: {tabName}";

                        ValidationHelper.Fail(errorMessage);
                        break;
                }

                ValidationHelper.FailIfNullOrEmpty("partitionKey", partitionKey, $"is null or empty for '{tabName}' tab");

                var bestBomQuery = CosmosDBQueries.GetDataUsingPartitionKey(partitionKey);

                var bestBomData = await cosmosDb.GetDataFromCosmosDB<BestBomPoolRecord>(pimDatabase, bestBomContainer, bestBomQuery);
                var bomPartitionKey = featureContext["BestBOMPartitionKey"] as string;
                var context = new ValidationContext(bomPartitionKey, actualOrPlanningBom);
                var idhBomData = await DocumentFetcherFactory.FetchDocumentAsync<PPT.Integration.CommonDataModel.Models.Boms.Bom>(context);
                ValidationHelper.FailIfNullOrEmpty("bestBomData", bestBomData, $"No BestBOM data found for partition key '{partitionKey}' in database '{pimDatabase}' container '{bestBomContainer}'");

                ValidationHelper.FailIfNullOrEmpty("idhBomData", idhBomData, $"No IDH BOM data found for partition key '{bomPartitionKey}' in database '{idhDatabase}' container '{boMsContainer}'");

                if (idhBomData.Asset == null)
                {
                    errorMessage = $"Asset is null in IDH BOM data";
                    ValidationHelper.Fail(errorMessage);
                }

                ValidationHelper.AssertEqual("Serial Number", bestBomData.Asset.SerialNumber, idhBomData.Asset.SerialNumber, $"Serial Number mismatch. BestBOM: '{bestBomData.Asset.SerialNumber}', IDH: '{idhBomData.Asset.SerialNumber}'");

                ValidationHelper.AssertEqual("OEM Name", bestBomData.Oem.Name, idhBomData.Asset.Item.Oem.Name, $"OEM name mismatch. BestBOM: '{bestBomData.Oem.Name}', IDH: '{idhBomData.Asset.Item.Oem.Name}'");

                ValidationHelper.AssertEqual("Item SKU", bestBomData.Item.Sku, idhBomData.Asset.Item.Sku, $"Item SKU mismatch. BestBOM: '{bestBomData.Item.Sku}', IDH: '{idhBomData.Asset.Item.Sku}'");

                var idhBomType = (int)idhBomData.BomType;
                ValidationHelper.AssertEqual("BOM Type", bomType, idhBomType, $"BOM Type mismatch. Expected: {bomType}, Actual: {idhBomType}");

                var itemIdFromUi = (await bomPage.GetBomLineTableValue(BomConstants.BomLineGridFields.ItemId, rowNumber).TextContentAsync()).Trim();
                var bestBomPoolBomLine = bestBomData.BomLines.FirstOrDefault(b => b.Item.Sku.Equals(itemIdFromUi));

                ValidationHelper.FailIfNullOrEmpty("bestBomPoolBomLine", bestBomPoolBomLine, $"No matching BestBOM BOM line found for SKU: '{itemIdFromUi}'");

                var idhBomLine = idhBomData.BomLines.FirstOrDefault(b => b.Item.Sku.Equals(itemIdFromUi));

                ValidationHelper.FailIfNullOrEmpty("idhBomLine", idhBomLine, $"No matching IDH BOM line found for SKU: '{itemIdFromUi}'");

                ValidationHelper.AssertEqual("BOM Line OEM Name", bestBomPoolBomLine.Oem.Name, idhBomLine.Item.Oem.Name, $"BOM Line OEM name mismatch. BestBOM: '{bestBomPoolBomLine.Oem.Name}', IDH: '{idhBomLine.Item.Oem.Name}'");

                ValidationHelper.AssertEqual("BOM Line Item SKU", bestBomPoolBomLine.Item.Sku, idhBomLine.Item.Sku, $"BOM Line Item SKU mismatch. BestBOM: '{bestBomPoolBomLine.Item.Sku}', IDH: '{idhBomLine.Item.Sku}'");

                ValidationHelper.AssertEqual("BOM Line Quantity", bestBomPoolBomLine.Quantity, idhBomLine.Quantity, $"BOM Line Quantity mismatch. BestBOM: '{bestBomPoolBomLine.Quantity}', IDH: '{idhBomLine.Quantity}'");
            }
            catch (Exception ex)
            {
                var errorMessage = $"In {idhDatabase} database of {boMsContainer} container data is not displayed due to an error: {ex.Message}";

                ValidationHelper.Fail(errorMessage, ex);
            }
        }
    }
}
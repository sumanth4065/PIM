using Microsoft.Playwright;
using NUnit.Framework;
using OfficeOpenXml;
using PIMQAAutomation.Common.Pages;
using PIMQAAutomation.Modules.Alternates.Constants;
using PIMQAAutomation.Modules.Bom.Constants;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Modules.Items.Constants;
using PIMQAAutomation.Utilities;
using PPT.ItemMaster.Data.Cosmos.Features.BOMs;
using System.Globalization;
using System.Text;
using TechTalk.SpecFlow;

namespace PIMQAAutomation.Modules.Common.Steps
{
    [Binding]
    public class ImportSteDef
    {
        private readonly CommonLocators commonLocators;
        private readonly ScenarioContext scenarioContext;
        private FeatureContext featureContext;
        public IPage page;
        private CosmosDbHelper cosmosDb;
        private ImportsPage importsPage;
        private string testCaseName = string.Empty;

        public ImportSteDef(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            page = featureContext["Page"] as IPage;
            this.featureContext = featureContext;
            this.scenarioContext = scenarioContext;
            cosmosDb = new CosmosDbHelper();
            importsPage = new ImportsPage(page);
            commonLocators = new CommonLocators(page);
            importsPage = new ImportsPage(page);
        }

        [Then("User is able to view the Import screen")]
        public async Task ThenUserAbleToViewNavigateToImportScreen()
        {
            await importsPage.GetImportScreenHeader().IsVisibleAsync();
        }

        [Then(@"User selects import type as ""(.*)"" from dropdown")]
        public async Task ThenUserSelectImportTypeAsAsset_BOMFromDropdown(string dropDownSelectValue)
        {
            if (await importsPage.GetCrossButton().IsVisibleAsync())
            {
                await importsPage.GetCrossButton().ClickAsync();
            }
            await importsPage.GetSelectImportTypeDropdown().ClickAsync();
            await BasePage.TypeTextAsync(importsPage.GetSelectImportTypeDropdown(), dropDownSelectValue);
            await commonLocators.GetSelectDropdownOption(dropDownSelectValue).ClickAsync();
        }

        [Then(@"User clicks on ""(.*)"" button")]
        public async Task ThenUserClicksOnBrowseButton(string buttonName)
        {
            await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);
            bool status = await importsPage.GetButtonInImportPage(buttonName.ToLower()).IsEnabledAsync();
            if (status = true) { await importsPage.GetButtonInImportPage(buttonName.ToLower()).ClickAsync(); }
            await page.WaitForLoadStateAsync();
        }

        [Then("User clicks on ok popup")]
        public async Task ThenUserClicksOnOkPopup()
        {
            await commonLocators.GetPopupOkButton().ClickAsync();
        }

        [Then(@"Verify File ""(.*)"" and ""(.*)"" is displayed on import screen")]
        public async Task ThenUserViewsFileNameAndSizeIsDisplayedOnImportScreen(string fileName, string fileSize)
        {
            await page.WaitForLoadStateAsync();
            await importsPage.GetFileNameAndType(fileName).IsVisibleAsync();
            await importsPage.GetFileNameAndType(fileSize).IsVisibleAsync();
            await Task.Delay(TimeConstants.MailTriggerTimeMs);// mail trigger
        }

        [Then("User uploads the Excel sheet Header")]
        public async Task ThenUserUploadsTheExcelSheetHeader()
        {
            try
            {
                await importsPage.GetInputFile().SetInputFilesAsync(Utility.ImportFilePath);
            }
            catch (Exception e)
            {
                Assert.Fail($"Flle is not uploaded due to the error {e.Message}");
            }
        }

        [Then(@"User verifies the Excel sheet headers from the ""(.*)"" Excel file for the ""(.*)"" tab")]
        public async Task ThenUserVerifiesTheExcelSheetHeadersFromTheExcelFileForTheTab(string fileName, string tabName)
        {
            List<string> expectedHeaders = tabName.ToLower() switch
            {
                "asset" => BomImportHeaders.AssetExpectedHeaders,
                "product" => BomImportHeaders.ProductExpectedHeaders,
                "part" => BomImportHeaders.PartExpectedHeaders,
                "explicit" => ItemMasterImport.CharacteristicsDataExplicit,
                "baseline" => ItemMasterImport.CharacteristicsDataExplictBaseline,
                "taxonomy" => ItemMasterImport.CharacteristicsDataTaxonomy,
                "explicit-item" => ItemMasterImport.ExplicictItemData,
                "baseline-item" => ItemMasterImport.BaselineItemData,
                "alternate" => AlternatesConstants.AlternateImportData,
                _ => throw new ArgumentException($"Unsupported tab name: {tabName}")
            };
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            List<string> _actualHeaders = Utility.GetExcelHeaders(FileUtil.GetImportExcelFilePath(scenarioContext, fileName)).Where(header => !string.IsNullOrWhiteSpace(header)).ToList();
            bool headersMatch = _actualHeaders.SequenceEqual(expectedHeaders);
            Assert.IsTrue(headersMatch, $"The Excel headers for '{tabName}' do not match the expected headers.");
        }

        [Then(@"User verifies valid data is present in the ""(.*)"" Excel file")]
        public async Task ThenUserVerifiesValidDataIsPresentInTheExcelFile(string fileName)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            Utility.ValidateDataRows(FileUtil.GetImportExcelFilePath(scenarioContext, fileName));
        }

        [Then(@"User uploads the ""(.*)"" Excel file")]
        public async Task ThenUserUploadsTheExcelFile(string fileName)
        {
            var filePath = FileUtil.GetImportExcelFilePath(scenarioContext, fileName);
            scenarioContext["ImportFilePath"] = filePath;
            await importsPage.GetInputFile().SetInputFilesAsync(filePath);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var excelData = Utility.ReadExcelData(fileName);
            await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);
        }

        [Then(@"Verify the BOM Line grid row ""(.*)"" data of ""(.*)"" Excel file matches with Cosmos DB using ""(.*)"" database and ""(.*)"" container")]
        public async Task ThenVerifyTheBOMLineGridRowDataOfExcelFileMatchesWithCosmosDBUsingDatabaseAndContainer(int rowNumber, string filePath, string dataBase, string container)
        {
            try
            {
                testCaseName = scenarioContext.ScenarioInfo.Title;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                var fullFilePath = FileUtil.GetImportExcelFilePath(scenarioContext, filePath);
                var fileName = Path.GetFileName(fullFilePath);
                var excelData = Utility.ReadExcel(fullFilePath);
                var excelRow = excelData[0];
                string bomKind = excelRow[BomConstants.ExcelHeaders.ExcelColumnBOMKind];
                scenarioContext["BOM-Kind"] = bomKind; // for recall
                scenarioContext["BOMKind"] = bomKind;

                BOMRecord dbBomData = null;
                string bomQuery;
                BOMRecord.Component matchingComponent = null;
                if (fileName.IndexOf(BomConstants.BOMAsset, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    var assetPartitionKey = featureContext["PartOrProductPartitionKey"].ToString();
                    bomQuery = CosmosDBQueries.GetPimBomDataUsingPartitionKey(bomKind, assetPartitionKey);
                    dbBomData = await cosmosDb.GetDataFromCosmosDB<BOMRecord>(dataBase.ToLower(), container, bomQuery);

                    scenarioContext["PartitionKey"] = dbBomData.PartitionKey;
                    string serialNo = dbBomData.Filter.Asset.SerialNumber;
                    string assetId = dbBomData.Filter.Asset.Ref.Id.ToString();
                    featureContext["OemId"] = dbBomData.Filter.Oem.Ref.Id;
                    featureContext["FamilyId"] = dbBomData.Filter.Family.Ref.Id;
                    scenarioContext["SerialNumber"] = serialNo;
                    featureContext["AssetOrOemId"] = assetId;
                    var expectedSku = excelRow[BomConstants.ExcelHeaders.ExcelColumnBOMLineItemId];
                    var expectedInstallBase = excelRow[BomConstants.ExcelHeaders.ExcelColumnBOMLineInstallBase];
                    var expectedObsolete = excelRow[BomConstants.ExcelHeaders.ExcelColumnBOMLineObsolete];
                    expectedObsolete = expectedObsolete == "N" ? "False" : expectedObsolete == "Y" ? "True" : expectedObsolete;
                    // Convert expectedInstallBase to int (assuming it's numeric)
                    int.TryParse(expectedInstallBase, out int expectedQuantity);
                    matchingComponent = dbBomData.Components.FirstOrDefault(c =>
                    c.Item != null &&
                    c.Line != null &&
                    c.Item.Sku?.Trim().Equals(expectedSku.Trim(), StringComparison.OrdinalIgnoreCase) == true &&
                    Math.Abs(c.Line.Quantityexpected - Convert.ToDouble(expectedQuantity)) < 0.001); ;
                    Assert.IsNotNull(matchingComponent, $"No matching component found in DB for SKU '{expectedSku}',Install Base '{expectedInstallBase}'");
                    //Validate Obsolete
                    var actualObsolete = matchingComponent.IsObsolete.ToString();
                    Assert.AreEqual(expectedObsolete, actualObsolete,
                        $"Mismatch in Obsolete flag for SKU '{expectedSku}'. Expected: '{expectedObsolete}', Actual: '{actualObsolete}'");
                    
                }
                else if (fileName.IndexOf(BomConstants.BOMPart, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    var oemId = featureContext["OemId"].ToString();
                    scenarioContext["Oem"] = excelRow[BomConstants.ExcelHeaders.ExcelColumnOem];
                    string itemId = excelRow[BomConstants.ExcelHeaders.ExcelColumnItemID];
                    scenarioContext["Sku"] = itemId;
                    string partBomPartitionKey = $"{oemId}|{itemId}";
                    bomQuery = CosmosDBQueries.GetPimBomDataUsingPartitionKey(bomKind, partBomPartitionKey);
                    dbBomData = await cosmosDb.GetDataFromCosmosDB<BOMRecord>(dataBase.ToLower(), container, bomQuery);
                    scenarioContext["PartitionKey"] = dbBomData.PartitionKey;
                    Assert.AreEqual(excelData[0][BomConstants.ExcelHeaders.ExcelColumnBOMLineItemId], dbBomData.Components[rowNumber].Item.Sku, $"BOM Line Item ID mismatch. Expected: {excelData[0][BomConstants.ExcelHeaders.ExcelColumnBOMLineItemId]}, Found: {dbBomData.Components[rowNumber].Item.Sku}");
                    Assert.AreEqual(excelData[0][BomConstants.ExcelHeaders.ExcelColumnBOMLineInstallBase], dbBomData.Components[rowNumber].Line.Quantityexpected.ToString("0", CultureInfo.InvariantCulture), $"BOM Line Install Base mismatch. Expected: {excelData[0][BomConstants.ExcelHeaders.ExcelColumnBOMLineInstallBase]}, Found: {dbBomData.Components[rowNumber].Line.Quantityexpected.ToString("0", CultureInfo.InvariantCulture)}");
                    var expectedSku = excelRow[BomConstants.ExcelHeaders.ExcelColumnBOMLineItemId];
                    var expectedInstallBase = excelRow[BomConstants.ExcelHeaders.ExcelColumnBOMLineInstallBase];
                    var expectedObsolete = excelRow[BomConstants.ExcelHeaders.ExcelColumnBOMLineObsolete];
                    expectedObsolete = expectedObsolete == "N" ? "False" : expectedObsolete == "Y" ? "True" : expectedObsolete;
                    // Convert expectedInstallBase to int (assuming it's numeric)
                    int.TryParse(expectedInstallBase, out int expectedQuantity);
                    matchingComponent = dbBomData.Components.FirstOrDefault(c =>
                     c.Item != null &&
                     c.Line != null &&
                     c.Item.Sku?.Trim().Equals(expectedSku.Trim(), StringComparison.OrdinalIgnoreCase) == true &&
                     Math.Abs(c.Line.Quantityexpected - Convert.ToDouble(expectedQuantity)) < 0.001); ;
                    Assert.IsNotNull(matchingComponent, $"No matching component found in DB for SKU '{expectedSku}',Install Base '{expectedInstallBase}'");
                    //Validate Obsolete
                    var actualObsolete = matchingComponent.IsObsolete.ToString();
                    Assert.AreEqual(expectedObsolete, actualObsolete,
                        $"Mismatch in Obsolete flag for SKU '{expectedSku}'. Expected: '{expectedObsolete}', Actual: '{actualObsolete}'");
                }
                else if (fileName.IndexOf(BomConstants.BOMProduct, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    scenarioContext["Oem"] = excelRow[BomConstants.ExcelHeaders.ExcelColumnOem];
                    scenarioContext["Family"] = excelRow[BomConstants.ExcelHeaders.ExcelColumnFamily];
                    scenarioContext["Line"] = excelRow[BomConstants.ExcelHeaders.ExcelColumnLine];
                    scenarioContext["Group"] = excelRow[BomConstants.ExcelHeaders.ExcelColumnGroup];
                    var familyId = featureContext["FamilyId"].ToString();
                    string productBOMQuery = CosmosDBQueries.GetPimBomDataUsingPartitionKey(bomKind, familyId);
                    dbBomData = await cosmosDb.GetDataFromCosmosDB<BOMRecord>(dataBase.ToLower(), container, productBOMQuery);
                    scenarioContext["ProductBOMQuery"] = productBOMQuery;
                    var expectedSku = excelRow[BomConstants.ExcelHeaders.ExcelColumnProductBOMLineItemId].ToString();
                    if (expectedSku == null)
                    {
                        throw new Exception($"Missing SKU for row: {excelRow}"); // or handle as needed
                    }
                    var Sku = expectedSku.ToString();
                    var expectedInstallBase = excelRow[BomConstants.ExcelHeaders.ExcelColumnDefaultInstallBase];
                    var expectedObsolete = excelRow[BomConstants.ExcelHeaders.ExcelColumnBOMLineObsolete];
                    expectedObsolete = expectedObsolete == "N" ? "False" : expectedObsolete == "Y" ? "True" : expectedObsolete;
                    // Convert expectedInstallBase to int (assuming it's numeric)
                    int.TryParse(expectedInstallBase, out int expectedQuantity);
                    matchingComponent = dbBomData.Components.FirstOrDefault(c =>
                    c.Item != null &&
                    c.Line != null &&
                    c.Item.Sku?.Trim().Equals(Sku.Trim(), StringComparison.OrdinalIgnoreCase) == true &&
                    Math.Abs(c.Line.Quantityexpected - Convert.ToDouble(expectedQuantity)) < 0.001);
                    //Validate Obsolete
                    var actualObsolete = matchingComponent.IsObsolete.ToString();
                    Assert.AreEqual(expectedObsolete, actualObsolete,
                        $"Mismatch in Obsolete flag for SKU '{expectedSku}'. Expected: '{expectedObsolete}', Actual: '{actualObsolete}'");
                }
                string partPartitionKey = dbBomData.PartitionKey;
                scenarioContext["PartitionKey"] = partPartitionKey;
                var expectedOem = excelRow[BomConstants.ExcelHeaders.ExcelColumnBOMLineOem];
                matchingComponent = dbBomData.Components.FirstOrDefault(c =>
                    c.Item.Oem.Name.Equals(expectedOem, StringComparison.OrdinalIgnoreCase));
                Assert.IsNotNull(matchingComponent,
                    $"No matching component found in DB for SKU '{expectedOem}'");
                // Assert BOM Kind separately
                Assert.AreEqual(excelData[0][BomConstants.ExcelHeaders.ExcelColumnBOMKind], bomKind,
                    $"BOM Kind mismatch. Expected: {excelData[0][BomConstants.ExcelHeaders.ExcelColumnBOMKind]}, Found: {bomKind}");
            }
            catch (Exception e)
            {
                Assert.Fail($"Imported data is not reflected in DB: {e.Message}");
            }
        }

        [Then(@"User retrives the PartitionKey for ""(.*)"" Excel SerialNumber from Cosmos DB using ""(.*)"" database and ""(.*)"" container")]
        public async Task ThenUserRetrivesThePartitionKeyForExcelSerialNumberFromCosmosDBUsingDatabaseAndContainer(string filePath, string database, string container)
        {
            testCaseName = scenarioContext.ScenarioInfo.Title;
            var partitionKey = Utilities.Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.PartitionKey");
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var excelData = Utility.ReadExcel(FileUtil.GetImportExcelFilePath(scenarioContext, filePath));
            var excelRow = excelData[0];
            string BOMKind = excelRow[BomConstants.ExcelHeaders.ExcelColumnBOMKind];
            var dbBOMData = await cosmosDb.GetDataFromCosmosDB<BOMRecord>(database.ToLower(), container, CosmosDBQueries.GetPimBomDataUsingPartitionKey(BOMKind, partitionKey));
            featureContext["PartOrProductPartitionKey"] = dbBOMData.PartitionKey;
            featureContext["BOMKind"] = BOMKind;
            featureContext["BestBOMPartitionKey"] = partitionKey;
        }

        [Then(@"User verifies the error message is displayed after successfully importing the ""(.*)"" Excel file")]
        public async Task ThenUserVerifiesTheErrorMessageIsDisplayedAfterSuccessfullyImportingTheExcelFile(string baselineEmptyCharacteristicValue)
        {
            await page.WaitForTimeoutAsync(TimeConstants.ItemOrAlternatesPoolTriggerTimeMs);
            await importsPage.GetStatusViewLink(baselineEmptyCharacteristicValue).ClickAsync();
            await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
            Assert.IsTrue(await importsPage.GetTitlePopup().IsVisibleAsync(), "Popup title not visible");
            // Verify error message is shown
            Assert.IsTrue(await importsPage.GetErrorMessage().IsVisibleAsync(), "Error message not visible");
            // Verify completion status contains COMPLETE
            var completionStatus = await importsPage.GetImportCompletionStatus().TextContentAsync();
            if (!completionStatus.Equals(ImportConstants.ImportStatus))
            {
                await importsPage.GetImportPopupCloseIcon().ClickAsync();
                await importsPage.GetStatusViewLink(baselineEmptyCharacteristicValue).ClickAsync();
                await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
                completionStatus = await importsPage.GetImportCompletionStatus().TextContentAsync();
            }
            Assert.IsTrue(completionStatus.Contains(ImportConstants.ImportStatus), $"Completion status does not contain '{ImportConstants.ImportStatus}'");
            // Verify created date is not null or empty
            var createdDate = await importsPage.GetImportCreatedDate().TextContentAsync();
            Assert.IsFalse(string.IsNullOrWhiteSpace(createdDate), "Created date is null or empty");
            // Verify detailed error message
            var detailedErrorMessage = await importsPage.GetDetailedErrorMessageOfImportFail().TextContentAsync();
            Assert.IsTrue(detailedErrorMessage.Contains(ImportConstants.ImpportFailedMessage), "Expected error message not found");
        }
        [Then(@"Verify the imported file is processed successfully on the import screen")]
        public async Task ThenImportedFileIsProcessedSuccessfullyOnImportScreen()
        {
            try
            {
                var filePath = scenarioContext["ImportFilePath"] as string;
                ValidationHelper.FailIfNullOrEmpty("ImportFilePath", filePath, "File path not found in ScenarioContext");
                var extractedFileName = Path.GetFileName(filePath);
                var result = await WaitForImportToCompleteAsync(extractedFileName);
                ValidationHelper.FailIfNullOrEmpty("ImportResult", result, $"Import result was null for file '{extractedFileName}'");
                if (result.IsSuccessful)
                {
                    Console.WriteLine($"Import of file '{extractedFileName}' was successful in {result.TotalTime.TotalMinutes:F2} minutes.");
                }
                else
                {

                    var sb = new StringBuilder();
                    sb.AppendLine($"Import failed - '{extractedFileName}' or timed out after {result.TotalTime.TotalMinutes:F2} minutes.");

                    if (!string.IsNullOrEmpty(result.ErrorMessages))
                    {
                        sb.Append(" Errors: ");
                        sb.Append(result.ErrorMessages.Replace(Environment.NewLine, " ").Replace(";", " "));
                    }

                    ValidationHelper.Fail(sb.ToString());
                }
            }
            catch (Exception ex)
            {

                var errorMessage = $"Exception occurred during import verification: {ex.Message}";
                ValidationHelper.Fail(errorMessage, ex);
            }
        }
        private async Task<ImportResult> WaitForImportToCompleteAsync(string extractedFileName)
        {
            var result = new ImportResult();
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                await importsPage.GetImportFileViewLink(extractedFileName).ClickAsync();
                TimeSpan staticWaitTime = TimeSpan.FromMinutes(TimeConstants.ImportInitialWaitMinutes);
                TimeSpan pollingInterval = TimeSpan.FromMinutes(TimeConstants.ImportPollingIntervalMinutes);
                TimeSpan maxWaitTime = TimeSpan.FromMinutes(TimeConstants.ImportMaxLimitMinutes);
                Console.WriteLine($"Waiting {staticWaitTime.TotalMinutes} minutes before polling...");
                await Task.Delay(staticWaitTime);

                string status = string.Empty;
                while (stopwatch.Elapsed < maxWaitTime)
                {
                    await importsPage.GetImportRefreshButton().ClickAsync();
                    await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());

                    status = await importsPage.GetImportCompletionStatus().InnerTextAsync();
                    Console.WriteLine($"[{DateTime.Now}] Import status: {status}");

                    if (status.Contains("COMPLETE", StringComparison.OrdinalIgnoreCase))
                        break;

                    await Task.Delay(pollingInterval);
                }

                if (await importsPage.GetImportSuccessBar().IsVisibleAsync())
                {
                    result.IsSuccessful = true;
                    await importsPage.GetImportCancelButton().ClickAsync();
                    await Task.Delay(pollingInterval);
                }
                else if (await importsPage.GetImportErrorList().IsVisibleAsync())
                {
                    result.ErrorMessages = await importsPage.GetImportErrorList().InnerTextAsync();
                }
                else if (stopwatch.Elapsed >= TimeSpan.FromMinutes(TimeConstants.ImportMaxLimitMinutes))
                {
                    result.ErrorMessages = "Maximum time reached before import could complete.";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception during import: {e.Message}");
                throw;
            }
            finally
            {
                result.TotalTime = stopwatch.Elapsed;
            }

            return result;
        }
        private class ImportResult
        {
            public bool IsSuccessful { get; set; } = false;
            public string ErrorMessages { get; set; } = string.Empty;
            public TimeSpan TotalTime { get; set; } = TimeSpan.Zero;
        }

    }
}
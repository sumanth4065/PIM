using Microsoft.Playwright;
using OfficeOpenXml;
using PIMQAAutomation.Common.Pages;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Modules.Items.Constants;
using PIMQAAutomation.Modules.Items.Pages;
using PIMQAAutomation.Utilities;
using TechTalk.SpecFlow;
namespace PIMQAAutomation.Modules.Items.Actions
{
    

    /// <summary>
    /// Contains business logic and action methods for Excel import/export operations in Item module
    /// </summary>
    public class ItemExcelActions
    {
        private readonly IPage page;
        private readonly ScenarioContext scenarioContext;
        private readonly FeatureContext featureContext;
        private readonly ItemMasterPage itemMasterPage;
        private readonly CommonLocators commonLocators;

        public ItemExcelActions(IPage page, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            this.page = page;
            this.scenarioContext = scenarioContext;
            this.featureContext = featureContext;
            itemMasterPage = new ItemMasterPage(page);
            commonLocators = new CommonLocators(page);
        }

        public async Task RecallFieldsFromExcelAsync(string fileName, Table table)
        {
            foreach (var row in table.Rows)
            {
                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                var excelData = Utility.ReadExcel(FileUtil.GetImportExcelFilePath(scenarioContext, fileName));

                var itemId = excelData[0]["Item ID"];
                var characteristicKind = excelData[0]["Characteristic Kind"];
                var characteristicType = excelData[0]["Characteristic Type"];

                featureContext["characteristicKind"] = characteristicKind;
                featureContext["characteristicType"] = characteristicType;
                featureContext["itemIdValue"] = itemId;

                await FillAndSelectFieldAsync(row[0].ToLower(), itemId);
                await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
            }
        }

        public async Task RecallTaxonomyFieldsFromExcelAsync(string fileName, Table table)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var excelData = Utility.ReadExcel(FileUtil.GetImportExcelFilePath(scenarioContext, fileName));
            var data = excelData[0];

            featureContext["characteristicKind"] = data["Characteristic Kind"];
            featureContext["characteristicType"] = data["Characteristic Type"];

            foreach (var row in table.Rows)
            {
                var fieldName = row[0];

                if (data.TryGetValue(fieldName, out var fieldValue) && !string.IsNullOrWhiteSpace(fieldValue))
                {
                    await FillTaxonomyFieldFromExcelAsync(fieldName, fieldValue);
                }
                else
                {
                    Console.WriteLine($"Skipping empty or missing field '{fieldName}' from Excel.");
                }
            }

            await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader(), TimeConstants.LowWaitTimeMs);
        }

        public async Task RecallItemIdFromExcelAsync(string fieldName, string fileName)
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            var excelData = Utility.ReadExcel(FileUtil.GetImportExcelFilePath(scenarioContext, fileName));
            var itemId = excelData[0]["Item ID"];

            featureContext["itemIdValue"] = itemId;

            await itemMasterPage.GetDropdownInputField(fieldName.ToLower()).ClickAsync();
            await BasePage.TypeTextAsync(itemMasterPage.GetDropdownInputField(fieldName.ToLower()), itemId);
            await page.WaitForLoadStateAsync();
            await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
            await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);

            var isDropdownOpen = await commonLocators.GetDropdownListBox().IsVisibleAsync();
            if (isDropdownOpen)
            {
                await commonLocators.GetSelectDropdownOption(itemId).ClickAsync();
            }
        }

        public async Task RecallFieldsFromExcelForExplicitItemAsync(string fileName, Table table)
        {
            foreach (var row in table.Rows)
            {
                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                var excelData = Utility.ReadExcel(FileUtil.GetImportExcelFilePath(scenarioContext, fileName));

                string itemId = excelData[0][ItemConstants.ImportExcelHeaders.ItemId];
                string oem = excelData[0][ItemConstants.ImportExcelHeaders.Oem];
                string description = excelData[0][ItemConstants.ImportExcelHeaders.Description];

                scenarioContext["oem"] = oem;
                scenarioContext["description"] = description;
                scenarioContext["itemId"] = itemId;

                await itemMasterPage.GetDropdownInputField(row[0].ToLower()).ClickAsync();
                await BasePage.TypeTextAsync(itemMasterPage.GetDropdownInputField(row[0].ToLower()), itemId);
            }
        }

        public async Task RetrieveFieldValuesFromExcelAsync(string fileName, Table table)
        {
            try
            {
                foreach (var row in table.Rows)
                {
                    ExcelPackage.LicenseContext = LicenseContext.Commercial;
                    var excelData = Utility.ReadExcel(FileUtil.GetImportExcelFilePath(scenarioContext, fileName));

                    var itemId = excelData[0]["Item ID"];
                    var component = excelData[0]["Component"];
                    var product = excelData[0]["Product"];

                    featureContext["itemIdValue"] = itemId;
                    featureContext["Component"] = component;
                    featureContext["Product"] = product;

                    await FillAndSelectFieldAsync(row[0].ToLower(), itemId);
                    await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
                }
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"[FAIL] Failed to retrieve and populate field values from Excel file '{fileName}'.", e);
            }
        }

        private async Task FillAndSelectFieldAsync(string fieldLocator, string value)
        {
            await itemMasterPage.GetDropdownInputField(fieldLocator).ClickAsync();
            await BasePage.TypeTextAsync(itemMasterPage.GetDropdownInputField(fieldLocator), value);
            await page.WaitForLoadStateAsync();
            await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);

            var isDropdownOpen = await commonLocators.GetDropdownListBox().IsVisibleAsync();
            if (isDropdownOpen)
            {
                await commonLocators.GetSelectDropdownOption(value).ClickAsync();
            }
        }

        private async Task FillTaxonomyFieldFromExcelAsync(string fieldName, string fieldValue)
        {
            var fieldLocator = itemMasterPage.GetProductTaxonomyFields(fieldName.ToLower());

            await fieldLocator.ClickAsync();
            await BasePage.TypeTextAsync(fieldLocator, fieldValue);
            await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
            await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);

            var isDropdownOpen = await commonLocators.GetDropdownListBox().IsVisibleAsync();
            if (isDropdownOpen)
            {
                await commonLocators.GetSelectDropdownOption(fieldValue).ClickAsync();
            }

            await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
            await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);
        }
    }
}

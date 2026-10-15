using Microsoft.Playwright;
using NUnit.Framework;
using PIMQAAutomation.Common.Pages;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Modules.Items.Constants;
using PIMQAAutomation.Modules.Items.Pages;
using PIMQAAutomation.Modules.Lob.Constants;
using PIMQAAutomation.Utilities;
using TechTalk.SpecFlow;
namespace PIMQAAutomation.Modules.Items.Actions
{
   

    /// <summary>
    /// Contains business logic and action methods for Item Taxonomy functionality
    /// </summary>
    public class ItemTaxonomyActions
    {
        private readonly IPage page;
        private readonly ScenarioContext scenarioContext;
        private readonly FeatureContext featureContext;
        private readonly ItemMasterPage itemMasterPage;
        private readonly CommonLocators commonLocators;

        public ItemTaxonomyActions(IPage page, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            this.page = page;
            this.scenarioContext = scenarioContext;
            this.featureContext = featureContext;
            itemMasterPage = new ItemMasterPage(page);
            commonLocators = new CommonLocators(page);
        }

        public async Task VerifyTaxonomyIsDisplayedAsync(string taxonomyOption)
        {
            await itemMasterPage.GetTaxonomyCharacteristicKindPanel().ScrollIntoViewIfNeededAsync();
            var classAttribute = await itemMasterPage.GetTaxonomyOption(taxonomyOption.ToLower()).GetAttributeAsync("class");
            Assert.IsTrue(classAttribute.Contains("active"), $"The {taxonomyOption} not automatically selected in Taxonomy Characteristic Kind Panel");
        }

        public async Task ClickTaxonomyAsync(string taxonomyOption)
        {
            var taxonomyLocator = taxonomyOption.ToLower();

            if (taxonomyLocator.Contains(ItemConstants.LocatorProperties.UserTaxonomy.ToLower()))
            {
                taxonomyLocator = ItemConstants.UserTaxonomy;
            }

            await itemMasterPage.GetTaxonomyOption(taxonomyLocator).ClickAsync();
        }

        public async Task EnterTaxonomyDetailsAsync(string taxonomyOption, Table fieldNames)
        {
            var uiServiceTaxonomyValues = new Dictionary<string, string>();
            var testCaseName = scenarioContext.ScenarioInfo.Title;
            var currentOem = featureContext.ContainsKey("CurrentOemField")
                         ? featureContext["CurrentOemField"].ToString()
           : string.Empty;

            foreach (var row in fieldNames.Rows)
            {
                var baseFieldName = row[0];
                var fieldLocator = NormalizeFieldLocator(baseFieldName);
                var taxonomyLocator = NormalizeTaxonomyLocator(taxonomyOption);

                var jsonKey = MapJsonKey(baseFieldName, currentOem);
                var inputValue = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.{jsonKey}")
                  ?? Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.{baseFieldName}");

                await FillTaxonomyFieldAsync(taxonomyLocator, fieldLocator, inputValue);
                uiServiceTaxonomyValues[row[0]] = inputValue;
            }

            await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
            await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);
            featureContext[LOBConstants.FeatureContextValues.ServiceTaxonomyDetails] = uiServiceTaxonomyValues;
        }

        public async Task EnterTaxonomyDetailsIfEmptyAsync(string taxonomyType, string button, Table fieldsToCheck)
        {
            taxonomyType = taxonomyType.ToLowerInvariant();

            var taxonomyDetails = await GetTaxonomyDetailsAsync(taxonomyType);

            if (string.IsNullOrWhiteSpace(taxonomyDetails))
            {
                await itemMasterPage.GetTaxonomyButtonName(ButtonNames.EditButton).ClickAsync();

                var testCaseName = scenarioContext.ScenarioInfo.Title;
                var currentOem = featureContext.ContainsKey("CurrentOemField")
             ? featureContext["CurrentOemField"].ToString()
               : string.Empty;

                foreach (var row in fieldsToCheck.Rows)
                {
                    string fieldName = row[0];
                    string jsonKey = MapJsonKey(fieldName, currentOem);

                    string value = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.{jsonKey}")
              ?? Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.{fieldName}");

                    if (string.IsNullOrWhiteSpace(value))
                    {
                        Console.WriteLine($"JSON key '{jsonKey}' not found. Skipping field: {fieldName}");
                        continue;
                    }

                    var uiFieldName = fieldName.Split('_')[0].ToLowerInvariant();
                    await FillTaxonomyFieldAsync(taxonomyType, uiFieldName, value);
                }

                await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);
                await itemMasterPage.GetTaxonomyButtonName(button.ToLower()).ClickAsync();
                await commonLocators.GetPopupYesButton().ClickAsync();
                await commonLocators.GetPopupOkButton().ClickAsync();
            }
        }

        public async Task EnterDetailsInTaxonomyAsync(string taxonomyOption, Table fieldNames)
        {
            foreach (var row in fieldNames.Rows)
            {
                var fieldLocator = row[0].ToLower();
                var taxonomyLocator = taxonomyOption.ToLower();
                var inputValue = featureContext[$"Random{row[0]}Name"].ToString();

                await itemMasterPage.GetTaxonomyField(taxonomyLocator, fieldLocator).ClickAsync();
                await BasePage.TypeTextAsync(itemMasterPage.GetTaxonomyField(taxonomyOption.ToLower(), fieldLocator), inputValue);
                await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
                await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);

                var isDropdownOpen = await commonLocators.GetDropdownListBox().IsVisibleAsync();
                if (isDropdownOpen)
                {
                    try
                    {
                        await commonLocators.GetSelectDropdownOption(inputValue).ClickAsync();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("List Item Not Found");
                    }
                }
            }
        }

        public async Task EnterDataInTaxonomyAsync(string taxonomyOption, Table fieldNames)
        {
            foreach (var row in fieldNames.Rows)
            {
                var fieldLocator = row[0].ToLower();
                var taxonomyLocator = taxonomyOption.ToLower();
                var inputValue = featureContext[$"Random{row[0]}Name"].ToString();

                await itemMasterPage.GetTaxonomyField(taxonomyLocator, fieldLocator).ClickAsync();
                await BasePage.TypeTextAsync(itemMasterPage.GetTaxonomyField(taxonomyOption.ToLower(), fieldLocator), inputValue);
                await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
            }
        }

        public async Task SearchAndSelectInProductTaxonomyAsync(string fieldName)
        {
            await page.WaitForLoadStateAsync();
            var testCaseName = scenarioContext.ScenarioInfo.Title;

            var taxonomyFields = new[]
        {
          ItemConstants.ProductTaxonomyFields.Oem,
          ItemConstants.ProductTaxonomyFields.Family,
          ItemConstants.ProductTaxonomyFields.Line,
          ItemConstants.ProductTaxonomyFields.Group
        };

            foreach (var taxonomyField in taxonomyFields)
            {
                string jsonKey = GetProductTaxonomyJsonKey(taxonomyField, fieldName);

                var inputValue = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.{jsonKey}");

                if (string.IsNullOrWhiteSpace(inputValue))
                {
                    throw new Exception($"JSON value not found for key '{jsonKey}' in test case '{testCaseName}'");
                }

                await FillProductTaxonomyFieldAsync(taxonomyField, inputValue);
                featureContext[jsonKey] = inputValue;
                Console.WriteLine($"Selected '{inputValue}' for Product Taxonomy field '{jsonKey}'");
            }

            if (fieldName.ToLower().Contains("oem"))
            {
                featureContext["CurrentOemField"] = fieldName;
                featureContext["CurrentOemSuffix"] = fieldName;
                Console.WriteLine($"?? OEM context set for {fieldName}");
            }

            await page.WaitForLoadStateAsync();
        }

        private async Task FillProductTaxonomyFieldAsync(string taxonomyField, string value)
        {
            var taxonomyFieldLower = taxonomyField.ToLower();
            var taxonomyInput = itemMasterPage.GetTaxonomyField(ItemConstants.ProductTaxonomyFields.ProductTaxonomy, taxonomyFieldLower);

            await taxonomyInput.ClickAsync();
            await BasePage.TypeTextAsync(taxonomyInput, value);
            await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
            await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);

            var isDropdownOpen = await commonLocators.GetDropdownListBox().IsVisibleAsync();
            if (isDropdownOpen)
            {
                await commonLocators.GetSelectDropdownOption(value).ClickAsync();
            }
        }

        private string GetProductTaxonomyJsonKey(string taxonomyField, string oemField)
        {
            if (taxonomyField.Equals(ItemConstants.ProductTaxonomyFields.Oem, StringComparison.OrdinalIgnoreCase))
            {
                return oemField;
            }

            return MapJsonKeyForTaxonomy(taxonomyField, oemField);
        }

        private string MapJsonKeyForTaxonomy(string baseField, string oemField)
        {
            return oemField.ToLower() switch
            {
                "oem" => baseField,
                "oem1" => $"{baseField}1",
                "oem2" => $"{baseField}2",
                _ => baseField
            };
        }

        private async Task FillTaxonomyFieldAsync(string taxonomyLocator, string fieldLocator, string value)
        {
            await itemMasterPage.GetTaxonomyField(taxonomyLocator, fieldLocator).ClickAsync();
            await BasePage.TypeTextAsync(itemMasterPage.GetTaxonomyField(taxonomyLocator, fieldLocator), value);
            await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
            await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);

            var isDropdownOpen = await commonLocators.GetDropdownListBox().IsVisibleAsync();
            if (isDropdownOpen)
            {
                await commonLocators.GetSelectDropdownOption(value).ClickAsync();
            }
        }

        private string NormalizeFieldLocator(string fieldName)
        {
            var fieldLocator = fieldName.ToLower();

            if (fieldLocator.Contains(ItemConstants.Item))
            {
                return ItemConstants.Item;
            }

            return fieldLocator;
        }

        private string NormalizeTaxonomyLocator(string taxonomyOption)
        {
            var taxonomyLocator = taxonomyOption.ToLower();

            if (taxonomyLocator.Contains(ItemConstants.LocatorProperties.UserTaxonomy.ToLower()))
            {
                return ItemConstants.LocatorProperties.UserTaxonomy.ToLower();
            }

            return taxonomyLocator;
        }

        private string MapJsonKey(string baseName, string oem)
        {
            return oem.ToLower() switch
            {
                "oem" => baseName,
                "oem1" => $"{baseName}1",
                "oem2" => $"{baseName}2",
                "oem1edit" => $"{baseName}Oem1Edit",
                "oem2edit" => $"{baseName}Oem2Edit",
                _ => baseName
            };
        }

        private async Task<string> GetTaxonomyDetailsAsync(string taxonomyType)
        {
            return taxonomyType switch
            {
                ItemConstants.ProductTaxonomyFields.ProductTaxonomy =>
           await itemMasterPage.GetTaxonomyField(taxonomyType, ItemConstants.ProductTaxonomyFields.Family.ToLower()).InputValueAsync(),
                ItemConstants.ServiceTaxonomyFields.ServiceTaxonomy =>
                  await itemMasterPage.GetTaxonomyField(taxonomyType, ItemConstants.ServiceTaxonomyFields.Class.ToLower()).InputValueAsync(),
                _ => throw new AssertionException($"Taxonomy Type {taxonomyType} not found")
            };
        }
    }
}

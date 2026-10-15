using Microsoft.Playwright;
using NUnit.Framework;
using OfficeOpenXml;
using PIMQAAutomation.Common.Pages;
using PIMQAAutomation.Helpers;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Modules.Items.Constants;
using PIMQAAutomation.Modules.Items.Pages;
using PIMQAAutomation.Utilities;
using PPT.ItemMaster.Data.Cosmos.Features.ItemPools;
using PPT.ItemMaster.Domain.Features.ItemPools;
using TechTalk.SpecFlow;
using Utilities;

namespace PIMQAAutomation.Modules.Items.Actions
{
    /// <summary>
    /// Contains business logic and action methods for Item Characteristic functionality
    /// </summary>
    public class ItemCharacteristicActions
    {
        private readonly IPage page;
        private readonly ScenarioContext scenarioContext;
        private readonly FeatureContext featureContext;
        private readonly ItemMasterPage itemMasterPage;
        private readonly CommonLocators commonLocators;
        private readonly CosmosDbHelper cosmosDb;

        public ItemCharacteristicActions(IPage page, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            this.page = page;
            this.scenarioContext = scenarioContext;
            this.featureContext = featureContext;
            itemMasterPage = new ItemMasterPage(page);
            commonLocators = new CommonLocators(page);
            cosmosDb = new CosmosDbHelper();
        }

        public async Task VerifyCharacteristicDisplayBoxesAsync(Table panelNames)
        {
            foreach (var panel in panelNames.Rows)
            {
                var panelName = panel[0];
                var displayBox = itemMasterPage.GetCharacteristicDisplayPanel(panelName.ToLower());
                await displayBox.ScrollIntoViewIfNeededAsync();
                Assert.IsTrue(await displayBox.IsVisibleAsync(), $"Dropdown for '{panelName}' is not visible.");
            }
        }

        public async Task VerifyRowIsDisplayedInCharacteristicSectionAsync(int rowNumber, string kindOrtype)
        {
            var rowLocator = itemMasterPage.GetFirstLineCharacteristicKindOrType(kindOrtype.ToLower(), rowNumber);
            var classAttribute = await rowLocator.GetAttributeAsync("class");
            Assert.IsTrue(classAttribute.Contains("active"), $"The first Line is not automatically selected in {kindOrtype}");
        }

        public async Task SelectCharacteristicKindAndTypeAsync(string kindName, string typeName)
        {
            try
            {
                var kindLocator = itemMasterPage.GetCharacteristicKindOrType(kindName);
                var typeLocator = itemMasterPage.GetCharacteristicKindOrType(typeName);

                await ClickWithFallbackAsync(kindLocator, $"Kind '{kindName}'");
                await ClickWithFallbackAsync(typeLocator, $"Type '{typeName}'");

                featureContext["CharacteristicType"] = typeName;
                featureContext["CharacteristicKind"] = kindName;
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to select characteristic Kind='{kindName}' and Type='{typeName}' in Item Master. Error: {ex.Message}", ex);
            }
        }

        public async Task SelectCreatedCharacteristicKindAsync()
        {
            try
            {
                if (!featureContext.TryGetValue("RandomKindName", out string kindName) || string.IsNullOrEmpty(kindName))
                {
                    throw new CustomException("[ERROR] RandomKindName not found in scenario context. Make sure Kind was created first.");
                }

                var kindLocator = itemMasterPage.GetCharacteristicKind(kindName);
                Console.WriteLine($"The generated Characteristic Kind Locator is: {kindLocator}");
                Console.WriteLine($"The identified Characteristic Kind is: {kindName}");

                await kindLocator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
                Console.WriteLine($"Waited for the {kindLocator} successfully....");
                await kindLocator.ScrollIntoViewIfNeededAsync();
                Console.WriteLine($"Scrolled the {kindLocator} successfully.");

                await ClickWithFallbackAsync(kindLocator, $"Kind '{kindName}'");
                Console.WriteLine($"Clicked on the {kindLocator} successfully.");

                featureContext["CharacteristicKind"] = kindName;
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to select characteristic Kind from scenario context. Error: {ex.Message}", ex);
            }
        }

        public async Task SelectCharacteristicTypeFromDropdownAsync()
        {
            try
            {
                if (!featureContext.TryGetValue("RandomTypeName", out string typeName) || string.IsNullOrEmpty(typeName))
                {
                    throw new CustomException("[ERROR] RandomTypeName not found in scenario context. Make sure Type was created first.");
                }

                await itemMasterPage.GetCharacteristicTypeDropDown().WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
                await itemMasterPage.GetCharacteristicTypeDropDown().ClickAsync();
                await itemMasterPage.GetCharacteristicTypeDropDown().FillAsync(typeName);

                var typeOption = itemMasterPage.GetCharacteristicTypeOption(typeName);
                await typeOption.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
                await typeOption.ClickAsync();

                featureContext["CharacteristicType"] = typeName;
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Failed to select characteristic Type from scenario context. Error: {ex.Message}", ex);
            }
        }

        public async Task SelectCharacteristicKindAndTypeFromExcelAsync(string fileName, string fieldName, string tabName)
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            var excelData = Utility.ReadExcel(FileUtil.GetImportExcelFilePath(scenarioContext, fileName));
            var oemName = excelData[0][ItemConstants.LocatorProperties.Oem.ToUpper()];
            scenarioContext["OEM"] = oemName;

            foreach (var row in excelData)
            {
                await page.WaitForLoadStateAsync();
                var kind = row["Characteristic Kind"].Trim();
                var type = row["Characteristic Type"].Trim();
                var expectedValue = row["Value"].Trim();

                await page.WaitForLoadStateAsync();
                await itemMasterPage.GetCharacteristicKind(kind).ClickAsync();
                await itemMasterPage.GetCharacteristicType(type).ClickAsync();

                var actualValue = (await itemMasterPage.GetDefaultCharacteristicsFields(tabName.ToLower(), fieldName.ToLower()).TextContentAsync()).Trim();
                ValidationHelper.AssertEqual("Characteristics", expectedValue, actualValue, $"Mismatch for Kind: '{kind}', Type: '{type}'. Expected: '{expectedValue}' but found: '{actualValue}'");
            }
        }

        public async Task VerifyButtonsAreEnabledAsync(Table buttonNames)
        {
            foreach (var row in buttonNames.Rows)
            {
                await BasePage.VerifyFieldIsEnabled(itemMasterPage.GetCharacteristicButton(row[0].ToLower()), $"{row[0]}");
            }
        }

        public async Task VerifyButtonsAreDisabledAsync(Table buttonNames)
        {
            foreach (var row in buttonNames.Rows)
            {
                await BasePage.VerifyFieldIsDisabled(itemMasterPage.GetCharacteristicButton(row[0].ToLower()), $"{row[0]}");
            }
        }

        public async Task ClickButtonInCharacteristicPanelAsync(string buttonName)
        {
            var buttonLocator = itemMasterPage.GetCharacteristicButton(buttonName.ToLower());
            await buttonLocator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
            await BasePage.VerifyFieldIsEnabled(buttonLocator, buttonName);

            if (buttonName.Equals(ButtonNames.SaveButton, StringComparison.OrdinalIgnoreCase))
            {
                await HandleSaveButtonClickAsync(buttonLocator);
            }
            else
            {
                Console.WriteLine($"Clicking '{buttonName}' button.");
                await buttonLocator.ClickAsync();
            }
        }

        public async Task ClickEnabledButtonAsync(string addlevelbutton, string editbutton)
        {
            var addlevelEnableState = itemMasterPage.GetCharacteristicButton(addlevelbutton.ToLower());
            var editEnableState = itemMasterPage.GetCharacteristicButton(editbutton.ToLower());

            if (await addlevelEnableState.IsEnabledAsync())
            {
                await addlevelEnableState.ClickAsync();
            }
            else if (await editEnableState.IsEnabledAsync())
            {
                await editEnableState.ClickAsync();
            }
            else
            {
                ValidationHelper.Fail("No enabled button found to proceed.");
            }
        }

        public async Task VerifyFieldsAreAutoPopulatedAndNotEditableAsync(Table fieldsToCheck)
        {
            await itemMasterPage.GetCharacteristicField("type").ScrollIntoViewIfNeededAsync();

            foreach (var row in fieldsToCheck.Rows)
            {
                var fieldName = row[0];
                var locator = itemMasterPage.GetCharacteristicField(fieldName.ToLower());
                Assert.IsFalse(string.IsNullOrWhiteSpace(await locator.InputValueAsync()), $"{fieldName} is not auto-populated.");
                Assert.IsTrue(await locator.IsDisabledAsync(), $"{fieldName} is editable but should be read-only.");
            }

            scenarioContext["CharacteristicType"] = await itemMasterPage.GetCharacteristicField("type").InputValueAsync();
        }

        public async Task EnterDataIntoCharacteristicFieldAsync(string fieldName)
        {
            var testCaseName = scenarioContext.ScenarioInfo.Title;
            var inputValue = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.{fieldName}");

            if (fieldName.Contains(ItemConstants.LocatorProperties.CharacteristicValue))
            {
                fieldName = ItemConstants.LocatorProperties.CharacteristicValue;
            }

            var field = itemMasterPage.GetCharacteristicField(fieldName.ToLower());
            await field.IsEnabledAsync();
            await field.ClickAsync();
            await BasePage.ClearInputIfNotEmptyAsync(field);
            await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);
            await field.FillAsync(inputValue);
            await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());

            var isDropdownOpen = await commonLocators.GetDropdownListBox().IsVisibleAsync();
            if (isDropdownOpen)
            {
                await commonLocators.GetSelectDropdownOption(inputValue).ClickAsync();
            }

            featureContext["Value"] = inputValue;
        }

        public async Task EnterDataIntoCharacteristicValueFieldAsync()
        {
            if (!featureContext.ContainsKey("RandomLOVName"))
            {
                throw new Exception("[ERROR] RandomLOVName not found in ScenarioContext. Make sure LOV creation step ran before this.");
            }

            string inputValue = featureContext["RandomLOVName"].ToString();

            await itemMasterPage.GetCharacteristicValueDropDown().ClickAsync();
            await itemMasterPage.GetCharacteristicValueDropDown().FillAsync(inputValue);
            await page.WaitForLoadStateAsync();
            await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);

            var isDropdownOpen = await commonLocators.GetDropdownListBox().IsVisibleAsync();
            if (isDropdownOpen)
            {
                await commonLocators.GetSelectDropdownOption(inputValue).ClickAsync();
            }

            featureContext["Value"] = inputValue;
        }

        public async Task EnterDataIntoCharacteristicTextAreaFieldAsync(string fieldName)
        {
            var testCaseName = scenarioContext.ScenarioInfo.Title;
            var value = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.{fieldName}");
            var inputField = itemMasterPage.GetCharacteristicTextAreaField(fieldName.ToLower());
            await inputField.FillAsync(value);
            await page.WaitForLoadStateAsync();
        }

        public async Task EnterTextIntoFieldInCharacteristicPanelAsync(string fieldName)
        {
            var testCaseName = scenarioContext.ScenarioInfo.Title;
            var getValue = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.{fieldName}");
            await itemMasterPage.GetCharacteristicTextAreaField(fieldName.ToLower()).FillAsync(getValue);
        }

        public async Task VerifyFieldsAreNotEditableAsync(Table fieldsToCheck)
        {
            foreach (var row in fieldsToCheck.Rows)
            {
                var fieldName = row[0];
                var locator = itemMasterPage.GetCharacteristicTextAreaField(fieldName.ToLower());
                Assert.IsTrue(await locator.IsDisabledAsync(), $"{fieldName} is editable but should be read-only.");
            }
        }

        public async Task VerifyFieldsAreEditableAsync(Table fieldsToCheck)
        {
            foreach (var row in fieldsToCheck.Rows)
            {
                var fieldName = row[0];
                var locator = itemMasterPage.GetCharacteristicTextAreaField(fieldName.ToLower());
                Assert.IsTrue(await locator.IsEnabledAsync(), $"{fieldName} is not editable but should be read-write.");
            }
        }

        public async Task VerifyCharacteristicTypeIsEditableAsync(Table fieldsToCheck)
        {
            foreach (var row in fieldsToCheck.Rows)
            {
                var fieldName = row[0];
                var locator = itemMasterPage.GetCharacteristicField(fieldName.ToLower());
                Assert.IsTrue(await locator.IsEnabledAsync(), $"{fieldName} is not editable but should be read-write.");
            }
        }

        public async Task VerifyTaxonomyCharacteristicKindFieldsAreNotEmptyAsync(Table fieldNames)
        {
            foreach (var row in fieldNames.Rows)
            {
                await itemMasterPage.GetTaxonomyFields(row[0].ToLower()).IsVisibleAsync();
            }
        }

        public async Task SearchAndSelectCharacteristicAsync(string characteristicType)
        {
            var testCaseName = scenarioContext.ScenarioInfo.Title;
            var inputValue = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.{characteristicType}");
            var field = itemMasterPage.GetCharacteristicField(characteristicType.ToLower());

            await field.ClickAsync();
            await field.FillAsync(inputValue);
            await page.WaitForLoadStateAsync();
            await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);

            var isDropdownOpen = await commonLocators.GetDropdownListBox().IsVisibleAsync();
            if (isDropdownOpen)
            {
                await commonLocators.GetSelectDropdownOption(inputValue).ClickAsync();
            }

            featureContext["CharacteristicType"] = await itemMasterPage.GetCharacteristicField("type").InputValueAsync();
        }

        public async Task SearchAndSelectFromDropdownAsync(string fieldName)
        {
            await page.WaitForLoadStateAsync();
            var testCaseName = scenarioContext.ScenarioInfo.Title;
            var inputValue = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.{fieldName}");
            var fieldLocator = NormalizeFieldLocator(fieldName);

            await itemMasterPage.GetDropdownInputField(fieldLocator).ClickAsync();
            await BasePage.TypeTextAsync(itemMasterPage.GetDropdownInputField(fieldLocator), inputValue);
            await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
            await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);

            var isDropdownOpen = await commonLocators.GetDropdownListBox().IsVisibleAsync();
            if (isDropdownOpen)
            {
                await commonLocators.GetSelectDropdownOption(inputValue).ClickAsync();
            }

            featureContext[$"{fieldName}"] = inputValue;

            if (fieldName.ToLower().Contains("oem"))
            {
                featureContext["CurrentOemField"] = fieldName;
                featureContext["CurrentOemValue"] = inputValue;
                Console.WriteLine($"OEM context set to Field: {fieldName}, Value: {inputValue}");
            }

            await page.WaitForLoadStateAsync();
        }

        public async Task RemoveExistingIdAsync(string button)
        {
            await itemMasterPage.GetCrossButton(button.ToLower()).ClickAsync();
        }

        public async Task GetCharacteristicValueAsync(string characteristicName)
        {
            try
            {
                var characteristicValue = await itemMasterPage.GetCharacteristicValue(characteristicName).TextContentAsync();
                featureContext["Value"] = characteristicValue;
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"Failed to get characteristic value for {characteristicName}", e);
            }
        }

        public async Task VerifyCheckboxIsNotEditableAsync(string checkbox)
        {
            Assert.IsTrue(await itemMasterPage.GetCharacteristicCheckbox(checkbox.ToLower()).IsDisabledAsync(), $"{checkbox} is editable but should be read-only.");
        }

        public async Task ClickCharacteristicCheckboxesAsync(Table fieldsToCheck)
        {
            foreach (var row in fieldsToCheck.Rows)
            {
                await itemMasterPage.GetCharacteristicCheckbox(row[0].ToLower()).ClickAsync();
            }
        }

        public async Task VerifyCharacteristicCheckboxesAreCheckedAsync(Table fieldsToCheck)
        {
            foreach (var row in fieldsToCheck.Rows)
            {
                Assert.IsTrue(await itemMasterPage.GetCharacteristicCheckbox(row[0].ToLower()).IsCheckedAsync(), $"{row[0]} checkbox should be checked");
            }
        }

        public async Task VerifyCharacteristicCheckboxesAreUncheckedAsync(Table fieldsToCheck)
        {
            foreach (var row in fieldsToCheck.Rows)
            {
                Assert.False(await itemMasterPage.GetCharacteristicCheckbox(row[0].ToLower()).IsCheckedAsync(), $"{row[0]} checkbox should be unchecked");
            }
        }

        public async Task VerifyCheckboxesAreEnabledAsync(Table fieldsToCheck)
        {
            foreach (var row in fieldsToCheck.Rows)
            {
                await BasePage.VerifyFieldIsEnabled(itemMasterPage.GetCharacteristicCheckbox(row[0].ToLower()), $"{row[0]}");
            }
        }

        public async Task VerifyCheckboxesAreDisabledAsync(Table fieldsToCheck)
        {
            foreach (var row in fieldsToCheck.Rows)
            {
                await BasePage.VerifyFieldIsDisabled(itemMasterPage.GetCharacteristicCheckbox(row[0].ToLower()), $"{row[0]}");
            }
        }

        public async Task VerifyCheckboxFieldsAreNotEditableAsync(Table fieldsToCheck)
        {
            foreach (var row in fieldsToCheck.Rows)
            {
                var fieldName = row[0];
                var locator = itemMasterPage.GetTypeExplicitValueField(fieldName.ToLower());
                Assert.IsTrue(await locator.IsDisabledAsync(), $"{fieldName} is editable but should be read-only.");
            }
        }

        public async Task VerifyCheckboxFieldsAreEditableAndEditFlagAsync(Table fieldsToCheck)
        {
            foreach (var row in fieldsToCheck.Rows)
            {
                var fieldName = row[0];
                var locator = itemMasterPage.GetTypeExplicitValueField(fieldName.ToLower());
                Assert.IsTrue(await locator.IsEnabledAsync(), $"{fieldName} is not editable but should be read-write.");
                await itemMasterPage.GetTypeExplicitValueField(fieldName.ToLower()).ClickAsync();
            }
        }

        public async Task SetCheckboxStateAsync(string checkboxName, bool shouldBeChecked)
        {
            var checkboxLocator = itemMasterPage.GetCharacteristicCheckbox(checkboxName.ToLower());
            bool isChecked = await checkboxLocator.IsCheckedAsync();

            if (isChecked != shouldBeChecked)
            {
                await checkboxLocator.ClickAsync();
            }
        }

        public async Task VerifyCreatedAndModifiedFieldsAsync(Table dateFields)
        {
            foreach (var row in dateFields.Rows)
            {
                await itemMasterPage.GetCharacteristicPanelDateFields(row[0].ToLower()).IsVisibleAsync();
            }
        }

        public async Task VerifyExplicitValuesFieldsAreEnabledAsync(Table characteristicButton)
        {
            foreach (var row in characteristicButton.Rows)
            {
                await itemMasterPage.GetCharacteristicButton(row[0].ToLower()).IsVisibleAsync();
            }
        }

        public async Task VerifyNewCharacteristicIsDisplayedAsync(string tabName, string valueField)
        {
            await itemMasterPage.GetDefaultCharacteristicsFields(tabName.ToLower(), valueField.ToLower()).IsVisibleAsync();
        }

        public async Task VerifyAddedCharacteristicWithFromLevelAsync(string levelValue)
        {
            var characteristicType = featureContext["CharacteristicType"].ToString();
            string characteristicValue = ContextHelper.GetContextValue(featureContext, "Value");

            Assert.IsTrue(
                await itemMasterPage.GetCharacteristicDisplayAndLevelValue(characteristicType, characteristicValue, levelValue).IsVisibleAsync(),
                $"Expected row with Characteristic Type '{characteristicType}', Value '{characteristicValue}', and From Level '{levelValue}' was not found in the grid.");
        }

        public void CalculatePLLaborValuesAsync(Table table)
        {
            try
            {
                var row = table.Rows[0];
                decimal listPrice = Convert.ToDecimal(row["List Price"]);
                decimal mslpPercent = Convert.ToDecimal(row["MSLP Percent in Trigger Calc"]);
                decimal pl4Hour = listPrice * (mslpPercent / 100);

                decimal plNextDay = pl4Hour * 0.8m;
                decimal pl7x24x4 = pl4Hour * 1.25m;
                decimal plDepot = plNextDay * 0.8m;
                decimal pl7x24x2 = pl7x24x4 / 0.8m;
                decimal plAdvExchg = plNextDay * 0.9m;

                decimal labor4Hour = pl4Hour * 0.65m;
                decimal labor7x24x4 = pl7x24x4 * 0.65m;
                decimal labor7x24x2 = pl7x24x2 * 0.65m;
                decimal laborAdvExchg = plAdvExchg * 0.65m;
                decimal laborDepot = plDepot * 0.65m;
                decimal laborNextDay = plNextDay * 0.65m;

                var results = new Dictionary<string, int>
                {
                    [ItemConstants.CharacteristicNames.PL4Hour] = (int)Math.Round(pl4Hour, MidpointRounding.AwayFromZero),
                    [ItemConstants.CharacteristicNames.PLNextDay] = (int)Math.Round(plNextDay, MidpointRounding.AwayFromZero),
                    [ItemConstants.CharacteristicNames.PL7x24x4] = (int)Math.Round(pl7x24x4, MidpointRounding.AwayFromZero),
                    [ItemConstants.CharacteristicNames.PLDepot] = (int)Math.Floor(plDepot),
                    [ItemConstants.CharacteristicNames.PL7x24x2] = (int)Math.Round(pl7x24x2, MidpointRounding.AwayFromZero),
                    [ItemConstants.CharacteristicNames.PLAdvExchge] = (int)Math.Round(plAdvExchg, MidpointRounding.AwayFromZero),
                    [ItemConstants.CharacteristicNames.Labor4Hour] = (int)Math.Round(labor4Hour, MidpointRounding.AwayFromZero),
                    [ItemConstants.CharacteristicNames.Labor7x24x4] = (int)Math.Round(labor7x24x4, MidpointRounding.AwayFromZero),
                    [ItemConstants.CharacteristicNames.Labor7x24x2] = (int)Math.Round(labor7x24x2, MidpointRounding.AwayFromZero),
                    [ItemConstants.CharacteristicNames.LaborAdvExchg] = (int)Math.Round(laborAdvExchg, MidpointRounding.AwayFromZero),
                    [ItemConstants.CharacteristicNames.LaborDepot] = (int)Math.Floor(laborDepot),
                    [ItemConstants.CharacteristicNames.LaborNextDay] = (int)Math.Round(laborNextDay, MidpointRounding.AwayFromZero)
                };

                featureContext["PLLaborResults"] = results;
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Error while calculating P&L and Labor values: {ex.Message}", ex);
            }
        }

        public async Task VerifyPLLaborValuesWithCalculatedValuesAsync()
        {
            try
            {
                if (!featureContext.TryGetValue("PLLaborResults", out Dictionary<string, int> expectedIntValues))
                {
                    throw new KeyNotFoundException("Expected 'PLLaborResults' not found in FeatureContext.");
                }

                var expectedValues = expectedIntValues.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());
                var actualValues = new Dictionary<string, string>();

                foreach (var key in expectedValues.Keys)
                {
                    string uiText = (await itemMasterPage.GetCharacteristicValue(key).TextContentAsync())?.Trim() ?? string.Empty;
                    actualValues[key] = uiText;
                }

                ValidationHelper.AssertDictionariesMatch(actualValues, expectedValues, "P&L and Labor Validation");
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Error during P&L and Labor validation: {ex.Message}", ex);
            }
        }

        public async Task VerifyPLLaborValuesInItemPoolAsync()
        {
            var itemId = string.Empty;
            try
            {
                if (!featureContext.TryGetValue("PLLaborResults", out Dictionary<string, int> plLaborResults))
                {
                    throw new KeyNotFoundException("Expected 'PLLaborResults' not found in FeatureContext.");
                }

                var expectedValues = plLaborResults.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());
                var database = DatabaseConstants.DatabaseName;
                var container = DatabaseConstants.ItemPoolsContainer;
                itemId = ContextHelper.GetContextValue(featureContext, "itemIdValue");
                var query = CosmosDBQueries.GetItemPoolQuery(itemId);

                var dbData = await cosmosDb.GetDataFromCosmosDB<ItemPoolRecord>(database, container, query);
                ValidationHelper.FailIfNullOrEmpty("ItemPoolRecord", dbData, $"for itemId: {itemId}");

                var skuAttributes = dbData.Item?.SkuAttributes;
                ValidationHelper.FailIfNullOrEmpty("SkuAttributes", skuAttributes, $"SkuAttributes not found for itemId: {itemId}");

                var actualValues = skuAttributes?.Kinds?
            .FirstOrDefault(k => k.Name.Equals("List Price Amounts", StringComparison.OrdinalIgnoreCase))
             ?.Characteristics?
    .Where(c => !string.IsNullOrWhiteSpace(c.Name))
    .ToDictionary(c => c.Name.Trim(), c => c.Value?.ToString().Trim());

                ValidationHelper.FailIfNullOrEmpty(ItemConstants.CharacteristicKinds.ListPriceAmounts, actualValues, $"List Price Amounts not found for itemId: {itemId}");

                foreach (var kvp in expectedValues)
                {
                    if (!actualValues.TryGetValue(kvp.Key, out var actualValue))
                    {
                        ValidationHelper.Fail($"{kvp.Key}", new Exception($"Characteristic not found for itemId: {itemId}"));
                    }

                    ValidationHelper.AssertEqual($"{kvp.Key}", kvp.Value, actualValue, $"Mismatch for itemId: {itemId}");
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"P&L and Labor value validation failed for itemId '{itemId}'", ex);
            }
        }

        public async Task VerifySkuAttributesInItemPoolAsync(Table characteristicValues)
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

                var characteristics = dbData.Item?.SkuAttributes?.Kinds?
                  .SelectMany(
                      kind => kind.Characteristics ?? new List<SkuAttributes.Characteristic>(),
                      (kind, c) => (Kind: kind.Name?.Trim(), Type: c.Name?.Trim(), Value: c.Value?.ToString().Trim().ToLower()))
             .ToList();

                ValidationHelper.FailIfNullOrEmpty("SkuAttributes.Characteristics", characteristics, $"SkuAttributes not found for itemId: {itemId}");

                foreach (var row in characteristicValues.Rows)
                {
                    var expectedKind = row[0]?.Trim();
                    var expectedType = row[1]?.Trim();
                    var expectedValue = row[2]?.Trim();

                    var match = characteristics.FirstOrDefault(c =>
                             c.Kind.Equals(expectedKind, StringComparison.OrdinalIgnoreCase) &&
                    c.Type.Equals(expectedType, StringComparison.OrdinalIgnoreCase));

                    ValidationHelper.FailIfNullOrEmpty($"{expectedKind} - {expectedType}", match, $"not found for itemId: {itemId}");
                    ValidationHelper.AssertEqual($"{expectedKind} - {expectedType}", expectedValue, match.Value, $"Mismatch for itemId: {itemId}");
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"SkuAttributes validation failed for itemId '{itemId}'", ex);
            }
        }

        public async Task ValidateCharacteristicKindIsNotVisibleAsync()
        {
            try
            {
                if (!featureContext.TryGetValue("RandomKindName", out string kindName) || string.IsNullOrEmpty(kindName))
                {
                    throw new CustomException("[ERROR] RandomKindName not found in scenario context. Make sure Kind was created first.");
                }

                var kindLocator = itemMasterPage.GetCharacteristicKind(kindName);
                Console.WriteLine($"[INFO] Checking visibility of Characteristic Kind: {kindName}");

                await BasePage.WaitForElementToDisappearAsync(itemMasterPage.GetCharacteristicKind(kindName));

                bool isVisible = await kindLocator.IsVisibleAsync();

                if (isVisible)
                {
                    throw new CustomException($"[FAIL] Characteristic Kind '{kindName}' is unexpectedly visible.");
                }

                Console.WriteLine($"[PASS] Characteristic Kind '{kindName}' is not visible as expected.");
                featureContext["CharacteristicKind"] = kindName;
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("validate Characteristic Kind is not visible.", ex);
            }
        }

        public async Task ValidateErrorMessageAsync()
        {
            try
            {
                string expectedTitle = ItemConstants.expectedTitle;
                string expectedMessage = ItemConstants.expectedMessage;

                await itemMasterPage.GetCharacteristicErrorTitle().WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });

                string actualTitle = await itemMasterPage.GetCharacteristicErrorTitle().TextContentAsync();
                string actualMessage = await itemMasterPage.GetCharacteristicErrorMessage().TextContentAsync();

                ValidationHelper.AssertEqual("Error title", expectedTitle, actualTitle.Trim());
                ValidationHelper.AssertEqual("Error message", expectedMessage, actualMessage);

                await commonLocators.GetPopupOkButton().ClickAsync();

                Console.WriteLine("[INFO] Error popup message validated successfully.");
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("Error while validating the optional type in Characteristics Kind types", ex);
            }
        }

        private async Task ClickWithFallbackAsync(ILocator locator, string label)
        {
            await locator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            await locator.ScrollIntoViewIfNeededAsync();

            try
            {
                await locator.ClickAsync();
            }
            catch (Exception)
            {
                Console.WriteLine($"[Warning] Normal click failed for {label}, retrying with Force=true");
                await locator.ClickAsync(new LocatorClickOptions { Force = true });
            }
        }

        private async Task HandleSaveButtonClickAsync(ILocator buttonLocator)
        {
            var maxRetries = 3;
            var isPopupVisible = false;

            for (var attempt = 1; attempt <= maxRetries; attempt++)
            {
                Console.WriteLine($"Attempt {attempt}: Clicking Save button");
                await buttonLocator.ScrollIntoViewIfNeededAsync();
                await buttonLocator.ClickAsync();
                isPopupVisible = await commonLocators.GetSuccessTitlePopup().IsVisibleAsync();

                if (isPopupVisible)
                {
                    Console.WriteLine("Popup appeared.");
                    break;
                }
            }

            if (!isPopupVisible)
            {
                Console.WriteLine("Popup did not appear after max retries.");
            }
        }

        private string NormalizeFieldLocator(string fieldName)
        {
            var fieldLocator = fieldName.ToLower();

            if (fieldLocator.Contains(ItemConstants.LocatorProperties.Oem))
            {
                return ItemConstants.LocatorProperties.Oem;
            }
            else if (fieldLocator.Contains(ItemConstants.LocatorProperties.LOBDeff))
            {
                return ItemConstants.LocatorProperties.LOBDeff;
            }

            return fieldLocator;
        }
    }
}

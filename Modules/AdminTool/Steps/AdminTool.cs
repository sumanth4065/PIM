using Microsoft.Playwright;
using NUnit.Framework;
using PIMQAAutomation.Utilities;
using PPT.ItemMaster.Data.Cosmos.Features.AdminDefinitions;
using TechTalk.SpecFlow;
using PPT.ItemMaster.Domain.Features.DocumentSources;
using PIMQAAutomation.Modules.Bom.Pages;
using PIMQAAutomation.Modules.Items.Constants;
using PIMQAAutomation.Common.Pages;
using PIMQAAutomation.Modules.AdminTool.Pages;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Modules.Lob.Constants;

namespace PIMQAAutomation.StepDefinitions.AdminToolStepDefinitions
{
    [Binding]
    public class AdminToolSteps
    {
        private ScenarioContext scenarioContext;
        private FeatureContext featureContext;
        public IPage page;
        private CommonLocators commonLocators;
        private AdminToolPage adminToolPage;
        private CosmosDbHelper cosmosDb;
        private string testCaseName = string.Empty;

        public AdminToolSteps(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            page = featureContext["Page"] as IPage;
            this.featureContext = featureContext;
            this.scenarioContext = scenarioContext;
            adminToolPage = new AdminToolPage(page);
            commonLocators = new CommonLocators(page);
            cosmosDb = new CosmosDbHelper();
        }

        [Then(@"Verify ""(.*)"" ""(.*)"" subtab is displayed")]
        public async Task ThenVerifyTabIsDisplaye(string mainTab, string subTab)
        {
            await Assertions.Expect(adminToolPage.GetAdminToolSubTab(mainTab.ToLower(), subTab.ToLower())).ToHaveAttributeAsync("aria-selected", "true");
        }

        [When(@"User selects Admin Tool ""(.*)"" Tab")]
        public async Task WhenUserSelectsAdminToolTab(string mainTab)
        {
            await adminToolPage.GetAdminToolMainTab(mainTab.ToLower()).ClickAsync();
        }

        [Then(@"Verify row ""(.*)"" is displayed in ""(.*)"" section")]
        public async Task ThenVerifyRowIsDisplayedInSection(int rowNumber, string bomStyleOrKind)
        {
            Assert.IsTrue((await adminToolPage.GetFirstLineBOMStyleOrKind(bomStyleOrKind.ToLower(), rowNumber).GetAttributeAsync("class")).Contains("active"), $"The first Line is not automatically selected in {bomStyleOrKind}");
        }

        [When(@"User clicks ""(.*)"" item in ""(.*)"" Section")]
        public async Task WhenUserClicksItemInSection(string bomStyleOrKindName, string bomStyleOrKind)
        {
            await adminToolPage.GetSelectBOMStyleOrkind(bomStyleOrKind.ToLower(), bomStyleOrKindName).ClickAsync();
        }

        [Then(@"User verifies ""(.*)"" button is enabled under ""(.*)"" tab")]
        public async Task ThenVerifyButtonIsEnabledUnderTab(string button, string tabName)
        {
            await BasePage.VerifyFieldIsEnabled(adminToolPage.GetBOMKindButton(tabName.ToLower(), button.ToLower()), $"{button}");
        }

        [Then(@"User verifies ""(.*)"" button is disabled under ""(.*)"" tab")]
        public async Task ThenVerifyButtonIsDisabledUnderTab(string button, string tabName)
        {
            await BasePage.VerifyFieldIsDisabled(adminToolPage.GetBOMKindButton(tabName.ToLower(), button.ToLower()), $"{button}");
        }

        [When(@"User clicks on ""(.*)"" button under ""(.*)"" tab")]
        public async Task WhenUserClicksOnButtonUnderTab(string button, string tabName)
        {
            await adminToolPage.GetBOMKindButton(tabName.ToLower(), button.ToLower()).ClickAsync();
        }

        [Then(@"Verify Is ""(.*)"" checkbox is checked")]
        public async Task ThenVerifyIsCheckboxIsChecked(string checkbox)
        {
            await AdminToolPageActions.VerifyCheckboxStateAsync(adminToolPage.GetBOMKindCheckbox(checkbox.ToLower()), checkbox, true);
        }

        [Then(@"Verify Not ""(.*)"" checkbox is unchecked")]
        public async Task ThenVerifyNotCheckboxIsUnchecked(string checkbox)
        {
            await AdminToolPageActions.VerifyCheckboxStateAsync(adminToolPage.GetBOMKindCheckbox(checkbox.ToLower()), checkbox, false);
        }

        [Then("User enters data for the below fields")]
        public async Task ThenUserEntersDataForTheBelowFields(Table fieldNames)
        {
            foreach (var fieldName in fieldNames.Rows)
            {
                testCaseName = scenarioContext.ScenarioInfo.Title;
                string fieldLocator = fieldName[0].ToLower();
                string inputValue = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.{fieldName[0]}");
                if (fieldLocator == "name")
                {
                    inputValue = $"{inputValue}-{Guid.NewGuid().ToString().Substring(0, 2).ToUpper()}";
                }
                await adminToolPage.GetInputField(fieldLocator).ClickAsync();
                await adminToolPage.GetInputField(fieldLocator).FillAsync(inputValue);
            }
            featureContext["BOMKind"] = await adminToolPage.GetInputField("name").InputValueAsync();
        }

        [Then(@"User enters text into the Internal ""(.*)"" field")]
        public async Task ThenUserEntersTextIntoTheInternalField(string notes)
        {
            testCaseName = scenarioContext.ScenarioInfo.Title;
            string inputValue = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.{notes}");
            var fieldLocator = notes.ToLower();
            if (fieldLocator.Contains(LOBConstants.LocatorProperties.InternalNotes, StringComparison.OrdinalIgnoreCase))
            {
                fieldLocator = LOBConstants.LocatorProperties.InternalNotes;
            }
            await adminToolPage.GetInternalNotes(fieldLocator).FillAsync(inputValue);
        }

        [Then("Verify created and modified username and date in right corner")]
        public async Task ThenVerifyCreatedAndModifiedUsernameAndDateInRightCorner(Table dateFields)
        {
            foreach (var dateField in dateFields.Rows)
            {
                await adminToolPage.GetCreatedModifiedUserDate(dateField[0]).IsVisibleAsync();
            }
        }

        [Then(@"Verify below field values with Cosmos DB using ""(.*)"" database and ""(.*)"" container using ""(.*)"" reference")]
        public async Task ThenVerifyBelowFiledValuesWithCosmosDBUsingDatabaseAndContainer(string database, string container, string referenceType, Table fieldNames)
        {
            try
            {
                string BOMKind = featureContext["BOMKind"].ToString();
                var dbAdminDefData = await cosmosDb.GetDataFromCosmosDB<BOMSourceRecord>(database.ToLower(), container, CosmosDBQueries.GetDataUsingNameOrSerialNo(BOMKind));
                var uiBOMHeaderValues = new Dictionary<string, string>();
                var dbBOMHeaderValues = new Dictionary<string, string>();
                foreach (var fieldName in fieldNames.Rows)
                {
                    string originalKey = fieldName[0];
                    string menuLocator = originalKey.ToLower();
                    string dbKey = originalKey == ItemConstants.Sequencing ? ItemConstants.Sequence : originalKey;
                    string inputValue = null;
                    if (menuLocator == ItemConstants.InternalNotes)
                    {
                        inputValue = await adminToolPage.GetInternalNotes(menuLocator).InputValueAsync();
                    }
                    else if (menuLocator.Contains(ItemConstants.IsActiveCheckbox))
                    {
                        bool isChecked = await adminToolPage.GetBOMKindCheckbox(ItemConstants.IsActiveCheckbox).IsCheckedAsync();
                        inputValue = isChecked ? "True" : "False";
                    }
                    else
                    {
                        inputValue = await adminToolPage.GetInputField(menuLocator).InputValueAsync();
                    }
                    uiBOMHeaderValues[dbKey] = inputValue;
                    var propertyInfo = dbAdminDefData.GetType().GetProperty(dbKey);
                    var dbValue = propertyInfo.GetValue(dbAdminDefData).ToString();
                    dbBOMHeaderValues[dbKey] = dbValue;
                }
                foreach (var key in uiBOMHeaderValues.Keys)
                {
                    Assert.True(dbBOMHeaderValues.ContainsKey(key), $"DB does not contain expected field: {key}");
                    Assert.AreEqual(dbBOMHeaderValues[key], uiBOMHeaderValues[key],
                        $"Mismatch for field '{key}': Expected '{dbBOMHeaderValues[key]}' but found '{uiBOMHeaderValues[key]}'");
                }
                //get partition key to use further queries
                var partitionKey = dbAdminDefData.PartitionKey;
                scenarioContext["partitionKey"] = partitionKey;
            }
            catch (Exception ex)
            {
                Assert.Fail($"BOM UI data not matching wih DB with error {ex.Message}");
            }
        }

        [Then(@"User verifies the UI ""(.*)"" and Source Type as ""(.*)"" with Cosmos DB using the ""(.*)"" database and ""(.*)"" container")]
        public async Task ThenUserVerifiesTheUIAndSourceTypeAsWithCosmosDBUsingTheDatabaseAndContainer(string ModifiedDate, string source_Type, string dataBase, string container)
        {
            try
            {
                string partitionKey = scenarioContext["partitionKey"].ToString() ?? "";
                string BOMQuery = CosmosDBQueries.GetDataUsingPartitionKey(partitionKey);
                var dbBOMData = await cosmosDb.GetDataFromCosmosDB<BOMSourceRecord>(dataBase.ToLower(), container, BOMQuery);
                List<DocumentModifiedSource> documentModifiedSource = dbBOMData.DocumentModifiedSource;
                foreach (var documentModifiedSour in documentModifiedSource)
                {
                    Console.WriteLine($"documentModifiedSour is {documentModifiedSour}");
                }
                if (documentModifiedSource != null)
                {
                    var sourceType = documentModifiedSource
                                            .FirstOrDefault(item =>
                                            {
                                                return item.SourceType == source_Type;
                                            });

                    string modifiedDate = sourceType.ModifiedDate.ToString();
                    DateTime parsedDate = DateTime.Parse(modifiedDate);
                    string sourceModifiedDate = parsedDate.ToString(TimeConstants.DateFormat);

                    var uiDocumentModifiedDate = (await commonLocators.GetDocumentModifiedUserDate(ModifiedDate).TextContentAsync()).Trim();
                    Assert.AreEqual(sourceModifiedDate, uiDocumentModifiedDate, $"Mismatch: Expected Modified Date '{sourceModifiedDate}', but found '{uiDocumentModifiedDate}' in UI.");
                }
            }
            catch (Exception e)
            {
                Assert.Fail($"Modified Date and Source Type not reflecting in BestBOMPool with error {e.Message}");
            }
        }

        [Then(@"User clicks on the ""(.*)"" subtab under the ""(.*)"" main tab")]
        public async Task ThenUserClicksOnTheSubtabUnderTheMainTab(string subTab, string mainTab)
        {
            await adminToolPage.GetAdminToolSubTab(mainTab.ToLower(), subTab.ToLower()).ClickAsync();
        }

        [Then(@"User enters text into the ""(.*)"" textbox in ""(.*)"" subtab")]
        public async Task ThenUserEntersTextIntoTheTextboxInSubtab(string textField, string subTab)
        {
            testCaseName = scenarioContext.ScenarioInfo.Title;
            string getValue = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.{textField}");
            await adminToolPage.GetInternalNotes(textField.ToLower()).FillAsync(getValue);
        }
    }
}

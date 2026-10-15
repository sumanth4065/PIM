using Microsoft.Playwright;
using NUnit.Framework;
using PIMQAAutomation.Common.Pages;
using PIMQAAutomation.Helpers;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Modules.Common.Helpers;
using PIMQAAutomation.Modules.Items.Constants;
using PIMQAAutomation.Modules.Items.Pages;
using PIMQAAutomation.Utilities;
using PPT.ItemMaster.Data.Cosmos.Features.Items;
using PPT.ItemMaster.Domain.Features.DocumentSources;
using TechTalk.SpecFlow;
namespace PIMQAAutomation.Modules.Items.Actions
{
    

    /// <summary>
    /// Contains business logic and action methods for PIM Service Default document validation in Cosmos DB
    /// </summary>
    public class ItemPIMServiceDefaultActions
    {
        private readonly IPage page;
        private readonly ScenarioContext scenarioContext;
        private readonly FeatureContext featureContext;
        private readonly ItemMasterPage itemMasterPage;
        
        private readonly CosmosDbHelper cosmosDb;

        // Add a private readonly field for commonLocators
        private readonly CommonLocators commonLocators;

        public ItemPIMServiceDefaultActions(IPage page, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            this.page = page;
            this.scenarioContext = scenarioContext;
            this.featureContext = featureContext;
            itemMasterPage = new ItemMasterPage(page);
          
            cosmosDb = new CosmosDbHelper();

           
        }

        public async Task VerifyServiceFieldValuesAsync(string database, string container, string documentType, Table fieldsNames)
        {
            var itemId = string.Empty;
            try
            {
                itemId = featureContext["itemIdValue"].ToString() ?? string.Empty;
                var defaultQuery = CosmosDBQueries.GetDataUsingItemIdAndDocumentType(itemId, documentType);
                var dbServiceDefaultData = await cosmosDb.GetDataFromCosmosDB<ServiceDefaultRecord>(database.ToLower(), container, defaultQuery);

                var explicitValues = new Dictionary<string, string>();
                foreach (var fieldName in fieldsNames.Rows)
                {
                    var columnName = fieldName[0];
                    var menuLocator = columnName.ToLower();

                    if (menuLocator.Equals(ItemConstants.Status, StringComparison.OrdinalIgnoreCase))
                    {
                        var value = (await itemMasterPage.GetRightCornerInputField(menuLocator).InputValueAsync()).Trim();
                        explicitValues[columnName] = value.Equals(ItemConstants.IsActiveCheckbox, StringComparison.OrdinalIgnoreCase) ? "True" : "False";
                    }
                    else if (menuLocator == ItemConstants.ItemMasterInternalNotes)
                    {
                        explicitValues[columnName] = (await itemMasterPage.GetInputField(menuLocator).InputValueAsync()).Trim();
                    }
                    else
                    {
                        explicitValues[columnName] = (await itemMasterPage.GetDropdownInputField(menuLocator).InputValueAsync()).Trim();
                    }
                }

                var dbExplicitValues = new Dictionary<string, string>
           {
      { ItemConstants.ItemCreationValues.ItemId, dbServiceDefaultData.Item.Sku },
      { ItemConstants.ItemCreationValues.Oem, dbServiceDefaultData.Item.Oem.Name },
      { ItemConstants.ItemCreationValues.Description, dbServiceDefaultData.Description },
      { ItemConstants.ItemCreationValues.ItemStatus, dbServiceDefaultData.IsActive.ToString() },
      { ItemConstants.ItemCreationValues.InternalNotes, dbServiceDefaultData.InternalNotes }
           };

                ValidationHelper.AssertDictionariesMatch(explicitValues, dbExplicitValues, $"UI Service field values vs Database for {itemId}");
                scenarioContext["PIMDefaultId"] = dbServiceDefaultData.Id;
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"UI Service field values validation failed against database for ItemId: {itemId}.", e);
            }
        }

        public async Task VerifyServiceCreatedAndModifiedDetailsAsync(string database, string container, string documentType, Table fieldsToCheck)
        {
            var documentId = string.Empty;
            try
            {
                documentId = scenarioContext["PIMDefaultId"].ToString();
                var defaultQuery = CosmosDBQueries.GetId(documentId);
                var dbServiceDefaultData = await cosmosDb.GetDataFromCosmosDB<ServiceDefaultRecord>(database.ToLower(), container, defaultQuery);

                var explicitValues = new Dictionary<string, string>();
                foreach (var dateField in fieldsToCheck.Rows)
                {
                    var columnName = dateField[0];
                    if (columnName.Contains(ItemConstants.DocumentDateUser))
                    {
                        explicitValues[columnName] = (await commonLocators.GetCreatedModifiedUserDate(dateField[0]).TextContentAsync()).Trim();
                    }
                    else
                    {
                        explicitValues[columnName] = (await itemMasterPage.GetItemCreationDateFields(dateField[0].ToLower()).TextContentAsync()).Trim();
                    }
                }

                var dbExplicitValues = new Dictionary<string, string>
            {
    { DateFieldConstants.CreatedDate, DateTimeHelper.ConvertDbUtcToLocal(dbServiceDefaultData.Created.On.ToString().Trim()) },
    { DateFieldConstants.CreatedUser, dbServiceDefaultData.Created.By.Name.Trim() },
    { DateFieldConstants.ModifiedDate, DateTimeHelper.ConvertDbUtcToLocal(dbServiceDefaultData.Modified.On.ToString().Trim()) },
    { DateFieldConstants.ModifiedUser, dbServiceDefaultData.Modified.By.Name.Trim() },
    { DateFieldConstants.DocumentModifiedDate, DateTimeHelper.ConvertDbUtcToLocal(dbServiceDefaultData.DocumentModified.On.ToString().Trim()) },
    { DateFieldConstants.DocumentModifiedUser, dbServiceDefaultData.DocumentModified.By.Name.Trim() }
            };

                ValidationHelper.AssertDictionariesMatch(dbExplicitValues, explicitValues, "Service created and modified details vs Database");
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"Service created and modified date fields validation failed against database for document {documentId}.", e);
            }
        }

        public async Task VerifyServiceDocumentModifiedSourceAsync(string modifiedDate, string sourceType, string database, string container, string documentType)
        {
            var documentId = string.Empty;
            try
            {
                documentId = scenarioContext["PIMDefaultId"].ToString();
                var defaultQuery = CosmosDBQueries.GetId(documentId);
                var dbServiceDefaultData = await cosmosDb.GetDataFromCosmosDB<ServiceDefaultRecord>(database.ToLower(), container, defaultQuery);

                List<DocumentModifiedSource> documentModifiedSource = dbServiceDefaultData.DocumentModifiedSource;

                if (documentModifiedSource != null)
                {
                    var source = documentModifiedSource.FirstOrDefault(item => item.SourceType == sourceType);
                    ValidationHelper.FailIfNullOrEmpty("DocumentModifiedSource", source, $"SourceType '{sourceType}' not found.");

                    var sourceModifiedDate = source.ModifiedDate.ToString();
                    var dbLocalDate = DateTimeHelper.ConvertDbUtcToLocal(sourceModifiedDate);
                    var uiDocumentModifiedDate = (await commonLocators.GetDocumentModifiedUserDate(modifiedDate).TextContentAsync()).Trim();

                    ValidationHelper.AssertEqual("Validate DB and UI Date", dbLocalDate, uiDocumentModifiedDate,
                  $"Mismatch: DB Date '{dbLocalDate}', UI Date '{uiDocumentModifiedDate}'");
                }
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"Modified Date and Source Type not reflecting in {documentType} for id {documentId}", e);
            }
        }

        public async Task VerifyCheckboxStatusAsync(string documentType, Table checkboxStatus)
        {
            var itemId = string.Empty;
            try
            {
                var database = DatabaseConstants.DatabaseName;
                var container = DatabaseConstants.DefaultsContainer;
                itemId = featureContext["itemIdValue"].ToString() ?? string.Empty;

                var defaultQuery = CosmosDBQueries.GetDataUsingItemIdAndDocumentType(itemId, documentType);
                var dbItemServiceData = await cosmosDb.GetDataFromCosmosDB<ServiceDefaultRecord>(database, container, defaultQuery);

                var uiCheckboxStatus = new Dictionary<string, string>();
                foreach (var status in checkboxStatus.Rows)
                {
                    var checkbox = status[0];
                    uiCheckboxStatus[checkbox] = (await itemMasterPage.GetCheckbox(checkbox).IsCheckedAsync()) ? "True" : "False";
                }

                var dbCheckboxStatus = new Dictionary<string, string>
      {
         { ItemConstants.ItemCreationValues.IsAlias, dbItemServiceData.IsAlias.ToString() },
         { ItemConstants.ItemCreationValues.IsTest, dbItemServiceData.IsTest.ToString() }
      };

                ValidationHelper.AssertDictionariesMatch(uiCheckboxStatus, dbCheckboxStatus, "UI Alias and test checkbox status with Database");
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"UI Alias and test checkbox status validation failed against database for {itemId}.", e);
            }
        }
    }
}

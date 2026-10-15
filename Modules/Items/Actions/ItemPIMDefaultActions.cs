using Microsoft.Playwright;
using NUnit.Framework;
using OfficeOpenXml;
using PIMQAAutomation.Common.Pages;
using PIMQAAutomation.Helpers;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Modules.Common.Helpers;
using PIMQAAutomation.Modules.Items.Constants;
using PIMQAAutomation.Modules.Items.Pages;
using PIMQAAutomation.Utilities;
using PPT.ItemMaster.Data.Cosmos.Features.Items;
using TechTalk.SpecFlow;

namespace PIMQAAutomation.Modules.Items.Actions
{
    /// <summary>
    /// Contains business logic and action methods for PIM Item Default document validation in Cosmos DB
    /// </summary>
    public class ItemPIMDefaultActions
    {
        private readonly IPage page;
        private readonly ScenarioContext scenarioContext;
        private readonly FeatureContext featureContext;
        private readonly ItemMasterPage itemMasterPage;
        private readonly CommonLocators commonLocators;
        private readonly CosmosDbHelper cosmosDb;

  public ItemPIMDefaultActions(IPage page, ScenarioContext scenarioContext, FeatureContext featureContext)
  {
     this.page = page;
            this.scenarioContext = scenarioContext;
      this.featureContext = featureContext;
        itemMasterPage = new ItemMasterPage(page);
            commonLocators = new global::PIMQAAutomation.Common.Pages.CommonLocators(page);
            cosmosDb = new CosmosDbHelper();
        }

        public async Task VerifyFieldValuesInCosmosDBAsync(string database, string container, string documentType, Table fieldsToCheck)
        {
   var itemId = string.Empty;
            try
 {
itemId = featureContext["itemIdValue"].ToString() ?? string.Empty;
    var defaultQuery = CosmosDBQueries.GetDataUsingItemIdAndDocumentType(itemId, documentType);
       var dbItemDefaultData = await cosmosDb.GetDataFromCosmosDB<ItemDefaultRecord>(database.ToLower(), container, defaultQuery);

    var baselineValues = new Dictionary<string, string>();
     foreach (var fieldName in fieldsToCheck.Rows)
          {
           var columnName = fieldName[0];
            var menuLocator = columnName.ToLower();

             if (menuLocator.Equals(ItemConstants.Status, StringComparison.OrdinalIgnoreCase))
             {
        var value = (await itemMasterPage.GetRightCornerInputField(menuLocator).InputValueAsync()).Trim();
   baselineValues[columnName] = value.Equals(ItemConstants.IsActiveCheckbox, StringComparison.OrdinalIgnoreCase) ? "True" : "False";
  }
    else if (menuLocator == ItemConstants.ItemMasterInternalNotes)
     {
  baselineValues[columnName] = (await itemMasterPage.GetInputField(menuLocator).InputValueAsync()).Trim();
         }
   else if (menuLocator == ItemConstants.LocatorProperties.LOBDeff)
  {
  var isChecked = await itemMasterPage.GetDropdownInputField(menuLocator).IsCheckedAsync();
        baselineValues[columnName] = isChecked ? "True" : "False";
                  }
                  else
             {
        baselineValues[columnName] = (await itemMasterPage.GetDropdownInputField(menuLocator).InputValueAsync()).Trim();
         }
         }

      var dbBaselineValues = new Dictionary<string, string>
           {
      { ItemConstants.ItemCreationValues.ItemId, dbItemDefaultData.Item.Sku },
  { ItemConstants.ItemCreationValues.Oem, dbItemDefaultData.Item.Oem.Name },
        { ItemConstants.ItemCreationValues.Description, dbItemDefaultData.Item.Description },
   { ItemConstants.ItemCreationValues.ItemStatus, dbItemDefaultData.Item.General.IsActive.ToString() }
          };

    var tabLocator = itemMasterPage.GetTab(ItemConstants.LocatorProperties.BaseLineTab);
          var isSelected = await tabLocator.GetAttributeAsync(AttributeConstants.AriaSelected);
         if (isSelected == "true")
        {
dbBaselineValues.Add(ItemConstants.ItemCreationValues.InternalNotes, dbItemDefaultData.InternalNotes);
   dbBaselineValues.Add(ItemConstants.ItemCreationValues.LOBDeff, dbItemDefaultData.IsLOBDef.ToString());
          }

      ValidationHelper.AssertDictionariesMatch(baselineValues, dbBaselineValues, "UI Baseline values vs Database");
     scenarioContext["PIMDefaultId"] = dbItemDefaultData.Id;
        }
 catch (Exception e)
            {
                ValidationHelper.Fail($"UI Baseline values validation failed against database for ItemId: {itemId}.", e);
     }
        }

        public async Task VerifyCreatedAndModifiedDetailsAsync(string database, string container, string documentType, Table fieldsToCheck)
    {
    var documentId = string.Empty;
try
         {
         documentId = scenarioContext["PIMDefaultId"].ToString();
         var defaultQuery = CosmosDBQueries.GetId(documentId);
     var dbItemDefaultData = await cosmosDb.GetDataFromCosmosDB<ItemDefaultRecord>(database.ToLower(), container, defaultQuery);

  var baselineValues = new Dictionary<string, string>();
         foreach (var dateField in fieldsToCheck.Rows)
              {
          var columnName = dateField[0];
      if (columnName.Contains(ItemConstants.DocumentDateUser))
        {
    baselineValues[columnName] = (await commonLocators.GetCreatedModifiedUserDate(dateField[0]).TextContentAsync()).Trim();
           }
     else
     {
       baselineValues[columnName] = (await itemMasterPage.GetItemCreationDateFields(dateField[0].ToLower()).TextContentAsync()).Trim();
             }
    }

                var dbBaselineValues = new Dictionary<string, string>
{
         { DateFieldConstants.CreatedDate, DateTimeHelper.ConvertDbUtcToLocal(dbItemDefaultData.Created.On.ToString().Trim()) },
         { DateFieldConstants.CreatedUser, dbItemDefaultData.Created.By.Name.Trim() },
           { DateFieldConstants.ModifiedDate, DateTimeHelper.ConvertDbUtcToLocal(dbItemDefaultData.Modified.On.ToString().Trim()) },
       { DateFieldConstants.ModifiedUser, dbItemDefaultData.Modified.By.Name.Trim() },
 { DateFieldConstants.DocumentModifiedDate, DateTimeHelper.ConvertDbUtcToLocal(dbItemDefaultData.DocumentModified.On.ToString().Trim()) },
          { DateFieldConstants.DocumentModifiedUser, dbItemDefaultData.DocumentModified.By.Name.Trim() }
             };

     ValidationHelper.AssertDictionariesMatch(dbBaselineValues, baselineValues, "Created and modified details vs Database");
       }
     catch (Exception e)
            {
 ValidationHelper.Fail($"Created and modified date fields validation failed against database for {documentId}.", e);
            }
   }

        public async Task VerifyDocumentModifiedSourceAsync(string modifiedDate, string sourceType, string database, string container, string documentType)
        {
        var documentId = string.Empty;
       List<PPT.ItemMaster.Domain.Features.DocumentSources.DocumentModifiedSource> documentModifiedSource = null;

            try
         {
          documentId = scenarioContext["PIMDefaultId"].ToString();
                var defaultQuery = CosmosDBQueries.GetId(documentId);

        if (documentType.Contains("Taxonomy"))
             {
 var dbTaxonomyDefaultData = await cosmosDb.GetDataFromCosmosDB<TaxonomyDefaultRecord>(database.ToLower(), container, defaultQuery);
        documentModifiedSource = dbTaxonomyDefaultData.DocumentModifiedSource;
         }
     else
      {
    var dbItemDefaultData = await cosmosDb.GetDataFromCosmosDB<ItemDefaultRecord>(database.ToLower(), container, defaultQuery);
       documentModifiedSource = dbItemDefaultData.DocumentModifiedSource;
    }

          if (documentModifiedSource != null)
        {
       var source = documentModifiedSource.FirstOrDefault(item => item.SourceType == sourceType);
 ValidationHelper.FailIfNullOrEmpty("DocumentModifiedSource", source, $"SourceType '{sourceType}' not found in document.");

               string sourceModifiedDate = source.ModifiedDate.ToString();
      var dbLocalDate = DateTimeHelper.ConvertDbUtcToLocal(sourceModifiedDate);
           string uiDocumentModifiedDate = (await commonLocators.GetDocumentModifiedUserDate(modifiedDate).TextContentAsync()).Trim();

 ValidationHelper.AssertEqual("Document Modified Date", dbLocalDate, uiDocumentModifiedDate,
             $"Mismatch: DB Date '{dbLocalDate}', UI Date '{uiDocumentModifiedDate}'");
    }
            }
        catch (Exception e)
        {
   ValidationHelper.Fail($"Modified Date and Source Type not reflecting in {container} container for {documentId}.", e);
        }
        }

 public async Task VerifyProductTaxonomyDetailsAsync(string taxonomy, string database, string container, string documentType, Table fieldsToCheck)
        {
   var documentId = string.Empty;
    try
      {
      documentId = scenarioContext["PIMDefaultId"].ToString();
       var defaultQuery = CosmosDBQueries.GetId(documentId);
        var dbItemDefaultData = await cosmosDb.GetDataFromCosmosDB<ItemDefaultRecord>(database.ToLower(), container, defaultQuery);

      var baselineValues = new Dictionary<string, string>();
                foreach (var dateField in fieldsToCheck.Rows)
        {
             var columnName = dateField[0];
  baselineValues[columnName] = (await itemMasterPage.GetTaxonomyField(taxonomy.ToLower(), dateField[0].ToLower()).InputValueAsync()).Trim();
        }

    var dbBaselineValues = new Dictionary<string, string>
          {
       { ItemConstants.ProductTaxonomyFields.Oem, dbItemDefaultData.Item.Oem.Name },
              { ItemConstants.ProductTaxonomyFields.Family, dbItemDefaultData.Item.Taxonomy.Family.Name },
        { ItemConstants.ProductTaxonomyFields.Line, dbItemDefaultData.Item.Taxonomy.Line.Name },
           { ItemConstants.ProductTaxonomyFields.Group, dbItemDefaultData.Item.Taxonomy.Group.Name }
         };

      ValidationHelper.AssertDictionariesMatch(baselineValues, dbBaselineValues, $"UI Product Taxonomy {taxonomy} details vs Database");
            }
 catch (Exception e)
     {
     ValidationHelper.Fail($"UI Product Taxonomy {taxonomy} details validation failed against database for {documentId}.", e);
         }
        }

        public async Task VerifyServiceTaxonomyDetailsAsync(string taxonomy, string database, string container, string documentType, Table fieldsToCheck)
        {
  var itemId = string.Empty;
         try
            {
       itemId = featureContext["itemIdValue"].ToString() ?? string.Empty;
             var defaultQuery = CosmosDBQueries.GetDataUsingItemIdAndDocumentType(itemId, documentType);
                var dbItemDefaultData = await cosmosDb.GetDataFromCosmosDB<ItemDefaultRecord>(database.ToLower(), container, defaultQuery);

       var baselineValues = new Dictionary<string, string>();
   foreach (var dateField in fieldsToCheck.Rows)
          {
        var columnName = dateField[0];
             baselineValues[columnName] = (await itemMasterPage.GetTaxonomyField(taxonomy.ToLower(), dateField[0].ToLower()).InputValueAsync()).Trim();
    }

                var dbBaselineValues = new Dictionary<string, string>
    {
         { ItemConstants.ServiceTaxonomyFields.Class, dbItemDefaultData.Item.ServiceTaxonomy.ServiceClass.Name },
      { ItemConstants.ServiceTaxonomyFields.Domain, dbItemDefaultData.Item.ServiceTaxonomy.ServiceDomain.Name },
           { ItemConstants.ServiceTaxonomyFields.Type, dbItemDefaultData.Item.ServiceTaxonomy.ServiceType.Name },
             { ItemConstants.ServiceTaxonomyFields.Item, dbItemDefaultData.Item.ServiceTaxonomy.ServiceItem.Name }
     };

  ValidationHelper.AssertDictionariesMatch(baselineValues, dbBaselineValues, $"UI Service Taxonomy {taxonomy} details vs Database");
            }
     catch (Exception e)
    {
             ValidationHelper.Fail($"UI Service Taxonomy {taxonomy} details validation failed against database for ItemId: {itemId}.", e);
            }
        }

        public async Task VerifyTaxonomyDetailsInCosmosDBAsync(string taxonomyName, string database, string container, string documentType, Table fields)
      {
 var documentId = string.Empty;
  try
            {
                var uiValues = new Dictionary<string, string>();
      foreach (var row in fields.Rows)
   {
               var fieldName = row[0];
    var fieldKey = fieldName.ToLower();
          var uiValue = (await itemMasterPage.GetTaxonomyField(taxonomyName.ToLower(), fieldKey).InputValueAsync()).Trim();
         uiValues[fieldName] = uiValue;
      }

 documentId = scenarioContext["PIMDefaultId"].ToString();
      var query = CosmosDBQueries.GetId(documentId);
         var dbItem = await cosmosDb.GetDataFromCosmosDB<ItemDefaultRecord>(database, container, query);

           var dbCharacteristics = dbItem?.Characteristics?
       .Where(c => (bool)c.Characteristic?.Ref?.PartitionKey.Contains(taxonomyName, StringComparison.OrdinalIgnoreCase)).ToList();

                ValidationHelper.FailIfNullOrEmpty("DB Characteristics", dbCharacteristics, $"No DB characteristics found for taxonomy '{taxonomyName}' in document {documentId}.");

             foreach (var field in fields.Rows)
           {
         var fieldName = field[0];
          var expectedValue = uiValues[fieldName];

  var matchFound = dbCharacteristics.Any(c =>
           (bool)c.Characteristic?.Name.Equals(expectedValue, StringComparison.OrdinalIgnoreCase));

        Assert.IsTrue(
      matchFound,
   $"Field '{fieldName}' with value '{expectedValue}' not found in DB for taxonomy '{taxonomyName}'.");
              }
   }
          catch (Exception e)
            {
                ValidationHelper.Fail($"UI Taxonomy values validation failed for '{taxonomyName}' against database for document {documentId}.", e);
     }
      }

  public async Task VerifyCharacteristicValuesAsync(string database, string container, string documentType, Table table)
        {
          var itemId = string.Empty;
        List<PPT.ItemMaster.Data.Domain.Features.Common.Characteristics> dbCharacteristics = null;

            try
     {
        if (documentType.Contains("Taxonomy"))
            {
 var testCaseName = scenarioContext.ScenarioInfo.Title;
             var oemName = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.Oem");
       var groupName = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.Group");
           var defaultQuery = CosmosDBQueries.GetDataUsingOemAndGroupName(oemName, groupName);
     var dbTaxonomyDefaultData = await cosmosDb.GetDataFromCosmosDB<TaxonomyDefaultRecord>(database.ToLower(), container, defaultQuery);
       scenarioContext["PIMDefaultId"] = dbTaxonomyDefaultData.Id;
     featureContext["CharacteristicType"] = await itemMasterPage.GetCharacteristicField("type").InputValueAsync();
     dbCharacteristics = dbTaxonomyDefaultData.Characteristics;
         }
           else if (documentType.Contains("Service"))
       {
     itemId = featureContext["itemIdValue"].ToString() ?? string.Empty;
        var lob = featureContext["LOB"].ToString() ?? string.Empty;
             var defaultQuery = CosmosDBQueries.GetDataUsingItemIdLOBAndDocumentType(itemId, lob, documentType);
    var dbServiceDefaultData = await cosmosDb.GetDataFromCosmosDB<ServiceDefaultRecord>(database.ToLower(), container, defaultQuery);
               scenarioContext["PIMDefaultId"] = dbServiceDefaultData.Id;
       dbCharacteristics = dbServiceDefaultData.Characteristics;
              }
     else
        {
                  itemId = featureContext["itemIdValue"].ToString() ?? string.Empty;
          var defaultQuery = CosmosDBQueries.GetDataUsingItemIdAndDocumentType(itemId, documentType);
          var dbItemDefaultData = await cosmosDb.GetDataFromCosmosDB<ItemDefaultRecord>(database.ToLower(), container, defaultQuery);
   scenarioContext["PIMDefaultId"] = dbItemDefaultData.Id;
              dbCharacteristics = dbItemDefaultData.Characteristics;
                }

   var characteristicName = featureContext["CharacteristicType"].ToString();
    var matchingDbCharacteristic = dbCharacteristics.FirstOrDefault(c => c.Characteristic.Name == characteristicName);

       ValidationHelper.FailIfNullOrEmpty("DB Characteristic", matchingDbCharacteristic,
  $"Characteristic '{characteristicName}' not found in {documentType} for itemId: {itemId}");

    var dbCharacteristicValues = new Dictionary<string, string>
      {
        { ItemConstants.CharacteristicValues.Value, matchingDbCharacteristic.Value },
         { ItemConstants.CharacteristicValues.InternalNotes, matchingDbCharacteristic.Notes },
              { ItemConstants.CharacteristicValues.IsActive, matchingDbCharacteristic.IsActive.ToString() },
     { ItemConstants.CharacteristicValues.ScopeObsolete, matchingDbCharacteristic.ScopeObsolete.ToString() }
      };

     var uiCharacteristicValues = new Dictionary<string, string>();
     foreach (var row in table.Rows)
       {
      var fieldName = row[0];
     var fieldLocator = fieldName.ToLower();

             if (fieldLocator.Equals(ItemConstants.LocatorProperties.NumberCharacteristic, StringComparison.OrdinalIgnoreCase) ||
   fieldLocator.Equals(ItemConstants.LocatorProperties.BooleanCharacteristic, StringComparison.OrdinalIgnoreCase) ||
              fieldLocator.Equals(ItemConstants.LocatorProperties.StringCharacteristic, StringComparison.OrdinalIgnoreCase) ||
    fieldLocator.Equals(ItemConstants.LocatorProperties.DateCharacteristic, StringComparison.OrdinalIgnoreCase))
            {
             fieldName = ItemConstants.LocatorProperties.CharacteristicValue;
    }

    var value = fieldLocator switch
              {
     ItemConstants.ItemMasterInternalNotes => (await itemMasterPage.GetCharacteristicTextAreaField(fieldLocator).InputValueAsync()).Trim(),
          ItemConstants.IsActiveCheckbox or ItemConstants.ScopeObsolete => (await itemMasterPage.GetCharacteristicCheckbox(fieldLocator).IsCheckedAsync()).ToString(),
          _ => (await itemMasterPage.GetCharacteristicField(fieldLocator).InputValueAsync()).Trim()
         };

             uiCharacteristicValues[fieldName] = value;
                }

           ValidationHelper.AssertDictionariesMatch(uiCharacteristicValues, dbCharacteristicValues, "UI Characteristic values vs Database");
    }
        catch (Exception e)
          {
        ValidationHelper.Fail($"Characteristic values validation failed against database for itemId: {itemId}.", e);
    }
    }

        public async Task VerifyExcelCharacteristicValuesAsync(string tabName, string fileName, string database, string container, string documentType, Table fields)
     {
 string documentId = null;
            List<PPT.ItemMaster.Data.Domain.Features.Common.Characteristics> dbCharacteristics = null;

   try
        {
        ExcelPackage.LicenseContext = LicenseContext.Commercial;
      var excelData = Utility.ReadExcel(FileUtil.GetImportExcelFilePath(scenarioContext, fileName));

   if (documentType.Contains("Taxonomy"))
        {
  var oemName = excelData[0]["OEM"];
           var groupName = excelData[0]["Group"];
        var defaultQuery = CosmosDBQueries.GetDataUsingOemAndGroupName(oemName, groupName);
        var dbTaxonomyDefaultData = await cosmosDb.GetDataFromCosmosDB<TaxonomyDefaultRecord>(database, container, defaultQuery);
        scenarioContext["PIMDefaultId"] = dbTaxonomyDefaultData.Id;
              dbCharacteristics = dbTaxonomyDefaultData.Characteristics;
    }
     else
       {
        documentId = scenarioContext["PIMDefaultId"].ToString();
               var defaultQuery = CosmosDBQueries.GetId(documentId);
         var dbDefaultData = await cosmosDb.GetDataFromCosmosDB<ServiceDefaultRecord>(database.ToLower(), container, defaultQuery);
    dbCharacteristics = dbDefaultData.Characteristics;
  }

     ValidationHelper.FailIfNullOrEmpty("DB Characteristics", dbCharacteristics,
           $"No characteristics found in document '{documentId}' for document type '{documentType}'.");

      var dbCharacteriticValuesList = dbCharacteristics
   .Select(c => new Dictionary<string, string>
              {
            { "value", c.Value },
              { "notes", c.Notes },
      { "name", c.Characteristic?.Name },
  { "active-icon", c.IsActive.ToString() },
         { "scope-active-icon", c.ScopeObsolete.ToString() }
    }).ToList();

           var uiAllRowsList = new List<Dictionary<string, string>>();

     foreach (var row in excelData)
     {
       var kind = row["Characteristic Kind"].Trim();
          var type = row["Characteristic Type"].Trim();
        var expectedValue = row["Value"].Trim();

 await page.WaitForLoadStateAsync();
       await itemMasterPage.GetCharacteristicKind(kind).ClickAsync();
   await itemMasterPage.GetCharacteristicType(type).ClickAsync();

       var uiValuesDict = new Dictionary<string, string>();

      foreach (var menu in fields.Rows)
        {
             var fieldName = menu[0].ToLower();
       var locator = itemMasterPage.GetDefaultCharacteristicsFields(tabName.ToLower(), fieldName);
   await locator.ScrollIntoViewIfNeededAsync();

            var uiValue = fieldName switch
{
   "value" => (await locator.TextContentAsync())?.Trim() ?? string.Empty,
     "notes" => (await locator.EvaluateAsync<string>("el => el.value"))?.Trim() ?? string.Empty,
     _ when fieldName.Contains("scope-active") => (await locator.IsVisibleAsync()) ? "False" : "True",
     _ when fieldName.Contains("active") => (await locator.IsVisibleAsync()) ? "True" : "False",
                 _ => string.Empty
          };

    uiValuesDict[fieldName] = uiValue;
         }

     uiValuesDict["name"] = type;
       uiAllRowsList.Add(uiValuesDict);
      }

                foreach (var uiRow in uiAllRowsList)
         {
var typeName = uiRow["name"];
         var matchingDbRow = dbCharacteriticValuesList.FirstOrDefault(dbRow =>
      dbRow.TryGetValue("name", out var dbName) && dbName.Equals(typeName, StringComparison.OrdinalIgnoreCase));

          Assert.IsNotNull(matchingDbRow, $"No matching DB record found for characteristic type '{typeName}'.");

       foreach (var kv in uiRow)
             {
              var field = kv.Key;
            var uiValue = kv.Value;
          var dbValue = matchingDbRow.ContainsKey(field) ? matchingDbRow[field] : null;
            ValidationHelper.AssertEqual(field, dbValue, uiValue,
        $"Mismatch in '{typeName}' for field '{field}': Expected: '{dbValue}' but found: '{uiValue}'");
     }
         }
            }
            catch (Exception ex)
            {
 ValidationHelper.Fail($"Characteristic values validation failed for {documentType} document.", ex);
            }
        }

        public async Task VerifyCharacteristicTypeValueAsync(string locatorType, string database, string container, string documentType)
 {
 var itemId = string.Empty;
     try
            {
        itemId = featureContext["itemIdValue"].ToString();
      var characteristicName = featureContext["CharacteristicType"].ToString();

  var query = CosmosDBQueries.GetDataUsingItemIdAndDocumentType(itemId, documentType);
var record = await cosmosDb.GetDataFromCosmosDB<ItemDefaultRecord>(database.ToLower(), container, query);

     var dbValue = record.Characteristics
      .First(c => c.Characteristic.Name.Equals(characteristicName, StringComparison.OrdinalIgnoreCase))
       .Value.Trim();

       var uiValue = ContextHelper.GetContextValue(featureContext, "Value");

    Assert.That(uiValue, Is.EqualTo(dbValue).IgnoreCase,
      $"Expected '{characteristicName}' to be '{dbValue}', but found '{uiValue}' in UI.");
  }
            catch (Exception ex)
            {
          ValidationHelper.Fail($"Characteristic type value not getting in {database}>{container} for itemId: {itemId}.", ex);
          }
        }

  public async Task VerifyCharacteristicValueInDocumentAsync(string documentType, Table characteristicValues)
        {
     var itemId = string.Empty;
            List<PPT.ItemMaster.Data.Domain.Features.Common.Characteristics> dbCharacteristics = null;

   try
      {
           var database = DatabaseConstants.DatabaseName;
          var container = DatabaseConstants.DefaultsContainer;

                if (documentType.Contains(ItemConstants.LevelLineageValue.Taxonomy, StringComparison.OrdinalIgnoreCase))
        {
    var testCaseName = scenarioContext.ScenarioInfo.Title;
               var oemName = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.Oem");
              var groupName = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.Group");
    var defaultQuery = CosmosDBQueries.GetDataUsingOemAndGroupName(oemName, groupName);
  var dbTaxonomyDefaultData = await cosmosDb.GetDataFromCosmosDB<TaxonomyDefaultRecord>(database, container, defaultQuery);
           scenarioContext["PIMDefaultId"] = dbTaxonomyDefaultData.Id;
 dbCharacteristics = dbTaxonomyDefaultData.Characteristics;
       }
           else if (documentType.Contains(ItemConstants.LevelLineageValue.Service, StringComparison.OrdinalIgnoreCase))
            {
         itemId = featureContext["itemIdValue"].ToString() ?? string.Empty;
      var lob = featureContext["LOB"].ToString() ?? string.Empty;
     var defaultQuery = CosmosDBQueries.GetDataUsingItemIdLOBAndDocumentType(itemId, lob, documentType);
            var dbServiceDefaultData = await cosmosDb.GetDataFromCosmosDB<ServiceDefaultRecord>(database, container, defaultQuery);
          scenarioContext["PIMDefaultId"] = dbServiceDefaultData.Id;
              dbCharacteristics = dbServiceDefaultData.Characteristics;
          }
       else
      {
     itemId = featureContext["itemIdValue"].ToString() ?? string.Empty;
      var defaultQuery = CosmosDBQueries.GetDataUsingItemIdAndDocumentType(itemId, documentType);
var dbItemDefaultData = await cosmosDb.GetDataFromCosmosDB<ItemDefaultRecord>(database, container, defaultQuery);
      scenarioContext["PIMDefaultId"] = dbItemDefaultData.Id;
          dbCharacteristics = dbItemDefaultData.Characteristics;
    }

         foreach (var value in characteristicValues.Rows)
      {
  var expectedCharacteristicName = value[0];
         var expectedValue = value[1];

        var matchingDbCharacteristic = dbCharacteristics
     .FirstOrDefault(c => c.Characteristic.Name.Equals(expectedCharacteristicName, StringComparison.OrdinalIgnoreCase));

              ValidationHelper.FailIfNullOrEmpty($"Characteristic '{expectedCharacteristicName}'", matchingDbCharacteristic,
      $"not found in {documentType} document for itemId: {itemId}");

              var actualValue = matchingDbCharacteristic.Value;

      if (!string.Equals(actualValue, expectedValue, StringComparison.OrdinalIgnoreCase))
    {
      ValidationHelper.Fail(
   $"Mismatch for Characteristic '{expectedCharacteristicName}' in {documentType}. " +
        $"Expected: '{expectedValue}', Found: '{actualValue}'");
                  }
              }
    }
         catch (Exception e)
            {
       ValidationHelper.Fail($"Characteristic values validation failed against {documentType} document for itemId: {itemId}.", e);
            }
 }

        public async Task VerifyGeneralFlagsAsync(string documentType, string database, string container, Table generalDetails)
        {
            try
            {
   await DatabaseValidationhelper.VerifyGeneralFlagsAsync<ItemDefaultRecord>(
             featureContext,
               itemMasterPage,
     cosmosDb,
        documentType,
        database,
         container,
             generalDetails);
            }
       catch (Exception ex)
     {
   ValidationHelper.Fail($"Failed to validate Product/Component flags between UI and DB for ItemId '{featureContext["itemIdValue"]}'.", ex);
            }
        }
    }
}

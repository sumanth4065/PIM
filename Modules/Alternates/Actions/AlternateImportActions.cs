namespace PIMQAAutomation.Modules.Alternates.Actions
{
    using Microsoft.Playwright;
    using NUnit.Framework;
    using OfficeOpenXml;
    using PIMQAAutomation.Common.Pages;
    using PIMQAAutomation.Modules.Alternates.Constants;
    using PIMQAAutomation.Modules.Alternates.Pages;
    using PIMQAAutomation.Utilities;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Contains business logic and action methods for Alternate Import operations
    /// </summary>
 public class AlternateImportActions
    {
        private readonly IPage page;
        private readonly ScenarioContext scenarioContext;
        private readonly FeatureContext featureContext;
        private readonly AlternatePage alternatePage;
 private readonly CommonLocators commonLocators;
        private readonly AlternateCommonActions commonActions;

        public AlternateImportActions(IPage page, ScenarioContext scenarioContext, FeatureContext featureContext, AlternateCommonActions commonActions)
    {
            this.page = page;
     this.scenarioContext = scenarioContext;
 this.featureContext = featureContext;
            this.commonActions = commonActions;
   alternatePage = new AlternatePage(page);
  commonLocators = new CommonLocators(page);
        }

        #region Excel Data Loading

 /// <summary>
        /// Recalls lookup item from Excel for Alternate panel
     /// </summary>
        public async Task RecallLookupItemFromExcelAsync(string excelFileName, string panelName, int rowIndex, Table inputParametersList)
        {
  ExcelPackage.LicenseContext = LicenseContext.Commercial;
        var excelData = Utility.ReadExcel(FileUtil.GetImportExcelFilePath(scenarioContext, excelFileName));

            ValidationHelper.FailIfNullOrEmpty("Excel data", excelData, $"Excel file '{excelFileName}' is empty or not found.");

   if (rowIndex < 1 || rowIndex > excelData.Count)
{
        throw new ArgumentOutOfRangeException(nameof(rowIndex), $"Invalid row index: {rowIndex}. Excel has {excelData.Count} data rows.");
            }

       var dataRow = excelData[rowIndex - 1];
    var lookupItem = dataRow["Lookup Item"];

       foreach (var row in inputParametersList.Rows)
     {
        var fieldName = row["FieldName"].ToLower();
      var inputField = alternatePage.GetTextField(panelName.ToLower(), fieldName);

      await inputField.ClickAsync();
    await BasePage.TypeTextAsync(inputField, lookupItem);
       await page.WaitForLoadStateAsync();
    await page.WaitForTimeoutAsync(AlternatesConstants.LowWaitTimeMs);

            var isDropdownOpen = await commonLocators.GetDropdownListBox().IsVisibleAsync();
          if (isDropdownOpen)
        {
       await commonLocators.GetSelectDropdownOption(lookupItem).ClickAsync();
 }
    }

  await page.WaitForTimeoutAsync(AlternatesConstants.LowWaitTimeMs);
  }

 /// <summary>
     /// Loads data from Excel with two items
        /// </summary>
   public async Task LoadExcelDataWithTwoItemsAsync(string excelFileName, int rowIndex, string item1Key, string item2Key)
    {
            try
            {
    ExcelPackage.LicenseContext = LicenseContext.Commercial;
     var excelData = Utility.ReadExcel(FileUtil.GetImportExcelFilePath(scenarioContext, excelFileName));

       ValidationHelper.FailIfNullOrEmpty("Excel data", excelData, $"Excel file '{excelFileName}' is empty or not found.");

if (rowIndex < 1 || rowIndex > excelData.Count)
       {
    throw new ArgumentOutOfRangeException(nameof(rowIndex), $"Invalid row index: {rowIndex}. Excel has {excelData.Count} rows.");
    }

       var row = excelData[rowIndex - 1];

  // Extract values
        var item1 = row["Lookup Item"];
   var item2 = row["Item"];
      var lookUpOem = row["Lookup OEM"];
          var oem = row["OEM"];

     // Build dictionary
            var itemObject1 = new Dictionary<string, string>
 {
             ["Item-Id"] = item1,
         ["Oem"] = lookUpOem
   };

         var itemObject2 = new Dictionary<string, string>
            {
      ["Item-Id"] = item2,
          ["Oem"] = oem
     };

    // Store full object in FeatureContext with both keys
       featureContext[item1Key] = itemObject1;
       featureContext[item2Key] = itemObject2;
     }
 catch (Exception ex)
      {
          ValidationHelper.Fail($"Error during loading data from '{excelFileName}' for LookUp Item: {item1Key} and Item: {item2Key} at row index {rowIndex}. Message: {ex.Message}", ex);
         }
        }

      /// <summary>
        /// Loads data from Excel with single item
        /// </summary>
        public async Task LoadExcelDataWithSingleItemAsync(string excelFileName, int rowIndex, string item)
        {
   try
 {
           ExcelPackage.LicenseContext = LicenseContext.Commercial;
              var excelData = Utility.ReadExcel(FileUtil.GetImportExcelFilePath(scenarioContext, excelFileName));

  ValidationHelper.FailIfNullOrEmpty("Excel data", excelData, $"Excel file '{excelFileName}' is empty or not found.");

                if (rowIndex < 1 || rowIndex > excelData.Count)
     {
 throw new ArgumentOutOfRangeException(nameof(rowIndex), $"Invalid row index: {rowIndex}. Excel has {excelData.Count} rows.");
}

    var row = excelData[rowIndex - 1];

           if (!row.ContainsKey("Item ID"))
 {
       throw new KeyNotFoundException("The 'Item ID' column was not found in the Excel row.");
    }

   var itemId = row["Item ID"];
     var oem = row["OEM"];
       var description = row["Description"];

     var itemObject = new Dictionary<string, string>
     {
["Item-Id"] = itemId,
   ["Oem"] = oem,
      ["Description"] = description
   };

                featureContext[item] = itemObject;
          }
    catch (Exception ex)
     {
              ValidationHelper.Fail($"Error loading Excel data {ex.Message}", ex);
         }
        }

        #endregion

      #region Cluster Item Validation

        /// <summary>
     /// Validates cluster item rows from Excel
        /// </summary>
        public async Task ValidateClusterItemRowsFromExcelAsync(string panelName, string excelFileName, int rowIndex, Table inputParametersList)
  {
  ExcelPackage.LicenseContext = LicenseContext.Commercial;
      var excelData = Utility.ReadExcel(FileUtil.GetImportExcelFilePath(scenarioContext, excelFileName));

            ValidationHelper.FailIfNullOrEmpty("Excel data", excelData, $"Excel file '{excelFileName}' is empty or not found.");

          if (rowIndex < 1 || rowIndex > excelData.Count)
 {
       throw new ArgumentOutOfRangeException(nameof(rowIndex), $"Invalid row index: {rowIndex}. Excel has {excelData.Count} rows.");
            }

            var row = excelData[rowIndex - 1];

       string[] itemKeys = ["Lookup Item", "Item"];
 for (var i = 0; i < itemKeys.Length; i++)
      {
       var key = itemKeys[i];
       if (!row.ContainsKey(key))
         {
           continue;
      }

    var itemId = row[key];
    var expectedFields = GetExpectedValuesFromTable(inputParametersList, row, i);
            featureContext[itemId] = expectedFields;

      await VerifyItemDetailsInClusterTableAsync(itemId, panelName, inputParametersList);
            }
        }

        /// <summary>
        /// Verifies item details in cluster table
        /// </summary>
   public async Task VerifyItemDetailsInClusterTableAsync(string itemId, string panelName, Table table)
   {
   var rowLocator = alternatePage.GetItemRowDetails(panelName.ToLower(), itemId);
            var rowDetails = await AlternateGridActions.GetRowDetailsAsDictionaryAsync(rowLocator);

            var expectedFields = featureContext[itemId] as Dictionary<string, string>;

    foreach (var menu in table.Rows)
   {
   var fieldName = menu["FieldNames"];
      var expectedValue = string.Empty;

    if (menu["FieldNames"] == menu["Values"])
   {
   expectedValue = commonActions.GetFieldValue(itemId, fieldName);
  }
    else
       {
 expectedValue = expectedFields.ContainsKey(fieldName) ? expectedFields[fieldName] : menu["Values"];
   }

    if (!rowDetails.ContainsKey(fieldName))
        {
  throw new Exception($"UI field '{fieldName}' not found for item '{itemId}'");
 }

var actualValue = rowDetails[fieldName];

 Assert.AreEqual(expectedValue.ToLower(), actualValue.ToLower(),
    $"Mismatch for {fieldName}. Expected: {expectedValue}, Actual: {actualValue}");
       }
   }

        #endregion

        #region Helper Methods

        private Dictionary<string, string> GetExpectedValuesFromTable(Table table, Dictionary<string, string> excelRow, int rowIndex)
     {
   var result = new Dictionary<string, string>();

       foreach (var row in table.Rows)
   {
     var fieldName = row["FieldNames"];
 var excelKeyOrValues = row["Values"];
var rawValue = string.Empty;

  if (excelKeyOrValues.Contains(","))
 {
 var values = excelKeyOrValues.Split(',').Select(v => v.Trim()).ToArray();

  if (rowIndex >= values.Length)
   {
       continue;
    }

    rawValue = values[rowIndex];
ValidationHelper.FailIfNullOrEmpty("rawValue", rawValue, $"rawValue is null or empty for row index {rowIndex} in table.");
   }
                else
   {
      rawValue = excelKeyOrValues.Trim();
   }

     string finalValue;
      if (excelRow.ContainsKey(rawValue))
     {
     finalValue = excelRow[rawValue];
   }
     else
          {
  finalValue = rawValue;
     }

   finalValue = NormalizeBoolean(finalValue);
   result[fieldName] = finalValue;
      }

  return result;
        }

        private string NormalizeBoolean(string value)
 {
      switch (value?.Trim().ToUpper())
     {
  case "Y":
      return "true";
    case "N":
       return "false";
    default:
 return value?.Trim();
  }
        }

        #endregion
    }
}

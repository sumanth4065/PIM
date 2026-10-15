namespace PIMQAAutomation.Modules.Alternates.Actions
{
    using Microsoft.Playwright;
    using NUnit.Framework;
    using PIMQAAutomation.Common.Pages;
    using PIMQAAutomation.Modules.Alternates.Pages;
    using PIMQAAutomation.Modules.Common.Constants;
    using PIMQAAutomation.Modules.Items.Constants;
    using PIMQAAutomation.Utilities;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Contains business logic and action methods for Alternate Cluster Mode UI operations
    /// </summary>
    public class AlternateClusterModeActions
 {
        private readonly IPage page;
      private readonly ScenarioContext scenarioContext;
        private readonly FeatureContext featureContext;
      private readonly AlternatePage alternatePage;
     private readonly CommonLocators commonLocators;
        private readonly AlternateCommonActions commonActions;

     public AlternateClusterModeActions(IPage page, ScenarioContext scenarioContext, FeatureContext featureContext, AlternateCommonActions commonActions)
        {
       this.page = page;
  this.scenarioContext = scenarioContext;
  this.featureContext = featureContext;
            this.commonActions = commonActions;
    alternatePage = new AlternatePage(page);
         commonLocators = new CommonLocators(page);
        }

        #region Tab Navigation

    /// <summary>
        /// Selects a tab from cluster mode
        /// </summary>
        public async Task SelectTabAsync(string pageTab)
        {
            await alternatePage.GetTab(pageTab.ToLower()).ClickAsync();
   }

        /// <summary>
        /// Selects a tab from a specific panel
        /// </summary>
    public async Task SelectPanelTabAsync(string panelName, string tabName)
     {
      await alternatePage.GetPanelTab(panelName.ToLower(), tabName.ToLower()).ClickAsync();
        }

    /// <summary>
 /// Verifies that a tab is displayed
 /// </summary>
        public async Task VerifyTabDisplayedAsync(string tabName)
  {
    await page.WaitForLoadStateAsync();
       try
            {
   await Assertions.Expect(alternatePage.GetTab(tabName.ToLower()))
       .ToHaveAttributeAsync("aria-selected", "true");
      }
         catch (Exception ex)
     {
   ValidationHelper.Fail($"[FAIL] '{tabName}' tab is not displayed. Playwright error: {ex.Message}", ex);
  }
        }

        #endregion

        #region Field Verification

    /// <summary>
  /// Verifies that text fields are empty in a specified panel
/// </summary>
        public async Task VerifyTextFieldsEmptyAsync(string panelName, Table table)
        {
            await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);
            foreach (var row in table.Rows)
            {
  var fieldName = row[0];
        var fieldLocator = fieldName.ToLower();
                var actualValue = await alternatePage
           .GetTextField(panelName.ToLower(), fieldLocator)
     .InputValueAsync();

    if (!string.IsNullOrWhiteSpace(actualValue))
{
              ValidationHelper.Fail($"{fieldName} is expected to be empty in panel '{panelName}', but found: '{actualValue}'");
      }
            }
 }

        /// <summary>
        /// Verifies field details in a panel
   /// </summary>
        public async Task VerifyFieldDetailsAsync(string panelName, Table table)
  {
            await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);
            await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
     foreach (var row in table.Rows)
   {
       var fieldName = row[0];
    var expectedValue = row[1];

     var actualValue = await alternatePage
   .GetTextField(panelName.ToLower(), fieldName.ToLower())
             .InputValueAsync();

         ValidationHelper.AssertEqual(fieldName, expectedValue, actualValue, $"Panel: '{panelName}'");
   }
        }

        /// <summary>
      /// Verifies that the same item is populated in a panel
        /// </summary>
        public async Task VerifySameItemPopulatedAsync(string item, string panelName, Table table)
        {
   await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);
     foreach (var row in table.Rows)
    {
       var fieldName = row[0];

       var selectClusterItemId = await alternatePage
   .GetTextField(panelName.ToLower(), fieldName.ToLower())
    .InputValueAsync();

   var itemMasterItemId = commonActions.GetFieldValue(item, fieldName);

   ValidationHelper.AssertEqual(
     fieldName,
     itemMasterItemId,
  selectClusterItemId,
 $"Mismatch for field '{fieldName}': Expected '{itemMasterItemId}' but found '{selectClusterItemId}'"
       );
  }
        }

        #endregion

        #region Button Operations

        /// <summary>
        /// Verifies that buttons are disabled in a panel
        /// </summary>
public async Task VerifyButtonsDisabledAsync(string panelName, Table table)
        {
            foreach (var row in table.Rows)
          {
        var buttonName = row[0];
                var buttonLocator = buttonName.ToLower();

       var isDisabled = await alternatePage
     .GetButton(panelName.ToLower(), buttonLocator)
         .IsDisabledAsync();

          if (!isDisabled)
            {
      ValidationHelper.Fail($"{buttonName} button in panel '{panelName}' is expected to be disabled but is enabled.");
                }
 }
 }

        /// <summary>
      /// Verifies that buttons are enabled in a panel
      /// </summary>
        public async Task VerifyButtonsEnabledAsync(string panelName, Table table)
        {
       await page.WaitForLoadStateAsync();
   foreach (var row in table.Rows)
            {
      await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
       await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);

    var buttonName = row[0];
      var buttonLocator = buttonName.ToLower();

        var isEnabled = await alternatePage
          .GetButton(panelName.ToLower(), buttonLocator)
       .IsEnabledAsync();

   if (!isEnabled)
            {
 ValidationHelper.Fail($"{buttonName} button in panel '{panelName}' is expected to be enabled but is disabled.");
     }
    }
        }

        /// <summary>
    /// Clicks a button in a specified panel
        /// </summary>
    public async Task ClickButtonAsync(string buttonName, string panelName)
     {
     await alternatePage.GetButton(panelName.ToLower(), buttonName.ToLower()).ClickAsync();
    }

        #endregion

        #region Data Entry

        /// <summary>
        /// Enters item data for fields in a specified panel
        /// </summary>
        public async Task EnterItemDataAsync(string item, string panelName, Table table)
 {
      await page.WaitForLoadStateAsync();
    var fieldLocator = string.Empty;
     var inputValue = string.Empty;
foreach (var fieldName in table.Rows)
            {
    fieldLocator = fieldName[0].ToLower();
  inputValue = commonActions.GetFieldValue(item, ItemConstants.ItemId);
       await alternatePage.GetTextField(panelName.ToLower(), fieldLocator).ClickAsync();
  await BasePage.TypeTextAsync(alternatePage.GetTextField(panelName.ToLower(), fieldLocator), inputValue);
        await page.WaitForLoadStateAsync();
await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);
       var isDropdownOpen = await commonLocators.GetDropdownListBox().IsVisibleAsync();
     if (isDropdownOpen)
       {
       await commonLocators.GetSelectDropdownOption(inputValue).ClickAsync();
  }
}
        }

        #endregion

        #region Grid Verification

        /// <summary>
        /// Verifies item details in interchange master table
        /// </summary>
        public async Task VerifyItemDetailsInMasterTableAsync(string item, string panelName, Table table)
        {
       var itemId = commonActions.GetFieldValue(item, ItemConstants.ItemId);

    var itemRowDetails = await AlternateGridActions
            .GetRowDetailsAsDictionaryAsync(alternatePage.GetItemRowDetails(panelName.ToLower(), itemId));

   Console.WriteLine(itemRowDetails);

          foreach (var row in table.Rows)
          {
      var fieldKey = row[0];
   var expectedValue = fieldKey == row[1]
? commonActions.GetFieldValue(item, fieldKey)
              : row[1];

          var actualValue = itemRowDetails[fieldKey];

    ValidationHelper.AssertEqual(fieldKey, expectedValue, actualValue,
        $"Mismatch in '{panelName}' Interchange Master Table for Item '{itemId}'.");
     }
        }

        /// <summary>
        /// Verifies cluster details of multiple items in a table
    /// </summary>
        public async Task VerifyClusterDetailsInTableAsync(string itemsCsv, string panelName, Table table)
 {
            await page.WaitForLoadStateAsync();

       var itemKeys = itemsCsv.Split(',').Select(x => x.Trim()).ToList();

     for (var i = 0; i < itemKeys.Count; i++)
       {
           var itemKey = itemKeys[i];

         if (!featureContext.ContainsKey(itemKey))
 {
              throw new KeyNotFoundException($"Feature context does not contain key '{itemKey}'");
             }

                var itemId = commonActions.GetFieldValue(itemKey, ItemConstants.ItemId);
       Console.WriteLine($"Looking for row of {itemKey} (Item-Id: {itemId}) in panel: {panelName}");

        var rowLocator = alternatePage.GetClusterItemRowDetails(panelName.ToLower(), itemId);
         var itemRowDetails = await AlternateGridActions.GetRowDetailsAsDictionaryAsync(rowLocator);

     ValidationHelper.FailIfNullOrEmpty("itemRowDetails", itemRowDetails, $"No data found in UI for item '{itemKey}' (Item-Id: {itemId}). Check locator or wait timing.");

  foreach (var row in table.Rows)
     {
         var field = row["FieldName"];
        var valueRaw = row["Value"];
               var expectedValue = string.Empty;

             if (valueRaw.Equals(field, StringComparison.OrdinalIgnoreCase))
          {
         expectedValue = commonActions.GetFieldValue(itemKey, field);
         }
else if (valueRaw.Contains(","))
       {
   var valueList = valueRaw.Split(',').Select(v => v.Trim()).ToArray();
              expectedValue = i < valueList.Length ? valueList[i] : valueList.Last();
      }
         else
  {
         expectedValue = valueRaw;
 }

         if (!itemRowDetails.TryGetValue(field, out var actualValue))
     {
       throw new KeyNotFoundException($"Field '{field}' not found in row data for item '{itemKey}'. Available keys: {string.Join(", ", itemRowDetails.Keys)}");
            }

     ValidationHelper.AssertEqual(field, expectedValue, actualValue, $"{field} for {itemKey} should be '{expectedValue}' but was '{actualValue}'");
                }
            }
        }

    /// <summary>
        /// Verifies that an entered item is automatically selected in the grid
   /// </summary>
   public async Task VerifyItemAutoSelectedAsync(string item, string panelName)
   {
   var itemId = commonActions.GetFieldValue(item, ItemConstants.ItemId);
   var itemRowElement = alternatePage.GetItemRowDetails(panelName.ToLower(), itemId);

    var classAttribute = await itemRowElement.GetAttributeAsync(AttributeConstants.Class);

   Assert.IsTrue(classAttribute?.Contains("active") == true,
       $"The item with ID '{itemId}' is not selected (missing 'active' class).");
 }

        /// <summary>
  /// Verifies that a cluster item is automatically selected in the grid
    /// </summary>
        public async Task VerifyClusterItemAutoSelectedAsync(string item, string panelName)
        {
   var itemId = commonActions.GetFieldValue(item, ItemConstants.ItemId);
   var itemRowElement = alternatePage.GetClusterItemRowDetails(panelName.ToLower(), itemId);

      var classAttribute = await itemRowElement.GetAttributeAsync(AttributeConstants.Class);

            Assert.IsTrue(classAttribute?.Contains("active") == true,
     $"The item with ID '{itemId}' is not selected (missing 'active' class).");
        }

     /// <summary>
        /// Selects an item from the interchange master table
        /// </summary>
  public async Task SelectItemFromMasterTableAsync(string item, string panelName)
        {
   var itemId = commonActions.GetFieldValue(item, ItemConstants.ItemId);
      var itemRowElement = alternatePage.GetItemRowDetails(panelName.ToLower(), itemId);
 await itemRowElement.ClickAsync();
            await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
        }

    /// <summary>
  /// Verifies that an item is not visible on the interchange master table
 /// </summary>
        public async Task VerifyItemNotVisibleAsync(string item, string panelName)
     {
     var itemId = commonActions.GetFieldValue(item, ItemConstants.ItemId);
     var itemRowElement = alternatePage.GetItemRowDetails(panelName.ToLower(), itemId);
     Assert.False(await itemRowElement.IsVisibleAsync(), $"The row for the Item-Id - '{itemId}' is not displayed on the grid");
        }

        /// <summary>
        /// Selects an item option
   /// </summary>
  public async Task SelectItemOptionAsync(string item)
 {
            var inputValue = commonActions.GetFieldValue(item, ItemConstants.ItemId);
     await alternatePage.GetAddAnotherItemOption(inputValue).ClickAsync();
      }

        #endregion

        #region Item Selection

     /// <summary>
        /// Verifies item details are populated in a panel
        /// </summary>
        public async Task VerifyItemDetailsPopulatedAsync(string itemId, string panelname)
        {
     var innerText = await alternatePage.GetItemRowDetails(itemId).InnerTextAsync();

    if (string.IsNullOrWhiteSpace(innerText))
   {
ValidationHelper.Fail($"Item '{itemId}' details in panel '{panelname}' are displaying as empty.");
   }
        }

     #endregion

        #region Audit Information

        /// <summary>
        /// Verifies created and modified username and date in a panel
     /// </summary>
        public async Task VerifyCreatedModifiedInfoAsync(string panelName, Table table)
        {
 foreach (var row in table.Rows)
    {
                var fieldName = row[0];
  var text = await alternatePage
                .GetCreatedModifiedUsernameAndDate(panelName.ToLower(), fieldName.ToLower())
            .InnerTextAsync();

                if (string.IsNullOrWhiteSpace(text))
              {
      ValidationHelper.Fail($"{fieldName} created and modified date and username is not visible.");
         }
          }
      }

        /// <summary>
        /// Verifies created and modified date and username in tab container
        /// </summary>
        public async Task VerifyCreatedModifiedDateInTabAsync(string panelName, string tabName, Table table)
        {
  foreach (var menu in table.Rows)
          {
      await alternatePage.GetCreatedAndModifiedDateAndUser(panelName.ToLower(), tabName.ToLower(), menu[0].ToLower()).IsVisibleAsync();
      }
      }

        #endregion

      #region Toggle Operations

        /// <summary>
        /// Verifies display inactive toggle switch state
  /// </summary>
    public async Task VerifyToggleSwitchStateAsync(string state)
  {
var isVisible = await alternatePage
    .GetDisplayInactiveToggleButton(state.ToLower())
      .IsVisibleAsync();

   if (!isVisible)
            {
   ValidationHelper.Fail($"Toggle switch for state '{state}' is not displayed.");
 }
      }

        /// <summary>
   /// Selects display inactive toggle button
        /// </summary>
        public async Task SelectDisplayInactiveToggleAsync(string state)
        {
 await alternatePage.GetDisplayInactiveToggleButton(state.ToLower()).ClickAsync();
     await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
      }

  #endregion

        #region Popup Operations

        /// <summary>
        /// Verifies that a popup message is displayed
    /// </summary>
        public async Task VerifyPopupMessageAsync(string messageTitle)
        {
            var actualMessageTitle = await commonLocators.GetSuccessTitlePopup().TextContentAsync();

            actualMessageTitle = actualMessageTitle.Trim();
     messageTitle = messageTitle.Trim();

        Assert.AreEqual(actualMessageTitle, messageTitle, $"The {messageTitle} message is not displayed.");
        }

        /// <summary>
  /// Verifies that a popup title is displayed in a panel
        /// </summary>
public async Task VerifyPopupTitleAsync(string expectedTitle, string panelName)
        {
            var actualTitle = await alternatePage.GetTablePopUpTitle(panelName.ToLower()).InnerHTMLAsync();
  Assert.AreEqual(expectedTitle, actualTitle, $"The {expectedTitle} is not displayed.");
   }

        /// <summary>
    /// Verifies that a popup is displayed
      /// </summary>
        public async Task VerifyPopupDisplayedAsync(string title)
        {
  var actualTitle = await alternatePage.GetPopUpMessage(title).InnerHTMLAsync();
    Assert.AreEqual(title, actualTitle.Trim(), $"The {title} is not displayed.");
        }

        /// <summary>
        /// Verifies that the inactive clusters grid is displayed
   /// </summary>
   public async Task VerifyInactiveClustersGridDisplayedAsync()
        {
            Assert.True(await alternatePage.GetPopUpItemTable().IsVisibleAsync(), "Inactive clusters grid is not displayed");
        }

 /// <summary>
        /// Verifies that the clusters grid is displayed
        /// </summary>
        public async Task VerifyClustersGridDisplayedAsync()
  {
   Assert.True(await alternatePage.GetPopUpItemTable().IsVisibleAsync());
        }

        /// <summary>
  /// Verifies details in popup for a panel
   /// </summary>
    public async Task VerifyPopupDetailsAsync(string panelName, Table table)
        {
  foreach (var menu in table.Rows)
          {
        Assert.AreEqual(menu[1].Trim(), (await alternatePage.GetPopUpTableTotalItemsAndTotalActive(panelName.ToLower(), menu[0].ToLower()).InnerHTMLAsync()).Trim(), $"The Cluster does not have {menu[0]} as {menu[1]}");
            }
        }

   /// <summary>
   /// Verifies that the same item is populated in popup panel
    /// </summary>
        public async Task VerifySameItemInPopupAsync(string panelName, Table table)
     {
    var selectClusterItemId = string.Empty;
  var itemMasterItemId = string.Empty;
    foreach (var menu in table.Rows)
 {
selectClusterItemId = await alternatePage.GetPopUpTextField(panelName.ToLower(), menu[0].ToLower()).InputValueAsync();
      itemMasterItemId = scenarioContext["itemId"].ToString();
    Assert.AreEqual(selectClusterItemId, itemMasterItemId, $"Mismatch for '{itemMasterItemId}'. Expected: '{itemMasterItemId}' but found '{selectClusterItemId}'.");
        }
        }

        /// <summary>
        /// Verifies cluster details in popup table
   /// </summary>
  public async Task VerifyClusterDetailsInPopupTableAsync(string itemsCsv, string tableName, string panelName, Table inputParametersList)
      {
      await page.WaitForLoadStateAsync();

      var itemKeys = itemsCsv.Split(',').Select(x => x.Trim()).ToList();

        for (var i = 0; i < itemKeys.Count; i++)
    {
  var itemKey = itemKeys[i];

  if (!featureContext.ContainsKey(itemKey))
                {
         throw new KeyNotFoundException($"Feature context does not contain key '{itemKey}'");
             }

          var itemId = commonActions.GetFieldValue(itemKey, ItemConstants.ItemId);
        Console.WriteLine($"Looking for row of {itemKey} (Item-Id: {itemId}) in panel: {panelName}");

     var rowLocator = alternatePage.GetPopupClusterRowItemDetails(panelName.ToLower(), tableName.ToLower(), itemId);
    var itemRowDetails = await AlternateGridActions.GetRowDetailsAsDictionaryAsync(rowLocator);

     ValidationHelper.FailIfNullOrEmpty("itemRowDetails", itemRowDetails, $"No data found in UI for item '{itemKey}' (Item-Id: {itemId}). Check locator or wait timing.");

                foreach (var row in inputParametersList.Rows)
  {
    var field = row["FieldName"];
var valueRaw = row["Value"];
        var expectedValue = string.Empty;

         if (valueRaw.Equals(field, StringComparison.OrdinalIgnoreCase))
          {
     expectedValue = commonActions.GetFieldValue(itemKey, field);
     }
      else if (valueRaw.Contains(","))
           {
        var valueList = valueRaw.Split(',').Select(v => v.Trim()).ToArray();
   expectedValue = i < valueList.Length ? valueList[i] : valueList.Last();
       }
     else
                    {
     expectedValue = valueRaw;
        }

        if (!itemRowDetails.TryGetValue(field, out var actualValue))
{
            throw new KeyNotFoundException($"Field '{field}' not found in row data for item '{itemKey}'. Available keys: {string.Join(", ", itemRowDetails.Keys)}");
          }

 Assert.AreEqual(expectedValue, actualValue, $"{field} for {itemKey} should be '{expectedValue}' but was '{actualValue}'");
                }
            }
        }

        /// <summary>
     /// Handles popup verification and then enters data in specified panel
 /// </summary>
public async Task HandlePopupThenEnterDataAsync(string expectedTitle, string popupPanelName, string item, string textFieldPanelName, Table inputParametersList)
        {
  var titleLocator = alternatePage.GetTablePopUpTitle(popupPanelName.ToLower());

   if (await titleLocator.IsVisibleAsync())
 {
        var actualTitle = await titleLocator.InnerTextAsync();

     if (actualTitle.Trim() == expectedTitle.Trim())
   {
       await alternatePage.GetPopupTableCloseButton(popupPanelName.ToLower(), "close-popup").ClickAsync();
   Console.WriteLine($"Pop-up '{expectedTitle}' was displayed and closed.");
  await page.WaitForLoadStateAsync();
    var fieldLocator = string.Empty;
     foreach (var fieldName in inputParametersList.Rows)
    {
       fieldLocator = fieldName[0].ToLower();
    var inputValue = commonActions.GetFieldValue(item, ItemConstants.ItemId);
      await alternatePage.GetTextField(textFieldPanelName.ToLower(), fieldLocator).ClickAsync();
  await BasePage.TypeTextAsync(alternatePage.GetTextField(textFieldPanelName.ToLower(), fieldLocator), inputValue);
 await page.WaitForLoadStateAsync();
      await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);

   var isDropdownOpen = await commonLocators.GetDropdownListBox().IsVisibleAsync();
 if (isDropdownOpen)
    {
  await commonLocators.GetSelectDropdownOption(inputValue).ClickAsync();
    }
  }
       }
    else
 {
    Console.WriteLine($"Pop-up is visible, but title mismatch. Expected: {expectedTitle}, Actual: {actualTitle}");
    }
   }
 else
     {
   Console.WriteLine($"The '{expectedTitle}' pop-up is not displayed.");
      }
        }

        /// <summary>
      /// Confirms action by clicking OK button
  /// </summary>
     public async Task ConfirmActionAsync(string popupTitle)
    {
  await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());

            var successTitle = await alternatePage.GetSuccessTitlePopup().InnerHTMLAsync();
      successTitle = successTitle.Trim();
            popupTitle = popupTitle.Trim();

     Assert.AreEqual(successTitle, popupTitle, $"Mismatch for field '{popupTitle}'. Expected: '{popupTitle}' but found '{successTitle}'.");

  await alternatePage.GetPopupOkButton().ClickAsync();
   }

     #endregion
    }
}

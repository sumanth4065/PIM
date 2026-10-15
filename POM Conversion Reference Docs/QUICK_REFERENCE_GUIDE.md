# Quick Reference Guide - POM Actions Pattern

## For Test Automation Engineers

### How to Use the New Actions Layer

#### ? **DON'T** - Old Pattern (Direct Page Access)
```csharp
[When(@"User clicks on ""(.*)"" button")]
public async Task WhenUserClicksOnButton(string buttonName)
{
    var button = itemMasterPage.GetButtonName(buttonName.ToLower());
    await button.ScrollIntoViewIfNeededAsync();
    if (await button.IsVisibleAsync() && await button.IsEnabledAsync())
    {
        await button.ClickAsync();
    }
    else
  {
        ValidationHelper.Fail($"Button '{buttonName}' is not interactable");
    }
}
```

#### ? **DO** - New Pattern (Actions Layer)
```csharp
[When(@"User clicks on ""(.*)"" button")]
public async Task WhenUserClicksOnButton(string buttonName)
{
    await _itemMasterActions.ClickButtonAsync(buttonName);
}
```

---

## Quick Start Checklist

### 1. **Understand the 3 Layers**

| Layer | Responsibility | Example |
|-------|----------------|---------|
| **Pages** | Locators only | `GetButtonName()` returns `ILocator` |
| **Actions** | Business logic | `ClickButtonAsync()` does the work |
| **Steps** | Test orchestration | Calls Actions methods |

### 2. **Initialize Actions in Constructor**

```csharp
[Binding]
public partial class MySteps
{
    private readonly IPage _page;
    private readonly ScenarioContext _scenarioContext;
    private readonly FeatureContext _featureContext;
  
    // Actions instances
    private readonly ItemMasterActions _itemMasterActions;
  
  public MySteps(FeatureContext featureContext, ScenarioContext scenarioContext)
  {
  _page = featureContext["Page"] as IPage;
        _scenarioContext = scenarioContext;
  _featureContext = featureContext;
        
        // Initialize Actions
  _itemMasterActions = new ItemMasterActions(_page, _scenarioContext, _featureContext);
    }
}
```

### 3. **Call Actions in Step Definitions**

```csharp
[When(@"User enters header fields")]
public async Task WhenUserEntersHeaderFields(Table table)
{
    await _itemMasterActions.EnterHeaderFieldsAsync(table);
}

[Then(@"Verify fields are displayed")]
public async Task ThenVerifyFieldsAreDisplayed(Table table)
{
    await _itemMasterActions.VerifyFieldsAreDisplayedAsync(table);
}
```

---

## Available Actions Classes

### **ItemMasterActions**
```csharp
var actions = new ItemMasterActions(page, scenarioContext, featureContext);

// Navigation
await actions.ClickButtonAsync("save");
await actions.ClickTabAsync("taxonomy");

// Data Entry
await actions.EnterHeaderFieldsAsync(table);
await actions.EnterProductFieldsAsync(table);

// Verification
await actions.VerifyFieldsCountAsync("5", table);
await actions.VerifyFieldsAreEmptyAsync(table);

// Checkboxes & Radio Buttons
await actions.SetCheckboxStateAsync("isActive", "true");
await actions.SelectRadioButtonAsync("product");

// Context Operations
await actions.RecallCreatedItemIdAsync("itemId");
await actions.GetFieldValueAsync("description");
```

### **ItemTaxonomyActions**
```csharp
var actions = new ItemTaxonomyActions(page, scenarioContext, featureContext);

// Taxonomy Operations
await actions.EnterTaxonomyDetailsAsync("Product Taxonomy", table);
await actions.ClickTaxonomyAsync("Service Taxonomy");
await actions.VerifyTaxonomyIsDisplayedAsync("Product Taxonomy");

// Product Taxonomy
await actions.SearchAndSelectInProductTaxonomyAsync("Oem1");
```

### **ItemExcelActions**
```csharp
var actions = new ItemExcelActions(page, scenarioContext, featureContext);

// Excel Operations
await actions.RecallFieldsFromExcelAsync("CreateItem.xlsx", table);
await actions.RecallTaxonomyFieldsFromExcelAsync("TaxonomyData.xlsx", table);
await actions.RetrieveFieldValuesFromExcelAsync("ItemData.xlsx", table);
```

---

## Common Patterns

### **Pattern 1: Simple Button Click**
```csharp
[When(@"User clicks ""(.*)"" button")]
public async Task WhenUserClicksButton(string button)
{
    await _itemMasterActions.ClickButtonAsync(button);
}
```

### **Pattern 2: Field Verification**
```csharp
[Then(@"Verify ""(.*)"" displays as ""(.*)""")]
public async Task ThenVerifyFieldDisplaysAs(string field, string value)
{
    await _itemMasterActions.VerifyFieldStatusAsync(field, value);
}
```

### **Pattern 3: Data Entry from Table**
```csharp
[When(@"User enters the following data")]
public async Task WhenUserEntersData(Table table)
{
    await _itemMasterActions.EnterHeaderFieldsAsync(table);
}
```

### **Pattern 4: Excel Data Recall**
```csharp
[When(@"User recalls data from ""(.*)"" Excel")]
public async Task WhenUserRecallsDataFromExcel(string fileName, Table table)
{
    await _itemExcelActions.RecallFieldsFromExcelAsync(fileName, table);
}
```

---

## Creating New Actions Methods

### **Template**
```csharp
public async Task MyNewActionAsync(string parameter1, Table table)
{
    // 1. Get test case name if needed
    var testCaseName = _scenarioContext.ScenarioInfo.Title;
    
    // 2. Read test data if needed
    var value = Utility.ReadDataFromJsonString(_scenarioContext, $"testCases.{testCaseName}.FieldName");
    
    // 3. Perform action using Page Objects
    var element = _itemMasterPage.GetSomeElement();
    await element.ClickAsync();
    
    // 4. Store in context if needed
    _featureContext["SomeKey"] = value;
    
    // 5. Wait for UI to stabilize if needed
    await BasePage.WaitForElementToDisappearAsync(_commonLocators.GetLoader());
}
```

### **Best Practices**
1. ? Use `Async` suffix for async methods
2. ? Use descriptive method names (`EnterHeaderFieldsAsync` not `EnterFieldsAsync`)
3. ? Group related methods together
4. ? Keep methods focused (single responsibility)
5. ? Use private helper methods for reusability
6. ? Add XML documentation for complex methods
7. ? Handle exceptions gracefully
8. ? Use ValidationHelper for assertions

---

## Debugging Tips

### **Check Actions Initialization**
```csharp
// In step constructor, verify Actions are initialized
public MySteps(FeatureContext featureContext, ScenarioContext scenarioContext)
{
  _itemMasterActions = new ItemMasterActions(page, scenarioContext, featureContext);
    
    // Debug check
    if (_itemMasterActions == null)
        throw new Exception("ItemMasterActions not initialized!");
}
```

### **Verify Context Data**
```csharp
// Check what's in context
var itemId = _featureContext.ContainsKey("itemIdValue") 
    ? _featureContext["itemIdValue"].ToString() 
  : "NOT FOUND";
Console.WriteLine($"ItemId from context: {itemId}");
```

### **Test Actions in Isolation**
```csharp
// Create Actions instance for testing
var testActions = new ItemMasterActions(page, scenarioContext, featureContext);
await testActions.ClickButtonAsync("save");
```

---

## Migration Guide

### **Converting Existing Step Definitions**

#### Step 1: Identify Business Logic
```csharp
// ? This has business logic (button visibility check, click logic, error handling)
[When(@"User clicks button")]
public async Task WhenUserClicksButton()
{
 var button = itemMasterPage.GetButton();
    if (await button.IsVisibleAsync())
    await button.ClickAsync();
    else
        ValidationHelper.Fail("Button not visible");
}
```

#### Step 2: Move to Actions Class
```csharp
// In ItemMasterActions.cs
public async Task ClickButtonAsync(string buttonName)
{
    var button = _itemMasterPage.GetButtonName(buttonName);
    if (await button.IsVisibleAsync())
        await button.ClickAsync();
    else
        ValidationHelper.Fail($"Button '{buttonName}' not visible");
}
```

#### Step 3: Update Step Definition
```csharp
// ? Clean step definition
[When(@"User clicks button")]
public async Task WhenUserClicksButton()
{
    await _itemMasterActions.ClickButtonAsync("save");
}
```

---

## FAQs

### Q: When should I create a new Actions class vs. adding to existing?
**A**: Create new when:
- Functionality is for a different page/module
- Methods exceed 30-40 per class
- Clear separation of concerns exists

Add to existing when:
- Related to same page/functionality
- Reuses same page objects
- Logical grouping makes sense

### Q: Should I pass ScenarioContext/FeatureContext to every method?
**A**: No! They should be constructor-injected fields:
```csharp
// ? DO THIS
public ItemMasterActions(IPage page, ScenarioContext scenarioContext, FeatureContext featureContext)
{
    _scenarioContext = scenarioContext; // Store as field
}

public async Task MyMethodAsync()
{
    var value = _scenarioContext["key"]; // Use field
}

// ? DON'T DO THIS
public async Task MyMethodAsync(ScenarioContext scenarioContext)
{
    var value = scenarioContext["key"]; // Parameter
}
```

### Q: Can I call Actions from another Actions class?
**A**: Yes, but avoid circular dependencies:
```csharp
public class ItemMasterActions
{
    private readonly ItemTaxonomyActions _taxonomyActions;
    
public ItemMasterActions(IPage page, ScenarioContext scenarioContext, FeatureContext featureContext)
    {
 _taxonomyActions = new ItemTaxonomyActions(page, scenarioContext, featureContext);
    }
    
    public async Task CreateItemWithTaxonomyAsync()
    {
        await CreateItemAsync();
        await _taxonomyActions.EnterTaxonomyDetailsAsync("Product Taxonomy", table);
 }
}
```

### Q: How do I handle test data from JSON?
**A**: Use Utility.ReadDataFromJsonString in Actions:
```csharp
public async Task EnterFieldAsync(string fieldName)
{
    var testCaseName = _scenarioContext.ScenarioInfo.Title;
    var value = Utility.ReadDataFromJsonString(_scenarioContext, $"testCases.{testCaseName}.{fieldName}");
    // Use value...
}
```

---

## Code Examples

### **Complete Example: Create Item Flow**

**Step Definition**:
```csharp
[When(@"User creates a new item")]
public async Task WhenUserCreatesNewItem(Table table)
{
    await _itemMasterActions.EnterHeaderFieldsAsync(table);
    await _itemTaxonomyActions.EnterTaxonomyDetailsAsync("Product Taxonomy", taxonomyTable);
    await _itemMasterActions.ClickButtonAsync("save");
}
```

**Actions Method**:
```csharp
public async Task EnterHeaderFieldsAsync(Table table)
{
    await _page.WaitForLoadStateAsync();
    var testCaseName = _scenarioContext.ScenarioInfo.Title;
    
    foreach (var row in table.Rows)
    {
      var fieldName = row[0];
      var value = Utility.ReadDataFromJsonString(_scenarioContext, $"testCases.{testCaseName}.{fieldName}");
        
        if (fieldName == ItemConstants.ItemId)
        {
            await EnterItemIdAsync(fieldName.ToLower(), value);
        }
        else
        {
            await EnterDropdownFieldAsync(fieldName.ToLower(), value);
     }
    }
}

private async Task EnterItemIdAsync(string fieldLocator, string baseValue)
{
    var itemId = $"{baseValue}-{Guid.NewGuid().ToString().Substring(0, 4).ToUpper()}";
    var field = _itemMasterPage.GetDropdownInputField(fieldLocator);
    await field.ClickAsync();
    await BasePage.TypeTextAsync(field, itemId);
    _featureContext["itemIdValue"] = itemId;
}
```

---

## Summary

? **Always** use Actions for business logic  
? **Always** initialize Actions in constructor
? **Always** keep step definitions thin  
? **Never** put business logic in step definitions  
? **Never** access page objects directly from steps  
? **Never** duplicate code across Actions  

**Remember**: 
- **Pages** = WHAT elements exist
- **Actions** = HOW to interact  
- **Steps** = WHICH test scenario

---

*For questions or issues, refer to TRANSFORMATION_SUMMARY.md or contact the framework team.*

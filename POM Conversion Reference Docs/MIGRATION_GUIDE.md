# ?? Migration Guide - Item Module Refactoring

## ?? Overview

This guide helps developers update their code to use the new Action class architecture.

---

## ?? What Changed?

### **Before Refactoring:**
```
Item.PIMItemDefault.cs (Step File)
?? Direct CosmosDB queries
?? Direct page interactions
?? Business logic mixed with test flow
?? ~400 lines of mixed concerns
```

### **After Refactoring:**
```
Item.PIMItemDefault.cs (Step File)
?? Thin wrapper with [Then] bindings
?? Delegates to ItemPIMDefaultActions

ItemPIMDefaultActions.cs (Action Class)
?? All CosmosDB validation logic
?? Reusable business methods
?? ~500 lines of pure business logic
```

---

## ?? Step-by-Step Migration

### **1. Item.PIMItemDefault.cs Step Bindings**

#### **BEFORE:**
```csharp
[Then(@"Verify the below field values in Cosmos DB using ""(.*)"" database and ""(.*)"" container in ""(.*)"" document")]
public async Task ThenVerifyTheBelowFieldValuesInCosmosDBUsingDatabaseAndContainerInDocument(
    string database, string container, string documentType, Table fieldsToCheck)
{
  try
    {
        var itemId = featureContext["itemIdValue"].ToString() ?? string.Empty;
      var defaultQuery = CosmosDBQueries.GetDataUsingItemIdAndDocumentType(itemId, documentType);
        var dbItemDefaultData = await cosmosDb.GetDataFromCosmosDB<ItemDefaultRecord>(database.ToLower(), container, defaultQuery);
        
        // ... 50+ lines of validation logic ...
    }
  catch (Exception e)
    {
        ValidationHelper.Fail($"UI Baseline values validation failed against database.", e);
  }
}
```

#### **AFTER:**
```csharp
[Then(@"Verify the below field values in Cosmos DB using ""(.*)"" database and ""(.*)"" container in ""(.*)"" document")]
public async Task ThenVerifyTheBelowFieldValuesInCosmosDBUsingDatabaseAndContainerInDocument(
    string database, string container, string documentType, Table fieldsToCheck)
{
    await _itemPIMDefaultActions.VerifyFieldValuesInCosmosDBAsync(database, container, documentType, fieldsToCheck);
}
```

**Changes:**
- ? Step binding reduced from 50+ lines to 1 line
- ? Business logic moved to `ItemPIMDefaultActions.cs`
- ? Error handling now in Action class
- ? Reusable across multiple scenarios

---

### **2. Item.PIMItemPool.cs Step Bindings**

#### **BEFORE:**
```csharp
[Then(@"Verify Event History should be updated with in the ""(.*)"" container of the ""(.*)"" database in ""(.*)"" document")]
public async Task ThenVerifyEventHistoryShouldBeUpdatedWithInTheContainerOfTheDatabaseInItemPoolsDocument(
    string container, string database, string documentType, Table eventHistories)
{
    string eventName = string.Empty;
    var itemId = string.Empty;
    try
    {
     itemId = featureContext["itemIdValue"].ToString();
   await page.WaitForTimeoutAsync(TimeConstants.ItemOrAlternatesPoolTriggerTimeMs);
        
        var itemPoolQuery = CosmosDBQueries.GetDataUsingItemIdAndDocumentType(itemId, documentType);
      var itemPoolData = await cosmosDb.GetDataFromCosmosDB<ItemPoolRecord>(database, container, itemPoolQuery);
 
        // ... validation logic ...
    }
    catch (Exception e)
    {
        ValidationHelper.Fail($"Event History validation failed...", e);
    }
}
```

#### **AFTER:**
```csharp
[Then(@"Verify Event History should be updated with in the ""(.*)"" container of the ""(.*)"" database in ""(.*)"" document")]
public async Task ThenVerifyEventHistoryShouldBeUpdatedWithInTheContainerOfTheDatabaseInItemPoolsDocument(
    string container, string database, string documentType, Table eventHistories)
{
    await _itemPIMPoolActions.VerifyEventHistoryAsync(container, database, documentType, eventHistories);
}
```

---

### **3. Item.PIMServiceDefault.cs Step Bindings**

#### **BEFORE:**
```csharp
[Then(@"Verify the below service field values in Cosmos DB using ""(.*)"" database and ""(.*)"" container in ""(.*)"" document")]
public async Task ThenVerifyTheBelowServiceFieldValuesInCosmosDBUsingDatabaseAndContainerInDocument(
    string database, string container, string documentType, Table fieldsNames)
{
    var itemId = string.Empty;
    try
    {
    itemId = featureContext["itemIdValue"].ToString() ?? string.Empty;
        var defaultQuery = CosmosDBQueries.GetDataUsingItemIdAndDocumentType(itemId, documentType);
  var dbServiceDefaultData = await cosmosDb.GetDataFromCosmosDB<ServiceDefaultRecord>(database.ToLower(), container, defaultQuery);
 
        // ... validation logic ...
    }
    catch (Exception e)
    {
        ValidationHelper.Fail($"UI Service field values validation failed...", e);
    }
}
```

#### **AFTER:**
```csharp
[Then(@"Verify the below service field values in Cosmos DB using ""(.*)"" database and ""(.*)"" container in ""(.*)"" document")]
public async Task ThenVerifyTheBelowServiceFieldValuesInCosmosDBUsingDatabaseAndContainerInDocument(
    string database, string container, string documentType, Table fieldsNames)
{
    await _itemPIMServiceDefaultActions.VerifyServiceFieldValuesAsync(database, container, documentType, fieldsNames);
}
```

---

## ?? Finding the Right Action Method

### **Method Naming Conventions:**

| Step Binding Pattern | Action Method Name |
|---------------------|-------------------|
| `Verify the below field values in Cosmos DB` | `VerifyFieldValuesInCosmosDBAsync` |
| `Verify Event History should be updated` | `VerifyEventHistoryAsync` |
| `User verifies that the general item details` | `VerifyGeneralItemDetailsAsync` |
| `Verify characteristic value` | `VerifyCharacteristicValueAsync` |

### **Quick Search Tips:**

1. **Open the Action class** matching your Step file:
   - `Item.PIMItemDefault.cs` ? `ItemPIMDefaultActions.cs`
   - `Item.PIMItemPool.cs` ? `ItemPIMPoolActions.cs`
   - `Item.PIMServiceDefault.cs` ? `ItemPIMServiceDefaultActions.cs`

2. **Search for keywords** from your step binding:
   - "Verify field values" ? Search for `VerifyField`
   - "Event History" ? Search for `EventHistory`
   - "Service attribute" ? Search for `ServiceAttribute`

3. **Check the #regions** in Action classes for organization:
   - `#region Field Values Validation`
   - `#region Event History Validation`
   - `#region Characteristic Validation`

---

## ?? Breaking Changes

### **1. Direct Page Object Access**

? **No Longer Available in Step Files:**
```csharp
// This was possible before but should be avoided
var value = await itemMasterPage.GetField("test").InputValueAsync();
```

? **Use Action Class Instead:**
```csharp
await _itemMasterActions.GetFieldValueAsync("test");
```

### **2. Direct CosmosDB Helper Usage**

? **No Longer in Step Files:**
```csharp
var dbData = await cosmosDb.GetDataFromCosmosDB<ItemDefaultRecord>(database, container, query);
```

? **Use Action Class:**
```csharp
await _itemPIMDefaultActions.VerifyFieldValuesInCosmosDBAsync(database, container, documentType, table);
```

### **3. FeatureContext Access**

? **Still Available** - Both Step and Action classes have access to FeatureContext and ScenarioContext.

```csharp
// In Step File
var itemId = ContextHelper.GetContextValue(featureContext, "itemIdValue");

// In Action Class (same thing)
var itemId = ContextHelper.GetContextValue(_featureContext, "itemIdValue");
```

---

## ??? Update Checklist for Your Feature Files

### **For Each Step Binding:**

- [ ] 1. Identify which Action class handles this validation
- [ ] 2. Find the corresponding method in the Action class
- [ ] 3. Replace the Step method body with a single Action call
- [ ] 4. Remove any local variables not needed anymore
- [ ] 5. Remove try-catch blocks (Action class handles errors)
- [ ] 6. Remove direct page/DB interactions
- [ ] 7. Test the updated step binding

---

## ?? Example: Complete Migration of a Step Binding

### **BEFORE:**
```csharp
[Then(@"Verify the characteristic value in ""(.*)"" document in Pim DB")]
public async Task ThenVerifyTheCharacteristicValueInPim(string documentType, Table characteristicValues)
{
  testCaseName = scenarioContext.ScenarioInfo.Title;
    string defaultQuery = string.Empty;
    List<PPT.ItemMaster.Data.Domain.Features.Common.Characteristics> dbCharacteristics = null;

    try
    {
        var database = DatabaseConstants.DatabaseName;
        var container = DatabaseConstants.DefaultsContainer;
        
        if (documentType.Contains(ItemConstants.LevelLineageValue.Taxonomy, StringComparison.OrdinalIgnoreCase))
        {
            var oemName = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.Oem");
var groupName = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.Group");
            defaultQuery = CosmosDBQueries.GetDataUsingOemAndGroupName(oemName, groupName);
   var dbTaxonomyDefaultData = await cosmosDb.GetDataFromCosmosDB<TaxonomyDefaultRecord>(database, container, defaultQuery);
            scenarioContext["PIMDefaultId"] = dbTaxonomyDefaultData.Id;
  dbCharacteristics = dbTaxonomyDefaultData.Characteristics;
        }
else if (documentType.Contains(ItemConstants.LevelLineageValue.Service, StringComparison.OrdinalIgnoreCase))
        {
       var itemId = featureContext["itemIdValue"].ToString() ?? string.Empty;
            var lob = featureContext["LOB"].ToString() ?? string.Empty;
          defaultQuery = CosmosDBQueries.GetDataUsingItemIdLOBAndDocumentType(itemId, lob, documentType);
         var dbServiceDefaultData = await cosmosDb.GetDataFromCosmosDB<ServiceDefaultRecord>(database, container, defaultQuery);
            scenarioContext["PIMDefaultId"] = dbServiceDefaultData.Id;
   dbCharacteristics = dbServiceDefaultData.Characteristics;
        }
        else
    {
            var itemId = featureContext["itemIdValue"].ToString() ?? string.Empty;
            defaultQuery = CosmosDBQueries.GetDataUsingItemIdAndDocumentType(itemId, documentType);
        var dbItemDefaultData = await cosmosDb.GetDataFromCosmosDB<ItemDefaultRecord>(database, container, defaultQuery);
   scenarioContext["PIMDefaultId"] = dbItemDefaultData.Id;
dbCharacteristics = dbItemDefaultData.Characteristics;
        }

   foreach (var value in characteristicValues.Rows)
    {
     var expectedCharacteristicName = value[0];
  var expectedValue = value[1];
            
            var matchingDbCharacteristic = dbCharacteristics.FirstOrDefault(c => 
         c.Characteristic.Name.Equals(expectedCharacteristicName, StringComparison.OrdinalIgnoreCase));

      if (matchingDbCharacteristic == null)
    {
            ValidationHelper.Fail($"Characteristic '{expectedCharacteristicName}' not found in {documentType} document.");
            }

var actualValue = matchingDbCharacteristic.Value;
            if (!string.Equals(actualValue, expectedValue, StringComparison.OrdinalIgnoreCase))
            {
            ValidationHelper.Fail($"Mismatch for Characteristic '{expectedCharacteristicName}' in {documentType}. Expected: '{expectedValue}', Found: '{actualValue}'");
            }
        }
 }
    catch (Exception e)
    {
        ValidationHelper.Fail($"Characteristic values validation failed against {documentType} document for {defaultQuery}.", e);
    }
}
```

### **AFTER:**
```csharp
[Then(@"Verify the characteristic value in ""(.*)"" document in Pim DB")]
public async Task ThenVerifyTheCharacteristicValueInPim(string documentType, Table characteristicValues)
{
    await _itemPIMDefaultActions.VerifyCharacteristicValueInDocumentAsync(documentType, characteristicValues);
}
```

**Lines of Code:**
- Before: **~60 lines**
- After: **~3 lines**
- Reduction: **95%** ??

---

## ?? Common Migration Patterns

### **Pattern 1: Simple DB Validation**

```csharp
// BEFORE - 30+ lines
[Then(@"Verify field")]
public async Task Step()
{
    try {
 var itemId = featureContext["itemIdValue"].ToString();
     var query = CosmosDBQueries.Get...(itemId);
   var dbData = await cosmosDb.GetDataFromCosmosDB<T>(db, container, query);
        // validation logic
    } catch (Exception e) { ValidationHelper.Fail(..., e); }
}

// AFTER - 1 line
[Then(@"Verify field")]
public async Task Step()
{
    await _itemPIMDefaultActions.VerifyFieldAsync(database, container, table);
}
```

### **Pattern 2: Excel + DB Validation**

```csharp
// BEFORE - 40+ lines
[Then(@"Verify excel field")]
public async Task Step(string fileName, Table table)
{
  try {
 ExcelPackage.LicenseContext = LicenseContext.Commercial;
        var excelData = Utility.ReadExcel(...);
var dbData = await cosmosDb.GetDataFromCosmosDB<T>(...);
        // comparison logic
    } catch (Exception e) { ValidationHelper.Fail(..., e); }
}

// AFTER - 1 line
[Then(@"Verify excel field")]
public async Task Step(string fileName, Table table)
{
    await _itemPIMDefaultActions.VerifyExcelCharacteristicValuesAsync(tabName, fileName, database, container, documentType, table);
}
```

---

## ?? Performance & Benefits

| Metric | Before | After | Improvement |
|--------|--------|-------|-------------|
| **Lines per Step Binding** | 30-60 | 1-3 | -95% |
| **Code Duplication** | High | None | ? |
| **Testability** | Low | High | ? |
| **Maintainability** | Low | High | ? |
| **Reusability** | None | High | ? |

---

## ?? Rollback Plan

If you need to temporarily rollback:

1. **Keep both implementations** during transition period
2. **Use feature flags** to switch between old/new code
3. **Git branches** for gradual migration

```csharp
// Transition Code
[Then(@"Verify field")]
public async Task Step()
{
    if (UseNewArchitecture)
    {
 await _itemPIMDefaultActions.VerifyFieldAsync(database, container, table);
    }
    else
    {
     // Old implementation
    }
}
```

---

## ?? Support

**Questions?**
- Check `COMPLETE_REFACTORING_SUMMARY.md`
- Check `ACTION_CLASSES_QUICK_REFERENCE.md`
- Review the Action class code - it has XML comments

**Found an Issue?**
- Verify Action class is initialized in `Item.cs` constructor
- Check method signatures match
- Verify using correct Action class for your validation

---

## ? Migration Completion Checklist

- [ ] All `Item.PIMItemDefault.cs` step bindings migrated
- [ ] All `Item.PIMItemPool.cs` step bindings migrated
- [ ] All `Item.PIMServiceDefault.cs` step bindings migrated
- [ ] `Item.cs` updated with new Action class initialization
- [ ] Build compiles successfully
- [ ] Existing tests pass
- [ ] Code reviewed
- [ ] Documentation updated

---

**?? Migration Complete! Welcome to the new clean architecture!**

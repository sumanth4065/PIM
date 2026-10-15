# ?? Item Module - Action Classes Quick Reference Guide

## ?? Quick Lookup Table

| I need to... | Use this Action Class | Method Pattern |
|-------------|----------------------|----------------|
| Validate UI fields | `ItemMasterActions` | `Verify*Async()` |
| Create/Edit items | `ItemMasterActions` | `Enter*Async()`, `Click*Async()` |
| Work with characteristics | `ItemCharacteristicActions` | `Verify*`, `Select*`, `Enter*` |
| Work with taxonomy | `ItemTaxonomyActions` | `Enter*`, `Verify*`, `Click*` |
| Import/Export Excel | `ItemExcelActions` | `Recall*FromExcelAsync()` |
| Validate IDH/Integration | `ItemIDHActions` | `Verify*InDocumentAsync()` |
| Validate ItemDefault DB | `ItemPIMDefaultActions` | `Verify*InCosmosDBAsync()` |
| Validate ItemPool DB | `ItemPIMPoolActions` | `Verify*Async()` |
| Validate ServiceDefault DB | `ItemPIMServiceDefaultActions` | `VerifyService*Async()` |

---

## ??? **Action Classes by Domain**

### **1. ItemMasterActions.cs** - UI Interactions & Item CRUD

```csharp
// Tab Navigation
await _itemMasterActions.VerifyTabIsDisplayedAsync(tabName);
await _itemMasterActions.ClickTabAsync(tabName);

// Button Interactions
await _itemMasterActions.ClickButtonAsync(buttonName);
await _itemMasterActions.VerifyButtonsAreEnabledAsync(button1, button2);

// Field Entry
await _itemMasterActions.EnterHeaderFieldsAsync(table);
await _itemMasterActions.EnterTextIntoFieldAsync(textField);
await _itemMasterActions.SelectFromDropdownAsync(value, fieldName);

// Field Verification
await _itemMasterActions.VerifyTextBoxFieldsAreDisplayedAsync(table);
await _itemMasterActions.VerifyFieldsCountAsync(count, table);
await _itemMasterActions.VerifyFieldsAreNotEmptyAsync(table);

// Checkbox/Radio Operations
await _itemMasterActions.VerifyCheckboxIsDisplayedAsync(checkbox);
await _itemMasterActions.SetCheckboxStateAsync(checkboxName, shouldBeChecked);
await _itemMasterActions.SelectRadioButtonAsync(fieldName);

// Context Recall
await _itemMasterActions.RecallCreatedItemIdAsync(itemId);
await _itemMasterActions.GetFieldValueAsync(fieldName);
```

---

### **2. ItemCharacteristicActions.cs** - Characteristic Management

```csharp
// Display Panel
await _itemCharacteristicActions.VerifyCharacteristicDisplayBoxesAsync(table);
await _itemCharacteristicActions.VerifyRowIsDisplayedInCharacteristicSectionAsync(rowNumber, kindOrType);

// Selection
await _itemCharacteristicActions.SelectCharacteristicKindAndTypeAsync(kindName, typeName);
await _itemCharacteristicActions.SelectCreatedCharacteristicKindAsync();
await _itemCharacteristicActions.SearchAndSelectCharacteristicAsync(characteristicType);

// Data Entry
await _itemCharacteristicActions.EnterDataIntoCharacteristicFieldAsync(fieldName);
await _itemCharacteristicActions.EnterTextIntoFieldInCharacteristicPanelAsync(internalNotes);

// Button Actions
await _itemCharacteristicActions.ClickButtonInCharacteristicPanelAsync(buttonName);
await _itemCharacteristicActions.VerifyButtonsAreEnabledAsync(table);

// Field Verification
await _itemCharacteristicActions.VerifyFieldsAreAutoPopulatedAndNotEditableAsync(table);
await _itemCharacteristicActions.VerifyFieldsAreEditableAsync(table);

// Checkbox Operations
await _itemCharacteristicActions.ClickCharacteristicCheckboxesAsync(table);
await _itemCharacteristicActions.VerifyCharacteristicCheckboxesAreCheckedAsync(table);

// P&L Calculations
_itemCharacteristicActions.CalculatePLLaborValuesAsync(table);
await _itemCharacteristicActions.VerifyPLLaborValuesWithCalculatedValuesAsync();
await _itemCharacteristicActions.VerifyPLLaborValuesInItemPoolAsync();
```

---

### **3. ItemTaxonomyActions.cs** - Taxonomy Operations

```csharp
// Verification
await _itemTaxonomyActions.VerifyTaxonomyIsDisplayedAsync(taxonomyOption);

// Navigation
await _itemTaxonomyActions.ClickTaxonomyAsync(taxonomyOption);

// Data Entry
await _itemTaxonomyActions.EnterTaxonomyDetailsAsync(taxonomyOption, table);
await _itemTaxonomyActions.EnterDetailsInTaxonomyAsync(taxonomyOption, table);
await _itemTaxonomyActions.EnterDataInTaxonomyAsync(taxonomyOption, table);
await _itemTaxonomyActions.EnterTaxonomyDetailsIfEmptyAsync(taxonomyType, button, table);

// Product Taxonomy Specific
await _itemTaxonomyActions.SearchAndSelectInProductTaxonomyAsync(fieldName);
```

---

### **4. ItemExcelActions.cs** - Excel Operations

```csharp
// Field Recall from Excel
await _itemExcelActions.RecallFieldsFromExcelAsync(fileName, table);
await _itemExcelActions.RecallTaxonomyFieldsFromExcelAsync(fileName, table);
await _itemExcelActions.RecallItemIdFromExcelAsync(fieldName, fileName);

// Explicit Item Excel
await _itemExcelActions.RecallFieldsFromExcelForExplicitItemAsync(fileName, table);

// Retrieve Values
await _itemExcelActions.RetrieveFieldValuesFromExcelAsync(fileName, table);
```

---

### **5. ItemIDHActions.cs** - IDH/Integration Validation

```csharp
// Characteristic Validation
await _itemIDHActions.VerifyCharacteristicValuesInDocumentAsync(database, container, documentType, table);
await _itemIDHActions.VerifyCharacteristicsInKindAsync(database, container, documentType, table);
await _itemIDHActions.ValidateCharacteristicValuesInItemCatalogAsync(table);
await _itemIDHActions.VerifyCharacteristicValueInItemDocumentAsync(table);
await _itemIDHActions.VerifyCharacteristicValueInItemCatalogServiceNodeAsync(table);
await _itemIDHActions.VerifyCharacteristicValueInItemCatalogItemNodeAsync(table);

// Document Validation
await _itemIDHActions.VerifyDocumentsAreCreatedAsync(database, container, table);
await _itemIDHActions.VerifyFieldValuesInDocumentAsync(database, container, type, table);

// Taxonomy Validation
await _itemIDHActions.ValidateTaxonomyDetailsOnItemCatalogAsync(table);
await _itemIDHActions.ValidateTaxonomyDetailsOnItemAsync(table);

// General Flags
await _itemIDHActions.VerifyGeneralFlagsInItemDocumentAsync(table);
await _itemIDHActions.VerifyGeneralFlagsInItemCatalogDocumentAsync(table);
await _itemIDHActions.VerifyCharacteristicsInItemCatalogServicesAsync(table);
```

---

### **6. ItemPIMDefaultActions.cs** ? NEW - PIM ItemDefault Validation

```csharp
// Field Values Validation
await _itemPIMDefaultActions.VerifyFieldValuesInCosmosDBAsync(database, container, documentType, table);

// Created/Modified Details
await _itemPIMDefaultActions.VerifyCreatedAndModifiedDetailsAsync(database, container, documentType, table);

// Document Modified Source
await _itemPIMDefaultActions.VerifyDocumentModifiedSourceAsync(modifiedDate, sourceType, database, container, documentType);

// Taxonomy Validation
await _itemPIMDefaultActions.VerifyProductTaxonomyDetailsAsync(taxonomy, database, container, documentType, table);
await _itemPIMDefaultActions.VerifyServiceTaxonomyDetailsAsync(taxonomy, database, container, documentType, table);
await _itemPIMDefaultActions.VerifyTaxonomyDetailsInCosmosDBAsync(taxonomyName, database, container, documentType, table);

// Characteristic Validation
await _itemPIMDefaultActions.VerifyCharacteristicValuesAsync(database, container, documentType, table);
await _itemPIMDefaultActions.VerifyExcelCharacteristicValuesAsync(tabName, fileName, database, container, documentType, table);
await _itemPIMDefaultActions.VerifyCharacteristicTypeValueAsync(locatorType, database, container, documentType);
await _itemPIMDefaultActions.VerifyCharacteristicValueInDocumentAsync(documentType, table);

// General Flags
await _itemPIMDefaultActions.VerifyGeneralFlagsAsync(documentType, database, container, table);
```

---

### **7. ItemPIMPoolActions.cs** ? NEW - PIM ItemPool Validation

```csharp
// Excel Characteristic Validation
await _itemPIMPoolActions.VerifyExcelCharacteristicFieldAsync(tabName, fileName, fieldName, database, container, documentType);

// Field Validation
await _itemPIMPoolActions.VerifyFieldsInDocumentAsync(database, container, documentType, table);
await _itemPIMPoolActions.VerifyTaxonomyFieldsAsync(taxonomy, database, container, documentType, table);

// Event History
await _itemPIMPoolActions.VerifyEventHistoryAsync(container, database, documentType, table);
await _itemPIMPoolActions.VerifyEventHistoryForOemAsync(oemKey, database, container, table);

// Language Translation
await _itemPIMPoolActions.VerifyLanguageTranslationAsync(database, container, documentType, table);

// General Item Details
await _itemPIMPoolActions.VerifyGeneralItemDetailsAsync(database, container, documentType, table);
await _itemPIMPoolActions.VerifyGeneralItemDetailsDisplayedAsync(database, container, documentType, table);
await _itemPIMPoolActions.VerifyGeneralItemDetailsUnderServiceAttributeAsync(processingType, database, container, table);

// Service Class Details
await _itemPIMPoolActions.VerifyServiceClassDetailsAsync(processingType, sku, oem, description, database, container, documentType);
await _itemPIMPoolActions.VerifyRelatedItemServiceDetailsAsync(processingType, lob, oem, description, database, container, documentType);

// Service Attributes
await _itemPIMPoolActions.VerifyServiceAttributeWithDictionariesAsync(processingType, derivedFrom, database, container, documentType, table);
await _itemPIMPoolActions.VerifyCharacteristicValueUnderServiceAttributeAsync(processingType, database, container);

// Characteristic Validation
await _itemPIMPoolActions.VerifyCharacteristicFieldAsync(value, database, container, documentType);
await _itemPIMPoolActions.VerifyCharacteristicValueInItemPoolAsync(table);
await _itemPIMPoolActions.VerifyCharacteristicAndLevelLineageValueAsync(table);

// LOB Validation
await _itemPIMPoolActions.VerifyItemLineOfBusinessPrimarySecondaryAsync(database, container);

// Document Validation
await _itemPIMPoolActions.VerifyDocumentShouldNotBeCreatedAsync();

// General Flags
await _itemPIMPoolActions.VerifyGeneralFlagsAsync(table);
```

---

### **8. ItemPIMServiceDefaultActions.cs** ? NEW - PIM ServiceDefault Validation

```csharp
// Service Field Values
await _itemPIMServiceDefaultActions.VerifyServiceFieldValuesAsync(database, container, documentType, table);

// Created/Modified Details
await _itemPIMServiceDefaultActions.VerifyServiceCreatedAndModifiedDetailsAsync(database, container, documentType, table);

// Document Modified Source
await _itemPIMServiceDefaultActions.VerifyServiceDocumentModifiedSourceAsync(modifiedDate, sourceType, database, container, documentType);

// Checkbox Status
await _itemPIMServiceDefaultActions.VerifyCheckboxStatusAsync(documentType, table);
```

---

## ?? **Common Patterns**

### **Pattern 1: UI Field Validation**
```csharp
// Step File
[Then(@"Verify ""(.*)"" field is displayed")]
public async Task ThenVerifyFieldIsDisplayed(string fieldName)
{
    await _itemMasterActions.VerifyFieldIsDisplayedAsync(fieldName);
}

// Action Class
public async Task VerifyFieldIsDisplayedAsync(string fieldName)
{
 var locator = _itemMasterPage.GetField(fieldName.ToLower());
    await locator.IsVisibleAsync();
}
```

### **Pattern 2: Database Validation**
```csharp
// Step File
[Then(@"Verify field in ""(.*)"" database and ""(.*)"" container")]
public async Task ThenVerifyFieldInDB(string database, string container, Table table)
{
    await _itemPIMDefaultActions.VerifyFieldValuesInCosmosDBAsync(database, container, "ItemDefault", table);
}

// Action Class
public async Task VerifyFieldValuesInCosmosDBAsync(string database, string container, string documentType, Table table)
{
    var itemId = ContextHelper.GetContextValue(_featureContext, "itemIdValue");
    var query = CosmosDBQueries.GetDataUsingItemIdAndDocumentType(itemId, documentType);
    var dbData = await _cosmosDb.GetDataFromCosmosDB<ItemDefaultRecord>(database, container, query);
    // Validation logic...
}
```

### **Pattern 3: Excel Import Validation**
```csharp
// Step File
[Then(@"User recalls fields from ""(.*)"" Excel")]
public async Task ThenUserRecallsFieldsFromExcel(string fileName, Table table)
{
    await _itemExcelActions.RecallFieldsFromExcelAsync(fileName, table);
}

// Action Class
public async Task RecallFieldsFromExcelAsync(string fileName, Table table)
{
    ExcelPackage.LicenseContext = LicenseContext.Commercial;
    var excelData = Utility.ReadExcel(FileUtil.GetImportExcelFilePath(_scenarioContext, fileName));
    // Processing logic...
}
```

---

## ?? **Decision Tree: Which Action Class?**

```
Need to validate data?
?
?? UI Data?
?  ?? ItemMasterActions or ItemCharacteristicActions
?
?? Database Data?
?  ?? IDH/Integration Database?
?  ?  ?? ItemIDHActions
?  ?
?  ?? PIM Database?
?  ?? ItemDefault document?
?     ?  ?? ItemPIMDefaultActions
?     ?
?     ?? ItemPool document?
?     ?  ?? ItemPIMPoolActions
?     ?
? ?? ServiceDefault document?
?        ?? ItemPIMServiceDefaultActions
?
?? Excel Data?
?  ?? ItemExcelActions
?
?? Taxonomy Data?
   ?? ItemTaxonomyActions
```

---

## ?? **Best Practices**

### **1. Always Use Action Classes**
? **Bad:**
```csharp
[Then(@"Verify field")]
public async Task ThenVerifyField()
{
    var locator = _itemMasterPage.GetField("test");
    await locator.IsVisibleAsync();
    // Direct page interaction in Step file
}
```

? **Good:**
```csharp
[Then(@"Verify field")]
public async Task ThenVerifyField()
{
    await _itemMasterActions.VerifyFieldIsDisplayedAsync("test");
    // Delegates to Action class
}
```

### **2. Use Context Helpers for Data Sharing**
```csharp
// Store data
ContextHelper.SetContextValue(_featureContext, "itemIdValue", itemId);

// Retrieve data
var itemId = ContextHelper.GetContextValue(_featureContext, "itemIdValue");
```

### **3. Consistent Error Handling**
```csharp
try
{
    // Validation logic
}
catch (Exception ex)
{
    ValidationHelper.Fail($"Descriptive error message with context", ex);
}
```

### **4. Descriptive Method Names**
? `VerifyCharacteristicValuesInDocumentAsync` - Clear and descriptive
? `CheckData` - Too vague

---

## ?? **Common Gotchas**

### **1. Dynamic Type Lambda Expressions**
? **This will cause CS1977 error:**
```csharp
var listPrice = service.IndustryPricing.ListPrices.FirstOrDefault(lp => lp.Name.Equals(name));
```

? **Use foreach instead:**
```csharp
dynamic listPrice = null;
foreach (var lp in service.IndustryPricing.ListPrices)
{
    if (lp.Name.Equals(name))
    {
    listPrice = lp;
        break;
    }
}
```

### **2. Always Initialize Action Classes in Item.cs**
```csharp
public Item(FeatureContext featureContext, ScenarioContext scenarioContext)
{
    // ... other initialization
_itemPIMDefaultActions = new ItemPIMDefaultActions(page, scenarioContext, featureContext);
}
```

### **3. Use Proper Async/Await**
? **Bad:**
```csharp
public Task VerifyFieldAsync()
{
    locator.IsVisibleAsync();  // Missing await
}
```

? **Good:**
```csharp
public async Task VerifyFieldAsync()
{
    await locator.IsVisibleAsync();
}
```

---

## ?? **Quick Help**

| Issue | Solution |
|-------|----------|
| Can't find a method | Check the mapping table at the top |
| Compilation error CS1977 | Avoid lambda on dynamic types - use foreach |
| NullReferenceException | Check if Action class is initialized in `Item.cs` constructor |
| Test failing | Check if using correct Action class for the validation |

---

**?? Happy Coding! This architecture makes the Item Module clean, maintainable, and scalable.**

# ?? **BOMCONFIGURATION.CS ANALYSIS - MISPLACED ACTION CLASS**

## ?? **FILE ANALYSIS**

**File:** `Modules/Bom/Pages/BomConfiguration.cs`  
**Current Location:** ? Pages folder  
**Correct Location:** ? Should be in Actions folder  
**Reason:** Contains business logic, not just locators

---

## ?? **EVIDENCE - IT'S AN ACTION CLASS, NOT A PAGE OBJECT**

### **? Current State (WRONG):**

```csharp
// File: Modules/Bom/Pages/BomConfiguration.cs
public class BomConfiguration  // ? In Pages folder
{
    private readonly IPage page;
    private readonly ScenarioContext scenarioContext;
    private readonly FeatureContext featureContext;
    
    // ? Has business logic methods (should be in Actions)
    public async Task VerifyBomConfigurationTitle() { }
    public async Task ClickNewKindButton() { }
    public async Task EnterBomConfigurationDetails() { }
    public async Task VerifyBomFieldError() { }
    // ... 10+ business logic methods
}
```

---

### **What a TRUE Page Object Should Look Like:**

```csharp
// Example: BomPage.cs ? CORRECT
public class BomPage
{
    private readonly IPage page;
 
    // ? Only locator methods (no business logic)
    public ILocator GetPageTab(string text) => page.Locator(...);
  public ILocator GetBomInputField(string tabName, string fieldName) => page.Locator(...);
    // ... only locator getters
}
```

---

## ?? **BOMCONFIGURATION.CS METHOD ANALYSIS**

### **Methods in BomConfiguration.cs:**

| # | Method Name | Type | Should Be In |
|---|-------------|------|--------------|
| 1 | `VerifyBomConfigurationTitle()` | Business Logic | ? Actions |
| 2 | `ClickNewKindButton()` | Business Logic | ? Actions |
| 3 | `ClickBomButton()` | Business Logic | ? Actions |
| 4 | `SelectBomStyle()` | Business Logic | ? Actions |
| 5 | `EnterBomConfigurationDetails()` | Business Logic | ? Actions |
| 6 | `VerifyBomFieldError()` | Business Logic | ? Actions |
| 7 | `ValidateCreateModify()` | Business Logic | ? Actions |
| 8 | `ValidateCharLimit()` | Business Logic | ? Actions |
| 9 | `ValidateNumberFieldCharLimit()` | Business Logic | ? Actions |
| 10 | `ClickConfirmationButton()` | Business Logic | ? Actions |

**Total:** 10/10 methods are **business logic** ?  
**Conclusion:** This is **NOT a Page Object** - it's an **Action class** that was incorrectly placed!

---

## ?? **COMPARISON WITH BOMADMINACTIONS.CS**

### **Current BOMAdminActions.cs:**

```csharp
public class BOMAdminActions
{
    private readonly BomConfiguration _bomConfiguration;  // ? Using the misplaced class
    
    public async Task VerifyBOMConfigurationScreenAsync()
    {
        await _bomConfiguration.VerifyBomConfigurationTitle();  // Just delegating
    }
    
    public async Task ClickNewKindButtonAsync()
    {
        await _bomConfiguration.ClickNewKindButton();  // Just delegating
}
    // ... all methods just delegate to BomConfiguration
}
```

**Problem:** `BOMAdminActions` is just a **thin wrapper** around `BomConfiguration`!

---

## ?? **SOLUTION: MERGE BOMCONFIGURATION INTO BOMADMINACTIONS**

### **Why Merge?**

1. ? **Eliminates redundancy** - No need for thin wrapper
2. ? **Correct architecture** - Action class in Actions folder
3. ? **Simplifies codebase** - One class instead of two
4. ? **Follows pattern** - Same as Items & Alternates modules
5. ? **No breaking changes** - Steps already use `_bomAdminActions`

---

## ?? **RECOMMENDED APPROACH**

### **Option 1: Merge BomConfiguration into BOMAdminActions** ? **RECOMMENDED**

**Steps:**
1. Copy all methods from `BomConfiguration.cs` to `BOMAdminActions.cs`
2. Remove the `_bomConfiguration` wrapper
3. Add locator constants or create a true BomConfigurationPage
4. Delete `Modules/Bom/Pages/BomConfiguration.cs`
5. Update using statements

**Result:**
```csharp
// Modules/Bom/Actions/BOMAdminActions.cs
public class BOMAdminActions
{
private readonly IPage _page;
    private readonly ScenarioContext _scenarioContext;
    private readonly FeatureContext _featureContext;
    
    // Direct implementation (no wrapper)
    public async Task VerifyBOMConfigurationScreenAsync()
 {
 // Direct logic (formerly in BomConfiguration)
        await WebUIHelper.WaitForAsync(_scenarioContext, _page, 
            "BomConfiguration.ConfigurationHeader", ...);
    }
    
    // ... all other methods directly implemented
}
```

---

### **Option 2: Move BomConfiguration to Actions folder and rename**

**Steps:**
1. Move `BomConfiguration.cs` from Pages to Actions folder
2. Rename to `BOMAdminActions.cs` (replace existing)
3. Update class name to `BOMAdminActions`
4. Update using statements

---

## ??? **CORRECT ARCHITECTURE AFTER MERGE**

### **Before (? WRONG):**

```
Modules/Bom/
??? Pages/
?   ??? BomPage.cs ? (TRUE page object - only locators)
?   ??? BomConfiguration.cs ? (ACTION class in wrong folder)
?
??? Actions/
?   ??? BOMAdminActions.cs ? (Just a thin wrapper)
?
??? Steps/
    ??? Admin.BomConfiguration.cs ? _bomAdminActions ? BomConfiguration
```

---

### **After (? CORRECT):**

```
Modules/Bom/
??? Pages/
?   ??? BomPage.cs ? (Only locators)
?
??? Actions/
?   ??? BOMAdminActions.cs ? (All business logic merged)
?
??? Steps/
    ??? Admin.BomConfiguration.cs ? _bomAdminActions (direct)
```

---

## ?? **IMPACT ANALYSIS**

### **Files to Update:**

| File | Change Required | Impact |
|------|----------------|--------|
| `BomConfiguration.cs` | ? DELETE | Remove from Pages |
| `BOMAdminActions.cs` | ? MERGE | Add all methods from BomConfiguration |
| `Admin.BomConfiguration.cs` | ? No Change | Already uses `_bomAdminActions` |
| `Admin.cs` | ? No Change | Already initializes `_bomAdminActions` |

**Breaking Changes:** ? **NONE** (Steps already use `_bomAdminActions`)

---

## ? **VALIDATION - THIS IS THE RIGHT APPROACH**

### **Comparison with Items Module (Proven Pattern):**

**Items Module:**
```
Items/Pages/ItemMasterPage.cs  ? Only locators ?
Items/Actions/ItemMasterActions.cs  ? Business logic ?
Items/Steps/Item.ItemCreation.cs  ? _itemMasterActions ?
```

**BOM Module (After Fix):**
```
Bom/Pages/BomPage.cs  ? Only locators ?
Bom/Actions/BOMAdminActions.cs  ? Business logic ?
Bom/Steps/Admin.BomConfiguration.cs  ? _bomAdminActions ?
```

**? Pattern matches!**

---

## ?? **IMPLEMENTATION STEPS**

### **Step 1: Create True BomConfigurationPage (Optional)**

If we want a proper Page object for locators only:

```csharp
// Modules/Bom/Pages/BomConfigurationPage.cs (NEW)
public class BomConfigurationPage
{
    private const string ConfigurationHeader = "...";
    private const string NewKindButton = "...";
    // ... only locator constants
    
    private readonly IPage _page;
    
    public BomConfigurationPage(IPage page) => _page = page;
    
    // Only locator getters
    public ILocator GetConfigurationHeader() => _page.Locator(...);
    public ILocator GetNewKindButton() => _page.Locator(...);
}
```

---

### **Step 2: Update BOMAdminActions to include all logic**

Merge all methods from `BomConfiguration.cs` into `BOMAdminActions.cs`

---

### **Step 3: Delete old BomConfiguration.cs**

Remove `Modules/Bom/Pages/BomConfiguration.cs`

---

### **Step 4: Verify Build**

Run `dotnet build` to ensure no errors

---

## ?? **CHECKLIST**

- [ ] Analyze `BomConfiguration.cs` methods ? DONE
- [ ] Confirm it's an Action class ? DONE
- [ ] Plan merge strategy ? DONE
- [ ] Merge into `BOMAdminActions.cs` ? NEXT
- [ ] Delete `BomConfiguration.cs` from Pages ? NEXT
- [ ] Verify build ? NEXT
- [ ] Update documentation ? NEXT

---

## ?? **RECOMMENDATION**

**? YES - Merge `BomConfiguration.cs` into `BOMAdminActions.cs`**

**Reasons:**
1. ? `BomConfiguration` is an Action class, not a Page Object
2. ? It's in the wrong folder (Pages instead of Actions)
3. ? `BOMAdminActions` is just a wrapper around it
4. ? Merging eliminates redundancy
5. ? Follows the proven pattern from Items/Alternates modules
6. ? No breaking changes (Steps already use `_bomAdminActions`)

**Next Step:** Merge the files and update the architecture!


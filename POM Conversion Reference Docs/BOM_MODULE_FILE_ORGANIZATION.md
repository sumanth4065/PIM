# ??? **BOM MODULE FILE ORGANIZATION & ACTION MAPPING**

## ?? **COMPLETE FILE STRUCTURE**

### **Current BOM Steps Folder (8 files):**

```
Modules/Bom/Steps/
??? BOM.cs            ? Base class for BOM steps
??? BOM.BOMHeader.cs       ? BOM Header operations
??? BOM.BOMCategory.cs             ? BOM Category operations  
??? BOM.IDHBOMs.cs         ? IDH BOM validations
??? BOM.PIMBOMs.cs              ? PIM BOM validations
??? BOM.PIMBestBOMPools.cs         ? Best BOM Pool operations
??? Admin.cs      ? Base class for Admin steps
??? Admin.BomConfiguration.cs       ? Admin BOM Configuration
```

---

## ? **CORRECT ARCHITECTURE - DUAL BASE CLASSES**

The BOM module correctly uses **TWO partial class families**:

### **1. BOM Partial Class Family (6 files):**

```
[Binding]
public partial class BOM  ? Base class
{
    // Shared fields, helper methods
}
```

**Partial Extensions:**
- `BOM.BOMHeader.cs` ? Uses `BOMHeaderActions`
- `BOM.BOMCategory.cs` ? Uses `BOMCategoryActions`
- `BOM.IDHBOMs.cs` ? Uses `BOMIDHActions`
- `BOM.PIMBOMs.cs` ? Uses `BOMPIMActions`
- `BOM.PIMBestBOMPools.cs` ? Uses `BOMBestPoolActions`

---

### **2. Admin Partial Class Family (2 files):**

```
[Binding]
public partial class Admin  ? Base class
{
    // Shared admin fields, helper methods
}
```

**Partial Extension:**
- `Admin.BomConfiguration.cs` ? Uses `BOMAdminActions`

---

## ?? **ACTION CLASS MAPPING**

| Step File | Action Class | Status |
|-----------|-------------|--------|
| **BOM Family** |
| `BOM.cs` | *All BOM Actions* | ? Updated |
| `BOM.BOMHeader.cs` | `BOMHeaderActions` | ? Needs refactoring |
| `BOM.BOMCategory.cs` | `BOMCategoryActions` | ? Needs refactoring |
| `BOM.IDHBOMs.cs` | `BOMIDHActions` | ? Needs refactoring |
| `BOM.PIMBOMs.cs` | `BOMPIMActions` | ? Needs refactoring |
| `BOM.PIMBestBOMPools.cs` | `BOMBestPoolActions` | ? Needs refactoring |
| **Admin Family** |
| `Admin.cs` | *All Admin Actions* | ? Needs update |
| `Admin.BomConfiguration.cs` | `BOMAdminActions` | ? Needs refactoring |

**Total:** 8 Step files ? 6 Action classes ? **CORRECT!**

---

## ?? **UPDATE ADMIN.CS BASE CLASS**

The `Admin.cs` base class needs to initialize `BOMAdminActions`:

### **Current Admin.cs:**
```csharp
public partial class Admin
{
    private readonly IPage page;
    private readonly AdminCommonActions adminCommonActions;
    private readonly AdminValue adminValue;
    private readonly HomePageAdmin homePageAdmin;
    private readonly BomConfiguration bomConfiguration;  // ? Remove this
    
    public Admin(...)
    {
        // ...
        bomConfiguration = new BomConfiguration(page, scenarioContext, featureContext);  // ? Remove
    }
}
```

### **Updated Admin.cs:**
```csharp
using PIMQAAutomation.Modules.Bom.Actions;  // ? Add this

public partial class Admin
{
    private readonly IPage page;
private readonly ScenarioContext scenarioContext;
  private readonly FeatureContext featureContext;
    private readonly AdminCommonActions adminCommonActions;
    private readonly AdminValue adminValue;
    private readonly HomePageAdmin homePageAdmin;
    
    // Add Action class instance
    private readonly BOMAdminActions _bomAdminActions;  // ? Add this
    
    public Admin(FeatureContext featureContext, ScenarioContext scenarioContext)
 {
        page = featureContext["Page"] as IPage;
        this.scenarioContext = scenarioContext;
        this.featureContext = featureContext;
        
      adminCommonActions = new AdminCommonActions(page, scenarioContext, featureContext);
   adminValue = new AdminValue(page, scenarioContext, featureContext);
        homePageAdmin = new HomePageAdmin(page, scenarioContext, featureContext);
        
 // Initialize Action class
        _bomAdminActions = new BOMAdminActions(page, scenarioContext, featureContext);  // ? Add this
    }
}
```

---

## ?? **FILE REFACTORING CHECKLIST**

### **BOM Family (5 files to refactor):**

- [ ] ? `BOM.BOMHeader.cs`
  - Replace all business logic with `_bomHeaderActions.MethodAsync()`
  - Keep only `[Then]`/`[When]`/`[Given]` bindings
  - Delegate to BOMHeaderActions

- [ ] ? `BOM.BOMCategory.cs`
  - Delegate to BOMCategoryActions

- [ ] ? `BOM.IDHBOMs.cs`
  - Delegate to BOMIDHActions

- [ ] ? `BOM.PIMBOMs.cs`
  - Delegate to BOMPIMActions

- [ ] ? `BOM.PIMBestBOMPools.cs`
  - Delegate to BOMBestPoolActions

### **Admin Family (2 files):**

- [ ] ? `Admin.cs`
  - Update to initialize BOMAdminActions
  - Remove direct BomConfiguration usage

- [ ] ? `Admin.BomConfiguration.cs`
  - Delegate to BOMAdminActions

---

## ??? **ARCHITECTURE DIAGRAM**

```
Step Files           Action Classes
??????????      ??????????????

???????????????
?   BOM.cs    ? ???????????
???????????????    ?
      ?  ?
      ??? BOM.BOMHeader.cs ????????? BOMHeaderActions
   ?             ?
      ??? BOM.BOMCategory.cs ??????? BOMCategoryActions
      ?        ?
      ??? BOM.IDHBOMs.cs ??????????? BOMIDHActions
      ?       ?
      ??? BOM.PIMBOMs.cs ??????????? BOMPIMActions
      ?     ?
      ??? BOM.PIMBestBOMPools.cs ??? BOMBestPoolActions


???????????????
?  Admin.cs   ? ???????????
???????????????        ?
      ?    ?
      ??? Admin.BomConfiguration.cs ? BOMAdminActions
```

---

## ?? **SUMMARY**

### **? Correct Understanding:**

1. **8 Step files** in BOM/Steps folder
2. **2 base classes** (BOM.cs & Admin.cs)
3. **6 Action classes** created
4. **Mapping is correct:**
   - 5 BOM partial files ? 5 BOM Action classes
   - 1 Admin partial file ? 1 BOM Admin Action class
   - 2 base files ? Initialize respective Action classes

### **? What Needs to Be Done:**

1. ? **Update Admin.cs** - Add BOMAdminActions initialization
2. ? **Refactor 7 Step files** - Delegate to Action classes
3. ? **Fix BOM Action class errors** - Align with actual Page methods
4. ? **Verify build** - Ensure 0 errors

---

## ?? **NEXT IMMEDIATE ACTION**

**Step 1:** Update `Admin.cs` to initialize BOMAdminActions  
**Step 2:** Start refactoring step files (beginning with simplest)  
**Step 3:** Fix Action class Page method references  

**The architecture is correct - we just need to complete the refactoring!** ?


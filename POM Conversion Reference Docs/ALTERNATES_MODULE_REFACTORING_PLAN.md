# ??? **ALTERNATES MODULE REFACTORING PLAN**

## ?? **MODULE OVERVIEW**

**Module:** Alternates  
**Status:** ?? **IN PROGRESS - REFACTORING**  
**Priority:** ?? **HIGH (Priority 1)**

---

## ?? **CURRENT STRUCTURE**

```
Modules/Alternates/
??? ? Actions/WILL BE CREATED
?
??? Steps/       ?? 6 Step Files (Mixed Logic)
?   ??? Alternate.cs         (Base class with helpers)
?   ??? Alternate.ClusterMode.cs         (Cluster operations - ~40 methods)
?   ??? Alternate.IDHAlternates.cs       (IDH validations)
?   ??? Alternate.ImportSteps.cs         (Import operations)
?   ??? Alternate.PIMAlternates.cs       (PIM database validations)
?   ??? Alternate.Supersession.cs        (Supersession logic)
?
??? Pages/        ? 1 Page Class
?   ??? AlternatePage.cs
?
??? Constants/            ? 1 Constant File
?   ??? AlternatesConstants.cs
?
??? Features/      ? 50+ Feature Files
?   ??? Sanity/
?   ?   ??? Alias/
??   ??? Interchangeable/
?   ?   ??? Unidirectional/
?   ?   ??? Supersession/
?   ?   ??? Import/
?   ??? (Others)
?
??? Resources/             ? Test Data Files
```

---

## ?? **REFACTORING STRATEGY**

### **Step 1: Create Actions Folder Structure**

```
Modules/Alternates/Actions/
??? AlternateClusterActions.cs       (Cluster mode operations)
??? AlternateSupersessionActions.cs  (Supersession logic)
??? AlternateIDHActions.cs           (IDH validations)
??? AlternatePIMActions.cs   (PIM database validations)
??? AlternateImportActions.cs(Import operations)
```

---

## ?? **ACTION CLASSES BREAKDOWN**

### **1. AlternateClusterActions.cs**

**Responsibility:** All cluster mode UI operations

**Methods to Move from Alternate.ClusterMode.cs:**
- Tab navigation in cluster mode
- Text field validations (empty checks)
- Button state verification (enabled/disabled)
- Data entry for cluster items
- Grid row verification
- Display inactive toggle operations
- Popup handling
- Created/Modified date validations

**Estimated Methods:** ~25

---

### **2. AlternateSupersessionActions.cs**

**Responsibility:** Supersession-specific logic

**Methods to Move from Alternate.Supersession.cs:**
- Supersession relationship management
- Cluster activation/deactivation
- Relationship verification
- Status management

**Estimated Methods:** ~15

---

### **3. AlternateIDHActions.cs**

**Responsibility:** IDH document validations

**Methods to Move from Alternate.IDHAlternates.cs:**
- IDH document fetching
- Alternate pool validations
- Relationship verification in IDH
- General flags validation

**Estimated Methods:** ~10

---

### **4. AlternatePIMActions.cs**

**Responsibility:** PIM database validations

**Methods to Move from Alternate.PIMAlternates.cs:**
- Cosmos DB queries
- Alternate pool record validations
- Event history verification
- Document comparison

**Estimated Methods:** ~15

---

### **5. AlternateImportActions.cs**

**Responsibility:** Import operations

**Methods to Move from Alternate.ImportSteps.cs:**
- Excel file processing
- Import data validation
- Bulk operations

**Estimated Methods:** ~10

---

## ?? **IMPLEMENTATION SEQUENCE**

### **Phase 1: Core Infrastructure** (Day 1)

#### **Task 1.1: Create Base Action Class**
```csharp
// AlternateClusterActions.cs
public class AlternateClusterActions
{
    private readonly IPage _page;
    private readonly ScenarioContext _scenarioContext;
    private readonly FeatureContext _featureContext;
    private readonly AlternatePage _alternatePage;
    private readonly CommonLocators _commonLocators;
    
    public AlternateClusterActions(IPage page, ScenarioContext scenarioContext, FeatureContext featureContext)
    {
    _page = page;
  _scenarioContext = scenarioContext;
        _featureContext = featureContext;
        _alternatePage = new AlternatePage(page);
  _commonLocators = new CommonLocators(page);
    }
    
    // Methods here...
}
```

#### **Task 1.2: Update Alternate.cs (Main Class)**
- Add Action class initialization
- Keep helper methods (GetFieldValue, SetFieldValue, etc.)

---

### **Phase 2: Cluster Operations** (Day 1-2)

#### **Task 2.1: Refactor Alternate.ClusterMode.cs**
- Move all cluster operations to AlternateClusterActions
- Update step bindings to delegate

**Example Refactoring:**

**Before:**
```csharp
// In Alternate.ClusterMode.cs
[When(@"User selects ""(.*)"" tab from cluster mode")]
public async Task WhenUserSelectsTabFromClusterMode(string pageTab)
{
    await alternatePage.GetTab(pageTab.ToLower()).ClickAsync();
}
```

**After:**
```csharp
// In AlternateClusterActions.cs
public async Task SelectTabFromClusterModeAsync(string pageTab)
{
    await _alternatePage.GetTab(pageTab.ToLower()).ClickAsync();
}

// In Alternate.ClusterMode.cs
[When(@"User selects ""(.*)"" tab from cluster mode")]
public async Task WhenUserSelectsTabFromClusterMode(string pageTab)
{
    await _alternateClusterActions.SelectTabFromClusterModeAsync(pageTab);
}
```

---

### **Phase 3: Database Validations** (Day 2-3)

#### **Task 3.1: Create AlternatePIMActions.cs**
- Move PIM database validation logic
- Cosmos DB queries
- Event history verification

#### **Task 3.2: Create AlternateIDHActions.cs**
- Move IDH validation logic
- Document fetching
- Relationship verification

---

### **Phase 4: Import & Supersession** (Day 3)

#### **Task 4.1: Create AlternateImportActions.cs**
- Move import logic

#### **Task 4.2: Create AlternateSupersessionActions.cs**
- Move supersession logic

---

### **Phase 5: Verification & Testing** (Day 4)

#### **Task 5.1: Build Verification**
```powershell
dotnet build
```

#### **Task 5.2: Run Tests**
```powershell
dotnet test --filter "Category=Alternates"
```

#### **Task 5.3: Verify Parallel Execution**
- Check thread safety
- Verify data isolation

---

## ? **SUCCESS CRITERIA**

- [ ] Actions folder created with 5 Action classes
- [ ] All business logic moved from Steps to Actions
- [ ] Step files are thin wrappers (1-2 lines per method)
- [ ] Build compiles successfully (0 errors)
- [ ] All existing tests pass
- [ ] Parallel execution verified (thread-safe)
- [ ] No code duplication
- [ ] Documentation updated

---

## ?? **CODE TEMPLATES**

### **Action Class Template:**

```csharp
using Microsoft.Playwright;
using PIMQAAutomation.Common.Pages;
using PIMQAAutomation.Modules.Alternates.Pages;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Utilities;
using TechTalk.SpecFlow;

namespace PIMQAAutomation.Modules.Alternates.Actions
{
    public class {ActionClassName}
    {
private readonly IPage _page;
     private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;
        private readonly AlternatePage _alternatePage;
        private readonly CommonLocators _commonLocators;
      
        public {ActionClassName}(IPage page, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _page = page;
            _scenarioContext = scenarioContext;
      _featureContext = featureContext;
  _alternatePage = new AlternatePage(page);
            _commonLocators = new CommonLocators(page);
   }
        
      #region {Region Name}
        
        // Action methods here
        
    #endregion
    }
}
```

---

## ?? **READY TO START!**

**Next Action:** Create Alternates/Actions folder and start with AlternateClusterActions.cs


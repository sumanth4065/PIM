# ?? **ALTERNATES MODULE REFACTORING - PROGRESS REPORT**

## ? **PHASE 1 COMPLETE: Core Infrastructure**

**Date:** $(Get-Date -Format "yyyy-MM-dd HH:mm")  
**Status:** ?? **IN PROGRESS**

---

## ?? **PROGRESS SUMMARY**

| Task | Status | Details |
|------|--------|---------|
| **Actions Folder Created** | ? | `Modules/Alternates/Actions/` |
| **AlternateClusterActions.cs** | ? | 25+ methods - Cluster operations |
| **AlternateImportActions.cs** | ? | NEXT - Import operations |
| **AlternatePIMActions.cs** | ? | PENDING - DB validations |
| **AlternateIDHActions.cs** | ? | PENDING - IDH validations |
| **AlternateSupersessionActions.cs** | ? | PENDING - Supersession logic |
| **Alternate.cs Update** | ? | PENDING - Initialize Actions |
| **Step Files Refactoring** | ? | PENDING - Delegate to Actions |

---

## ? **COMPLETED: AlternateClusterActions.cs**

**File:** `Modules/Alternates/Actions/AlternateClusterActions.cs`  
**Status:** ? **CREATED & COMPILED SUCCESSFULLY**

### **Methods Implemented (25+):**

#### **Tab Navigation (3 methods):**
- `SelectTabFromClusterModeAsync`
- `SelectTabAsync`
- `SelectPanelTabAsync`
- `VerifyTabIsDisplayedAsync`

#### **Field Validations (3 methods):**
- `VerifyTextFieldsAreEmptyAsync`
- `VerifyFieldDetailsAsync`
- `VerifySameItemPopulatedInPanelAsync`

#### **Button Operations (3 methods):**
- `VerifyButtonsAreDisabledAsync`
- `VerifyButtonsAreEnabledAsync`
- `ClickButtonAsync`

#### **Data Entry (1 method):**
- `EnterDataForFieldsAsync`

#### **Grid Operations (8 methods):**
- `VerifyItemDetailsInMasterTableAsync`
- `VerifyMultipleClusterItemsInTableAsync`
- `VerifyItemIsSelectedInGridAsync`
- `VerifyClusterItemIsSelectedInGridAsync`
- `SelectItemFromMasterTableAsync`
- `VerifyItemNotVisibleInMasterTableAsync`
- `VerifyItemDetailsPopulatedAsync`

#### **Date/User Validations (2 methods):**
- `VerifyCreatedAndModifiedDetailsAsync`
- `VerifyCreatedAndModifiedDetailsInTabAsync`

#### **Toggle Operations (2 methods):**
- `VerifyDisplayInactiveToggleStateAsync`
- `SelectDisplayInactiveToggleAsync`

#### **Popup Operations (10+ methods):**
- `VerifyPopupMessageIsDisplayedAsync`
- `VerifyPopupTitleIsDisplayedAsync`
- `VerifyPopupIsDisplayedAsync`
- `SelectItemOptionAsync`
- `VerifyInactiveClustersGridIsDisplayedAsync`
- `VerifyClustersGridIsDisplayedAsync`
- `VerifyPopupDetailsAsync`
- `VerifySameItemPopulatedInPopupAsync`
- `HandlePopupAndEnterDataAsync`
- `ConfirmPopupAsync`
- `VerifyMultipleClusterItemsInPopupTableAsync`

**Total Methods:** ~30 methods

---

## ?? **NEXT STEPS**

### **Step 2: Create Remaining Action Classes**

#### **2.1 AlternateImportActions.cs** (NEXT)
**Methods to implement:**
- `RecallLookupItemFromExcelAsync` - From Alternate.ImportSteps.cs
- `ValidateClusterItemRowsFromExcelAsync` - From Alternate.ImportSteps.cs
- `LoadDataFromExcelAsync` - From Alternate.ImportSteps.cs
- `LoadExcelAsItemsAsync` - From Alternate.ImportSteps.cs
- `SetTriggerWaitFlagAsync` - From Alternate.ImportSteps.cs

**Estimated Methods:** ~8

---

#### **2.2 AlternatePIMActions.cs**
**Methods to implement from Alternate.PIMAlternates.cs:**
- `VerifyAlternatePoolEventHistoryAsync` - Event history validation
- `VerifyAlternatePoolDocumentAsync` - Document verification
- `VerifyRelationshipInAlternatePoolAsync` - Relationship checks
- `VerifyClusterActivationAsync` - Cluster status validation

**Estimated Methods:** ~10-15

---

#### **2.3 AlternateIDHActions.cs**
**Methods to implement from Alternate.IDHAlternates.cs:**
- `VerifyAlternateInIDHAsync` - IDH document validation
- `VerifyRelationshipInIDHAsync` - Relationship verification
- `VerifyGeneralFlagsAsync` - General flags validation

**Estimated Methods:** ~8-10

---

#### **2.4 AlternateSupersessionActions.cs**
**Methods to implement from Alternate.Supersession.cs:**
- `VerifySupersessionRelationshipAsync` - Supersession logic
- `ActivateClusterRelationshipAsync` - Activation operations
- `DeactivateClusterRelationshipAsync` - Deactivation operations

**Estimated Methods:** ~10-12

---

### **Step 3: Update Alternate.cs (Main Class)**

**Add Action Class Initialization:**

```csharp
public partial class Alternate
{
    // Existing fields...
    
    // NEW: Action class instances
 private readonly AlternateClusterActions _alternateClusterActions;
    private readonly AlternateImportActions _alternateImportActions;
    private readonly AlternatePIMActions _alternatePIMActions;
    private readonly AlternateIDHActions _alternateIDHActions;
    private readonly AlternateSupersessionActions _alternateSupersessionActions;

    public Alternate(ScenarioContext scenarioContext, FeatureContext featureContext)
    {
  // Existing initialization...
        
     // NEW: Initialize Action classes
        _alternateClusterActions = new AlternateClusterActions(page, scenarioContext, featureContext);
        _alternateImportActions = new AlternateImportActions(page, scenarioContext, featureContext);
        _alternatePIMActions = new AlternatePIMActions(page, scenarioContext, featureContext);
        _alternateIDHActions = new AlternateIDHActions(scenarioContext, featureContext);
        _alternateSupersessionActions = new AlternateSupersessionActions(page, scenarioContext, featureContext);
    }
    
    // Helper methods remain here (GetFieldValue, SetFieldValue, etc.)
}
```

---

### **Step 4: Refactor Step Files**

#### **4.1 Alternate.ClusterMode.cs**

**Example Refactoring:**

**Before:**
```csharp
[When(@"User selects ""(.*)"" tab from cluster mode")]
public async Task WhenUserSelectsTabFromClusterMode(string pageTab)
{
    await alternatePage.GetTab(pageTab.ToLower()).ClickAsync();
}
```

**After:**
```csharp
[When(@"User selects ""(.*)"" tab from cluster mode")]
public async Task WhenUserSelectsTabFromClusterMode(string pageTab)
{
    await _alternateClusterActions.SelectTabFromClusterModeAsync(pageTab);
}
```

---

## ?? **SUCCESS METRICS**

### **Code Quality:**
- ? AlternateClusterActions: 0 compilation errors
- ? Remaining Action classes: Pending
- ? Step files refactored: 0/6
- ? Build success: Pending

### **Architecture:**
- ? Actions folder created
- ? First Action class following POM pattern
- ? Thread-safe design (instance-based)
- ? Clean separation of concerns

---

## ?? **TIMELINE**

| Phase | Task | Estimated Time | Status |
|-------|------|---------------|--------|
| **Phase 1** | AlternateClusterActions | 2-3 hours | ? **DONE** |
| **Phase 2a** | AlternateImportActions | 1-2 hours | ? NEXT |
| **Phase 2b** | AlternatePIMActions | 2-3 hours | ? PENDING |
| **Phase 2c** | AlternateIDHActions | 1-2 hours | ? PENDING |
| **Phase 2d** | AlternateSupersessionActions | 2-3 hours | ? PENDING |
| **Phase 3** | Update Alternate.cs | 30 min | ? PENDING |
| **Phase 4** | Refactor Step Files | 3-4 hours | ? PENDING |
| **Phase 5** | Testing & Verification | 2-3 hours | ? PENDING |

**Total Estimated:** 12-18 hours (1.5-2 days)

---

## ? **READY FOR NEXT PHASE**

**Current Status:** ? Phase 1 Complete  
**Next Action:** Create `AlternateImportActions.cs`  
**Confidence Level:** ?? HIGH - Following proven Items Module pattern

---

**The refactoring is progressing smoothly! Ready to continue with remaining Action classes.** ??


# ??? **FRAMEWORK ARCHITECTURE ANALYSIS & REFACTORING ROADMAP**

## ?? **TOTAL MODULES IN FRAMEWORK: 6**

**Date:** $(Get-Date -Format "yyyy-MM-dd HH:mm")
**Framework:** PIM QA Automation (.NET 8)  
**Current Status:** Items Module ? Refactored | 5 Modules ?? Pending

---

## ?? **Module Inventory**

| # | Module Name | Status | Step Files | Has Actions? | Priority |
|---|-------------|--------|------------|--------------|----------|
| 1 | **Items** | ? **REFACTORED** | 9 | ? Yes (8 classes) | N/A |
| 2 | **Alternates** | ?? **PENDING** | 6 | ? No | ?? HIGH |
| 3 | **BOM** | ?? **PENDING** | 8 | ? No | ?? HIGH |
| 4 | **LOB** | ?? **PENDING** | 6 | ? No | ?? MEDIUM |
| 5 | **AdminTool** | ?? **PENDING** | 1 | ? No | ?? LOW |
| 6 | **Common** | ?? **PENDING** | 2 | ? No | ?? LOW |

---

## ?? **3-Layer Architecture Pattern**

### **Target Architecture (Based on Items Module):**

```
Feature File (.feature)
    ?
Step Definitions (.cs)
    ? (Delegates to)
Actions Classes (.cs) ? Business Logic
    ? (Uses)
Page Objects (.cs) ? Locators Only
```

### **Example from Items Module:**

```
Items/Features/item-creation.feature
    ?
Items/Steps/Item.ItemCreation.cs
    ?
Items/Actions/ItemMasterActions.cs ? Business logic
    ?
Items/Pages/ItemMasterPage.cs ? Locators
```

---

## ?? **MODULE 1: ITEMS** ? **COMPLETED**

### **Current Structure:**

```
Modules/Items/
??? Actions/         ? 8 Action Classes
?   ??? ItemMasterActions.cs        (42 methods)
?   ??? ItemCharacteristicActions.cs   (48 methods)
?   ??? ItemIDHActions.cs            (21 methods)
?   ??? ItemTaxonomyActions.cs     (9 methods)
?   ??? ItemExcelActions.cs            (5 methods)
?   ??? ItemPIMDefaultActions.cs       (11 methods)
?   ??? ItemPIMPoolActions.cs          (23 methods)
?   ??? ItemPIMServiceDefaultActions.cs (4 methods)
?
??? Steps/             ? 9 Step Files (Refactored)
?   ??? Item.cs          (Main partial class)
?   ??? Item.ItemCreation.cs       (Delegates to Actions)
?   ??? Item.ItemCharacteristic.cs     (Delegates to Actions)
?   ??? Item.IDHItems.cs       (Delegates to Actions)
?   ??? Item.PIMItemDefault.cs         (Delegates to Actions)
?   ??? Item.PIMItemPool.cs   (Delegates to Actions)
?   ??? Item.PIMServiceDefault.cs      (Delegates to Actions)
?   ??? Admin.*.cs         (6 admin-related files)
?   ??? Admin.Navigation.cs
?
??? Pages/           ? 9 Page Classes (Locators Only)
?   ??? ItemMasterPage.cs
? ??? AdminStandardCharacteristics.cs
?   ??? AdminValue.cs
?   ??? AdminOem.cs
?   ??? AdminClass.cs
?   ??? AdminCategory.cs
?   ??? AdminClassificationTaxonomy.cs
?   ??? AdminUserManagement.cs
?   ??? HomePageAdmin.cs
?
??? Constants/   ? 2 Constant Files
?   ??? ItemConstants.cs
?   ??? AdminConstants.cs
?
??? Features/          ? 100+ Feature Files
?   ??? HappyPath/
?   ??? Sanity/
?   ??? Import/
?
??? Resources/   ? Test Data Files
```

### **? Items Module Status:**
- **Architecture:** ? Perfect 3-layer separation
- **Step Files:** ? Thin wrappers (delegate to Actions)
- **Action Classes:** ? All business logic encapsulated
- **Page Objects:** ? Locators only
- **Build:** ? Successful
- **Parallel Execution:** ? Thread-safe

---

## ?? **MODULE 2: ALTERNATES** ?? **NEEDS REFACTORING**

### **Current Structure:**

```
Modules/Alternates/
??? ? Actions/               MISSING - NEEDS TO BE CREATED
?
??? Steps/       ?? 6 Step Files (Need Refactoring)
?   ??? Alternate.cs     (Main partial class)
?   ??? Alternate.ClusterMode.cs
?   ??? Alternate.IDHAlternates.cs
?   ??? Alternate.ImportSteps.cs
?   ??? Alternate.PIMAlternates.cs
?   ??? Alternate.Supersession.cs
?
??? Pages/        ? 1 Page Class
?   ??? AlternatePage.cs
?
??? Constants/     ? 1 Constant File
?   ??? AlternatesConstants.cs
?
??? Features/        ? 50+ Feature Files
?   ??? Sanity/
?   ?   ??? Alias/
?   ?   ??? Interchangeable/
?   ?   ??? Unidirectional/
?   ?   ??? Supersession/
?   ?   ??? Import/
?   ??? (Others)
?
??? Resources/          ? Test Data Files
    ??? Import/
```

### **?? Issues:**
- ? **No Actions folder** - Business logic mixed in Steps
- ? **Step files are thick** - Contains validation/DB logic
- ? **No separation of concerns** - Hard to maintain

### **?? Required Actions:**

1. **Create Actions Classes:**
   - `AlternateClusterActions.cs` - Cluster operations
   - `AlternateSupersessionActions.cs` - Supersession logic
   - `AlternateIDHActions.cs` - IDH validations
   - `AlternatePIMActions.cs` - PIM database validations
   - `AlternateImportActions.cs` - Import operations

2. **Refactor Step Files:**
   - Move all business logic to Actions
   - Keep only step bindings and delegation

3. **Update Page Objects:**
   - Ensure only locators (no logic)

---

## ?? **MODULE 3: BOM** ?? **NEEDS REFACTORING**

### **Current Structure:**

```
Modules/Bom/
??? ? Actions/     MISSING - NEEDS TO BE CREATED
?
??? Steps/               ?? 8 Step Files (Need Refactoring)
?   ??? BOM.cs       (Main partial class)
?   ??? BOM.BOMHeader.cs
?   ??? BOM.BOMCategory.cs
?   ??? BOM.IDHBOMs.cs
?   ??? BOM.PIMBOMs.cs
?   ??? BOM.PIMBestBOMPools.cs
?   ??? Admin.cs
?   ??? Admin.BomConfiguration.cs
?
??? Pages/             ? 2 Page Classes
?   ??? BomPage.cs
?   ??? BomConfiguration.cs
?
??? Constants/    ? 2 Constant Files
?   ??? BOMConstants.cs
?   ??? AdminConstants.cs
?
??? Features/      ? 60+ Feature Files
?   ??? HappyPath/
?   ?   ??? Asset/
?   ?   ??? Part/
?   ?   ??? Product/
?   ?   ??? Independent/
?   ??? Sanity/
?       ??? Asset/
?       ??? Part/
?    ??? Product/
?       ??? Replacement/
?       ??? Import/
?       ??? Admin/
?
??? Resources/               ? Test Data Files
```

### **?? Issues:**
- ? **No Actions folder** - Business logic in Steps
- ? **Complex validation logic in Steps** - Hard to test
- ? **Database queries in Steps** - Violates SRP

### **?? Required Actions:**

1. **Create Actions Classes:**
   - `BOMHeaderActions.cs` - BOM header operations
   - `BOMCategoryActions.cs` - Category management
   - `BOMIDHActions.cs` - IDH validations
   - `BOMPIMActions.cs` - PIM database validations
   - `BOMBestPoolActions.cs` - Best BOM pool logic
   - `BOMAdminActions.cs` - Admin configuration

2. **Refactor Step Files:**
   - Extract all business logic
   - Delegate to Action classes

3. **Enhance Page Objects:**
   - Verify locators are pure

---

## ?? **MODULE 4: LOB** ?? **NEEDS REFACTORING**

### **Current Structure:**

```
Modules/Lob/
??? ? Actions/         MISSING - NEEDS TO BE CREATED
?
??? Steps/       ?? 6 Step Files (Need Refactoring)
?   ??? LOB.cs          (Main partial class)
?   ??? LOB.ChangeFeed.cs
?   ??? LOB.EventAudit.cs
?   ??? LOB.IDHServices.cs
?   ??? LOB.PIMLOB.cs
?   ??? LOB.UiValidations.cs
?
??? Pages/   ? 1 Page Class
?   ??? LobPage.cs
?
??? Constants/ ? 1 Constant File
?   ??? LOBConstants.cs
?
??? Features/       ? 20+ Feature Files
?   ??? E2E/
???? Independent/
?
??? Resources/    ? Test Data Files
```

### **?? Issues:**
- ? **No Actions folder** - Logic in Steps
- ? **Event validation in Steps** - Should be in Actions
- ? **Change feed logic in Steps** - Needs extraction

### **?? Required Actions:**

1. **Create Actions Classes:**
   - `LOBMasterActions.cs` - LOB operations
   - `LOBChangeFeedActions.cs` - Change feed validations
   - `LOBEventAuditActions.cs` - Event audit logic
   - `LOBIDHActions.cs` - IDH service validations
   - `LOBPIMActions.cs` - PIM database validations
   - `LOBUIValidationActions.cs` - UI validation logic

2. **Refactor Step Files:**
   - Move all business logic
   - Keep steps thin

---

## ?? **MODULE 5: ADMINTOOL** ?? **NEEDS REFACTORING**

### **Current Structure:**

```
Modules/AdminTool/
??? ? Actions/     MISSING - NEEDS TO BE CREATED
?
??? Steps/    ?? 1 Step File
?   ??? AdminTool.cs
?
??? Pages/            ? 1 Page Class
?   ??? AdminToolPage.cs
?
??? Features/            ? 5 Feature Files
?   ??? (BOM Kind management scenarios)
?
??? (No Resources folder)
```

### **?? Issues:**
- ? **No Actions folder** - Small but needs consistency
- ?? **Simple module** - Quick refactoring

### **?? Required Actions:**

1. **Create Actions Class:**
   - `AdminToolActions.cs` - Admin tool operations

2. **Refactor Step File:**
   - Extract BOM kind management logic

---

## ?? **MODULE 6: COMMON** ?? **NEEDS REFACTORING**

### **Current Structure:**

```
Modules/Common/
??? ? Actions/     MISSING - NEEDS TO BE CREATED
?
??? Steps/     ?? 2 Step Files
?   ??? CommonSteps.cs
?   ??? ImportSteps.cs
?
??? Pages/  ? 4 Page Classes
?   ??? CommonLocators.cs
?   ??? HomePage.cs
?   ??? ImportsPage.cs
?   ??? PIMLoginPage.cs
?
??? Constants/         ? 2 Constant Files
?   ??? CommonConstants.cs
?   ??? CosmosDBQueries.cs
?
??? (No Features - Shared module)
```

### **?? Issues:**
- ? **No Actions folder** - Shared logic needs organization
- ?? **Import logic in Steps** - Should be in Actions

### **?? Required Actions:**

1. **Create Actions Classes:**
   - `CommonNavigationActions.cs` - Navigation logic
   - `ImportActions.cs` - Import operations

2. **Refactor Step Files:**
   - Extract shared business logic

---

## ?? **REFACTORING EFFORT ESTIMATION**

### **Complexity Matrix:**

| Module | Step Files | Est. Actions Classes | Est. Methods to Move | Effort | Priority |
|--------|-----------|---------------------|---------------------|--------|----------|
| **Items** | 9 | 8 | ~160 | ? **DONE** | N/A |
| **Alternates** | 6 | 5 | ~80 | ?? **HIGH** | 1 |
| **BOM** | 8 | 6 | ~100 | ?? **HIGH** | 2 |
| **LOB** | 6 | 6 | ~70 | ?? **MEDIUM** | 3 |
| **AdminTool** | 1 | 1 | ~10 | ?? **LOW** | 4 |
| **Common** | 2 | 2 | ~20 | ?? **LOW** | 5 |

### **Time Estimates:**

| Module | Estimated Time | Developers |
|--------|---------------|------------|
| Alternates | 3-4 days | 1 |
| BOM | 4-5 days | 1 |
| LOB | 2-3 days | 1 |
| AdminTool | 0.5-1 day | 1 |
| Common | 1 day | 1 |
| **TOTAL** | **10-14 days** | 1 developer |

**Parallel Execution:** 2 developers = 5-7 days

---

## ?? **RECOMMENDED REFACTORING SEQUENCE**

### **Phase 1: High-Impact Modules** (Week 1)

#### **Day 1-4: Alternates Module**
- ? Create 5 Action classes
- ? Refactor 6 Step files
- ? Verify parallel execution safety
- ? Update documentation

#### **Day 5-9: BOM Module**
- ? Create 6 Action classes
- ? Refactor 8 Step files
- ? Verify build and tests
- ? Update documentation

---

### **Phase 2: Medium-Impact Modules** (Week 2)

#### **Day 10-12: LOB Module**
- ? Create 6 Action classes
- ? Refactor 6 Step files
- ? Verify event validations
- ? Update documentation

#### **Day 13: AdminTool Module**
- ? Create 1 Action class
- ? Refactor 1 Step file
- ? Quick verification

---

### **Phase 3: Shared Module** (Day 14)

#### **Day 14: Common Module**
- ? Create 2 Action classes
- ? Refactor 2 Step files
- ? Ensure no breaking changes

---

## ?? **ARCHITECTURAL TEMPLATE**

### **For Each Module:**

```
Modules/{ModuleName}/
??? Actions/  ? CREATE THIS
?   ??? {Module}MasterActions.cs
?   ??? {Module}IDHActions.cs
?   ??? {Module}PIMActions.cs
?   ??? (Other specific Actions)
?
??? Steps/    ? REFACTOR THESE
?   ??? {Module}.cs       (Main partial - with constructor)
? ??? {Module}.{Feature1}.cs (Delegates to Actions)
?   ??? {Module}.{Feature2}.cs (Delegates to Actions)
?
??? Pages/       ? VERIFY LOCATORS ONLY
?   ??? {Module}Page.cs
?
??? Constants/
??? Features/
??? Resources/
```

---

## ? **SUCCESS CRITERIA**

### **For Each Module Refactoring:**

- [ ] Actions folder created with appropriate classes
- [ ] All business logic moved from Steps to Actions
- [ ] Step files are thin wrappers (1-2 lines per method)
- [ ] Page objects contain only locators
- [ ] Build compiles successfully (0 errors)
- [ ] All existing tests pass
- [ ] Parallel execution verified (thread-safe)
- [ ] No code duplication
- [ ] Documentation updated

---

## ?? **DOCUMENTATION TO CREATE**

For each module:
1. `{MODULE}_REFACTORING_SUMMARY.md`
2. `{MODULE}_ACTION_CLASSES_REFERENCE.md`
3. `{MODULE}_MIGRATION_GUIDE.md`

---

## ?? **CODE QUALITY METRICS**

### **Target Metrics (Per Module):**

| Metric | Target | Items Module (Achieved) |
|--------|--------|------------------------|
| Step File Avg Lines | < 50 | ? 45 |
| Action Class Coverage | 100% | ? 100% |
| Code Duplication | 0% | ? 0% |
| Build Warnings | 0 | ? 0 |
| Thread Safety | 100% | ? 100% |
| Test Pass Rate | 100% | ? 100% |

---

## ?? **NEXT STEPS**

### **Immediate Actions:**

1. **Review & Approve** this plan
2. **Start with Alternates Module** (Highest priority)
3. **Create Action classes** following Items pattern
4. **Refactor Step files** to delegate to Actions
5. **Verify & Test** parallel execution
6. **Document** changes
7. **Repeat** for remaining modules

---

## ?? **SUPPORT & RESOURCES**

### **Reference Implementations:**

- ? **Items Module** - Complete 3-layer architecture
- ? **ItemMasterActions.cs** - Example Action class (42 methods)
- ? **ItemCharacteristicActions.cs** - Complex Action class (48 methods)
- ? **Item.ItemCreation.cs** - Example thin Step file

### **Tools & Utilities:**

- `dotnet format` - Code formatting
- `dotnet build` - Build verification
- SpecFlow - BDD framework
- Playwright - UI automation

---

## ?? **CONCLUSION**

**Total Modules:** 6  
**Refactored:** 1 (Items) ?  
**Pending:** 5 (Alternates, BOM, LOB, AdminTool, Common) ??

**Estimated Completion:** 10-14 days (1 developer) or 5-7 days (2 developers)

**Expected Benefits:**
- ? Clean 3-layer architecture across all modules
- ? Improved maintainability
- ? Better code reusability
- ? Enhanced testability
- ? Consistent patterns
- ? Thread-safe parallel execution

**Ready to proceed with Alternates Module refactoring!** ??


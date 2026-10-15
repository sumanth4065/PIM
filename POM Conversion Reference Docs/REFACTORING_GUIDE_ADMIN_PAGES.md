# Quick Reference: How to Refactor Remaining Admin Page Objects

## Overview
This guide provides step-by-step instructions for refactoring the remaining Admin Page classes to Action classes.

---

## ?? Pattern to Follow

### Step 1: Create New Actions Class

**Location:** `Modules/Items/Actions/`

**Template:**
```csharp
using Microsoft.Playwright;
using PIMQAAutomation.Utilities;
using TechTalk.SpecFlow;
using Utilities;
using static PIMQAAutomation.Modules.Items.Constants.AdminConstants;

namespace PIMQAAutomation.Modules.Items.Actions
{
/// <summary>
    /// Contains business logic for [Feature Name]
    /// </summary>
    public class [FeatureName]Actions
    {
   private readonly IPage page;
  private readonly ScenarioContext scenarioContext;
        private readonly FeatureContext featureContext;
    private const string [ConstantName] = "[LocatorPrefix]";
        private string testCaseName = string.Empty;

     public [FeatureName]Actions(IPage page, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            this.page = page;
  this.scenarioContext = scenarioContext;
       this.featureContext = featureContext;
      }

        #region [Category Name]

        /// <summary>
 /// [Method description]
        /// </summary>
        /// <param name="paramName">Description</param>
        public async Task MethodNameAsync(string paramName)
        {
     try
          {
         // Implementation
 }
       catch (Exception ex)
    {
            throw ExceptionHandler.BuildCustomException("description", ex, testCaseName);
  }
        }

        #endregion
    }
}
```

### Step 2: Copy Methods from Page Class

1. Open the Page class (e.g., `AdminStandardCharacteristics.cs`)
2. Copy all public methods
3. Paste into the new Actions class
4. Organize into logical regions:
   - Button Operations
   - Verification Methods
   - Data Entry Methods
   - Checkbox Operations
   - Helper Methods

### Step 3: Update Method Names

Add `Async` suffix to all public methods:
- `CreateCharKind()` ? `CreateCharKindAsync()`
- `VerifyPageHeader()` ? `VerifyPageHeaderAsync()`
- `EnterDetails()` ? `EnterDetailsAsync()`

### Step 4: Update Admin.cs

Add the new Actions class initialization:

```csharp
// In Admin.cs constructor:

// Action class instances - following POM pattern
private readonly [FeatureName]Actions [featureName]Actions;

public Admin(FeatureContext featureContext, ScenarioContext scenarioContext)
{
    // ...existing code...
    
    // Initialize Action classes - POM pattern
    [featureName]Actions = new [FeatureName]Actions(page, scenarioContext, featureContext);
}
```

### Step 5: Update Step File

Replace Page object usage with Actions:

**Before:**
```csharp
await adminStandardCharacteristics.CreateCharKind();
```

**After:**
```csharp
await adminStandardCharacteristicsActions.CreateCharKindAsync();
```

### Step 6: Delete Page Class

Once all step bindings are updated:
1. Verify build is successful
2. Delete the old Page class from `Modules/Items/Pages/`
3. Remove initialization from Admin.cs

### Step 7: Verify Build

```bash
dotnet build
```

---

## ?? Refactoring Checklist

For each Page class to refactor:

- [ ] Create new Actions class in `Modules/Items/Actions/`
- [ ] Copy all public methods from Page class
- [ ] Organize methods into logical regions
- [ ] Add `Async` suffix to method names
- [ ] Add XML documentation to public methods
- [ ] Add Actions class to `Admin.cs` constructor
- [ ] Update all step file usages
- [ ] Remove Page class initialization from `Admin.cs`
- [ ] Delete old Page class
- [ ] Run build and verify success
- [ ] Test affected scenarios

---

## ?? Example: AdminStandardCharacteristics Refactoring

### Current State:
```
Modules/Items/Pages/AdminStandardCharacteristics.cs
   ? (used by)
Modules/Items/Steps/Admin.Characteristics.Standard.cs
```

### Steps to Refactor:

#### 1. Create Actions Class:
```bash
File: Modules/Items/Actions/AdminStandardCharacteristicsActions.cs
```

#### 2. Method Migration:

**From Page Class:**
```csharp
public async Task CreateCharKind()
{
    await ClickOnNewCharKindButton();
  await EnterKindDetails();
    await ClicksOnCharStandardSaveButton();
}
```

**To Actions Class:**
```csharp
/// <summary>
/// Creates a new Characteristic Kind
/// </summary>
public async Task CreateCharKindAsync()
{
    try
    {
        await ClickOnNewCharKindButtonAsync();
 await EnterKindDetailsAsync();
      await ClickCharStandardSaveButtonAsync();
    }
    catch (Exception ex)
    {
        throw ExceptionHandler.BuildCustomException("create Characteristic Kind", ex, testCaseName);
    }
}
```

#### 3. Update Admin.cs:

```csharp
// Add field
private readonly AdminStandardCharacteristicsActions adminStandardCharacteristicsActions;

// Add initialization
adminStandardCharacteristicsActions = new AdminStandardCharacteristicsActions(page, scenarioContext, featureContext);
```

#### 4. Update Step File:

**In Admin.Characteristics.Standard.cs:**

```csharp
// Before:
[When(@"User creates a new Characteristic Kind")]
public async Task WhenUserCreatesCharacteristicKind()
{
    await adminStandardCharacteristics.CreateCharKind();
}

// After:
[When(@"User creates a new Characteristic Kind")]
public async Task WhenUserCreatesCharacteristicKind()
{
    await adminStandardCharacteristicsActions.CreateCharKindAsync();
}
```

#### 5. Delete Page Class:

Remove from `Admin.cs`:
```csharp
// Remove this line:
private readonly AdminStandardCharacteristics adminStandardCharacteristics;

// Remove this line:
adminStandardCharacteristics = new AdminStandardCharacteristics(page, scenarioContext, featureContext);
```

Delete file:
```bash
Modules/Items/Pages/AdminStandardCharacteristics.cs
```

---

## ?? Common Patterns to Look For

### Pattern 1: Static Methods
Some Page classes have static methods that return field mappings:

**Page Class:**
```csharp
public static Dictionary<string, (string locatorKey, string controlType)> GetCharTypeFieldMappingsAsync()
{
    // field mappings
}
```

**Actions Class:**
```csharp
// Convert to private instance method
private Dictionary<string, (string locatorKey, string controlType)> GetCharTypeFieldMappings()
{
    // field mappings
}
```

### Pattern 2: Nested Helper Methods
Keep helper methods as private:

```csharp
// Public method
public async Task ValidateBusinessPriorityAsync()
{
    await ReadCheckboxStatesAsync();
    await CalculateExpectedValueAsync();
await AssertValueAsync();
}

// Private helpers
private async Task ReadCheckboxStatesAsync() { }
private async Task CalculateExpectedValueAsync() { }
private async Task AssertValueAsync() { }
```

### Pattern 3: Constants
Move constants to the Actions class:

```csharp
private const string Standard = "AdminStandardCharacteristics";
private const string CommonActions = "AdminCommonActions";
```

---

## ?? Things to Watch Out For

### 1. Duplicate Method Names
If two methods have the same name but different signatures:
- Keep both with different parameter lists
- Add XML documentation to clarify differences

### 2. Shared Helper Methods
If methods are used across multiple Page classes:
- Consider adding to `AdminCommonActions.cs`
- Or create a new `AdminCommonActionsHelper.cs`

### 3. Complex Dependencies
If a method calls methods from other Page classes:
- Inject the required Actions class
- Use dependency injection pattern

### 4. Static Field Mappings
Convert static methods to instance methods when moving to Actions:
- Remove `static` keyword
- Keep as `private` if only used internally

---

## ?? Progress Tracking

| Page Class | Actions Class | Step File | Status |
|-----------|---------------|-----------|--------|
| AdminCategory | AdminCategoryActions | Admin.Classification.Category | ? Done |
| AdminClass | AdminClassActions | Admin.Classification.Class | ? Done |
| AdminStandardCharacteristics | AdminStandardCharacteristicsActions | Admin.Characteristics.Standard | ? Pending |
| AdminValue | AdminValueActions | Admin.Characteristics.Value | ? Pending |
| AdminUserManagement | AdminUserManagementActions | Admin.Characteristics.UserManagement | ? Pending |
| AdminClassificationTaxonomy | AdminClassificationTaxonomyActions | Admin.Classification.Taxonomy | ? Pending |

---

## ?? Tips for Success

### 1. Work One File at a Time
- Complete refactoring of one Page class fully before moving to next
- Build and test after each refactoring

### 2. Use Find & Replace
- Use IDE find/replace to update step file method calls
- Be careful with method names that are similar

### 3. Keep Regions Organized
Standard regions to use:
- `#region Button Operations`
- `#region Verification Methods`
- `#region Data Entry Methods`
- `#region Checkbox Operations`
- `#region Helper Methods`

### 4. Document As You Go
- Add XML documentation to each public method
- Explain parameters and return values
- Note any special behavior

### 5. Test Incrementally
- Build after each major change
- Run affected test scenarios
- Fix issues immediately

---

## ?? Example Commands

### Build Solution:
```bash
dotnet build
```

### Build Specific Project:
```bash
dotnet build PIMQAAutomation.csproj
```

### Check for Errors:
```bash
dotnet build --no-incremental
```

---

## ? FAQ

**Q: Should I keep the Page class until Actions is complete?**
A: Yes, keep both during transition. Delete Page class only after all step files are updated and build is successful.

**Q: What if a method is used in multiple step files?**
A: Create the Actions method once and call it from all step files that need it.

**Q: Should I refactor private helper methods?**
A: Yes, include all methods (public and private) when creating the Actions class.

**Q: What about method calls between Page classes?**
A: Convert to Actions class calls. If needed, inject the Actions class via constructor.

**Q: Do I need to update async naming in old code?**
A: Yes, add `Async` suffix to all async methods for consistency.

---

## ?? Success Criteria

For each refactoring to be considered complete:

- ? New Actions class created with all methods
- ? All methods have `Async` suffix
- ? All methods have XML documentation
- ? Methods organized into logical regions
- ? Admin.cs updated with Actions initialization
- ? All step file bindings updated
- ? Old Page class deleted
- ? Old Page class removed from Admin.cs
- ? Build successful
- ? No compilation errors or warnings
- ? Affected scenarios tested

---

## ?? Need Help?

If you encounter issues:
1. Check this guide for common patterns
2. Look at `AdminCategoryActions.cs` and `AdminClassActions.cs` as examples
3. Review `ADMIN_REFACTORING_PHASE1_COMPLETE.md` for details
4. Ask team for code review

---

**Happy Refactoring! ??**

Last Updated: 2024-12-21

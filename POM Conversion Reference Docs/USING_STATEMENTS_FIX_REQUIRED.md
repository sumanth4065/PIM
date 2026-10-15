# Moving Using Statements Outside Namespace - Action Required

## Date: 2024-12-21
## Status: ?? **IN PROGRESS - REQUIRES MANUAL INTERVENTION**

---

## ?? **Issue Summary**

Several Action class files in the `Modules/Items/Actions/` directory have using statements **inside** the namespace block instead of outside. This was caused by the `dotnet format` tool moving them incorrectly.

---

## ? **Files That Need Fixing**

Based on analysis, the following files have using statements inside the namespace:

1. **AdminCategoryActions.cs** - Has duplicate namespace + using statements inside
2. **AdminClassificationTaxonomyActions.cs** - Using statements inside namespace
3. **AdminCommonActions.cs** - Using statements inside namespace
4. **AdminNavigationActions.cs** - Using statements inside namespace
5. **AdminOemActions.cs** - Using statements inside namespace
6. **AdminStandardCharacteristicsActions.cs** - Using statements inside namespace
7. **AdminUserManagementActions.cs** - Using statements inside namespace
8. **ItemCharacteristicActions.cs** - Using statements inside namespace
9. **ItemExcelActions.cs** - Using statements inside namespace
10. **ItemIDHActions.cs** - Using statements inside namespace
11. **ItemMasterActions.cs** - Using statements inside namespace
12. **ItemPIMPoolActions.cs** - Using statements inside namespace
13. **ItemPIMServiceDefaultActions.cs** - Using statements inside namespace
14. **ItemTaxonomyActions.cs** - Using statements inside namespace

---

## ? **Files Already Correct**

The following files already have correct structure:

1. **AdminClassActions.cs** ?
2. **AdminValueActions.cs** ?
3. **ItemCommonActions.cs** ?
4. **ItemPIMDefaultActions.cs** ?
5. **AdminCommonActionsHelper.cs** ?

---

## ?? **How to Fix Manually**

For each file that needs fixing, follow this pattern:

### **Before (Incorrect):**
```csharp
namespace PIMQAAutomation.Modules.Items.Actions
{
    using Microsoft.Playwright;  // ? WRONG - Inside namespace
    using PIMQAAutomation.Utilities;
    using TechTalk.SpecFlow;

    public class SomeActions
    {
        // ...
    }
}
```

### **After (Correct):**
```csharp
using Microsoft.Playwright;  // ? CORRECT - Outside namespace
using PIMQAAutomation.Utilities;
using TechTalk.SpecFlow;

namespace PIMQAAutomation.Modules.Items.Actions
{
    public class SomeActions
    {
        // ...
    }
}
```

---

## ?? **Step-by-Step Fix Process**

### **For Each File:**

1. **Open the file** in Visual Studio or your editor
2. **Locate the namespace declaration** (should be near the top)
3. **Cut all using statements** from inside the namespace block
4. **Paste them BEFORE** the namespace declaration
5. **Remove any duplicate namespace declarations**
6. **Save the file**
7. **Verify** it compiles (Ctrl+Shift+B)

---

## ? **Quick Fix PowerShell Script**

You can use this PowerShell script to automatically fix all files:

```powershell
$actionFiles = Get-ChildItem -Path "Modules/Items/Actions/*.cs" -File

foreach ($file in $actionFiles) {
    $content = Get-Content $file.FullName -Raw
    
    # Check if using statements are inside namespace
    if ($content -match '(?s)namespace\s+[\w.]+\s*\{\s*((?:using\s+[^;]+;[\s\r\n]+)+)') {
        Write-Host "Fixing: $($file.Name)"
        
        $usings = $matches[1].Trim()
        
        # Remove using statements from inside namespace
        $content = $content -replace '(?s)(namespace\s+[\w.]+\s*\{)\s*(?:using\s+[^;]+;[\s\r\n]+)+', '$1'
        
        # Add using statements before namespace
        $content = "$usings`r`n`r`n$content"
        
        # Remove duplicate namespace declarations
        $content = $content -replace '(?s)namespace\s+[\w.]+\s*\{\s*namespace\s+([\w.]+)\s*\{', 'namespace $1 {'
        
        # Save the fixed content
        Set-Content -Path $file.FullName -Value $content -NoNewline
        
        Write-Host "? Fixed: $($file.Name)" -ForegroundColor Green
    } else {
        Write-Host "? Already correct: $($file.Name)" -ForegroundColor Cyan
    }
}

Write-Host "`r`nDone! Run 'dotnet build' to verify." -ForegroundColor Yellow
```

---

## ?? **Recommended Approach**

### **Option 1: Use the PowerShell Script (Fastest)**
1. Copy the PowerShell script above
2. Run it in the project root directory
3. Verify with `dotnet build`
4. Fix any remaining issues manually

### **Option 2: Manual Fix (Most Reliable)**
1. Open each file listed in "Files That Need Fixing"
2. Follow the "How to Fix Manually" pattern
3. Test each file individually
4. Run full build when done

### **Option 3: Use .editorconfig + dotnet format**
1. Create/update `.editorconfig` with:
   ```ini
   [*.cs]
   csharp_using_directive_placement = outside_namespace
   ```
2. Run: `dotnet format --include "Modules/Items/Actions/"`
3. Verify the changes
4. Test build

---

## ?? **Known Issues**

### **Issue 1: Duplicate Namespace**
**File:** AdminCategoryActions.cs  
**Problem:** Has duplicate `namespace PIMQAAutomation.Modules.Items.Actions` declarations  
**Fix:** Remove one of the namespace declarations

### **Issue 2: Method Signature Mismatch**
**File:** AdminClassificationTaxonomyActions.cs  
**Problem:** `SearchAndSelectLOVAsync` called with 2 arguments but method takes 3  
**Fix:** Check method signature and update call sites

---

## ? **Verification Checklist**

After fixing, verify:

- [ ] All using statements are **before** the namespace
- [ ] No duplicate namespace declarations
- [ ] No using statements **inside** the namespace
- [ ] File compiles without errors (`dotnet build`)
- [ ] No duplicate using statements
- [ ] Proper indentation (4 spaces)

---

## ?? **Current Status**

| Category | Count |
|----------|-------|
| **Files Needing Fix** | 14 |
| **Files Already Fixed** | 5 |
| **Total Action Files** | 19 |
| **Completion** | 26% |

---

## ?? **Critical Notes**

1. **Do NOT use `dotnet format` again** without proper `.editorconfig` settings
2. **Backup your changes** before running scripts
3. **Test incrementally** - fix a few files, build, repeat
4. **Watch for compilation errors** as you go

---

## ?? **Next Steps**

1. Choose your fixing approach (Script vs Manual vs .editorconfig)
2. Fix all 14 files that need correction
3. Run `dotnet build` to verify
4. Commit changes once build succeeds

---

**Status:** ?? **AWAITING FIX**  
**Priority:** ?? **HIGH**  
**Blocker:** Yes - Prevents compilation  
**Estimated Time:** 30-60 minutes

---

*Document Created: 2024-12-21*  
*Issue Source: dotnet format incorrectly moved using statements*  
*Solution: Move using statements back outside namespace*

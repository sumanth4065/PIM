using NUnit.Framework;
using PIMQAAutomation.Modules.Common.Constants;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace PIMQAAutomation.Utilities
{
    class FileUtil
    {
        public static string BuildTestDataPath(ScenarioContext scenarioContext)
        {
            // Get module and sub-module names
            string moduleName = Utility.GetModuleNameFromTags(scenarioContext);
            string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
            // Final full path
            return string.Concat($"{projectRoot}\\Modules\\",$"{moduleName}\\Resources\\TestData\\",$"{TestContext.Parameters[RunSettingsConstants.Environment]}\\TestData.json");
        }

        public static string BuildNavigationDataPath(ScenarioContext scenarioContext)
        {
            // Get module and sub-module names
            string moduleName = Utility.GetModuleNameFromTags(scenarioContext);
            string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
            // Final full path
            return string.Concat($"{projectRoot}\\Modules\\", $"{moduleName}\\Resources\\TestData\\", $"{TestContext.Parameters[RunSettingsConstants.Environment]}\\Navigation.json");
        }

        public static string BuildLocatorPath(ScenarioContext scenarioContext)
        {
            // Get module and sub-module names
            string moduleName = Utility.GetModuleNameFromTags(scenarioContext);
            string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
            // Final full path
            return string.Concat($"{projectRoot}\\Modules\\", $"{moduleName}\\Resources\\Locators\\", $"{TestContext.Parameters[RunSettingsConstants.Environment]}\\Locators.json");
        }

        public static string GetImportExcelFilePath(ScenarioContext scenarioContext, string actionName)
        {
            string moduleName = Utility.GetModuleNameFromTags(scenarioContext);
            string environment = TestContext.Parameters[RunSettingsConstants.Environment];
            string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
            string testCaseId = Utility.GetTestCaseIdByTitle(scenarioContext.ScenarioInfo.Title);
            string formattedAction = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(actionName.ToLower());
            string importFolder = Path.Combine(projectRoot, "Modules", moduleName, "Resources", "Import", environment);

            var matchedFile = Directory.GetFiles(importFolder, "*.xlsx", SearchOption.AllDirectories)
                                       .FirstOrDefault(file =>
                                           Path.GetFileName(file).StartsWith($"{testCaseId}-", StringComparison.OrdinalIgnoreCase) &&
                                           Path.GetFileName(file).EndsWith($"-{formattedAction}.xlsx", StringComparison.OrdinalIgnoreCase));

            if (matchedFile == null)
            {
                throw new FileNotFoundException($"Excel file not found for pattern '{testCaseId}-*-{formattedAction}.xlsx' in '{importFolder}' or subfolders.");
            }
            string fullPath = Path.GetFullPath(matchedFile);
            Console.WriteLine("Resolved Excel Path: " + fullPath);
            return fullPath;
        }
    }
}

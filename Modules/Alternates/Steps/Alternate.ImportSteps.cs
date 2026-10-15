using NUnit.Framework;
using OfficeOpenXml;
using PIMQAAutomation.Common.Pages;
using PIMQAAutomation.Modules.Alternates.Constants;
using PIMQAAutomation.Modules.Alternates.Pages;
using PIMQAAutomation.Utilities;
using TechTalk.SpecFlow;

namespace PIMQAAutomation.Modules.Alternates.Steps
{
    public partial class Alternate
    {
        [When(@"User recalls the Lookup Item from ""(.*)"" Excel for Alternate in the ""(.*)"" panel with row index (.*)")]
        public async Task ThenUserRecallOnlyLookupItemFromExcel(string excelFileName, string panelName, int rowIndex, Table inputParametersList)
        {
            await alternateImportActions.RecallLookupItemFromExcelAsync(excelFileName, panelName, rowIndex, inputParametersList);
        }

        [Then(@"Validate cluster item rows in ""(.*)"" panel from ""(.*)"" Excel with row index (.*)")]
        public async Task ThenValidateClusterItemRowFromExcelWithFields(string panelName, string excelFileName, int rowIndex, Table inputParametersList)
        {
            await alternateImportActions.ValidateClusterItemRowsFromExcelAsync(panelName, excelFileName, rowIndex, inputParametersList);
        }

        [Then(@"User Loads data from ""(.*)"" excel with row index (.*) as ""(.*)"" and ""(.*)""")]
        public async Task ThenUserLoadsExcelWithRowIndexAsItem1AndItem2(string excelFileName, int rowIndex, string item1Key, string item2Key)
        {
            await alternateImportActions.LoadExcelDataWithTwoItemsAsync(excelFileName, rowIndex, item1Key, item2Key);
        }

        [Then(@"User Loads ""(.*)"" excel with row index (.*) as ""(.*)""")]
        public async Task ThenUserLoadsExcelWithRowIndexAsItem(string excelFileName, int rowIndex, string item)
        {
            await alternateImportActions.LoadExcelDataWithSingleItemAsync(excelFileName, rowIndex, item);
        }
    }
}
namespace PIMQAAutomation.Modules.Items.Steps
{
    using Microsoft.Playwright;
    using NUnit.Framework;
    using OfficeOpenXml;
    using PIMQAAutomation.Common.Pages;
    using PIMQAAutomation.Modules.Bom.Constants;
    using PIMQAAutomation.Modules.Common.Constants;
    using PIMQAAutomation.Modules.Items.Actions;
    using PIMQAAutomation.Modules.Items.Constants;
    using PIMQAAutomation.Modules.Lob.Constants;
    using PIMQAAutomation.Utilities;
    using TechTalk.SpecFlow;

    public partial class Item
    {
        // No field declarations here - they're in Item.cs (main partial class file)
        [Then(@"Verify Item Master ""(.*)"" tab is displayed")]
        public async Task ThenVerifyItemMasterTabIsDisplayed(string tabName)
        {
            await itemMasterActions.VerifyTabIsDisplayedAsync(tabName);
        }

        [Then("Verify that the following radio button fields are displayed")]
        public async Task VerifyBelowRadioButtonFieldsAreDisplayed(Table table)
        {
            await itemMasterActions.VerifyRadioButtonFieldsAreDisplayedAsync(table);
        }

        [Then("User enters data for the following header fields")]
        public async Task ThenUserEntersDataForTheFollowingHeaderFields(Table table)
        {
            await itemMasterActions.EnterHeaderFieldsAsync(table);
        }

        [Then(@"Verify that the following fields count displays as ""(.*)""")]
        public async Task ThenVerifyBelowFieldsCountDisplaysAs(string count, Table table)
        {
            await itemMasterActions.VerifyFieldsCountAsync(count, table);
        }

        [Then(@"Verify that the following fields count displays as more ""(.*)""")]
        public async Task ThenVerifyBelowFieldsCountDisplaysAsMoreThan(int expectedCount, Table table)
        {
            await itemMasterActions.VerifyFieldsCountIsGreaterThanAsync(expectedCount, table);
        }

        [Then("Verify that the following text box fields are displayed")]
        public async Task ThenVerifyTextBoxFieldsAreDisplayed(Table textBoxFields)
        {
            await itemMasterActions.VerifyTextBoxFieldsAreDisplayedAsync(textBoxFields);
        }

        [Then(@"User enters text into the ""(.*)"" field")]
        public async Task ThenUserEntersTextIntoTheField(string textField)
        {
            await itemMasterActions.EnterTextIntoFieldAsync(textField);
        }

        [Then("Verify the below fields are not empty")]
        public async Task ThenVerifyTheBelowFieldsAreNotEmpty(Table table)
        {
            await itemMasterActions.VerifyFieldsAreNotEmptyAsync(table);
        }

        [Then(@"Verify count display as ""(.*)"" for below fields")]
        public async Task ThenVerifyCountDisplayAsForBelowFields(string count, Table table)
        {
            await itemMasterActions.VerifyFieldsCountAsync(count, table);
        }

        [Then("Verify below field values displayed for Item Id")]
        public async Task ThenVerifyBelowFieldValuesDisplayedForItemId(Table table)
        {
            await itemMasterActions.VerifyFieldValueDisplayedForItemIdAsync(table);
        }

        [Then("Verify text is displayed for below fields")]
        public async Task ThenVerifyTextIsDisplayedForBelowFields(Table table)
        {
            await itemMasterActions.VerifyTextIsDisplayedForFieldsAsync(table);
        }

        [Then("Verify radio button is displayed")]
        public async Task ThenVerifyRadioButtonIsDisplayed(Table table)
        {
            await itemMasterActions.VerifyRadioButtonIsDisplayedAsync(table);
        }

        [When(@"User clicks on ""(.*)"" button in Item Master")]
        public async Task WhenUserClicksOnButtonInItemMaser(string buttonName)
        {
            await itemMasterActions.ClickButtonAsync(buttonName);
        }

        [When(@"User clicks on the ""(.*)"" tab")]
        public async Task WhenUserClicksOnTheTab(string tabName)
        {
            await itemMasterActions.ClickTabAsync(tabName);
        }

        [Then(@"Verify ""(.*)"" checkbox is displayed")]
        public async Task ThenVerifyCheckboxIsDisplayed(string checkbox)
        {
            await itemMasterActions.VerifyCheckboxIsDisplayedAsync(checkbox);
        }

        [Then(@"Verify ""(.*)"" is displayed as ""(.*)""")]
        public async Task ThenVerifyStatusIsDisplayedAs(string fieldName, string status)
        {
            await itemMasterActions.VerifyFieldStatusAsync(fieldName, status);
        }

        [Then(@"Verify ""(.*)"" normally radio button is selected")]
        public async Task ThenVerifyNormallyRadioButtonIsSelected(string product)
        {
            await itemMasterActions.VerifyRadioButtonIsSelectedAsync(product);
        }

        [Then(@"Verify ""(.*)"" normally radio button is unselected")]
        public async Task ThenVerifyNormallyRadioButtonIsUnselected(string component)
        {
            await itemMasterActions.VerifyRadioButtonIsUnselectedAsync(component);
        }

        [Then("Verify that the following checkboxes are unchecked")]
        public async Task ThenVerifyTheBelowCheckboxesAreUnchecked(Table fieldNames)
        {
            await itemMasterActions.VerifyCheckboxesAreUncheckedAsync(fieldNames);
        }

        [Then("Verify the below fields are empty")]
        public async Task ThenVerifyTheBelowFieldsAreEmpty(Table fieldNames)
        {
            await itemMasterActions.VerifyFieldsAreEmptyAsync(fieldNames);
        }

        [Then("Verify the below field values are displayed")]
        public async Task ThenVerifyTheBelowFieldValuesAreDisplayed(Table fieldNames)
        {
            await itemMasterActions.VerifyFieldValuesAreDisplayedAsync(fieldNames);
        }

        [Then(@"User selects ""(.*)"" from the ""(.*)"" dropdown")]
        public async Task ThenUserSelectsFromTheDropdown(string active, string status)
        {
            await itemMasterActions.SelectFromDropdownAsync(active, status);
        }

        [Then("User enters data for the following product fields")]
        public async Task ThenUserEntersDataForBelowFields(Table fieldNames)
        {
            await itemMasterActions.EnterProductFieldsAsync(fieldNames);
        }

        [Then(@"User recalls the created ""(.*)"" Id")]
        public async Task ThenUserRecallsTheCreatedItemId(string itemId)
        {
            await itemMasterActions.RecallCreatedItemIdAsync(itemId);
        }

        [When(@"User clicks on ""(.*)"" button in Taxonomy section")]
        public async Task WhenUserClicksOnButtonInTaxonomySection(string button)
        {
            await itemMasterActions.ClickTaxonomyButtonAsync(button);
        }

        [Then(@"Verify ""(.*)"" and ""(.*)"" buttons are enabled")]
        public async Task ThenVerifyAndButtonsAreEnabled(string button1, string button2)
        {
            await itemMasterActions.VerifyButtonsAreEnabledAsync(button1, button2);
        }

        [Then(@"Verify ""(.*)"" and ""(.*)"" buttons are disabled")]
        public async Task ThenVerifyAndButtonsAreDisabled(string button1, string button2)
        {
            await itemMasterActions.VerifyButtonsAreDisabledAsync(button1, button2);
        }

        [Then(@"Verify ""(.*)"" Taxonomy is displayed")]
        public async Task ThenVerifyTaxonomyIsDisplayed(string taxonomyOption)
        {
            await itemTaxonomyActions.VerifyTaxonomyIsDisplayedAsync(taxonomyOption);
        }

        [Then(@"User enters ""(.*)"" Taxonomy details")]
        public async Task ThenUserEntersTaxonomyDetails(string taxonomyOption, Table fieldNames)
        {
            await itemTaxonomyActions.EnterTaxonomyDetailsAsync(taxonomyOption, fieldNames);
        }

        [When(@"User clicks on ""(.*)"" Taxonomy")]
        public async Task WhenUserClicksOnTaxonomy(string taxonomyOption)
        {
            await itemTaxonomyActions.ClickTaxonomyAsync(taxonomyOption);
        }

        [Then(@"User enters ""(.*)"" Taxonomy details and clicks on ""(.*)"" button if empty")]
        public async Task ThenUserEntersTaxonomyDetailsIfEmpty(string taxonomyType, string button, Table fieldsToCheck)
        {
            await itemTaxonomyActions.EnterTaxonomyDetailsIfEmptyAsync(taxonomyType, button, fieldsToCheck);
        }

        [Then(@"User checks ""(.*)"" deff checkbox")]
        public async Task ThenUserChecksDeffCheckbox(string isLobDeffCheckbox)
        {
            await itemMasterActions.CheckDeffCheckboxAsync(isLobDeffCheckbox);
        }

        [Then(@"User recalls ""(.*)"" from baseline screen")]
        public async Task ThenUserRecallsFromBaselineScreen(string fieldname)
        {
            await itemMasterActions.RecallFromBaselineScreenAsync(fieldname);
        }

        [Then(@"User gets ""(.*)"" value")]
        public async Task ThenUserGetValue(string filedValue)
        {
            await itemMasterActions.GetFieldValueAsync(filedValue);
        }

        [Then(@"User recalls LOB in ""(.*)"" field under Explicit tab")]
        public async Task ThenUserRecallsLOBInFieldUnderExplicitTab(string itemId)
        {
            await itemMasterActions.RecallLOBInExplicitTabAsync(itemId);
        }

        [Then(@"User recalls the following fields from ""(.*)"" Excel")]
        public async Task ThenUserRecallBelowFieldsFromExcel(string fileName, Table table)
        {
            await itemExcelActions.RecallFieldsFromExcelAsync(fileName, table);
        }

        [Then(@"User gets the value of ""(.*)"" and ""(.*)"" and ""(.*)""")]
        public async Task ThenUserGetsTheValueOfAnd(string lob, string oem, string description)
        {
            await itemMasterActions.GetMultipleFieldValuesAsync(lob, oem, description);
        }

        [Then(@"User recalls the following taxonomy fields from ""(.*)"" Excel")]
        public async Task ThenUserRecallTheFollowingTaxonomyFieldsFromExcel(string fileName, Table table)
        {
            await itemExcelActions.RecallTaxonomyFieldsFromExcelAsync(fileName, table);
        }

        [Then(@"User recalls ""(.*)"" id from ""(.*)"" Excel")]
        public async Task ThenUserRecallsItemIdFromExcel(string fieldName, string fileName)
        {
            await itemExcelActions.RecallItemIdFromExcelAsync(fieldName, fileName);
        }

        [Then("User clicks on OK popup button")]
        public async Task ThenUserClicksOnOKPopupButton()
        {
            await itemMasterActions.HandleOKPopupAsync();
        }

        [Then(@"User recalls the following fields from ""(.*)"" Excel for Explicit Item")]
        public async Task ThenUserRecallBelowFieldsFromExcelForExplicitItem(string excelFileName, Table table)
        {
            await itemExcelActions.RecallFieldsFromExcelForExplicitItemAsync(excelFileName, table);
        }

        [Then(@"User sets ""(.*)"" deff checkbox to ""(.*)""")]
        public async Task ThenUserSetsDeffCheckboxTo(string checkboxName, string shouldBeChecked)
        {
            await itemMasterActions.SetCheckboxStateAsync(checkboxName, shouldBeChecked);
        }

        [Then("User selects below explicit tab checkboxes")]
        public async Task ThenUserSelectsBelowExplicitTabCheckBoxes(Table fieldsToCheck)
        {
            await itemMasterActions.SelectExplicitTabCheckboxesAsync(fieldsToCheck);
        }

        [Then(@"User recalls the ""(.*)"" Id from BOM")]
        public async Task ThenUserRecallsTheCreatedItemIdFromBOM(string itemId)
        {
            await itemMasterActions.RecallItemIdFromBOMAsync(itemId);
        }

        [Then("User enters data into below product fields")]
        public async Task ThenUserEntersDataIntoBelowProductFields(Table fieldNames)
        {
            await itemMasterActions.EnterProductFieldsWithoutDropdownAsync(fieldNames);
        }

        [Then("verify no data found")]
        public async Task ThenVerifyNoDataFound()
        {
            await itemMasterActions.VerifyNoDataFoundAsync();
        }

        [Then(@"User recalls ""(.*)"" from the admin page which has ""(.*)"" status")]
        public async Task ThenUserRecallsFromTheAdminOemPageAndVerifies(string oemDropdown, string expectedStatus)
        {
            await itemMasterActions.RecallAndVerifyOemAsync(oemDropdown, expectedStatus);
        }

        [When(@"User enters details in ""(.*)"" Taxonomy")]
        public async Task WhenUserEntersTaxonomyDetails(string taxonomyOption, Table fieldNames)
        {
            await itemTaxonomyActions.EnterDetailsInTaxonomyAsync(taxonomyOption, fieldNames);
        }

        [Then(@"User searches and selects ""(.*)"" in Product Taxonomy")]
        public async Task ThenUserSearchesAndSelectsInProductTaxonomy(string fieldName)
        {
            await itemTaxonomyActions.SearchAndSelectInProductTaxonomyAsync(fieldName);
        }

        [Then("Validate that the checkbox states on the UI match the corresponding values from the imported Excel file")]
        public async Task ThenValidateThatTheCheckboxStatesOnTheUIMatchTheCorrespondingValuesFromTheImportedExcelFile(Table table)
        {
            await itemMasterActions.ValidateCheckboxStatesFromExcelAsync(table);
        }

        [Then(@"User Selects ""(.*)"" radio button")]
        public async Task ThenUserSelectsRadioButton(string fieldName)
        {
            await itemMasterActions.SelectRadioButtonAsync(fieldName);
        }

        [Then(@"User retrieves the following field values from the ""(.*)"" Excel file")]
        public async Task ThenUserRetrievesTheFollowingFieldValuesFromTheCreateItemExcelFile(string fileName, Table table)
        {
            await itemExcelActions.RetrieveFieldValuesFromExcelAsync(fileName, table);
        }

        [When(@"User enters below data in ""(.*)"" Taxonomy")]
        public async Task WhenUserEntersBelowDataInTaxonomy(string taxonomyOption, Table fieldNames)
        {
            await itemTaxonomyActions.EnterDataInTaxonomyAsync(taxonomyOption, fieldNames);
        }
    }
}

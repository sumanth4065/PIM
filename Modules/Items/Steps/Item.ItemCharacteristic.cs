using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using OfficeOpenXml;
using PIMQAAutomation.Common.Pages;
using PIMQAAutomation.Helpers;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Modules.Items.Actions;
using PIMQAAutomation.Modules.Items.Constants;
using PIMQAAutomation.Utilities;
using PPT.ItemMaster.Data.Cosmos.Features.ItemPools;
using PPT.ItemMaster.Domain.Features.ItemPools;
using TechTalk.SpecFlow;
using Utilities;
namespace PIMQAAutomation.Modules.Items.Steps
{
    

    public partial class Item
    {
        // No field declarations here - they're in Item.cs (main partial class file)
        [Then("Verify that the following characteristic display boxes are displayed")]
        public async Task ThenVerifyThatTheFollowingCharacteristicDisplayBoxesAreDisplayed(Table panelNames)
        {
            await itemCharacteristicActions.VerifyCharacteristicDisplayBoxesAsync(panelNames);
        }

        [Then(@"Verify row ""(.*)"" is displayed in characteristic ""(.*)"" section")]
        public async Task ThenVerifyRowIsDisplayedInCharacteristicSection(int rowNumber, string kindOrtype)
        {
            await itemCharacteristicActions.VerifyRowIsDisplayedInCharacteristicSectionAsync(rowNumber, kindOrtype);
        }

        [Then(@"User selects characteristic Kind as ""(.*)"" and Type as ""(.*)""")]
        public async Task ThenUserSelectsCharacteristicKindAsAndTypeAs(string kindName, string typeName)
        {
            await itemCharacteristicActions.SelectCharacteristicKindAndTypeAsync(kindName, typeName);
        }

        [Then("Verify below buttons are enabled in Characteristic display Panel")]
        public async Task ThenVerifyBelowButtonsAreEnabledInCharacteristicDisplayPanel(Table buttonNames)
        {
            await itemCharacteristicActions.VerifyButtonsAreEnabledAsync(buttonNames);
        }

        [When(@"User clicks on ""(.*)"" button in Characteristic display Panel")]
        public async Task WhenUserClicksOnButtonInCharacteristicDisplayPanel(string buttonName)
        {
            await itemCharacteristicActions.ClickButtonInCharacteristicPanelAsync(buttonName);
        }

        [Then("Verify below characteristic field values are auto-populated and not editable")]
        public async Task ThenVerifyBelowCharacteristicFieldValuesAreAuto_PopulatedAndNotEditable(Table fieldsToCheck)
        {
            await itemCharacteristicActions.VerifyFieldsAreAutoPopulatedAndNotEditableAsync(fieldsToCheck);
        }

        [Then(@"User enters data into the characteristic ""(.*)"" field")]
        public async Task ThenUserEntersDataIntoTheCharacteristicField(string value)
        {
            await itemCharacteristicActions.EnterDataIntoCharacteristicFieldAsync(value);
        }

        [Then(@"Verify below buttons are disabled in Characteristic display Panel")]
        public async Task ThenVerifyBelowButtonsAreDisabledInCharacteristicDisplayPanel(Table buttonNames)
        {
            await itemCharacteristicActions.VerifyButtonsAreDisabledAsync(buttonNames);
        }

        [Then("Verify below fields are not editable")]
        public async Task ThenVerifyBelowFieldsAreNotEditable(Table fieldsToCheck)
        {
            await itemCharacteristicActions.VerifyFieldsAreNotEditableAsync(fieldsToCheck);
        }

        [Then("Verify below fields are editable")]
        public async Task ThenVerifyBelowFieldsAreEditable(Table fieldsToCheck)
        {
            await itemCharacteristicActions.VerifyFieldsAreEditableAsync(fieldsToCheck);
        }

        [Then("Verify taxonomy characteristic kind fields are not empty")]
        public async Task ThenVerifyTaxonomyCharacteristicKindFieldsAreNotEmpty(Table fieldNames)
        {
            await itemCharacteristicActions.VerifyTaxonomyCharacteristicKindFieldsAreNotEmptyAsync(fieldNames);
        }

        [Then(@"User searches and selects characteristic ""(.*)"" from the dropdown")]
        public async Task ThenUserSearchAndSelectsCharacteristicFromTheDropdown(string characteristicType)
        {
            await itemCharacteristicActions.SearchAndSelectCharacteristicAsync(characteristicType);
        }

        [Then(@"User enters text into the ""(.*)"" field in Characteristic display Panel")]
        public async Task ThenUserEntersTextIntoTheFieldInCharacteristicDisplayPanel(string internalNotes)
        {
            await itemCharacteristicActions.EnterTextIntoFieldInCharacteristicPanelAsync(internalNotes);
        }

        [Then(@"Verify ""(.*)"" checkbox is not editable")]
        public async Task ThenVerifyCheckboxIsNotEditable(string checkbox)
        {
            await itemCharacteristicActions.VerifyCheckboxIsNotEditableAsync(checkbox);
        }

        [Then("User clicks the below characteristic checkboxes")]
        public async Task ThenUserClicksTheBelowCharacteristicCheckboxes(Table fieldsToCheck)
        {
            await itemCharacteristicActions.ClickCharacteristicCheckboxesAsync(fieldsToCheck);
        }

        [Then("Verify characteristic type is editable")]
        public async Task ThenVerifyCharacteristicTypeIsEditable(Table fieldsToCheck)
        {
            await itemCharacteristicActions.VerifyCharacteristicTypeIsEditableAsync(fieldsToCheck);
        }

        [Then("Verify below checkbox fields are not editable")]
        public async Task ThenVerifyBelowCheckboxFieldsAreNotEditable(Table fieldsToCheck)
        {
            await itemCharacteristicActions.VerifyCheckboxFieldsAreNotEditableAsync(fieldsToCheck);
        }

        [Then("Verify below checkbox fields are editable and edit the flag")]
        public async Task ThenVerifyBelowCheckboxFieldsAreEditableAndEditTheFlag(Table fieldsToCheck)
        {
            await itemCharacteristicActions.VerifyCheckboxFieldsAreEditableAndEditFlagAsync(fieldsToCheck);
        }

        [Then(@"User enters data into the characteristic ""(.*)"" textarea field")]
        public async Task ThenUserEntersDataIntoTheCharacteristicTextAreaField(string fieldName)
        {
            await itemCharacteristicActions.EnterDataIntoCharacteristicTextAreaFieldAsync(fieldName);
        }

        [Then("Verify created and modified username and date in Characteristic display Panel")]
        public async Task ThenVerifyCreatedAndModifiedUsernameAndDateInCharacteristicDisplayPanel(Table dateFields)
        {
            await itemCharacteristicActions.VerifyCreatedAndModifiedFieldsAsync(dateFields);
        }

        [Then(@"User selects characteristic kind and type from ""(.*)"" excel and verifies new characteristic ""(.*)"" is displayed in ""(.*)"" tab")]
        public async Task ThenUserSelectsCharacteristicKindAndTypeFromExcelAndVerifiesNewCharacteristicIsDisplayedInTab(string fileName, string fieldName, string tabName)
        {
            await itemCharacteristicActions.SelectCharacteristicKindAndTypeFromExcelAsync(fileName, fieldName, tabName);
        }

        [Then("Verify Explicit Values fields are enabled")]
        public async Task ThenVerifyExplicitValuesFieldsAreEnabled(Table characteristicButton)
        {
            await itemCharacteristicActions.VerifyExplicitValuesFieldsAreEnabledAsync(characteristicButton);
        }

        [Then(@"Verify ""(.*)"" new characteristic ""(.*)"" is displayed")]
        public async Task ThenVerifyNewCharacteristicIsDisplayed(string tabName, string valueField)
        {
            await itemCharacteristicActions.VerifyNewCharacteristicIsDisplayedAsync(tabName, valueField);
        }

        [Then(@"User searches and selects ""(.*)"" from dropdown")]
        public async Task ThenUserSerachesAndSelectsFromDropdown(string fieldName)
        {
            await itemCharacteristicActions.SearchAndSelectFromDropdownAsync(fieldName);
        }

        [Then(@"User removes existing ""(.*)""")]
        public async Task ThenUserRemovesExistingId(string button)
        {
            await itemCharacteristicActions.RemoveExistingIdAsync(button);
        }

        [Then("Verify the below characteristic checkboxes are checked")]
        public async Task ThenVerifyTheBelowCharacteristicCheckboxesAreChecked(Table fieldsToCheck)
        {
            await itemCharacteristicActions.VerifyCharacteristicCheckboxesAreCheckedAsync(fieldsToCheck);
        }

        [Then("Verify the below characteristic checkboxes are unchecked")]
        public async Task ThenVerifyTheBelowCharacteristicCheckboxesAreUnChecked(Table fieldsToCheck)
        {
            await itemCharacteristicActions.VerifyCharacteristicCheckboxesAreUncheckedAsync(fieldsToCheck);
        }

        [Then("Verify below checkboxes are enabled in Characteristic display Panel")]
        public async Task ThenVerifyBelowCheckboxesAreEnabledInCharacteristicDisplayPanel(Table fieldsToCheck)
        {
            await itemCharacteristicActions.VerifyCheckboxesAreEnabledAsync(fieldsToCheck);
        }

        [Then("Verify below checkboxes are disabled in Characteristic display Panel")]
        public async Task ThenVerifyBelowCheckboxesAreDisabledInCharacteristicDisplayPanel(Table fieldsToCheck)
        {
            await itemCharacteristicActions.VerifyCheckboxesAreDisabledAsync(fieldsToCheck);
        }

        [Then(@"User verifies that the grid displays the added characteristic with From Level ""(.*)""")]
        public async Task ThenUserVerifiesThatTheGridDisplaysTheAddedCharacteristicWithFromLevel(string levelValue)
        {
            await itemCharacteristicActions.VerifyAddedCharacteristicWithFromLevelAsync(levelValue);
        }

        [Then(@"User clicks the enabled button from ""(.*)"" or ""(.*)"" button")]
        public async Task ThenUserClicksTheEnabledButtonFromOrButton(string addlevelbutton, string editbutton)
        {
            await itemCharacteristicActions.ClickEnabledButtonAsync(addlevelbutton, editbutton);
        }

        [When("User selects the created Characteristic Kind")]
        public async Task WhenUserSelectsTheCreatedCharacteristicKind()
        {
            await itemCharacteristicActions.SelectCreatedCharacteristicKindAsync();
        }

        [When(@"User selects the Characteristic Type option from Characteristic Type drop down")]
        public async Task WhenUserSelectsTheCharacteristicTypeOptionFromCharacteristicTypeDropDown()
        {
            await itemCharacteristicActions.SelectCharacteristicTypeFromDropdownAsync();
        }

        [Then(@"User enters data into the characteristic value field")]
        public async Task ThenUserEntersDataIntoTheCharacteristicField()
        {
            await itemCharacteristicActions.EnterDataIntoCharacteristicValueFieldAsync();
        }

        [Then(@"User calculates P&L values based on the below values")]
        public void ThenUserCalculatesPLValuesBasedOnTheBelowValues(Table table)
        {
            itemCharacteristicActions.CalculatePLLaborValuesAsync(table);
        }

        [Then(@"User verifies P&L and Labor values with calculated values")]
        public async Task ThenUserVerifiesPLandLaborValuesWithCalculatedValues()
        {
            await itemCharacteristicActions.VerifyPLLaborValuesWithCalculatedValuesAsync();
        }

        [Then(@"Verify P&L and Labor values in ItemPools document in Pim database")]
        public async Task ThenVerifyPLLaborValuesInItemPool()
        {
            await itemCharacteristicActions.VerifyPLLaborValuesInItemPoolAsync();
        }

        [Then(@"User gets the value of the ""(.*)"" characteristic")]
        public async Task ThenUserGetsTheValueOfTheCharacteristic(string characteristicName)
        {
            await itemCharacteristicActions.GetCharacteristicValueAsync(characteristicName);
        }

        [Then("User validates the Charcateristic Kind is not visible in Characteristic Kind list")]
        public async Task ThenUserValidatesTheCharcateristicKindIsNotVisibleInCharacteristicKindList()
        {
            await itemCharacteristicActions.ValidateCharacteristicKindIsNotVisibleAsync();
        }

        [Then("User validates the Error Message")]
        public async Task ThenUserValidatesTheErrorMessage()
        {
            await itemCharacteristicActions.ValidateErrorMessageAsync();
        }

        [Then(@"Verify Characteristic value in SkuAttributes in ItemPools document in Pim database")]
        public async Task ThenVerifySkuAttributesValuesInItemPool(Table characteristicValues)
        {
            await itemCharacteristicActions.VerifySkuAttributesInItemPoolAsync(characteristicValues);
        }

        [Then(@"User sets ""(.*)"" checkbox in Item Master to ""(.*)""")]
        public async Task ThenUserSetsCheckboxInItemMasterTo(string checkboxName, bool shouldBeChecked)
        {
            await itemCharacteristicActions.SetCheckboxStateAsync(checkboxName, shouldBeChecked);
        }
    }
}

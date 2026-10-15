using Microsoft.Playwright;

namespace PIMQAAutomation.Modules.Items.Pages
{
    public class ItemMasterPage
    {
        private const string ButtonName = "//button[@id='item-[TEXT]-button']";
        private const string CharacteristicButton = "//button[@id='item-characteristics-display-[TEXT]-button']";
        private const string CharacteristicCheckbox = "//input[@id='item-characteristics-display-[TEXT]-checkbox']";
        private const string CharacteristicDisplayPanel = "//div[@id='item-[TEXT]-card']";
        private const string CharacteristicDisplayAndLevelValue = "//tr[td/div[contains(text(),'[TEXT1]')] and td/span[contains(text(),'[TEXT2]')] and td[contains(text(),'[TEXT3]')]]";
        private const string CharacteristicField = "//input[@id='item-characteristics-display-[TEXT]-input'or @id='item-characteristics-display-[TEXT]-autocomplete-input']";
        private const string CharacteristicKind = "//div[contains(@id, 'item-display-grid-kind-item') and normalize-space(text())='[TEXT]']";
        private const string CharacteristicKindOrType = "//div[normalize-space(text())='[TEXT]']";
        private const string CharacteristicPanelDateFields = "//span[@id='item-characteristics-display-[TEXT]']";
        private const string CharacteristicTextAreaField = "//textarea[@id='item-characteristics-display-[TEXT]-textarea']";
        private const string CharacteristicType = "//div[normalize-space()='[TEXT]']";
        private const string Checkbox = "//input[@id='item-right-panel-[TEXT]-checkbox']";
        private const string CountBox = "//span[contains(@id,'item-[TEXT]')]";
        private const string CrossButton = "#item-[TEXT]-autocomplete-input_clearBtn";
        private const string DefaultCharacteristics = "#item-default-characteristics-[TEXT]-header";
        private const string DefaultCharacteristicsFields = "//*[@id='item-default-characteristics-[TEXT1]-[TEXT2]']";
        private const string FirstCharacteristicKind = "#item-display-grid-kind-item-0";
        private const string FirstLineCharacteristicKindOrType = "//*[@id='item-display-grid-[TEXT1]-row-[TEXT2]' or @id='item-display-grid-[TEXT1]-item-[TEXT2]']";
        private const string InputDropdownField = "//input[@id='item-[TEXT]-autocomplete-input' or @id='item-[TEXT]-input']";
        private const string InputField = "//textarea[@id='item-[TEXT]-textarea']";
        private const string InputTab = "//li[@id='item-[TEXT]-tab']";
        private const string InternalExternalNotesTab = "#item-[TEXT]-tab";
        private const string ItemCreationDateFields = "//span[@id='item-[TEXT]']";
        private const string ProductTaxonomyField = "#item-taxonomy-product-[TEXT]-autocomplete-input";
        private const string RadioButton = "//input[@id='item-right-panel-[TEXT]-radio']";
        private const string RightCornerInputField = "//input[@id='item-right-panel-[TEXT]-autocomplete-input']";
        private const string SaveButtonName = "//button[@id='item-taxonomy-[TEXT1]-[TEXT2]-button']";
        private const string Tab = "//li[@id='item-[TEXT]-tab' or @id='item-[TEXT]-[TEXT]-tab']/a";
        private const string TaxonomyButtonName = "//button[@id='item-taxonomy-[TEXT]-button']";
        private const string TaxonomyCharacteristicKindPanel = "//div[@id='item-taxonomy-card-header']";
        private const string TaxonomyDefaultValuesLevelManagementPanel = "//div[@id='item-characteristics-display-card-header']";
        private const string TaxonomyField = "(//input[@id='item-taxonomy-[TEXT1]-[TEXT2]-autocomplete-input' or @id='item-taxonomy-[TEXT1]-[TEXT2]-input'])[1]";
        private const string TaxonomyFields = "#item-product-[TEXT]-autocomplete-input , #item-product-[TEXT]-input";
        private const string TaxonomyOption = "//div[@id='item-taxonomy-[TEXT]-option' or normalize-space(text())='[TEXT]']";
        private const string TextBox = "//input[@id='item-right-panel-[TEXT]-checkbox']";
        private const string TypeExplicitValueField = "//input[@id='item-characteristics-display-[TEXT]-checkbox']";
        private const string ExplicitCheckBox = "#item-right-panel-[TEXT]-checkbox";
        private const string CharacteristicTypeDropDown = "#item-characteristics-display-type-autocomplete-input";
        private const string CharacteristicTypeOption = "(//span[normalize-space()='[TEXT]'])[last()]";
        private const string CharacteristicValueDropDown = "#item-characteristics-display-value-autocomplete-input";
        private const string NoDataFound = "//mat-option[@id='no-data-found']";

        private const string CharacteristicValue = "(//td/div[contains(text(),'[TEXT]')])/parent::td/following-sibling::td/span";
        private const string ToastMessage = "//span[normalize-space()='[TEXT]']";

        private const string CharErrorTitle = "#modal-title";
        private const string CharErrorMessage = "#modal-body";
        private const string OkMessage = "#modal-ok-button";
        private readonly IPage page;

        public ItemMasterPage(IPage page) => this.page = page;

        public ILocator GetButtonName(string button) => page.Locator(ButtonName.Replace("[TEXT]", button));

        public ILocator GetCharacteristicButton(string buttonName) => page.Locator(CharacteristicButton.Replace("[TEXT]", buttonName));

        public ILocator GetCharacteristicCheckbox(string checkbox) => page.Locator(CharacteristicCheckbox.Replace("[TEXT]", checkbox));

        public ILocator GetCharacteristicDisplayPanel(string panelName) => page.Locator(CharacteristicDisplayPanel.Replace("[TEXT]", panelName));

        public ILocator GetCharacteristicDisplayAndLevelValue(string characteristicType, string characteristicValue, string levelValue) => page.Locator(CharacteristicDisplayAndLevelValue.Replace("[TEXT1]", characteristicType).Replace("[TEXT2]", characteristicValue).Replace("[TEXT3]", levelValue));

        public ILocator GetCharacteristicField(string fieldName) => page.Locator(CharacteristicField.Replace("[TEXT]", fieldName));

        public ILocator GetCharacteristicKind(string kind) => page.Locator(CharacteristicKind.Replace("[TEXT]", kind));

        public ILocator GetCharacteristicKindOrType(string kindOrType) => page.Locator(CharacteristicKindOrType.Replace("[TEXT]", kindOrType));

        public ILocator GetCharacteristicPanelDateFields(string characteristicDateField) => page.Locator(CharacteristicPanelDateFields.Replace("[TEXT]", characteristicDateField));

        public ILocator GetCharacteristicTextAreaField(string fieldName) => page.Locator(CharacteristicTextAreaField.Replace("[TEXT]", fieldName));

        public ILocator GetCharacteristicType(string type) => page.Locator(CharacteristicType.Replace("[TEXT]", type));

        public ILocator GetCheckbox(string checkbox) => page.Locator(Checkbox.Replace("[TEXT]", checkbox.ToLower()));

        public ILocator GetCountBox(string fieldName) => page.Locator(CountBox.Replace("[TEXT]", fieldName));

        public ILocator GetCrossButton(string buttonName) => page.Locator(CrossButton.Replace("[TEXT]", buttonName.ToLower()));

        public ILocator GetDefaultCharacteristics(string fieldName) => page.Locator(DefaultCharacteristics.Replace("[TEXT]", fieldName));

        public ILocator GetDefaultCharacteristicsFields(string tabName, string fieldValue) => page.Locator(DefaultCharacteristicsFields.Replace("[TEXT1]", tabName).Replace("[TEXT2]", fieldValue));

        public ILocator GetDropdownInputField(string dropdownInputField) => page.Locator(InputDropdownField.Replace("[TEXT]", dropdownInputField.ToLower()));

        public ILocator GetFirstCharacteristicKind() => page.Locator(FirstCharacteristicKind);

        public ILocator GetFirstLineCharacteristicKindOrType(string kindOrType, int rowNumber) => page.Locator(FirstLineCharacteristicKindOrType.Replace("[TEXT1]", kindOrType).Replace("[TEXT2]", rowNumber.ToString()));

        public ILocator GetInputField(string fieldName) => page.Locator(InputField.Replace("[TEXT]", fieldName.ToLower()));

        public ILocator GetInputTab(string inputTab) => page.Locator(InputTab.Replace("[TEXT]", inputTab));

        public ILocator GetInternalExternalNotesTab(string tabName) => page.Locator(InternalExternalNotesTab.Replace("[TEXT]", tabName.ToLower()));

        public ILocator GetItemCreationDateFields(string creationDateUser) => page.Locator(ItemCreationDateFields.Replace("[TEXT]", creationDateUser));

        public ILocator GetProductTaxonomyFields(string field) => page.Locator(ProductTaxonomyField.Replace("[TEXT]", field));

        public ILocator GetRadioButton(string radioButton) => page.Locator(RadioButton.Replace("[TEXT]", radioButton));

        public ILocator GetRightCornerInputField(string fieldName) => page.Locator(RightCornerInputField.Replace("[TEXT]", fieldName));

        public ILocator GetSaveButton(string button, string taxonomy) => page.Locator(SaveButtonName.Replace("[TEXT1]", button).Replace("[TEXT2]", taxonomy));

        public ILocator GetTab(string tabName) => page.Locator(Tab.Replace("[TEXT]", tabName));

        public ILocator GetTaxonomyButtonName(string button) => page.Locator(TaxonomyButtonName.Replace("[TEXT]", button));

        public ILocator GetTaxonomyCharacteristicKindPanel() => page.Locator(TaxonomyCharacteristicKindPanel);

        public ILocator GetTaxonomyDefaultValuesLevelManagementPanel() => page.Locator(TaxonomyDefaultValuesLevelManagementPanel);

        public ILocator GetTaxonomyField(string taxonomyOption, string taxonomyField) => page.Locator(TaxonomyField.Replace("[TEXT1]", taxonomyOption).Replace("[TEXT2]", taxonomyField));

        public ILocator GetTaxonomyFields(string fieldName) => page.Locator(TaxonomyFields.Replace("[TEXT]", fieldName));

        public ILocator GetTaxonomyOption(string taxonomyOption) => page.Locator(TaxonomyOption.Replace("[TEXT]", taxonomyOption));

        public ILocator GetTextBox(string textBox) => page.Locator(TextBox.Replace("[TEXT]", textBox));

        public ILocator GetTypeExplicitValueField(string fieldName) => page.Locator(TypeExplicitValueField.Replace("[TEXT]", fieldName));

        public ILocator GetExplicitCheckBox(string checkBoxName) => page.Locator(ExplicitCheckBox.Replace("[TEXT]", checkBoxName.ToLower()));

        public ILocator GetCharacteristicTypeDropDown() => page.Locator(CharacteristicTypeDropDown);

        public ILocator GetCharacteristicTypeOption(string option) => page.Locator(CharacteristicTypeOption.Replace("[TEXT]", option));
        public ILocator GetCharacteristicValueDropDown() => page.Locator(CharacteristicValueDropDown);

        public ILocator GetCharacteristicValue(string characteristicName) => page.Locator(CharacteristicValue.Replace("[TEXT]", characteristicName));
        public ILocator GetToastMessage(string toastMessage) => page.Locator(ToastMessage.Replace("[TEXT]", toastMessage));
        public ILocator GetNoDataFound() => page.Locator(NoDataFound);

        public ILocator GetCharacteristicErrorTitle() => page.Locator(CharErrorTitle);
        public ILocator GetCharacteristicErrorMessage() => page.Locator(CharErrorMessage);
    }
}

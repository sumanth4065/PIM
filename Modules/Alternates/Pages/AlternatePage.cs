using Microsoft.Playwright;
using TechTalk.SpecFlow;

namespace PIMQAAutomation.Modules.Alternates.Pages
{
    /// <summary>
    /// Page Object class containing locators for the Alternate module UI elements.
    /// This class follows the Page Object Model pattern and contains only locator methods.
    /// All business logic and actions should be placed in corresponding Actions classes.
    /// </summary>
    [Binding]
    public class AlternatePage
    {
        private const string AddAnotherItemOption = "#[ITEM_ID_VALUE]";
        private const string AlternateTextField = "//input[@data-placeholder='[FIELD_NAME]']";
        private const string Button = "#alternate-[PANEL_NAME]-[BUTTON_NAME]-button";
        private const string CheckBox = "#alternate-[PANEL_NAME]-[CHECKBOX_NAME]-checkbox";
        private const string ClusterItemRowDetails = "(//table[@id='alternate-[PANEL_NAME]-items-table']//tr[td[contains(., '[ITEM_ID]')]])";
        private const string ClusterToClusterRelationshipIsActiveCheckbox = "#alternate-[PANEL_NAME]-[CHECKBOX_NAME]-checkbox";
        private const string CreatedAndModifiedDate = "(//div[@id='alternate-[PANEL_NAME]-active-[FIELD_NAME]-container']) | (//div[@id='alternate-[PANEL_NAME]-[FIELD_NAME]-container'])";
        private const string CreatedAndModifiedDateAndUser = "//span[@id='alternate-[PANEL_NAME]-[TAB_NAME]-[TEXT_NAME]']";
        private const string DisplayActiveToggleButton = "#alternate-supersession-mode-master-interchangeable-toggle-[TEXT]-label";
        private const string DisplayInactiveToggleButton = "#alternate-cluster-mode-master-interchange-toggle-[TEXT]-label";
        private const string ItemRowDetails = "//table[@id='alternate-[PANEL_NAME]-master-interchange-items-table']//tr[td[contains(., '[ITEM_ID]')]] | //table[@id='alternate-[PANEL_NAME]-master-table']//tr[td[contains(., '[ITEM_ID]')]] | //table[@id='alternate-[PANEL_NAME]-master-interchangeable-items-table']//tr[td[contains(., '[ITEM_ID]')]]";
        private const string PanelTwoTextField = "(//input[@id='alternate-[PANEL_NAME]-[FIELD_NAME]-autocomplete-input'])[1] | (//input[@id='alternate-[PANEL_NAME]-[FIELD_NAME]-input'])[1]";
        private const string PannelTab = "#alternate-[PANEL_NAME]-[TAB_NAME]-tab a";
        private const string PopupClusterRowItemDetails = "(//table[@id='alternate-[PANEL_NAME]-popup-master-table-[TABLE_NAME]-items']//tr[td[contains(., '[ITEM_ID]')]])";
        private const string PopUpItemTable = "(//table[@id='alternate-cluster-mode-alternate-items-popup-cluster-table']) | (//table[@id='alternate-supersession-mode-cluster-relationship-popup-cluster-table'])";
        private const string PopUpMessage = "//h4[contains(text(), '[TEXT]')]";
        private const string PopupOkButton = "#modal-ok-button";
        private const string PopUpPanelTwoTextField = "(//input[@id='alternate-[PANEL_NAME]-[FIELD_NAME]-input'])[1]";
        private const string PopupTableCloseButton = "#alternate-[PANEL_NAME]-popup-[BUTTON_NAME]-button";
        private const string PopUpTableTotalAndTotalActive = "//td[contains(@id,'alternate-[PANEL_NAME]-popup-table-body-[FIELD_NAME]')]";
        private const string SuccessTitlePopup = "#modal-title";
        private const string SupersessionAltItemsManagementToolCloseButton = "//button[text()='Close']";
        private const string Tab = "#alternate-[TAB_NAME]-tab a";
        private const string TablePopUpTitle = "//h4[@id='alternate-[PANEL_NAME]-popup-header-title']";
        private const string TextField = "(//input[@id='alternate-[PANEL_NAME]-[FIELD_NAME]-autocomplete-input']) | (//input[@id='alternate-[PANEL_NAME]-[FIELD_NAME]-input'])";
        private const string TotalClusterField = "[id='[FIELD_NAME]']";
        private const string TotalTextField = "#alternate-[PANEL_NAME]-[FIELD_NAME]-input";

        private readonly IPage page;

        public AlternatePage(IPage page) => this.page = page;

        public ILocator GetAddAnotherItemOption(string itemIdValue) => page.Locator(AddAnotherItemOption.Replace("[ITEM_ID_VALUE]", itemIdValue));

        public ILocator GetAlternateDropdownInputField(string fieldName) => page.Locator(AlternateTextField.Replace("[FIELD_NAME]", fieldName));

        public ILocator GetButton(string panelName, string buttonName) => page.Locator(Button.Replace("[PANEL_NAME]", panelName).Replace("[BUTTON_NAME]", buttonName)).First;

        public ILocator GetCheckBox(string panelName, string checkboxName) => page.Locator(CheckBox.Replace("[PANEL_NAME]", panelName).Replace("[CHECKBOX_NAME]", checkboxName));

        public ILocator GetClusterItemRowDetails(string panelName, string itemId) => page.Locator(ClusterItemRowDetails.Replace("[PANEL_NAME]", panelName).Replace("[ITEM_ID]", itemId));

        public ILocator GetClusterToClusterRelationshipIsActiveCheckbox(string panelName, string checkboxName) => page.Locator(ClusterToClusterRelationshipIsActiveCheckbox.Replace("[PANEL_NAME]", panelName).Replace("[CHECKBOX_NAME]", checkboxName));

        public ILocator GetCreatedAndModifiedDateAndUser(string panelName, string tabName, string text) => page.Locator(CreatedAndModifiedDateAndUser.Replace("[PANEL_NAME]", panelName).Replace("[TAB_NAME]", tabName).Replace("[TEXT_NAME]", text));

        public ILocator GetCreatedModifiedUsernameAndDate(string panelName, string fieldName) => page.Locator(CreatedAndModifiedDate.Replace("[PANEL_NAME]", panelName).Replace("[FIELD_NAME]", fieldName));

        public ILocator GetDisplayActiveToggleButton(string state) => page.Locator(DisplayActiveToggleButton.Replace("[TEXT]", state));

        public ILocator GetDisplayInactiveToggleButton(string text) => page.Locator(DisplayInactiveToggleButton.Replace("[TEXT]", text));

        public ILocator GetItemRowDetails(string panelName, string itemId) => page.Locator(ItemRowDetails.Replace("[PANEL_NAME]", panelName).Replace("[ITEM_ID]", itemId));

        public ILocator GetItemRowDetails(string itemId) => page.Locator(ItemRowDetails.Replace("[TEXT]", itemId));

        public ILocator GetPanelTab(string panelName, string tabName) => page.Locator(PannelTab.Replace("[PANEL_NAME]", panelName).Replace("[TAB_NAME]", tabName));

        public ILocator GetPanelTwoTextFiel(string panelName, string filedName) => page.Locator(PanelTwoTextField.Replace("[PANEL_NAME]", panelName).Replace("[FIELD_NAME]", filedName));

        public ILocator GetPopupClusterRowItemDetails(string panelName, string tableName, string itemID) => page.Locator(PopupClusterRowItemDetails.Replace("[PANEL_NAME]", panelName).Replace("[TABLE_NAME]", tableName).Replace("[ITEM_ID]", itemID)).First;

        public ILocator GetPopUpItemTable() => page.Locator(PopUpItemTable);

        public ILocator GetPopUpMessage(string text) => page.Locator(PopUpMessage.Replace("[TEXT]", text));

        public ILocator GetPopupOkButton() => page.Locator(PopupOkButton).First;

        public ILocator GetPopupTableCloseButton(string panelName, string buttonName) => page.Locator(PopupTableCloseButton.Replace("[PANEL_NAME]", panelName).Replace("[BUTTON_NAME]", buttonName));

        public ILocator GetPopUpTableTotalItemsAndTotalActive(string panelName, string fieldName) => page.Locator(PopUpTableTotalAndTotalActive.Replace("[PANEL_NAME]", panelName).Replace("[FIELD_NAME]", fieldName));

        public ILocator GetPopUpTextField(string panelName, string fieldName) => page.Locator(PopUpPanelTwoTextField.Replace("[PANEL_NAME]", panelName).Replace("[FIELD_NAME]", fieldName));

        public ILocator GetSuccessTitlePopup() => page.Locator(SuccessTitlePopup).First;

        public ILocator GetSupersessionAltItemsManagementToolCloseButton() => page.Locator(SupersessionAltItemsManagementToolCloseButton);

        public ILocator GetTab(string tabName) => page.Locator(Tab.Replace("[TAB_NAME]", tabName));

        public ILocator GetTablePopUpTitle(string panelName) => page.Locator(TablePopUpTitle.Replace("[PANEL_NAME]", panelName));

        public ILocator GetTextField(string pannelName, string fieldName) => page.Locator(TextField.Replace("[PANEL_NAME]", pannelName).Replace("[FIELD_NAME]", fieldName)).First;

        public ILocator GetTotalClusterField(string fieldName) => page.Locator(TotalClusterField.Replace("[FIELD_NAME]", fieldName));

        public ILocator GetTotalTextField(string panelName, string fieldName) => page.Locator(TotalTextField.Replace("[PANEL_NAME]", panelName).Replace("[FIELD_NAME]", fieldName));
    }
}
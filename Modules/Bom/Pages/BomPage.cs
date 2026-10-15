using Microsoft.Playwright;

namespace PIMQAAutomation.Modules.Bom.Pages
{
    public class BomPage
    {
        // BOM Header locators
        private const string PageTab = "//div[@data-test-id='bom-management-tool-[TEXT]-tab']";
        private const string BomInputField = "//input[@id='[TAB_NAME]-header-[FIELD_NAME]-autocomplete-input' or @id='[TAB_NAME]-header-[FIELD_NAME]-name']";
        private const string CreateBomInputField = "//input[@id='[TEXT1]-header-[TEXT2]-autocomplete-input-create']";
        private const string BomHeaderInternalNotes = "//textarea[contains(@id,'[TEXT]-header-internal-notes')]";
        private const string CreatedModifiedUsernameAndDateBomHeader = "//span[@id='bom-history-header-[TEXT]-value']";
        private const string TaxonomyDescription = "//input[@id='[TAB_NAME]-header-[FIELD_NAME]-description']";
        private const string HistoryButton = "//span[@id='bom-header-history-button']";
        private const string ToggleButton = "//button[contains(@id,'header-toggle-button')]/span";
        private const string CloseHistoryIcon = "//button[@id='bom-history-close-button']";
        private const string ButtonInBOMHeader = "//button[@id='[TAB]-header-[BUTTON]-button' or @id='bom-create-modal-[BUTTON]-button']";

        // BOM Line locators
        private const string BomLineTableValue = "//span[@id='bom-filter-table-body-[FIELD_NAME]-[ROW_COUNT]']";
        private const string BomLineCreationValue = "//input[contains(@id,'bom-category-[FIELD_NAME]')]";
        private const string BomLineCreationObsoleteCheckbox = "//input[@id='bom-category-obsolete-checkbox']";
        private const string ButtonInBOMLineCreation = "//button[@id='bom-category-[TEXT]-button']";
        private const string BomLineEditButton = "//div[@id='bom-filter-table-button-edit-[TEXT]']";
        private const string QuantityErrorMessage = "//div[@id='bom-category-quantity-validation']/span";

        // Relationship and metadata locators
        private const string ItemToBomNotes = "#bom-category-item-to-bom-notes-textarea";
        private const string BomLineCreatedDateModifiedDateAndUser = "#bom-history-line-[TEXT1]-value-[TEXT2]";

        private readonly IPage page;

        public BomPage(IPage page) => this.page = page;

        // BOM Header methods
        public ILocator GetPageTab(string text) => page.Locator(PageTab.Replace("[TEXT]", text.ToLower()));
        public ILocator GetBomInputField(string tabName, string fieldName) => page.Locator(BomInputField.Replace("[TAB_NAME]", tabName.ToLower()).Replace("[FIELD_NAME]", fieldName.ToLower()));
        public ILocator GetCreateBomInputField(string tabName, string fieldName) => page.Locator(CreateBomInputField.Replace("[TEXT1]", tabName.ToLower()).Replace("[TEXT2]", fieldName.ToLower()));
        public ILocator GetBomHeaderInternalNotes(string tabName) => page.Locator(BomHeaderInternalNotes.Replace("[TEXT]", tabName.ToLower()));
        public ILocator GetCreatedModifiedUsernameAndDateBomHeader(string fieldName) => page.Locator(CreatedModifiedUsernameAndDateBomHeader.Replace("[TEXT]",fieldName));
        public ILocator GetTaxonomyDescription(string tabName, string fieldName) => page.Locator(TaxonomyDescription.Replace("[TAB_NAME]", tabName).Replace("[FIELD_NAME]", fieldName));
        public ILocator GetQuantityErrorMessage() => page.Locator(QuantityErrorMessage);
        public ILocator GetHistoryButton() => page.Locator(HistoryButton);
        public ILocator GetToggleButton() => page.Locator(ToggleButton);
        public ILocator GetCloseHistoryIcon() => page.Locator(CloseHistoryIcon);
        public ILocator GetButtonInBOMHeader(string tabName, string buttonName) => page.Locator(ButtonInBOMHeader.Replace("[TAB]", tabName.ToLower()).Replace("[BUTTON]", buttonName.ToLower()));

        // BOM Line methods
        public ILocator GetBomLineTableValue(string fieldName, int rowCount) => page.Locator(BomLineTableValue.Replace("[FIELD_NAME]", fieldName.ToLower()).Replace("[ROW_COUNT]", rowCount.ToString()));

        public ILocator GetBomLineCreationValue(string fieldName) => page.Locator(BomLineCreationValue.Replace("[FIELD_NAME]", fieldName.ToLower()));

        public ILocator GetBomLineCreationObsoleteCheckbox() => page.Locator(BomLineCreationObsoleteCheckbox);

        public ILocator GetButtonInBOMLineCreation(string text) => page.Locator(ButtonInBOMLineCreation.Replace("[TEXT]", text.ToLower()));
        public ILocator GetBomLineEditButton(int number) => page.Locator(BomLineEditButton.Replace("[TEXT]", number.ToString()));

        // Relationship and metadata methods
        public ILocator GetItemToBomNotes() => page.Locator(ItemToBomNotes);
        public ILocator GetCreatedDateInItemToBomRelationship(string fieldName, int rowCount) => page.Locator(BomLineCreatedDateModifiedDateAndUser.Replace("[TEXT1]", fieldName.ToLower()).Replace("[TEXT2]", rowCount.ToString()));
    }
}
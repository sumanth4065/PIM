using Microsoft.Playwright;
using NUnit.Framework;

namespace PIMQAAutomation.Modules.AdminTool.Pages
{
    public class AdminToolPage
    {
        private readonly IPage _page;

        public AdminToolPage(IPage page) => _page = page;


        private const string AdminToolMainTab = "//li[@id='admin-tool-[TEXT]-tab']";

        private const string AdminToolSubTab = "//a[@data-test-id='admin-[TEXT1]-[TEXT2]-tab-link']";

        private const string SelectBOMStyleOrKind = "//div[contains(@id,'admin-[TEXT1]-item') and contains(text(),'[TEXT2]')]";
        private const string BOMKindButton = "//button[@id='admin-[TEXT1]-[TEXT2]-button']";
        private const string BOMKindCheckbox = "//input[@data-test-id='admin-bom-[TEXT]-checkbox']";
        private const string CreatedModifiedUserDate = "//span[@id='common-date-[TEXT]']";
        private const string FirstLineBOMStyleOrKind = "//div[@id='admin-[TEXT1]-item-[TEXT2]']";
        private const string InputField = "//input[@data-test-id='admin-bom-[TEXT]-input']";
        private const string InternalNotes = "//textarea[contains(@id,'[TEXT]-textarea')]";
        private const string PrimaryLOVsFiled = "//input[@id='admin-lovs-[TEXT1]-[TEXT2]-view-autocomplete-input' or @id='admin-lovs-[TEXT1]-[TEXT2]-edit-autocomplete-input' or @id='admin-lovs-[TEXT1]-[TEXT2]-input' or @id='admin-lovs-[TEXT1]-[TEXT2]-autocomplete-input']";

        public ILocator GetAdminToolMainTab(string mainTabName) => _page.Locator(AdminToolMainTab.Replace("[TEXT]", mainTabName));

        public ILocator GetAdminToolSubTab(string mainTabName, string subTabName) => _page.Locator(AdminToolSubTab.Replace("[TEXT1]", mainTabName).Replace("[TEXT2]", subTabName));
        public ILocator GetSelectBOMStyleOrkind(string bomStyleOrKind, string bomStyleOrKindName) => _page.Locator(SelectBOMStyleOrKind.Replace("[TEXT1]", bomStyleOrKind).Replace("[TEXT2]", bomStyleOrKindName));
        public ILocator GetBOMKindButton(string tabName, string buttonName) => _page.Locator(BOMKindButton.Replace("[TEXT1]", tabName).Replace("[TEXT2]", buttonName));
        public ILocator GetBOMKindCheckbox(string checkbox) => _page.Locator(BOMKindCheckbox.Replace("[TEXT]", checkbox));
        public ILocator GetCreatedModifiedUserDate(string createdModifiedUserDate) => _page.Locator(CreatedModifiedUserDate.Replace("[TEXT]", createdModifiedUserDate));
        public ILocator GetFirstLineBOMStyleOrKind(string bomStyleOrKind, int rowNumber) => _page.Locator(FirstLineBOMStyleOrKind.Replace("[TEXT1]", bomStyleOrKind).Replace("[TEXT2]", rowNumber.ToString()));
        public ILocator GetInputField(string fieldName) => _page.Locator(InputField.Replace("[TEXT]", fieldName));
        public ILocator GetInternalNotes(string notes) => _page.Locator(InternalNotes.Replace("[TEXT]", notes));
        public ILocator GetPrimaryLOVsFiled(string subTab, string fieldName) => _page.Locator(PrimaryLOVsFiled.Replace("[TEXT1]", subTab).Replace("[TEXT2]", fieldName));
    }

    public class AdminToolPageActions
    {
        private IPage _page;

        public AdminToolPageActions(IPage page) => _page = page;
        public static async Task VerifyCheckboxStateAsync(ILocator locator, string checkboxName, bool shouldBeChecked)
        {
            bool actualState = await locator.IsCheckedAsync();
            Assert.AreEqual(shouldBeChecked, actualState, $"Checkbox '{checkboxName}' checked state mismatch.");
        }
    }
}

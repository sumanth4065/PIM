using Microsoft.Playwright;
using PIMQAAutomation.Modules.Common.Constants;

namespace PIMQAAutomation.Modules.Lob.Pages
{
    public class LobPage
    {
        private const string ItemMenu = "//li[@id='item']";
        private const string CharacteristicsMenu = "//li[@id='item-configuration']";
        private const string LobMenu = "//li[@id='item-lob']";
        private const string LobHeaderTitle = "//div[normalize-space()='Line Of Business']";
        private const string GeneralLedgerDescription = "//div[@id='admin-lovs-lob-to-gl-[TEXT]']/span";
        private const string ServiceTaxonomyName = "//input[@id='admin-lovs-lob-to-gl-service-[TEXT]-name-input']";
        private const string PrimaryLOVsFiled = "//input[@id='admin-lovs-[TEXT1]-[TEXT2]-view-autocomplete-input' or @id='admin-lovs-[TEXT1]-[TEXT2]-edit-autocomplete-input' or @id='admin-lovs-[TEXT1]-[TEXT2]-input' or @id='admin-lovs-[TEXT1]-[TEXT2]-autocomplete-input']";
        private const string CreatedModifiedUserDateField = "//span[@id='admin-lovs-lob-to-gl-[TEXT]']";

        private readonly IPage page;

        public LobPage(IPage page) => this.page = page;

        public ILocator GetItemMenu() => page.Locator(ItemMenu);

        public ILocator GetCharacteristicsMenu() => page.Locator(CharacteristicsMenu);

        public ILocator GetLobMenu() => page.Locator(LobMenu);

        public ILocator GetLobHeaderTitle() => page.Locator(LobHeaderTitle);

        public ILocator GetServiceTaxonomyName(string serviceName) => page.Locator(ServiceTaxonomyName.Replace("[TEXT]", serviceName.ToLower()));

        public ILocator GetPrimaryLOVsFiled(string subTab, string fieldName) => page.Locator(PrimaryLOVsFiled.Replace("[TEXT1]", subTab.ToLower()).Replace("[TEXT2]", fieldName.ToLower()));

        public ILocator GetCreatedModifiedUserDate(string createdModifiedUserDate) => page.Locator(CreatedModifiedUserDateField.Replace("[TEXT]", createdModifiedUserDate.ToLower()));

        public ILocator GetGeneralLedgerDescription(string generalLedgerDescription) => page.Locator(GeneralLedgerDescription.Replace("[TEXT]", generalLedgerDescription.ToLower()));

        public async Task NavigateToLineOfBusinessAsync()
        {
            // Expand "Item" menu if collapsed
            if ((await GetItemMenu().GetAttributeAsync(AttributeConstants.AriaExpanded)) == AttributeConstants.False)
            {
                await GetItemMenu().ClickAsync();
            }
            // Expand "Configuration" menu if collapsed
            if ((await GetCharacteristicsMenu().GetAttributeAsync(AttributeConstants.AriaExpanded)) == AttributeConstants.False)
            {
                await GetCharacteristicsMenu().ClickAsync();
            }
            // Click "Line of Business"
            await GetLobMenu().WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            await GetLobMenu().ClickAsync();
            //wait for LOB page load
            await GetLobHeaderTitle().IsVisibleAsync();
        }
    }
}
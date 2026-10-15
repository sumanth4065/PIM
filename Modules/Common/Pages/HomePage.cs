using Microsoft.Playwright;

namespace PIMQAAutomation.Common.Pages
{
    internal class HomePage
    {
        private readonly IPage _page;

        private const string PageHeader = "//u[normalize-space()='Welcome to the Park Place Item Master']";
        private const string HamburgerMenu = "//li[@id='[TEXT]']";
        private const string ManagementOption = "//span[normalize-space(text())='Management']";

        public HomePage(IPage page) => _page = page;

        public ILocator GetHeaderDetails() => _page.Locator(PageHeader);

        public ILocator GetHamburgerMenu(string text) => _page.Locator(HamburgerMenu.Replace("[TEXT]", text).ToLower());
        public ILocator GetManagementOption() => _page.Locator(ManagementOption);
    }
}
using Microsoft.Playwright;

namespace PIMQAAutomation.Common.Pages
{
    public class PIMLoginPage
    {
        private const string LoginButton = "//button[text()='Login']";
        private const string MicrosoftPassword = "//input[@type='password']";
        private const string MicrosoftUsername = "//input[@type='email']";
        private const string NextButton = "#idp-discovery-submit";
        private const string Password = "#okta-signin-password";
        private const string SendPushButton = "//input[@type='submit']";
        private const string SignInButton = "#okta-signin-submit";
        private const string SubmitButton = "//input[@type='submit']";
        private const string UserName = "#idp-discovery-username";
        private readonly IPage page;

        public PIMLoginPage(IPage page) => this.page = page;

        public ILocator GetLogin() => page.Locator(LoginButton);

        public ILocator GetMicrosoftPassword() => page.Locator(MicrosoftPassword);

        public ILocator GetMicrosoftUsername() => page.Locator(MicrosoftUsername);

        public ILocator GetNext() => page.Locator(NextButton);

        public ILocator GetPassword() => page.Locator(Password);

        public ILocator GetSendPush() => page.Locator(SendPushButton);

        public ILocator GetSignIn() => page.Locator(SignInButton);

        public ILocator GetSubmitButton() => page.Locator(SubmitButton);

        public ILocator GetUserName() => page.Locator(UserName);
    }
}
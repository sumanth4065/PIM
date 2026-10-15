using Microsoft.Playwright;
using NUnit.Framework;
using PIMQAAutomation.Common.Pages;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Utilities;
using System.Text.Json;
using TechTalk.SpecFlow;

namespace PIMQAAutomation.Modules.Common.Steps
{
    [Binding]
    public class CommonSteps
    {
        private readonly CommonLocators commonLocators;
        private readonly HomePage homePage;
        private readonly IPage page;
        private readonly PIMLoginPage pimLoginPage;

        public CommonSteps(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            page = featureContext["Page"] as IPage;
            pimLoginPage = new PIMLoginPage(page);
            commonLocators = new CommonLocators(page);
            homePage = new HomePage(page);
        }

        [When(@"User selects the ""(.*)"" in Hamburger menu")]
        public async Task GivenUserClickOnTheInHamburgerMenu(string navigationTab)
        {
            await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
            var menuElement = homePage.GetHamburgerMenu(navigationTab);
            var isExpanded = await menuElement.GetAttributeAsync(AttributeConstants.AriaExpanded);
            if (string.IsNullOrEmpty(isExpanded) || isExpanded == AttributeConstants.False)
            {
                await menuElement.ClickAsync();
            }
            else
            {
                await homePage.GetManagementOption().ClickAsync();
            }
        }

        [Given("User login to the PIM application using the ppt username and password")]
        public async Task GivenUserLoginToThePIMApplicationUsingThePPTUsernameAndPassword()
        {
            int attempt = 0;
            while (attempt < 3)
            {
                try
                {
                    attempt++;
                    await page.GotoAsync(SecureDataHelper.DecryptWithMachineKey(TestContext.Parameters[RunSettingsConstants.PIMQAUrl]));

                    // Add a random delay to stagger login across parallel threads
                    Random rnd = new Random();
                    await Task.Delay(rnd.Next(TimeConstants.DefaultTimeOut));
                    if (attempt > 1)
                    {
                        await Task.Delay(TimeConstants.LowWaitTimeMs);
                        await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
                        var headerPageLocator = homePage.GetHeaderDetails();
                        if (await headerPageLocator.IsVisibleAsync())
                        {
                            Console.WriteLine("Already authenticated, skipping login.");
                            return;
                        }
                    }

                    // Fill login form
                    await pimLoginPage.GetLogin().ClickAsync();
                    await pimLoginPage.GetUserName().FillAsync(SecureDataHelper.DecryptWithMachineKey(TestContext.Parameters[RunSettingsConstants.PPTUsername]));
                    await pimLoginPage.GetNext().ClickAsync();
                    var microsoftAuth = ValidationConstants.MicrosoftAuth;
                    if (microsoftAuth)
                    {
                        var username = pimLoginPage.GetMicrosoftUsername();
                        await username.IsVisibleAsync();
                        await username.FillAsync(SecureDataHelper.DecryptWithMachineKey(TestContext.Parameters[RunSettingsConstants.PPTUsername]));
                        await pimLoginPage.GetSubmitButton().ClickAsync();
                        await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
                        var password = pimLoginPage.GetMicrosoftPassword();
                        await password.IsVisibleAsync();
                        await password.FillAsync(SecureDataHelper.DecryptWithMachineKey(TestContext.Parameters[RunSettingsConstants.PPTPassword]));
                        await pimLoginPage.GetSubmitButton().ClickAsync();
                    }
                    else
                    {
                        await pimLoginPage.GetPassword().FillAsync(SecureDataHelper.DecryptWithMachineKey(TestContext.Parameters[RunSettingsConstants.PPTPassword]));
                        await pimLoginPage.GetSignIn().ClickAsync();
                    }

                    await page.WaitForLoadStateAsync();

                    // Wait for response AFTER clicking SignIn
                    var waitForResponse = page.WaitForResponseAsync(
                       response =>
                       response.Url.Contains(SecureDataHelper.DecryptWithMachineKey(TestContext.Parameters[RunSettingsConstants.ApiUri])) &&
                       response.Status >= 200 && response.Status < 300,
                       new PageWaitForResponseOptions { Timeout = TimeConstants.LoginTimeout });

                    // Handle Terms & Conditions popup
                    if (await commonLocators.GetTermsAndConditionsPopUp().IsVisibleAsync())
                    {
                        await commonLocators.GetCheckboxTermsAndCondition().ClickAsync();
                        await commonLocators.GetAcceptButton().ClickAsync();
                    }

                    // Now the awaited response should not timeout
                    var response = await waitForResponse;
                    var responseBody = await response.BodyAsync();
                    var json = JsonDocument.Parse(responseBody);
                    var accessToken = json.RootElement.GetProperty(ApplicationConstants.AccessToken).GetString();
                    await Hooks.Hooks.DeleteApiIfRequiredAsync(accessToken);

                    // Final UI check
                    var headerLocator = homePage.GetHeaderDetails();
                    await headerLocator.WaitForAsync(new() { Timeout = TimeConstants.DefaultTimeOut });
                    await Assertions.Expect(headerLocator).ToHaveTextAsync(ApplicationConstants.HomePageHeaderMessage);

                    Console.WriteLine($"Login attempt {attempt} succeeded.");
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Login attempt {attempt} failed. Reason: {e.GetType().Name} - {e.Message}");
                    if (attempt == 3)
                    {
                        ValidationHelper.Fail($"Login failed after 3 attempts.", e);
                    }

                    await Task.Delay(TimeConstants.LowWaitTimeMs);
                }
            }
        }

        [Then("User clicks on Yes button on the pop up")]
        public async Task ThenUserClicksOnYesButtonPopup()
        {
            await page.WaitForLoadStateAsync();
            var yesButton = commonLocators.GetCreatedPopupYesButton();
            if (await yesButton.IsVisibleAsync())
                {
                await yesButton.ClickAsync();
            }
        }

        [Then("User confirms the Save changes")]
        public async Task WhenUserConfirmsTheSaveChanges()
        {
            for (int attempt = 1; attempt <= 3; attempt++)
            {
                if (await commonLocators.GetSavePopupMessage().IsVisibleAsync())
                {
                    if (await commonLocators.GetPopupYesButton().IsVisibleAsync())
                    {
                        await commonLocators.GetPopupYesButton().ClickAsync();
                        await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());

                        if (await commonLocators.GetSuccessTitlePopup().IsVisibleAsync())
                        {
                            await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());
                            if (await commonLocators.GetPopupOkButton().IsVisibleAsync())
                            {
                                await commonLocators.GetPopupOkButton().ClickAsync();
                                break;
                            }
                            else
                            {
                                Assert.Fail("Success popup appeared, but OK button is not visible.");
                            }
                        }
                        else
                        {
                            Assert.Fail("Success title popup not visible after clicking Yes.");
                        }
                    }
                    else
                    {
                        Assert.Fail("Save popup appeared, but Yes button is not visible.");
                    }
                }
                else
                {
                    if (attempt == 3)
                    {
                        Assert.Fail("Save popup message did not appear after 3 attempts.");
                    }
                }
            }
        }

        [When(@"User confirms the ""(.*)"" by clicking on OK button")]
        public async Task WhenUserConfirmsTheSaveChangesByClickingOKButton(string popupTitle)
        {
            await BasePage.WaitForElementToDisappearAsync(commonLocators.GetLoader());

            string successTitle = await commonLocators.GetSuccessTitlePopup().InnerHTMLAsync();
            successTitle = successTitle.Trim();
            popupTitle = popupTitle.Trim();

            Assert.AreEqual(successTitle, popupTitle, $"Mismatch for field '{popupTitle}'. Expected: '{popupTitle}' but found '{successTitle}'.");

            await commonLocators.GetPopupOkButton().ClickAsync();
        }

        [When("User refreshes the page")]
        public async Task WhenUserReloadsTheApplicationPage()
        {
            await page.ReloadAsync();
        }
    }
}
using Microsoft.Playwright;
using NUnit.Framework;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Utilities;
namespace PIMQAAutomation.Common.Pages
{
    public static class BasePage
    {
        public static async Task ClearInputIfNotEmptyAsync(ILocator inputLocator)
        {
            if (!string.IsNullOrWhiteSpace(await inputLocator.InputValueAsync()))
            {
                await inputLocator.ClearAsync();
            }
        }

        public static async Task WaitForElementToDisappearAsync(ILocator locator, int timeout = 80000)
        {
            try
            {
                await locator.WaitForAsync(new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Detached,
                    Timeout = timeout
                });
            }
            catch (Exception e)
            {
                Console.WriteLine($"Element '{locator}' did not disappear within {timeout} ms, {e.Message}");
            }
        }

        public static async Task VerifyFieldIsEnabled(ILocator locator, string elementName, int timeout = 5000)
        {
            try
            {
                await Assertions.Expect(locator).ToBeEnabledAsync(new LocatorAssertionsToBeEnabledOptions { Timeout = timeout });
            }
            catch (Exception e)
            {
                Assert.Fail($"The field {elementName} is not enabled due to an error {e.Message}");
            }
        }

        public static async Task VerifyFieldIsDisabled(ILocator locator, string elementName, int timeout = 5000)
        {
            try
            {
                await Assertions.Expect(locator).ToBeDisabledAsync(new LocatorAssertionsToBeDisabledOptions { Timeout = timeout });
            }
            catch (Exception e)
            {
                Assert.Fail($"The field {elementName} is not enabled due to an error {e.Message}");
            }
        }

        public static async Task TypeTextAsync(ILocator inputLocator, string textToEnter)
        {
            try
            {
                // Wait until the element is visible
                await inputLocator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });

                // Get the element's tag name
                var tagName = (await inputLocator.EvaluateAsync<string>("el => el.tagName")).ToLower();

                if (tagName == "input" || tagName == "textarea")
                {
                    // Handle normal input fields
                    await ClearInputIfNotEmptyAsync(inputLocator);
                    await inputLocator.PressSequentiallyAsync(textToEnter, new() { Delay = TimeConstants.DelayPerChar });
                }
                else
                {
                    // Handle custom dropdowns
                    Console.WriteLine($"[Info] Element is not an input. Treating as custom dropdown.");

                    // Click to focus or open the dropdown
                    await inputLocator.ClickAsync();

                    // If the custom dropdown has a hidden input inside, fill that
                    var innerInput = inputLocator.Locator("input, textarea");
                    if (await innerInput.CountAsync() > 0)
                    {
                        await ClearInputIfNotEmptyAsync(innerInput.First);
                        await innerInput.First.PressSequentiallyAsync(textToEnter);
                    }
                    else
                    {
                        // If no inner input, type directly into the element
                        await inputLocator.PressSequentiallyAsync(textToEnter);
                    }
                }
            }
            catch (Exception ex)
            {
                var errorMessage = $"Failed to type text '{textToEnter}' into dropdown/input. Error: {ex.Message}";
                ValidationHelper.Fail(errorMessage, ex);
            }
        }
        public static async Task<string> ValidateToastMessage(IPage page, ILocator locator, string expectedMessage)
        {
            string toastText = string.Empty;
            try
            {
                bool isFound = false;
                toastText = string.Empty;

                int retryCount = 0;
                const int maxRetries = 3;

                // Initial toast count
                int count = await locator.CountAsync();

                // Retry if toast not found initially
                while (count == 0 && retryCount < maxRetries)
                {
                    retryCount++;
                    Console.WriteLine($"[Retry {retryCount}] No toast message found. Waiting {TimeConstants.ToastMessageWait} ms...");
                    await page.WaitForTimeoutAsync(TimeConstants.ToastMessageWait);
                    count = await locator.CountAsync();
                }

                Console.WriteLine($"Total toast messages found after {retryCount} retries: {count}");
                // Validate toast message
                for (int i = 0; i < count; i++)
                {
                    toastText = (await locator.Nth(i).TextContentAsync())?.Trim() ?? string.Empty;

                    if (toastText.Contains(expectedMessage, StringComparison.OrdinalIgnoreCase))
                    {
                        isFound = true;
                        Console.WriteLine($"Expected toast message found: {toastText}");
                        return toastText;
                    }
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail("Failed while verifying toast message.", ex);
            }
            return string.Empty;
        }
    }
}
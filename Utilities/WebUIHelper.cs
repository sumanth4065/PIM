using Microsoft.Playwright;
using PIMQAAutomation.Common.Pages;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Utilities;
using TechTalk.SpecFlow;

namespace Utilities
{
    public static class WebUIHelper
    {
        public static async Task FillAsync(ScenarioContext scenarioContext, IPage page, string locatorKey, string value)
        {
            try
            {
                LocatorReader locatorReader = new LocatorReader(scenarioContext);
                var locator = locatorReader.GetByKey(page, locatorKey);
                await locator.FillAsync(value);
                Logger.LogInfo($"Filled {locatorKey} with value: {value}");
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to fill {locatorKey} with value {value}");
                throw new CustomException($"Unable to fill value '{value}' in element '{locatorKey}'", ex);
            }
        }

        public static async Task ClickAsync(ScenarioContext scenarioContext, IPage page, string locatorKey)
        {
            try
            {
                LocatorReader locatorReader = new LocatorReader(scenarioContext);
                var locator = locatorReader.GetByKey(page, locatorKey);
                await locator.ClickAsync();
                Logger.LogInfo($"Clicked on {locatorKey}");
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to click on {locatorKey}");
                throw new CustomException($"Unable to click on element '{locatorKey}'", ex);
            }
        }

        public static async Task<bool> IsVisibleAsync(ScenarioContext scenarioContext, IPage page, string locatorKey)
        {
            try
            {
                LocatorReader locatorReader = new LocatorReader(scenarioContext);
                var locator = locatorReader.GetByKey(page, locatorKey);
                Logger.LogInfo($"{locatorKey} Is visible");
                return await locator.IsVisibleAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError($"{locatorKey} is not visible");
                throw new CustomException($"{locatorKey} is not visible", ex);
            }
        }

        public static async Task<bool> IsVisibleAsync(ScenarioContext scenarioContext, IPage page, string locatorKey, string replaceString, string replaceStrings = null)
        {
            try
            {
                LocatorReader locatorReader = new LocatorReader(scenarioContext);
                var locator = locatorReader.GetByKey(page, locatorKey, replaceString, replaceStrings);
                Logger.LogInfo($"{locatorKey} Is visible");
                return await locator.IsVisibleAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError($"{locatorKey} is not visible");
                throw new CustomException($"{locatorKey} is not visible", ex);
            }
        }

        public static async Task ToHaveTextAsync(ScenarioContext scenarioContext, IPage page, string locatorKey, string validateText)
        {
            try
            {
                LocatorReader locatorReader = new LocatorReader(scenarioContext);
                var locator = locatorReader.GetByKey(page, locatorKey);
                await locator.WaitForAsync(new() { Timeout = TimeConstants.DefaultTimeOut });
                await Assertions.Expect(locator).ToHaveTextAsync(validateText);
                Logger.LogInfo($"{locatorKey} validation success with text {validateText}");
            }
            catch (Exception ex)
            {
                Logger.LogError($"{locatorKey} validation failed with text {validateText}");
                throw new CustomException($"{locatorKey} validation failed with text {validateText}", ex);
            }
        }

        public static async Task GotoAsync(ScenarioContext scenarioContext, IPage page, string gotoUrl)
        {
            try
            {
                page.GotoAsync(gotoUrl);
                Logger.LogInfo($"Navigated to {gotoUrl}");
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed navigation to {gotoUrl}");
                throw new CustomException($"Failed navigation to {gotoUrl}", ex);
            }
        }

        public static async Task WaitForElementToDisappearAsync(ScenarioContext scenarioContext, IPage page, string locatorKey, int timeoutSeconds)
        {
            try
            {
                LocatorReader locatorReader = new LocatorReader(scenarioContext);
                var locator = locatorReader.GetByKey(page, locatorKey);
                await locator.WaitForAsync(new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Detached,
                    Timeout = timeoutSeconds * 1000
                });
            }
            catch (Exception ex)
            {
                Logger.LogError($"Element '{locatorKey}' did not disappear within {timeoutSeconds} secs.");
                throw new CustomException($"Element '{locatorKey}' did not disappear within {timeoutSeconds} secs.", ex);
            }
        }

        public static async Task<string> GetInnerTextAsync(ScenarioContext scenarioContext, IPage page, string locatorKey)
        {
            try
            {
                LocatorReader locatorReader = new LocatorReader(scenarioContext);
                var locator = locatorReader.GetByKey(page, locatorKey);
                return await locator.InnerTextAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to retrieve inner text for element '{locatorKey}'. Reason: {ex.Message}");
                throw new CustomException($"Unable to get inner text for element '{locatorKey}'. See inner exception for details.", ex);
            }
        }

        public static async Task<string> TextContentAsync(ScenarioContext scenarioContext, IPage page, string locatorKey)
        {
            try
            {
                LocatorReader locatorReader = new LocatorReader(scenarioContext);
                var locator = locatorReader.GetByKey(page, locatorKey);
                return await locator.TextContentAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to retrieve inner text for element '{locatorKey}'. Reason: {ex.Message}");
                throw new CustomException($"Unable to get inner text for element '{locatorKey}'. See inner exception for details.", ex);
            }
        }

        public static async Task ToHaveTextAsync(ScenarioContext scenarioContext, IPage page, string locatorKey, string expected, LocatorAssertionsToHaveTextOptions? options = null)
        {
            try
            {
                LocatorReader locatorReader = new LocatorReader(scenarioContext);
                var locator = locatorReader.GetByKey(page, locatorKey);
                await Assertions.Expect(locator).ToHaveTextAsync(expected, options);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Assertion failed: Element '{locatorKey}' did not have expected text '{expected}'. Reason: {ex.Message}");
                throw new CustomException($"Assertion failed: Element '{locatorKey}' did not have expected text '{expected}'.", ex);
            }
        }

        public static async Task WaitForAsync(ScenarioContext scenarioContext, IPage page, string locatorKey, int timeoutSeconds, WaitForSelectorState state)
        {
            try
            {
                LocatorReader locatorReader = new LocatorReader(scenarioContext);
                var locator = locatorReader.GetByKey(page, locatorKey);

                await locator.WaitForAsync(new LocatorWaitForOptions
                {
                    Timeout = timeoutSeconds * 1000,
                    State = state
                });
            }
            catch (TimeoutException ex)
            {
                Logger.LogError($"Assertion failed: Timeout waiting {timeoutSeconds}s for element '{locatorKey}' to be '{state}'. Reason: {ex.Message}");
                throw new CustomException($"Assertion failed: Timeout waiting {timeoutSeconds}s for element '{locatorKey}' to be '{state}'.", ex);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Assertion failed: Could not wait for element '{locatorKey}' to be '{state}'. Reason: {ex.Message}");
                throw new CustomException($"Assertion failed: Could not wait for element '{locatorKey}' to be '{state}'.", ex);
            }
        }

        public static async Task WaitForAsync(ScenarioContext scenarioContext, IPage page, string locatorKey, int timeoutSeconds, string replaceText, WaitForSelectorState state, string replaceTexts = null)
        {
            try
            {
                LocatorReader locatorReader = new LocatorReader(scenarioContext);
                var locator = locatorReader.GetByKey(page, locatorKey, replaceText, replaceTexts);

                await locator.WaitForAsync(new LocatorWaitForOptions
                {
                    Timeout = timeoutSeconds * 1000,
                    State = state
                });
            }
            catch (TimeoutException ex)
            {
                Logger.LogError($"Assertion failed: Timeout waiting {timeoutSeconds}s for element '{locatorKey}' to be '{state}'. Reason: {ex.Message}");
                throw new CustomException($"Assertion failed: Timeout waiting {timeoutSeconds}s for element '{locatorKey}' to be '{state}'.", ex);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Assertion failed: Could not wait for element '{locatorKey}' to be '{state}'. Reason: {ex.Message}");
                throw new CustomException($"Assertion failed: Could not wait for element '{locatorKey}' to be '{state}'.", ex);
            }
        }

        public static async Task ToHaveAttributeAsync(ScenarioContext scenarioContext, IPage page, string locatorKey, string attributeName, string expectedValue, LocatorAssertionsToHaveAttributeOptions? options = null)
        {
            try
            {
                LocatorReader locatorReader = new LocatorReader(scenarioContext);
                var locator = locatorReader.GetByKey(page, locatorKey);

                await Assertions.Expect(locator).ToHaveAttributeAsync(attributeName, expectedValue, options);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Assertion failed: Element '{locatorKey}' did not have attribute '{attributeName}' with expected value '{expectedValue}'. Reason: {ex.Message}");
                throw new CustomException($"Assertion failed: Element '{locatorKey}' did not have attribute '{attributeName}' with expected value '{expectedValue}'.", ex);
            }
        }

        public static async Task CheckAsync(ScenarioContext scenarioContext, IPage page, string locatorKey, LocatorCheckOptions? options = null)
        {
            try
            {
                LocatorReader locatorReader = new LocatorReader(scenarioContext);
                var locator = locatorReader.GetByKey(page, locatorKey);

                await locator.CheckAsync(options);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Action failed: Unable to check the checkbox '{locatorKey}'. Reason: {ex.Message}");
                throw new CustomException($"Action failed: Unable to check the checkbox '{locatorKey}'.", ex);
            }
        }

        public static async Task UncheckAsync(ScenarioContext scenarioContext, IPage page, string locatorKey, LocatorUncheckOptions? options = null)
        {
            try
            {
                LocatorReader locatorReader = new LocatorReader(scenarioContext);
                var locator = locatorReader.GetByKey(page, locatorKey);

                await locator.UncheckAsync(options);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Action failed: Unable to uncheck the checkbox '{locatorKey}'. Reason: {ex.Message}");
                throw new CustomException($"Action failed: Unable to uncheck the checkbox '{locatorKey}'.", ex);
            }
        }


        public static async Task<string> GetInputValueAsync(ScenarioContext scenarioContext, IPage page, string locatorKey)
        {
            try
            {
                LocatorReader locatorReader = new LocatorReader(scenarioContext);
                var locator = locatorReader.GetByKey(page, locatorKey);

                var value = await locator.InputValueAsync();

                return value;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Action failed: Unable to get input value from element '{locatorKey}'. Reason: {ex.Message}");
                throw new CustomException($"Action failed: Unable to get input value from element '{locatorKey}'.", ex);
            }
        }

        public static async Task WaitForLoadStateAsync(ScenarioContext scenarioContext, IPage page, LoadState state = LoadState.Load, int timeoutSeconds = 30)
        {
            try
            {
                await page.WaitForLoadStateAsync(state, new PageWaitForLoadStateOptions
                {
                    Timeout = timeoutSeconds * 1000
                });
            }
            catch (TimeoutException ex)
            {
                Logger.LogError($"Action failed: Timeout after {timeoutSeconds}s while waiting for page load state '{state}'. Reason: {ex.Message}");
                throw new CustomException($"Action failed: Timeout after {timeoutSeconds}s while waiting for page load state '{state}'.", ex);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Action failed: Could not wait for page load state '{state}'. Reason: {ex.Message}");
                throw new CustomException($"Action failed: Could not wait for page load state '{state}'.", ex);
            }
        }

        public static async Task HandleCheckboxAsync(ScenarioContext scenarioContext, IPage page, string locatorKey, string fieldValue, string fieldDisplayName)
        {
            if (string.IsNullOrWhiteSpace(fieldValue))
            {
                Console.WriteLine($"[INFO] Skipped '{fieldDisplayName}' checkbox as no test data was provided.");
                return;
            }

            if (fieldValue.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                await CheckAsync(scenarioContext, page, locatorKey);
                Console.WriteLine($"[INFO] Checked {fieldDisplayName} checkbox.");
            }
            else if (fieldValue.Equals("false", StringComparison.OrdinalIgnoreCase))
            {
                await UncheckAsync(scenarioContext, page, locatorKey);
                Console.WriteLine($"[INFO] Unchecked {fieldDisplayName} checkbox.");
            }
            else
            {
                Console.WriteLine($"[WARN] Invalid value '{fieldValue}' provided for {fieldDisplayName} checkbox. Expected 'true' or 'false'.");
            }
        }

        public static async Task<string> GetSelectedDropDownValueAsync(ScenarioContext scenarioContext, IPage page, string locatorKey)
        {
            try
            {
                LocatorReader locatorReader = new LocatorReader(scenarioContext);
                var locator = locatorReader.GetByKey(page, locatorKey);

                await WaitForAsync(scenarioContext, page, locatorKey, 5, WaitForSelectorState.Visible);

                var selectedValue = await locator.InputValueAsync();

                Console.WriteLine($"[INFO] Selected dropdown value for '{locatorKey}' is '{selectedValue}'");
                return selectedValue;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to get selected dropdown value for '{locatorKey}'. Error: {ex.Message}");
                throw new CustomException($"[AUTOMATION ERROR] Could not fetch selected value for dropdown '{locatorKey}'", ex);
            }
        }

        public static async Task SelectDropDownValueAsync(ScenarioContext scenarioContext, IPage page, string dropDownLocatorKey, string optionLocatorKey)
        {
            try
            {
                LocatorReader locatorReader = new LocatorReader(scenarioContext);
                var dropDownLocator = locatorReader.GetByKey(page, dropDownLocatorKey);
                var optionLocator = locatorReader.GetByKey(page, optionLocatorKey);

                await WaitForAsync(scenarioContext, page, dropDownLocatorKey, 5, WaitForSelectorState.Visible);

                await dropDownLocator.ClickAsync();

                await WaitForAsync(scenarioContext, page, optionLocatorKey, 5, WaitForSelectorState.Visible);

                await optionLocator.ClickAsync();

                Console.WriteLine($"[INFO] Selected option in dropdown: '{dropDownLocatorKey}' with option '{optionLocatorKey}'");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to select value for dropdown '{dropDownLocatorKey}' and option '{optionLocatorKey}'. Error: {ex.Message}");
                throw new CustomException($"[ERROR] Could not select value for dropdown '{dropDownLocatorKey}'", ex);
            }
        }

        public static async Task SelectDropDownOptionAsync(ScenarioContext scenarioContext, IPage page, string dropDownLocatorKey, string optionText)
        {
            try
            {
                LocatorReader locatorReader = new LocatorReader(scenarioContext);
                var dropDownlocator = locatorReader.GetByKey(page, dropDownLocatorKey);
                await WaitForAsync(scenarioContext, page, dropDownLocatorKey, 5, WaitForSelectorState.Visible);
                await dropDownlocator.ClickAsync();
                await dropDownlocator.PressSequentiallyAsync(optionText);
                var optionLocator = page.Locator($"//mat-option//span[normalize-space(text())='{optionText}']");
                await optionLocator.First.ClickAsync();
                Console.WriteLine($"[INFO] Successfully selected '{optionText}' from dropdown after typing.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to select option '{optionText}' from dropdown. Error: {ex.Message}");
                throw new CustomException($"[ERROR] Could not select option '{optionText}' from dropdown", ex);
            }
        }

        public static async Task FillAndPressEnterAsync(ScenarioContext scenarioContext, IPage page, string locatorKey, string text)
        {
            try
            {
                LocatorReader locatorReader = new LocatorReader(scenarioContext);
                var locator = locatorReader.GetByKey(page, locatorKey);

                Console.WriteLine($"[INFO] Filling '{locatorKey}' with '{text}' and pressing Enter...");

                await locator.FillAsync(text);
                await locator.PressAsync("Enter");

                Console.WriteLine($"[INFO] Successfully entered text '{text}' in '{locatorKey}' and pressed Enter.");
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException(
                    $"fill '{locatorKey}' with '{text}' and press Enter",
                    ex
                );
            }
        }

        public static async Task ScrollIntoViewIfNeededAsync(ScenarioContext scenarioContext, IPage page, string locatorKey)
        {
            try
            {
                LocatorReader locatorReader = new LocatorReader(scenarioContext);
                var locator = locatorReader.GetByKey(page, locatorKey);

                if (locator == null)
                    throw new ArgumentNullException(nameof(locator), $"Locator for key '{locatorKey}' was not found.");

                await locator.ScrollIntoViewIfNeededAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Action failed: Unable to scroll element '{locatorKey}' into view. Reason: {ex.Message}");
                throw new CustomException($"Action failed: Unable to scroll element '{locatorKey}' into view.", ex);
            }
        }

        public static async Task<bool> IsEnabledAsync(ScenarioContext scenarioContext, IPage page, string locatorKey)

        {
            try

            {
                LocatorReader locatorReader = new LocatorReader(scenarioContext);
                var locator = locatorReader.GetByKey(page, locatorKey);
                if (locator == null)
                {

                    throw new ArgumentNullException(nameof(locator), $"Locator for key '{locatorKey}' was not found.");
                }
                return await locator.IsEnabledAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Action failed: Unable to determine if element '{locatorKey}' is enabled. Reason: {ex.Message}");
                throw new CustomException($"Action failed: Unable to determine if element '{locatorKey}' is enabled.", ex);
            }
        }

        public static async Task FillFieldsFromTestDataAsync(IPage page, ScenarioContext scenarioContext, FeatureContext featureContext, string testCaseName, Dictionary<string, (string locatorKey, string controlType)> fieldMappings, string randomFieldKey = null, string randomContextKey = null)
        {
            foreach (var kvp in fieldMappings)
            {
                string fieldName = kvp.Key;
                var (locatorKey, controlType) = kvp.Value;

                string keyPath = $"testCases.{testCaseName}.{fieldName}";

                string fieldValue;
                try
                {
                    fieldValue = Utility.ReadDataFromJsonString(scenarioContext, keyPath);
                }
                catch (KeyNotFoundException)
                {
                    continue;
                }

                if (string.IsNullOrWhiteSpace(fieldValue))
                {
                    Console.WriteLine($"[INFO] Skipping '{fieldName}' as no value provided in test data.");
                    continue;
                }

                switch (controlType.ToLower())
                {
                    case "textbox":
                        if (!string.IsNullOrEmpty(randomFieldKey) &&
                            fieldName.Equals(randomFieldKey, StringComparison.OrdinalIgnoreCase))
                        {
                            string randomValue = $"{fieldValue}-{Guid.NewGuid().ToString().Substring(0, 4).ToUpper()}";
                            if (!string.IsNullOrEmpty(randomContextKey))
                            {
                                featureContext[randomContextKey] = randomValue;
                            }
                            await WebUIHelper.FillAsync(scenarioContext, page, locatorKey, randomValue);
                        }
                        else
                        {
                            await WebUIHelper.FillAsync(scenarioContext, page, locatorKey, fieldValue);
                        }
                        break;

                    case "checkbox":
                        await WebUIHelper.HandleCheckboxAsync(scenarioContext, page, locatorKey, fieldValue, fieldName);
                        break;

                    case "dropdown":
                        await WebUIHelper.SelectDropDownOptionAsync(scenarioContext, page, locatorKey, fieldValue);
                        break;

                    case "SearchTextBox":
                        await WebUIHelper.FillAndPressEnterAsync(scenarioContext, page, locatorKey, fieldValue);
                        break;

                    default:
                        Console.WriteLine($"[WARN] Unhandled control type '{controlType}' for field '{fieldName}'.");
                        break;
                }
            }
        }

        public static async Task JSClickAsync(ScenarioContext scenarioContext, IPage page, string locatorKey, string replaceString)
        {
            try
            {
                LocatorReader locatorReader = new LocatorReader(scenarioContext);
                var locator = locatorReader.GetByKey(page, locatorKey, replaceString);
                await locator.EvaluateAsync("element => element.click()");
                Logger.LogInfo($"Clicked on {locatorKey}");
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to click on {replaceString}");
                throw new CustomException($"Unable to click on element '{replaceString}'", ex);
            }
        }

        public static async Task<string> WaitForInputValueAsync(ScenarioContext scenarioContext, IPage page, string key, int timeoutMs = 30000)
        {
            var end = DateTime.Now.AddMilliseconds(timeoutMs);
            string value = "";

            while (DateTime.Now < end)
            {
                value = await WebUIHelper.GetInputValueAsync(scenarioContext, page, key);
                if (!string.IsNullOrEmpty(value))
                {
                    return value;
                }
                await Task.Delay(500);
            }

            throw new TimeoutException($"Input value for {key} did not populate within {timeoutMs}ms.");
        }

        public static Task WaitAsync(int milliseconds)
        {
            return Task.Delay(milliseconds);
        }

        private static readonly Random random = new Random();

        public static string GenerateRandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz";  // letters only
            Random random = new Random();
            char[] stringChars = new char[length];

            for (int i = 0; i < length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(stringChars);
        }

        public static async Task ClickAndPressSequenceAsync(ScenarioContext scenarioContext, IPage page, string locatorKey, string text, string? replaceString = null)
        {
            try
            {
                LocatorReader locatorReader = new LocatorReader(scenarioContext);
                var locator = locatorReader.GetByKey(page, locatorKey, replaceString);

                await locator.ClickAsync();
                await locator.FillAsync(string.Empty);

                foreach (char c in text)
                {
                    await locator.PressAsync(c.ToString());
                    await Task.Delay(50);
                }

                Logger.LogInfo($"[INFO] Successfully typed '{text}' into '{locatorKey}' using PressAsync sequence.");
            }
            catch (Exception ex)
            {
                Logger.LogError($"[ERROR] Failed to press sequence into '{locatorKey}'. Reason: {ex.Message}");
                throw new CustomException($"Unable to type text into element '{locatorKey}' using key sequence.", ex);
            }
        }
        public static string GenerateRandomNumericString(int length)
        {
            var random = new Random();
            var digits = new char[length];
            for (int i = 0; i < length; i++)
            {
                digits[i] = (char)('0' + random.Next(0, 10));
            }
            return new string(digits);
        }

        public static async Task ClickAsync(ScenarioContext scenarioContext, IPage page, string locatorKey, string replaceString)
        {
            try
            {
                LocatorReader locatorReader = new LocatorReader(scenarioContext);
                var locator = locatorReader.GetByKey(page, locatorKey, replaceString);
                await locator.ClickAsync();
                Logger.LogInfo($"Clicked on {locatorKey}");
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to click on {locatorKey}");
                throw new CustomException($"Unable to click on element '{locatorKey}'", ex);
            }
        }

        public static async Task JSClickAsync(ScenarioContext scenarioContext, IPage page, string locatorKey)
        {
            try
            {
                LocatorReader locatorReader = new LocatorReader(scenarioContext);
                var locator = locatorReader.GetByKey(page, locatorKey);
                await locator.EvaluateAsync("element => element.click()");
                Logger.LogInfo($"Clicked on {locatorKey}");
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to click on {locatorKey}");
                throw new CustomException($"Unable to click on element '{locatorKey}'", ex);
            }
        }

        public static async Task FillCharByCharAndPressEnterAsync(ScenarioContext scenarioContext, IPage page, string locatorKey, string text, int delayMs = 100) // delay between characters
        {
            try
            {
                var locatorReader = new LocatorReader(scenarioContext);
                var locator = locatorReader.GetByKey(page, locatorKey);

                Console.WriteLine($"[INFO] Typing text '{text}' sequentially into '{locatorKey}'...");

                // Focus on the input field
                await locator.ClickAsync();

                // Clear any existing text
                await locator.FillAsync(string.Empty);

                // Type each character sequentially (with optional delay)
                await locator.PressSequentiallyAsync(text, new LocatorPressSequentiallyOptions
                {
                    Delay = delayMs
                });

                // Press Enter after typing
                await locator.PressAsync("Enter");

                Console.WriteLine($"[INFO] Successfully typed '{text}' in '{locatorKey}' and pressed Enter.");
            }
            catch (Exception ex)
            {
                throw new CustomException($"Failed to type '{text}' character by character in '{locatorKey}' and press Enter", ex);
            }
        }
    }
}
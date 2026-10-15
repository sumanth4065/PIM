using Microsoft.Playwright;
using PIMQAAutomation.Utilities;
using TechTalk.SpecFlow;
using Utilities;
using static global::PIMQAAutomation.Modules.Items.Constants.AdminConstants;
namespace PIMQAAutomation.Modules.Items.Actions
{
   

    /// <summary>
    /// Contains business logic for Admin Product Class operations
    /// </summary>
    public class AdminClassActions
    {
        private readonly IPage page;
        private readonly ScenarioContext scenarioContext;
        private readonly FeatureContext featureContext;
        private const string ProductClass = "AdminProductClass";
        private string testCaseName = string.Empty;

        public AdminClassActions(IPage page, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            this.page = page;
            this.scenarioContext = scenarioContext;
            this.featureContext = featureContext;
        }


        /// <summary>
        /// Clicks a button in the Product Class screen
        /// </summary>
        /// <param name="buttonType">Type of button to click (e.g., Save, Cancel, New)</param>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task ClickProductClassButtonAsync(string buttonType)
        {
            try
            {
                string locatorKey = $"{ProductClass}.{buttonType}Button";
                await WebUIHelper.ClickAsync(scenarioContext, page, locatorKey);
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException($"click {buttonType} button", ex, testCaseName);
            }
        }



        /// <summary>
        /// Verifies that the Product Class page title is visible
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task VerifyProductClassTitleAsync()
        {
            try
            {
                string locatorKey = $"{ProductClass}.ClassHeader";
                await WebUIHelper.IsVisibleAsync(scenarioContext, page, locatorKey);
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("verify Admin Product class title", ex, testCaseName);
            }
        }

        /// <summary>
        /// Verifies error message for a specific field
        /// </summary>
        /// <param name="field">Field name to verify error for</param>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task VerifyProductClassFieldErrorAsync(string field)
        {
            try
            {
                string locatorKey;
                string expectedMessage;

                switch (field.ToLower())
                {
                    case "classname":
                        locatorKey = $"{ProductClass}.ClassNameErrorMessage";
                        expectedMessage = ProductClassConstants.ErrorMessages.ProductClassRequired;
                        break;

                    case "description":
                        locatorKey = $"{ProductClass}.DescriptionErrorMessage";
                        expectedMessage = ProductClassConstants.ErrorMessages.DescriptionRequired;
                        break;

                    case "internalnotes":
                        locatorKey = $"{ProductClass}.InternalNotesErrorMessage";
                        expectedMessage = ProductClassConstants.ErrorMessages.InternalNotesRequired;
                        break;

                    default:
                        throw new ArgumentException($"Unsupported field '{field}'");
                }

                ValidationHelper.FailIfNullOrEmpty($"Expected error message for '{field}'", expectedMessage, "Expected error message is required.");
                var actualMessage = await WebUIHelper.GetInnerTextAsync(scenarioContext, page, locatorKey);
                ValidationHelper.FailIfNullOrEmpty($"Actual error message for '{field}'", actualMessage, "Actual error message is missing.");
                ValidationHelper.AssertEqual($"Error message for '{field}'", expectedMessage.Trim(), actualMessage.Trim(), $"Locator: {locatorKey}");
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException($"verify error message for field '{field}'", ex, testCaseName);
            }
        }

        /// <summary>
        /// Verifies character limit error for a specific field
        /// </summary>
        /// <param name="field">Field name to verify character limit for</param>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task VerifyMaxCharLimitAsync(string field)
        {
            try
            {
                int charLimit;
                string inputLocatorKey;
                string errorLocatorKey;

                switch (field.ToLower())
                {
                    case "classname":
                        charLimit = ProductClassConstants.ClassCharLimit;
                        inputLocatorKey = $"{ProductClass}.ClassTextBox";
                        errorLocatorKey = $"{ProductClass}.ClassNameCharacterLimitError";
                        break;

                    case "description":
                        charLimit = ProductClassConstants.DescriptionCharLimit;
                        inputLocatorKey = $"{ProductClass}.DescriptionTextBox";
                        errorLocatorKey = $"{ProductClass}.DescriptionCharacterLimitError";
                        break;

                    default:
                        throw new ArgumentException($"Unsupported field '{field}'");
                }

                string longText = WebUIHelper.GenerateRandomString(charLimit);
                LocatorReader locatorReader = new LocatorReader(scenarioContext);
                var inputElement = locatorReader.GetByKey(page, inputLocatorKey);
                await inputElement.ClickAsync();
                await inputElement.FillAsync(longText);
                string actualMessage = await WebUIHelper.GetInnerTextAsync(scenarioContext, page, errorLocatorKey);
                string expectedMessage = ProductClassConstants.ErrorMessages.CharacterLimitExceeded;

                if (string.IsNullOrWhiteSpace(expectedMessage) || string.IsNullOrWhiteSpace(actualMessage))
                {
                    ValidationHelper.Fail($"Error message validation failed for '{field}'. Expected: '{expectedMessage ?? "null"}', Actual: '{actualMessage ?? "null"}'. Expected or Actual error message is missing.");
                }

                ValidationHelper.AssertEqual($"Error message for '{field}'", expectedMessage.Trim(), actualMessage.Trim(), $"Locator: {errorLocatorKey}");
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to enter text and verify error for field '{field}'. Reason: {ex.Message}");
                throw ExceptionHandler.BuildCustomException($"enter text and verify error for field '{field}'", ex, testCaseName);
            }
        }



        /// <summary>
        /// Enters Product Class details from test data
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task EnterProductClassDetailsAsync()
        {
            try
            {
                testCaseName = scenarioContext.ScenarioInfo.Title;

                var fieldMappings = GetClassFieldMappings();
                await WebUIHelper.FillFieldsFromTestDataAsync(page, scenarioContext, featureContext, testCaseName, fieldMappings, randomFieldKey: null, randomContextKey: null);
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("enter product class details", ex, testCaseName);
            }
        }

        /// <summary>
        /// Searches for and selects an existing Product Class
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task SearchExistingProductClassAsync()
        {
            try
            {
                string testCaseName = scenarioContext.ScenarioInfo.Title;
                string inputValue = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.ProductClass");
                const string locatorKey = $"{ProductClass}.ClassTextBox";

                Console.WriteLine($"[INFO] Searching and selecting existing Product Class: '{inputValue}'");

                await WebUIHelper.ClickAsync(scenarioContext, page, locatorKey);
                await WebUIHelper.ClickAndPressSequenceAsync(scenarioContext, page, locatorKey, inputValue);
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("search and select existing Product Class", ex, testCaseName);
            }
        }

        /// <summary>
        /// Enters data into the Internal Notes field
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task EnterDataIntoInternalNotesAsync()
        {
            try
            {
                string testCaseName = scenarioContext.ScenarioInfo.Title;
                string inputValue = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.Internal Notes");
                const string locatorKey = $"{ProductClass}.InternalNotesTextBox";

                Console.WriteLine($"[INFO] Entering Internal Notes: '{inputValue}'");

                await WebUIHelper.ClickAsync(scenarioContext, page, locatorKey);
                LocatorReader locatorReader = new LocatorReader(scenarioContext);
                var element = locatorReader.GetByKey(page, locatorKey);
                await element.FillAsync(string.Empty);
                await WebUIHelper.FillAsync(scenarioContext, page, locatorKey, inputValue);
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("enter Internal Notes", ex, testCaseName);
            }
        }



        /// <summary>
        /// Unchecks the Active checkbox
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task UncheckActiveCheckboxAsync()
        {
            try
            {
                const string locatorKey = $"{ProductClass}.ActiveCheckBox";
                await WebUIHelper.UncheckAsync(scenarioContext, page, locatorKey);
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("uncheck 'Active' checkbox", ex, testCaseName);
            }
        }

        /// <summary>
        /// Checks the Active checkbox
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task CheckActiveCheckboxAsync()
        {
            try
            {
                const string locatorKey = $"{ProductClass}.ActiveCheckBox";
                await WebUIHelper.CheckAsync(scenarioContext, page, locatorKey);
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("check 'Active' checkbox", ex, testCaseName);
            }
        }



        /// <summary>
        /// Returns field mappings for Product Class
        /// </summary>
        private Dictionary<string, (string locatorKey, string controlType)> GetClassFieldMappings()
        {
            var fieldMappings = new Dictionary<string, (string locatorKey, string controlType)>
    {
    { "ClassTextBox", ($"{ProductClass}.ClassTextBox", "textbox") },
    { "DescriptionTextBox", ($"{ProductClass}.DescriptionTextBox", "textbox") },
    { "InternalNotesTextBox", ($"{ProductClass}.InternalNotesTextBox", "textbox") },
    { "ActiveCheckBox", ($"{ProductClass}.ActiveCheckBox", "checkbox") }
    };
            return fieldMappings;
        }
    }
}

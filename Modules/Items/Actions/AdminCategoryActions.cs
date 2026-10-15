using Microsoft.Playwright;
using PIMQAAutomation.Utilities;
using TechTalk.SpecFlow;
using Utilities;
using static PIMQAAutomation.Modules.Items.Constants.AdminConstants;

namespace PIMQAAutomation.Modules.Items.Actions
{
    /// <summary>
    /// Contains business logic for Admin Product Category operations
    /// </summary>
    public class AdminCategoryActions
    {
        private readonly IPage page;
        private readonly ScenarioContext scenarioContext;
        private readonly FeatureContext featureContext;
        private const string ProductCategory = "AdminProductCategory";
        private string testCaseName = string.Empty;

        public AdminCategoryActions(IPage page, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            this.page = page;
            this.scenarioContext = scenarioContext;
            this.featureContext = featureContext;
        }

        /// <summary>
        /// Clicks a button in the Product Category screen
        /// </summary>
        /// <param name="buttonType">Type of button to click (e.g., Save, Cancel, New)</param>
        public async Task ClickProductCategoryButtonAsync(string buttonType)
        {
            try
            {
                string locatorKey = $"{ProductCategory}.{buttonType}Button";
                await WebUIHelper.ClickAsync(scenarioContext, page, locatorKey);
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException($"click '{buttonType}' button in Admin Product Category", ex, testCaseName);
            }
        }

        /// <summary>
        /// Verifies that the Product Category page title is visible
        /// </summary>
        public async Task VerifyProductCategoryTitleAsync()
        {
            try
            {
                string locatorKey = $"{ProductCategory}.CategoryHeader";
                await WebUIHelper.IsVisibleAsync(scenarioContext, page, locatorKey);
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("verify Admin Product Category title", ex, testCaseName);
            }
        }

        /// <summary>
        /// Verifies error message for a specific field
        /// </summary>
        /// <param name="field">Field name to verify error for</param>
        public async Task VerifyProductCategoryFieldErrorAsync(string field)
        {
            try
            {
                string locatorKey;
                string expectedMessage;

                switch (field.ToLower())
                {
                    case "categoryname":
                        locatorKey = $"{ProductCategory}.CategoryNameErrorMessage";
                        expectedMessage = ProductCategoryConstants.ErrorMessages.ProductCategoryRequired;
                        break;

                    case "description":
                        locatorKey = $"{ProductCategory}.DescriptionErrorMessage";
                        expectedMessage = ProductCategoryConstants.ErrorMessages.DescriptionRequired;
                        break;

                    case "internalnotes":
                        locatorKey = $"{ProductCategory}.InternalNotesErrorMessage";
                        expectedMessage = ProductCategoryConstants.ErrorMessages.InternalNotesRequired;
                        break;

                    default:
                        throw new ArgumentException($"Unsupported field '{field}'");
                }

                var actualMessage = await WebUIHelper.GetInnerTextAsync(scenarioContext, page, locatorKey);

                if (string.IsNullOrWhiteSpace(expectedMessage) || string.IsNullOrWhiteSpace(actualMessage))
                {
                    ValidationHelper.Fail($"Error message validation failed for '{field}'. Expected: '{expectedMessage ?? "null"}', Actual: '{actualMessage ?? "null"}'. Expected or Actual error message is missing.");
                }

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
        public async Task VerifyMaxCharLimitAsync(string field)
        {
            try
            {
                int charLimit;
                string inputLocatorKey;
                string errorLocatorKey;

                switch (field.ToLower())
                {
                    case "categoryname":
                        charLimit = ProductCategoryConstants.CategoryCharLimit;
                        inputLocatorKey = $"{ProductCategory}.CategoryTextBox";
                        errorLocatorKey = $"{ProductCategory}.CategoryNameCharacterLimitError";
                        break;

                    case "description":
                        charLimit = ProductCategoryConstants.DescriptionCharLimit;
                        inputLocatorKey = $"{ProductCategory}.DescriptionTextBox";
                        errorLocatorKey = $"{ProductCategory}.DescriptionCharacterLimitError";
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
                string expectedMessage = ProductCategoryConstants.ErrorMessages.CharacterLimitExceeded;

                ValidationHelper.FailIfNullOrEmpty($"Expected error message for '{field}'", expectedMessage, "Expected error message is required.");
                ValidationHelper.FailIfNullOrEmpty($"Actual error message for '{field}'", actualMessage, "Actual error message is missing.");
                ValidationHelper.AssertEqual($"Error message for '{field}'", expectedMessage.Trim(), actualMessage.Trim(), $"Locator: {errorLocatorKey}");
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException($"enter text and verify error for field '{field}'", ex, testCaseName);
            }
        }

        /// <summary>
        /// Enters Product Category details from test data
        /// </summary>
        public async Task EnterProductCategoryDetailsAsync()
        {
            try
            {
                testCaseName = scenarioContext.ScenarioInfo.Title;

                var fieldMappings = GetCategoryFieldMappings();
                await WebUIHelper.FillFieldsFromTestDataAsync(page, scenarioContext, featureContext, testCaseName, fieldMappings, randomFieldKey: null, randomContextKey: null);
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("create Product Category", ex, testCaseName);
            }
        }

        /// <summary>
        /// Searches for and selects an existing Product Category
        /// </summary>
        public async Task SearchExistingProductCategoryAsync()
        {
            try
            {
                string testCaseName = scenarioContext.ScenarioInfo.Title;
                string inputValue = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.ProductClass");
                const string locatorKey = $"{ProductCategory}.CategoryTextBox";

                Console.WriteLine($"[INFO] Searching and selecting existing Product Category: '{inputValue}'");

                await WebUIHelper.ClickAsync(scenarioContext, page, locatorKey);
                await WebUIHelper.ClickAndPressSequenceAsync(scenarioContext, page, locatorKey, inputValue);
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("search and select existing Product Category", ex, testCaseName);
            }
        }

        /// <summary>
        /// Enters data into the Internal Notes field
        /// </summary>
        public async Task EnterDataIntoInternalNotesAsync()
        {
            try
            {
                string testCaseName = scenarioContext.ScenarioInfo.Title;
                string inputValue = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.Internal Notes");
                const string locatorKey = $"{ProductCategory}.InternalNotesTextBox";

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
        public async Task UncheckActiveCheckboxAsync()
        {
            try
            {
                const string locatorKey = $"{ProductCategory}.ActiveCheckBox";
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
        public async Task CheckActiveCheckboxAsync()
        {
            try
            {
                const string locatorKey = $"{ProductCategory}.ActiveCheckBox";
                await WebUIHelper.CheckAsync(scenarioContext, page, locatorKey);
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("check 'Active' checkbox", ex, testCaseName);
            }
        }

        /// <summary>
        /// Returns field mappings for Product Category
        /// </summary>
        private Dictionary<string, (string locatorKey, string controlType)> GetCategoryFieldMappings()
        {
            var fieldMappings = new Dictionary<string, (string locatorKey, string controlType)>
            {
                { "CategoryTextBox", ($"{ProductCategory}.CategoryTextBox", "textbox") },
                { "DescriptionTextBox", ($"{ProductCategory}.DescriptionTextBox", "textbox") },
                { "InternalNotesTextBox", ($"{ProductCategory}.InternalNotesTextBox", "textbox") },
                { "ActiveCheckBox", ($"{ProductCategory}.ActiveCheckBox", "checkbox") }
            };
            return fieldMappings;
        }
    }
}

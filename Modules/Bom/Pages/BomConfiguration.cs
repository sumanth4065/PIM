using Microsoft.Playwright;
using PIMQAAutomation.Modules.Bom.Constants;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Utilities;
using TechTalk.SpecFlow;
using Utilities;

namespace PIMQAAutomation.Modules.Bom.Pages
{
    public class BomConfiguration
    {
        private const string BomStyle = "BomConfiguration";
        private readonly IPage page;
        private readonly ScenarioContext scenarioContext;
        private readonly FeatureContext featureContext;
        private string testCaseName;


        public BomConfiguration(IPage page, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            this.page = page;
            this.scenarioContext = scenarioContext;
            this.featureContext = featureContext;
        }

        public async Task VerifyBomConfigurationTitle()
        {
            try
            {
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{BomStyle}.ConfigurationHeader", TimeConstants.MinimumWait, WaitForSelectorState.Visible);
                await WebUIHelper.IsVisibleAsync(scenarioContext, page, $"{BomStyle}.ConfigurationHeader");
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("verify BOM Configuration title", ex, testCaseName);
            }
        }

        public async Task ClickNewKindButton()
        {
            try
            {
                await WebUIHelper.ClickAsync(scenarioContext, page, $"{BomStyle}.NewKindButton");
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("click 'New Kind' button in BOM Configuration", ex, testCaseName);
            }
        }


        public async Task ClickBomButton(string buttonName)
        {
            try
            {
                string locatorKey = $"{BomStyle}.{buttonName}Button";
                await WebUIHelper.ClickAsync(scenarioContext, page, locatorKey);
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException($"click '{buttonName}' button in BOM Configuration", ex, testCaseName);
            }
        }


        public async Task SelectBomStyle(string name)
        {
            try
            {
                string locatorKey = $"{BomStyle}.{name}Option";
                await WebUIHelper.ClickAsync(scenarioContext, page, locatorKey);
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException($"select BOM Style option '{name}'", ex, testCaseName);
            }
        }

        public async Task EnterBomConfigurationDetails()
        {
            try
            {
                testCaseName = scenarioContext.ScenarioInfo.Title;

                await WebUIHelper.FillFieldsFromTestDataAsync(page, scenarioContext, featureContext, testCaseName, GetBomFieldMappings);
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("Enter BOM Configuration Details", ex, testCaseName);
            }
        }
        private readonly Dictionary<string, (string locatorKey, string controlType)> GetBomFieldMappings = new()
        {
            { "KindName", ($"{BomStyle}.KindName", "textbox") },
            { "Sequencing", ($"{BomStyle}.Sequencing", "textbox") },
            { "KindRank", ($"{BomStyle}.KindRank", "textbox") },
            { "Description", ($"{BomStyle}.Description", "textbox") },
            { "InternalNotes", ($"{BomStyle}.InternalNotes", "textarea") }
        };

        public async Task VerifyBomFieldError(string field)
        {
            try
            {
                if (!FieldErrorMappings.TryGetValue(field, out var config))
                {
                    throw new ArgumentException($"Unsupported field '{field}'");
                }

                string locatorKey = config.locatorKey;
                string expectedMessage = config.expectedMessage;

                var actualMessage = await WebUIHelper.GetInnerTextAsync(scenarioContext, page, locatorKey);

                if (string.IsNullOrWhiteSpace(expectedMessage) || string.IsNullOrWhiteSpace(actualMessage))
                {
                    ValidationHelper.Fail($"Error message validation failed for '{field}'. Expected: '{expectedMessage ?? "null"}', Actual: '{actualMessage ?? "null"}'. Expected or Actual error message is missing.");
                }

                ValidationHelper.AssertEqual($"Error message for '{field}'", expectedMessage.Trim(), actualMessage.Trim(), $"Locator: {locatorKey}");
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException($"Verify error message for field '{field}'", ex, testCaseName);
            }
        }

        private static readonly Dictionary<string, (string locatorKey, string expectedMessage)> FieldErrorMappings =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "kindname", ($"{BomStyle}.KindNameError", AdminConstants.ErrorMessages.KindNameRequired) },
            { "description", ($"{BomStyle}.DescriptionError", AdminConstants.ErrorMessages.DescriptionRequired) },
            { "sequencing", ($"{BomStyle}.SequencingError", AdminConstants.ErrorMessages.SequencingDigitExceeded) },
            { "kindrank", ($"{BomStyle}.KindRankError", AdminConstants.ErrorMessages.SequencingDigitExceeded) }
        };


        public async Task ValidateCreateModify()
        {
            try
            {
                string modifyKey = $"{BomStyle}.ModifiedLabel";
                string createKey = $"{BomStyle}.CreatedLabel";
                LocatorReader locatorReader = new LocatorReader(scenarioContext);
                var modifyLocator = locatorReader.GetByKey(page, modifyKey);
                var createLocator = locatorReader.GetByKey(page, createKey);
                await modifyLocator.ScrollIntoViewIfNeededAsync();
                await createLocator.ScrollIntoViewIfNeededAsync();
                bool isModifyVisible = await modifyLocator.IsVisibleAsync();
                bool isCreateVisible = await createLocator.IsVisibleAsync();
                ValidationHelper.AssertEqual("Modified label visibility", true, isModifyVisible, $"Element not visible: {modifyKey}");
                ValidationHelper.AssertEqual("Created label visibility", true, isCreateVisible, $"Element not visible: {createKey}");
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("Validate 'Created' and 'Modified' labels visibility", ex, testCaseName);
            }
        }

        public async Task ValidateCharLimit(string field)
        {
            try
            {
                if (!FieldValidationMappings.TryGetValue(field, out var config))
                {
                    throw new ArgumentException($"Unsupported field '{field}'");
                }

                string inputLocator = config.inputLocator;
                string errorLocator = config.errorLocator;
                int charLimit = config.charLimit;
                string expectedMessage = config.expectedMessage;

                string longText = WebUIHelper.GenerateRandomString(charLimit);
                LocatorReader locatorReader = new LocatorReader(scenarioContext);
                await locatorReader.GetByKey(page, inputLocator).FillAsync(longText);
                var actualMessage = await WebUIHelper.GetInnerTextAsync(scenarioContext, page, errorLocator);

                ValidationHelper.FailIfNullOrEmpty($"Expected error message for '{field}'", expectedMessage, "Expected error message is required.");
                ValidationHelper.FailIfNullOrEmpty($"Actual error message for '{field}'", actualMessage, "Actual error message is missing.");
                ValidationHelper.AssertEqual($"Error message for '{field}'", expectedMessage.Trim(), actualMessage.Trim(), $"Locator: {errorLocator}");
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException($"validate char limit error for field '{field}'", ex, testCaseName);
            }
        }

        private static readonly Dictionary<string, (string inputLocator, string errorLocator, int charLimit, string expectedMessage)> FieldValidationMappings =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "kindname", ($"{BomStyle}.KindName", $"{BomStyle}.kindNameCharError", AdminConstants.KindNameCharLimit, AdminConstants.ErrorMessages.KindNameCharLimitExceeded) },
            { "description", ($"{BomStyle}.Description", $"{BomStyle}.DescriptionCharError", AdminConstants.DescriptionCharLimit, AdminConstants.ErrorMessages.DescriptionCharLimitExceeded) },
            { "sequencing", ($"{BomStyle}.Sequencing", $"{BomStyle}.SequencingNumberOnlyError", AdminConstants.SequencingKindRankCharLimit, AdminConstants.ErrorMessages.SequencingOnlyNumbers) },
            { "kindrank", ($"{BomStyle}.KindRank", $"{BomStyle}.KindRankNumberOnlyError", AdminConstants.SequencingKindRankCharLimit, AdminConstants.ErrorMessages.KindRankOnlyNumbers) }
        };

        public async Task ValidateNumberFieldCharLimit(string field)
        {
            try
            {
                if (!NumberFieldCharLimitMappings.TryGetValue(field, out var config))
                {
                    throw new ArgumentException($"Unsupported field '{field}' for numeric validation");
                }
                string inputLocator = config.inputLocator;
                string errorLocator = config.errorLocator;
                string expectedMessage = config.expectedMessage;
                int charLimit = AdminConstants.SequencingKindRankCharLimit;

                string longNumber = WebUIHelper.GenerateRandomNumericString(charLimit);
                LocatorReader locatorReader = new LocatorReader(scenarioContext);
                await locatorReader.GetByKey(page, inputLocator).FillAsync(longNumber);
                var actualMessage = await WebUIHelper.GetInnerTextAsync(scenarioContext, page, errorLocator);

                if (string.IsNullOrWhiteSpace(expectedMessage) || string.IsNullOrWhiteSpace(actualMessage))
                {
                    ValidationHelper.Fail($"Error message validation failed for '{field}'. Expected: '{expectedMessage ?? "null"}', Actual: '{actualMessage ?? "null"}'. Expected or Actual error message is missing.");
                }

                ValidationHelper.AssertEqual($"Error message for '{field}'", expectedMessage.Trim(), actualMessage.Trim(), $"Locator: {errorLocator}");
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException($"Validate numeric char limit error for field '{field}'", ex, testCaseName);
            }
        }

        private static readonly Dictionary<string, (string inputLocator, string errorLocator, string expectedMessage)> NumberFieldCharLimitMappings =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "kindrank", ($"{BomStyle}.KindRank", $"{BomStyle}.KindRankDigitError", AdminConstants.ErrorMessages.kindRankDigitExceed) },
            { "sequencing", ($"{BomStyle}.Sequencing", $"{BomStyle}.SequencingDigitError", AdminConstants.ErrorMessages.SequencingDigitExceeded) }
        };


        public async Task ClickConfirmationButton(string buttonName)
        {
            try
            {
                string locatorKey = $"{BomStyle}.{buttonName}Button";
                await WebUIHelper.ClickAsync(scenarioContext, page, locatorKey);
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException($"click '{buttonName}' button on confirmation modal", ex, testCaseName);
            }
        }
    }
}

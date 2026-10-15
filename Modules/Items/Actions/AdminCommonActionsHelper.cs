namespace PIMQAAutomation.Modules.Items.Actions
{
    using global::PIMQAAutomation.Modules.Common.Constants;
    using global::Utilities;
    using Microsoft.Playwright;
    using TechTalk.SpecFlow;
    using Utilities;

    /// <summary>
    /// Static helper class for common Admin validation operations
    /// </summary>
    public static class AdminCommonActionsHelper
    {
        public static async Task ValidateErrorMessageAsync(ScenarioContext scenarioContext, IPage page, string errorLocator, string expectedError)
        {
            try
            {
                // Wait for the error element to be visible
                await WebUIHelper.WaitForAsync(scenarioContext, page, errorLocator, TimeConstants.MinimumWait, WaitForSelectorState.Visible);
                if (errorLocator == null)
                {
                    throw new Exception($"Error element '{errorLocator}' not found on page.");
                }

                LocatorReader locatorReader = new LocatorReader(scenarioContext);
                var element = locatorReader.GetByKey(page, errorLocator);
                var actualError = (await element.InnerTextAsync()).Trim();
                ValidationHelper.AssertEqual(errorLocator, expectedError, actualError, $"Validation failed. Expected: '{expectedError}', Actual: '{actualError}'");
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Error message validation failed for locator '{errorLocator}'", ex);
            }
        }
    }
}

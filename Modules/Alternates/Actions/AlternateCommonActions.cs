namespace PIMQAAutomation.Modules.Alternates.Actions
{
    using Microsoft.Playwright;
    using NUnit.Framework;
    using PIMQAAutomation.Modules.Alternates.Constants;
    using PIMQAAutomation.Modules.Common.Constants;
    using PIMQAAutomation.Modules.Items.Constants;
    using PIMQAAutomation.Utilities;
    using System.Text.Json;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Contains common helper methods and shared business logic used across all Alternate action classes
    /// </summary>
    public class AlternateCommonActions
    {
        private readonly IPage page;
        private readonly ScenarioContext scenarioContext;
        private readonly FeatureContext featureContext;

        public AlternateCommonActions(IPage page, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            this.page = page;
            this.scenarioContext = scenarioContext;
            this.featureContext = featureContext;
        }

        #region Context Management

        /// <summary>
        /// Retrieves a field value from feature context using context key and field key
        /// </summary>
        /// <param name="contextKey">The key to look up in feature context</param>
        /// <param name="fieldKey">The field key within the dictionary</param>
        /// <returns>The field value as string</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the key is not found</exception>
        public string GetFieldValue(string contextKey, string fieldKey)
        {
            if (featureContext[contextKey] is Dictionary<string, string> fieldData &&
     fieldData.ContainsKey(fieldKey))
            {
                return fieldData[fieldKey];
            }

            throw new KeyNotFoundException($"Field '{fieldKey}' not found for key '{contextKey}' in FeatureContext.");
        }

        /// <summary>
        /// Sets a field value in feature context
        /// </summary>
        /// <param name="contextKey">The key to store in feature context</param>
        /// <param name="fieldKey">The field key within the dictionary</param>
        /// <param name="fieldValue">The value to set</param>
        public void SetFieldValue(string contextKey, string fieldKey, string fieldValue)
        {
            Dictionary<string, string> fieldData;

            if (!featureContext.ContainsKey(contextKey) ||
             featureContext[contextKey] is not Dictionary<string, string>)
            {
                fieldData = new Dictionary<string, string>();
            }
            else
            {
                fieldData = (Dictionary<string, string>)featureContext[contextKey];
            }

            fieldData[fieldKey] = fieldValue;
            featureContext[contextKey] = fieldData;
        }

        /// <summary>
        /// Saves item details from JSON test data to feature context
        /// </summary>
        /// <param name="item">The item key to save</param>
        public async Task SaveItemDetailsAsync(string item)
        {
            await page.WaitForLoadStateAsync();

            var testCaseName = scenarioContext.ScenarioInfo.Title;
            var itemDetails = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.{item}");
            var itemObject = JsonSerializer.Deserialize<Dictionary<string, string>>(itemDetails);
            itemObject[ItemConstants.ItemId] = featureContext["itemIdValue"] as string;
            featureContext[item] = itemObject;
        }

        /// <summary>
        /// Loads cluster item from JSON test data to feature context
        /// </summary>
        /// <param name="item">The item key to load</param>
        public async Task LoadClusterItemAsync(string item)
        {
            await page.WaitForLoadStateAsync();

            var testCaseName = scenarioContext.ScenarioInfo.Title;
            var itemDetails = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.{item}");
            var itemObject = JsonSerializer.Deserialize<Dictionary<string, string>>(itemDetails);
            featureContext[item] = itemObject;
        }

        #endregion

        #region Validation Helpers

        /// <summary>
        /// Checks if IDH validation is enabled in test settings
        /// </summary>
        /// <returns>True if IDH validation is enabled or not configured</returns>
        public bool IsIdhValidationEnabled()
        {
            return string.IsNullOrEmpty(TestContext.Parameters[RunSettingsConstants.EnableIDHVerification]) ||
                   Convert.ToBoolean(TestContext.Parameters[RunSettingsConstants.EnableIDHVerification]);
        }

        #endregion

        #region Timing and Triggers

        /// <summary>
        /// Waits for trigger time if the trigger wait flag is set
        /// Automatically sets the flag to false after waiting to prevent duplicate waits
        /// </summary>
        public async Task WaitIfTriggerToWaitAsync()
        {
            if (!scenarioContext.TryGetValue("IsTriggerToWait", out object? value) || (value is bool b && b))
            {
                await page.WaitForTimeoutAsync(AlternatesConstants.AlternatePartPoolTriggerTimeMs);

                // Set the flag to false to avoid re-waiting
                scenarioContext["IsTriggerToWait"] = false;
            }
        }

        /// <summary>
        /// Sets the trigger wait flag to false (trigger wait not required)
        /// </summary>
        public void SetTriggerWaitNotRequired()
        {
            scenarioContext["IsTriggerToWait"] = false;
        }

        /// <summary>
        /// Sets the trigger wait flag to true (trigger wait required)
        /// </summary>
        public void SetTriggerWaitRequired()
        {
            scenarioContext["IsTriggerToWait"] = true;
        }

        #endregion
    }
}

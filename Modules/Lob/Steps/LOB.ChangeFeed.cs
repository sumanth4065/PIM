using PIMQAAutomation.Helpers;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Modules.Lob.Constants;
using PIMQAAutomation.Utilities;
using PPT.ItemMaster.Domain.Features.ChangeFeeds;
using TechTalk.SpecFlow;

namespace PIMQAAutomation.Modules.Lob.Steps
{
    public partial class LOB
    {
        [Then(@"User waits for status to change to ""(.*)"" in ChangeFeeds container of Pim database")]
        public async Task ThenUserWaitsForStatusToChangeAsync(string expectedStatus)
        {
            var itemId = string.Empty;
            try
            {
                const int maxRetryCount = LOBConstants.TimeConstants.MaxRetryAttempts;
                const int delayInSeconds = LOBConstants.TimeConstants.RetryDelaySeconds;
                itemId = ContextHelper.GetContextValue(featureContext, LOBConstants.FeatureContextValues.ItemId);
                for (int attempt = 1; attempt <= maxRetryCount; attempt++)
                {
                    var document = await cosmosDb.GetDataFromCosmosDB<ChangeFeed>(DatabaseConstants.DatabaseName, DatabaseConstants.ChangeFeedContainer, CosmosDBQueries.GetDataUsingPartitionKey(itemId));
                    string currentStatus = document.Status.ToString();
                    ValidationHelper.FailIfNullOrEmpty("Status", currentStatus, $"Status cannot be null or empty. Current: {currentStatus}");
                    if (string.Equals(currentStatus, expectedStatus, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"Status is '{expectedStatus}' at attempt {attempt}.");
                        return;
                    }
                    Console.WriteLine($"Attempt {attempt}: Current status = '{currentStatus ?? "null"}'. Waiting for '{expectedStatus}'");
                    await Task.Delay(TimeSpan.FromSeconds(delayInSeconds));
                }
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"Status did not change to '{expectedStatus}' within 8 minutes for Item Id {itemId}", e);
            }
        }
    }
}
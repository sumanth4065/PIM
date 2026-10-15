using PIMQAAutomation.Helpers.DocumentFetcher;

namespace PIMQAAutomation.Helpers.EventAudit
{
    /// <summary>
    /// Fetches EventAudit documents from CosmosDB based on the given ValidationContext.
    /// </summary>
    public class EventAuditDocumentFetcher : IDocumentFetcher
    {
        public async Task<T?> GetDocumentAsync<T>(ValidationContext context) where T : class
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context), "ValidationContext cannot be null.");
            }

            try
            {
                return await EventAuditDataFetcherService.GetEventAuditDocumentAsync<T>(context);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                $"Failed to fetch IdhDocumentFetcher  document for type {typeof(T).Name} with context {context}.", ex);
            }
        }
    }
}
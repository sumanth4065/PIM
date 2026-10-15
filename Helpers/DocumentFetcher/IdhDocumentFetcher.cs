using PIMQAAutomation.Helpers.IDH;

namespace PIMQAAutomation.Helpers.DocumentFetcher
{
    /// <summary>
    /// IdhDocumentFetcher is responsible for fetching documents from the IDH (Information Data Hub) service.
    /// Implements the IDocumentFetcher interface for generic document retrieval based on the provided ValidationContext.
    /// </summary>
    public class IdhDocumentFetcher : IDocumentFetcher
    {
        public async Task<T?> GetDocumentAsync<T>(ValidationContext context) where T : class
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context), "ValidationContext cannot be null.");
            }

            try
            {
                return await IDHDataFetcherService.GetIdhDocumentAsync<T>(context);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                $"Failed to fetch IdhDocumentFetcher  document for type {typeof(T).Name} with context {context}.", ex);
            }
        }
    }
}

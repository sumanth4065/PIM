using PIMQAAutomation.Helpers.EventAudit;

namespace PIMQAAutomation.Helpers.DocumentFetcher
{
    /// <summary>
    /// Provides a single entry point to fetch documents from different sources
    /// (EventAudit, IDH, etc.) based on the document type <T>.
    /// </summary>
    public static class DocumentFetcherFactory
    {
        // Registered fetchers for each source
        private static readonly Dictionary<DocumentSource, IDocumentFetcher> Fetchers = new()
        {
            { DocumentSource.EventAudit, new EventAuditDocumentFetcher() },
            { DocumentSource.IDH, new IdhDocumentFetcher() }
        };

        /// <summary>
        /// Fetches a document of type <T> from the correct source.
        /// Source is resolved automatically using SourceRules.
        /// </summary>
        /// <typeparam name="T">Document type (Item, ItemCatalog, Bom, etc.).</typeparam>
        /// <param name="context">ValidationContext with fetch parameters.</param>
        /// <returns>Fetched document of type <T>, or null if not found.</returns>
        public static async Task<T?> FetchDocumentAsync<T>(ValidationContext context) where T : class
        {
            try
            {
                // Find which source handles this document type
                var source = SourceRules.GetSourceForType<T>();

                // Delegate to the correct fetcher
                return await Fetchers[source].GetDocumentAsync<T>(context);
            }
            catch (KeyNotFoundException ex)
            {
                throw new InvalidOperationException(
                    $"No fetcher registered for type {typeof(T).Name}.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(
                    $"Failed to fetch document for type {typeof(T).Name}.", ex);
            }
        }
    }
}
namespace PIMQAAutomation.Helpers.DocumentFetcher
{
    /// <summary>
    /// Represents the possible sources from which a document can be fetched.
    /// Used by the DocumentFetcherFactory to resolve the correct fetcher.
    /// </summary>
    public enum DocumentSource
    {
        EventAudit,
        IDH
    }
}
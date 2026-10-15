namespace PIMQAAutomation.Helpers.DocumentFetcher
{
    public interface IDocumentFetcher
    {
        Task<T?> GetDocumentAsync<T>(ValidationContext context) where T : class;
    }
}
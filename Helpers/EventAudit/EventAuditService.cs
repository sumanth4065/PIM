using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Utilities;

namespace PIMQAAutomation.Helpers.EventAudit
{
    /// <summary>
    /// Service to fetch documents from the EventAudit Cosmos DB container.
    /// </summary>
    public static class EventAuditService
    {
        // Database and container are class-level readonly fields
        private static readonly string _database = DatabaseConstants.IntegrationIngestionDatabaseName;

        private static readonly string _container = DatabaseConstants.EventAuditContainer;

        /// <summary>
        /// Executes the given query against the EventAudit Cosmos DB container.
        /// </summary>
        /// <typeparam name="T">The type of document expected.</typeparam>
        /// <param name="query">The Cosmos DB SQL query string.</param>
        /// <returns>The requested document of type <typeparamref name="T"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if query is null/empty.</exception>
        public static async Task<T?> GetEventAuditDocumentAsync<T>(string query) where T : class
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentNullException(nameof(query), "Query cannot be null or empty.");

            try
            {
                return await CosmosDbHelper.GetAndInspectDataFromCosmosDB<T>(
                    _database,
                    _container,
                    query
                );
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(
                    $"Unexpected error in GetEventAuditDocumentAsync<{typeof(T).Name}>: {ex.Message}"
                );
                throw;
            }
        }
    }
}
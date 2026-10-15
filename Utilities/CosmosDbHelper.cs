using Microsoft.Azure.Cosmos;
using System.Text.Json;
using NUnit.Framework;
using PIMQAAutomation.Support;
using PPT.Integration.CommonDataModel.Models.Bases;
using PPT.Integration.DataHub.Clients;
using PIMQAAutomation.Modules.Common.Constants;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace PIMQAAutomation.Utilities
{
    public class CosmosDbHelper
    {
        public static async Task<T> GetDataFromIDHClient<T>(string id, string partitionKey)
            where T : IStandardModel
        {
            var client = DependencyInjection.GetService<IIntegrationDataHubClient>();

            var result = await client.Documents.GetByIdOrThrow<T>(partitionKey, id);
            return result;
        }

        public static async Task<T> GetDataFromIDHClient<T>(string entity, string sourceKeyName, string sourceKeyValue)
            where T : IStandardModel
        {
            var client = DependencyInjection.GetService<IIntegrationDataHubClient>();

            var result = await client.Documents.GetSingleBySource<T>(entity, ApplicationConstants.PimApplicationName, sourceKeyName, sourceKeyValue);
            return result;
        }

        public async Task<T> GetDataFromCosmosDB<T>(string databaseName, string containerName, string query)
        {
            if (string.IsNullOrWhiteSpace(databaseName))
                throw new ArgumentException("Database name cannot be null or empty.", nameof(databaseName));

            if (string.IsNullOrWhiteSpace(containerName))
                throw new ArgumentException("Container name cannot be null or empty.", nameof(containerName));

            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentException("Query cannot be null or empty.", nameof(query));

            try
            {
                var decryptedConnectionString = SecureDataHelper.DecryptWithMachineKey(
                    TestContext.Parameters[RunSettingsConstants.DbConnectionString]);

                var dbClient = await DBClient.GetInstanceAsync(decryptedConnectionString);
                CosmosClient cosmosClient = dbClient.Client;

                Container container = cosmosClient
                    .GetDatabase(databaseName.ToLowerInvariant())
                    .GetContainer(containerName);

                QueryDefinition queryDefinition = new QueryDefinition(query);
                using FeedIterator<T> resultSet = container.GetItemQueryIterator<T>(queryDefinition);

                while (resultSet.HasMoreResults)
                {
                    FeedResponse<T> response = await resultSet.ReadNextAsync();
                    foreach (var item in response)
                    {
                        return item; // Return the first item found
                    }
                }

                return default; // Return null/default if nothing is found
            }
            catch (CosmosException ex)
            {
                Console.Error.WriteLine($"Cosmos DB error: {ex.StatusCode} - {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<T> GetDataFromIDHCosmosDB<T>(string databaseName, string containerName, string query)
        {
            if (string.IsNullOrWhiteSpace(databaseName))
                throw new ArgumentException("Database name cannot be null or empty.", nameof(databaseName));

            if (string.IsNullOrWhiteSpace(containerName))
                throw new ArgumentException("Container name cannot be null or empty.", nameof(containerName));

            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentException("Query cannot be null or empty.", nameof(query));

            try
            {
                var idhConnectionString = TestContext.Parameters[RunSettingsConstants.IDHConnectionString];
                var decryptedConnection = SecureDataHelper.DecryptWithMachineKey(idhConnectionString);

                var dbClient = await DBClient.GetIdhInstanceAsync(decryptedConnection);
                CosmosClient cosmosClient = dbClient.Client;

                Container container = cosmosClient
                    .GetDatabase(databaseName.ToLowerInvariant())
                    .GetContainer(containerName);

                QueryDefinition queryDefinition = new QueryDefinition(query);
                using FeedIterator<T> resultSet = container.GetItemQueryIterator<T>(queryDefinition);

                while (resultSet.HasMoreResults)
                {
                    FeedResponse<T> response = await resultSet.ReadNextAsync();
                    foreach (var item in response)
                    {
                        return item; // Return first deserialized item
                    }
                }

                return default; // Return null/default if no items found
            }
            catch (CosmosException ex)
            {
                Console.Error.WriteLine($"Cosmos DB error: {ex.StatusCode} - {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
        public static async Task<T> GetAndInspectDataFromCosmosDB<T>(string databaseName, string containerName, string query)
        {
            if (string.IsNullOrWhiteSpace(databaseName))
                throw new ArgumentException("Database name cannot be null or empty.", nameof(databaseName));

            if (string.IsNullOrWhiteSpace(containerName))
                throw new ArgumentException("Container name cannot be null or empty.", nameof(containerName));

            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentException("Query cannot be null or empty.", nameof(query));

            try
            {
                var decryptedConnectionString = SecureDataHelper.DecryptWithMachineKey(
                    TestContext.Parameters[RunSettingsConstants.DbConnectionString]);

                var dbClient = await DBClient.GetInstanceAsync(decryptedConnectionString);
                CosmosClient cosmosClient = dbClient.Client;

                Container container = cosmosClient
                    .GetDatabase(databaseName.ToLowerInvariant())
                    .GetContainer(containerName);

                QueryDefinition queryDefinition = new QueryDefinition(query);
                using FeedIterator<JObject> resultSet = container.GetItemQueryIterator<JObject>(queryDefinition);

                while (resultSet.HasMoreResults)
                {
                    FeedResponse<JObject> response = await resultSet.ReadNextAsync();
                    foreach (var rawJsonObj in response)
                    {
                        //  Log the entire document JSON
                        string rawJson = rawJsonObj.ToString(Formatting.Indented);

                        try
                        {
                            if (rawJsonObj["body"] == null)
                            {
                                throw new InvalidOperationException("Document is missing 'body' property.");
                            }
                            // Attempt generic deserialization
                            T result = rawJsonObj["body"].ToObject<T>();
                            return result;
                        }
                        catch (Exception ex)
                        {
                            throw new InvalidOperationException($"Failed to deserialize document to {typeof(T).Name}", ex);
                        }
                    }
                }

                return default;
            }
            catch (CosmosException ex)
            {
                Console.Error.WriteLine($"Cosmos DB error: {ex.StatusCode} - {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
    }
}
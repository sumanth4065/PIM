using Microsoft.Azure.Cosmos;

namespace PIMQAAutomation.Support
{
    public sealed class DBClient : IAsyncDisposable
    {
        private static readonly SemaphoreSlim _initLock = new SemaphoreSlim(1, 1);
        private static DBClient _instance;
        private static DBClient _idhInstance;
        private CosmosClient _cosmosClient;

        private DBClient()
        { }

        public static async Task<DBClient> GetInstanceAsync(string connectionString)
        {
            if (_instance == null)
            {
                await _initLock.WaitAsync();
                try
                {
                    if (_instance == null)
                    {
                        _instance = new DBClient();
                        await _instance.InitializeAsync(connectionString);
                    }
                }
                finally
                {
                    _initLock.Release();
                }
            }

            return _instance;
        }

        public static async Task<DBClient> GetIdhInstanceAsync(string connectionString)
        {
            if (_idhInstance == null)
            {
                await _initLock.WaitAsync();
                try
                {
                    if (_idhInstance == null)
                    {
                        _idhInstance = new DBClient();
                        await _idhInstance.InitializeAsync(connectionString);
                    }
                }
                finally
                {
                    _initLock.Release();
                }
            }

            return _idhInstance;
        }
        private async Task InitializeAsync(string connectionString)
        {
            _cosmosClient = new CosmosClient(connectionString);
            Console.WriteLine("Cosmos DB client initialized.");

            // Optional: Create database/container
            // await _cosmosClient.CreateDatabaseIfNotExistsAsync("MyDatabase");
        }

        //public static DBClient Instance => _instance ?? throw new InvalidOperationException("DBClient is not initialized. Call GetInstanceAsync() first.");
        public CosmosClient Client => _cosmosClient ?? throw new InvalidOperationException("Cosmos client is not initialized.");

        public static async Task DisposeInstanceAsync()
        {
            if (_instance != null)
            {
                await _instance.DisposeAsync();
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_cosmosClient != null)
            {
                _cosmosClient.Dispose();
                _cosmosClient = null;
                _instance = null;
                Console.WriteLine("Cosmos DB client disposed.");
                await Task.CompletedTask;
            }
        }
    }
}
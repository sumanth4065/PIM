using NUnit.Framework;
using PIMQAAutomation.Support;
using PIMQAAutomation.Utilities;

namespace PIMQAAutomation.Libraries

{
    public sealed class DeleteApi
    {
        private static readonly SemaphoreSlim InitLock = new SemaphoreSlim(1, 1);
        private static DeleteApi _instance;

        private readonly HttpClient _httpClient;
        

        // Private constructor
        private DeleteApi()
        {
            _httpClient = new HttpClient();
           
        }
        // Async-safe singleton accessor
        public static async Task<DeleteApi> GetInstanceAsync()
        {
            if (_instance is null)
            {
                await InitLock.WaitAsync();
                try
                {
                    if (_instance is null)
                    {
                        _instance = new DeleteApi();
                    }
                }
                finally
                {
                    InitLock.Release();
                }
            }
            return _instance;
        }

        // Method to send DELETE request to the API and delete the record from Cosmos DB
        public async Task DeleteAssetAsync(string accessToken, string deleteApiuri, string assetId)
        {
            try
            {
                var apiUrl = $"{deleteApiuri}={assetId}";
                var request = new HttpRequestMessage(HttpMethod.Delete, apiUrl);
                request.Headers.Add("Authorization", $"Bearer {accessToken}");

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Asset successfully deleted.");
                }
                else
                {
                    Console.WriteLine($"Failed to delete asset. Status Code: {response.StatusCode}");
                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Response Content: {responseContent}");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail($"Error during DELETE request: {ex.Message}");
            }
        }
        public async Task DeleteAutomationData(string accessToken, string deleteApiuri)
        {
            try
            {
                Console.WriteLine($"Calling API : {deleteApiuri}");
                var apiUrl = deleteApiuri;
                var request = new HttpRequestMessage(HttpMethod.Delete, apiUrl);
                request.Headers.Add("Authorization", $"Bearer {accessToken}");
                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Data successfully deleted.");
                }
                else
                {
                    Console.WriteLine($"Failed to delete test data. Status Code: {response.StatusCode}");
                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Response Content: {responseContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during DELETE request: {ex.Message}");
                throw;
            }
        }
    }
}
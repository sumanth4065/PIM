
using NUnit.Framework;
using PIMQAAutomation.Libraries;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Utilities;


namespace PIMQAAutomation.Helpers
{
    public static class ApiService
    {
        public static async Task<bool> CleanAutomationData(string accessToken)
        {
            try
            {

                string baseUri = SecureDataHelper.DecryptWithMachineKey(TestContext.Parameters[RunSettingsConstants.DeleteApiUri]);
                string deleteOemEndpoint = Endpoints.CleanBOMByOemEndpoint;
                string automationCleanDataEndpoint = Endpoints.AutomationCleanDataEndpoint;
                string deleteOemUri = $"{baseUri}{deleteOemEndpoint}";
                string automationCleanDataUri = $"{baseUri}{automationCleanDataEndpoint}";

                if (!string.IsNullOrEmpty(accessToken))
                {
                    var deleteApi = await DeleteApi.GetInstanceAsync();
                    await deleteApi.DeleteAutomationData(accessToken, deleteOemUri);
                    await deleteApi.DeleteAutomationData(accessToken, automationCleanDataUri);

                    Console.WriteLine("Deleted data from Cosmos DB");
                    return true;
                }
                else
                {
                    Console.WriteLine("Access Token is empty , skipping delete operation.");
                    return false;
                }


            }
            catch (Exception e)
            {
                Console.WriteLine($"CleanAutomationData API function failed with error {e.Message}");
                return false;
            }
        }
    }
}

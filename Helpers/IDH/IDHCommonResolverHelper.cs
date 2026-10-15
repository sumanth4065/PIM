
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Utilities;
using PPT.Integration.CommonDataModel.Models.Bases;

namespace PIMQAAutomation.Helpers.IDH
{
    public static class IDHCommonResolverHelper
    {
        public static async Task<T?> ResolveUsingReference<T>(
            string id,
            string partitionKey,
            string entityName,
            string fallbackId) 
            where T : class, IStandardModel
        {
            return await CosmosDbHelper.GetDataFromIDHClient<T>(id, partitionKey)
                   ?? await ResolveUsingPoolId<T>(entityName, fallbackId);
        }

        public static async Task<T?> ResolveUsingPoolId<T>(
            string entityName, string id) 
            where T : class, IStandardModel
        {
            return await CosmosDbHelper.GetDataFromIDHClient<T>(
                entityName,
                KeyNameConstants.Id,
                id);
        }
    }

}

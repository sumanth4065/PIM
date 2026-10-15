using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Modules.Items.Constants;
using PIMQAAutomation.Utilities;

namespace PIMQAAutomation.Helpers.IDH.Strategies
{
    public class ServicesFetchStrategy : IIdhFetchStrategy
    {
        public async Task<object?> FetchAsync(Dictionary<string, string> info)
        {
            if (!info.TryGetValue(ItemConstants.ItemId, out var itemId) || string.IsNullOrWhiteSpace(itemId))
                throw new ArgumentException("Missing or empty 'Item-Id' in strategy input.");

            var cosmosDb = new CosmosDbHelper();
            var query = CosmosDBQueries.GetDataUsingNameOrSerialNo(itemId);

            var servicesDoc = await cosmosDb.GetDataFromCosmosDB<PPT.ItemMaster.Domain.Features.LineOfBusiness.LineOfBusiness>(
                DatabaseConstants.DatabaseName,
                DatabaseConstants.LineOfBusinessContainer,
                query);
            var serviceDocId = servicesDoc.Id.ToString();
            if (servicesDoc == null || string.IsNullOrWhiteSpace(serviceDocId))
                throw new InvalidOperationException($"No document found in Cosmos DB for itemId: '{itemId}'");

            return await IDHCommonResolverHelper.ResolveUsingPoolId<PPT.Integration.CommonDataModel.Models.LineOfBusinesses.LineOfBusiness>(
                EntityConstants.LineOfBusinesses,
                serviceDocId);
        }
    }
}
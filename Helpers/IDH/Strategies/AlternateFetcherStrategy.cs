using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Modules.Items.Constants;
using PIMQAAutomation.Utilities;
using PPT.Integration.CommonDataModel.Models.Alternates;
using PPT.ItemMaster.Data.Cosmos.Features.AlternatePartPools;

namespace PIMQAAutomation.Helpers.IDH.Strategies
{
    public class AlternateFetcherStrategy : IIdhFetchStrategy
    {
        public async Task<object?> FetchAsync(Dictionary<string, string> info)
        {
            if (!info.TryGetValue(ItemConstants.ItemId, out var itemId))
                throw new ArgumentException("Missing 'Item-Id'");

            var cosmosDb = new CosmosDbHelper();
            var query = CosmosDBQueries.GetItemPoolQuery(itemId);
            var altDoc = await cosmosDb.GetDataFromCosmosDB<AlternatePartPoolRecord>(
                DatabaseConstants.DatabaseName,
                DatabaseConstants.AlternatePartPoolsContainer,
                query);

            return await IDHCommonResolverHelper.ResolveUsingPoolId<Alternate>(
                EntityConstants.AlternatePartPool,
                altDoc?.Id.ToString());
        }
    }
}

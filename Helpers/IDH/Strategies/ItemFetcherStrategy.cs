using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Modules.Items.Constants;
using PIMQAAutomation.Utilities;
using PPT.Integration.CommonDataModel.Models.Items;
using PPT.ItemMaster.Data.Cosmos.Features.ItemPools;

namespace PIMQAAutomation.Helpers.IDH.Strategies
{
    public class ItemFetcherStrategy : IIdhFetchStrategy
    {
        public async Task<object?> FetchAsync(Dictionary<string, string> info)
        {
            if (!info.TryGetValue(ItemConstants.ItemId, out var itemId))
                throw new ArgumentException("Missing 'Item-Id' in additionalInfo.");

            var cosmosDb = new CosmosDbHelper();
            var query = CosmosDBQueries.GetItemPoolQuery(itemId);

            var itemPoolData = await cosmosDb.GetDataFromCosmosDB<ItemPoolRecord>(
                DatabaseConstants.DatabaseName,
                DatabaseConstants.ItemPoolsContainer,
                query);

            if (itemPoolData?.IdhReferences?.ItemReference?.Id != null &&
                !string.IsNullOrWhiteSpace(itemPoolData.IdhReferences.ItemReference.PartitionKey))
            {
                return await IDHCommonResolverHelper.ResolveUsingReference<Item>(
                    itemPoolData.IdhReferences.ItemReference.Id.ToString(),
                    itemPoolData.IdhReferences.ItemReference.PartitionKey,
                    EntityConstants.ItemPartPool,
                    itemPoolData.Id.ToString());
            }

            return await IDHCommonResolverHelper.ResolveUsingPoolId<Item>(
                EntityConstants.ItemPartPool,
                itemPoolData?.Id.ToString());
        }
    }
}

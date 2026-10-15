using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Modules.Items.Constants;
using PIMQAAutomation.Utilities;
using PPT.Integration.CommonDataModel.Models.ItemCatalogs;
using PPT.ItemMaster.Data.Cosmos.Features.ItemPools;

namespace PIMQAAutomation.Helpers.IDH.Strategies
{
    public class ItemCatalogFetcherStrategy : IIdhFetchStrategy
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

            if (itemPoolData?.IdhReferences?.ItemCatalogReference?.Id != null &&
                !string.IsNullOrWhiteSpace(itemPoolData.IdhReferences.ItemCatalogReference.PartitionKey))
            {
                return await IDHCommonResolverHelper.ResolveUsingReference<ItemCatalog>(
                    itemPoolData.IdhReferences.ItemCatalogReference.Id.ToString(),
                    itemPoolData.IdhReferences.ItemCatalogReference.PartitionKey,
                    EntityConstants.ItemPartPool,
                    itemPoolData.Id.ToString());
            }

            return await IDHCommonResolverHelper.ResolveUsingPoolId<ItemCatalog>(
                EntityConstants.ItemPartPool,
                itemPoolData?.Id.ToString());
        }
    }
}

using PIMQAAutomation.Modules.Bom.Constants;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Utilities;
using PPT.Integration.CommonDataModel.Models.Boms;
using PPT.ItemMaster.Data.Cosmos.Features.BestBomPools;

namespace PIMQAAutomation.Helpers.IDH.Strategies
{
    public class BomFetcherStrategy : IIdhFetchStrategy
    {
        public async Task<object?> FetchAsync(Dictionary<string, string> info)
        {
            if (!info.TryGetValue(KeyNameConstants.PartitionKey, out var partitionKey))
                throw new ArgumentException("Missing 'PartitionKey'");

            if (!info.TryGetValue(KeyNameConstants.Name, out var name))
                throw new ArgumentException("Missing 'Name'");

            var cosmosDb = new CosmosDbHelper();
            var query = CosmosDBQueries.GetDataUsingPartitionKey(partitionKey);
            var bestBomData = await cosmosDb.GetDataFromCosmosDB<BestBomPoolRecord>(
                DatabaseConstants.DatabaseName,
                DatabaseConstants.BestBOMPoolsContainer,
                query);

            if (bestBomData?.IdhReferences != null)
            {
                var references = bestBomData.IdhReferences;

                if (name.Equals(BomConstants.ActualBom, StringComparison.OrdinalIgnoreCase) &&
                    references.ActualBom?.Id != null &&
                    !string.IsNullOrWhiteSpace(references.ActualBom.PartitionKey))
                {
                    return await IDHCommonResolverHelper.ResolveUsingReference<Bom>(
                        references.ActualBom.Id.ToString(),
                        references.ActualBom.PartitionKey,
                        EntityConstants.BestBomPool,
                        bestBomData.Id.ToString());
                }

                if (name.Equals(BomConstants.PlanningBom, StringComparison.OrdinalIgnoreCase) &&
                    references.PlanningBom?.Id != null &&
                    !string.IsNullOrWhiteSpace(references.PlanningBom.PartitionKey))
                {
                    return await IDHCommonResolverHelper.ResolveUsingReference<Bom>(
                        references.PlanningBom.Id.ToString(),
                        references.PlanningBom.PartitionKey,
                        EntityConstants.BestBomPool,
                        bestBomData.Id.ToString());
                }
            }

            return await IDHCommonResolverHelper.ResolveUsingPoolId<Bom>(
                EntityConstants.BestBomPool,
                bestBomData?.Id.ToString());
        }
    }
}

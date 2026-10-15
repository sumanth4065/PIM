using PIMQAAutomation.Helpers.DocumentFetcher;
using PIMQAAutomation.Helpers.IDH.Strategies;
using PPT.Integration.CommonDataModel.Models.Alternates;
using PPT.Integration.CommonDataModel.Models.Boms;
using PPT.Integration.CommonDataModel.Models.ItemCatalogs;

namespace PIMQAAutomation.Helpers.IDH
{
    public static class IDHDataFetcherService
    {
        private static readonly Dictionary<Type, IIdhFetchStrategy> StrategyMap = new()
        {
            { typeof(PPT.Integration.CommonDataModel.Models.Items.Item), new ItemFetcherStrategy() },
            { typeof(ItemCatalog), new ItemCatalogFetcherStrategy() },
            { typeof(Alternate), new AlternateFetcherStrategy() },
            { typeof(Bom), new BomFetcherStrategy() },
            { typeof(PPT.Integration.CommonDataModel.Models.LineOfBusinesses.LineOfBusiness), new ServicesFetchStrategy() }
        };

        public static async Task<T?> GetIdhDocumentAsync<T>(ValidationContext context) where T : class
        {
            Dictionary<string, string> additionalInfo;
            var type = typeof(T);
            if (type.ToString().Contains("Bom"))
                additionalInfo = IDHInfoBuilder.BuildBomInfo(context.PartitionKey, context.DocType);
            else
                additionalInfo = IDHInfoBuilder.BuildItemInfo(context.ItemId);

            if (!StrategyMap.TryGetValue(type, out var strategy))
                throw new NotSupportedException($"Unsupported IDH document type: {type.Name}");

            var result = await strategy.FetchAsync(additionalInfo);
            return result as T;
        }

        public static async Task<T?> GetIdhDocumentAsync<T>(Dictionary<string, string> additionalInfo) where T : class
        {
            var type = typeof(T);
            if (!StrategyMap.TryGetValue(type, out var strategy))
                throw new NotSupportedException($"Unsupported IDH document type: {type.Name}");

            var result = await strategy.FetchAsync(additionalInfo);
            return result as T;
        }
    }
}
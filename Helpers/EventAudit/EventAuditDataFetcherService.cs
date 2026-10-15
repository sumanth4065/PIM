using PIMQAAutomation.Helpers.DocumentFetcher;
using PIMQAAutomation.Helpers.EventAudit.Strategies;
using PPT.Integration.CommonDataModel.Models.Alternates;
using PPT.Integration.CommonDataModel.Models.Boms;
using PPT.Integration.CommonDataModel.Models.ItemCatalogs;
using PPT.Integration.CommonDataModel.Models.LineOfBusinesses;

namespace PIMQAAutomation.Helpers.EventAudit
{
    public class EventAuditDataFetcherService
    {
        // Maps each document type to its corresponding EventAudit fetch strategy
        private static readonly Dictionary<Type, IEventAuditFetchStrategy> StrategyMap = new()
        {
            { typeof(PPT.Integration.CommonDataModel.Models.Items.Item), new ItemEventAuditStrategy() },
            { typeof(ItemCatalog), new ItemEventAuditStrategy() },
            { typeof(Alternate), new AlternateEventAuditStrategy() },
            { typeof(Bom), new BomEventAuditStrategy() },
            { typeof(LineOfBusiness), new LobEventAuditStrategy() }
        };

        // Fetches the EventAudit document for the given type using the appropriate strategy
        public static async Task<T?> GetEventAuditDocumentAsync<T>(ValidationContext context) where T : class
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var type = typeof(T);

            if (!StrategyMap.TryGetValue(type, out var strategy))
                throw new NotSupportedException($"No EventAudit strategy defined for {type.Name}.");

            try
            {
                var result = await strategy.FetchAsync(context);
                return result as T;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"Failed to fetch EventAudit document for type {type.Name} (ItemId={context.ItemId}).", ex);
            }
        }
    }
}
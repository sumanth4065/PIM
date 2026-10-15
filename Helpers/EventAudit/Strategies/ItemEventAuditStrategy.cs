using PIMQAAutomation.Helpers.DocumentFetcher;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Modules.Items.Constants;
using PPT.Integration.CommonDataModel.Models.ItemCatalogs;

namespace PIMQAAutomation.Helpers.EventAudit.Strategies
{
    public class ItemEventAuditStrategy : IEventAuditFetchStrategy
    {
        private const string ItemEventAuditQuery = $"SELECT * FROM c WHERE (c.body.name = '[ItemID]' or c.body.item.name = '[ItemID]') AND CONTAINS(c.standardEventName, 'item.') AND c.type = 'hub' ORDER BY c._ts DESC";
        private const string ItemCatalogEventAuditQuery = $"SELECT * FROM c WHERE (c.body.name = '[ItemID]' or c.body.item.name = '[ItemID]') AND CONTAINS(c.standardEventName, 'itemCatalog.') AND c.type = 'hub' ORDER BY c._ts DESC";
        public async Task<object?> FetchAsync(ValidationContext context)
        {
            if (string.IsNullOrWhiteSpace(context.ItemId))
                throw new ArgumentNullException(nameof(context.ItemId));

            string query = context.DocType.Contains(ItemConstants.ItemCatalog, StringComparison.OrdinalIgnoreCase)
                ? GetItemCatalogEventAuditQuery(context.ItemId)
                : GetItemEventAuditQuery(context.ItemId);
            if(context.DocType.Contains(ItemConstants.ItemCatalog))
                return await EventAuditService.GetEventAuditDocumentAsync<ItemCatalog>(query);
            else
                return await EventAuditService.GetEventAuditDocumentAsync<PPT.Integration.CommonDataModel.Models.Items.Item>(query);

        }
        private static string GetItemEventAuditQuery(string ItemId)
        {
            return ItemEventAuditQuery.Replace("[ItemID]", ItemId);
        }
        private static string GetItemCatalogEventAuditQuery(string ItemId)
        {
            return ItemCatalogEventAuditQuery.Replace("[ItemID]", ItemId);
        }
    }
}
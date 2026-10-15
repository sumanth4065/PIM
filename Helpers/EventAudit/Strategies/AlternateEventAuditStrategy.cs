using PIMQAAutomation.Helpers.DocumentFetcher;
using PIMQAAutomation.Modules.Common.Constants;
using PPT.Integration.CommonDataModel.Models.Alternates;

namespace PIMQAAutomation.Helpers.EventAudit.Strategies
{
    public class AlternateEventAuditStrategy : IEventAuditFetchStrategy
    {
        private const string AlternateEventAuditQuery = $"SELECT * FROM c WHERE c.body.item.sku = '[ItemID]' AND CONTAINS(c.standardEventName, 'alternate') AND c.type = 'hub' ORDER BY c._ts DESC";
        public async Task<object?> FetchAsync(ValidationContext context)
        {
            if (string.IsNullOrWhiteSpace(context.ItemId))
                throw new ArgumentNullException(nameof(context.ItemId));

            string query = GetAlternateEventAuditQuery(context.ItemId);
            return await EventAuditService.GetEventAuditDocumentAsync<Alternate>(query);
        }
        private static string GetAlternateEventAuditQuery(string ItemId)
        {
            return AlternateEventAuditQuery.Replace("[ItemID]", ItemId);
        }

    }
}
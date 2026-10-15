using PIMQAAutomation.Helpers.DocumentFetcher;
using PIMQAAutomation.Modules.Common.Constants;
using PPT.Integration.CommonDataModel.Models.LineOfBusinesses;

namespace PIMQAAutomation.Helpers.EventAudit.Strategies
{
    public class LobEventAuditStrategy : IEventAuditFetchStrategy
    {
        private const string LobEventAuditQuery = $"SELECT * FROM c WHERE c.body.name ='[ItemID]' and c.body.type='Line Of Business' and c.type='hub' ORDER BY c._ts DESC";
        public async Task<object?> FetchAsync(ValidationContext context)
        {
            if (string.IsNullOrWhiteSpace(context.ItemId))
                throw new ArgumentNullException(nameof(context.ItemId));

            string query = GetLobEventAuditQuery(context.ItemId);
            return await EventAuditService.GetEventAuditDocumentAsync<LineOfBusiness>(query);
        }
        private static string GetLobEventAuditQuery(string ItemId)
        {
            return LobEventAuditQuery.Replace("[ItemID]", ItemId);
        }
    }
}
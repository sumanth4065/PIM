using PIMQAAutomation.Helpers.DocumentFetcher;
using PIMQAAutomation.Modules.Common.Constants;
using PPT.Integration.CommonDataModel.Models.Boms;

namespace PIMQAAutomation.Helpers.EventAudit.Strategies
{
    public class BomEventAuditStrategy : IEventAuditFetchStrategy
    {
        private const string BomEventAuditQuery = $"SELECT * FROM c WHERE c.partitionKey ='[PartitionKey]' and CONTAINS(c.standardEventName, '[Type]') AND c.type = 'hub' ORDER BY c._ts DESC";
        public async Task<object?> FetchAsync(ValidationContext context)
        {
            if (string.IsNullOrWhiteSpace(context.PartitionKey) || string.IsNullOrWhiteSpace(context.DocType))
                throw new ArgumentException("PartitionKey and DocType are required for BOM queries.");

            string query = GetBomEventAuditQuery(context.PartitionKey, context.DocType);
            return await EventAuditService.GetEventAuditDocumentAsync<Bom>(query);
        }

        private static string GetBomEventAuditQuery(string PartitionKey, string BomType)
        {
            string noSpace = BomType.Replace(" ", "");
            string result = char.ToLower(noSpace[0]) + noSpace.Substring(1);
            return BomEventAuditQuery.Replace("[PartitionKey]", PartitionKey).Replace("[Type]", result);
        }
    }
}
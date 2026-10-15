namespace PIMQAAutomation.Modules.Common.Constants
{
    public static class CosmosDBQueries
    {
        private const string ContextNameQuery = $"SELECT * FROM c WHERE c.context.name = '[TEXT]' ORDER BY c._ts DESC";
        private const string PartitionKeyQuery = $"SELECT * FROM c WHERE c.partitionKey = '[TEXT]' ORDER BY c._ts DESC";
        private const string PlanningOrActualBOMQuery = $"SELECT * FROM c WHERE c.partitionKey='[TEXT1]'AND c.name='[TEXT2]' ORDER BY c._ts DESC";
        private const string IdQuery = $"SELECT * FROM c WHERE c.id ='[TEXT]'ORDER BY c._ts DESC";
        private const string NameOrSerialNoQuery = $"SELECT * FROM c WHERE c.name = '[TEXT]' ORDER BY c._ts DESC";
        private const string ItemIdAndDocumentTypeQuery = $"SELECT * FROM c WHERE c.item.sku = '[TEXT1]' and c.type='[TEXT2]'";
        private const string ItemIdLOBAndDocumentTypeQuery = $"SELECT * FROM c WHERE c.partitionKey = '[TEXT1]' and c.lineOfBusiness.name='[TEXT2]' and c.type='[TEXT3]'";
        private const string OemAndGroupNameQuery = $"SELECT * FROM c WHERE c.oem.name ='[TEXT1]' and c.taxonomy['group'].name='[TEXT2]'";
        private const string AlternateClusterQuery = $"SELECT * FROM c WHERE c.item.sku ='[TEXT1]'AND c.cluster.type ='[TEXT2]' ORDER BY c._ts DESC";
        private const string AlternateClusterReplacementData = $"SELECT * FROM c WHERE c.partitionKey = '[TEXT1]' AND c.type = '[TEXT2]'";
        private const string ItemPoolQuery = $"SELECT * FROM c WHERE c.item.sku ='[TEXT1]'";
        private const string ItemIDHQuery = $"SELECT * FROM c WHERE c.name='[TEXT1]' and c.type = '[TEXT2]'";
        private const string AssetPartProductPimBomQuery = $"SELECT * FROM c WHERE c.context.name='[TEXT1]' and c.partitionKey = '[TEXT2]'";
        private const string EventAuditQuery = $"SELECT * FROM c WHERE c.body.name ='[TEXT]' and c.body.type='Line Of Business' and c.type='standard' ORDER BY c._ts DESC";
        private const string StanardEventQuery = $"SELECT * FROM c WHERE c.body.name ='[TEXT1]' and c.standardEventName='[TEXT2]' and c.type='standard' ORDER BY c._ts DESC";
        private const string ItemIdAndDocumentTypeQueryMultipleOEMs = $"SELECT * FROM c WHERE (c.item.sku = '[TEXT1]' AND c.item.oem.name = '[TEXT2]')";
        public static string GetDataUsingContextName(string contextName)
        {
            return ContextNameQuery.Replace("[TEXT]", contextName);
        }

        public static string GetDataUsingPartitionKey(string partitionKey)
        {
            return PartitionKeyQuery.Replace("[TEXT]", partitionKey);
        }

        public static string GetPlanningOrActualBOM(string id, string actualOrPlanningBOMName)
        {
            return PlanningOrActualBOMQuery.Replace("[TEXT1]", id).Replace("[TEXT2]", actualOrPlanningBOMName);
        }

        public static string GetId(string documentId)
        {
            return IdQuery.Replace("[TEXT]", documentId);
        }

        public static string GetDataUsingNameOrSerialNo(string name)
        {
            return NameOrSerialNoQuery.Replace("[TEXT]", name);
        }

        public static string GetDataUsingItemIdAndDocumentType(string itemId, string documentType)
        {
            return ItemIdAndDocumentTypeQuery.Replace("[TEXT1]", itemId).Replace("[TEXT2]", documentType);
        }

        public static string GetDataUsingItemIdAndDocumentTypeMultipleOems(string itemId, string oem)
        {
            return ItemIdAndDocumentTypeQueryMultipleOEMs.Replace("[TEXT1]", itemId).Replace("[TEXT2]", oem);
        }

        public static string GetDataUsingOemAndGroupName(string oemName, string groupName)
        {
            return OemAndGroupNameQuery.Replace("[TEXT1]", oemName).Replace("[TEXT2]", groupName);
        }

        public static string GetDataUsingItemIdLOBAndDocumentType(string itemId, string lob, string documentType)
        {
            return ItemIdLOBAndDocumentTypeQuery.Replace("[TEXT1]", itemId).Replace("[TEXT2]", lob).Replace("[TEXT3]", documentType);
        }

        public static string GetAlternateClusterData(string itemId, string clusterType)
        {
            return AlternateClusterQuery.Replace("[TEXT1]", itemId).Replace("[TEXT2]", clusterType);
        }

        public static string GetReplacementClusterData(string partitionKey, string documentType)
        {
            return AlternateClusterReplacementData.Replace("[TEXT1]", partitionKey).Replace("[TEXT2]", documentType);
        }

        public static string GetItemPoolQuery(string itemId)
        {
            return ItemPoolQuery.Replace("[TEXT1]", itemId);
        }

        public static string GetItemIDHQuery(string itemId, string type)
        {
            return ItemIDHQuery.Replace("[TEXT1]", itemId).Replace("[TEXT2]", type);
        }

        public static string GetPimBomDataUsingPartitionKey(string contextName, string partitionKey)
        {
            return AssetPartProductPimBomQuery.Replace("[TEXT1]", contextName).Replace("[TEXT2]", partitionKey);
        }

        public static string GetDataUsingBodyName(string ItemId)
        {
            return EventAuditQuery.Replace("[TEXT]", ItemId);
        }

        public static string GetStanardEventQuery(string itemId, string eventname)
        {
            return StanardEventQuery.Replace("[TEXT1]", itemId).Replace("[TEXT2]", eventname);
        }
    }
}
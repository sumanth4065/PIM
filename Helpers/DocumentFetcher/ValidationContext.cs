namespace PIMQAAutomation.Helpers.DocumentFetcher
{
    public class ValidationContext
    {
        public string ItemId { get; set; }
        public string PartitionKey { get; set; }
        public string DocType { get; set; }

        public ValidationContext()
        { }

        public ValidationContext(string itemId)
        {
            ItemId = itemId;
        }

        public ValidationContext(string partitionKey, string docType)
        {
            PartitionKey = partitionKey;
            DocType = docType;
            ItemId = partitionKey;
        }
    }
}
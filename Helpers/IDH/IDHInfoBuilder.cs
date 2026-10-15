using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Modules.Items.Constants;

namespace PIMQAAutomation.Helpers.IDH
{
    public static class IDHInfoBuilder
    {
        public static Dictionary<string, string> BuildBomInfo(string partitionKey, string bomType)
        {
            return new Dictionary<string, string>
        {
            { KeyNameConstants.PartitionKey, partitionKey },
            { KeyNameConstants.Name, bomType }
        };
        }
        public static Dictionary<string, string> BuildItemInfo(string itemId)
        {
            return new Dictionary<string, string>
        {
            { ItemConstants.ItemId, itemId }

        };
        }
    }
}

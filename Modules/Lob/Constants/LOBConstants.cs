namespace PIMQAAutomation.Modules.Lob.Constants
{
    public class LOBConstants
    {
        public const int MinPriorityRank = 1000;
        public const int MaxPriorityRank = 10000;

        public static readonly Dictionary<string, string> StandardEventNames = new(StringComparer.OrdinalIgnoreCase)
        {
            { "LineOfBusinessUpdated", "lineOfBusiness.updated" },
            { "LineOfBusinessActivated", "lineOfBusiness.activated" }
        };

        public static class ServiceTaxonomyFields
        {
            public const string ServiceClass = "Class";
            public const string ServiceDomain = "Domain";
            public const string ServiceItem = "Item";
            public const string ServiceType = "Type";
        }

        public static class GeneralLedgerAccounts
        {
            public const string BilledAccountCode = "Billed-Account-Code";
            public const string BilledDivisionCode = "Billed-Division-Code";
            public const string UnbilledAccountCode = "Unbilled-Account-Code";
            public const string BilledAccountCodeName = "Billed-Account-Code-Name";
            public const string BilledDivisionCodeName = "Billed-Division-Code-Name";
            public const string UnbilledAccountCodeName = "Unbilled-Account-Code-Name";
        }

        public static readonly Dictionary<string, string> ErrorPopUpMessages = new(StringComparer.OrdinalIgnoreCase)
        {
            { "MissingServiceTaxonomyDetails", "Service Taxonomy details are missing for the selected Line of Business. Please update the item service taxonomy details before proceeding." },
            { "DuplicatePriorityRank", "An attempt is being made to Create or Update this Record and another Record already exists with the same Priority Rank" }
        };

        public static class LocatorProperties
        {
            public const string LovToGl = "lob-to-gl";
            public const string BilledAccountName = "Billed Revenue";
            public const string UnBilledAccountName = "Un-Billed Revenue";
            public const string InternalNotes = "notes";
        }

        public static class FeatureContextValues
        {
            public const string ServiceTaxonomyDetails = "ServiceTaxonomyDetails";
            public const string GeneralLedgerCodeDetails = "GeneralLedgerCodeDetails";
            public const string GeneralLedgerDescriptionDetails = "GeneralLedgerDescriptionDetails";
            public const string PriorityRank = "priorityRank";
            public const string ItemId = "itemIdValue";
        }

        public static class TimeConstants
        {
            public const int MaxRetryAttempts = 9; // Total retries
            public const int RetryDelaySeconds = 120; // Delay between retries
            public const int EventAuditTiggerTime = 120000;
        }
    }
}
namespace PIMQAAutomation.Modules.Items.Constants
{
    public static class ItemConstants
    {
        public const string CharacteristicKind = "CharacteristicKind";
        public const string CharacteristicType = "CharacteristicType";
        public const string CharacteristicValue = "Characteristic Value";
        public const string Description = "description";
        public const string DocumentDateUser = "Document";
        public const string ImportCharacteristicKind = "Characteristic Kind";
        public const string ImportCharacteristicType = "Characteristic Type";
        public const string InternalNotes = "notes";
        public const string IsActiveCheckbox = "active";
        public const string IsAlias = "alias";
        public const string IsTest = "test";
        public const string Item = "item";
        public const string ItemId = "Item-Id";
        public const string ItemMasterInternalNotes = "internal-notes";
        public const string ItemMasterExternalNotes = "external-notes";
        public const string ProductCategory = "product-category";
        public const string ProductClass = "product-class";
        public const string ScopeObsolete = "scope-obsolete";
        public const string Sequence = "Sequence";
        public const string Sequencing = "Sequencing";
        public const string ServiceType = "Primary";
        public const string Status = "status";
        public const string UserTaxonomy = "User Taxonomy";
        public const string ItemCatalog = "ItemCatalog";
        public const string ItemDefault = "Item Default";
        public const string ItemPools = "ItemPools";
        public const string Date = "Date";
        public const string expectedTitle = "Error: Invalid Data";
        public const string expectedMessage = "An attempt has been made to add optional type with empty type list. Add optional type in Characteristics Kind types";

        //IdH Reference Id and PartitionKey from PIM>ItemPools
        public const string ItemReferenceId = "ItemReferenceId";
        public const string ItemReferencePartitionKey = "ItemReferencePartitionKey";
        public const string ItemCatalogId = "ItemCatalogId";
        public const string ItemCatalogPartitionKey = "ItemCatalogPartitionKey";
        public const string ItemPoolDocType = "ItemPool";

        public static readonly Dictionary<string, string> DocumentTypeMapping = new Dictionary<string, string>
        {
            { "ItemCatalog", "Item Catalog" },
            { "Item", "Item" }
        };

        public static readonly Dictionary<string, string> EventHistory = new Dictionary<string, string>
        {
            { "ItemCreated", "item.created" },
            { "ItemCatalogCreated", "itemCatalog.created" },
            { "ItemUpdated", "item.updated" },
            { "ItemCatalogUpdated", "itemCatalog.updated" },
            { "ItemCatalogQuotingActivated", "itemCatalog.quoting.activated" },
            { "ItemCatalogQuotingUpdated", "itemCatalog.quoting.updated" },
            { "ItemCatalogLineOfBusinessActivated", "itemCatalog.lineOfBusiness.activated" },
            { "ItemCatalogLineOfBusinessUpdated", "itemCatalog.lineOfBusiness.updated" },
            { "ItemCatalogMaintenanceActivated", "itemCatalog.maintenance.activated" },
            { "ItemCatalogMaintenanceUpdated", "itemCatalog.maintenance.updated" },
            { "ItemInventoryActivated","item.inventory.activated" },
            { "ItemInventoryUpdated","item.inventory.updated" },
            { "ItemQuotingActivated","item.quoting.activated" },
            { "ItemQuotingUpdated","item.quoting.updated" },
            { "ItemTaxonomyChanged","item.taxonomy.changed" }
        };

        public static class LocatorProperties
        {
            public const string BooleanCharacteristic = "Boolean";
            public const string CharacteristicValue = "Value";
            public const string LOBDeff = "lob";
            public const string UserTaxonomy = "User";
            public const string BaseLineTab = "baseline";
            public const string NumberCharacteristic = "Number";
            public const string Oem = "oem";
            public const string StringCharacteristic = "String";
            public const string DateCharacteristic = "Date";
        }

        public static class LevelLineageValue
        {
            public const string Item = "Item";
            public const string Service = "Service";
            public const string Taxonomy = "Taxonomy";
        }

        public static class ProductTaxonomyFields
        {
            public const string Family = "Family";
            public const string Group = "Group";
            public const string IsAlias = "Alias";
            public const string IsTest = "Test";
            public const string Line = "Line";
            public const string Oem = "Oem";
            public const string ProductTaxonomy = "product";
        }

        public static class ServiceTaxonomyFields
        {
            public const string Class = "Class";
            public const string Domain = "Domain";
            public const string Item = "Item";
            public const string ServiceTaxonomy = "service";
            public const string Type = "Type";
        }

        public static class CharacteristicTypes
        {
            public const string IsQuoteEligible = "isQuoteEligible";
            public const string PPTSupportability = "PPT Supportability";
        }

        public static class CharacteristicValues
        {
            public const string InternalNotes = "Internal-Notes";
            public const string IsActive = "Active";
            public const string ScopeObsolete = "Scope-Obsolete";
            public const string Value = "Value";
        }

        public static class ItemCreationValues
        {
            public const string Description = "Description";
            public const string InternalNotes = "Internal-Notes";
            public const string IsAlias = "Alias";
            public const string IsComponent = "Component";
            public const string IsProduct = "Product";
            public const string IsTest = "Test";
            public const string ItemId = "Item";
            public const string ItemStatus = "Status";
            public const string LOBDeff = "LOB";
            public const string Oem = "Oem";
        }
        public static class ImportExcelHeaders
        {
            public const string Description = "Description";
            public const string ItemId = "Item ID";
            public const string Oem = "OEM";
        }
        public static class CharacteristicNames
        {
            public const string PriceChangeComment = "Price Change Comment";
            public const string PricingGroup = "Pricing Group";
            public const string ChargeBack = "Charge Back";
            public const string PPTSupportability = "PPT Supportability";
            public const string PPTOverridePricingNextDay = "PPT Override Pricing Next Day";
            public const string PPTOverride7x24x4 = "PPT Override 7x24x4";
            public const string PPTOverridePricingDepot = "PPT Override Pricing Depot";
            public const string PPTOverrideFourHour = "PPT Override 4 Hour";
            public const string PricingCalculator = "Pricing Calculator";
            public const string PPTFlatRateMmcDirect = "PPT Flat Rate MMC Direct";
            public const string PPTFlatRateMmcChannel = "PPT Flat Rate MMC Channel";
            public const string PricingCatalogComponentOnly = "Pricing Catalog Component Only";
            public const string PricingCatalogProductOnly = "Pricing Catalog Product Only";
            public const string ProductFactor = "Product Factor";
            public const string InventoryTaxType = "Inventory Tax Type";
            public const string PriceChangeReasonCode = "Price Change Reason Code";
            public const string PriceMarginException = "Price Margin Exception";
            public const string AdvancedEngineeringApproved = "Advanced Engineering Approved";
            public const string BacklineSupport = "Backline Support";
            public const string ClearviewRank = "Clearview Rank";
            public const string InternalClearviewNotes = "Internal Clearview Notes";
            public const string L3PreApproved = "L3 PreApproved";
            public const string PPTListPrice = "PPT List Price";
            public const string Color = "Color";
            public const string DefaultStockingLocation = "Default Stocking Location";
            public const string IsDangerousGoods = "isDangerousGoods";
            public const string BillingType = "Billing Type";
            public const string Invoicing = "Invoicing";
            public const string SerialNumReqforInvoice = "Serial Num Req for Invoice";
            public const string PlanningCost = "Planning Cost";
            public const string PlannerGroup = "Planner Group";
            public const string RefundPercent = "Refund Percent";
            public const string ServiceFeePercent = "Service Fee Percent";
            public const string FourSNICompetitiveReference = "4SNI Competitive Reference";
            public const string FourSNIComments = "4SNI Comments";
            public const string FourSNISource = "4SNI Source";
            public const string IsActive4SNI = "IsActive4SNI";
            public const string IsPublishable = "IsPublishable";
            public const string ListPrice = "List Price";
            public const string IsCustomerOwned = "Is Customer Owned";
            public const string IsBillableMaintenance = "Is Billable Maintenance";
            public const string TaxCode = "Tax Code";
            public const string PL4Hour = "P&L 4 Hour";
            public const string PLNextDay = "P&L Next Day";
            public const string PL7x24x4 = "P&L 7x24x4";
            public const string PLDepot = "P&L Depot";
            public const string PL7x24x2 = "P&L 7x24x2";
            public const string PLAdvExchge = "P&L Adv Exchg";
            public const string Labor4Hour = "Labor 4 Hour";
            public const string Labor7x24x4 = "Labor 7x24x4";
            public const string Labor7x24x2 = "Labor 7x24x2";
            public const string LaborAdvExchg = "Labor Adv Exchg";
            public const string LaborDepot = "Labor Depot";
            public const string LaborNextDay = "Labor Next Day";
        }
        public static class CharacteristicKinds
        {
            public const string ListPriceAmounts = "List Price Amounts";
        }
    }

    public static class ItemMasterImport
    {
        public static readonly List<string> CharacteristicsDataExplicit = new List<string>
        {
            "Item ID",
            "OEM",
            "Line Of Business",
            "Characteristic Kind",
            "Characteristic Type",
            "Scope",
            "Value",
            "Scope Obsolete",
            "Is Active",
            "Internal Notes",
            "Create or Edit?"
        };

        public static readonly List<string> CharacteristicsDataExplictBaseline = new List<string>
        {
            "Item ID",
            "OEM",
            "Characteristic Kind",
            "Characteristic Type",
            "Scope",
            "Value",
            "Scope Obsolete",
            "Is Active",
            "Internal Notes",
            "Create or Edit?"
        };

        public static readonly List<string> CharacteristicsDataTaxonomy = new List<string>
        {
            "OEM",
            "Family",
            "Line",
            "Group",
            "Characteristic Kind",
            "Characteristic Type",
            "Scope",
            "Value",
            "Scope Obsolete",
            "Is Active",
            "Internal Notes",
            "Create or Edit?"
        };

        public static readonly List<string> ExplicictItemData = new List<string>
        {
            "Item ID",
            "OEM",
            "Line Of Business",
            "Description",
            "Create or Edit?",
            "Active",
            "Product",
            "Component",
            "Product Class",
            "Product Category",
            "Internal Notes",
            "External Notes"
        };

        public static readonly List<string> BaselineItemData = new List<string>
        {
            "Item ID",
            "OEM",
            "Description",
            "Create or Edit?",
            "Active",
            "Product",
            "Component",
            "Product Class",
            "Product Category",
            "Internal Notes",
            "External Notes"
        };
    }

    public static class ItemMasterExcelFileMap
    {
        public static readonly Dictionary<string, string> Baseline = new()
        {
            { "BaseLineDefaultStringLov", "ItemMasterImport/Baseline/BaseLineDefaultStringLov.xlsx" },
            { "BaseLineAdminStringLov", "ItemMasterImport/Baseline/BaseLineAdminStringLov.xlsx" },
            { "BaselineDefaultValueNumber", "ItemMasterImport/Baseline/BaselineDefaultValueNumber.xlsx" },
            { "BaselineAdminValueNumber", "ItemMasterImport/Baseline/BaselineAdminValueNumber.xlsx" },
            { "BaselineItemCreation", "ItemMasterImport/Baseline/BaselineItemCreation.xlsx" },
            { "BaselineTaxonomyItemCreation", "ItemMasterImport/Baseline/BaselineTaxonomyItemCreation.xlsx" },
            { "BaselineStingItemCreation", "ItemMasterImport/Baseline/BaselineStingItemCreation.xlsx" },
            { "BaselineTaxonomyStringItemCreation", "ItemMasterImport/Baseline/BaselineTaxonomyStringItemCreation.xlsx" },
            { "BaselineEmptyCharacteristicValue", "ItemMasterImport/Baseline/BaselineEmptyCharacteristicValue.xlsx" }
        };

        public static readonly Dictionary<string, string> Explicit = new()
        {
            { "ExplicitAdminValueNumber", "ItemMasterImport/Explicit/ExplicitAdminValueNumber.xlsx" },
            { "ExplicitDefaultNumber", "ItemMasterImport/Explicit/ExplicitDefaultNumber.xlsx" },
            { "ExplicitAdminBooleanValue", "ItemMasterImport/Explicit/ExplicitAdminBooleanValue.xlsx" },
            { "ExplicitDefaultBooleanValue", "ItemMasterImport/Explicit/ExplicitDefaultBooleanValue.xlsx" },
            { "ExplicitTaxonomyItemCreation", "ItemMasterImport/Explicit/ExplicitTaxonomyItemCreation.xlsx" },
            { "ExplicitItemCreation", "ItemMasterImport/Explicit/ExplicitItemCreation.xlsx" },
            { "ExplicitTaxonomyItemNumberCreation", "ItemMasterImport/Explicit/ExplicitTaxonomyItemNumberCreation.xlsx" },
            { "ExplicitItemNumberCreation", "ItemMasterImport/Explicit/ExplicitItemNumberCreation.xlsx" }
        };
        public static readonly Dictionary<string, string> Taxonomy = new()
        {
            { "ExplicitBooleanItemCreation", "ItemMasterImport/Taxonomy/ExplicitBooleanItemCreation.xlsx" },
            { "TaxonomyBooleanCreation", "ItemMasterImport/Taxonomy/TaxonomyBooleanCreation.xlsx" },
            { "TaxonomyBooleanValue", "ItemMasterImport/Taxonomy/TaxonomyBooleanValue.xlsx" },
            { "TaxonomyAdminBooleanValue", "ItemMasterImport/Taxonomy/TaxonomyAdminBooleanValue.xlsx" },
            { "ExplicitStringLovCreation", "ItemMasterImport/Taxonomy/ExplicitStringLovCreation.xlsx" },
            { "TaxonomyStringLov", "ItemMasterImport/Taxonomy/TaxonomyStringLov.xlsx" },
            { "TaxonomyDefaultStringLov", "ItemMasterImport/Taxonomy/TaxonomyDefaultStringLov.xlsx" },
            { "BooleanValue", "ItemMasterImport/Taxonomy/BooleanValue.xlsx" }
        };

    }
}
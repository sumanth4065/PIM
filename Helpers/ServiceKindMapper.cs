using PIMQAAutomation.Modules.Items.Constants;
using PIMQAAutomation.Utilities;
using PPT.Integration.CommonDataModel.Models.ItemCatalogs;
using PPT.Integration.CommonDataModel.Models.Items;
using System.Reflection;

namespace PIMQAAutomation.Helpers
{
    public static class ServiceKindMapper
    {
        private static readonly Dictionary<string, Func<ListPrice, object>> ListPricePropertyGetters =
            new(StringComparer.OrdinalIgnoreCase)
            {
                { "AdvExchg", lp => lp.AdvExchg },
                { "Depot", lp => lp.Depot },
                { "FourHour", lp => lp.FourHour },
                { "NextDay", lp => lp.NextDay },
                { "Sevenx24x2", lp => lp.Sevenx24x2 },
                { "Sevenx24x4", lp => lp.Sevenx24x4 }
            };

        private static readonly Dictionary<string, Func<ItemCatalogService, object>> KindAccessors =
            new(StringComparer.OrdinalIgnoreCase)
           {
                { "hasBacklineSupport", s => s.HasBacklineSupport },
                { "isActive", s => s.IsActive },
                { "isAvailable", s => s.IsAvailable },
                { "isBillableMaintenance", s => s.IsBillableMaintenance },
                { "isInventoryEligible", s => s.IsInventoryEligible },
                { "isMaintenanceEligible", s => s.IsMaintenanceEligible },
                { "isPrimary", s => s.IsPrimary },
                { "isQuoteEligible", s => s.IsQuoteEligible },
                { "price", s => s.Price },
                { "support", s => s.Support },
                { "approval", s => s.Approval },
                { "clearView", s => s.ClearView },
                { "supplyChain", s => s.SupplyChain },
                { "tax", s => s.Tax },
                { "billing", s => s.Billing },
                { "finance", s => s.Finance },
                { "industryPricing", lp => lp.IndustryPricing },
                { "isServiceProduct", s => s.SupplyChain.IsServiceProduct },
                { "PPTSupportability", s => s.Support.Rule }
           };

        private static readonly Dictionary<string, Func<PPT.Integration.CommonDataModel.Models.Items.Item, object>> IdhItemPropertyGetters =
          new(StringComparer.OrdinalIgnoreCase)
           {
                { "id", i => i.Id },
                { "partitionKey", i => i.PartitionKey },
                { "name", i => i.Name },
                { "comments", i => i.Comments },
                { "description", i => i.Description },
                { "isRemoved", i => i.IsRemoved },
                { "isActive", i => i.IsActive },
                { "isComponent", i => i.IsComponent },
                { "isCpu", i => i.IsCpu },
                { "isCustomerOwned", i => i.IsCustomerOwned },
                { "isDiskDrive", i => i.IsDiskDrive },
                { "isIndustryPriceEligible", i => i.IsIndustryPriceEligible },
                { "isInventoryEligible", i => i.IsInventoryEligible },
                { "isMaintenanceEligible", i => i.IsMaintenanceEligible },
                { "isMemory", i => i.IsMemory },
                { "isProduct", i => i.IsProduct },
                { "isPublishable", i => i.IsPublishable },
                { "isQuoteEligible", i => i.IsQuoteEligible },
                { "isSerialized", i => i.IsSerialized },
                { "isServer", i => i.IsServer },
                { "isServiceClass", i => i.IsServiceClass },
                { "isServiceProduct", i => i.IsServiceProduct },
                { "isSoftware", i => i.IsSoftware },
                { "isStorage", i => i.IsStorage },
                { "languageTranslations", i => i.LanguageTranslations },
                { "averageSeekTimeInMS", i => i.Attributes.Disk.AverageSeekTimeInMS },
                { "cacheInMB", i => i.Attributes.Disk.CacheInMB },
                { "capacity", i => i.Attributes.Disk.Capacity },
                { "type", i => i.Attributes.Disk.Type },
                { "transferRate", i => i.Attributes.Disk.TransferRate },
                { "manufacturer", i => i.Attributes.Manufacturer },
                { "formFactor", i => i.Attributes.Disk.FormFactor },
                { "interface", i => i.Attributes.Disk.Interface },
                { "speed", i => i.Attributes.Disk.Speed },
                {"serverArchitecture", i => i.Server.Architecture }
           };

        private static readonly Dictionary<string, Func<ItemCatalogService, object?>> IdhCatalogServicePropertyGetters =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "description", s => s.Description },
            { "hasBacklineSupport", s => s.HasBacklineSupport },
            { "isActive", s => s.IsActive },
            { "isAvailable", s => s.IsAvailable },
            { "isBillableMaintenance", s => s.IsBillableMaintenance },
            { "isInventoryEligible", s => s.IsInventoryEligible },
            { "isMaintenanceEligible", s => s.IsMaintenanceEligible },
            { "isPrimary", s => s.IsPrimary },
            { "isQuoteEligible", s => s.IsQuoteEligible },
            { "isServiceProduct", s => s.SupplyChain.IsServiceProduct },
            { "PPTSupportability", s => s.Support.Rule },
            { "isMslpInTriggerCalc", s => s.IndustryPricing.IsMslpInTriggerCalc},
            { "mslpPercentInTriggerCalc", s => s.IndustryPricing.MslpPercentInTriggerCalc},
            { "leadTimeInDays", s => s.SupplyChain.LeadTimeInDays },
            { "code", s => s.Tax.Code },
            { "classLegacy", s => s.ClassLegacy.Name}

        };

        private static readonly Dictionary<string, Func<PPT.Integration.CommonDataModel.Models.ItemCatalogs.Item, object?>> IdhCatalogItemPropertyGetters =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "isCustomerOwned", item => item.IsCustomerOwned },
            { "isSerialized", item => item.IsSerialized },
            { "isComponent", item => item.IsComponent },
            { "isProduct", item => item.IsProduct },
            { "isInventoryEligible", item => item.IsInventoryEligible },
            { "isServiceProduct", item => item.IsServiceProduct },
            { "isConfigurable", item => item.IsConfigurable },
            { "isActive", item => item.IsActive },
            { "ECCN", item => item.Compliance.Eccn },
            { "UN Code", item => item.Compliance.UNCodes },
            { "HTS Code", item => item.Compliance.HtsCode },
            { "ParkViewE", item => item.IsParkViewE },
            { "remoteMonitoringType", item => item.RemoteMonitoringType },
            { "AHR Eligible", item => item.IsAHREligible },
            { "isMaintenanceEligible",item=> item.IsMaintenanceEligible },
            { "IsQuoteEligible", item => item.IsQuoteEligible }
        };

        public static object GetKindSection(string kind, ItemCatalogService service)
        {
            if (string.IsNullOrWhiteSpace(kind))
            {
                return service;
            }
            if (!KindAccessors.TryGetValue(kind, out var accessor))
            {
                throw new ArgumentException($"Unsupported kind: '{kind}'. Supported kinds are: {string.Join(", ", KindAccessors.Keys)}", nameof(kind));
            }
            return accessor(service);
        }

        private static readonly Dictionary<string, string> ServicePropertyMappings
        = new()
        {
            { ItemConstants.CharacteristicNames.PriceChangeComment, "ChangeComment" },
            { ItemConstants.CharacteristicNames.PricingGroup, "Group" },
            { ItemConstants.CharacteristicNames.ChargeBack, "IsChargeBack" },
            { ItemConstants.CharacteristicNames.PPTOverridePricingNextDay, "NextDay" },
            { ItemConstants.CharacteristicNames.PPTOverride7x24x4, "Sevenx24x4Override" },
            { ItemConstants.CharacteristicNames.PPTOverridePricingDepot, "Depot" },
            { ItemConstants.CharacteristicNames.PricingCalculator, "IsPricingCalculatorNeeded" },
            { ItemConstants.CharacteristicNames.PPTOverrideFourHour, "FourHour" },
            { ItemConstants.CharacteristicNames.PPTSupportability, "Rule" },
            { ItemConstants.CharacteristicNames.PricingCatalogComponentOnly, "IsComponentOnly" },
            { ItemConstants.CharacteristicNames.PricingCatalogProductOnly, "IsProductOnly" },
            { ItemConstants.CharacteristicNames.ProductFactor, "ProductFactor" },
            { ItemConstants.CharacteristicNames.InventoryTaxType, "InventoryTax" },
            { ItemConstants.CharacteristicNames.PPTFlatRateMmcChannel, "FlatChannel" },
            { ItemConstants.CharacteristicNames.PPTFlatRateMmcDirect, "Flat" },
            { ItemConstants.CharacteristicNames.PPTListPrice, "List" },
            { ItemConstants.CharacteristicNames.Color, "Color" },
            { ItemConstants.CharacteristicNames.DefaultStockingLocation, "DefaultStockingLocation" },
            { ItemConstants.CharacteristicNames.IsDangerousGoods, "IsDangerousGoods" },
            { ItemConstants.CharacteristicNames.BillingType, "Type" },
            { ItemConstants.CharacteristicNames.Invoicing, "IsInvoicing" },
            { ItemConstants.CharacteristicNames.SerialNumReqforInvoice, "IsSerialNumberRequiredForInvoice" },
            { ItemConstants.CharacteristicNames.PlanningCost, "PlanningCost" },
            { ItemConstants.CharacteristicNames.AdvancedEngineeringApproved, "IsAdvancedEngineeringApproved" },
            { ItemConstants.CharacteristicNames.BacklineSupport, "HasBacklineSupport" },
            { ItemConstants.CharacteristicNames.ClearviewRank, "Rank" },
            { ItemConstants.CharacteristicNames.InternalClearviewNotes, "InternalNotes" },
            { ItemConstants.CharacteristicNames.L3PreApproved, "IsL3PreApproved" },
            { ItemConstants.CharacteristicNames.PriceChangeReasonCode, "ChangeReasonCode" },
            { ItemConstants.CharacteristicNames.PriceMarginException, "IsMarginException" },
            { ItemConstants.CharacteristicNames.RefundPercent, "RefundPercent" },
            { ItemConstants.CharacteristicNames.ServiceFeePercent, "ServiceFeePercent" },
            { ItemConstants.CharacteristicNames.PlannerGroup, "PlannerGroup" },
            { ItemConstants.CharacteristicNames.FourSNICompetitiveReference, "CompetitiveReference" },
            { ItemConstants.CharacteristicNames.FourSNIComments, "Comments" },
            { ItemConstants.CharacteristicNames.FourSNISource, "Source" },
            { ItemConstants.CharacteristicNames.IsActive4SNI, "IsActive" },
            { ItemConstants.CharacteristicNames.IsPublishable, "IndustryPriceIsPublishable" },
            { ItemConstants.CharacteristicNames.ListPrice, "ManufacturerSuggestedListPrice" },
            { ItemConstants.CharacteristicNames.IsCustomerOwned, "IsCustomerOwned" },
            { ItemConstants.CharacteristicNames.IsBillableMaintenance, "IsBillableMaintenance" },
            { ItemConstants.CharacteristicNames.TaxCode, "Code" }
        };

        public static string GetServicePropertyName(string type)
        {
            if (!ServicePropertyMappings.TryGetValue(type, out var propertyName))
                throw new ArgumentException($"Unknown characteristic type: '{type}'. Supported types are: {string.Join(", ", ServicePropertyMappings.Keys)}", nameof(type));

            return propertyName;
        }

        public static object GetListPriceValue(ListPrice listPrice, string characteristicType)
        {
            if (ListPricePropertyGetters.TryGetValue(characteristicType, out var getter))
            {
                return getter(listPrice);
            }
            throw new KeyNotFoundException($"Characteristic type '{characteristicType}' is not mapped in ListPrice.");
        }

        public static object GetIdhItemValue(PPT.Integration.CommonDataModel.Models.Items.Item item, string characteristicType)
        {
            if (IdhItemPropertyGetters.TryGetValue(characteristicType, out var getter))
            {
                return getter(item);
            }
            throw new KeyNotFoundException($"Characteristic type '{characteristicType}' is not mapped in Item.");
        }

        public static object GetIdhItemCatalogServiceValue(ItemCatalogService service, string characteristicType)
        {
            if (IdhCatalogServicePropertyGetters.TryGetValue(characteristicType, out var getter))
            {
                return getter(service);
            }
            throw new KeyNotFoundException($"Characteristic type '{characteristicType}' is not mapped in Service.");
        }

        public static object? GetIdhItemCatalogValue(dynamic item, string characteristicType)
        {
            if (IdhCatalogItemPropertyGetters.TryGetValue(characteristicType, out var getter))
            {
                return getter(item);
            }

            throw new KeyNotFoundException($"Characteristic type '{characteristicType}' is not mapped in the Item node.");
        }

        public static object? GetTaxonomyValue(dynamic taxonomy, string node, string property)
        {
            try
            {
                // Normalize node name to lowercase (so 'Class' or 'CLASS' both work)
                var normalizedNode = node.Trim().ToLowerInvariant();

                // Determine the correct node object
                object nodeObject = normalizedNode switch
                {
                    "class" => taxonomy.Class,
                    "category" => taxonomy.Category,
                    "family" => taxonomy.Family,
                    "group" => taxonomy.Group,
                    "line" => taxonomy.Line,
                    _ => throw new ArgumentException($"Unsupported taxonomy node: {node}")
                };

                ValidationHelper.FailIfNullOrEmpty(normalizedNode, nodeObject, "in Taxonomy");

                // Find property (case-insensitive)
                var propertyInfo = nodeObject.GetType().GetProperty(property,
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                ValidationHelper.FailIfNullOrEmpty(property, propertyInfo, $"Property '{property}' not found in taxonomy node '{node}'");

                // Retrieve property value
                var actualValue = propertyInfo.GetValue(nodeObject, null);
                ValidationHelper.FailIfNullOrEmpty(property, actualValue, $"Value for '{property}' is null in taxonomy node '{node}'");

                return actualValue.ToString();
            }
            catch (Exception ex)
            {
                ValidationHelper.Fail($"Error while fetching taxonomy value for Node: '{node}', Property: '{property}'.", ex);
                return null;
            }
        }
    }
}
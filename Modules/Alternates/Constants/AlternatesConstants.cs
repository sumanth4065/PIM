namespace PIMQAAutomation.Modules.Alternates.Constants
{
    public static class AlternatesConstants
    {
        public const int LowWaitTimeMs = 4000;
        // Trigger Times
        public const int AlternatePartPoolTriggerTimeMs = 420000;
        public const string AlternatePartPoolId = "AlternatePartPoolId";

        public static readonly Dictionary<string, string> EventHistory = new Dictionary<string, string>
        {
            { "AlternateCreated", "alternate.created" },
            { "AlternateItemAdded", "alternateItem.added" },
            { "AlternateItemPrimaryStatusUpdated", "alternateItem.primaryStatus.updated" },
            { "AlternateItemDeactivated", "alternateItem.deactivated" },
            { "AlternateItemReactivated", "alternateItem.reactivated" }
        };

        public static readonly List<string> AlternateImportData = new List<string>
        {
            "Lookup Item",
            "Lookup OEM",
            "Item",
            "OEM",
            "Active",
            "Set Master",
            "Internal Notes",
            "Type",
            "Mode"
        };
    }

    public static class AlternateImportExcelFileMap
    {
        public static readonly Dictionary<string, string> Alternate = new()
        {
            { "ExplicitItemData", "AlternatesImport/ExplicitItem/ExplicitItemData.xlsx" },
            { "AlternateInterchangeableActivate", "AlternatesImport/AlternatePart/Interchangeable/AlternateInterchangeableActivate.xlsx" },
            { "AlternateInterchangeableInactivate", "AlternatesImport/AlternatePart/Interchangeable/AlternateInterchangeableInactivate.xlsx" },
            { "AlternateInterchangeableSetAsMaster", "AlternatesImport/AlternatePart/Interchangeable/AlternateInterchangeableSetAsMaster.xlsx" },
            { "ExplicitItemDataUnidirectional", "AlternatesImport/ExplicitItem/ExplicitItemDataUnidirectional.xlsx" },
            { "AlternateUnidirectionalPartDataActivate", "AlternatesImport/AlternatePart/AlternateUnidirectionalPartDataActivate.xlsx" },
            { "AlternateUnidirectionalPartDataInactivate", "AlternatesImport/AlternatePart/AlternateUnidirectionalPartDataInactivate.xlsx" },
            { "ExplicitItemDataInterchangeableSupersession", "AlternatesImport/ExplicitItem/ExplicitItemDataInterchangeableSupersession.xlsx" },
            { "AlternateInterchangeable", "AlternatesImport/AlternatePart/Supersession/AlternateInterchangeable.xlsx" },
            { "AlternateInterchangeableSupersessionActive", "AlternatesImport/AlternatePart/Supersession/AlternateInterchangeableSupersessionActive.xlsx" },
            { "AlternateInterchangeableSupersessionInactive", "AlternatesImport/AlternatePart/Supersession/AlternateInterchangeableSupersessionInactive.xlsx" },
            { "AlternateInterchangeableSupersessionReactivate", "AlternatesImport/AlternatePart/Supersession/AlternateInterchangeableSupersessionReactivate.xlsx" },
            { "AlternateAliasPartData", "AlternatesImport/AlternatePart/Alias/AlternateAliasPartData.xlsx" },
            { "BaselineItemSelfReferencingInterchangeable", "AlternatesImport/AlternatePart/Interchangeable/BaselineItemSelfReferencingInterchangeable.xlsx" },
            { "ExplicitItemDataAlias", "AlternatesImport/AlternatePart/Alias/ExplicitItemDataAlias.xlsx" },
            { "ExplicitItemSelfReferencingInterchangeable", "AlternatesImport/AlternatePart/Interchangeable/ExplicitItemSelfReferencingInterchangeable.xlsx" }
        };
    }
}
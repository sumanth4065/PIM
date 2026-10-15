using System.Collections.Immutable;

namespace PIMQAAutomation.Modules.Bom.Constants
{
    public static class BomConstants
    {
        // BOM-specific constants
        public const string BOMHeaderItemId = "Sku";
        public const string BOMInternalNotes = "Internal-Notes";
        public const string BOMKind = "bom-kind";
        public const string BOMLineCreateEventStatus = "Creation";
        public const string BOMLineGrid = "BOM Line Grid";
        public const string BOMLineUpdateEventStatus = "Updation";
        public const string SerialNo = "serial-no";
        public const string PropertyName = "Name";

        //Sourcetype constants
        public const string GuiSourceType = "UI";
        public const string ImportSourceType = "Import";

        // BOM Types
        public const string BOMAsset = "Asset";
        public const string BOMProduct = "Product";
        public const string BOMPart = "Part";

        // Trigger Times
        public const int AssetBestBOMPoolTriggerTimeMs = 600000;
        public const int PartBestBOMPoolTriggerTimeMs = 660000;
        public const int ProductBestBOMPoolTriggerTimeMs = 780000;

        // Event History
        public const string EventHistoryPlanningBOM = "planning";
        public const string EventHistoryActualBOM = "actual";

        // Document Type
        public const string PlanningBom = "Planning Bom";
        public const string ActualBom = "Actual Bom";

        //IdH Reference Id and PartitionKey from PIM>BestBOMPools
        public const string PlanningBOMId = "planningBOMId";
        public const string PlanningBOMPartitionKey = "planningBOMPartitionKey";
        public const string ActualBOMId = "actualBOMId";
        public const string ActualBOMPartitionKey = "actualBOMPartitionKey";

        //Shown when BOM quantity is less than 1
        public const string QuantityErrorMessage = "Quantity must be at least 1";

        public const string True = "true";
        public const string False = "false";

        // Event History Dictionary
        public static readonly IImmutableDictionary<string, string> EventHistory = ImmutableDictionary.CreateRange(
        [
                new KeyValuePair<string, string>("PlanningBOMCreated", "planningBom.created"),
                new KeyValuePair<string, string>("ActualBOMCreated", "actualBom.created"),
                new KeyValuePair<string, string>("PlanningBOMLineAdded", "planningBom.bomLine.added"),
                new KeyValuePair<string, string>("ActualBOMLineAdded", "actualBom.bomLine.added"),
                new KeyValuePair<string, string>("PlanningBOMLineQuantityUpdated", "planningBom.lineQuantity.updated"),
                new KeyValuePair<string, string>("ActualBOMLineQuantityUpdated", "actualBom.lineQuantity.updated")
        ]);

        //Error message when data is duplicate
        public static readonly IImmutableDictionary<string, string> DuplicateErrorMessages = ImmutableDictionary.CreateRange(
        [
                new KeyValuePair<string, string>("Asset", "BOM Already Exists, Please select different BOM Kind or Serial Number"),
                new KeyValuePair<string, string>("Part", "BOM Already Exists, Please select different BOM Kind or OEM or SKU"),
                new KeyValuePair<string, string>("Product", "BOM Already Exists, Please select different BOM Kind or OEM or Family or Line or Group"),
                new KeyValuePair<string, string>("BOMLine", "Line already exists")
        ]);

        //FeatureContext Values
        public static class FeatureContextValue
        {
            public const string BOMLineCreationValues = "BOMLineGridValues";
            public const string BOMLineValues = "BOMLineValues";
            public const string BOMKind = "BOMKind";
            public const string SerialNumber = "SerialNumber";
            public const string BOMHeaderDetails = "BOMHeaderDetails"; //AssetFields
            public const string BOMCreationValues = "BOMCreationValues";
            public const string BOMInternalNotes = "InternalNotes";
        }
        public static class LocatorProperties
        {
            public const string BOMAsset = "asset";
            public const string BOMItemId = "sku";
            public const string BOMObsolete = "obsolete";
            public const string BOMOem = "oem";
            public const string BOMPart = "part";
            public const string BOMProduct = "product";
            public const string BOMQuantity = "quantity";
            public const string InstalledBase = "Installed-Base";
            public const string DefaultInstallBase = "Default-Install-Base";
        }

        public static class BomLineGridFields
        {
            public const string Description = "Description";
            public const string ItemId = "Sku";
            public const string Obsolete = "Obsolete";
            public const string Oem = "Oem";
            public const string Quantity = "Quantity";
            public const string Weight = "Weight";
        }

        public static class ExcelHeaders
        {
            public const string ExcelColumnBOMKind = "BOM Kind";
            public const string ExcelColumnBOMLineInstallBase = "BOM Line Install Base";
            public const string ExcelColumnBOMLineItemId = "BOM Line Item ID";
            public const string ExcelColumnBOMLineOem = "BOM Line OEM";
            public const string ExcelColumnDefaultInstallBase = "Default Install Base";
            public const string ExcelColumnBOMLineObsolete = "BOM Line Obsolete";
            public const string ExcelColumnFamily = "Family";
            public const string ExcelColumnGroup = "Group";
            public const string ExcelColumnItemID = "Item ID";
            public const string ExcelColumnLine = "Line";
            public const string ExcelColumnOem = "OEM";
            public const string ExcelColumnProductBOMLineItemId = "Bom Line Item ID";
            public const string ExcelColumnSerialNumber = "Serial Number";
            public const string ExcelSheetImport = "ImportDataSheet";
        }

        public static class ToastMessages
        {
            public const string BOMHeaderSuccess = "BOM Created Successfully";
            public const string BOMLineSuccess = "BOM Line updated successfully";
        }
    }

    public static class BomImportHeaders
    {
        public static readonly List<string> AssetExpectedHeaders = new List<string>
        {
            "BOM Kind",
            "Serial Number",
            "BOM Line Item ID",
            "BOM Line OEM",
            "BOM Line Install Base",
            "BOM Line Obsolete",
            "Create/Edit"
        };

        public static readonly List<string> ProductExpectedHeaders = new List<string>
        {
            "BOM Kind",
            "OEM",
            "Family",
            "Line",
            "Group",
            "Category",
            "Default Install Base",
            "Bom Line Item ID",
            "BOM Line OEM",
            "BOM Line Weight",
            "BOM Line Obsolete",
            "Create or Edit?"
        };

        public static readonly List<string> PartExpectedHeaders = new List<string>
        {
            "BOM Kind",
            "OEM",
            "Item ID",
            "BOM Line Item ID",
            "BOM Line OEM",
            "BOM Line Install Base",
            "BOM Line Obsolete",
            "Create/Edit"
        };
    }
    public static class BOMImportExcelFileMap
    {
        public static readonly Dictionary<string, string> BOMImportAsset = new()
        {
            { "AssetBOM", "BOMImport/Asset/AssetBOM.xlsx" },
            { "ImportAsset1LineAddedTestData", "BOMImport/Asset/ImportAsset1LineAddedTestData.xlsx" },
            { "ImportAsset1Obsolete", "BOMImport/Asset/ImportAsset1Obsolete.xlsx" },
            { "ImportAsset1TestData", "BOMImport/Asset/ImportAsset1TestData.xlsx" },
            { "ImportAsset1UpdateTestData", "BOMImport/Asset/ImportAsset1UpdateTestData.xlsx" },
            { "ImportAsset2LineAddedTestData", "BOMImport/Asset/ImportAsset2LineAddedTestData.xlsx" },
            { "ImportAsset2Obsolete", "BOMImport/Asset/ImportAsset2Obsolete.xlsx" },
            { "ImportAsset2TestData", "BOMImport/Asset/ImportAsset2TestData.xlsx" },
            { "ImportAsset2UpdateTestData", "BOMImport/Asset/ImportAsset2UpdateTestData.xlsx" },
            { "ImportAsset3Obsolete", "BOMImport/Asset/ImportAsset3Obsolete.xlsx" },
            { "ImportAsset3TestData", "BOMImport/Asset/ImportAsset3TestData.xlsx" },
        };

        public static readonly Dictionary<string, string> BOMImportProduct = new()
        {
            { "AssetProduct1BOM", "BOMImport/Product/AssetProduct1BOM.xlsx" },
            { "AssetProduct2BOM", "BOMImport/Product/AssetProduct2BOM.xlsx" },
            { "ImportProduct1LineAddedTestData", "BOMImport/Product/ImportProduct1LineAddedTestData.xlsx" },
            { "ImportProduct1Obsolete", "BOMImport/Product/ImportProduct1Obsolete.xlsx" },
            { "ImportProduct1TestData", "BOMImport/Product/ImportProduct1TestData.xlsx" },
            { "ImportProduct1UpdatedTestData", "BOMImport/Product/ImportProduct1UpdatedTestData.xlsx" },
            { "ImportProduct2LineAddedTestData", "BOMImport/Product/ImportProduct2LineAddedTestData.xlsx" },
            { "ImportProduct2Obsolete", "BOMImport/Product/ImportProduct2Obsolete.xlsx" },
            { "ImportProduct2TestData", "BOMImport/Product/ImportProduct2TestData.xlsx" },
            { "ImportProduct2UpdatedTestData", "BOMImport/Product/ImportProduct2UpdatedTestData.xlsx" },
            { "ImportProduct3Obsolete", "BOMImport/Product/ImportProduct3Obsolete.xlsx" },
            { "ImportProduct3TestData", "BOMImport/Product/ImportProduct3TestData.xlsx" },
            { "AssetProduct3BOM", "BOMImport/Product/AssetProduct3BOM.xlsx" },
        };
        public static readonly Dictionary<string, string> BOMImportPart = new()
        {
            { "AssetPart1BOM", "BOMImport/Part/AssetPart1BOM.xlsx" },
            { "AssetPart2BOM", "BOMImport/Part/AssetPart2BOM.xlsx" },
            { "ImportPart1LineAddedTestData", "BOMImport/Part/ImportPart1LineAddedTestData.xlsx" },
            { "ImportPart1Obsolete", "BOMImport/Part/ImportPart1Obsolete.xlsx" },
            { "ImportPart1TestData", "BOMImport/Part/ImportPart1TestData.xlsx" },
            { "ImportPart1UpdatedTestData", "BOMImport/Part/ImportPart1UpdatedTestData.xlsx" },
            { "ImportPart2LineAdded", "BOMImport/Part/ImportPart2LineAdded.xlsx" },
            { "ImportPart2LineUpdated", "BOMImport/Part/ImportPart2LineUpdated.xlsx" },
            { "ImportPart2Obsolete", "BOMImport/Part/ImportPart2Obsolete.xlsx" },
            { "ImportPart2TestData", "BOMImport/Part/ImportPart2TestData.xlsx" },
            { "ImportPart2UpdatedTestData", "BOMImport/Part/ImportPart2UpdatedTestData.xlsx" },
            { "ImportPart3Obsolete", "BOMImport/Part/ImportPart3Obsolete.xlsx" },
            { "ImportPart3TestData", "BOMImport/Part/ImportPart3TestData.xlsx" },
            { "AssetPart3BOM", "BOMImport/Part/AssetPart3BOM.xlsx" },
        };
    }
}
namespace PIMQAAutomation.Modules.Common.Constants
{
    public static class TimeConstants
    {
        public const int DelayPerChar = 60;
        public const int DefaultNavigationTimeOut = 60000;
        public const int DefaultTimeOut = 60000;
        public const int LoginTimeout = 80000;
        public const int ItemOrAlternatesPoolTriggerTimeMs = 420000;
        public const int LowWaitTimeMs = 4000;
        public const int MailTriggerTimeMs = 540000;
        public const string DateFormat = "MM/dd/yyyy";
        public const int ImportInitialWaitMinutes = 5;
        public const int ImportPollingIntervalMinutes = 1;
        public const int ImportMaxLimitMinutes = 60;
        public const int MinimumWait = 30; //Will wait until 30 sec's for element visibility
        public const int DefaultPollingWait = 1000; // Polling the UI with a 1-second wait
        public const int NavigationTaskWait = 5000;
        public const int ToastMessageWait = 1000; // will wait until 1 sec for Toastmessage visibility
        public const int DefaultWait = 30; //Will wait until 
        public const int MediumWait = 60; //Will wait until 60 sec's for element visibility
        public const int MaximumWait = 90; //Will wait until 90 sec's for element visibility
    }

    public static class ApplicationConstants
    {
        public const string AccessToken = "access_token";
        public const string HomePageHeaderMessage = "Welcome to the Park Place Item Master";
        public const string PimApplicationName = "pim";
        public const string PropertyName = "Name";
    }

    public static class DatabaseConstants
    {
        public const string AssetsContainer = "Assets";
        public const string BOMsContainer = "BOMs";
        public const string DatabaseName = "pim";
        public const string IntegrationIngestionDatabaseName = "integration-ingestion";
        public const string ItemsContainer = "Items";
        public const string ProductsContainer = "Products";
        public const string Idh = "integration-data-hub";
        public const string AlternatePartPoolsContainer = "AlternatePartPools";
        public const string AlternatesContainer = "Alternates";
        public const string ItemPoolsContainer = "ItemPools";
        public const string BestBOMPoolsContainer = "BestBOMPools";
        public const string LineOfBusinessContainer = "LineOfBusinesses";
        public const string EventAuditContainer = "event-audit";
        public const string ServicesContainer = "Services";
        public const string ChangeFeedContainer = "ChangeFeeds";
        public const string DefaultsContainer = "Defaults";
    }

    public static class EntityConstants
    {
        public const string AlternatePartPool = "AlternatePartPool";
        public const string ItemPartPool = "ItemPool";
        public const string BestBomPool = "BestBomPool";
        public const string LineOfBusinesses = "Line Of Business";
    }

    public static class KeyNameConstants
    {
        public const string Id = "Id";
        public const string PartitionKey = "PartitionKey";
        public const string Name = "Name";
    }

    public static class DateFieldConstants
    {
        public const string CreatedDate = "Created-Date";
        public const string CreatedUser = "Created-User";
        public const string DocumentModifiedDate = "Document-Modified-Date";
        public const string DocumentModifiedUser = "Document-Modified-User";
        public const string ModifiedDate = "Modified-Date";
        public const string ModifiedUser = "Modified-User";
    }

    public static class Endpoints
    {
        public const string AutomationCleanDataEndpoint = "/pim/v1/Automation/Automation/cleanData";
        public const string CleanBOMByOemEndpoint = "/pim/v1/BOMs/bom/cleanBomByOem";
    }

    public static class ImportConstants
    {
        public const string ImportStatus = "COMPLETE";
        public const string ImpportFailedMessage = "Failed to process the record with Item characteristic record";
    }

    public static class FolderName
    {
        public const string FolderConfig = "Config";
        public const string FolderResources = "Resources";
        public const string ImportTestData = "ImportTestData.xlsx";
        public const string JsonFileRunSettings = "RunSettings.json";
        public const string JsonFileTestData = "TestData.json";
    }

    public static class RunSettingsConstants
    {
        public const string ApiUri = "APIURI";
        public const string AzureUri = "AzureURL";
        public const string DbConnectionString = "DBConnectionString";
        public const string DeleteApiUri = "DeleteAPIURI";
        public const string EnableIDHVerification = "enableIDHVerification";
        public const string EnableAutomationDataCleanup = "enableAutomationDataCleanup";
        public const string IDHConnectionString = "IDHConnectionString";
        public const string PIMQAUrl = "PIMQAURL";
        public const string PPTPassword = "pptPassword";
        public const string PPTUsername = "pptUsername";
        public const string PIMQAAutomationPAT = "PIMQAAutomationPAT";
        public const string ADOOrganization = "ADOOrganization";
        public const string BugProject = "BugProject";
        public const string TestProject = "TestProject";
        public const string CreateBug = "CreateBug";
        public const string Environment = "Environment";
        public const string CreateTraceLog = "CreateTraceLog";
        public const string BugAreaPath = "BugAreaPath";
        public const string ProjectTeam = "ProjectTeam";

        public const string AlternatesOwner = "AlternatesOwner";
        public const string LOBOwner = "LOBOwner";
        public const string BOMOwner = "BOMOwner";
        public const string ItemOwner = "ItemOwner";
        public const string AdminOwner = "AdminOwner";
        public const string DefaultOwner = "DefaultOwner";
    }

    public static class ButtonNames
    {
        public const string SaveButton = "Save";
        public const string EditButton = "edit";
        public const string UndoButton = "undo";
    }

    public class AppSettingConstants
    {
        public const string InternalApplicationServiceOktaSecret = "InternalApplicationServiceOktaSecret";
        public const string Keyvault = "Application:Keyvault";
        public const string OktaApplicationClientSecret = "Application:OktaApplicationClientSecret";
    }

    public static class ValidationConstants
    {
        public const double QuantityTolerance = 0.0001;
        public const bool MicrosoftAuth = true; // Set to true to enable Microsoft Authentication. Remove once Okta is fully implemented.
    }

    public static class AttributeConstants
    {
        public const string AriaSelected = "aria-selected";
        public const string Class = "class";
        public const string AriaExpanded = "aria-expanded";
        public const string False = "false";
        public const string True = "true";
        public const string Active = "active";
    }

    public static class ToastConstants
    {
        public const string SuccessfulTitle = "success";
    }
}
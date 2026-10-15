using PIMQAAutomation.Helpers;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Modules.Lob.Constants;
using PIMQAAutomation.Utilities;
using PPT.Integration.CommonDataModel.Models.LineOfBusinesses;
using TechTalk.SpecFlow;

namespace PIMQAAutomation.Modules.Lob.Steps
{
    public partial class LOB
    {
        [Then(@"User verifies standardEventName ""(.*)"" in Event-Audit container of Integration-Ingestion database")]
        public async Task ThenUserVerifiesStandardEventNameInContainerOfDatabase(string standardEventName)
        {
            try
            {
                var itemId = ContextHelper.GetContextValue(featureContext, LOBConstants.FeatureContextValues.ItemId);
                var database = DatabaseConstants.IntegrationIngestionDatabaseName;
                var container = DatabaseConstants.EventAuditContainer;
                var eventName = LOBConstants.StandardEventNames.ContainsKey(standardEventName)
                        ? LOBConstants.StandardEventNames[standardEventName]
                        : standardEventName;
                await page.WaitForTimeoutAsync(TimeConstants.ItemOrAlternatesPoolTriggerTimeMs);
                var standardEventQuery = CosmosDBQueries.GetStanardEventQuery(itemId, eventName);
                var standardEventRecord = await CosmosDbHelper.GetAndInspectDataFromCosmosDB<LineOfBusiness>(database, container, standardEventQuery);
                ValidationHelper.FailIfNullOrEmpty("standardEventRecord", standardEventRecord, $"No {eventName} standardEventRecord for item-id {itemId} '{standardEventQuery}' in database '{database}' container '{container}'");
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"UI standard Event {standardEventName} validation failed against database.", e);
            }
        }

        [Then(@"Verify the created and modified details in Cosmos DB using Integration-Ingestion database and Event-Audit container")]
        public async Task ThenVerifyTheCreatedAndModifiedDetailsInCosmosDBUsingDatabaseAndContainer(Table fieldsToCheck)
        {
            try
            {
                var itemId = ContextHelper.GetContextValue(featureContext, LOBConstants.FeatureContextValues.ItemId);
                var eventAuditQuery = CosmosDBQueries.GetDataUsingBodyName(itemId);
                var dbEventAuditData = await CosmosDbHelper.GetAndInspectDataFromCosmosDB<LineOfBusiness>(DatabaseConstants.IntegrationIngestionDatabaseName, DatabaseConstants.EventAuditContainer, eventAuditQuery);
                ValidationHelper.FailIfNullOrEmpty("dbEventAuditData", dbEventAuditData, $"No dbEventAuditData data found for {itemId} reference in database");
                var uiDateFields = new Dictionary<string, string>();
                foreach (var dateField in fieldsToCheck.Rows)
                {
                    var columnName = dateField[0];
                    uiDateFields[columnName] = (await lobPage.GetCreatedModifiedUserDate(dateField[0]).TextContentAsync()).Trim().TrimEnd(',').Trim();
                }

                var dbDateFields = new Dictionary<string, string>
                {
                     { DateFieldConstants.CreatedDate, dbEventAuditData.Created.On.ToString(TimeConstants.DateFormat).Trim() },
                     { DateFieldConstants.CreatedUser, dbEventAuditData.Created.By.Name.Trim() },
                     { DateFieldConstants.ModifiedDate, dbEventAuditData.LastModified.On.ToString(TimeConstants.DateFormat).Trim() },
                     { DateFieldConstants.ModifiedUser, dbEventAuditData.LastModified.By.Name.Trim() }
                };
                ValidationHelper.AssertDictionariesMatch(uiDateFields, dbDateFields, "Created and modified details vs Database");
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"Created and modified date fields validation failed against database.", e);
            }
        }

        [Then(@"User should not find any event created in the database for ""(.*)""")]
        public async Task ThenUserShouldNotFindAnyEventCreatedInTheDatabaseFor(string standardEventName)
        {
            var itemId = string.Empty;
            try
            {
                itemId = ContextHelper.GetContextValue(featureContext, LOBConstants.FeatureContextValues.ItemId);
                var eventName = LOBConstants.StandardEventNames.ContainsKey(standardEventName)
                        ? LOBConstants.StandardEventNames[standardEventName]
                        : standardEventName;
                var standardEventQuery = CosmosDBQueries.GetStanardEventQuery(itemId, eventName);
                var standardEventRecord = await CosmosDbHelper.GetAndInspectDataFromCosmosDB<LineOfBusiness>(DatabaseConstants.IntegrationIngestionDatabaseName, DatabaseConstants.EventAuditContainer, standardEventQuery);
                ValidationHelper.FailIfNotNullOrNotEmpty("StandardEventRecord", standardEventRecord, $"Expected no events with name '{eventName}', but found some in the database.");
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"Failed to validate absence of event '{standardEventName}' in DB. Details for {itemId}", e);
            }
        }

        [Then("User verifies the Service Taxonomy details in Event-Audit container of Integration-Ingestion database")]
        public async Task ThenUserVerifiesTheServiceDetailsInEvent_AuditContainerOfIntegration_IngestionDatabase()
        {
            var itemId = string.Empty;
            try
            {
                itemId = ContextHelper.GetContextValue(featureContext, LOBConstants.FeatureContextValues.ItemId);
                var defaultQuery = CosmosDBQueries.GetDataUsingBodyName(itemId);
                await page.WaitForTimeoutAsync(LOBConstants.TimeConstants.EventAuditTiggerTime);
                var servicesDocument = await CosmosDbHelper.GetAndInspectDataFromCosmosDB<LineOfBusiness>(DatabaseConstants.IntegrationIngestionDatabaseName, DatabaseConstants.EventAuditContainer, defaultQuery);
                ValidationHelper.FailIfNullOrEmpty("servicesDocument", servicesDocument, $"No servicesDocument data found for {itemId} reference in database");
                var serviceTaxonomy = servicesDocument.Services.FirstOrDefault().ServiceTaxonomy;
                ValidationHelper.FailIfNullOrEmpty("serviceTaxonomy", serviceTaxonomy, $"No serviceTaxonomy data found for {itemId} reference in Services node");
                var uiServiceTaxonomyValues = featureContext[LOBConstants.FeatureContextValues.ServiceTaxonomyDetails] as Dictionary<string, string>;
                ValidationHelper.FailIfNullOrEmpty("ServiceTaxonomyDetails", uiServiceTaxonomyValues, $"is null or empty in feature context for verifying Service Taxonomy details");
                var dbServiceTaxonomyValues = new Dictionary<string, string>
                {
                     { LOBConstants.ServiceTaxonomyFields.ServiceClass, serviceTaxonomy.ServiceClass.Name},
                     { LOBConstants.ServiceTaxonomyFields.ServiceDomain, serviceTaxonomy.ServiceDomain.Name},
                     { LOBConstants.ServiceTaxonomyFields.ServiceItem, serviceTaxonomy.ServiceItem.Name},
                     { LOBConstants.ServiceTaxonomyFields.ServiceType, serviceTaxonomy.ServiceType.Name}
                };
                ValidationHelper.AssertDictionariesMatch(uiServiceTaxonomyValues, dbServiceTaxonomyValues, $"{itemId} UI Service Taxonomy details vs Database");
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"{itemId} UI Service Taxonomy details validation failed against database.", e);
            }
        }

        [Then("User verifies the General Ledger code details in Event-Audit container of Integration-Ingestion database")]
        public async Task ThenUserVerifiesTheGeneralLedgerCodeDetailsInEvent_AuditContainerOfIntegration_IngestionDatabase()
        {
            try
            {
                var itemId = ContextHelper.GetContextValue(featureContext, LOBConstants.FeatureContextValues.ItemId);
                var eventAuditQuery = CosmosDBQueries.GetDataUsingBodyName(itemId);
                var generalLedgerAccoutData = await CosmosDbHelper.GetAndInspectDataFromCosmosDB<LineOfBusiness>(DatabaseConstants.IntegrationIngestionDatabaseName, DatabaseConstants.EventAuditContainer, eventAuditQuery);
                ValidationHelper.FailIfNullOrEmpty("generalLedgerAccoutData", generalLedgerAccoutData, $"No general Ledger Accout Data data found for {itemId} reference in database");
                var uiGeneralLedgerValues = featureContext[LOBConstants.FeatureContextValues.GeneralLedgerCodeDetails] as Dictionary<string, string>;
                ValidationHelper.FailIfNullOrEmpty("GeneralLedgerValues", uiGeneralLedgerValues, $"is null or empty in feature context for verifying General Ledger details");
                var dbGeneralLedgerValues = new Dictionary<string, string>
                {
                     { LOBConstants.GeneralLedgerAccounts.BilledDivisionCode, generalLedgerAccoutData.Finance.Division.Code},
                     { LOBConstants.GeneralLedgerAccounts.BilledAccountCode, generalLedgerAccoutData.Finance.NaturalAccounts.FirstOrDefault(n => n.Context.Equals(LOBConstants.LocatorProperties.BilledAccountName)).Code},
                     { LOBConstants.GeneralLedgerAccounts.UnbilledAccountCode, generalLedgerAccoutData.Finance.NaturalAccounts.FirstOrDefault(n => n.Context.Equals(LOBConstants.LocatorProperties.UnBilledAccountName)).Code},
                };
                ValidationHelper.AssertDictionariesMatch(uiGeneralLedgerValues, dbGeneralLedgerValues, $"UI General Ledger details vs Database");
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"Failed to verify UI General Ledger Code details in Integration-Ingestion>Event-Audit.", e);
            }
        }

        [Then("User verifies the following General Ledger code description details in Event-Audit container of Integration-Ingestion database")]
        public async Task ThenUserVerifiesTheFollowingGeneralLedgerCodeDescriptionDetailsInEvent_AuditContainerOfIntegration_IngestionDatabase(Table fieldsToCheck)
        {
            try
            {
                var itemId = ContextHelper.GetContextValue(featureContext, LOBConstants.FeatureContextValues.ItemId);
                var eventAuditQuery = CosmosDBQueries.GetDataUsingBodyName(itemId);
                var generalLedgerAccoutData = await CosmosDbHelper.GetAndInspectDataFromCosmosDB<LineOfBusiness>(DatabaseConstants.IntegrationIngestionDatabaseName, DatabaseConstants.EventAuditContainer, eventAuditQuery);
                ValidationHelper.FailIfNullOrEmpty("generalLedgerAccoutData", generalLedgerAccoutData, $"No general Ledger Accout Data data found for {itemId} reference in database");
                var uiGeneralLedgerValues = await GetUiGeneralLedgerCodeDescriptionValuesAsync(fieldsToCheck);
                ValidationHelper.FailIfNullOrEmpty("General Ledger Code Description Values", uiGeneralLedgerValues, $"is null or empty in feature context for verifying General Ledger Code Description details");
                var dbGeneralLedgerValues = new Dictionary<string, string>
                {
                     { LOBConstants.GeneralLedgerAccounts.BilledDivisionCodeName, generalLedgerAccoutData.Finance.Division.Name},
                     { LOBConstants.GeneralLedgerAccounts.BilledAccountCodeName, generalLedgerAccoutData.Finance.NaturalAccounts.FirstOrDefault(n => n.Context.Equals(LOBConstants.LocatorProperties.BilledAccountName)).Name},
                     { LOBConstants.GeneralLedgerAccounts.UnbilledAccountCodeName, generalLedgerAccoutData.Finance.NaturalAccounts.FirstOrDefault(n => n.Context.Equals(LOBConstants.LocatorProperties.UnBilledAccountName)).Name},
                };
                ValidationHelper.AssertDictionariesMatch(uiGeneralLedgerValues, dbGeneralLedgerValues, $"UI General Ledger details vs Database");
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"Failed to verify UI General Ledger Code Description details in Integration-Ingestion>Event-Audit.", e);
            }
        }
    }
}
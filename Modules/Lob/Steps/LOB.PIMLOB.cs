using NUnit.Framework;
using PIMQAAutomation.Helpers;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Modules.Lob.Constants;
using PIMQAAutomation.Utilities;
using PPT.ItemMaster.Domain.Features.LineOfBusiness;
using TechTalk.SpecFlow;

namespace PIMQAAutomation.Modules.Lob.Steps
{
    public partial class LOB
    {
        [Then(@"User verifies the following Service details in LineOfBusinesses container of Pim database")]
        public async Task ThenUserVerifiesTheFollowingServiceDetailsInContainerOfDatabase()
        {
            var itemId = string.Empty;
            try
            {
                itemId = ContextHelper.GetContextValue(featureContext, LOBConstants.FeatureContextValues.ItemId);
                await page.WaitForTimeoutAsync(TimeConstants.LowWaitTimeMs);
                var defaultQuery = CosmosDBQueries.GetDataUsingNameOrSerialNo(itemId);
                var servicesDocument = await cosmosDb.GetDataFromCosmosDB<LineOfBusiness>(DatabaseConstants.DatabaseName, DatabaseConstants.LineOfBusinessContainer, defaultQuery);
                var serviceTaxonomy = servicesDocument.ServiceTaxonomy;
                var uiServiceTaxonomyValues = featureContext[LOBConstants.FeatureContextValues.ServiceTaxonomyDetails] as Dictionary<string, string>;
                ValidationHelper.FailIfNullOrEmpty("ServiceTaxonomyDetails", uiServiceTaxonomyValues, $"is null or empty in feature context for verifying Service Taxonomy details");
                var dbServiceTaxonomyValues = new Dictionary<string, string>
                {
                     { LOBConstants.ServiceTaxonomyFields.ServiceClass, serviceTaxonomy.ServiceClass.Name},
                     { LOBConstants.ServiceTaxonomyFields.ServiceDomain, serviceTaxonomy.ServiceDomain.Name},
                     { LOBConstants.ServiceTaxonomyFields.ServiceItem, serviceTaxonomy.ServiceItem.Name},
                     { LOBConstants.ServiceTaxonomyFields.ServiceType, serviceTaxonomy.ServiceType.Name}
                };
                ValidationHelper.AssertDictionariesMatch(uiServiceTaxonomyValues, dbServiceTaxonomyValues, $"UI Service Taxonomy details vs Database");
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"{itemId} UI Service Taxonomy details validation failed against database.", e);
            }
        }

        [Then(@"User verifies ""(.*)"" status in LineOfBusinesses container of Pim database")]
        public async Task ThenUserVerifiesStatusInContainerOfDatabase(string activeStatus)
        {
            var itemId = string.Empty;
            var database = DatabaseConstants.DatabaseName;
            var container = DatabaseConstants.LineOfBusinessContainer;
            try
            {
                itemId = ContextHelper.GetContextValue(featureContext, LOBConstants.FeatureContextValues.ItemId);
                var query = CosmosDBQueries.GetDataUsingNameOrSerialNo(itemId);
                var lobDocument = await cosmosDb.GetDataFromCosmosDB<LineOfBusiness>(database, container, query);
                Assert.IsNotNull(lobDocument, $"{query} document is not found in {database}>{container}'.");
                var isActiveStatus = await lobPage.GetPrimaryLOVsFiled(LOBConstants.LocatorProperties.LovToGl, activeStatus).IsCheckedAsync();
                var activeCheckboxStatus = isActiveStatus ? "True" : "False";
                ValidationHelper.AssertEqual("Item SKU", activeCheckboxStatus, lobDocument.IsActive.ToString());
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"ItemId '{itemId}' and Description not displaying in {database}>{container}");
            }
        }

        [Then(@"User verifies the following General Ledger code details in LineOfBusinesses container of Pim database")]
        public async Task ThenUserVerifiesTheFollowingGeneralLedgerCodeDetailsInContainerOfDatabase(Table fieldsToCheck)
        {
            try
            {
                var itemId = ContextHelper.GetContextValue(featureContext, LOBConstants.FeatureContextValues.ItemId);
                var query = CosmosDBQueries.GetDataUsingNameOrSerialNo(itemId);
                var generalLedgerAccoutData = await cosmosDb.GetDataFromCosmosDB<LineOfBusiness>(DatabaseConstants.DatabaseName, DatabaseConstants.LineOfBusinessContainer, query);
                ValidationHelper.FailIfNullOrEmpty("General Ledger Accout Data", generalLedgerAccoutData, $"No general Ledger Accout Data data found for {itemId} reference in database");
                var uiGeneralLedgerValues = featureContext[LOBConstants.FeatureContextValues.GeneralLedgerCodeDetails] as Dictionary<string, string>;
                ValidationHelper.FailIfNullOrEmpty("General Ledger Values", uiGeneralLedgerValues, $"is null or empty in feature context for verifying General Ledger details");
                var dbGeneralLedgerValues = new Dictionary<string, string>
                {
                     { LOBConstants.GeneralLedgerAccounts.BilledDivisionCode, generalLedgerAccoutData.Finance.Division.Code},
                     { LOBConstants.GeneralLedgerAccounts.BilledAccountCode, generalLedgerAccoutData.Finance.NaturalAccounts.FirstOrDefault(n => n.Context.Equals(LOBConstants.LocatorProperties.BilledAccountName)).Code},
                     { LOBConstants.GeneralLedgerAccounts.UnbilledAccountCode, generalLedgerAccoutData.Finance.NaturalAccounts.FirstOrDefault(n => n.Context.Equals(LOBConstants.LocatorProperties.UnBilledAccountName)).Code}
                };
                ValidationHelper.AssertDictionariesMatch(uiGeneralLedgerValues, dbGeneralLedgerValues, $"UI General Ledger details vs Database");
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"UI General Ledger details validation failed in database.", e);
            }
        }
    }
}
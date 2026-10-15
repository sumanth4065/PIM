using NUnit.Framework;
using PIMQAAutomation.Helpers;
using PIMQAAutomation.Helpers.DocumentFetcher;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Modules.Lob.Constants;
using PIMQAAutomation.Utilities;
using PPT.Integration.CommonDataModel.Models.LineOfBusinesses;
using TechTalk.SpecFlow;

namespace PIMQAAutomation.Modules.Lob.Steps
{
    public partial class LOB
    {
        [Then("User verifies the Service Taxonomy details in Services container of Integration-Data-Hub database")]
        public async Task ThenUserVerifiesTheFollowingServiceDetailsInServicesContainerOfIntegration_Data_HubDatabase()
        {
            var itemId = string.Empty;
            try
            {
                if (!IsIdhValidationEnabled())
                {
                    return;
                }
                itemId = ContextHelper.GetContextValue(featureContext, LOBConstants.FeatureContextValues.ItemId);
                var context = new ValidationContext(itemId);

                var servicesDocument = await DocumentFetcherFactory.FetchDocumentAsync<LineOfBusiness>(context);
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

        [Then("User verifies the General Ledger code details in Services container of Integration-Data-Hub database")]
        public async Task ThenUserVerifiesTheGeneralLedgerCodeDetailsInServicesContainerOfIntegration_Data_HubDatabase()
        {
            var database = DatabaseConstants.Idh;
            var container = DatabaseConstants.ServicesContainer;
            try
            {
                if (!IsIdhValidationEnabled())
                {
                    return;
                }
                var itemId = ContextHelper.GetContextValue(featureContext, LOBConstants.FeatureContextValues.ItemId);
                var context = new ValidationContext(itemId);

                var generalLedgerAccoutData = await DocumentFetcherFactory.FetchDocumentAsync<LineOfBusiness>(context);
                ValidationHelper.FailIfNullOrEmpty("generalLedgerAccoutData", generalLedgerAccoutData, $"No general Ledger Accout Data data found for {itemId} reference in database");
                var uiGeneralLedgerValues = featureContext[LOBConstants.FeatureContextValues.GeneralLedgerCodeDetails] as Dictionary<string, string>;
                ValidationHelper.FailIfNullOrEmpty("GeneralLedgerValues", uiGeneralLedgerValues, $"is null or empty in feature context for verifying General Ledger details");
                var dbGeneralLedgerValues = new Dictionary<string, string>
                {
                     { LOBConstants.GeneralLedgerAccounts.BilledDivisionCode, generalLedgerAccoutData.Finance.Division.Code},
                     { LOBConstants.GeneralLedgerAccounts.BilledAccountCode, generalLedgerAccoutData.Finance.NaturalAccounts.FirstOrDefault(n => n.Context.Equals(LOBConstants.LocatorProperties.BilledAccountName)).Code},
                     { LOBConstants.GeneralLedgerAccounts.UnbilledAccountCode, generalLedgerAccoutData.Finance.NaturalAccounts.FirstOrDefault(n => n.Context.Equals(LOBConstants.LocatorProperties.UnBilledAccountName)).Code},
                };
                ValidationHelper.AssertDictionariesMatch(uiGeneralLedgerValues, dbGeneralLedgerValues, $"UI Service Taxonomy details in {database}>{container}");
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"UI General Ledger details validation failed in {database}>{container}.", e);
            }
        }

        [Then(@"User verifies ""(.*)"" status in Services container of Integration-Data-Hub database")]
        public async Task ThenUserVerifiesStatusInServicesContainerOfIntegration_Data_HubDatabase(string activeStatus)
        {
            var itemId = string.Empty;
            var database = DatabaseConstants.Idh;
            var container = DatabaseConstants.ServicesContainer;
            try
            {
                if (!IsIdhValidationEnabled())
                {
                    return;
                }
                itemId = ContextHelper.GetContextValue(featureContext, LOBConstants.FeatureContextValues.ItemId);
                var context = new ValidationContext(itemId);
                var idhServicesData = await DocumentFetcherFactory.FetchDocumentAsync<LineOfBusiness>(context);
                Assert.IsNotNull(idhServicesData, $"Services Document = '{idhServicesData}' not found.");
                var isActiveStatus = await lobPage.GetPrimaryLOVsFiled(LOBConstants.LocatorProperties.LovToGl, activeStatus).IsCheckedAsync();
                var activeCheckboxStatus = isActiveStatus ? "True" : "False";
                ValidationHelper.AssertEqual("Item SKU", activeCheckboxStatus, idhServicesData.IsActive.ToString());
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"Failed to verify {activeStatus} status for item {itemId} in {database}>{container}", e);
            }
        }

        [Then(@"User verifies the ""(.*)"" Id and ""(.*)"" values in Services container of Integration-Data-Hub database")]
        public async Task ThenUserVerifiesTheIdAndValuesInContainerOfDatabase(string sku, string description)
        {
            var itemId = string.Empty;
            var database = DatabaseConstants.Idh;
            var container = DatabaseConstants.ServicesContainer;
            try
            {
                if (!IsIdhValidationEnabled())
                {
                    return;
                }
                itemId = ContextHelper.GetContextValue(featureContext, LOBConstants.FeatureContextValues.ItemId);
                var context = new ValidationContext(itemId);
                var idhServicessData = await DocumentFetcherFactory.FetchDocumentAsync<LineOfBusiness>(context);
                Assert.IsNotNull(idhServicessData, $"Service with processingType = '{idhServicessData}' not found.");

                // Get UI Decription
                var uiDescription = featureContext["Description"].ToString();

                // Compare DB with UI
                Assert.Multiple(() =>
                {
                    ValidationHelper.AssertEqual(sku, itemId, idhServicessData.Name);
                    ValidationHelper.AssertEqual(description, uiDescription, idhServicessData.Description);
                });
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"ItemId '{itemId}' and Description not displaying in {database}>{container}", e);
            }
        }

        [Then(@"User verifies the following General Ledger code description details in Services container of Integration-Data-Hub database")]
        public async Task ThenUserVerifiesTheFollowingGeneralLedgerDetailsInContainerOfDatabase(Table fieldsToCheck)
        {
            var database = DatabaseConstants.Idh;
            var container = DatabaseConstants.ServicesContainer;
            try
            {
                if (!IsIdhValidationEnabled())
                {
                    return;
                }
                var itemId = ContextHelper.GetContextValue(featureContext, LOBConstants.FeatureContextValues.ItemId);
                var context = new ValidationContext(itemId);
                var generalLedgerAccoutData = await Helpers.DocumentFetcher.DocumentFetcherFactory.FetchDocumentAsync<LineOfBusiness>(context);
                ValidationHelper.FailIfNullOrEmpty("generalLedgerAccoutData", generalLedgerAccoutData, $"No general Ledger Accout Data data found for {itemId} reference in database");
                var uiGeneralLedgerValues = await GetUiGeneralLedgerCodeDescriptionValuesAsync(fieldsToCheck);
                ValidationHelper.FailIfNullOrEmpty("GeneralLedgerValues", uiGeneralLedgerValues, $"is null or empty in feature context for verifying General Ledger details");
                var dbGeneralLedgerValues = new Dictionary<string, string>
                {
                     { LOBConstants.GeneralLedgerAccounts.BilledDivisionCodeName, generalLedgerAccoutData.Finance.Division.Name},
                     { LOBConstants.GeneralLedgerAccounts.BilledAccountCodeName, generalLedgerAccoutData.Finance.NaturalAccounts.FirstOrDefault(n => n.Context.Equals(LOBConstants.LocatorProperties.BilledAccountName)).Name},
                     { LOBConstants.GeneralLedgerAccounts.UnbilledAccountCodeName, generalLedgerAccoutData.Finance.NaturalAccounts.FirstOrDefault(n => n.Context.Equals(LOBConstants.LocatorProperties.UnBilledAccountName)).Name},
                };
                ValidationHelper.AssertDictionariesMatch(uiGeneralLedgerValues, dbGeneralLedgerValues, $"UI General Ledger details in {database}>{container}");
            }
            catch (Exception e)
            {
                ValidationHelper.Fail($"UI General Ledger details validation failed in {database}>{container}.", e);
            }
        }
    }
}
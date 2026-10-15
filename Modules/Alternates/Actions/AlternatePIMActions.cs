namespace PIMQAAutomation.Modules.Alternates.Actions
{
  using Microsoft.Playwright;
    using NUnit.Framework;
    using PIMQAAutomation.Modules.Alternates.Constants;
    using PIMQAAutomation.Modules.Common.Constants;
  using PIMQAAutomation.Modules.Items.Constants;
    using PIMQAAutomation.Utilities;
    using PPT.ItemMaster.Data.Cosmos.Features.AlternatePartPools;
    using PPT.ItemMaster.Data.Cosmos.Features.Alternates;
    using PPT.ItemMaster.Data.Cosmos.Features.ItemPools;
    using PPT.ItemMaster.Domain.Features.AlternatePartPools;
    using TechTalk.SpecFlow;

    /// <summary>
    /// Contains business logic and action methods for PIM Alternate document validation in Cosmos DB
    /// </summary>
    public class AlternatePIMActions
    {
        private readonly IPage page;
  private readonly ScenarioContext scenarioContext;
        private readonly FeatureContext featureContext;
        private readonly CosmosDbHelper cosmosDb;
  private readonly AlternateCommonActions commonActions;

  public AlternatePIMActions(IPage page, ScenarioContext scenarioContext, FeatureContext featureContext, AlternateCommonActions commonActions)
        {
   this.page = page;
       this.scenarioContext = scenarioContext;
            this.featureContext = featureContext;
            this.commonActions = commonActions;
    cosmosDb = new CosmosDbHelper();
      }

        #region Cluster Details Verification

        /// <summary>
        /// Verifies the cluster details for a specific item in Cosmos DB
        /// </summary>
      public async Task VerifyClusterDetailsAsync(string item, string dataBase, string container, Table table)
        {
     try
         {
         var itemId = commonActions.GetFieldValue(item, ItemConstants.ItemId);
    var row = table.Rows[0];
    var clusterType = row["ClusterType"];
         var defaultQuery = CosmosDBQueries.GetAlternateClusterData(itemId, clusterType);
     var dbAlternateItemData = await cosmosDb.GetDataFromCosmosDB<ClusterItemRecord>(dataBase.ToLower(), container, defaultQuery);

         Assert.Multiple(() =>
   {
         ValidationHelper.AssertEqual("IsActive", row["IsActive"], dbAlternateItemData.IsActive.ToString());
            ValidationHelper.AssertEqual("IsMaster", row["IsMaster"], dbAlternateItemData.IsMaster.ToString());
   });
       }
            catch (Exception e)
{
      ValidationHelper.Fail($"Alternate cluster values are not matching with DB with error {e.Message}", e);
    }
        }

        #endregion

      #region Document Creation Verification

     /// <summary>
        /// Verifies that a document is created for a specific item in ItemPool
      /// </summary>
        public async Task VerifyDocumentCreatedAsync(string item, string dataBase, string container)
        {
 try
          {
                var triggerTime = TimeConstants.ItemOrAlternatesPoolTriggerTimeMs;
         await page.WaitForTimeoutAsync(triggerTime);
    var itemId = commonActions.GetFieldValue(item, ItemConstants.ItemId);
            var itemQuery = CosmosDBQueries.GetItemPoolQuery(itemId);
       var itemPoolData = await cosmosDb.GetDataFromCosmosDB<ItemPoolRecord>(dataBase.ToLower(), container, itemQuery);
     ValidationHelper.FailIfNullOrEmpty("dbAlternateItemData", itemPoolData, $"Document not created in ItemPool for the Item-Id - '{itemId}'");
 }
            catch (Exception ex)
            {
   ValidationHelper.Fail($"Document verification failed in ItemPool. Error: {ex.Message}", ex);
      }
  }

    #endregion

    #region AlternatePartPools Validation

        /// <summary>
      /// Verifies alternate part pool details including event history and alternate items
        /// </summary>
        public async Task VerifyAlternatePartPoolDetailsAsync(string containerName, string databaseName, string item, Table table)
        {
            try
       {
                await commonActions.WaitIfTriggerToWaitAsync();
   var itemId = commonActions.GetFieldValue(item, ItemConstants.ItemId);

         var alternatePartPoolsQuery = CosmosDBQueries.GetItemPoolQuery(itemId);
     var document = await cosmosDb.GetDataFromCosmosDB<AlternatePartPoolRecord>(databaseName.ToLower(), containerName, alternatePartPoolsQuery);
     ValidationHelper.FailIfNullOrEmpty("document", document, $"Document for Item Id '{itemId}' not found in container '{containerName}'.");
       var expectedEventName = string.Empty;
      var expectedRelationshipType = string.Empty;
            var expectedItemId = string.Empty;

     foreach (var row in table.Rows)
     {
           expectedEventName = AlternatesConstants.EventHistory[row["EventName"]];
         expectedRelationshipType = row["RelationshipType"];
          expectedItemId = commonActions.GetFieldValue(row["Item"], ItemConstants.ItemId);

          var eventHistory = document.EventHistories.FirstOrDefault(eh => eh.Name == expectedEventName);
       ValidationHelper.FailIfNullOrEmpty("eventHistory", eventHistory, $"Event '{expectedEventName}' not found in eventHistories.");

      var alternateItem = document.AlternateItems.FirstOrDefault(ai => ai.Item.Sku == expectedItemId && ai.RelationshipType == expectedRelationshipType);
    ValidationHelper.FailIfNullOrEmpty("alternateItem", alternateItem, $"Alternate Item with SKU '{expectedItemId}' and relationshipType '{expectedRelationshipType}' not found under item '{itemId}'.");

            Assert.Multiple(() =>
          {
   ValidationHelper.AssertEqual("IsMaster", row["IsMaster"], alternateItem.IsMaster.ToString());
             ValidationHelper.AssertEqual("IsGenerated", row["IsGenerated"], alternateItem.IsGenerated.ToString());
                ValidationHelper.AssertEqual("IsActive", row["IsActive"], alternateItem.IsActive.ToString());
       ValidationHelper.AssertEqual("IsImmediateSupersessionMaster", row["IsImmediateSupersessionMaster"], alternateItem.IsImmediateSupersessionMaster.ToString());
ValidationHelper.AssertEqual("IsAliasPart", row["IsAliasPart"], alternateItem.IsAliasPart.ToString());
    ValidationHelper.AssertEqual("IsAliasAlternate", row["IsAliasAlternate"], alternateItem.IsAliasAlternate.ToString());
          });
             }
     }
            catch (Exception ex)
            {
      ValidationHelper.Fail($"Error during AlternatePartPools validation: {ex.Message}", ex);
            }
        }

   #endregion

        #region Cluster Replacement Verification

        /// <summary>
        /// Verifies cluster replacement field values
        /// </summary>
     public async Task VerifyClusterReplacementDetailsAsync(string item, string dataBase, string container, Table table)
        {
          try
      {
    var itemDetails = featureContext[item] as Dictionary<string, string>;
       var itemId = itemDetails[ItemConstants.ItemId];

   var row = table.Rows[0];
          var clusterType = row["ClusterType"];
 var expectedDocType = row["DocumentType"];

var clusterQuery = CosmosDBQueries.GetAlternateClusterData(itemId, clusterType);
      var clusterItem = await cosmosDb.GetDataFromCosmosDB<ClusterItemRecord>(dataBase.ToLower(), container, clusterQuery);

      ValidationHelper.FailIfNullOrEmpty("clusterItem", clusterItem, $"Cluster item not found in DB for ItemId: {itemId} with ClusterType: {clusterType}");
      var partitionKey = clusterItem.PartitionKey;

       var replacementQuery = CosmosDBQueries.GetReplacementClusterData(partitionKey, expectedDocType);
                var replacementDoc = await cosmosDb.GetDataFromCosmosDB<ClusterItemRecord>(dataBase.ToLower(), container, replacementQuery);

     ValidationHelper.FailIfNullOrEmpty("replacementDoc", replacementDoc, $"Cluster replacement document not found for PartitionKey: {partitionKey}");
 ValidationHelper.AssertEqual("IsActive", row["IsActive"], replacementDoc.IsActive.ToString());
ValidationHelper.AssertEqual("IsActive", expectedDocType, replacementDoc.Type.ToString());
            }
      catch (Exception e)
            {
                ValidationHelper.Fail($"Cluster replacement values did not match DB records. Error: {e.Message}", e);
            }
        }

        #endregion

        #region AlternatePartPool Non-Creation Verification

 /// <summary>
        /// Verifies that AlternatePartPool document is not created for a specific item
        /// </summary>
        public async Task VerifyAlternatePartPoolDocumentNotCreatedAsync(string item)
        {
     try
            {
    var triggerTime = AlternatesConstants.AlternatePartPoolTriggerTimeMs;
      await page.WaitForTimeoutAsync(triggerTime);
                var itemId = commonActions.GetFieldValue(item, ItemConstants.ItemId);
     var alternatePartPoolsQuery = CosmosDBQueries.GetItemPoolQuery(itemId);
            var document = await cosmosDb.GetDataFromCosmosDB<AlternatePartPool>(DatabaseConstants.DatabaseName, DatabaseConstants.AlternatePartPoolsContainer, alternatePartPoolsQuery);
     if (document != null)
        {
          ValidationHelper.Fail($"Document should not be created in AlternatePartPool for item {itemId}");
  }
        }
            catch (Exception ex)
    {
ValidationHelper.Fail($"Error during AlternatePartPools validation: {ex.Message}", ex);
            }
    }

        #endregion
    }
}

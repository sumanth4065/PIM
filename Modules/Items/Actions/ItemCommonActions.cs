using System.Text.Json;
using Microsoft.Playwright;
using NUnit.Framework;
using PIMQAAutomation.Helpers;
using PIMQAAutomation.Helpers.DocumentFetcher;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Modules.Items.Constants;
using PIMQAAutomation.Utilities;
using PPT.Integration.CommonDataModel.Models.ItemCatalogs;
using PPT.ItemMaster.Data.Cosmos.Features.ItemPools;
using PPT.ItemMaster.Data.Cosmos.Features.Items;
using TechTalk.SpecFlow;

namespace PIMQAAutomation.Modules.Items.Actions
    {
    /// <summary>
    /// Contains common business logic for Item operations
    /// </summary>
    public class ItemCommonActions
        {
        private readonly IPage page;
        private readonly ScenarioContext scenarioContext;
        private readonly FeatureContext featureContext;
        private readonly CosmosDbHelper cosmosDb;

        public ItemCommonActions(IPage page, ScenarioContext scenarioContext, FeatureContext featureContext)
            {
            this.page = page;
            this.scenarioContext = scenarioContext;
            this.featureContext = featureContext;
            cosmosDb = new CosmosDbHelper();
            }

        /// <summary>
        /// Checks if IDH validation is enabled in test settings
        /// </summary>
        /// <returns>True if IDH validation is enabled or not configured</returns>
        public bool IsIdhValidationEnabled()
            {
            return string.IsNullOrEmpty(TestContext.Parameters[RunSettingsConstants.EnableIDHVerification]) ||
             Convert.ToBoolean(TestContext.Parameters[RunSettingsConstants.EnableIDHVerification]);
            }

        /// <summary>
        /// Fetches document from Cosmos DB or IDH based on document type
        /// </summary>
        /// <param name="documentType">Type of document to fetch</param>
        /// <returns>Document object</returns>
        public async Task<object> GetDocumentAsync(string documentType)
            {
            try
                {
                var itemId = ContextHelper.GetContextValue(featureContext, "itemIdValue");
                var database = DatabaseConstants.DatabaseName;
                var container = string.Empty;

                if (string.Equals(documentType, ItemConstants.ItemDefault, StringComparison.OrdinalIgnoreCase))
                    {
                    container = DatabaseConstants.DefaultsContainer;
                    var defaultQuery = CosmosDBQueries.GetDataUsingItemIdAndDocumentType(itemId, documentType);
                    var document = await cosmosDb.GetDataFromCosmosDB<ItemDefaultRecord>(database, container, defaultQuery);
                    return document;
                    }
                else if (string.Equals(documentType, ItemConstants.ItemPools, StringComparison.OrdinalIgnoreCase))
                    {
                    container = DatabaseConstants.ItemPoolsContainer;
                    var poolQuery = CosmosDBQueries.GetItemPoolQuery(itemId);
                    var document = await cosmosDb.GetDataFromCosmosDB<ItemPoolRecord>(database, container, poolQuery);
                    return document;
                    }
                else if (string.Equals(documentType, ItemConstants.Item, StringComparison.OrdinalIgnoreCase))
                    {
                    var context = new ValidationContext(itemId, ItemConstants.Item);
                    var document = await DocumentFetcherFactory.FetchDocumentAsync<PPT.Integration.CommonDataModel.Models.Items.Item>(context);
                    return document;
                    }
                else if (string.Equals(documentType, ItemConstants.ItemCatalog, StringComparison.OrdinalIgnoreCase))
                    {
                    var context = new ValidationContext(itemId, ItemConstants.ItemCatalog);
                    var document = await DocumentFetcherFactory.FetchDocumentAsync<ItemCatalog>(context);
                    return document;
                    }
                else
                    {
                    Console.WriteLine($"Unsupported document type: {documentType}");
                    return null;
                    }
                }
            catch (Exception ex)
                {
                Console.WriteLine($"Error fetching document. DocumentType: {documentType}, Error: {ex}");
                return null;
                }
            }

        /// <summary>
        /// Extracts Class and Category names from a document JSON element
        /// </summary>
        /// <param name="element">JSON element containing the document data</param>
        /// <param name="documentType">Type of document</param>
        /// <returns>Tuple of (ClassName, CategoryName)</returns>
        public static (string ClassName, string CategoryName) GetClassAndCategory(JsonElement element, string documentType)
            {
            try
                {
                JsonElement taxonomyElement;

                if (string.Equals(documentType, ItemConstants.Item, StringComparison.OrdinalIgnoreCase))
                    {
                    if (!element.TryGetProperty("Taxonomy", out taxonomyElement))
                        {
                        return (null, null);
                        }
                    }
                else if (string.Equals(documentType, ItemConstants.ItemPools, StringComparison.OrdinalIgnoreCase) ||
                 string.Equals(documentType, ItemConstants.ItemDefault, StringComparison.OrdinalIgnoreCase) ||
                     string.Equals(documentType, ItemConstants.ItemCatalog, StringComparison.OrdinalIgnoreCase))
                    {
                    if (!element.TryGetProperty("Item", out var itemElement) ||
              !itemElement.TryGetProperty("Taxonomy", out taxonomyElement))
                        {
                        return (null, null);
                        }
                    }
                else
                    {
                    Console.WriteLine($"Unsupported document type: {documentType}");
                    return (null, null);
                    }

                var className = taxonomyElement.TryGetProperty("Class", out var classProp)
             ? classProp.GetProperty("Name").GetString()
            : null;

                var categoryName = taxonomyElement.TryGetProperty("Category", out var categoryProp)
                         ? categoryProp.GetProperty("Name").GetString()
                         : null;

                return (className, categoryName);
                }
            catch (Exception ex)
                {
                Console.WriteLine($"Error extracting Class/Category. DocumentType: {documentType}, Error: {ex}");
                return (null, null);
                }
            }

        /// <summary>
        /// Verifies product class and category in a document
        /// </summary>
        /// <param name="documentType">Type of document to verify</param>
        /// <param name="inputValues">Table containing expected Class and Category values</param>
        public async Task VerifyProductClassAndCategoryAsync(string documentType, Table inputValues)
            {
            try
                {
                if (string.Equals(documentType, ItemConstants.Item, StringComparison.OrdinalIgnoreCase) ||
            string.Equals(documentType, ItemConstants.ItemCatalog, StringComparison.OrdinalIgnoreCase))
                    {
                    if (!IsIdhValidationEnabled())
                        {
                        return;
                        }
                    }

                var expectedClass = inputValues.Rows[0]["Class"];
                var expectedCategory = inputValues.Rows[0]["Category"];
                var rawDocument = await GetDocumentAsync(documentType);
                ValidationHelper.FailIfNullOrEmpty("Document", rawDocument, $"Failed to fetch document for type: {documentType}");

                JsonElement element;
                if (rawDocument is JsonElement jsonElement)
                    {
                    element = jsonElement;
                    }
                else
                    {
                    element = JsonSerializer.SerializeToElement(rawDocument);
                    }

                var (actualClass, actualCategory) = GetClassAndCategory(element, documentType);

                ValidationHelper.AssertEqual("Product Class", expectedClass, actualClass);
                ValidationHelper.AssertEqual("Product Category", expectedCategory, actualCategory);
                }
            catch (Exception e)
                {
                ValidationHelper.Fail($"Exception occurred while verifying document type '{documentType}': {e.Message}");
                }
            }
        }
    }

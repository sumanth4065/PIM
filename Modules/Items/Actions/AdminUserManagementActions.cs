using Microsoft.Playwright;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Modules.Items.Constants;
using PIMQAAutomation.Utilities;
using TechTalk.SpecFlow;
using Utilities;
namespace PIMQAAutomation.Modules.Items.Actions
{
   

    /// <summary>
    /// Contains business logic for Admin User Management operations
    /// </summary>
    public class AdminUserManagementActions
    {
        private readonly IPage page;
        private readonly ScenarioContext scenarioContext;
        private readonly FeatureContext featureContext;
        private const string UserManagement = "AdminUserManagement";
        private const string CommonActions = "AdminCommonActions";
        private string testCaseName = string.Empty;

        public AdminUserManagementActions(IPage page, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            this.page = page;
            this.scenarioContext = scenarioContext;
            this.featureContext = featureContext;
        }


        /// <summary>
        /// Verifies that the User Management page header is visible and correct
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task VerifyUserManagementPageHeaderAsync()
        {
            try
            {
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{UserManagement}.UserManagementPageHeader", TimeConstants.MediumWait, WaitForSelectorState.Visible);

                var expectedPageHeader = AdminConstants.AuthorizedUsers;
                var actualPageHeader = (await WebUIHelper.GetInnerTextAsync(scenarioContext, page, $"{UserManagement}.UserManagementPageHeader")).Trim();
                Console.WriteLine($"The extracted text for page header: {actualPageHeader}");

                ValidationHelper.FailIfNullOrEmpty($"{UserManagement}.UserManagementPageHeader", actualPageHeader, "Expected not to be null. But was null.");
                ValidationHelper.AssertEqual($"{UserManagement}.UserManagementPageHeader", expectedPageHeader, actualPageHeader);
            }
            catch (PlaywrightException ex)
            {
                throw ExceptionHandler.BuildCustomException("User Management page header. The header was not found or not visible within the timeout.", ex, testCaseName);
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("validate User Management page header. The header was not found or not visible within the timeout.", ex);
            }
        }

        /// <summary>
        /// Verifies Characteristic Kind and User details
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task VerifyCharKindUserDetailsAsync()
        {
            try
            {
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{UserManagement}.DefaultCharKindOptionSelected", TimeConstants.MediumWait, WaitForSelectorState.Visible);

                await WebUIHelper.IsVisibleAsync(scenarioContext, page, $"{UserManagement}.DefaultCharKindOptionSelected");
                await WebUIHelper.IsVisibleAsync(scenarioContext, page, $"{UserManagement}.DefaultUsersOptionSelected");

                var defaultCharKindOption = (await WebUIHelper.GetInnerTextAsync(scenarioContext, page, $"{UserManagement}.DefaultCharKindOption")).Trim();
                Console.WriteLine($"[INFO] Default selected Characteristic Kind: '{defaultCharKindOption}'");

                var defaultUserOption = (await WebUIHelper.GetInnerTextAsync(scenarioContext, page, $"{UserManagement}.DefaultUsersOption")).Trim();
                Console.WriteLine($"[INFO] Default selected User: '{defaultUserOption}'");

                await WebUIHelper.IsVisibleAsync(scenarioContext, page, $"{UserManagement}.UserDetailsFormTitle");

                var actualEmailID = (await WebUIHelper.GetInputValueAsync(scenarioContext, page, $"{UserManagement}.EmailIdTextBox")).Trim();
                Console.WriteLine($"[INFO] The extracted Email ID: '{actualEmailID}'");

                var actualFullName = (await WebUIHelper.GetInputValueAsync(scenarioContext, page, $"{UserManagement}.FullNameTextBox")).Trim();
                Console.WriteLine($"[INFO] The extracted Full Name: '{actualFullName}'");

                ValidationHelper.AssertEqual($"{UserManagement}.FullNameTextBox", defaultUserOption, actualFullName, "The selected User does not match the User Option.");
                ValidationHelper.FailIfNullOrEmpty($"{UserManagement}.EmailIdTextBox", actualEmailID, "Email Id should not be null");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DEBUG] Failed to validate Authorized User details. Reason: {ex.Message}", ex);
                throw ExceptionHandler.BuildCustomException("validate Authorized User details", ex);
            }
        }

        /// <summary>
        /// Validates User details for selected Characteristic Kind and User
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task ValidateUsersDetailsForSelectedCharKindAndUserAsync(Table table)
        {
            try
            {
                testCaseName = scenarioContext.ScenarioInfo.Title;

                foreach (var row in table.Rows)
                {
                    string fieldName = row["FieldName"].Trim();
                    string fieldValue = Utilities.Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.{fieldName}");

                    if (!scenarioContext.ContainsKey("RandomKindName") && fieldName == "CharacteristicKindSearchTextBox")
                    {
                        throw new CustomException("[ERROR] RandomKindName is missing in ScenarioContext. Ensure Kind creation step stored it.");
                    }

                    switch (fieldName)
                    {
                        case "CharacteristicKindSearchTextBox":
                            await SearchCharacteristicKindAsync();
                            break;

                        case "UserSearchTextBox":
                            await SearchUserAsync(fieldValue);
                            break;

                        default:
                            Console.WriteLine($"[WARN] No validation rule defined for field '{fieldName}'. Skipping.");
                            break;
                    }
                }
            }
            catch (PlaywrightException ex)
            {
                Console.WriteLine($"[ERROR] Playwright failure: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw ExceptionHandler.BuildCustomException("validate Characteristic Kind and User Details validation", ex, testCaseName);
            }
        }



        /// <summary>
        /// Modifies user details based on provided table
        /// </summary>
        /// <returns><placeholder>A <see cref="Task"/> representing the asynchronous operation.</placeholder></returns>
        public async Task ModifyUserDetailsAsync(Table fieldNames)
        {
            try
            {
                testCaseName = scenarioContext.ScenarioInfo.Title;
                await page.WaitForTimeoutAsync(TimeConstants.MinimumWait);
                await WebUIHelper.WaitForElementToDisappearAsync(scenarioContext, page, $"{CommonActions}.LoadSpinner", TimeConstants.MediumWait);
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{UserManagement}.EditButton", TimeConstants.MediumWait, WaitForSelectorState.Visible);
                await WebUIHelper.ClickAsync(scenarioContext, page, $"{UserManagement}.EditButton");

                foreach (var row in fieldNames.Rows)
                {
                    string fieldName = row["FieldName"].Trim();
                    string fieldValue = Utility.ReadDataFromJsonString(scenarioContext, $"testCases.{testCaseName}.{fieldName}");

                    if (string.IsNullOrWhiteSpace(fieldValue))
                    {
                        Console.WriteLine($"[INFO] Skipped '{fieldName}' as no test data was provided.");
                        continue;
                    }

                    switch (fieldName)
                    {
                        case "ActiveCheckBox":
                            await WebUIHelper.HandleCheckboxAsync(scenarioContext, page, $"{UserManagement}.ActiveCheckBox", fieldValue, fieldName);
                            Console.WriteLine($"[INFO] Checked Active CheckBox with '{fieldValue}'");
                            break;

                        case "InternalNotesTextBox":
                            await WebUIHelper.FillAsync(scenarioContext, page, $"{UserManagement}.InternalNotesTextBox", fieldValue);
                            Console.WriteLine($"[INFO] Filled Internal Notes with '{fieldValue}'");
                            break;

                        default:
                            Console.WriteLine($"[WARN] No action defined for field '{fieldName}'");
                            break;
                    }
                }

                await ClickUpdateButtonAsync();
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("modify Active Checkbox User details", ex, testCaseName);
            }
        }



        /// <summary>
        /// Clicks the Update button
        /// </summary>
        private async Task ClickUpdateButtonAsync()
        {
            try
            {
                await page.WaitForTimeoutAsync(TimeConstants.MinimumWait);
                await WebUIHelper.ClickAsync(scenarioContext, page, $"{UserManagement}.UpdateButton");
                await WebUIHelper.WaitForElementToDisappearAsync(scenarioContext, page, $"{CommonActions}.LoadSpinner", TimeConstants.MinimumWait);
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("click on the Update button", ex, testCaseName);
            }
        }



        /// <summary>
        /// Searches for Characteristic Kind
        /// </summary>
        private async Task SearchCharacteristicKindAsync()
        {
            try
            {
                string scenarioKindName = scenarioContext["RandomKindName"].ToString();
                Console.WriteLine($"[INFO] Searching Characteristic Kind: {scenarioKindName}");

                await WebUIHelper.FillAsync(scenarioContext, page, $"{UserManagement}.CharacteristicKindSearchTextBox", scenarioKindName);

                var selectedKindOption = (await WebUIHelper.GetInnerTextAsync(scenarioContext, page, $"{UserManagement}.DefaultCharKindOption")).Trim();
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{UserManagement}.DefaultCharKindOption", TimeConstants.MinimumWait, selectedKindOption, WaitForSelectorState.Visible);
                await WebUIHelper.IsVisibleAsync(scenarioContext, page, $"{UserManagement}.DefaultCharKindOption", selectedKindOption);
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("search Characteristic Kind", ex);
            }
        }

        /// <summary>
        /// Searches for User and validates details
        /// </summary>
        private async Task SearchUserAsync(string fieldValue)
        {
            try
            {
                Console.WriteLine($"[INFO] Searching User: {fieldValue}");

                await WebUIHelper.ClickAsync(scenarioContext, page, $"{UserManagement}.UserSearchTextBox");
                await WebUIHelper.FillAndPressEnterAsync(scenarioContext, page, $"{UserManagement}.UserSearchTextBox", fieldValue);
                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{UserManagement}.SearchedUsersOption", TimeConstants.MinimumWait, fieldValue, WaitForSelectorState.Visible);

                var searchedOption = (await WebUIHelper.GetInnerTextAsync(scenarioContext, page, $"{UserManagement}.DefaultUsersOption")).Trim();
                Console.WriteLine($"[INFO] The extracted searched User option: {searchedOption}");

                await WebUIHelper.WaitForAsync(scenarioContext, page, $"{UserManagement}.UserDetailsFormTitle", TimeConstants.MinimumWait, WaitForSelectorState.Visible);

                var actualUser = await WebUIHelper.GetInputValueAsync(scenarioContext, page, $"{UserManagement}.FullNameTextBox");
                Console.WriteLine($"[INFO] Expected User: {searchedOption}, Actual User: {actualUser}");

                ValidationHelper.AssertEqual("User Details", searchedOption, actualUser);
                Console.WriteLine("[INFO] The Characteristic Kind and User Details are validated.");
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException("search and validate User", ex);
            }
        }
    }
}

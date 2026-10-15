namespace PIMQAAutomation.Modules.Alternates.Actions
{
    using Microsoft.Playwright;

    /// <summary>
    /// Contains business logic for grid-related operations in the Alternate module.
    /// This class provides helper methods for parsing and working with table/grid data.
    /// </summary>
    public static class AlternateGridActions
    {
        /// <summary>
        /// Extracts row details from a table row locator and returns as a dictionary.
        /// Maps table cell data to field names: Oem, Item-Id, Description, IsActive, IsMaster.
        /// </summary>
        /// <param name="rowLocator">The table row locator to extract data from</param>
        /// <returns>Dictionary containing field names and their values</returns>
        public static async Task<Dictionary<string, string>> GetRowDetailsAsDictionaryAsync(ILocator rowLocator)
        {
            var result = new Dictionary<string, string>();
            await rowLocator.First.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            await rowLocator.WaitForAsync();
            var cellLocators = rowLocator.Locator("td");
            int cellCount = await cellLocators.CountAsync();

            if (cellCount < 5)
            {
                return result;
            }

            // Map values to keys
            result["Oem"] = (await cellLocators.Nth(0).InnerTextAsync()).Trim();
            result["Item-Id"] = (await cellLocators.Nth(1).InnerTextAsync()).Trim();
            result["Description"] = (await cellLocators.Nth(2).InnerTextAsync()).Trim();

            // Active checkbox (4th column)
            var activeCheckbox = cellLocators.Nth(3).Locator("input[type='checkbox']");
            result["IsActive"] = await activeCheckbox.CountAsync() > 0
                ? (await activeCheckbox.IsCheckedAsync()).ToString().ToLower()
                : "false";

            // Master checkbox (5th column)
            var masterCheckbox = cellLocators.Nth(4).Locator("input[type='checkbox']");
            result["IsMaster"] = await masterCheckbox.CountAsync() > 0
                ? (await masterCheckbox.IsCheckedAsync()).ToString().ToLower()
                : "false";

            return result;
        }
    }
}

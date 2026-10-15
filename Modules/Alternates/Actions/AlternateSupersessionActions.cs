namespace PIMQAAutomation.Modules.Alternates.Actions
{
    using Microsoft.Playwright;
    using NUnit.Framework;
    using PIMQAAutomation.Modules.Alternates.Pages;
    using TechTalk.SpecFlow;

  /// <summary>
    /// Contains business logic and action methods for Alternate Supersession Mode UI operations
    /// </summary>
    public class AlternateSupersessionActions
    {
        private readonly IPage page;
        private readonly AlternatePage alternatePage;

        public AlternateSupersessionActions(IPage page)
{
     this.page = page;
            alternatePage = new AlternatePage(page);
        }

        #region Field Verification

        /// <summary>
      /// Verifies that text fields are enabled in a specified panel
    /// </summary>
        public async Task VerifyTextFieldsEnabledAsync(string panelName, Table table)
        {
      foreach (var menu in table.Rows)
   {
          Assert.True(await alternatePage.GetTextField(panelName.ToLower(), menu[0].ToLower()).IsEnabledAsync(), $"The {menu} text field is not enabled.");
      }
        }

  #endregion

        #region Toggle Operations

        /// <summary>
        /// Verifies display inactive toggle switch state in Supersession Mode
        /// </summary>
        public async Task VerifyToggleSwitchStateAsync(string state)
        {
          Assert.True(await alternatePage.GetDisplayActiveToggleButton(state.ToLower()).IsVisibleAsync());
        }

        #endregion

        #region Checkbox Operations

    /// <summary>
        /// Verifies that a checkbox is in unchecked state
        /// </summary>
        public async Task VerifyCheckboxUncheckedAsync(string checkboxName, string panelName)
   {
var state = await alternatePage.GetClusterToClusterRelationshipIsActiveCheckbox(panelName.ToLower(), checkboxName.ToLower()).IsCheckedAsync();
            Assert.False(state, $"Mismatch for field '{state}': Expected '{state}' but found checked.");
        }

        /// <summary>
    /// Verifies that a checkbox is in checked state
        /// </summary>
  public async Task VerifyCheckboxCheckedAsync(string checkboxName, string panelName)
    {
            var state = await alternatePage.GetClusterToClusterRelationshipIsActiveCheckbox(panelName.ToLower(), checkboxName.ToLower()).IsCheckedAsync();
        Assert.True(state, $"Mismatch for field '{state}': Expected '{state}' but found unchecked.");
  }

        #endregion

        #region Button Operations

        /// <summary>
        /// Verifies that close button is enabled
   /// </summary>
     public async Task VerifyCloseButtonEnabledAsync(string panelName)
   {
      Assert.True(await alternatePage.GetSupersessionAltItemsManagementToolCloseButton().IsEnabledAsync(), "Mismatch for button 'Close': Expected enabled but found disabled.");
        }

        /// <summary>
        /// Clicks the close button
        /// </summary>
        public async Task ClickCloseButtonAsync(string panelName)
        {
   await alternatePage.GetSupersessionAltItemsManagementToolCloseButton().ClickAsync();
        }

    #endregion
    }
}

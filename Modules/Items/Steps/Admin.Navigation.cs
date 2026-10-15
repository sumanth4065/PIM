using PIMQAAutomation.Modules.Common.Constants;
using TechTalk.SpecFlow;

namespace PIMQAAutomation.Modules.Items.Steps
{
   
    public partial class Admin
    {
        [When(@"User navigates to ""(.*)""")]
        public async Task UserSelectsStandardCharcateristicsFromItemConfiguration(string flowName)
        {
            await Task.Delay(TimeConstants.NavigationTaskWait);
            await adminNavigationActions.NavigateFlowAsync(flowName);
        }
    }
}

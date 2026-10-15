using global::PIMQAAutomation.Modules.Common.Constants;
using Microsoft.Playwright;
using PIMQAAutomation.Utilities;
using TechTalk.SpecFlow;
using Utilities;
namespace PIMQAAutomation.Modules.Items.Actions
{
    

    /// <summary>
    /// Contains business logic for Admin navigation operations
    /// </summary>
    public class AdminNavigationActions
    {
        private readonly IPage page;
        private readonly ScenarioContext scenarioContext;
        private readonly FeatureContext featureContext;
        private readonly string navigation = "LeftHamburgerMenuNavigation";

        public AdminNavigationActions(IPage page, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            this.page = page;
            this.scenarioContext = scenarioContext;
            this.featureContext = featureContext;
        }

        public async Task NavigateFlowAsync(string flowName)
        {
            string currentOption = string.Empty;

            try
            {
                var navigationOptions = NavigationReader.GetNavigationOptions(flowName, scenarioContext);
                Console.WriteLine("The Navigation options are loaded");

                //Iterate through each option from Navigation.json
                foreach (var option in navigationOptions)
                {
                    currentOption = option;
                    LocatorReader locatorReader = new LocatorReader(scenarioContext);
                    var menuElement = locatorReader.GetByKey(page, $"{navigation}.HamburgerMenuOption", option);

                    var isExpanded = await menuElement.GetAttributeAsync(AttributeConstants.AriaExpanded);

                    if (string.IsNullOrEmpty(isExpanded) || isExpanded == AttributeConstants.False)
                    {
                        Console.WriteLine($"[Navigation] Attempting to locate and click option: '{option}'");

                        await WebUIHelper.WaitAsync(TimeConstants.NavigationTaskWait);
                        Console.WriteLine("Clicking on the navigation option");
                        await menuElement.ClickAsync();
                        Console.WriteLine("Clicked on the Navigation option");
                        await page.WaitForLoadStateAsync();
                    }
                    else
                    {
                        Console.WriteLine($"[Navigation] Option '{option}' is already expanded/active, skipping click.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.BuildCustomException($"navigate to the option: {currentOption}", ex);
            }
        }
    }
}

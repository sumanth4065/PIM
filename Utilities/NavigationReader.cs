using Microsoft.Playwright;
using Newtonsoft.Json.Linq;
using TechTalk.SpecFlow;
using Utilities;

namespace PIMQAAutomation.Utilities
{
    public class NavigationReader
    {

        private static JObject LoadNavigationData(ScenarioContext scenarioContext)
        {
            string jsonPath = FileUtil.BuildNavigationDataPath(scenarioContext);

            Console.WriteLine($"The generated navigation file path is: {jsonPath}");

            if (!File.Exists(jsonPath))
            {
                throw new FileNotFoundException($"Navigation.json not found at path: {jsonPath}");
            }

            var jsonContent = File.ReadAllText(jsonPath);
            return JObject.Parse(jsonContent);
        }

        public static List<string> GetNavigationOptions(string flowName, ScenarioContext scenarioContext)
        {
            // Load JSON each time — no static variable
            var navigationData = LoadNavigationData(scenarioContext);

            var flowArray = navigationData["Navigation"]?["LeftHamburgerMenuNavigation"]?[flowName] as JArray;
            if (flowArray == null)
            {
                throw new KeyNotFoundException($"Navigation flow '{flowName}' not found.");
            }

            return flowArray.ToObject<List<string>>();
        }
    }
}

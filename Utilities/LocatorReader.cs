using Microsoft.Playwright;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using PIMQAAutomation.Modules.Common.Constants;
using PIMQAAutomation.Utilities;
using PPT.Data.Sql.EntityConfiguration;
using System.IO;
using TechTalk.SpecFlow;

namespace Utilities
{
    public class LocatorReader
    {
        //private static JObject _locators;

        private readonly ScenarioContext scenarioContext;

        public LocatorReader(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        // Initialize and return JObject
        public JObject Initialize()
        {
            //if (_locators != null)
            //    return _locators;
            JObject locators;

            string locatorPath = FileUtil.BuildLocatorPath(scenarioContext);
            if (!File.Exists(locatorPath))
            {
                Console.WriteLine($"[INFO]: Skip if Locators.json not found at path: {locatorPath}");
                return new JObject();
            }
            else
            {
            locators = JObject.Parse(File.ReadAllText(locatorPath));
            return locators;
        }
        }

        public ILocator GetByKey(IPage page, string key)
        {
            JObject locators = Initialize();
            var split = key.Split('.');
            if (split.Length != 2)
            {
                throw new CustomException("Locator key must be in format PageName.ElementName");
            }

            var selector = locators[split[0]]?[split[1]]?.ToString();
            if (string.IsNullOrWhiteSpace(selector))
            {
                throw new CustomException($"Locator not found for key: {key}");
            }

            return page.Locator(selector);
        }

        //public static ILocator GetByKey(IPage page, string key, string replaceString, string replaceStrings = null)
        //{
        //    var split = key.Split('.');
        //    if (split.Length != 2)
        //    {
        //        throw new CustomException("Locator key must be in format PageName.ElementName");
        //    }

        //    var selector = _locators[split[0]]?[split[1]]?.ToString();
        //    if (string.IsNullOrWhiteSpace(selector))
        //    {
        //        throw new CustomException($"Locator not found for key: {key}");
        //    }
        //    selector = selector.Replace("[TEXT]", replaceString);
        //    return page.Locator(selector);
        //}

        public ILocator GetByKey(IPage page, string key, string replaceString, string replaceStrings = null)
        {
            JObject locators = Initialize();
            var split = key.Split('.');
            if (split.Length != 2)
            {
                throw new CustomException("Locator key must be in format PageName.ElementName");
            }

            var selector = locators[split[0]]?[split[1]]?.ToString();
            if (string.IsNullOrWhiteSpace(selector))
            {
                throw new CustomException($"Locator not found for key: {key}");
            }

            selector = selector.Replace("[TEXT]", replaceString);

            if (!string.IsNullOrWhiteSpace(replaceStrings))
            {
                var replacements = replaceStrings.Split(',');
                for (int i = 0; i < replacements.Length; i++)
                {
                    selector = selector.Replace($"[TEXT{i + 1}]", replacements[i].Trim());
                }
            }

            return page.Locator(selector);
        }


        //private static void EnsureLocatorsLoaded()
        //{
        //    if (_locators == null)
        //    {
        //        throw new InvalidOperationException("Locators not initialized. Call LocatorReader.Initialize(scenarioContext) first.");
        //    }
        //}
    }
}
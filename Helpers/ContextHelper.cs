using PIMQAAutomation.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace PIMQAAutomation.Helpers
{
    public class ContextHelper
    {
        public static string GetContextValue(FeatureContext featureContext, string key)
        {
            if (featureContext == null)
                throw new ArgumentNullException(nameof(featureContext));

            var itemIdObj = featureContext[key];
            ValidationHelper.FailIfNullOrEmpty("Verify feature context key", key, $"{key} not found in feature context");
            return itemIdObj.ToString() ?? string.Empty;
        }
    }
}

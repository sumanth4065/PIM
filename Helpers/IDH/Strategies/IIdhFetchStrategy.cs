using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMQAAutomation.Helpers.IDH.Strategies
{
    public interface IIdhFetchStrategy
    {
        Task<object?> FetchAsync(Dictionary<string, string> additionalInfo);
    }
}

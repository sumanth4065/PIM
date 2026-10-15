using PIMQAAutomation.Helpers.DocumentFetcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMQAAutomation.Helpers.EventAudit.Strategies
{
    public interface IEventAuditFetchStrategy
    {
        /// <summary>
        /// Builds and executes the appropriate EventAudit query for a given context.
        /// </summary>
        Task<object?> FetchAsync(ValidationContext context);
    }
}

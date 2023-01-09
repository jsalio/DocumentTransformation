using Core.Models;
using System.Collections.Generic;

namespace Core.Contracts
{
    /// <summary>
    /// Represents responsability of interact with capture Api's for workflows
    /// </summary>
    public interface IWorkflowSource
    {
        /// <summary>
        /// Get all active workflows from capture
        /// </summary>
        /// <returns></returns>
        IEnumerable<Workflow> GetAllActiveWorkflows();
    }

}

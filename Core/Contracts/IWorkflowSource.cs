using Core.Models;
using System.Collections.Generic;

namespace Core.Contracts
{
    /// <summary>
    /// Represents responsibility of interact with capture Api's for workflows
    /// </summary>
    public interface IWorkflowSource
    {
        /// <summary>
        /// Get all active workflows from capture
        /// </summary>
        /// <returns></returns>
        IEnumerable<Workflow> GetAllActiveWorkflows();

        /// <summary>
        /// Get documents types configured in provide workflow
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        WorkflowDocumentTypes GetDocumentTypes(long workflowId);
    }

}

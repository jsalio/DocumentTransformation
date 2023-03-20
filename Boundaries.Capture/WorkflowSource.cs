using Boundaries.Request;
using Core.Contracts;
using Core.Models;
using System;
using System.Collections.Generic;

namespace Boundaries.Capture
{
    /// <summary>
    /// Class for interact with capture API's
    /// </summary>
    public class WorkflowSource : IWorkflowSource
    {
        private readonly CaptureApiEndPoints _apiEndPoint;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="capureApiEndPoints"></param>
        public WorkflowSource(CaptureApiEndPoints capureApiEndPoints)
        {
            _apiEndPoint = capureApiEndPoints;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        IEnumerable<Workflow> IWorkflowSource.GetAllActiveWorkflows()
        {
            var requestExecutor = new ExecuteRequest();
            requestExecutor.AddHeader(new KeyValuePair<string, string>(_apiEndPoint.ApiKeyName, _apiEndPoint.XApiKeyValue));
            var dataSet = requestExecutor.Get<IEnumerable<Workflow>>(_apiEndPoint.Workflow,"/api/Workflows");
            return dataSet;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        WorkflowDocumentTypes IWorkflowSource.GetDocumentTypes(long workflowId)
        {
            var requestExecutor = new ExecuteRequest();
            requestExecutor.AddHeader(new KeyValuePair<string, string>(_apiEndPoint.ApiKeyName, _apiEndPoint.XApiKeyValue));
            var workflow = requestExecutor.Get<WorkflowDocumentTypes>(_apiEndPoint.Workflow,$"/api/Workflows/{workflowId}/document-types");
            return workflow;
        }
    }
}

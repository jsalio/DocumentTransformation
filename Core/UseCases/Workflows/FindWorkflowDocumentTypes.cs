using Core.Contracts;
using Core.Models;
using Optional;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.UseCases.Workflows
{
    /// <summary>
    /// 
    /// </summary>
    public class FindWorkflowDocumentTypes
    {
        private readonly IRequest<long> _request;
        private readonly IWorkflowRepository _workflowRepository;
        private readonly IWorkflowSource _source;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workflowSource"></param>
        /// <param name="workflowRepository"></param>
        /// <param name="workflowIdRequest"></param>
        public FindWorkflowDocumentTypes(IWorkflowSource workflowSource, IWorkflowRepository workflowRepository , IRequest<long> workflowIdRequest)
        {
            _request = workflowIdRequest;
            _workflowRepository = workflowRepository;
            _source = workflowSource;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate()
        {
            var id = _request.BuildRequest();
            if (id <= 0)
            {
                yield return new ValidationResult("Invalid workflow id");
            }

            if (!_workflowRepository.Exists(x => x.Handle == id))
            {
                yield return new ValidationResult("ThisWorkflowIsNotAuthorize");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Option<WorkflowDocumentTypes, Exception> Execute()
        {
            var workflowId = _request.BuildRequest();
            try
            {
                var result = _source.GetDocumentTypes(workflowId);
                return Option.Some<WorkflowDocumentTypes, Exception>(result);
            }
            catch (Exception e)
            {
                return Option.None<WorkflowDocumentTypes, Exception>(e);
            }
        }
    }
}
using Core.Contracts;
using Core.Models;
using Optional;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.UseCases.Workflows
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class SaveDocumentType
    {
        private readonly IRequest<IEnumerable<DocumentConvertSetting>> _request;
        private readonly IWorkflowRepository _repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workflowRepository"></param>
        /// <param name="request"></param>
        public SaveDocumentType(IWorkflowRepository workflowRepository, IRequest<IEnumerable<DocumentConvertSetting>> request)
        {
            _request = request;
            _repository = workflowRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate()
        {
            var request = _request.BuildRequest();
            if (request == null)
            {
                yield return new ValidationResult("InvalidRequest");
            }

            if (request.Any(x => x.DocumentTypeId <= 0))
            {
                yield return new ValidationResult("InvalidDocumentType");
            }

            if (request.Any(x => string.IsNullOrWhiteSpace(x.DocumentTypeName)))
            {
                yield return new ValidationResult("InvalidDocumentType");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Option<Task<Workflow>, Exception> Execute()
        {
            var request = _request.BuildRequest();
            try
            {
                var result = _repository.SaveDocumentTypeSetting(request);
                return Option.Some<Task<Workflow>, Exception>(result);
            }
            catch (Exception e)
            {
                return Option.None<Task<Workflow>, Exception>(e);
            }
        }
    }
}

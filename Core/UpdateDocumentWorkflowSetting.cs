using Core.Contracts;
using Core.Models;
using Optional;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Core
{
    public class UpdateDocumentWorkflowSetting
    {
        private readonly IWorkflowRepository _workflowRepository;
        private readonly IRequest<IEnumerable<DocumentConvertSettingModel>> _request;

        public UpdateDocumentWorkflowSetting(IWorkflowRepository workflowRepository,IRequest<IEnumerable<DocumentConvertSettingModel>> request)
        {
            _workflowRepository = workflowRepository;
            _request = request;
        }

        public IEnumerable<ValidationResult> Validate()
        {
            var request = _request.BuildRequest();
            if (request == null)
            {
                yield return new ValidationResult("Request is empty");
            }
            foreach (var settingModel in request)
            {
                if (settingModel.WorkflowId == 0 || !_workflowRepository.Exists(x => x.Handle == settingModel.WorkflowId))
                {
                   yield return new ValidationResult("invalid workflow");
                }

                if (settingModel.DocumentTypeId == 0)
                {
                    yield return new ValidationResult("invalid documentType");
                }
            }
        }


        public Option<Task<int>, Exception> Execute()
        {
            IEnumerable<DocumentConvertSettingModel> request = _request.BuildRequest();
            try
            {
                var result = _workflowRepository.UpdateDocumentTypeSettings(request);
                return Option.Some<Task<int>, Exception>(result);
            }
            catch (Exception e)
            {
                return Option.None<Task<int>, Exception>(e);
            }
        }
    }
}
using Core.Contracts;
using Core.Models;
using Optional;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.UseCases.Workflows
{
    public sealed class GetWorkflowSettings
    {
        private readonly IWorkflowRepository _repository;

        public GetWorkflowSettings(IWorkflowRepository workflowRepository)
        {
            _repository = workflowRepository;
        }

        public Option<Task<List<Workflow>>, Exception> Execute()
        {
            try
            {
                var dataSet = _repository.GetAllSettings();
                return Option.Some<Task<List<Workflow>>, Exception>(dataSet);
            }
            catch (Exception e)
            {
                return Option.None<Task<List<Workflow>>, Exception>(e);
            }
        }
    }
}

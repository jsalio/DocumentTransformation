using Core.Contracts;
using Core.Models;
using Optional;
using System;
using System.Collections.Generic;

namespace Core.UseCase.Workflows
{
    public sealed class GetAllWorkflows
    {
        private readonly IWorflowStore _store;
        private readonly IWorkflowSource _captureWorkflowStore;

        public GetAllWorkflows(IWorflowStore store, IWorkflowSource _captureStore)
        {
            _store = store;
            _captureWorkflowStore = _captureStore;
        }

        public Option<IEnumerable<Workflow>, Exception> Execute()
        {
            try
            {
                var query = _captureWorkflowStore.GetAllActiveWorkflows();
                return Option.Some<IEnumerable<Workflow>, Exception>(query);
            }
            catch (Exception e)
            {

                return Option.None<IEnumerable<Workflow>, Exception>(e);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using Core.Contracts;
using Core.Models;
using Optional;

namespace Core.UseCases.Workflows
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GetAllWorkflows
    {
        private readonly IWorkflowRepository _store;
        private readonly IWorkflowSource _captureWorkflowStore;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="store"></param>
        /// <param name="_captureStore"></param>
        public GetAllWorkflows(IWorkflowRepository store, IWorkflowSource _captureStore)
        {
            _store = store;
            _captureWorkflowStore = _captureStore;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

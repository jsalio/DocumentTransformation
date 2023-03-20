using Core.Contracts;
using Core.Models;
using Optional;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.UseCases.Workflows
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GetActiveWorkflows
    {
        private readonly IWorkflowRepository _store;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="store"></param>
        public GetActiveWorkflows(IWorkflowRepository store)
        {
            _store = store;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Option<IEnumerable<Workflow>, Exception> Execute()
        {
            try
            {
                var query = _store.GetAll().GetAwaiter().GetResult();
                return Option.Some<IEnumerable<Workflow>, Exception>(query);
            }
            catch (Exception e)
            {

                return Option.None<IEnumerable<Workflow>, Exception>(e);
            }
        }
    }
}

using Core.Contracts;
using Core.Models;
using Optional;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.UseCase.Workflows
{
    public sealed class GetActiveWorkflows
    {
        private readonly IWorflowStore _store;

        public GetActiveWorkflows(IWorflowStore store)
        {
            _store = store;
        }

        public Option<IEnumerable<Workflow>, Exception> Execute()
        {
            try
            {
                var query = _store.GetAllActiveWorkflows();
                return Option.Some<IEnumerable<Workflow>, Exception>(query);
            }
            catch (Exception e)
            {

                return Option.None<IEnumerable<Workflow>, Exception>(e);
            }
        }
    }
}

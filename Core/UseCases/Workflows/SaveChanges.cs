using Core.Contracts;
using Core.Models;
using Optional;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.UseCase.Workflows
{
    public sealed class SaveChanges
    {
        private readonly IWorkflowRepository _store;
        private readonly IRequest<IEnumerable<Workflow>> _request;

        public SaveChanges(IWorkflowRepository store, IRequest<IEnumerable<Workflow>> request)
        {
            _store = store;
            _request = request;
        }

        public IEnumerable<ValidationResult> Validate()
        {
            var request = _request.BuildRequest();
            if (request == null)
            {
                yield return new ValidationResult("InvalidRequest", new[] { nameof(request) });
            }
        }

        public Option<string, Exception> Execute()
        {
            var request = _request.BuildRequest();
            try
            {
                var store = _store.SaveMany(request).GetAwaiter().GetResult();
                return Option.Some<string, Exception>(store.ToString());
            }
            catch (Exception e)
            {

                return Option.None<string, Exception>(e);
            }
        }
    }
}

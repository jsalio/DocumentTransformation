using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Core.Contracts;
using Core.Models.Attempts;
using Core.Models.Exceptions;
using Optional;

namespace Core.UseCases.Attempt
{
    public sealed class AddAttempt
    {
        private readonly IRequest<AddAttemptRequest> _request;
        private readonly IAttemptStore _attemptStore;

        public AddAttempt(IAttemptStore attemptStore, IRequest<AddAttemptRequest> request)
        {
            _request = request;
            _attemptStore = attemptStore;
        }

        public IEnumerable<ValidationResult> Validate()
        {
            var request = _request.BuildRequest();
            if (request == null)
            {
                yield return new ValidationResult("InvalidRequest");
            }

            if (string.IsNullOrWhiteSpace(request.Message))
            {
                yield return new ValidationResult("The attempt not contains description");
            }

            if (request.BatchId == 0)
            {
                yield return new ValidationResult("The attempt reference batch is invalid");
            }

            if (request.DocumentHandler == 0)
            {
                yield return new ValidationResult("The attempt document reference is invalid");
            }
        }

        public Option<Task<int>,StoreException> Execute()
        {
            var request = _request.BuildRequest();
            try
            {
                var query = _attemptStore.AddAttempt(request);
                return Option.Some<Task<int>, StoreException>(query);
            }
            catch (StoreException e)
            {
                return Option.None<Task<int>, StoreException>(e);
            }
        }
    }
}

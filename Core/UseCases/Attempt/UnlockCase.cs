using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Contracts;
using Core.Models.Attempts;
using Core.Models.Exceptions;
using Optional;

namespace Core.UseCases.Attempt
{
    public sealed class UnlockCase
    {
        private readonly IRequest<IEnumerable<UpdateCaseAttempt>> _request;

        private readonly IAttemptStore _attemptStore;

        public UnlockCase(IAttemptStore attemptStore, IRequest<IEnumerable<UpdateCaseAttempt>> request)
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

            var listOfCases = request.ToArray();
            for (var index = 0; index < request.Count(); index++)
            {
                if (_attemptStore.Exists(a => a.Id == listOfCases[index].CaseId ).Result)
                {
                    continue;
                }
                yield return new ValidationResult($"Case {listOfCases[index].CaseId} not found");
            }
        }

        public Option<Task<int>, StoreException> Execute()
        {
            var request = _request.BuildRequest();
            try
            {
                var query = _attemptStore.UnlockCase(request);
                return Option.Some<Task<int>, StoreException>(query);
            }
            catch (StoreException e)
            {
                return Option.None<Task<int>, StoreException>(e);
            }
        }
    }
}

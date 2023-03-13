using Core.Contracts;
using Core.Models.Attempts;
using Core.Models.Exceptions;
using Optional;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Core.UseCases.Attempt
{
    public sealed class GetCaseDetails
    {
        private readonly IRequest<long> _request;
        private readonly IAttemptStore _attemptStore;

        public GetCaseDetails(IAttemptStore attemptStore, IRequest<long> request)
        {
            _request = request;
            _attemptStore = attemptStore;
        }

        public IEnumerable<ValidationResult> Validate()
        {
            var request = _request.BuildRequest();
            if (request == 0)
            {
                yield return new ValidationResult("");
            }
        }

        public Option<Task<List<AttemptDetail>>, StoreException> Execute()
        {
            var caseId = _request.BuildRequest();
            try
            {
                var dataSet = _attemptStore.GetCaseDetails(caseId);
                return Option.Some<Task<List<AttemptDetail>>, StoreException>(dataSet);
            }
            catch (StoreException e)
            {
                return Option.None<Task<List<AttemptDetail>>, StoreException>(e);
            }
        }
    }
}

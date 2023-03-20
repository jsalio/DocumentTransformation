using Core.Contracts;
using Core.Models.Exceptions;
using Optional;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.UseCases.Attempt
{
    public sealed class GetAttemptList
    {
        private readonly IAttemptStore _attemptStore;

        public GetAttemptList(IAttemptStore attemptStore)
        {
            _attemptStore = attemptStore;
        }

        public Option<Task<List<Models.Attempts.AttemptView>>, StoreException> Execute()
        {
            try
            {
                var query = _attemptStore.GetAllCases();
                return Option.Some<Task<List<Models.Attempts.AttemptView>>, StoreException>(query);
            }
            catch (StoreException e)
            {
                return Option.None<Task<List<Models.Attempts.AttemptView>>, StoreException>(e);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Contracts;
using Core.Models.Exceptions;
using Optional;

namespace Core.UseCases.Attempt
{
    public sealed class GetAttemptList
    {
        private readonly IAttemptStore _attemptStore;

        public GetAttemptList(IAttemptStore attemptStore)
        {
            _attemptStore = attemptStore;
        }

        public Option<Task<List<Models.Attempts.Attempt>>, StoreException> Execute()
        {
            try
            {
                var query = _attemptStore.GetAllCases();
                return Option.Some<Task<List<Models.Attempts.Attempt>>, StoreException>(query);
            }
            catch (StoreException e)
            {
                return Option.None<Task<List<Models.Attempts.Attempt>>, StoreException>(e);
            }
        }
    }
}

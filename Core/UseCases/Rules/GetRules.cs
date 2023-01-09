using Core.Contracts;
using Core.Models;
using Optional;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.UseCase.Rules
{
    public class GetRules
    {
        private readonly IServiceRule _store;

        public GetRules(IServiceRule ruleStore)
        {
            _store = ruleStore;
        }

        public Option<ApplicationRules, Exception> Execute()
        {
            try
            {
                var dataSet = _store.GetRules();
                return Option.Some<ApplicationRules, Exception>(dataSet);
            }
            catch (Exception e)
            {

                return Option.None<ApplicationRules, Exception>(e);
            }
        }
    }
}

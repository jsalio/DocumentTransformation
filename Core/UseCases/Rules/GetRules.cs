using Core.Contracts;
using Core.Models;
using Optional;
using System;

namespace Core.UseCases.Rules
{
    /// <summary>
    /// 
    /// </summary>
    public class GetRules
    {
        private readonly IRuleRepository _store;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ruleStore"></param>
        public GetRules(IRuleRepository ruleStore)
        {
            _store = ruleStore;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Option<ApplicationRules, Exception> Execute()
        {
            try
            {
                var dataSet = _store.GetAll().Result;
                return Option.Some<ApplicationRules, Exception>(dataSet);
            }
            catch (Exception e)
            {

                return Option.None<ApplicationRules, Exception>(e);
            }
        }
    }
}

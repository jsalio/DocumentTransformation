using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.Contracts;
using Core.Models;
using Optional;

namespace Core.UseCases.Rules
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class SaveChanges
    {
        private readonly IRuleRepository _store;
        private readonly IRequest<ApplicationRules> _request;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ruleStore"></param>
        /// <param name="rules"></param>
        public SaveChanges(IRuleRepository ruleStore, IRequest<ApplicationRules> rules)
        {
            _store = ruleStore;
            _request = rules;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Option<string, Exception> Execute()
        {
            var request = _request.BuildRequest();
            try
            {
                var ruleSet = CastToRule(request);
                var result = _store.Update(ruleSet);
                return Option.Some<string, Exception>("Ok");
            }
            catch (Exception e)
            {

                return Option.None<string, Exception>(e);
            }
        }

        private IEnumerable<Rule> CastToRule(ApplicationRules rules)
        {
            IList<Rule> result = new List<Rule>();
            foreach (var rule in rules.GetType().GetProperties())
            {
                result.Add(new Rule
                {
                    Name = rule.Name,
                    Value = GetPropValue(rules, rule.Name).ToString(),
                });
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static object GetPropValue(object source, string propertyName)
        {
            var property = source.GetType().GetRuntimeProperties().FirstOrDefault(p => string.Equals(p.Name, propertyName, StringComparison.OrdinalIgnoreCase));
            return property?.GetValue(source);
        }
    }
}

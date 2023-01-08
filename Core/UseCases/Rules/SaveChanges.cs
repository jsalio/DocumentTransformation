using Core.Contracts;
using Core.Models;
using Optional;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.UseCase.Rules
{
    public sealed class SaveChanges
    {
        private readonly IServiceRule _store;
        private readonly IRequest<ApplicationRules> _request;

        public SaveChanges(IServiceRule ruleStore, IRequest<ApplicationRules> rules)
        {
            _store = ruleStore;
            _request = rules;
        }

        public Option<string, Exception> Execute()
        {
            var request = _request.BuildRequest();
            try
            {
                var result = _store.Save(request);
                return Option.Some<string, Exception>("Ok");
            }
            catch (Exception e)
            {

                return Option.None<string, Exception>(e);
            }
        }
    }
}

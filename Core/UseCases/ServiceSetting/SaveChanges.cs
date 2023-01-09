using Core.Contracts;
using Core.Models;
using Optional;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.UseCase.ServiceSetting
{
    public class SaveChanges
    {
        private readonly IServiceConfigStore _store;
        private readonly IRequest<ServiceSettings> _request;

        public SaveChanges(IServiceConfigStore serviceConfig, IRequest<ServiceSettings> request)
        {
            _store = serviceConfig;
            _request = request;
        }

        public IEnumerable<ValidationResult> Validations()
        {
            var request = _request.BuildRequest();
            if (request == null)
            {
                yield return new ValidationResult("TheRequesIsNull", new[] { nameof(request) });
            }
        }

        public Option<string, Exception> Execute()
        {
            var request = _request.BuildRequest();
            try
            {
                var result = _store.Save(request);
                return Option.Some<string, Exception>(result);
            }
            catch (Exception e)
            {

                return Option.None<string, Exception>(e);
            }
        }
    }
}

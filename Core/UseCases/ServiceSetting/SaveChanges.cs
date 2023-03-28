using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Core.Contracts;
using Core.Models;
using Optional;

namespace Core.UseCases.ServiceSetting
{
    /// <summary>
    /// 
    /// </summary>
    public class SaveChanges
    {
        private readonly IServiceConfigStore _store;
        private readonly IRequest<ServiceSettings> _request;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceConfig"></param>
        /// <param name="request"></param>
        public SaveChanges(IServiceConfigStore serviceConfig, IRequest<ServiceSettings> request)
        {
            _store = serviceConfig;
            _request = request;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validations()
        {
            var request = _request.BuildRequest();
            if (request == null)
            {
                yield return new ValidationResult("TheRequesIsNull", new[] { nameof(request) });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Option<Task<int>, Exception> Execute()
        {
            var request = _request.BuildRequest();
            try
            {
                var result = _store.Save(request);
                return Option.Some<Task<int>, Exception>(result);
            }
            catch (Exception e)
            {

                return Option.None<Task<int>, Exception>(e);
            }
        }
    }
}

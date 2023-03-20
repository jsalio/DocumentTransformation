using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Contracts;
using Core.Models.Exceptions;
using Optional;

namespace Core.UseCases.Queue
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class SaveChanges
    {
        private readonly IQueueSource _store;
        private readonly IRequest<Models.Queue> _request;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="request"></param>
        public SaveChanges(IQueueSource source, IRequest<Models.Queue> request)
        {
            _store = source;
            _request = request;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validations()
        {
            Models.Queue request = _request.BuildRequest();
            if (request == null)
            {
                yield return new ValidationResult("Request is null", new[] { nameof(request) });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Option<string, CoreException> Execute()
        {
            var request = _request.BuildRequest();
            try
            {
                var response = _store.Save(request);
                return Option.Some<string, CoreException>(response);
            }
            catch (Exception e)
            {

                return Option.None<string, CoreException>((CoreException)e);
            }
        }
    }
}

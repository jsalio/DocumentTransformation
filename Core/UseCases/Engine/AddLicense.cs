using Core.Contracts;
using Core.Models;
using Core.Models.Exceptions;
using Optional;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Core.UseCases.Engine
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class AddLicense
    {
        private readonly IRequest<EngineLicenseRequest> _request;
        private readonly IServiceEngine _repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="engineRepository"></param>
        /// <param name="request"></param>
        public AddLicense(IServiceEngine engineRepository, IRequest<EngineLicenseRequest> request)
        {
            _request = request;
            _repository = engineRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate()
        {
            var request = _request.BuildRequest();
            if (request == null)
            {
                yield return new ValidationResult("");
            }

            if (request.EngineId == 0)
            {
                yield return new ValidationResult("");
            }

            if (!_repository.Exists(x => x.Id == request.EngineId))
            {
                yield return new ValidationResult("");
            }

            if (string.IsNullOrWhiteSpace(request.LicenseString))
            {
                yield return new ValidationResult("");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Option<Task<EngineView>, StoreException> Execute()
        {
            var request = _request.BuildRequest();
            
            try
            {
                var result = _repository.AddLicense(request);
                return Option.Some<Task<EngineView>, StoreException>(result);
            }
            catch (StoreException e)
            {
                return Option.None<Task<EngineView>, StoreException>(e);
            }
        }
    }
}

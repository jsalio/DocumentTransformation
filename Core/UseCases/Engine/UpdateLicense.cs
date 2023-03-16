using Core.Contracts;
using Core.Models;
using Core.Models.Exceptions;
using Optional;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Core.UseCases.Engine
{
    public sealed class UpdateLicense
    {
        private readonly IRequest<EngineLicenseRequest> _request;
        private readonly IServiceEngine _repository;

        public UpdateLicense(IServiceEngine engineRepository, IRequest<EngineLicenseRequest> request)
        {
            _request = request;
            _repository = engineRepository;
        }

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

        public Option<Task<EngineView>, StoreException> Execute()
        {
            var request = _request.BuildRequest();
            
            try
            {
                var result = _repository.UpdateLicense(request);
                return Option.Some<Task<EngineView>, StoreException>(result);
            }
            catch (StoreException e)
            {
                return Option.None<Task<EngineView>, StoreException>(e);
            }
        }
    }
}

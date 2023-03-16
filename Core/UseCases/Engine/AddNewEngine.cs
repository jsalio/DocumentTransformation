using Core.Contracts;
using Core.Models;
using Core.Models.Exceptions;
using Optional;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Core.UseCases.Engine
{
    public sealed class AddNewEngine
    {
        private readonly IServiceEngine _engineRepository;

        private readonly IRequest<EngineRequest> _request;

        public AddNewEngine(IServiceEngine engineRepository, IRequest<EngineRequest>request)
        {
            _engineRepository = engineRepository;
            _request = request;
        }

        public IEnumerable<ValidationResult> Validate()
        {
            var request = _request.BuildRequest();
            if (request == null)
            {
                yield return new ValidationResult("Invalid request");
            }

            if (System.Enum.IsDefined(typeof(EngineType), request.EngineType))
            {
                yield return new ValidationResult("Invalid request");
            }

            if (System.Enum.IsDefined(typeof(EngineTypeName), request.EngineTypeName))
            {
                yield return new ValidationResult("Invalid request");
            }

            if (System.Enum.IsDefined(typeof(LicenseType), request.LicenseType))
            {
                yield return new ValidationResult("Invalid request");
            }

            if (string.IsNullOrWhiteSpace(request.EngineName))
            {
                yield return new ValidationResult("Invalid request");
            }
        }

        public Option<Task<int>, StoreException> Execute()
        {
            var request = _request.BuildRequest();
            try
            {
                var result = _engineRepository.AddEngine(request);
                return Option.Some<Task<int>, StoreException>(result);
            }
            catch (StoreException e)
            {
                return Option.None<Task<int>, StoreException>(e);
            }
        }
    }
}

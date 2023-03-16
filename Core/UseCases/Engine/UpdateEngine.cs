using Core.Contracts;
using Core.Models;
using Core.Models.Exceptions;
using Optional;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Core.UseCases.Engine
{
    public sealed class UpdateEngine
    {
        private readonly IRequest<EngineRequest> _request;
        private readonly IRequest<int> _engineId;
        private readonly IServiceEngine _engineRepository;

        public UpdateEngine(IServiceEngine engineRepository, IRequest<EngineRequest> request, IRequest<int> engineId)
        {
            _request = request;
            _engineId = engineId;
            _engineRepository = engineRepository;
        }

        public IEnumerable<ValidationResult> Validate()
        {
            var request = _request.BuildRequest();
            var engineId = _engineId.BuildRequest();
            if (request == null)
            {
                yield return new ValidationResult("Invalid request");
            }

            if (!_engineRepository.Exists(x => x.Id == engineId))
            {
                yield return new ValidationResult("Invalid request");
            }

            //if (System.Enum.IsDefined(typeof(EngineType), request.EngineType))
            //{
            //    yield return new ValidationResult("Invalid request");
            //}

            //if (System.Enum.IsDefined(typeof(EngineTypeName), request.EngineTypeName))
            //{
            //    yield return new ValidationResult("Invalid request");
            //}

            //if (System.Enum.IsDefined(typeof(LicenseType), request.LicenseType))
            //{
            //    yield return new ValidationResult("Invalid request");
            //}

            if (string.IsNullOrWhiteSpace(request.EngineName))
            {
                yield return new ValidationResult("Invalid request");
            }
        }

        public Option<Task<int>, StoreException> Execute()
        {
            var request = _request.BuildRequest();
            var engineId = _engineId.BuildRequest();
            try
            {
                var result = _engineRepository.UpdateEngine(request,engineId);
                return Option.Some<Task<int>, StoreException>(result);
            }
            catch (StoreException e)
            {
                return Option.None<Task<int>, StoreException>(e);
            }
        }
    }
}

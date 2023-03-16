using Core.Contracts;
using Core.Models;
using Core.Models.Exceptions;
using Optional;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Core.UseCases.Engine
{
    public sealed class GetLicense
    {
        private readonly IServiceEngine _repository;
        private readonly IRequest<int> _request;

        public GetLicense(IServiceEngine engineRepository , IRequest<int> request)
        {
            _repository = engineRepository;
            _request = request;
        }

        public IEnumerable<ValidationResult> Validate()
        {
            var request = _request.BuildRequest();
            if (request == 0)
            {
                yield return new ValidationResult("");
            }

            if (!_repository.Exists(x => x.Id == request))
            {
                yield return new ValidationResult("");
            }
        }

        public Option<Task<EngineLicense>,StoreException> Execute()
        {
            var request = _request.BuildRequest();
            try
            {
                var dataSet = _repository.GetLicense(request);
                return Option.Some<Task<EngineLicense>,StoreException>(dataSet);
            }
            catch (StoreException e)
            {
                return Option.None<Task<EngineLicense>,StoreException>(e);
            }
        }

        
    }
}

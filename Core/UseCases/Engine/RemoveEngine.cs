using Core.Contracts;
using Core.Models.Exceptions;
using Optional;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Core.UseCases.Engine
{
    public sealed class RemoveEngine
    {
        private readonly IRequest<int> _request;
        private readonly IServiceEngine _repository;

        public RemoveEngine(IServiceEngine engineRepository, IRequest<int> request)
        {
            _request = request;
            _repository = engineRepository;
        }

        public IEnumerable<ValidationResult> Validate()
        {
            var request = _request.BuildRequest();
            
            if (request == 0)
            {
                yield return new ValidationResult("Invalid request");
            }

            if (!_repository.Exists(x => x.Id == request))
            {
                yield return new ValidationResult("Invalid request");
            }
        }

        public Option<Task<int>, StoreException> Execute()
        {
            var request = _request.BuildRequest();
            
            try
            {
                var result = _repository.RemoveEngine(request);
                return Option.Some<Task<int>, StoreException>(result);
            }
            catch (StoreException e)
            {
                return Option.None<Task<int>, StoreException>(e);
            }
        }
    }
}

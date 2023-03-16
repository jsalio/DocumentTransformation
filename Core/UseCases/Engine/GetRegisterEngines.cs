using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Contracts;
using Core.Models;
using Core.Models.Exceptions;
using Optional;

namespace Core.UseCases.Engine
{
    public sealed class GetRegisterEngines
    {
        private readonly IServiceEngine _repository;

        public GetRegisterEngines(IServiceEngine engineRepository)
        {
            _repository = engineRepository;
        }

        public Option<Task<List<EngineView>>,StoreException> Execute()
        {
            try
            {
                var dataSet = _repository.GetEngines();
                return Option.Some<Task<List<EngineView>>,StoreException>(dataSet);
            }
            catch (StoreException e)
            {
                return Option.None<Task<List<EngineView>>,StoreException>(e);
            }
        }
    }
}

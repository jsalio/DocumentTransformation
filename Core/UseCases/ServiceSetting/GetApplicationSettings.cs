using Core.Contracts;
using Core.Models;
using Core.Models.Exceptions;
using Optional;
using System;

namespace Core.UseCase.ServiceSetting
{
    public sealed class GetApplicationSettings
    {
        private readonly IServiceConfigStore _store;

        public GetApplicationSettings(IServiceConfigStore store)
        {
            _store = store;
        }

        public Option<ServiceSettings, CoreException> Execute()
        {
            try
            {
                var query = _store.GetSettings();
                return Option.Some<ServiceSettings, CoreException>(query);
            }
            catch (Exception e)
            {

                return Option.None<ServiceSettings, CoreException>((CoreException)e);
            }
        }
    }
}

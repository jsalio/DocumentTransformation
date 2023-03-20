using System;
using Core.Contracts;
using Core.Models;
using Core.Models.Exceptions;
using Optional;

namespace Core.UseCases.ServiceSetting
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GetApplicationSettings
    {
        private readonly IServiceConfigStore _store;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="store"></param>
        public GetApplicationSettings(IServiceConfigStore store)
        {
            _store = store;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

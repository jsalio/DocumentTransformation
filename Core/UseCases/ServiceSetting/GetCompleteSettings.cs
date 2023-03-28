using System;
using System.Collections.Generic;
using System.Text;
using Core.Contracts;
using Core.Models;
using Optional;

namespace Core.UseCases.ServiceSetting
{
    public sealed class GetCompleteSettings
    {
        private readonly IServiceConfigStore _storeSettings;
        
        public GetCompleteSettings(IServiceConfigStore storeSettings)
        {
            _storeSettings = storeSettings;
        }

        public Option<ApplicationSettings, Exception> Execute()
        {
            try
            {
                ApplicationSettings result= _storeSettings.GetApplicartionSetting();
                return Option.Some<ApplicationSettings, Exception>(result);
            }
            catch (Exception e)
            {
                return Option.None<ApplicationSettings, Exception>(e);
            }
        }
    }
}

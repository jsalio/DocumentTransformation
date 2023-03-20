using Core.Contracts;
using Core.Models;
using Core.UseCases.ServiceSetting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Optional.Unsafe;

namespace DocumentTransformation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceSettingController  : BaseController, IRequest<ServiceSettings>
    {
        private ServiceSettings _settings;
        private readonly IServiceConfigStore _storeSettings;

        public ServiceSettingController(IServiceConfigStore serviceConfig)
        {
            _storeSettings = serviceConfig;
        }

        [Route("save")]
        [HttpPost]
        public ActionResult SaveChanges([FromBody] ServiceSettings settings)
        {
            _settings = settings;
            SaveChanges saveChanges = new SaveChanges(_storeSettings, this);
            saveChanges.Validations();
            var result = saveChanges.Execute();
            ValidateResult(result);
            return Ok();
        }

        [Route("service-settings")]
        [HttpGet]
        public ActionResult GetConfguration()
        {
            var getAppSettings = new GetApplicationSettings(_storeSettings);
            var result = getAppSettings.Execute();
            ValidateResult(result);
            return Ok(result.ValueOrFailure());
        }

        ServiceSettings IRequest<ServiceSettings>.BuildRequest()
         => _settings;
    }
}

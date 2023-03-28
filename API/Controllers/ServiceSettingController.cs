using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Contracts;
using Core.Models;
using Core.UseCases.Engine;
using Core.UseCases.Rules;
using Core.UseCases.ServiceSetting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Optional.Unsafe;
using SaveChanges = Core.UseCases.ServiceSetting.SaveChanges;

namespace DocumentTransformation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceSettingController : BaseController, IRequest<ServiceSettings>
    {
        private ServiceSettings _settings;
        private readonly IServiceConfigStore _storeSettings;
        private readonly IServiceEngine _engineRepository;
        private readonly IRuleRepository _ruleRepository;

        public ServiceSettingController(IServiceConfigStore serviceConfig, IServiceEngine engineRepository, IRuleRepository ruleRepository)
        {
            _storeSettings = serviceConfig;
            _engineRepository = engineRepository;
            _ruleRepository = ruleRepository;
        }

        [Route("save")]
        [HttpPost]
        public async Task<ActionResult> SaveChanges([FromBody] ServiceSettings settings)
        {
            _settings = settings;
            SaveChanges saveChanges = new SaveChanges(_storeSettings, this);
            saveChanges.Validations();
            var result = saveChanges.Execute();
            ValidateResult(result);
            await result.ValueOrFailure();
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

        [Route("")]
        [HttpGet]
        public ActionResult GetApplicationConfiguration()
        {
            var getAppSettings = new GetCompleteSettings(_storeSettings);
            var result = getAppSettings.Execute();
            ValidateResult(result);
            return Ok(result.ValueOrFailure());
        }

        ServiceSettings IRequest<ServiceSettings>.BuildRequest()
         => _settings;
    }

   
}

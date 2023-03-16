using Core.Contracts;
using Core.Models;
using Core.UseCases.Engine;
using Microsoft.AspNetCore.Mvc;
using Optional.Unsafe;
using System.Threading.Tasks;

namespace DocumentTransformation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EngineController : BaseController, IRequest<EngineRequest>, IRequest<EngineLicenseRequest>, IRequest<int>
    {
        private readonly IServiceEngine _engineRepository;
        private EngineRequest _engineRequest;
        private EngineLicenseRequest _engineLicenseRequest;
        private int _engineId;

        public EngineController(IServiceEngine engineRepository)
        {
            _engineRepository = engineRepository;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult> GetStoredEngine()
        {
            GetRegisterEngines getRegister = new GetRegisterEngines(_engineRepository);
            var result = getRegister.Execute();
            ValidateResult(result);
            var dataSet = await result.ValueOrFailure();
            return Ok(dataSet);
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> RegisterEngine([FromBody] EngineRequest request)
        {
            _engineRequest = request;
            AddNewEngine getRegister = new AddNewEngine(_engineRepository, this);
            ValidateRequestResult(getRegister.Validate());
            var result = getRegister.Execute();
            ValidateResult(result);
            var dataSet = await result.ValueOrFailure();
            return Ok(dataSet);
        }

        [HttpPut]
        [Route("update/{engineId}")]
        public async Task<ActionResult> UpdateEngine([FromBody] EngineRequest request, int engineId)
        {
            _engineRequest = request;
            _engineId = engineId;
            UpdateEngine getRegister = new UpdateEngine(_engineRepository, this, this);
            ValidateRequestResult(getRegister.Validate());
            var result = getRegister.Execute();
            ValidateResult(result);
            var dataSet = await result.ValueOrFailure();
            return Ok(dataSet);
        }

        [HttpDelete]
        [Route("remove/{engineId}")]
        public async Task<ActionResult> UpdateEngine(int engineId)
        {
            _engineId = engineId;
            RemoveEngine getRegister = new RemoveEngine(_engineRepository, this);
            ValidateRequestResult(getRegister.Validate());
            var result = getRegister.Execute();
            ValidateResult(result);
            var dataSet = await result.ValueOrFailure();
            return Ok(dataSet);
        }

        [HttpGet]
        [Route("{engineId}/license")]
        public async Task<ActionResult> GetEngineLicense(int engineId)
        {
            _engineId = engineId;
            GetLicense getRegister = new GetLicense(_engineRepository,this);
            ValidateRequestResult(getRegister.Validate());
            var result = getRegister.Execute();
            ValidateResult(result);
            var dataSet = await result.ValueOrFailure();
            return Ok(dataSet);
        }

        [HttpPost]
        [Route("{engineId}/license-update")]
        public async Task<ActionResult> addEngineLicense([FromBody] EngineLicenseRequest request,int engineId)
        {
            _engineLicenseRequest = request;
            AddLicense getRegister = new AddLicense(_engineRepository,this);
            ValidateRequestResult(getRegister.Validate());
            var result = getRegister.Execute();
            ValidateResult(result);
            var dataSet = await result.ValueOrFailure();
            return Ok(dataSet);
        }

        [HttpPut]
        [Route("{engineId}/license-update")]
        public async Task<ActionResult> UpdateEngineLicense([FromBody] EngineLicenseRequest request,int engineId)
        {
            _engineLicenseRequest = request;
            UpdateLicense getRegister = new UpdateLicense(_engineRepository,this);
            ValidateRequestResult(getRegister.Validate());
            var result = getRegister.Execute();
            ValidateResult(result);
            var dataSet = await result.ValueOrFailure();
            return Ok(dataSet);
        }

        EngineRequest IRequest<EngineRequest>.BuildRequest()
            => _engineRequest;

        EngineLicenseRequest IRequest<EngineLicenseRequest>.BuildRequest()
            => _engineLicenseRequest;

        int IRequest<int>.BuildRequest()
            => _engineId;
    }
}

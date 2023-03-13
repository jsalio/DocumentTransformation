using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Contracts;
using Core.Models.Attempts;
using Core.UseCases.Attempt;
using Microsoft.AspNetCore.Mvc;
using Optional;
using Optional.Unsafe;

namespace DocumentTransformation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttemptController : BaseController, IRequest<IEnumerable<UpdateCaseAttempt>> , IRequest<long>
    ,IRequest<AddAttemptRequest>
    {
        private readonly IAttemptStore _attemptStore;
        private IEnumerable<UpdateCaseAttempt> _casesList;
        private long _caseId;
        private AddAttemptRequest _strikeRequet;

        public AttemptController(IAttemptStore attemptStore)
        {
            _attemptStore = attemptStore;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult> GetAttemptList()
        {
            GetAttemptList attempt = new GetAttemptList(_attemptStore);
            var result = attempt.Execute();
            var dataSet = await result.ValueOrFailure();
            return Ok(dataSet);
        }

        [HttpGet]
        [Route("{caseId}/details")]
        public async Task<ActionResult> GetCaseError(long caseId)
        {
            _caseId = caseId;
            GetCaseDetails getDetails = new GetCaseDetails(_attemptStore, this);
            ValidateRequestResult(getDetails.Validate());
            var result = getDetails.Execute();
            ValidateResult(result);
            var resultSet = await result.ValueOrFailure();
            return Ok(resultSet);
        }

        [HttpPut]
        [Route("unlock")]
        public async Task<ActionResult> UnlockCases([FromBody] IEnumerable<long> cases)
        {
            _casesList = BuildCases(cases);
            UnlockCase unlock = new UnlockCase(_attemptStore, this);
            ValidateRequestResult(unlock.Validate());
            var result = unlock.Execute();
            ValidateResult(result);
            var resultSet = await result.ValueOrFailure();
            return Ok(cases);
        }

        [HttpPut]
        [Route("add-strike")]
        public async Task<ActionResult> AddCaseStricke([FromBody] AddAttemptRequest request)
        {
            _strikeRequet = request;
            AddAttempt caseCreate = new AddAttempt(_attemptStore, this);
            ValidateRequestResult(caseCreate.Validate());
            var result = caseCreate.Execute();
            ValidateResult(result);
            var resultSet = await result.ValueOrFailure();
            return Ok(new
            {
                Message = $"Added strike to document {request.DocumentHandler}"
            });
        }

        [HttpPut]
        [Route("lock-direct")]
        public async Task<ActionResult> LockDireclty([FromBody] AddAttemptRequest request)
        {
            _strikeRequet = request;
            DirectLock lockDocument = new DirectLock(_attemptStore, this);
            ValidateRequestResult(lockDocument.Validate());
            var result = lockDocument.Execute();
            ValidateResult(result);
            var resultSet = await result.ValueOrFailure();
            return Ok(new
            {
                Message = $"Added strike to document {request.DocumentHandler}"
            });
        }


        private IEnumerable<UpdateCaseAttempt> BuildCases(IEnumerable<long> cases)
        {
            return cases.Select(x => new UpdateCaseAttempt
            {
                CaseId = x,
                DocumentId = 0
            }).ToList();
        }

        IEnumerable<UpdateCaseAttempt> IRequest<IEnumerable<UpdateCaseAttempt>>.BuildRequest()
            => _casesList;

        long IRequest<long>.BuildRequest()
            => _caseId;

        AddAttemptRequest IRequest<AddAttemptRequest>.BuildRequest()
            => _strikeRequet;
    }
}

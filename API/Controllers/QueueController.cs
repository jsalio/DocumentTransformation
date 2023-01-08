using Core.Contracts;
using Core.Models;
using Core.UseCase.Queue;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Optional.Unsafe;

namespace DocumentTransformation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueController : BaseController, IRequest<Queue>
    {
        private readonly IQueueSource _store;
        Queue Queue;

        public QueueController(IQueueSource source)
        {
            _store = source;
        }

        [Route("save")]
        [HttpPost]
        public ActionResult SaveChanges([FromBody] Queue queue)
        {
            Queue = queue;
            SaveChanges saveChanges = new SaveChanges(_store, this);
            saveChanges.Validations();
            ValidateResult(saveChanges.Execute());
            return Ok();
        }

        [Route("queue-settings")]
        [HttpGet]
        public ActionResult GetConfguration()
        {
            GetQueue get = new GetQueue(_store);
            var result = get.Execute();
            ValidateResult(result);
            return Ok(result.ValueOrFailure());
        }


        Queue IRequest<Queue>.BuildRequest()
        => Queue;
    }
}

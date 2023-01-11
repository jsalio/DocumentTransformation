using Core.Contracts;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Optional.Unsafe;
using System.Collections.Generic;

namespace DocumentTransformation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowController : BaseController,  IRequest<IEnumerable<Workflow>>
    {
        private readonly IWorflowStore _store;
        private readonly IWorkflowSource _captureStore;
        private IEnumerable<Workflow> _request;

        public WorkflowController(IWorflowStore worflowStore,IWorkflowSource workflowSource)
        {
            _store = worflowStore;
            _captureStore = workflowSource;
        }

        [Route("capture-available")]
        [HttpGet]
        public ActionResult GetAllWorkflows()
        {
            Core.UseCase.Workflows.GetAllWorkflows getAll = new Core.UseCase.Workflows.GetAllWorkflows(_store, _captureStore);
            var dataSet = getAll.Execute().ValueOrFailure();
            return Ok(dataSet);
        }

        [Route("white-list")]
        [HttpGet]
        public ActionResult GetWhiteList()
        {
            Core.UseCase.Workflows.GetActiveWorkflows getAll = new Core.UseCase.Workflows.GetActiveWorkflows(_store);
            var dataSet = getAll.Execute().ValueOrFailure();
            return Ok(dataSet);
        }

        [Route("save")]
        [HttpPost]
        public ActionResult SaveChanges([FromBody] IEnumerable<Workflow> workflows)
        {
            _request = workflows;
            Core.UseCase.Workflows.SaveChanges getAll = new Core.UseCase.Workflows.SaveChanges(_store, this);
            var resukt = getAll.Execute().ValueOrFailure();
            return Ok(resukt);
        }

        IEnumerable<Workflow> IRequest<IEnumerable<Workflow>>.BuildRequest()
        {
            return _request;
        }
    }
}

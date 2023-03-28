using Core;
using Core.Contracts;
using Core.Models;
using Core.UseCases.Workflows;
using Microsoft.AspNetCore.Mvc;
using Optional.Unsafe;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentTransformation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowController : BaseController,  IRequest<IEnumerable<Workflow>>, IRequest<long>, IRequest<IEnumerable<DocumentConvertSetting>>, IRequest<IEnumerable<DocumentConvertSettingModel>>
    {
        private readonly IWorkflowRepository _store;
        private readonly IWorkflowSource _captureStore;
        private IEnumerable<DocumentConvertSetting> _dataSet;
        private IEnumerable<Workflow> _request;
        private long workflowId;
        private IEnumerable<DocumentConvertSettingModel> _items;

        public WorkflowController(IWorkflowRepository worflowStore,IWorkflowSource workflowSource)
        {
            _store = worflowStore;
            _captureStore = workflowSource;
        }

        [Route("capture-available")]
        [HttpGet]
        public ActionResult GetAllWorkflows()
        {
            GetAllWorkflows getAll = new GetAllWorkflows(_store, _captureStore);
            var dataSet = getAll.Execute().ValueOrFailure();
            return Ok(dataSet);
        }

        [Route("white-list")]
        [HttpGet]
        public ActionResult GetWhiteList()
        {
           GetActiveWorkflows getAll = new GetActiveWorkflows(_store);
            var dataSet = getAll.Execute().ValueOrFailure();
            return Ok(dataSet);
        }

        [Route("{workflowId}/add")]
        [HttpPost]
        public async Task<ActionResult> AddWorkflowToService(int workflowId)
        {
            this.workflowId = workflowId;
            FindWorkflowDocumentTypes finder = new FindWorkflowDocumentTypes(_captureStore, _store, this);
            ValidateRequestResult(finder.Validate());
            var result = finder.Execute();
            ValidateResult(result);
            _dataSet = result.ValueOrFailure().DocumentTypes.Select(x => new DocumentConvertSetting
            {
                WorkflowId = workflowId,
                ConvertPdf = false,
                DocumentTypeId = x.DocumentTypeId,
                DocumentTypeName = x.DocumentTypeName,
                SupportOcr = false
            });
            if (!_dataSet.Any())
            {
                return Ok();
            }
            SaveDocumentType saveDocumentType = new SaveDocumentType(_store, this);
            ValidateRequestResult(saveDocumentType.Validate());
            var execute = saveDocumentType.Execute();
            ValidateResult(execute);
            var workflow = await execute.ValueOrFailure();
            var l = new List<Workflow>() {workflow};
            return Ok(MapToModel(l));
        }

        [Route("save")]
        [HttpPost]
        public ActionResult SaveChanges([FromBody] IEnumerable<Workflow> workflows)
        {
            _request = workflows;
            SaveChanges getAll = new SaveChanges(_store, this);
            var resukt = getAll.Execute();
            ValidateResult(resukt);
            return Ok(resukt.ValueOrFailure());
        }

        [Route("settings")]
        [HttpGet]
        public async Task<ActionResult> GetAllWorkflowSettings()
        {
            GetWorkflowSettings getSettings = new GetWorkflowSettings(_store);
            var result = getSettings.Execute();
            ValidateResult(result);
            var dataSet = await result.ValueOrFailure();
            return Ok(MapToModel(dataSet));
        }

        [Route("{workflowId}/update-settings")]
        [HttpPut]
        public async Task<ActionResult> UpdateDocumentSettings([FromBody] IEnumerable<DocumentConvertSettingModel> items, long workflowId)
        {
            _items = items;
            UpdateDocumentWorkflowSetting update = new UpdateDocumentWorkflowSetting(_store, this);
            ValidateRequestResult(update.Validate());
            var result = update.Execute();
            ValidateResult(result);
            var confirmationId = await result.ValueOrFailure();
            return Ok(confirmationId);
        }

        private List<WorkflowModel> MapToModel(List<Workflow> dataSet)
        {
            return dataSet.Where(x => x.DocumentConvertSettings.Any()).Select(w => new WorkflowModel
            {
                Id = w.Id,
                WorkflowName = w.Name,
                Description = w.Description,
                WorkflowId = w.Handle,
                DocumentConvertSettings = w.DocumentConvertSettings.Select(dt => new DocumentConvertSettingModel
                {
                    ConvertPdf = dt.ConvertPdf,
                    Id  = dt.Id,
                    SupportOcr = dt.SupportOcr,
                    WorkflowId = dt.WorkflowId,
                    DocumentTypeId = dt.DocumentTypeId,
                    DocumentTypeName = dt.DocumentTypeName
                    
                })

            }).ToList();
        }

        IEnumerable<Workflow> IRequest<IEnumerable<Workflow>>.BuildRequest()
        {
            return _request;
        }

        long IRequest<long>.BuildRequest()
            => workflowId;

        IEnumerable<DocumentConvertSetting> IRequest<IEnumerable<DocumentConvertSetting>>.BuildRequest()
            => _dataSet;

        IEnumerable<DocumentConvertSettingModel> IRequest<IEnumerable<DocumentConvertSettingModel>>.BuildRequest()
            => _items;
    }
}

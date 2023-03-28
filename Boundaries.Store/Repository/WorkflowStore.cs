using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IContract = Core.Contracts.IWorkflowRepository;

namespace Boundaries.Store.Repository
{
    public sealed class WorkflowStore : IContract
    {

        private readonly IApplicationDbContext _context;

        public WorkflowStore(IApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        Task<int> IContract.Delete(int id)
        {
            var query = _context.Workflows.FirstOrDefault(x => x.Handle == id);
            _context.Workflows.Remove(query);
            return _context.SaveChanges();
        }

        Task<int> IContract.DeleteMany(IEnumerable<int> id)
        {
            var query = _context.Workflows.Where(x => id.Contains(x.Handle));
            _context.Workflows.RemoveRange(query);
            return _context.SaveChanges();
        }

        bool IContract.Exists(Expression<Func<Workflow, bool>> expression)
            => _context.Workflows.Any(expression);


        async Task<IEnumerable<Core.Models.Workflow>> IContract.GetAll()
        {
            var query = _context.Workflows;
            return await query.ToListAsync();
        }

        Task<List<Workflow>> IContract.GetAllSettings()
        {
            try
            {
                var query = _context.Workflows.Include(workflow => workflow.DocumentConvertSettings).ToListAsync();
                return query;
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new Exception(e.Message);
            }
            catch (ArgumentException e)
            {
                throw new Exception(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        async Task<Workflow> IContract.SaveDocumentTypeSetting(IEnumerable<DocumentConvertSetting> request)
        {
            try
            {
                var documentType = request;
                var rowWorkflow = _context.Workflows.FirstOrDefault(x => x.Handle == documentType.FirstOrDefault().WorkflowId);
                foreach (var setting in documentType)
                {
                    var settings = new DocumentConvertSetting
                    {
                        DocumentTypeId = setting.DocumentTypeId,
                        ConvertPdf = setting.ConvertPdf,
                        SupportOcr = setting.SupportOcr,
                        DocumentTypeName = setting.DocumentTypeName,
                        WorkflowId = rowWorkflow.Id
                    };
                    _context.DocumentConvertSettings.Add(settings);
                }

                await _context.SaveChanges();
                return await _context.Workflows.Include(x => x.DocumentConvertSettings)
                    .FirstOrDefaultAsync(x => x.Handle == rowWorkflow.Handle);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        Task<int> IContract.SaveMany(IEnumerable<Core.Models.Workflow> workflows)
        {

            try
            {
                var workflowsId = workflows.Select(x => x.Handle).ToList();
                var current = _context.Workflows.Select(x => x.Handle).ToList();
                
                var removeItems = current.Where(x => !workflowsId.Contains(x)).ToList();
                var newItems = workflowsId.Where(x => !current.Contains(x)).ToList();

                var forRemove = _context.Workflows.Where(x => removeItems.Contains(x.Handle));
                var newSettings = workflows.Where(x => newItems.Contains(x.Handle));

                foreach (var workflow in newSettings)
                {
                    _context.Workflows.Add(workflow);
                }
                return _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new Exception(e.Message);
            }
            catch (ArgumentException e)
            {
                throw new Exception(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        Task<int> IContract.UpdateDocumentTypeSettings(IEnumerable<DocumentConvertSettingModel> request)
        {
            try
            {
                var ids = request.Select(x => x.Id);
                var query = _context.DocumentConvertSettings.Where(x => ids.Contains(x.Id));
                foreach (var setting in query)
                {
                    setting.SupportOcr = request.FirstOrDefault(x => x.Id == setting.Id).SupportOcr;
                    setting.ConvertPdf = request.FirstOrDefault(x => x.Id == setting.Id).ConvertPdf;
                }

                return _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new Exception(e.Message);
            }
            catch (ArgumentException e)
            {
                throw new Exception(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

using Core.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IContract = Core.Contracts.IWorkflowRepository;

namespace Boundaries.Store.Repository
{
    public sealed class Workflow : IContract
    {

        private readonly IApplicationDbContext _context;

        public Workflow(IApplicationDbContext dbContext)
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

        async Task<IEnumerable<Core.Models.Workflow>> IContract.GetAll()
        {
            var query = _context.Workflows;
            return await query.ToListAsync();
        }

        Task<int> IContract.SaveMany(IEnumerable<Core.Models.Workflow> workflows)
        {
            foreach (var workflow in workflows)
            {
                _context.Workflows.Add(workflow);
            }
            return _context.SaveChanges();
        }
    }
}

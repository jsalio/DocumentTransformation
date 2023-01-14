using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Boundaries.Store
{
    public interface IApplicationDbContext
    {
        DbSet<Workflow> Workflows { get; set; }
        DbSet<Rule> Rules { get; set; }
        DbSet<ServiceSettings> ServiceSettings{ get; set; }

        Task<int> SaveChanges();
    }
}

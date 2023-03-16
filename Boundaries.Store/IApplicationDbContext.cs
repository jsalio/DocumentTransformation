using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Models.Attempts;

namespace Boundaries.Store
{
    public interface IApplicationDbContext
    {
        DbSet<Workflow> Workflows { get; set; }
        DbSet<Rule> Rules { get; set; }
        DbSet<ServiceSettings> ServiceSettings{ get; set; }
        DbSet<Attempt> Attempt { get; set; }
        DbSet<AttemptDetail> AttemptDetail { get; set; }
        DbSet<PdfEngine> PdfEngines { get; set; }
        DbSet<EngineLicense> EngineLicenses { get; set; }
        Task<int> SaveChanges();
    }
}

using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Boundaries.Store
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Workflow> Workflows { get; set; }  
        public DbSet<Rule> Rules { get ; set; }
        public DbSet<ServiceSettings> ServiceSettings { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        async Task<int> IApplicationDbContext.SaveChanges()
        {
           return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Pdf");
            modelBuilder.Entity<Workflow>(model =>
            {
                model.HasKey(x => x.Id);
                model.Property(x => x.Handle).IsRequired();
                model.Property(x => x.Name).IsRequired();
            }).Entity<ServiceSettings>(model =>
            {
                model.HasKey(x => x.Id);
                model.Property(x => x.WorkMode).IsRequired();

            }).Entity<Rule>(model =>
            {
                model.HasKey(x => x.Id);
                model.Property(x => x.Name);
            });
        }
    }
}

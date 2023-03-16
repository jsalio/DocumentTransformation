using System;
using Core.Models;
using Core.Models.Attempts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Boundaries.Store
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Workflow> Workflows { get; set; }  
        public DbSet<Rule> Rules { get ; set; }
        public DbSet<ServiceSettings> ServiceSettings { get; set; }
        public DbSet<Attempt> Attempt { get; set; } 
        public DbSet<AttemptDetail> AttemptDetail { get; set; }
        public DbSet<PdfEngine> PdfEngines { get; set; }
        public DbSet<EngineLicense> EngineLicenses { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.LogTo(Console.WriteLine);

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
            }).Entity<Attempt>(model =>
            {
                model.HasKey(x => x.Id);
                model.HasIndex(x => x.DocumentHandler);
                model.HasMany<AttemptDetail>(a => a.AttemptDetails)
                    .WithOne(d => d.Attempt)
                    .HasForeignKey(x => x.AttemptId);
            }).Entity<AttemptDetail>(model =>
            {
                model.HasKey(x => x.Id);
                model.HasIndex(x => x.AttemptId);
                model.Property(x => x.ErrorDetails).IsRequired();
                model.Property(x => x.RegistryDate).IsRequired();
            }).Entity<PdfEngine>(model =>
            {
                model.HasKey(x => x.Id);
                model.HasIndex(x => x.EngineTypeName);
                model.Property(x => x.EngineVersion).IsRequired(false);
                model.Property(x => x.EngineDescription).IsRequired(false);
                model.HasOne(x => x.EngineLicense)
                    .WithOne(x => x.PdfEngine)
                    .HasForeignKey<EngineLicense>(x => x.EngineId);
            }).Entity<EngineLicense>(model =>
            {
                model.HasKey(x => x.Id);
                model.Property(x => x.LicenseString).HasColumnType("Text").HasMaxLength(20000000);
            });
        }
    }
}

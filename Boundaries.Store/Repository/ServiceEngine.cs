using Core.Contracts;
using Core.Models;
using Core.Models.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boundaries.Store.Repository
{
    public sealed class ServiceEngine : IServiceEngine
    {
        private readonly IApplicationDbContext _context;

        public ServiceEngine(IApplicationDbContext context)
        {
            _context = context;
        }

        Task<int> IServiceEngine.AddEngine(EngineRequest request)
        {
            try
            {
                _context.PdfEngines.Add(new PdfEngine
                {
                    EngineTypeName = request.EngineTypeName,
                    EngineDescription = request.EngineDescription,
                    LicenseType = request.LicenseType,
                    EngineName = request.EngineName,
                    EngineStatus = request.EngineStatus.ToString(),
                    EngineType = request.EngineType,
                    EngineVersion = request.EngineVersion,
                    IsDefault = request.IsDefault,
                    SupportOcr = request.SupportOcr
                });
                return _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new StoreException($"Error occurs when update concurrent {e.Message}");
            }
            catch (DbUpdateException e)
            {
                throw new StoreException($"Error occurs when update {e.Message}");
            }
            catch (TimeoutException e)
            {
                throw new StoreException($"Execution time is over {e.Message}");
            }
            catch (ArgumentException e)
            {
                throw new StoreException($"Invalid {e.Message}");
            }
            catch (Exception e)
            {
                throw new StoreException($"UnManage error occurs {e.Message}");
            }
        }

        Task<EngineView> IServiceEngine.AddLicense(EngineLicenseRequest request)
        {
            try
            {
                var targetEngine = _context.PdfEngines.Where(x => x.Id == request.EngineId)
                    .Select(engine => new EngineView
                    {
                        Id = engine.Id,
                        EngineTypeName = engine.EngineTypeName,
                        EngineDescription = engine.EngineDescription,
                        LicenseType = engine.LicenseType,
                        EngineName = engine.EngineName,
                        EngineStatus = engine.EngineStatus,
                        EngineType = engine.EngineType,
                        EngineVersion = engine.EngineVersion,
                        IsDefault = engine.IsDefault,
                        SupportOcr = engine.SupportOcr
                    });
                _context.EngineLicenses.Add(new EngineLicense
                {
                    EngineId = request.EngineId,
                    LicenseString = request.LicenseString
                });
                return targetEngine.FirstOrDefaultAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new StoreException($"Error occurs when update concurrent {e.Message}");
            }
            catch (DbUpdateException e)
            {
                throw new StoreException($"Error occurs when update {e.Message}");
            }
            catch (TimeoutException e)
            {
                throw new StoreException($"Execution time is over {e.Message}");
            }
            catch (ArgumentException e)
            {
                throw new StoreException($"Invalid {e.Message}");
            }
            catch (Exception e)
            {
                throw new StoreException($"UnManage error occurs {e.Message}");
            }
        }

        bool IServiceEngine.Exists(Expression<Func<PdfEngine, bool>> expression)
            => _context.PdfEngines.Any(expression);

        Task<List<EngineView>> IServiceEngine.GetEngines()
        {
            try
            {
                var engines = _context.PdfEngines
                    .Select(engine => new EngineView
                    {
                        Id = engine.Id,
                        EngineTypeName = engine.EngineTypeName,
                        EngineDescription = engine.EngineDescription,
                        LicenseType = engine.LicenseType,
                        EngineName = engine.EngineName,
                        EngineStatus = engine.EngineStatus,
                        EngineType = engine.EngineType,
                        EngineVersion = engine.EngineVersion,
                        IsDefault = engine.IsDefault,
                        SupportOcr = engine.SupportOcr
                    }).ToListAsync();
                return engines;
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new StoreException($"Error occurs when update concurrent {e.Message}");
            }
            catch (DbUpdateException e)
            {
                throw new StoreException($"Error occurs when update {e.Message}");
            }
            catch (TimeoutException e)
            {
                throw new StoreException($"Execution time is over {e.Message}");
            }
            catch (ArgumentException e)
            {
                throw new StoreException($"Invalid {e.Message}");
            }
            catch (Exception e)
            {
                throw new StoreException($"UnManage error occurs {e.Message}");
            }
        }

        Task<EngineLicense> IServiceEngine.GetLicense(int engineId)
        {
            try
            {
                var query = _context.EngineLicenses.FirstOrDefaultAsync(x => x.EngineId == engineId);
                return query;
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new StoreException($"Error occurs when update concurrent {e.Message}");
            }
            catch (DbUpdateException e)
            {
                throw new StoreException($"Error occurs when update {e.Message}");
            }
            catch (TimeoutException e)
            {
                throw new StoreException($"Execution time is over {e.Message}");
            }
            catch (ArgumentException e)
            {
                throw new StoreException($"Invalid {e.Message}");
            }
            catch (Exception e)
            {
                throw new StoreException($"UnManage error occurs {e.Message}");
            }
        }

        Task<int> IServiceEngine.RemoveEngine(int id)
        {
            try
            {
                var targetEngine = _context.PdfEngines.Include(x => x.EngineLicense)
                    .FirstOrDefault(x => x.Id == id);
                _context.PdfEngines.Remove(targetEngine);
                return _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new StoreException($"Error occurs when update concurrent {e.Message}");
            }
            catch (DbUpdateException e)
            {
                throw new StoreException($"Error occurs when update {e.Message}");
            }
            catch (TimeoutException e)
            {
                throw new StoreException($"Execution time is over {e.Message}");
            }
            catch (ArgumentException e)
            {
                throw new StoreException($"Invalid {e.Message}");
            }
            catch (Exception e)
            {
                throw new StoreException($"UnManage error occurs {e.Message}");
            }
        }

        Task<int> IServiceEngine.UpdateEngine(EngineRequest request, int id)
        {
            try
            {
                var targetEngine = _context.PdfEngines.Include(x => x.EngineLicense)
                    .FirstOrDefault(x => x.Id == id);
                targetEngine.EngineTypeName = request.EngineTypeName;
                targetEngine.EngineDescription = request.EngineDescription;
                targetEngine.LicenseType = request.LicenseType;
                targetEngine.EngineName = request.EngineName;
                targetEngine.EngineStatus = request.EngineStatus.ToString();
                targetEngine.EngineType = request.EngineType;
                targetEngine.EngineVersion = request.EngineVersion;
                targetEngine.IsDefault = request.IsDefault;
                targetEngine.SupportOcr = request.SupportOcr;
                return _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new StoreException($"Error occurs when update concurrent {e.Message}");
            }
            catch (DbUpdateException e)
            {
                throw new StoreException($"Error occurs when update {e.Message}");
            }
            catch (TimeoutException e)
            {
                throw new StoreException($"Execution time is over {e.Message}");
            }
            catch (ArgumentException e)
            {
                throw new StoreException($"Invalid {e.Message}");
            }
            catch (Exception e)
            {
                throw new StoreException($"UnManage error occurs {e.Message}");
            }
        }

        Task<EngineView> IServiceEngine.UpdateLicense(EngineLicenseRequest request)
        {
            try
            {
                var targetEngine = _context.PdfEngines.Where(x => x.Id == request.EngineId)
                    .Select(engine => new EngineView
                    {
                        Id = engine.Id,
                        EngineTypeName = engine.EngineTypeName,
                        EngineDescription = engine.EngineDescription,
                        LicenseType = engine.LicenseType,
                        EngineName = engine.EngineName,
                        EngineStatus = engine.EngineStatus,
                        EngineType = engine.EngineType,
                        EngineVersion = engine.EngineVersion,
                        IsDefault = engine.IsDefault,
                        SupportOcr = engine.SupportOcr
                    });
                if (_context.EngineLicenses.Any(x => x.EngineId == request.EngineId))
                {
                    var target = _context.EngineLicenses.FirstOrDefault(x => x.EngineId == request.EngineId);
                    target.LicenseString = request.LicenseString;
                }
                else
                {
                    _context.EngineLicenses.Add(new EngineLicense
                    {
                        LicenseString = request.LicenseString,
                        EngineId = request.EngineId
                    });
                }

                _context.SaveChanges();
                return targetEngine.FirstOrDefaultAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new StoreException($"Error occurs when update concurrent {e.Message}");
            }
            catch (DbUpdateException e)
            {
                throw new StoreException($"Error occurs when update {e.Message}");
            }
            catch (TimeoutException e)
            {
                throw new StoreException($"Execution time is over {e.Message}");
            }
            catch (ArgumentException e)
            {
                throw new StoreException($"Invalid {e.Message}");
            }
            catch (Exception e)
            {
                throw new StoreException($"UnManage error occurs {e.Message}");
            }
        }
    }
}

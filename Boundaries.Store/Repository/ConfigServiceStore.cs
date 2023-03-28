using System.Linq;
using System.Threading.Tasks;
using Core.Contracts;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Boundaries.Store.Repository
{
    public sealed class ConfigServiceStore : IServiceConfigStore
    {

        private readonly IApplicationDbContext _context;

        public ConfigServiceStore(IApplicationDbContext context)
        {
            _context = context;
        }

        ApplicationSettings IServiceConfigStore.GetApplicartionSetting()
        {
            var serviceSettings = _context.ServiceSettings.FirstOrDefault();
            var engines = _context.PdfEngines.Include(l => l.EngineLicense)
                .Where(x => x.IsDefault).ToList().Select(engine => new EngineView
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
            var query = _context.Rules.ToList();
            bool.TryParse(query.First(x => x.Name == "EnableLog").Value, out bool enableLog);
            bool.TryParse(query.First(x => x.Name == "LockFailsElements").Value, out bool lockFails);
            int.TryParse(query.First(x => x.Name == "TryLimits").Value, out int tryLimit);
            bool.TryParse(query.First(x => x.Name == "DeleteDocumentAfterSync").Value, out bool deleteAfterSync);
            bool.TryParse(query.First(x => x.Name == "ValidateBatchConverttion").Value, out bool validateConvertion);
            bool.TryParse(query.First(x => x.Name == "EnableConsole").Value, out bool enableConsole);
            bool.TryParse(query.First(x => x.Name == "EnableLocalConfig").Value, out bool useLocalconfig);

            var result = new ApplicationRules
            {
                EnableLog = enableLog,
                LockFailsElements = lockFails,
                TryLimits = tryLimit,
                DeleteDocumentAfterSync = deleteAfterSync,
                ValidateBatchConverttion = validateConvertion,
                EnableConsole = enableConsole,
                EnableLocalConfig = useLocalconfig
            };
            return new ApplicationSettings(serviceSettings, engines, result);

        }

        ServiceSettings IServiceConfigStore.GetSettings()
        {
            return _context.ServiceSettings.FirstOrDefault();
        }

        Task<int> IServiceConfigStore.Save(ServiceSettings queue)
        {
            var query = _context.ServiceSettings.FirstOrDefault();
            if (query == null)
            {
                _context.ServiceSettings.Add(queue);
            }
            else
            {
                query.WorkMode = queue.WorkMode;
                query.EnableSecondQueue = queue.EnableSecondQueue;
                query.TimeUnit = queue.TimeUnit;
                query.Interval = queue.Interval;
                query.startDate = queue.startDate;
                query.TimerWorkMode = queue.TimerWorkMode;
                query.TimeEnd = queue.TimeEnd;
                query.TimeInit = queue.TimeInit;
            }

            return _context.SaveChanges();
        }
    }
}

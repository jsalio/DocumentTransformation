using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Models;
using System.Threading.Tasks;

namespace Core.Contracts
{
    public interface IServiceEngine
    {
        bool Exists(Expression<Func<PdfEngine, bool>> expression);
        Task<int> AddEngine(EngineRequest request);
        Task<int> UpdateEngine(EngineRequest request, int id);
        Task<int> RemoveEngine(int id);
        Task<EngineView> AddLicense(EngineLicenseRequest request);
        Task<EngineView> UpdateLicense(EngineLicenseRequest request);
        Task<List<EngineView>> GetEngines();
        Task<EngineLicense> GetLicense(int engineId);
    }
}

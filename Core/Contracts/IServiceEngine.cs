using Core.Models;
using System.Threading.Tasks;

namespace Core.Contracts
{
    public interface IServiceEngine
    {
        Task<int> AddEngine(CreateEngineRequest request);
        Task<int> UpdateEngine(CreateEngineRequest request, int id);
        Task<int> RemoveEngine(int id);
        Task<EngineView> AddLicense(EngineLicenseRequest request);
        Task<EngineView> UpdateLicense(EngineLicenseRequest request);
    }
}

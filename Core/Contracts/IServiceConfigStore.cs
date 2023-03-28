using System.Threading.Tasks;
using Core.Models;

namespace Core.Contracts
{
    public interface IServiceConfigStore
    {
        Task<int> Save(ServiceSettings queue);
        ServiceSettings GetSettings();
        ApplicationSettings GetApplicartionSetting();
    }

}

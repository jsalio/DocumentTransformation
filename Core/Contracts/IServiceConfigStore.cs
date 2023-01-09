using Core.Models;

namespace Core.Contracts
{
    public interface IServiceConfigStore
    {
        string Save(ServiceSettings queue);
        ServiceSettings GetSettings();
    }

}

using Core.Models;

namespace Core.Contracts
{
    public interface IServiceRule
    {
        string Save(ApplicationRules queue);
        ApplicationRules GetRules();
    }

}

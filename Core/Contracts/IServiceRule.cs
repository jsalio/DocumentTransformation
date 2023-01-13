using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Contracts
{
    public interface IServiceRule
    {
        string Save(ApplicationRules queue);
        ApplicationRules GetRules();
    }

     public interface IRuleRepository
    {
        Task<int> Create(ApplicationRules rules);
        Task<ApplicationRules> GetAll();
        Task<int> Update(IEnumerable<Rule> rules);
    }

}

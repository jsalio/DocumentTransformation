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

    public interface IWorkflowRepository
    {
        Task<IEnumerable<Workflow>> GetAll();
        Task<int> Delete(int id);
        Task<int> DeleteMany(IEnumerable<int> id);
        Task<int> SaveMany(IEnumerable<Workflow> workflows);

    }

}

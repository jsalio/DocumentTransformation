using System;
using Core.Models;
using System.Collections.Generic;
using System.Linq.Expressions;
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
        Task<Workflow> SaveDocumentTypeSetting(IEnumerable<DocumentConvertSetting> request);
        bool Exists(Expression<Func<Workflow, bool>> expression);
        Task<List<Workflow>> GetAllSettings();
        Task<int> UpdateDocumentTypeSettings(IEnumerable<DocumentConvertSettingModel> request);
    }

}

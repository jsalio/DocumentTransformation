using Core.Models;
using System.Collections.Generic;

namespace Core.Contracts
{
    public interface IWorflowStore
    {
        IEnumerable<Workflow> GetAllActiveWorkflows();
        string Save(IEnumerable<Workflow> workflows);
    }

}

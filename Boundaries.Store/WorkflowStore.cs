using Core.Contracts;
using Core.Models;
using System;
using System.Collections.Generic;

namespace Boundaries.Store
{
    public sealed class WorkflowStore : IWorflowStore
    {
        IEnumerable<Workflow> IWorflowStore.GetAllActiveWorkflows()
        {
            return new List<Workflow>
                {
                    new Workflow {Handle= 1, Name="Workflow 1", Description =""},
                    new Workflow {Handle= 10, Name="Workflow 10", Description =""},
                    new Workflow {Handle= 11, Name="Workflow 11", Description =""},
                };
        }

        string IWorflowStore.Save(IEnumerable<Workflow> workflows)
        {
            return "Ok";
        }
    }
}

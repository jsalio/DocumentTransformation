using Core.Contracts;
using Core.Models;
using System;
using System.Collections.Generic;

namespace Boundaries.Capture
{
    /// <summary>
    /// Class for interct with capture API's
    /// </summary>
    public class WorkflowSource : IWorkflowSource
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        IEnumerable<Workflow> IWorkflowSource.GetAllActiveWorkflows()
        {
            return new List<Workflow>
            {
                    new Workflow {Id= 1, Name="Workflow 1", Description =""},
                    new Workflow {Id= 2, Name="Workflow 2", Description =""},
                    new Workflow {Id= 3, Name="Workflow 3", Description =""},
                    new Workflow {Id= 4, Name="Workflow 4", Description =""},
                    new Workflow {Id= 5, Name="Workflow 5", Description =""},
                    new Workflow {Id= 6, Name="Workflow 6", Description =""},
                    new Workflow {Id= 7, Name="Workflow 7", Description =""},
                    new Workflow {Id= 8, Name="Workflow 8", Description =""},
                    new Workflow {Id= 9, Name="Workflow 9", Description =""},
                    new Workflow {Id= 10, Name="Workflow 10", Description =""},
                    new Workflow {Id= 11, Name="Workflow 11", Description =""}
            };
        }
    }
}

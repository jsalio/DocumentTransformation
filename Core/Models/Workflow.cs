using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public sealed class Workflow
    {
        public int Handle { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActiveQaIndex { get; set; }
        public bool IsActiveQaScan { get; set; }
        public bool IsMultipleIndexingActive { get; set; }
        public bool ConvertToPdf { get; set; }
    }
}

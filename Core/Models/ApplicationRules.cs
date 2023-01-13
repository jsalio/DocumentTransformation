using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public sealed class ApplicationRules
    {
        public bool EnableLog { get; set; }
        public bool LockFailsElements { get; set; }
        public int TryLimits { get; set; }
        public bool DeleteDocumentAfterSync { get; set; }
        public bool ValidateBatchConverttion { get; set; }
        public bool EnableLocalConfig { get; set; }
        public bool EnableConsole { get; set; }

    }

    public sealed class Rule
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}

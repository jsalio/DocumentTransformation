namespace Boundaries.DocumentTransformation
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
}

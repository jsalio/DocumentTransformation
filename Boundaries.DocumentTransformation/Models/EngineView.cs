namespace Boundaries.DocumentTransformation
{
    public sealed class EngineView
    {
        public int Id { get; set; } 
        public EngineTypeName EngineTypeName { get; set; }
        public string EngineName { get; set; }
        public string EngineVersion { get; set; }
        public EngineType EngineType { get; set; }
        public string EngineStatus { get; set; }
        public string EngineDescription { get; set; }
        public LicenseType LicenseType { get; set; }
        public bool IsDefault { get; set; }
        public bool SupportOcr { get; set; }
    }
}

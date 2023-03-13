using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class PdfEngine
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

        public EngineLicense EngineLicense { get; set; }
    }

    public class CreateEngineRequest
    {
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

    public sealed class EngineLicense
    {
        public int Id { get; set; }
        public int EngineId { get; set; }
        public string LicenseString { get; set; }

        public PdfEngine PdfEngine { get; set; }
    }

    public sealed class EngineLicenseRequest
    {
        public int EngineId { get; set; }
        public string LicenseString { get; set; }
    }

    public enum EngineTypeName
    {
        /// <summary>
        /// Represents Aspose engine
        /// <code>is default</code> 
        /// </summary>
        Aspose = 0,
        /// <summary>
        /// Represents a teseract engine
        /// </summary>
        Teseract = 1,
        /// <summary>
        /// Represents a ABBY engine implementation
        /// </summary>
        Abby = 2,
        /// <summary>
        /// Represents a IRIS engine implementation
        /// </summary>
        Iris = 3
    }

    public enum EngineType
    {
        /// <summary>
        /// Only generate pdf 
        /// </summary>
        PDF = 0,
        /// <summary>
        /// Generate readable pdf 
        /// </summary>
        OCR = 1
    }

    public enum LicenseType
    {
        /// <summary>
        /// Use when engine license only provide by physical file in disk
        /// </summary>
        Internal = 0,
        /// <summary>
        /// Use when license can be use or provide by config file
        /// </summary>
        JsonLicense = 1
    }
}

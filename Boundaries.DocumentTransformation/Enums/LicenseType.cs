namespace Boundaries.DocumentTransformation
{
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

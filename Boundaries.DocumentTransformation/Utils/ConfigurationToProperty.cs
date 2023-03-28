using System;

namespace Boundaries.DocumentTransformation.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class ConfigurationToProperty
    {
        /// <summary>
        /// Get data from config file and return this data converter in specific data type
        /// </summary>
        /// <typeparam name="T">target data type</typeparam>
        /// <param name="key">keyword to find in config</param>
        /// <returns></returns>
        public static T GetKeyValue<T>(string key)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    return default;
                }
                var value = System.Configuration.ConfigurationManager.AppSettings[key];
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception e)
            {
                return default;
            }
        }
    }
}

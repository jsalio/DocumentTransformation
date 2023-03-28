using System.Collections.Generic;
using Boundaries.DocumentTransformation.Utils;
using RestSharp;

namespace Boundaries.DocumentTransformation
{
    public class ServiceSettings
    {
        private readonly string _ApiUrl;
        private readonly string _CaptureUri;

        public ServiceSettings()
        {
            _ApiUrl = ConfigurationToProperty.GetKeyValue<string>("APIURL");
            _CaptureUri = ConfigurationToProperty.GetKeyValue<string>("QueueUrl");
        }

        public ApplicationSettings GetServiceConfig()
        {
            var requestExecutor = new ExecuteRequest();
            var data = requestExecutor.Get<ApplicationSettings>(_ApiUrl, "/api/ServiceSetting");
            return data;
        }

        public Queue GetQueueConfiguration(bool isSecondayQueue)
        {
            var requestExecutor = new ExecuteRequest();
            var data = requestExecutor.Get<Queue>(_CaptureUri, "/api/queue-configurations?queueConfigurationType=7");
            return data;
        }

        public EngineLicenseView GetEngineLicense(long engineId)
        {
            var requestExecutor = new ExecuteRequest();
            var data = requestExecutor.Get<EngineLicenseView>(_ApiUrl, $"/api/engine/{engineId}/license");
            return data;
        }
    }
}

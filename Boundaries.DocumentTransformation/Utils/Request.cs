using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Boundaries.DocumentTransformation.Utils
{
    public class Request
    {
        private readonly string _url;
        private readonly string _resource;
        private readonly RestClient _restClient;
        private RestRequest _request;

        public Method Method { get; set; }

        public Request(string url, string resource)
        {
            _url = url;
            _resource = resource;
            _restClient = new RestClient(url);
        }

        public void AddBody(object body)
        {
            _request.AddJsonBody(body);
        }

        public void AddResource(string resource)
        {
            _request = new RestRequest(resource, Method);
        }

        public void AddHeaders(ICollection<KeyValuePair<string, string>> headers)
        {
            foreach (var header in headers)
            {
                _request.AddHeader(header.Key, header.Value);
            }
        }

        public RequestResponse<T> Execute<T>()
        {
            var response = _restClient.Execute(_request);
            try
            {
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return RequestResponse<T>.BuildFailResponse(response.ErrorMessage, response.StatusCode);
                }

                var jSettings = new Newtonsoft.Json.JsonSerializerSettings();
                jSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;

                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(response.Content, jSettings);

                return RequestResponse<T>.BuildResponse(data);
            }
            catch (Exception e)
            {
                return RequestResponse<T>.BuildFailResponse(e.Message, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}

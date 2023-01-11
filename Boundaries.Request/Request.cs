using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Boundaries.Request
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
            foreach(var header in headers)
            {
                _request.AddHeader(header.Key, header.Value);
            }
        }

        public RequestResponse<T> Execute<T>()
        {
            var response = _restClient.Execute(_request);
            try
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return RequestResponse<T>.BuildFailResponse(response.ErrorMessage, response.StatusCode);
                }
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                    //PropertyNamingPolicy = null
                };

                var x = JsonSerializer.Deserialize<T>(response.Content, options);

                return RequestResponse<T>.BuildResponse(x);
            }
            catch (Exception e)
            {
                return RequestResponse<T>.BuildFailResponse(e.Message, HttpStatusCode.InternalServerError);
            }
        }
    }

    public class RequestResponse<T>
    {
        public string Message { get; set; }
        public T Content { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public static RequestResponse<T> BuildFailResponse(string message, HttpStatusCode code)
        {
            return new RequestResponse<T>
            {
                Message = message,
                StatusCode = code,
                Content = default
            };
        }

        public static RequestResponse<T> BuildResponse(T content)
        {
            return new RequestResponse<T>
            {
                Message = "",
                StatusCode = HttpStatusCode.OK,
                Content = content
            };
        }
    }
}

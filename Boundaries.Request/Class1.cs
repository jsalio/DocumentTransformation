using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Boundaries.Request
{
    public class ExecuteRequest
    {
        IList<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>();

        public T Post<T>(string uri, string resource, T body = default)
        {
            var requestBuilder = new Request(uri, resource);
            requestBuilder.Method = Method.Post;
            requestBuilder.AddResource(resource);
            if (headers.Any())
            {
                requestBuilder.AddHeaders(headers);
            }
            if (body != null)
            {
                requestBuilder.AddBody(body);
            }
            var resutl = requestBuilder.Execute<T>();
            return resutl.Content;
        }

        public ModelType Post<ModelType, Body>(string uri, string resource, Body body = default)
        {
            var requestBuilder = new Request(uri, resource);
            requestBuilder.Method = Method.Post;
            requestBuilder.AddResource(resource);
            if (headers.Any())
            {
                requestBuilder.AddHeaders(headers);
            }
            if (body != null)
            {
                requestBuilder.AddBody(body);
            }
            var resutl = requestBuilder.Execute<ModelType>();
            return resutl.Content;
        }

        public T Get<T>(string uri, string resource)
        {
            Request requestBuilder = new Request(uri, resource)
            {
                Method = Method.Get
            };
            requestBuilder.AddResource(resource);
            if (headers.Any())
            {
                requestBuilder.AddHeaders(headers);
            }
            RequestResponse<T> resutl = requestBuilder.Execute<T>();
            return resutl.Content;
        }

        public void AddHeader(KeyValuePair<string, string> keyValuePair)
        {
            headers.Add(keyValuePair);
        }
    }
}

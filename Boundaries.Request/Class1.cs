using RestSharp;
using System;

namespace Boundaries.Request
{
    public class ExecuteRequest
    {
        public T Post<T>(string uri, string resource, T body = default)
        {
            var requestBuilder = new Request(uri, resource);
            requestBuilder.Method = Method.Post;
            requestBuilder.AddResource(resource);
            if (body != null)
            {
                requestBuilder.AddBody(body);
            }
            var resutl = requestBuilder.Execute<T>();
            return resutl.Content;
        }

        public T Get<T>(string uri, string resource)
        {
            Request requestBuilder = new Request(uri, resource)
            {
                Method = Method.Get
            };
            requestBuilder.AddResource(resource);
            RequestResponse<T> resutl = requestBuilder.Execute<T>();
            return resutl.Content;
        }
    }
}

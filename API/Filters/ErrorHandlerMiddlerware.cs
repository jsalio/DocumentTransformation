using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using Core.Models.Exceptions;

namespace DocumentTransformation.Filters
{
    public class ErrorHandlerMiddlerware
    {
                private readonly RequestDelegate _next;

        public ErrorHandlerMiddlerware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "appplication/json";

                switch (error)
                {
                    case KeyNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case ApplicationException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case CoreException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case StoreException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case ApiException e:
                        response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                var result = JsonSerializer.Serialize(new { message = error?.Message, stacktrace = error?.StackTrace, source = error?.Source });
                await response.WriteAsync(result);
            }
        }
    }
}

using LightStore.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace LightStore.WebApi.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        private readonly RequestDelegate next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleException(ex, context);
            }
        }

        private static Task HandleException(Exception ex, HttpContext context)
        {
            var result = ex.Message;
            var code = ex switch
            {
                NotFoundException           => HttpStatusCode.NotFound,
                NotAvailableException       => HttpStatusCode.Forbidden,
                UserIdentificationException or
                UserAuthenticationException => HttpStatusCode.Unauthorized,
                ExistsException             or
                WarehouseWriteoffException  or
                DuplicateException          or 
                DuplicateException          => HttpStatusCode.Conflict,
                _                           => HttpStatusCode.InternalServerError
            };
            context.Response.StatusCode = (int)code;
            Debug.WriteLine(ex);
            return context.Response.WriteAsync(result);
        }
    }
}

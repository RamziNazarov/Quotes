using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Quotes.Common.Wrappers;

namespace Quotes.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionMessageAsync(context, ex);
            }
        }
        
        private static Task HandleExceptionMessageAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            Response response = new Response
            {
                Message = ex.Message,
                Succeeded = false
            };
            string jsonResult = JsonConvert.SerializeObject(response);
            return context.Response.WriteAsync(jsonResult);
        }
    }
}
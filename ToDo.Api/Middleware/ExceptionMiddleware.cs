using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using ToDo.Business.Result;

namespace ToDo.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(new MResult(new Meta()
            {
                Code = context.Response.StatusCode,
                Message = "Internal Server Error.",
                MessageDetail = exception.Message,
                IsSuccess = false

            }).ToString());
        }
    }
}

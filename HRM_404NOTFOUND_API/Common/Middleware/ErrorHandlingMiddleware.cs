using System;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace SalaryManagement.Api.Common.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            var result = JsonSerializer.Serialize(new
            {
                error = "An error has occured, please contact your administration!"
            });

            return context.Response.WriteAsync(result);
        }

    }
}

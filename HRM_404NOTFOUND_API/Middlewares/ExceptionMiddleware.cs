using GroupProject_HRM_Library.Errors;
using GroupProject_HRM_Library.Exceptions;
using Newtonsoft.Json;

namespace GroupProject_HRM_Api.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        public ExceptionMiddleware()
        {
            //define logging
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                //logging
                await next(context);
            }
            catch (Exception ex)
            {
                //logging
                await HandleException(context, ex);
            }
        }

        private static async Task HandleException(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)StatusCodes.Status500InternalServerError;
            bool check = true;
            switch (ex)
            {
                case NotFoundException _:
                    context.Response.StatusCode = (int)StatusCodes.Status404NotFound;
                    break;
                case BadRequestException _:
                    context.Response.StatusCode = (int)StatusCodes.Status400BadRequest;
                    break;
                case ForBiddenException _:
                    context.Response.StatusCode = (int)StatusCodes.Status403Forbidden;
                    break;
                case UnauthorizedException _:
                    context.Response.StatusCode = (int)StatusCodes.Status401Unauthorized;
                    break;
                default:
                    _ = context.Response.WriteAsync(JsonConvert.SerializeObject(ex.Message));
                    check = false;
                    break;
            }

            if (check)
            {
                Error error = new Error()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = JsonConvert.DeserializeObject<List<ErrorDetail>>(ex.Message)
                };

                await context.Response.WriteAsync(error.ToString());
            }
        }
    }
}

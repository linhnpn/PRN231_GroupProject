namespace GroupProject_HRM_Api.Middlewares
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<JWTMiddleWare>();
        }
    }
}

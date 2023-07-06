using GroupProject_HRM_Library.Constaints;
using GroupProject_HRM_Library.Errors;
using GroupProject_HRM_Library.Exceptions;
using GroupProject_HRM_Library.Services;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GroupProject_HRM_Api.Middlewares
{
    public class JWTMiddleWare
    {
        private readonly RequestDelegate _next;
        private IJWTServices jWTServices;

        //private readonly JwtOptions _jwtOptions;
        public JWTMiddleWare(RequestDelegate next)
        {
            _next = next;
            jWTServices = new JWTServices();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                AttachUserToContext(context, token);
            }
            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, string token)
        {
            try
            {              
                var objectToken = jWTServices.ExtractToken(token);
                // Lưu trữ thông tin xác thực trong HttpContext để sử dụng trong middleware tiếp theo hoặc trong controllers
                context.Items["Username"] = objectToken.Username;
                context.Items["Role"] = objectToken.Role;
                context.Items["EmployeeID"] = objectToken.EmployeeID;
            }
            catch
            {
                // Xử lý khi xác thực JWT token không hợp lệ
                List<ErrorDetail> errors = new List<ErrorDetail>();
                ErrorDetail error = new ErrorDetail()
                {
                    FieldNameError = "Exception",
                    DescriptionError = new List<string>() { "JWT Token is invalid." }
                };
                errors.Add(error);
                var message = JsonConvert.SerializeObject(errors);
                throw new ForBiddenException(message.ToString());
            }
        }
    }
}

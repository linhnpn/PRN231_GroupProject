using GroupProject_HRM_Library.DTOs.Authenticate;
using GroupProject_HRM_Library.Errors;
using GroupProject_HRM_Library.Exceptions;
using GroupProject_HRM_Library.Repository.Interface;
using GroupProject_HRM_Library.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GroupProject_HRM_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private IEmployeeRepository employeeRepository;
        private IJWTServices jWTServices;


        public AuthController(IEmployeeRepository employeeRepository, IJWTServices jWTServices)
        {
            this.employeeRepository = employeeRepository;
            this.jWTServices = jWTServices;
        }

        [HttpPost("Authenticate")]
        public IActionResult Authenticate(AuthenRequest authenRequest)
        {
            var employee = employeeRepository.Authenticate(authenRequest).Result;     
            if(employee == null)
            {
                List<ErrorDetail> errors = new List<ErrorDetail>();
                ErrorDetail error = new ErrorDetail()
                {
                    FieldNameError = "Exception",
                    DescriptionError = new List<string>() { "Username or Password is invalid." }
                };
                errors.Add(error);
                var message = JsonConvert.SerializeObject(errors);
                throw new UnauthorizedException(message);
            }
            var accessToken = jWTServices.GenerateJWTToken(employee.EmployeeID, employee.UserName, employee.Role.RoleName);
            AuthenResponse authenResponse = new AuthenResponse()
            {
                AccessToken = accessToken
            };
            return Ok(accessToken);
        }
    }
}

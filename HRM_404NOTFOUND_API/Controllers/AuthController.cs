using GroupProject_HRM_Library.DTOs.Authenticate;
using GroupProject_HRM_Library.DTOs.Employee;
using GroupProject_HRM_Library.DTOs.Income;
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
        public async Task<IActionResult> Authenticate(AuthenRequest authenRequest)
        {
            var employee = await employeeRepository.Authenticate(authenRequest);
            return Ok(new
            {
                Success = true,
                Data = employee
            });
        }
    }
}

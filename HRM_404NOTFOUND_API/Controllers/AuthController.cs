using GroupProject_HRM_Library.DTOs.Authenticate;
using GroupProject_HRM_Library.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GroupProject_HRM_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private IEmployeeRepository employeeRepository;

        public AuthController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
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

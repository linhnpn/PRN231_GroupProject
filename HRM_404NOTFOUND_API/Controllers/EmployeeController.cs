using GroupProject_HRM_Library.DTOs.Employee;
using GroupProject_HRM_Library.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroupProject_HRM_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository _employeeRepository)
        {
            this._employeeRepository = _employeeRepository;
        }
        [HttpGet("{id}"), ActionName("Get Profile")]
        public async Task<IActionResult> GetProfileAsync([FromRoute] int id)
        {
            GetProfileResponse getProfileResponse = await this._employeeRepository.GetProfileEmplAsync(id);
            return Ok(new
            {
                Success = true,
                Data = getProfileResponse
            });
        }
    }
}

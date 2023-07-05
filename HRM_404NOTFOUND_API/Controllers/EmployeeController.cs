using GroupProject_HRM_Library.DTOs.Employee;
using GroupProject_HRM_Library.Repository.Implement;
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
        private IProjectRepository _projectRepository;

        public EmployeeController(IEmployeeRepository _employeeRepository, IProjectRepository projectRepository)
        {
            this._employeeRepository = _employeeRepository;
            _projectRepository = projectRepository;
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

        [HttpGet("manager/{id}"), ActionName("Get Employee Of A Project")]
        public async Task<IActionResult> GetListEmplBaseManagerAsync([FromRoute] int id)
        {
            int projectId = await _projectRepository.GetIdProjectBaseOnManager(id);
            List<GetListEmployeeResponseIDandName> getListEmployeeResponses = await this._employeeRepository.GetListEmployeeResponseIDandNameAsync(projectId);
            return Ok(new
            {
                Success = true,
                Data = getListEmployeeResponses
            });
        }
    }
}

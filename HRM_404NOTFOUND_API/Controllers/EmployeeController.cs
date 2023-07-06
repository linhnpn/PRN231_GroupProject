using GroupProject_HRM_Library.DTOs.Employee;
using GroupProject_HRM_Library.Enums;
using GroupProject_HRM_Library.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [Authorize]
        public async Task<IActionResult> GetProfileAsync([FromRoute] int id)
        {
            GetProfileResponse getProfileResponse = await this._employeeRepository.GetProfileEmplAsync(id);
            return Ok(new
            {
                Success = true,
                Data = getProfileResponse
            });
        }
        [Authorize(Roles = "Manager")]
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

        [HttpGet("no-project"), ActionName("Get Employee Of A Project")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetListEmplNotInAnyProjectAsync()
        {
            List<GetListEmployeeResponseIDandName> getListEmployeeResponses = await this._employeeRepository.GetListEmployeeResponseIDandNameNotInAnyProjectAsync();
            return Ok(new
            {
                Success = true,
                Data = getListEmployeeResponses
            });
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("manager-all/{id}"), ActionName("Get Employee Of A Project")]
        public async Task<IActionResult> GetListEmplResponseBaseManagerAsync([FromRoute] int id)
        {
            int projectId = await _projectRepository.GetIdProjectBaseOnManager(id);
            var getListEmployeeResponses = await this._employeeRepository.GetListEmployeeResponseAsync(projectId);
            return Ok(new
            {
                Success = true,
                Data = getListEmployeeResponses
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("project/{id}"), ActionName("Get Employee Of A Project")]
        public async Task<IActionResult> GetListEmplResponseBaseProjectAsync([FromRoute] int id)
        {
            var getListEmployeeResponses = await this._employeeRepository.GetListEmployeeResponseAsync(id);
            return Ok(new
            {
                Success = true,
                Data = getListEmployeeResponses
            });
        }

        [HttpGet("no-payroll"), ActionName("Get Employee No Payroll")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetListEmplNoPayrollrAsync()
        {
            List<GetListEmployeeResponseIDandName> getListEmployeeResponses = await this._employeeRepository.GetListEmployeeResponseNoPayRollAsync();
            return Ok(new
            {
                Success = true,
                Data = getListEmployeeResponses
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeRequest employeeRequest)
        {
            var result = _employeeRepository.CreateEmployee(employeeRequest);
            return Ok(new
            {
                Success = true,
                RowEffect = result.Result
            });
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeRequest updateEmployeeRequest)
        {
            var result = _employeeRepository.UpdateEmployee(updateEmployeeRequest);
            return Ok(new
            {
                Success = true,
                Data = result.Result
            });
        }

        [HttpPut("UpdateStatus")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStatusEmployee(UpdateStatusEmployeeRequest employeeRequest)
        {
            var result = _employeeRepository.UpdateStatusEmployee(employeeRequest);
            return Ok(new
            {
                Success = true,
                Data = result.Result
            });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetALLEmployee()
        {
            var result = _employeeRepository.GetAllEmployee();
            return Ok(new
            {
                Success = true,
                Data = result.Result
            });
        }

        [HttpGet("NotStart")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetALLEmployeeNotStart()
        {
            var result = _employeeRepository.GetALLEmployeeNotStart();
            return Ok(new
            {
                Success = true,
                Data = result.Result
            });
        }
    }
}

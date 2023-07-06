using GroupProject_HRM_Library.DTOs.EmployeeProject;
using GroupProject_HRM_Library.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroupProject_HRM_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeProjectController : ControllerBase
    {
        private IEmployeeProjectRepository employeeProjectRepository;
        public EmployeeProjectController(IEmployeeProjectRepository employeeProjectRepository)
        {
            this.employeeProjectRepository = employeeProjectRepository;
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignEmployeeToProject(AssignEmployeeToProjectRequest assignRequest)
        {
            var employeeProject = employeeProjectRepository.AssignEmployeeToProject(assignRequest);
            return Ok(new
            {
                Success = true,
                Data = employeeProject.Result
            });
        }
    }
}

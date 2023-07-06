using GroupProject_HRM_Library.DTOs.EmployeeProject;
using GroupProject_HRM_Library.Errors;
using GroupProject_HRM_Library.Exceptions;
using GroupProject_HRM_Library.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GroupProject_HRM_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeProjectController : ControllerBase
    {
        private readonly IEmployeeProjectRepository _employeeProjectRepository;

        public EmployeeProjectController(IEmployeeProjectRepository employeeProjectRepository)
        {
            _employeeProjectRepository = employeeProjectRepository;
        }

        // POST api/<TaxController>
        [HttpPost, ActionName("PostTax")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostTaxAsync([FromBody] CreateEmployeeProjectRequest value)
        {
            if (!ModelState.IsValid)
            {
                var errors = new List<ErrorDetail>();
                foreach (var pair in ModelState)
                {
                    if (pair.Value.Errors.Count > 0)
                    {
                        ErrorDetail errorDetail = new ErrorDetail()
                        {
                            FieldNameError = pair.Key,
                            DescriptionError = pair.Value.Errors.Select(error => error.ErrorMessage).ToList()
                        };
                        errors.Add(errorDetail);
                    }
                }

                var message = JsonConvert.SerializeObject(errors);
                throw new BadRequestException(message);
            }
            await _employeeProjectRepository.CreateEmployeeProjectRequestAsync(value);
            return Ok(new
            {
                Success = true,
                Data = "Add Employee for a Project Successfully!"
            });
        }

        // PUT api/<TaxController>/5
        [Authorize(Roles = "Admin")]
        [HttpPut("update-empl"), ActionName("PutTax")]
        public async Task<IActionResult> PutTaxAsync([FromBody] UpdateEmployeeProjectRequest value)
        {
            if (!ModelState.IsValid)
            {
                var errors = new List<ErrorDetail>();
                foreach (var pair in ModelState)
                {
                    if (pair.Value.Errors.Count > 0)
                    {
                        ErrorDetail errorDetail = new ErrorDetail()
                        {
                            FieldNameError = pair.Key,
                            DescriptionError = pair.Value.Errors.Select(error => error.ErrorMessage).ToList()
                        };
                        errors.Add(errorDetail);
                    }
                }

                var message = JsonConvert.SerializeObject(errors);
                throw new BadRequestException(message);
            }
            await _employeeProjectRepository.UpdateEmployeeProjectRequestAsync(value);
            return Ok(new
            {
                Success = true,
                Data = "Updated Employee Of The Project Successfully!"
            });
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTaxAsync([FromQuery] int employeeID, [FromQuery] int projectID)
        {
            await _employeeProjectRepository.DeleteEmployeeProjectRequestAsync(employeeID, projectID);
            return Ok(new
            {
                Success = true,
                Data = "Deleted Employee of a Project Successfully!"
            });
        }

        [HttpPost("AssignEmployee")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignEmployeeToProject(AssignEmployeeToProjectRequest assignRequest)
        {
            var employeeProject = _employeeProjectRepository.AssignEmployeeToProject(assignRequest);
            return Ok(new
            {
                Success = true,
                Data = employeeProject.Result
            });
        }


        [HttpGet("{projectID}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetInforOfProject(int projectID)
        {
            var employeeProject = _employeeProjectRepository.GetInforOfProjects(projectID);
            return Ok(new
            {
                Success = true,
                Data = employeeProject.Result
            });
        }

    }
}

using GroupProject_HRM_Library.DTOs.EmployeeProject;
using GroupProject_HRM_Library.DTOs.Tax;
using GroupProject_HRM_Library.Enums;
using GroupProject_HRM_Library.Errors;
using GroupProject_HRM_Library.Exceptions;
using GroupProject_HRM_Library.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

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
        [HttpPut, ActionName("PutTax")]
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

        [HttpDelete(), ActionName("DeleteTax")]
        public async Task<IActionResult> DeleteTaxAsync([FromQuery] int employeeID, [FromQuery] int projectID)
        {
            await _employeeProjectRepository.DeleteEmployeeProjectRequestAsync(employeeID, projectID);
            return Ok(new
            {
                Success = true,
                Data = "Deleted Employee of a Project Successfully!"
            });
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

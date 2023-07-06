using GroupProject_HRM_Library.DTOs.Project;
using GroupProject_HRM_Library.Enums;
using GroupProject_HRM_Library.Errors;
using GroupProject_HRM_Library.Exceptions;
using GroupProject_HRM_Library.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace GroupProject_HRM_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        [HttpGet, ActionName("GetProjects")]
        [Authorize]
        public async Task<IActionResult> GetProjectsAsync()
        {
            List<GetProjectResponse> proResponses = await _projectRepository.GetProjectResponsesAsync();
            return Ok(new
            {
                Success = true,
                Data = proResponses
            });
        }
        [HttpGet("Sort"), ActionName("GetProjectsSorted")]
        [Authorize]
        public async Task<IActionResult> GetTaxesSortedAsync(
            [Required][FromQuery] string? projectName,
            [FromQuery] decimal? bonus = null,
            [FromQuery] ProjectEnum.ProjectStatus? status = null,
            [FromQuery] ProjectEnum.ProjectOrderBy? orderBy = null)
        {
            List<GetProjectResponse> proResponses =
                await _projectRepository.GetProjectResponsesSortedAsync(
                    projectName,
                    bonus,
                    status,
                    orderBy);
            return Ok(new
            {
                Success = true,
                Data = proResponses
            });
        }

        [HttpGet("{id}"), ActionName("GetProject")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetProjectAsync([FromRoute] int id)
        {
            GetProjectDetailResponse proResponse = await _projectRepository.GetProjectResponseAsync(id);

            return Ok(new
            {
                Success = true,
                Data = proResponse
            });
        }

        [HttpGet("manager/{id}"), ActionName("GetProject")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetProjectBaseManagerAsync([FromRoute] int id)
        {
            int projectId = await _projectRepository.GetIdProjectBaseOnManager(id);
            GetProjectDetailResponse proResponse = await _projectRepository.GetProjectResponseAsync(projectId);

            return Ok(new
            {
                Success = true,
                Data = proResponse
            });
        }

        [HttpPost, ActionName("PostProject")]
        [Authorize]
        public async Task<IActionResult> PostProjectAsync([FromBody] CreateProjectRequest value)
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
            await _projectRepository.CreateProjectRequestAsync(value);
            return Ok(new
            {
                Success = true,
                Data = "Created Project Successfully!"
            });
        }

        [HttpPut("{id}"), ActionName("PutProject")]
        [Authorize]
        public async Task<IActionResult> PutProjectAsync([FromRoute] int id, [FromBody] UpdateProjectRequest value)
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
            await _projectRepository.UpdateProjectRequestAsync(id, value);
            return Ok(new
            {
                Success = true,
                Data = "Updated Project Successfully!"
            });
        }

        [HttpPut("Status/{id}"), ActionName("PutProjectStatus")]
        [Authorize]
        public async Task<IActionResult> PutProjectStatusAsync(
            [FromRoute] int id,
            [Required(ErrorMessage = "Project Status is required")][FromBody] ProjectEnum.ProjectStatus value)
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
            await _projectRepository.UpdateProjectStatusRequestAsync(id, value);
            return Ok(new
            {
                Success = true,
                Data = "Updated Project Successfully!"
            });
        }

        [HttpDelete("{id}"), ActionName("DeleteTax")]
        [Authorize]
        public async Task<IActionResult> DeleteProjectAsync([FromRoute] int id)
        {
            await _projectRepository.DeleteProjectRequestAsync(id);
            return Ok(new
            {
                Success = true,
                Data = "Deleted Project Successfully!"
            });
        }

        [HttpGet("CanAssign")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllProjectCanAssignEmployee()
        {
            var projects = await _projectRepository.GetAllProjectCanAssignEmployee();
            return Ok(new
            {
                Success = true,
                Data = projects
            });
        }
    }
}

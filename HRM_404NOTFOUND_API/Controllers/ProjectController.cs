using GroupProject_HRM_Library.DTOs.Project;
using GroupProject_HRM_Library.DTOs.Tax;
using GroupProject_HRM_Library.Enums;
using GroupProject_HRM_Library.Errors;
using GroupProject_HRM_Library.Exceptions;
using GroupProject_HRM_Library.Repository.Implement;
using GroupProject_HRM_Library.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        // GET: api/<ProjectController>
        [HttpGet, ActionName("GetProjects")]
        public async Task<IActionResult> GetProjectsAsync()
        {
            List<GetProjectResponse> proResponses = await _projectRepository.GetProjectResponsesAsync();
            return Ok(new
            {
                Success = true,
                Data = proResponses
            });
        }
        // GET: api/<ProjectController>/Sort
        [HttpGet("Sort"), ActionName("GetProjectsSorted")]
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

        // GET api/<ProjectController>/5
        [HttpGet("{id}"), ActionName("GetProject")]
        public async Task<IActionResult> GetProjectAsync([FromRoute] int id)
        {
            GetProjectDetailResponse proResponse = await _projectRepository.GetProjectResponseAsync(id);

            return Ok(new
            {
                Success = true,
                Data = proResponse
            });
        }

        // POST api/<ProjectController>
        [HttpPost, ActionName("PostProject")]
        public async Task<IActionResult> PostProjectAsync([FromForm] CreateProjectRequest value)
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

        // PUT api/<ProjectController>/5
        [HttpPut("{id}"), ActionName("PutProject")]
        public async Task<IActionResult> PutProjectAsync([FromRoute] int id, [FromForm] UpdateProjectRequest value)
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

        // PUT api/<ProjectController>/5
        [HttpPut("{id}/Status"), ActionName("PutProjectStatus")]
        public async Task<IActionResult> PutProjectStatusAsync(
            [FromRoute] int id,
            [Required(ErrorMessage = "Project Status is required")][FromForm] ProjectEnum.ProjectStatus value)
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

        // DELETE api/<ProjectController>/5
        [HttpDelete("{id}"), ActionName("DeleteTax")]
        public async Task<IActionResult> DeleteProjectAsync([FromRoute] int id)
        {
            await _projectRepository.DeleteProjectRequestAsync(id);
            return Ok(new
            {
                Success = true,
                Data = "Deleted Project Successfully!"
            });
        }
    }
}

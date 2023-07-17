using GroupProject_HRM_Library.DTOs.LeaveLog;
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
    public class LeaveLogController : ControllerBase
    {
        private ILeaveLogRepository _leavelogRepository;
        private IProjectRepository _projectRepository;

        public LeaveLogController(ILeaveLogRepository leaveLogRepository, IProjectRepository projectRepository)
        {
            this._leavelogRepository = leaveLogRepository;
            this._projectRepository = projectRepository;
        }

        [HttpPost, ActionName("Post LeaveLog")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> PostLeaveLogAsync([FromForm] LeaveLogRequest request)
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

            await this._leavelogRepository.CreateLeaveLogRequestAsync(request);
            return Ok(new
            {
                Success = true,
                Data = "Created leave log successfully."
            });
        }

        [HttpPost("manager"), ActionName("Post LeaveLog")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> PostLeaveLogManagerAsync([FromForm] LeaveLogManagerRequest request)
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

            await this._leavelogRepository.CreateLeaveLogRequestAsync(request);
            return Ok(new
            {
                Success = true,
                Data = "Created flower bouquet successfully."
            });
        }
        [Authorize]
        [HttpGet("{id}"), ActionName("Get LeaveLog")]
        public async Task<IActionResult> GetLeaveLogByIDAsync([FromRoute] int id)
        {
            GetLeaveLogResponse leaveLogResponse = await this._leavelogRepository.GetLeaveLogAsync(id);
            return Ok(new
            {
                Success = true,
                Data = leaveLogResponse
            });
        }
        [Authorize(Roles = "Manager")]
        [HttpPut("reject/{id}")]
        public async Task<IActionResult> PutLeaveLogAsync([FromRoute] int id, [FromBody] UpdateLeaveLogRequest updateLeaveLogRequest)
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
            await this._leavelogRepository.UpdateLeaveLogRequestAsync(id, updateLeaveLogRequest);
            return Ok(new
            {
                Success = true,
                Data = "Reject leave log successfully."
            });
        }

        [HttpPut("status/{id}")]
        [Authorize]
        public async Task<IActionResult> PutStatusLeaveLogAsync([FromRoute] int id, [FromQuery] int status)
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
            await this._leavelogRepository.UpdateStatusRequestAsync(id, status);
            return Ok(new
            {
                Success = true,
                Data = "Updated status leave log successfully."
            });
        }

        [HttpGet("employee"), ActionName("GetLeaveLogByEmplIDs")]
        [Authorize]
        public async Task<IActionResult> GetLeaveLogsAsync([FromQuery] int id, [FromQuery] int? status)
        {
            List<GetLeaveLogResponse> leaveLogResponses = await this._leavelogRepository.GetLeaveLogResponsesByEmplIDAsync(id, status);
            return Ok(new
            {
                Success = true,
                Data = leaveLogResponses
            });
        }

        [HttpGet("project"), ActionName("GetLeaveLogByEmplIDs")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetLeaveLogByManagerIDsAsync([FromQuery] int managerId, [FromQuery] int? status)
        {
            int projectId = await _projectRepository.GetIdProjectBaseOnManager(managerId);
            List<GetLeaveLogResponse> leaveLogResponses = await this._leavelogRepository.GetLeaveLogResponsesByProjectIDAsync(projectId, status);
            return Ok(new
            {
                Success = true,
                Data = leaveLogResponses
            });
        }

        [HttpGet, ActionName("GetLeaveLogByStatuses")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetLeaveLogByProjectIDsAsync([FromQuery] int projectId, [FromQuery] int? status)
        {
            List<GetLeaveLogResponse> leaveLogResponses = await this._leavelogRepository.GetLeaveLogResponsesByStatusAsync(status);
            return Ok(new
            {
                Success = true,
                Data = leaveLogResponses
            });
        }
    }
}

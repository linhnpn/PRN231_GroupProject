using GroupProject_HRM_Library.DTOs.OvertimeLog;
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
    public class OvertimeLogController : ControllerBase
    {
        private IOvertimeLogRepository _overtimeLogRepository;
        private IProjectRepository _projectRepository;

        public OvertimeLogController(IOvertimeLogRepository leaveLogRepository, IProjectRepository projectRepository)
        {
            this._overtimeLogRepository = leaveLogRepository;
            _projectRepository = projectRepository;
        }

        [HttpPost, ActionName("Post OvertimeLog")]
        [Authorize]
        public async Task<IActionResult> PostOvertimeLogAsync(OvertimeLogRequest request)
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

            await this._overtimeLogRepository.CreateOvertimeLogRequestAsync(request);
            return Ok(new
            {
                Success = true,
                Data = "Created overtime Log successfully."
            });
        }

        [HttpGet("{id}"), ActionName("Get OvertimeLog")]
        [Authorize]
        public async Task<IActionResult> GetOvertimeLogByIDAsync([FromRoute] int id)
        {
            GetOvertimeLogResponse leaveLogResponse = await this._overtimeLogRepository.GetOvertimeLogAsync(id);
            return Ok(new
            {
                Success = true,
                Data = leaveLogResponse
            });
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutOvertimeLogAsync([FromRoute] int id, [FromForm] UpdateOvertimeLogRequest updateOvertimeLogRequest)
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
            await this._overtimeLogRepository.UpdateOvertimeLogRequestAsync(id, updateOvertimeLogRequest);
            return Ok(new
            {
                Success = true,
                Data = "Updated leave log successfully."
            });
        }

        [HttpPut("status/{id}")]
        [Authorize]
        public async Task<IActionResult> PutStatusOvertimeLogAsync([FromRoute] int id, [FromQuery] int status)
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
            await this._overtimeLogRepository.UpdateStatusRequestAsync(id, status);
            return Ok(new
            {
                Success = true,
                Data = "Updated status leave log successfully."
            });
        }

        [HttpGet("employee"), ActionName("GetOvertimeLogByEmplIDs")]
        [Authorize]
        public async Task<IActionResult> GetOvertimeLogsAsync([FromQuery] int id, [FromQuery] int? status)
        {
            List<GetOvertimeLogResponse> leaveLogResponses = await this._overtimeLogRepository.GetOvertimeLogResponsesByEmplIDAsync(id, status);
            return Ok(new
            {
                Success = true,
                Data = leaveLogResponses
            });
        }

        [HttpGet("mangager"), ActionName("GetOvertimeLogByEmplIDs")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetOvertimeLogByProjectIDsAsync([FromQuery] int managerId, [FromQuery] int? status)
        {
            int projectId = await _projectRepository.GetIdProjectBaseOnManager(managerId);
            List<GetOvertimeLogResponse> leaveLogResponses = await this._overtimeLogRepository.GetOvertimeLogResponsesByProjectIDAsync(projectId, status);
            return Ok(new
            {
                Success = true,
                Data = leaveLogResponses
            });
        }

        [HttpGet, ActionName("GetOvertimeLogByStatuses")]
        [Authorize]
        public async Task<IActionResult> GetOvertimeLogByProjectIDsAsync([FromQuery] int? status)
        {
            List<GetOvertimeLogResponse> leaveLogResponses = await this._overtimeLogRepository.GetOvertimeLogResponsesByStatusAsync(status);
            return Ok(new
            {
                Success = true,
                Data = leaveLogResponses
            });
        }
    }
}

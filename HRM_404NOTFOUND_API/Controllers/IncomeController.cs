using GroupProject_HRM_Library.DTOs.Income;
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
    public class IncomeController : ControllerBase
    {
        private IIncomeRepository _incomeRepository;

        public IncomeController(IIncomeRepository incomeRepository)
        {
            this._incomeRepository = incomeRepository;
        }

        [HttpGet("{id}"), ActionName("Get Income")]
        [Authorize]
        public async Task<IActionResult> GetByEmplIDAsync([FromRoute] int id)
        {
            List<GetIncomeEmployeeResponse> getIncomeEmployeeResponses = await this._incomeRepository.GetIncomeEmplAsync(id);
            return Ok(new
            {
                Success = true,
                Data = getIncomeEmployeeResponses
            });
        }

        [HttpPost, ActionName("Post Income")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostIncomeAsync([FromBody] List<CreateIncomeEmployeeResponse> request)
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

            await this._incomeRepository.CreateIncomeAsync(request);
            return Ok(new
            {
                Success = true,
                Data = "Created income successfully."
            });
        }

        [HttpPost("all"), ActionName("Post Income")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostIncomeAllAsync()
        {

            await this._incomeRepository.CreateIncomeAsync();
            return Ok(new
            {
                Success = true,
                Data = "Created income successfully."
            });
        }
    }
}

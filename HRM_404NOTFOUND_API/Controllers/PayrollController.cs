using GroupProject_HRM_Library.DTOs.Payroll;
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
    public class PayrollController : ControllerBase
    {
        private IPayrollRepository _payrollRepository;
        public PayrollController(IPayrollRepository payrollRepository) {
            this._payrollRepository = payrollRepository;
        }

        [HttpPost, ActionName("Post Payroll")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostPayrollAsync([FromBody] PayrollRequest request)
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

            await this._payrollRepository.CreatePayrollRequestAsync(request);
            return Ok(new
            {
                Success = true,
                Data = "Created payroll successfully."
            });
        }


            [HttpGet("{id}"), ActionName("Get Payroll")]
            [Authorize(Roles = "Admin")]
            public async Task<IActionResult> GetPayrollEmplIDAsync([FromRoute] int id)
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

                var response = await this._payrollRepository.GetPayrollResponsesByEmplIDAsync(id);
                return Ok(new
                {
                    Success = true,
                    Data = response
                });
            }
        }
}

using GroupProject_HRM_Library.DTOs.Tax;
using GroupProject_HRM_Library.Enums;
using GroupProject_HRM_Library.Errors;
using GroupProject_HRM_Library.Exceptions;
using GroupProject_HRM_Library.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace GroupProject_HRM_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxController : ControllerBase
    {
        private readonly ITaxRepository _taxRepository;

        public TaxController(ITaxRepository taxRepository)
        {
            _taxRepository = taxRepository;
        }

        [HttpGet, ActionName("GetTaxes")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTaxesAsync()
        {
            List<GetTaxResponse> taxResponses = await _taxRepository.GetTaxResponsesAsync();
            return Ok(new
            {
                Success = true,
                Data = taxResponses
            });
        }

        // GET: api/<TaxController>/Sort
        [Authorize(Roles = "Admin")]
        [HttpGet("Sort"), ActionName("GetTaxesSorted")]
        public async Task<IActionResult> GetTaxesSortedAsync(
            [Required][FromQuery] decimal? minSalary,
            [Required][FromQuery] decimal? maxSalary,
            [FromQuery] double? minPercent = null,
            [FromQuery] double? maxPercent = null,
            [FromQuery] DateTime? addDate = null,
            [FromQuery] TaxEnum.TaxStatus? status = null,
            [FromQuery] TaxEnum.TaxOrderBy? orderBy = null)
        {
            List<GetTaxResponse> taxResponses =
                await _taxRepository.GetTaxResponsesSortedAsync(
                    minSalary,
                    maxSalary,
                    minPercent,
                    maxPercent,
                    addDate,
                    status,
                    orderBy);
            return Ok(new
            {
                Success = true,
                Data = taxResponses
            });
        }

        // GET api/<TaxController>/5
        [HttpGet("{id}"), ActionName("GetTax")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTaxAsync([FromRoute] int id)
        {
            GetTaxResponse taxResponse = await _taxRepository.GetTaxResponseAsync(id);

            return Ok(new
            {
                Success = true,
                Data = taxResponse
            });
        }

        // POST api/<TaxController>
        [HttpPost, ActionName("PostTax")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostTaxAsync([FromBody] CreateTaxRequest value)
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
            await _taxRepository.CreateTaxRequestAsync(value);
            return Ok(new
            {
                Success = true,
                Data = "Created Tax Successfully!"
            });
        }

        // PUT api/<TaxController>/5
        [HttpPut("{id}"), ActionName("PutTax")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutTaxAsync([FromRoute] int id,[FromBody] UpdateTaxRequest value)
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
            await _taxRepository.UpdateTaxRequestAsync(id, value);
            return Ok(new
            {
                Success = true,
                Data = "Updated Tax Successfully!"
            });
        }

        // PUT api/<TaxController>/5
        [HttpPut("{id}/Status"), ActionName("PutTaxStatus")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutTaxStatusAsync(
            [FromRoute] int id, 
            [Required(ErrorMessage = "Tax Status is required")][FromBody] TaxEnum.TaxStatus value)
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
            await _taxRepository.UpdateTaxStatusRequestAsync(id, value);
            return Ok(new
            {
                Success = true,
                Data = "Updated Tax Successfully!"
            });
        }

        // DELETE api/<TaxController>/5
        [HttpDelete("{id}"), ActionName("DeleteTax")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTaxAsync([FromRoute] int id)
        {
            await _taxRepository.DeleteTaxRequestAsync(id);
            return Ok(new
            {
                Success = true,
                Data = "Deleted Tax Successfully!"
            });
        }
    }
}

using GroupProject_HRM_Library.DTOs.Bonus;
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
    public class BonusController : ControllerBase
    {
        private IBonusRepository _bonusRepository;

        public BonusController(IBonusRepository bonusRepository)
        {
            this._bonusRepository = bonusRepository;
        }

        [HttpPost, ActionName("Post Bonus")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> PostBonusAsync([FromBody] BonusRequest request)
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

            await this._bonusRepository.CreateBonusAsync(request);
            return Ok(new
            {
                Success = true,
                Data = "Created bonus successfully."
            });
        }
    }
}

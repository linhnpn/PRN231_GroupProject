using GroupProject_HRM_Library.DTOs.Income;
using GroupProject_HRM_Library.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetByEmplIDAsync([FromRoute] int id)
        {
            List<GetIncomeEmployeeResponse> getIncomeEmployeeResponses = await this._incomeRepository.GetIncomeEmplAsync(id);
            return Ok(new
            {
                Success = true,
                Data = getIncomeEmployeeResponses
            });
        }
    }
}

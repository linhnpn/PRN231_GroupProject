using GroupProject_HRM_Library.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroupProject_HRM_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private INotificationRepository _notificationRepository;

        public NotificationController(INotificationRepository notificationRepository)
        {
            this._notificationRepository = notificationRepository;
        }

        [HttpGet("{id}"), ActionName("Get Notification")]
        [Authorize]
        public async Task<IActionResult> GetNotificationEmpl([FromRoute] int id)
        {
            var getNotificationEmpl = await this._notificationRepository.GetNotiEmplAsync(id);
            return Ok(new
            {
                Success = true,
                Data = getNotificationEmpl
            });
        }
    }
}

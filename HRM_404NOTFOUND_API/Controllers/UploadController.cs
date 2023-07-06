using Google.Cloud.Storage.V1;
using GroupProject_HRM_Library.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GroupProject_HRM_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IEmailSenderService emailSender;
        public UploadController(IEmailSenderService emailSender)
        {
            this.emailSender = emailSender;
        }
        [HttpPost]
        public async Task<IActionResult> upload(IFormFile file)
        {
            FirebaseStorageService firebase = new FirebaseStorageService(StorageClient.Create());
            var url = await firebase.UploadFile(file);

            return Ok(url);
        }

        [HttpPost("sendMail")]
        public async Task<IActionResult> sendMailTest(string email, string subject, string message)
        {
            await emailSender.SendEmailAsync(email, subject, message);
            return Ok();
        }
    }
}

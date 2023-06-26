using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GroupProject_HRM_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> upload(IFormFile file) {
            FirebaseStorageService firebase = new FirebaseStorageService(StorageClient.Create());
            var url = await firebase.UploadFile(file);

            return Ok(url);
        }
    }
}

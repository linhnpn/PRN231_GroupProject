using GroupProject_HRM_Library.DTOs.LeaveLog;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace GroupProject_HRM_View.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly HttpClient client = null;
        private string LeaveLogApiUrl = "";

        public EmployeeController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            LeaveLogApiUrl = "https://localhost:5000/api/LeaveLog";
        }

        public IActionResult Income()
        {
            return View();
        }

        public IActionResult LeaveLogIndex()
        {
            return View();
        }

        public IActionResult CreateLeaveLog()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateLeaveLog(LeaveLogRequest request)
        {
            if (ModelState.IsValid)
            {
                var formData = new MultipartFormDataContent();
                formData.Add(new StringContent(request.StartDate.ToString()), "StartDate");
                formData.Add(new StringContent(request.EndDate.ToString()), "EndDate");
                formData.Add(new StringContent(request.Reason != null ? request.Reason : ""), "Reason");
                formData.Add(new StringContent("1"), "EmployeeID");

                // Add the file data
                if (request.File != null && request.File.Length > 0)
                {
                    var fileContent = new StreamContent(request.File.OpenReadStream());
                    fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = "File",
                        FileName = request.File.FileName
                    };
                    formData.Add(fileContent);
                }

                HttpResponseMessage response = await client.PostAsync(LeaveLogApiUrl, formData);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Insert successfully!";
                }
                else
                {
                    TempData["Message"] = "Error while calling WebAPI!";
                }
            }
            return Redirect("./LeaveLogIndex");
        }

        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult EmployeeIndex()
        {
            return View();
        }
        public IActionResult OvertimeLogIndex()
        {
            return View();
        }
    }
}

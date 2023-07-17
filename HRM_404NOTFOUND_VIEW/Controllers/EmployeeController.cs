using GroupProject_HRM_Library.DTOs.LeaveLog;
using GroupProject_HRM_View.Models.Employee;
using GroupProject_HRM_View.Models.Error;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json;

namespace GroupProject_HRM_View.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly HttpClient client = null;
        private string LeaveLogApiUrl = "";
        private string EmployeeApiUrl = "";

        public EmployeeController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            LeaveLogApiUrl = "https://localhost:5000/api/LeaveLog";
            EmployeeApiUrl = "https://localhost:5000/api/Employee";
        }

        public IActionResult NotificationIndex()
        {
            string? accessToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                if (role == Constants.Constants.ADMIN)
                {
                    return Redirect(Constants.Constants.NOTFOUND_URL);
                }
                else if (role == Constants.Constants.MANAGER)
                {
                    return Redirect(Constants.Constants.NOTFOUND_URL);
                }

            }
            else
            {
                return Redirect(Constants.Constants.LOGIN_URL);
            }

            return View();
        }

        public IActionResult Income()
        {
            string? accessToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                if (role == Constants.Constants.ADMIN)
                {
                    return Redirect(Constants.Constants.NOTFOUND_URL);
                }
                else if (role == Constants.Constants.MANAGER)
                {
                    return Redirect(Constants.Constants.NOTFOUND_URL);
                }

            }
            else
            {
                return Redirect(Constants.Constants.LOGIN_URL);
            }

            return View();
        }

        public IActionResult LeaveLogIndex()
        {
            string? accessToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                if (role == Constants.Constants.ADMIN)
                {
                    return Redirect(Constants.Constants.NOTFOUND_URL);
                }
                else if (role == Constants.Constants.MANAGER)
                {
                    return Redirect(Constants.Constants.NOTFOUND_URL);
                }

            }
            else
            {
                return Redirect(Constants.Constants.LOGIN_URL);
            }

            return View();
        }

        public IActionResult CreateLeaveLog()
        {
            string? accessToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                if (role == Constants.Constants.ADMIN)
                {
                    return Redirect(Constants.Constants.NOTFOUND_URL);
                }
                else if (role == Constants.Constants.MANAGER)
                {
                    return Redirect(Constants.Constants.NOTFOUND_URL);
                }

            }
            else
            {
                return Redirect(Constants.Constants.LOGIN_URL);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateLeaveLog(LeaveLogRequest request)
        {
            string? accessToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                if (role == Constants.Constants.ADMIN)
                {
                    return Redirect(Constants.Constants.NOTFOUND_URL);
                }
                else if (role == Constants.Constants.MANAGER)
                {
                    return Redirect(Constants.Constants.NOTFOUND_URL);
                }

            }
            else
            {
                return Redirect(Constants.Constants.LOGIN_URL);
            }

            if (ModelState.IsValid)
            {
                var formData = new MultipartFormDataContent();
                formData.Add(new StringContent(request.StartDate.ToString()), "StartDate");
                formData.Add(new StringContent(request.EndDate.ToString()), "EndDate");
                formData.Add(new StringContent(request.Reason != null ? request.Reason : ""), "Reason");
                string? id = HttpContext.Session.GetString("EMPLOYEE_ID");
                formData.Add(new StringContent(id), "EmployeeID");

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
                    string json = await response.Content.ReadAsStringAsync();
                    if ((int)response.StatusCode == StatusCodes.Status400BadRequest)
                    {
                        Error error = JsonConvert.DeserializeObject<Error>(json);
                        string errors = error.Message.FirstOrDefault().DescriptionError.FirstOrDefault();

                        TempData["Error"] = errors;
                    }
                }
            }
            else
            {
                return View();
            }
            return Redirect("./LeaveLogIndex");
        }

        public async Task<IActionResult> Profile()
        {
            string? accessToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                if (role == Constants.Constants.ADMIN)
                {
                    return Redirect(Constants.Constants.NOTFOUND_URL);
                }
                else if (role == Constants.Constants.MANAGER)
                {
                    return Redirect(Constants.Constants.NOTFOUND_URL);
                }
                
            }
            else
            {
                return Redirect(Constants.Constants.LOGIN_URL);
            }

           string? id = HttpContext.Session.GetString("EMPLOYEE_ID");
            HttpResponseMessage response = await client.GetAsync(EmployeeApiUrl + $"/{id}");
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var data = System.Text.Json.JsonSerializer.Deserialize<GetProfileResponseApi>(strData, options);

            return View(data.Data);
        }

        public IActionResult EmployeeIndex()
        {
            string? accessToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                if (role == Constants.Constants.ADMIN)
                {
                    return Redirect(Constants.Constants.NOTFOUND_URL);
                }
                else if (role == Constants.Constants.MANAGER)
                {
                    return Redirect(Constants.Constants.NOTFOUND_URL);
                }

            }
            else
            {
                return Redirect(Constants.Constants.LOGIN_URL);
            }

            return View();
        }
        public IActionResult OvertimeLogIndex()
        {
            string? accessToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                if (role == Constants.Constants.ADMIN)
                {
                    return Redirect(Constants.Constants.NOTFOUND_URL);
                }
                else if (role == Constants.Constants.MANAGER)
                {
                    return Redirect(Constants.Constants.NOTFOUND_URL);
                }

            }
            else
            {
                return Redirect(Constants.Constants.LOGIN_URL);
            }

            return View();
        }
    }
}

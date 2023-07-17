using GroupProject_HRM_Library.DTOs.LeaveLog;
using GroupProject_HRM_Library.DTOs.OvertimeLog;
using GroupProject_HRM_View.Models.Employee;
using GroupProject_HRM_View.Models.Error;
using GroupProject_HRM_View.Models.LeaveLog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json;

namespace GroupProject_HRM_View.Controllers
{
    public class ManagerController : Controller
    {
        private readonly HttpClient client = null;
        private string LeaveLogApiUrl = "";
        private string EmployeeApiUrl = "";
        private string OvertimeApiUrl = "";

        public ManagerController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            LeaveLogApiUrl = "https://localhost:5000/api/LeaveLog";
            EmployeeApiUrl = "https://localhost:5000/api/Employee";
            OvertimeApiUrl = "https://localhost:5000/api/OvertimeLog";
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
                else if (role == Constants.Constants.EMPLOYEE)
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
                else if (role == Constants.Constants.EMPLOYEE)
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

        public async Task<IActionResult> CreateLeaveLogManagerAsync()
        {
            string? accessToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            string? role = HttpContext.Session.GetString("ROLE_NAME");
            string? id = HttpContext.Session.GetString("EMPLOYEE_ID");
            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                if (role == Constants.Constants.ADMIN)
                {
                    return Redirect(Constants.Constants.NOTFOUND_URL);
                }
                else if (role == Constants.Constants.EMPLOYEE)
                {
                    return Redirect(Constants.Constants.NOTFOUND_URL);
                }

            }
            else
            {
                return Redirect(Constants.Constants.LOGIN_URL);
            }

            HttpResponseMessage responseEmployee = await client.GetAsync($"{EmployeeApiUrl}/manager/{id}");
            string strData = await responseEmployee.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var employees = System.Text.Json.JsonSerializer.Deserialize<GetEmployeeIDandNameResponse>(strData, options);
            ViewBag.Employees = new SelectList((System.Collections.IEnumerable)employees.Data, "EmployeeID", "EmployeeName");

            ViewBag.LeaveLogStatuses = new SelectList(ListLeaveLogStatus.Values, "StatusID", "StatusName");

            return View();
        }

        public async Task<IActionResult> CreateOvertimeLogAsync()
        {
            string? accessToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            string? role = HttpContext.Session.GetString("ROLE_NAME");
            string? id = HttpContext.Session.GetString("EMPLOYEE_ID");
            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                if (role == Constants.Constants.ADMIN)
                {
                    return Redirect(Constants.Constants.NOTFOUND_URL);
                }
                else if (role == Constants.Constants.EMPLOYEE)
                {
                    return Redirect(Constants.Constants.NOTFOUND_URL);
                }

            }
            else
            {
                return Redirect(Constants.Constants.LOGIN_URL);
            }

            HttpResponseMessage responseEmployee = await client.GetAsync($"{EmployeeApiUrl}/manager/{id}");
            string strData = await responseEmployee.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var employees = System.Text.Json.JsonSerializer.Deserialize<GetEmployeeIDandNameResponse>(strData, options);
            ViewBag.Employees = new SelectList((System.Collections.IEnumerable)employees.Data, "EmployeeID", "EmployeeName");

            ViewBag.LeaveLogStatuses = new SelectList(ListOvertimeStatus.Values, "StatusID", "StatusName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateLeaveLog(LeaveLogManagerRequest request)
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
                else if (role == Constants.Constants.EMPLOYEE)
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
                formData.Add(new StringContent(request.EmployeeID.ToString()), "EmployeeID");
                formData.Add(new StringContent(request.LeaveLogStatus.ToString()), "Status");

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

                HttpResponseMessage response = await client.PostAsync($"{LeaveLogApiUrl}/manager", formData);

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

        [HttpPost]
        public async Task<IActionResult> CreateOvertimeLog(OvertimeLogRequest request)
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
                else if (role == Constants.Constants.EMPLOYEE)
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
                string strData = System.Text.Json.JsonSerializer.Serialize(request);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(OvertimeApiUrl, contentData);

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

            return Redirect("./OvertimeLogIndex");
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
                else if (role == Constants.Constants.EMPLOYEE)
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
                else if (role == Constants.Constants.EMPLOYEE)
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
                else if (role == Constants.Constants.EMPLOYEE)
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
                else if (role == Constants.Constants.EMPLOYEE)
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
    }
}

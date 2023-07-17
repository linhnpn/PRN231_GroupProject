using GroupProject_HRM_Library.DTOs.EmployeeProject;
using GroupProject_HRM_Library.DTOs.Tax;
using GroupProject_HRM_Library.DTOs.Employee;
using GroupProject_HRM_View.Models.Employee;
using GroupProject_HRM_View.Models.LeaveLog;
using GroupProject_HRM_View.Models.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using GroupProject_HRM_View.Models.Error;

namespace GroupProject_HRM_View.Controllers
{
    public class AdminController : Controller
    {
        private readonly HttpClient client = null;
        private string LeaveLogApiUrl = "";
        private string EmployeeApiUrl = "";
        private readonly string TaxAPIUrl = "";
        private readonly string ProjectApiUrl = "";
        private readonly string EmployeeProjectApiUrl = "";
        private readonly string IncomeApiUrl = "";

        public AdminController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            LeaveLogApiUrl = "https://localhost:5000/api/LeaveLog";
            EmployeeApiUrl = "https://localhost:5000/api/Employee";
            TaxAPIUrl = "https://localhost:5000/api/Tax";
            ProjectApiUrl = "https://localhost:5000/api/Project";
            EmployeeProjectApiUrl = "https://localhost:5000/api/EmployeeProject";
            IncomeApiUrl = "https://localhost:5000/api/Income";
        }
        public IActionResult TaxIndex()
        {
            string? accessToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                if (role == Constants.Constants.EMPLOYEE)
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

        public IActionResult NotificationIndex()
        {
            string? accessToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                if (role == Constants.Constants.EMPLOYEE)
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

        public async Task<IActionResult> CalculateIncome()
        {
            string? accessToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                if (role == Constants.Constants.EMPLOYEE)
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
            var contentData = new StringContent("", System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync($"{IncomeApiUrl}/all", contentData);

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Some employee dont have payroll cannot have new income!";
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

            return Redirect("./EmployeeManagement");
        }

        public async Task<IActionResult> EmplProjCreate(int id)
        {
            string? accessToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                if (role == Constants.Constants.EMPLOYEE)
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
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            HttpResponseMessage responseProject = await client.GetAsync($"{ProjectApiUrl}/projectId={id}");
            string strDataProject = await responseProject.Content.ReadAsStringAsync();
            GetProjectResponseApi? project = System.Text.Json.JsonSerializer.Deserialize<GetProjectResponseApi>(strDataProject, options);

            if (project == null)
            {
                return Redirect(Constants.Constants.NOTFOUND_URL);
            }

            HttpResponseMessage responseEmployee = await client.GetAsync($"{EmployeeApiUrl}/no-project");
            string strData = await responseEmployee.Content.ReadAsStringAsync();
            var employees = System.Text.Json.JsonSerializer.Deserialize<GetEmployeeIDandNameResponse>(strData, options);
            ViewBag.Employees = new SelectList((System.Collections.IEnumerable)employees.Data, "EmployeeID", "EmployeeName");
            ViewBag.EmployeeProjectStatuses = new SelectList(ListEmployeeProjectStatus.Values, "StatusID", "StatusName");

            TempData["ProjectID"] = id;
            return View();
        }
        public IActionResult CreateTax()
        {
            string? accessToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                if (role == Constants.Constants.EMPLOYEE)
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
        public async Task<IActionResult> CreateTax(CreateTaxRequest request)
        {
            string? accessToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                if (role == Constants.Constants.EMPLOYEE)
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
                string strData = System.Text.Json.JsonSerializer.Serialize(request);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(TaxAPIUrl, contentData);

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
            return Redirect("./TaxIndex");
        }
        public IActionResult ProjectIndex()
        {
            string? accessToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                if (role == Constants.Constants.EMPLOYEE)
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

        public IActionResult DetailProjectIndex(int id)
        {
            string? accessToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                if (role == Constants.Constants.EMPLOYEE)
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
            TempData["ProjectID"] = id;
            return View();
        }

        public async Task<IActionResult> Profile()
        {
            string? accessToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                if (role == Constants.Constants.EMPLOYEE)
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

        public async Task<IActionResult> EmployeeManagement()
        {
            string? accessToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                if (role == Constants.Constants.EMPLOYEE)
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

        public async Task<IActionResult> EmployeeDetails(int id)
        {
            string? accessToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                if (role == Constants.Constants.EMPLOYEE)
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

            HttpResponseMessage response = await client.GetAsync(EmployeeApiUrl + $"/{id}");
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var data = System.Text.Json.JsonSerializer.Deserialize<GetProfileResponseApi>(strData, options);

            return View(data.Data);
        }

        public async Task<IActionResult> EditEmployee(int id)
        {
            string? accessToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                if (role == Constants.Constants.EMPLOYEE)
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

            HttpResponseMessage response = await client.GetAsync(EmployeeApiUrl + $"/{id}");
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var data = System.Text.Json.JsonSerializer.Deserialize<GetProfileResponseApi>(strData, options);
            UpdateEmployeeRequest editEmployee = new UpdateEmployeeRequest()
            {
                EmployeeID = data.Data.EmployeeID,
                EmployeeName = data.Data.EmployeeName,
                EmployeeImage = data.Data.EmployeeImage,
                Address = data.Data.Address,
                Gender = data.Data.Gender,
                PhoneNumber = data.Data.PhoneNumber,
                EmailAddress = data.Data.EmailAddress,
                BirthDate = data.Data.BirthDate,
                EmployeeStatus = data.Data.EmployeeStatus,
                RoleID = data.Data.RoleId
            };
            return View(editEmployee);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmployee(UpdateEmployeeRequest employeeUpdate)
        {
            string? accessToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                if (role == Constants.Constants.EMPLOYEE)
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
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var jsonContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(employeeUpdate), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(EmployeeApiUrl, jsonContent);
            string strData = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            return RedirectToAction("EmployeeManagement");
        }


        public IActionResult CreateEmployee()
        {
            string? accessToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                if (role == Constants.Constants.EMPLOYEE)
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


        public async Task<IActionResult> AddEmployeeForProject(CreateEmployeeProjectRequest request)
        {
            string? accessToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                if (role == Constants.Constants.EMPLOYEE)
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
                string strData = System.Text.Json.JsonSerializer.Serialize(request);
                var contentData = new StringContent(strData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(EmployeeProjectApiUrl, contentData);

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
            return Redirect("./DetailProjectIndex/" + request.ProjectID);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeRequest createEmployeeRequest)
        {
            string? accessToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                if (role == Constants.Constants.EMPLOYEE)
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
            //HttpResponseMessage response = await client.GetAsync(EmployeeApiUrl + $"{}")
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var jsonContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(createEmployeeRequest), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(EmployeeApiUrl, jsonContent);
            string strData = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            return RedirectToAction("EmployeeManagement");
        }


        public async Task<IActionResult> AssignEmployee()
        {
            string? accessToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                if (role == Constants.Constants.EMPLOYEE_URL)
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

            HttpResponseMessage response = await client.GetAsync(EmployeeApiUrl + $"/NotStart");
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var dataEmployees = System.Text.Json.JsonSerializer.Deserialize<GetEmployeesNotStartResponseApi>(strData, options);

            response = await client.GetAsync(ProjectApiUrl + $"/CanAssign");
            strData = await response.Content.ReadAsStringAsync();
            var dataProject = System.Text.Json.JsonSerializer.Deserialize<GetProjectCanAssignReponseApi>(strData, options);
            if(!dataProject.Success || !dataEmployees.Success)
            {
                ViewBag.Error = "Currently can't assign employee.";
            }
            string error = TempData["msg_error"] as string;
            if(error != null)
            {
                ViewBag.msgError = error;
            }
            Dictionary<int, string> employeeDictionary = dataEmployees.Data.ToDictionary(c => c.EmployeeID, c => c.UserName);

            Dictionary<int, string> projectDictionary = dataProject.Data.ToDictionary(c => c.ProjectID, c => c.ProjectName);

            ViewBag.ListEmployee = employeeDictionary;
            ViewBag.ListProject = projectDictionary;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AssignEmployee(AssignEmployeeToProjectRequest assignRequest)
        {
            string? accessToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                if (role == Constants.Constants.EMPLOYEE_URL)
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

            if(DateTime.Compare(assignRequest.EndDate, assignRequest.StartDate) < 0)
            {
                string msg_error = "End date need later than Start date";
                TempData["msg_error"] = msg_error;
                return RedirectToAction("AssignEmployee");
            }
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var jsonContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(assignRequest), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(EmployeeProjectApiUrl + "/AssignEmployee", jsonContent);
            string strData = await response.Content.ReadAsStringAsync();
            if(strData.Contains("instance with the same key value"))
            {
                TempData["msg_error"] = "This employee is currently in the project";
                return RedirectToAction("AssignEmployee");
            }
            //response.EnsureSuccessStatusCode();
            return RedirectToAction("DetailProjectIndex", new { id = assignRequest.ProjectId});
        }

    }
}

using GroupProject_HRM_Library.DTOs.Tax;
using GroupProject_HRM_Library.DTOs.Employee;
using GroupProject_HRM_View.Models.Employee;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace GroupProject_HRM_View.Controllers
{
    public class AdminController : Controller
    {
        private readonly HttpClient client = null;
        private string LeaveLogApiUrl = "";
        private string EmployeeApiUrl = "";
        private readonly string TaxAPIUrl = "";

        public AdminController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            LeaveLogApiUrl = "https://localhost:5000/api/LeaveLog";
            EmployeeApiUrl = "https://localhost:5000/api/Employee";
            TaxAPIUrl = "https://localhost:5000/api/Tax";
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
                    TempData["Message"] = "Error while calling WebAPI!";
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
            var data = JsonSerializer.Deserialize<GetProfileResponseApi>(strData, options);

            return View(data.Data);
        }

        public async Task<IActionResult> EmployeeManagement()
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
            return View();
        }

        public async Task<IActionResult> EmployeeDetails(int id)
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

            HttpResponseMessage response = await client.GetAsync(EmployeeApiUrl + $"/{id}");
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var data = JsonSerializer.Deserialize<GetProfileResponseApi>(strData, options);

            return View(data.Data);
        }

        public async Task<IActionResult> EditEmployee(int id)
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

            HttpResponseMessage response = await client.GetAsync(EmployeeApiUrl + $"/{id}");
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var data = JsonSerializer.Deserialize<GetProfileResponseApi>(strData, options);
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
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var jsonContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(employeeUpdate), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(EmployeeApiUrl , jsonContent);
            string strData = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            return RedirectToAction("EmployeeManagement");
        }


        public async Task<IActionResult> CreateEmployee()
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeRequest createEmployeeRequest)
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

    }
}

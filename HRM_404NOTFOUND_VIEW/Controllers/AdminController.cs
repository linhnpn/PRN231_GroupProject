using GroupProject_HRM_View.Models.Employee;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace GroupProject_HRM_View.Controllers
{
    public class AdminController : Controller
    {
        private readonly HttpClient client = null;
        private string LeaveLogApiUrl = "";
        private string EmployeeApiUrl = "";

        public AdminController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            LeaveLogApiUrl = "https://localhost:5000/api/LeaveLog";
            EmployeeApiUrl = "https://localhost:5000/api/Employee";
        }
        public IActionResult ProjectIndex()
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

        public async Task<IActionResult> Profile()
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
    }
}

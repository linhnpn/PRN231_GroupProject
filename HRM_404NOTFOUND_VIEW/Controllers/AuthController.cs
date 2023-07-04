using GroupProject_HRM_Library.DTOs.Authenticate;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using HRM_404NOTFOUND_VIEW.Models;
using System.Diagnostics;
using GroupProject_HRM_View.Models.Auth;

namespace GroupProject_HRM_View.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly HttpClient client = null;
        private string AuthenAPI_URL = "";

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            client.BaseAddress = new Uri("https://localhost:5000");
            AuthenAPI_URL = "/api/Auth";
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthenRequest authenRequest)
        {
            var json = JsonSerializer.Serialize(authenRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            AuthenAPI_URL += "/Authenticate";
            HttpResponseMessage response = await client.PostAsync(AuthenAPI_URL, content);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.error = "Username or Password is invalid.";
                return View();
            }
            string jsonResponse = await response.Content.ReadAsStringAsync();
            var authenResponse = JsonSerializer.Deserialize<GetAuthResponse>(jsonResponse, options);
            var reponseAuthen = authenResponse.Data;
            if (authenResponse.Success)
            {
                HttpContext.Session.SetString("ACCESS_TOKEN", reponseAuthen.AccessToken);
                HttpContext.Session.SetString("ROLE_NAME", reponseAuthen.RoleName);
                HttpContext.Session.SetString("EMPLOYEE_ID", reponseAuthen.EmployeeID.ToString());
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", reponseAuthen.AccessToken);
                TempData["ACCESS_TOKEN"] = reponseAuthen.AccessToken;
                TempData["ROLE_NAME"] = reponseAuthen.RoleName;
                TempData["EMPLOYEE_ID"] = reponseAuthen.EmployeeID.ToString();
            }
            if (reponseAuthen!.RoleName == Constants.Constants.ADMIN)
            {
                return base.Redirect(Constants.Constants.ADMIN_URL);
            }
            else if (reponseAuthen!.RoleName == Constants.Constants.MANAGER)
            {
                return base.Redirect(Constants.Constants.MANAGER_URL);
            }
            else
            {
                return base.Redirect(Constants.Constants.EMPLOYEE_URL);
            }

        }

        public IActionResult Login()
        {
            string? accessToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (!string.IsNullOrEmpty(accessToken) && !string.IsNullOrEmpty(role))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                if (role == Constants.Constants.ADMIN)
                {
                    return Redirect(Constants.Constants.ADMIN_URL);
                }
                else if (role == Constants.Constants.MANAGER)
                {
                    return Redirect(Constants.Constants.MANAGER_URL);
                }
                else if (role == Constants.Constants.EMPLOYEE)
                {
                    return Redirect(Constants.Constants.EMPLOYEE_URL);
                }
            }

            return View();
        }

        public IActionResult NotFound()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect(Constants.Constants.LOGIN_URL);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

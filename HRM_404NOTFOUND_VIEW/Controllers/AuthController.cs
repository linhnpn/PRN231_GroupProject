using GroupProject_HRM_Library.DTOs.Authenticate;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using HRM_404NOTFOUND_VIEW.Models;
using System.Diagnostics;
using GroupProject_HRM_View.Constants;

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
            var authenResponse = JsonSerializer.Deserialize<AuthenResponse>(jsonResponse, options);
            HttpContext.Session.SetString("ACCESS_TOKEN", authenResponse!.AccessToken);
            HttpContext.Session.SetString("ROLE_NAME", authenResponse!.RoleName);

            if (authenResponse!.RoleName == Constants.Constants.ADMIN)
            {
                return base.Redirect(Constants.Constants.ADMIN_URL);
            }
            else if (authenResponse!.RoleName == Constants.Constants.MANAGER)
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

using GroupProject_HRM_Library.DTOs.Authenticate;
using GroupProject_HRM_Library.Models;
using HRM_404NOTFOUND_VIEW.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace HRM_404NOTFOUND_VIEW.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient client = null;
        private string AuthenAPI_URL = "";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            client.BaseAddress = new Uri("https://localhost:5000");
            AuthenAPI_URL = "/api/Auth";
        }

        public IActionResult Index()
        {
            return View();
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
                return View("Index");
            }
            string jsonResponse = await response.Content.ReadAsStringAsync();
            var authenResponse = JsonSerializer.Deserialize<AuthenResponse>(jsonResponse, options);
            HttpContext.Session.SetString("ACCESS_TOKEN", authenResponse.AccessToken);
            //return main page
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("ACCESS_TOKEN");
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
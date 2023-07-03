using Microsoft.AspNetCore.Mvc;

namespace GroupProject_HRM_View.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

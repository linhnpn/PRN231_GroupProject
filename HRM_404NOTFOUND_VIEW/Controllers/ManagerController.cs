using Microsoft.AspNetCore.Mvc;

namespace GroupProject_HRM_View.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult LeaveLogIndex()
        {
            return View();
        }

        public IActionResult OvertimeLogIndex()
        {
            return View();
        }
    }
}

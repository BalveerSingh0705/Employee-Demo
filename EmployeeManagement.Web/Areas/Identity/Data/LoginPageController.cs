using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagmentUI.Areas.Identity.Data
{
    public class LoginPageController : Controller
    {
        [Area("Identity")]
        public class HomeController : Controller
        {
            public IActionResult Index()
            {
                return View();
            }
        }
    }
}

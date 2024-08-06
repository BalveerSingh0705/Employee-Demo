using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagment.Web.Controllers
{
    public class AttendancesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

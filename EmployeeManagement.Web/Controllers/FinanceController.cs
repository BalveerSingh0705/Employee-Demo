using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagment.Web.Controllers
{
    public class FinanceController : Controller
    {
        public IActionResult Salary()
        {
            return View();
        }
        public IActionResult Advance()
        {
            return View();  
        }
    }
}

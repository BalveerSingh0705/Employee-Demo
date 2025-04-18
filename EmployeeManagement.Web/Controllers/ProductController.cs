using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagment.Web.Controllers
{
    public class ProductController : Controller
    {
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public IActionResult Index()
        {
            return View();
        }
    }
}

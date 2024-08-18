using EmployeeManagement.Core.Common;
using EmployeeManagement.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Linq.Expressions;

namespace EmployeeManagment.Web.Controllers
{
    public class FinanceController : Controller
    {
        [HttpGet]
        public IActionResult Salary()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Advance()
        {
            return View();  
        }
        [HttpPost]
        [Route("FinanceController/AdvanceSalary")]
        public async Task<IActionResult> AdvanceSalary([FromBody] AdvanceSalaryEntity advanceSalaryEntity)
        {
            // Here, you can implement the logic for creating the employee data
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
            }

            try
            {
                var responce = await ProxyService.AdvanceSalary(advanceSalaryEntity);
                if(responce==true)
                {
                    return Json(new { success = true, Massage = "Advance Paymemt done..."});
                }
                return Json(responce);


            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = "response failed" });

            }

        }
        [HttpPost]
        [Route("FinanceController/FinalSalary")]
        public async Task<ActionResult<EmployeeSalaryEntity>> FinalSalaryAsync(SalaryRequestEntity salaryRequestEntity)
        {
            // Validate empID
            //if (string.IsNullOrEmpty(salaryRequestEntity.EmpID))
            //{
            //    return BadRequest("Employee ID cannot be null or empty.");
            //}

            //

            // Validate month
            if (string.IsNullOrEmpty(salaryRequestEntity.Month) || !DateTime.TryParseExact(salaryRequestEntity.Month, "MMMM", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedMonth))
            {
                return BadRequest("Invalid month. Please provide a valid month name (e.g., 'January').");
            }

            // Validate year
            if (salaryRequestEntity.Year < 2000 || salaryRequestEntity.Year > DateTime.Now.Year)
            {
                return BadRequest("Invalid year. Please provide a year between 2000 and the current year.");
            }
            try
            {
                var responce = await ProxyService.FinalSalary(salaryRequestEntity);
                return Json(new { success = true, Response= responce });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, });
            }
             return Json(new { success = false, });

        }

    }
}

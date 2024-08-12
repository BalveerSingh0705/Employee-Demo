using EmployeeManagement.Core.Common;
using EmployeeManagement.Web.Helper;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagment.Web.Controllers
{
    public class AttendancesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("AttendancesController/GetEmployeeAttendances")]
        public async Task<IActionResult> GetEmployeeAttendances()
        {
            try
            {
                var responce = await ProxyService.GetEmployeeDetailsInAttendanceTable();
                return Json(responce);


            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = "response failed" });

            }
            
        }
        [HttpPost]

        [Route("/AttendancesController/SendEmployeeAttendanceDetails")]
        public async Task<IActionResult> SendEmployeeAttendanceDetails([FromBody] List<AttendanceDataSendEntity> attendanceDataSendEntity)
        {
            if (attendanceDataSendEntity == null || !ModelState.IsValid)
            {
                return BadRequest(new { success = false, error = "Invalid data submitted." });
            }

            try
            {
                var response = await ProxyService.SendEmployeeAttendanceDetails(attendanceDataSendEntity);
                return Ok(new { success = true, data = response });
            }
            catch (Exception ex)
            {
                // Log the exception details if necessary
                return StatusCode(500, new { success = false, error = "An error occurred while processing the request." });
            }
        }
    }
}


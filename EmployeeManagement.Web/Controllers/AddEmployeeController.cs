using EmployeeManagement.Core.Common;
using EmployeeManagement.Core.Entities;
using EmployeeManagement.Web.Helper;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMvcFull.Controllers
{
    public class AddEmployeeController : Controller
    {
        // Add Employee Details
        public IActionResult AddEmployeeDetails()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddEmployeePage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployeeData([FromBody] EmployeeEntity employeeEntity)
        {
            var emp =  employeeEntity.aadhaarCard;
            // Here, you can implement the logic for creating the employee data
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, error = "Invalid Employee " });
            }
           
            try
            {
            
                    // Use the _proxyService to create a new employee
                var responce = await ProxyService.CreateEmployeeAsync(employeeEntity);
                return Json(new { success = responce, message = "Employee created successfully." });


            }
            catch
            {
                //{
                //  //  return Json(new { success = false, message = "Validation failed.", errors = ModelState.Values.SelectMany(v => v.Errors) });
                //    ModelState.AddModelError("", ex.Message);
                //}
            }
            // Save employee data logic here

            return Json(new { success = false, error = "Invalid Employee " });
        }

        [HttpPost]
        //[Route("ViewEmployeeAllDetails")]
        public IActionResult ViewEmployeeAllDetails([FromBody] EmployeeDataInIDEntity employeeDataInIDEntity)
        {
            if (employeeDataInIDEntity.EmpID <= 0)
            {
                return Json(new { success = false, error = "Invalid Employee ID" });
            }

            var empID = employeeDataInIDEntity.EmpID;
            var employeeDetails = GetEmployeeDetails(empID);

            if (employeeDetails != null)
            {
                return Json(new { success = true, data = employeeDetails });
            }
            else
            {
                return Json(new { success = false, error = "Employee not found" });
            }
        }

        [HttpPost]
       // [Route("SaveEmployeeChanges")]
        public IActionResult SaveEmployeeChanges([FromBody] EmployeeDetails updatedDetails)
        {
            bool updateSuccess = UpdateEmployeeDetails(updatedDetails);

            if (updateSuccess)
            {
                return Json(new { success = true, empName = updatedDetails.EmpName, empPosition = updatedDetails.EmpPosition });
            }
            else
            {
                return Json(new { success = false, error = "Error updating employee details" });
            }
        }

        private EmployeeDetails GetEmployeeDetails(int empID)
        {
            return new EmployeeDetails
            {
                EmpID = empID,
                EmpName = "John Doe",
                EmpPosition = "Software Engineer"
                // Add other fields as needed
            };
        }

        private bool UpdateEmployeeDetails(EmployeeDetails updatedDetails)
        {
            return true; // Replace with actual update logic
        }

        [HttpPost]
       // [Route("DeleteEmployeeDetails")] // Ensure this matches your AJAX request URL
        public IActionResult DeleteEmployeeDetails([FromBody] EmployeeDataInIDEntity employeeDataInIDEntity)
        {
            bool deleteSuccess = DeleteEmployee(employeeDataInIDEntity.EmpID); // Assume EmpID is sufficient for deletion

            if (deleteSuccess)
            {
                return Json(new { success = true, message = "Employee deleted successfully." });
            }
            else
            {
                return Json(new { success = false, error = "Error deleting employee details" });
            }
        }

        // Dummy method for deleting employee details
        private bool DeleteEmployee(int empID)
        {
            // Your delete logic here
            // Return true if deletion is successful, otherwise false
            return true; // Replace with actual delete result
        }








    }
}




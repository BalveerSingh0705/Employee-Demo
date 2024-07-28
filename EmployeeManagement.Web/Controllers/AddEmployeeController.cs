using EmployeeManagement.Core.Common;
using EmployeeManagement.Core.Entities;
using EmployeeManagement.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

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



        public async Task<IActionResult> GetEmployeeDetailsInTableForm()
        {

            try
            {
                var responce = await ProxyService.GetEmployeeDetailsInTableForm();
                return Json(responce);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = "response failed" });
            }



            return Json(new { success = false, error = "Invalid Employee " });

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
                return Json(new { success = responce, message = "Employee Add successfully." });

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

        [HttpGet]
        //[Route("ViewEmployeeAllDetails")]
        public async Task<IActionResult> ViewEmployeeAllDetails([FromBody] EmployeeDataInIDEntity employeeDataInIDEntity)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, error = "Invalid Employee " });
            }
            try {

                var responce = await ProxyService.ViewEmployeeAllDetails(employeeDataInIDEntity);
                return Json(responce);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = "response failed" });
            }



            return Json(new { success = false, error = "response failed" });


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

        //private EmployeeDetails GetEmployeeDetails(int empID)
        //{
        //    return new EmployeeDetails
        //    {
        //        EmpID = empID,
        //        EmpName = "John Doe",
        //        EmpPosition = "Software Engineer"
        //        // Add other fields as needed
        //    };
        //}

        private bool UpdateEmployeeDetails(EmployeeDetails updatedDetails)
        {
            return true; // Replace with actual update logic
        }
        /// <summary>
        /// Used to  Delete Template Field
        /// </summary>
        /// <param name="EmployeeDataInIDEntity"></param>
        /// <returns></returns>
             [HttpPost]
            public async Task<JsonResult> DeleteSingleEmployeeDetails([FromBody] EmployeeDataInIDEntity employeeDataInIDEntity)
            {
                bool isSuccess = false;
               
                try
                {
                    isSuccess = await ProxyService.DeleteSingleEmployeeDetails(employeeDataInIDEntity);
              
                if (isSuccess == true)
                    {
                     //   CacheManager.DeleteAllKeys(templateField.TenantId);

                    }
                    return Json(isSuccess);

                }
                catch (Exception ex)
                {
                    //using (LogException _error = new LogException(typeof(ConfigurationController), TenantCache.GetSqlDbConnectionFromCacheByTenantId(UserInfo.GetTenantId())))
                    //{
                    //    _error.Exception("Exception in DeleteTemplateField:", ex, TenantCache.GetSubDomainByTenantId((Guid)UserInfo.GetTenantId()), UserInfo.GetUserName(), templateField);

                    //}

                }
                return Json(isSuccess);

            }
    }

    
}




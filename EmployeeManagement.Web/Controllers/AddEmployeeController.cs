using AngleSharp.Io;
using Azure;
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
            

            return Json(new { success = false, error = "Invalid Employee " });
        }

        [HttpPost]
        //[Route("ViewEmployeeAllDetails")]
        public async Task<IActionResult> GetEmployeeDetailsClickOnEditButton([FromBody] EmployeeDataInIDEntity employeeDataInIDEntity)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, error = "Invalid Employee " });
            }
            try {

                var response = await ProxyService.GetEmployeeDetailsClickOnEditButton(employeeDataInIDEntity);
                return Json(new { success = response, message = "Employee Add successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = "response failed" });
            }



            return Json(new { success = false, error = "response failed" });


        }

        [HttpPost]
        // [Route("SaveEmployeeChanges")]
        public async Task<IActionResult> SaveEmployeeChangesInfo([FromBody] EmployeeEntity employeeEntity)
        {
            {
                // Here, you can implement the logic for creating the employee data
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, error = "Invalid Employee " });
                }
                try
                {
                    // Use the _proxyService to create a new employee
                    var responce = await ProxyService.SaveEmployeeChangesInfo(employeeEntity);
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




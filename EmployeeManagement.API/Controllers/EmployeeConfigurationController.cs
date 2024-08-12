using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Core.Common;
using EmployeeManagement.Business;
using EmployeeManagement.Core.Entities;

namespace EmployeeManagement.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeConfigurationController : ControllerBase
    {
        [HttpPost]
        [Route("SaveOrUpdateEmployeeDetails")]
        public IActionResult SaveOrUpdateEmployeeDetails([FromBody] EmployeeEntity employeeEntity)
        {
            try
            {
                var result = SingletonBO<ConfigurationBO>.Instance.SaveOrUpdateEmployeeDetails(employeeEntity);
                return Ok(result);
                //SaveOrUpdateTemplateFieldData
            }
            catch (Exception ex)
            {
                //    using (LogException _error = new LogException(typeof(EmployeeConfigurationController), TenantCache.GetSqlDbConnectionFromCacheByTenantId(template.TenantId)))
                //    {
                //        _error.Exception("SaveOrUpdateTemplateFieldData", ex, TenantCache.GetSubDomainByTenantId((Guid)template.TenantId), template.UserName, template);
                //}
                // success

                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
        /// <summary>
        ///This method is used to  Get Template FieldDetails by passing template name
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetEmployeeDetailsInTableForm")]
        public IActionResult GetEmployeeDetailsInTableForm()
        {
            try
            {
                return Ok(SingletonBO<ConfigurationBO>.Instance.GetEmployeeDetailsInTableForm());
            }
            catch (Exception ex)
            {
                //using (LogException _error = new LogException(typeof(ConfigurationController), TenantCache.GetSqlDbConnectionFromCacheByTenantId(template.TenantId)))
                //{
                //    _error.Exception("GetTemplateFieldsByTemplateName", ex, TenantCache.GetSubDomainByTenantId((Guid)template.TenantId), template.UserName, template);
                //}
            }
            return BadRequest();
        }


        [HttpPost]
        [Route("DeleteSingleEmployeeDetails")]
        public IActionResult DeleteSingleEmployeeDetails([FromBody] EmployeeDataInIDEntity employeeDataInIDEntity)
        {
            try
            {
                var result = SingletonBO<ConfigurationBO>.Instance.DeleteSingleEmployeeDetails(employeeDataInIDEntity);
                  return Ok(result);
                //SaveOrUpdateTemplateFieldData
            }
            catch (Exception ex)
            {
                //    using (LogException _error = new LogException(typeof(EmployeeConfigurationController), TenantCache.GetSqlDbConnectionFromCacheByTenantId(template.TenantId)))
                //    {
                //        _error.Exception("SaveOrUpdateTemplateFieldData", ex, TenantCache.GetSubDomainByTenantId((Guid)template.TenantId), template.UserName, template);
                //}
                // success

                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
        [HttpPost]
        [Route("GetEmployeeDetailsClickOnEditButton")]
        public IActionResult GetEmployeeDetailsClickOnEditButton(EmployeeDataInIDEntity employeeDataInIDEntity)
        {
            try
            {
                var employeeDetails = SingletonBO<ConfigurationBO>.Instance.GetEmployeeDetailsClickOnEditButton(employeeDataInIDEntity);
                return Ok(employeeDetails);
            }
            catch (Exception ex)
            {
                // Uncomment and modify the following lines according to your logging implementation
                // using (LogException _error = new LogException(typeof(ConfigurationController), TenantCache.GetSqlDbConnectionFromCacheByTenantId(template.TenantId)))
                // {
                //     _error.Exception("GetTemplateFieldsByTemplateName", ex, TenantCache.GetSubDomainByTenantId((Guid)template.TenantId), template.UserName, template);
                // }

                // Return a meaningful error response
                return StatusCode(500, "Internal server error");
            }
        }

        
             [HttpPost]
        [Route("SaveEmployeeChangesInfo")]
        public IActionResult SaveEmployeeChangesInfo([FromBody] EmployeeEntity employeeEntity)
        {
            try
            {
                var result = SingletonBO<ConfigurationBO>.Instance.SaveEmployeeChangesInfo(employeeEntity);
                return Ok(result);
                //SaveOrUpdateTemplateFieldData
            }
            catch (Exception ex)
            {
                //    using (LogException _error = new LogException(typeof(EmployeeConfigurationController), TenantCache.GetSqlDbConnectionFromCacheByTenantId(template.TenantId)))
                //    {
                //        _error.Exception("SaveOrUpdateTemplateFieldData", ex, TenantCache.GetSubDomainByTenantId((Guid)template.TenantId), template.UserName, template);
                //}
                // success

                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
        /// <summary>
        /// GetEmployeeDetailsInTableForm
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetEmployeeDetailsInAttendanceTable")]
        public IActionResult GetEmployeeDetailsInAttendanceTable()
        {
            try
            {
                var response = SingletonBO<ConfigurationBO>.Instance.GetEmployeeDetailsInAttendanceTable();

                if (response == null || !response.Any())
                {
                    return BadRequest(new ApiResponseEntity<AttendanceTableEntity>
                    {
                        IsSuccess = false,
                        StatusCode = 400,
                        Message = "No employee details found in the attendance table."
                    });
                }

                //return Ok(new ApiResponseEntity<List<AttendanceTableEntity>>
                //{
                //    IsSuccess = true,
                //    StatusCode = 200,
                //    Message = "Employee details retrieved successfully.",
                //    Data = response
                //});
               return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception here using your logging mechanism
                // Example:
                // LogError("GetEmployeeDetailsInAttendanceTable", ex);

                return StatusCode(500, new ApiResponseEntity<string>
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = "An error occurred while fetching employee details.",
                    Data = ex.Message // Consider using a more secure message in production
                });
            }
        }

        
         [HttpPost]
        [Route("SendEmployeeAttendanceDetails")]
        public IActionResult SendEmployeeAttendanceDetails([FromBody] List<AttendanceDataSendEntity> attendanceDataSendEntity)
        {
            if (attendanceDataSendEntity == null)
            { return BadRequest(ModelState);
            }
               
            try
            {
                var result = SingletonBO<ConfigurationBO>.Instance.SendEmployeeAttendanceDetails(attendanceDataSendEntity);
                return Ok(result);
               
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }



    }
}
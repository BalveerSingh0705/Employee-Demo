using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Core.Common;
using EmployeeManagement.Business;

namespace EmployeeManagement.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeConfigurationController : ControllerBase
    {
        [HttpPost]
        [Route("SaveOrUpdateEmployeeDetails")]
        public IActionResult SaveOrUpdateEmployeeDetails([FromBody]EmployeeEntity employeeEntity)
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
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}

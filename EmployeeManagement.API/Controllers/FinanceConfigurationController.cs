using EmployeeManagement.Business;
using EmployeeManagement.Business.Base;
using EmployeeManagement.Core.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinanceConfigurationController : ControllerBase
    {
        [HttpPost]
        [Route("AdvanceSalary")]
        public IActionResult AdvanceSalary([FromBody] AdvanceSalaryEntity advanceSalaryEntity)
        {
            try
            {
                var result = SingletonBO<FinanceBO>.Instance.AdvanceSalary(advanceSalaryEntity);
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
        [Route("FinalSalary")]
        public async Task<IActionResult> FinalSalary([FromBody] SalaryRequestEntity salaryRequestEntity)
        {
            try
            {
                // Call the business logic method to get salary details
                var result = await Task.Run(() => SingletonBO<FinanceBO>.Instance.FinalSalary(salaryRequestEntity));

                // Return the result as JSON
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception (implement logging as needed)
                // Example: LogException(ex);

                // Return a 500 Internal Server Error response
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

    }
}


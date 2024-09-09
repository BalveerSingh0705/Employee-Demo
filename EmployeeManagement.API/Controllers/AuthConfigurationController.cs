using EmployeeManagement.Business;
using EmployeeManagement.Core.Common;
using EmployeeManagement.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthConfigurationController : ControllerBase
    {

        [HttpPost]
        [Route("AuthRegister")]
        public async Task<IActionResult> AuthRegister([FromBody] AuthRegisterViewModel authRegisterViewModel)
        {
            try
            {
                var response =await SingletonBO<AuthBO>.Instance.AuthRegister(authRegisterViewModel);
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception and return a meaningful error response
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}

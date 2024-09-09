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
                var response = await SingletonBO<AuthBO>.Instance.AuthRegister(authRegisterViewModel);
                if (response.Success)
                {
                    return Ok(response); // Return a 200 OK with the success response
                }
                else
                {
                    return BadRequest(new AuthResponse
                    {
                        Success = false,
                        Message = response.Message ?? "Registration failed due to unknown reasons.",
                        UserId = response.UserId
                    });
                }
            }
            catch (Exception ex)
            {
                // Handle the exception and return a meaningful error response
                return StatusCode(500, new AuthResponse
                {
                    Success = true,
                    Message = $"Internal server error: {ex.Message}",
                    UserId = null
                });
            }
        }


    }
}

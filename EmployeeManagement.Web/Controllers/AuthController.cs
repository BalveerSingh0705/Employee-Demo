using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreMvcFull.Models;
using EmployeeManagement.Core.Common;
using EmployeeManagement.Web.Helper;

namespace AspnetCoreMvcFull.Controllers;
/// <summary>
/// Auth Controller
/// </summary>
public class AuthController : Controller
{

    public IActionResult RegisterBasic()
    {

        return View();
    }

     public IActionResult ForgotPasswordBasic() => View();
     public IActionResult LoginBasic() => View();

    [HttpPost]

    [Route("/AuthController/AuthRegister")]
    public async Task<IActionResult> AuthRegister([FromBody] AuthRegisterViewModel authRegisterViewModel)
    {
        if (authRegisterViewModel == null || !ModelState.IsValid)
        {
            return BadRequest(new { success = false, error = "Invalid data submitted." });
        }

        try
        {
            var response = await ProxyService.AuthRegister(authRegisterViewModel);
            return Ok(response);
        }
        catch (Exception ex)
        {
            // Log the exception details if necessary
            return StatusCode(500, new { success = false, error = "An error occurred while processing the request." });
        }
    }




}

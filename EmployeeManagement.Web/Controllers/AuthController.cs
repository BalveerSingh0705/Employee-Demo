using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreMvcFull.Models;

namespace AspnetCoreMvcFull.Controllers;
/// <summary>
/// Auth Controller
/// </summary>
public class AuthController : Controller
{
  public IActionResult ForgotPasswordBasic() => View();
  public IActionResult LoginBasic() => View();
  public IActionResult RegisterBasic() => View();
}

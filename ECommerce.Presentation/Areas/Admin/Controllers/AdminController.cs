using Common.Core.Enums;
using Common.Core.Extentions;
using Common.Helper.Encrypt;
using ECommerce.Domain.Dtos;
using ECommerce.Infrastructure.Services;
using ECommerce.Presentation.Filter;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Presentation.Areas.Admin.Controllers
{
  [Area("Admin")]
  [Route("[controller]/[action]/{id?}")]
  public class AdminController : Controller
  {
    private readonly ManagerUserService _userService;

    public AdminController(ManagerUserService userService)
    {
      _userService = userService;
    }

    [ManagerUserAuthFilter]
    public IActionResult Index()
    {
      return View();
    }

    //NuWOkUlAMj0=
    public IActionResult Login()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto model)
    {
      var result = await _userService.Login(model);

      if (result.ResultStatus == ResultStatus.Success)
      {
        HttpContext.Session.Set("ManagerUser", result.Data);
        return RedirectToAction(nameof(Index));
      }

      ViewBag.Result = result;

      return View(model);
    }
  }
}

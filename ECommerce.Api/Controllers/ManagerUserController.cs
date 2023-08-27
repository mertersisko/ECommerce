using ECommerce.Domain.Dtos;
using ECommerce.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class ManagerUserController : ControllerBase
  {
    private readonly ManagerUserService _managerUserService;

    public ManagerUserController(ManagerUserService managerUserService)
    {
      _managerUserService = managerUserService;
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto model)
    {
      var result = await _managerUserService.Login(model);

      return Ok(result);
    }

    //1.Adım Api Controller
    //2.Adımda Route İşlemi yapıyorum
    //Fetch , Axios
    //api Test için Conveyor by Keyoti ext.
  }
}

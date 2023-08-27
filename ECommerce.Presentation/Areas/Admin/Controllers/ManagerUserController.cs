using Common.Core.Extentions;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Services;
using ECommerce.Presentation.Filter;
using ECommerce.Validation.EntitiesValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Presentation.Areas.Admin.Controllers
{
  [Area("Admin")]
  [Route("[controller]/[action]/{id?}")]

  public class ManagerUserController : Controller
  {
    private readonly ManagerUserService _managerUserService;

    public ManagerUserController(ManagerUserService managerUserService)
    {
      _managerUserService = managerUserService;
    }

    [ManagerUserAuthFilter]
    public async Task<IActionResult> List()
    {
      var managerUserListResult = await _managerUserService.GetProtectedManagerUserList();

      return View(managerUserListResult.DataList);
    }

    [ManagerUserAuthFilter]
    public IActionResult Add()
    {
      return View();
    }

    [HttpPost]
    [ManagerUserAuthFilter]
    public async Task<IActionResult> Add(ManagerUser model)
    {
      if (_managerUserService.IsMailAdressUniq(model.EMail))
        ModelState.AddModelError("EMail","Mail Adresi Kullanımda");
      
      if (!ModelState.IsValid)
        return BadRequest(ModelStateValidationHelper.GetModelErrorList(ModelState));

      return Ok(await _managerUserService.AddManagerUser(model));
    }

    [ManagerUserAuthFilter]
    public async Task<IActionResult> Update(string id)
    {
      if (string.IsNullOrEmpty(id))
        return RedirectToAction(nameof(List));

      var managerUserResult = await _managerUserService.GetManagerUser(id);

      if (managerUserResult.Data == null)
        return RedirectToAction(nameof(List));


      return View(managerUserResult.Data);

    }

    [HttpPost]
    [ManagerUserAuthFilter]
    public async Task<IActionResult> Update(ManagerUser model)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelStateValidationHelper.GetModelErrorList(ModelState));


      var updateResult = await _managerUserService.UpdateManagerUser(model);

      return Ok(updateResult);
    }



    [ManagerUserAuthFilter]
    public async Task<IActionResult> ChangeStatus(string id)
    {
      await _managerUserService.ManagerUserChangeStatus(id);

      var managerUserList = await _managerUserService.GetProtectedManagerUserList();

      return PartialView("ManagerUserPartials/_ManagerUserListPartial", managerUserList.DataList);
    }


    [ManagerUserAuthFilter]
    public async Task<IActionResult> Delete(string id)
    {
      await _managerUserService.ManagerUserDelete(id);

      var managerUserList = await _managerUserService.GetProtectedManagerUserList();

      return PartialView("ManagerUserPartials/_ManagerUserListPartial", managerUserList.DataList);
    }
  }
}

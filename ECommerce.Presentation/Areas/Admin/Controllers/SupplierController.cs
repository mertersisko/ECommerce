using Common.Core.Extentions;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Services;
using ECommerce.Presentation.Filter;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Presentation.Areas.Admin.Controllers
{
  [Area("Admin")]
  [Route("[controller]/[action]/{id?}")]

  public class SupplierController : Controller
  {
    private readonly SupplierService _supplierService;
    private readonly CityService _cityService;
    public SupplierController(SupplierService supplierService, CityService cityService)
    {
      _supplierService = supplierService;
      _cityService = cityService;
    }

    [ManagerUserAuthFilter]
    public async Task<IActionResult> List()
    {
      var result = await _supplierService.GetListAsync();

      return View(result.DataList);
    }

    [ManagerUserAuthFilter]
    public async Task<IActionResult> Add()
    {
      ViewBag.Cities = await _cityService.GetCitySelectList();
      return View();
    }


    [HttpPost]
    [ManagerUserAuthFilter]
    public async Task<IActionResult> Add(Supplier model)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelStateValidationHelper.GetModelErrorList(ModelState));

      return Ok(await _supplierService.AddAsync(model));

    }

    [ManagerUserAuthFilter]
    public async Task<IActionResult> Update(string id)
    {
      var result = await _supplierService.GetAsync(id);

      if (result.Data == null)
        return RedirectToAction(nameof(List));

      return View(result.Data);
    }

    [HttpPost]
    [ManagerUserAuthFilter]
    public async Task<IActionResult> Update(Supplier model)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelStateValidationHelper.GetModelErrorList(ModelState));

      return Ok(await _supplierService.UpdateAsync(model));

    }


    [ManagerUserAuthFilter]
    public async Task<IActionResult> ChangeStatus(string id)
    {
      await _supplierService.ChangeStatus(id);

      var managerUserList = await _supplierService.GetListAsync();

      return PartialView("SupplierPartials/_SupplierListPartial", managerUserList.DataList);
    }


    [ManagerUserAuthFilter]
    public async Task<IActionResult> Delete(string id)
    {
      await _supplierService.Delete(id);

      var managerUserList = await _supplierService.GetListAsync();

      return PartialView("SupplierPartials/_SupplierListPartial", managerUserList.DataList);
    }

    [HttpGet]
    public async Task<IActionResult> GetTownsByCityId(int id)
    {
      return Ok(await _cityService.GetTownsByCityId(id));
    }
  }
}

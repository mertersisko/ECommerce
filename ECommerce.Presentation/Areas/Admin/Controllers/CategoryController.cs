using Common.Core.Extentions;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Services;
using ECommerce.Presentation.Filter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace ECommerce.Presentation.Areas.Admin.Controllers
{
  [Area("Admin")]
  [Route("[controller]/[action]/{id?}")]
  public class CategoryController : Controller
  {
    private readonly CategoryService _categoryService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public CategoryController(CategoryService categoryService, IWebHostEnvironment webHostEnvironment)
    {
      _categoryService = categoryService;
      _webHostEnvironment = webHostEnvironment;
    }

    [ManagerUserAuthFilter]
    public async Task<IActionResult> List()
    {
      var result = await _categoryService.GetListAsync();

      return View(result.DataList);
    }

    [ManagerUserAuthFilter]
    public async Task<IActionResult> Add()
    {
      ViewBag.Categories = await _categoryService.GetCategoriesSelectList();

      return View();
    }

    [HttpPost]
    [ManagerUserAuthFilter]
    public async Task<IActionResult> Add(Category model)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelStateValidationHelper.GetModelErrorList(ModelState));

      if (model.File is { Length: > 0 })
      {
        var fileName = Guid.NewGuid() + Path.GetExtension(model.File.FileName).ToLower();

        var path = Path.Combine(_webHostEnvironment.WebRootPath, "WebContent/CategoryImages", fileName);

        await using var stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);

        await model.File.CopyToAsync(stream);

        model.CoverPhoto = fileName;
      }

      return Ok(await _categoryService.AddAsync(model));
    }
  }
}

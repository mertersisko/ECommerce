using ECommerce.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CategoryController : ControllerBase
  {
    private readonly CategoryService _categoryService;

    public CategoryController(CategoryService categoryService)
    {
      _categoryService = categoryService;
    }


    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
      return Ok(await _categoryService.GetCategoriesSelectList());
    }
  }
}

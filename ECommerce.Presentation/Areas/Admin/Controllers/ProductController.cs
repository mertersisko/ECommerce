using Common.Core.Extentions;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Services;
using ECommerce.Presentation.Filter;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Presentation.Areas.Admin.Controllers
{
  [Area("Admin")]
  [Route("[controller]/[action]/{id?}")]
  public class ProductController : Controller
  {
    private readonly ProductService _productService;
    private readonly CategoryService _categoryService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductController(ProductService productService, CategoryService categoryService, IWebHostEnvironment webHostEnvironment)
    {
      _productService = productService;
      _categoryService = categoryService;
      _webHostEnvironment = webHostEnvironment;
    }

    [ManagerUserAuthFilter]
    public async Task<IActionResult> List()
    {
      var result = await _productService.GetListAsync();

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
    public async Task<IActionResult> Add(Product model)
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelStateValidationHelper.GetModelErrorList(ModelState));

      return Ok(await _productService.AddAsync(model));

    }

    [ManagerUserAuthFilter]
    public async Task<IActionResult> Update(string id)
    {
      var result = await _productService.GetAsync(id);

      if (result.Data == null)
        return RedirectToAction(nameof(List));

      ViewBag.Categories = await _categoryService.GetCategoriesSelectList();

      return View(result.Data);
    }

    [HttpPost]
    public IActionResult ImageUpload(IFormFile upload)
    {
      if (upload.Length <= 0) return null;

      var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();

      var path = Path.Combine(_webHostEnvironment.WebRootPath, "WebContent/ProductImage", fileName);

      using (var stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
        upload.CopyTo(stream);

      var url = $"/WebContent/ProductImage/{fileName}";

      return Json(new { uploaded = true, url });
    }


    [HttpPost]
    public async Task<IActionResult> DropZoneImageUpload(int id, IFormFile file)
    {
      if (file.Length <= 0) return BadRequest();


      var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName).ToLower();

      await _productService.ProductImageAdd(new ProductImages
      {
        ProductId = id,
        ImagePath = fileName
      });

      var path = Path.Combine(_webHostEnvironment.WebRootPath, "WebContent/ProductImage", fileName);

      await using var stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
      await file.CopyToAsync(stream);



      return Ok();
    }

    public async Task<IActionResult> GetProductImageList(int id)
    {
      var productImages = await _productService.GetProductImagesByProductId(id);

      return PartialView("ProductPartials/_ProductImageListPartial", productImages.DataList);
    }

    [HttpGet]
    public async Task<IActionResult> ProductImageDelete(int id)
    {
      var productImage = await _productService.GetProductImage(id);

      var productId = productImage.Data.ProductId;

      await _productService.DeleteProductImage(productImage.Data);

      var productImages = await _productService.GetProductImagesByProductId(productId);

      return PartialView("ProductPartials/_ProductImageListPartial", productImages.DataList);
    }

  }
}

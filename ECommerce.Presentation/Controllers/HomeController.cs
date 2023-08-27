using ECommerce.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using ECommerce.Domain.Dtos;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Services;

namespace ECommerce.Presentation.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly CityService _cityService;

    public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment, CityService cityService)
    {
      _logger = logger;
      _webHostEnvironment = webHostEnvironment;
      _cityService = cityService;
    }

    public async Task<IActionResult> Index()
    {
      var jsonString = System.IO.File.ReadAllText(Path.Combine(_webHostEnvironment.WebRootPath, "il-ilce.json"));

      //using System.Text.Json;
      var cityData = JsonSerializer.Deserialize<CityTownDto>(jsonString);

      List<City> lstCity = new List<City>();

      foreach (var city in cityData.data)
      {
        City newCity = new City
        {
          Definition = city.il_adi,
          Towns = new List<Town>()
        };

        foreach (var town in city.ilceler)
        {
          newCity.Towns.Add(new Town
          {
            Definition = town.ilce_adi,
            City = newCity
          });
        }
        lstCity.Add(newCity);
      }

      var sonuc = await _cityService.BulkCityAdd(lstCity);

      return View();
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
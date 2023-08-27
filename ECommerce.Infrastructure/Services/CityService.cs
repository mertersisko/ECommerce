using Common.Core.Repository;
using ECommerce.Domain.Entities;
using ECommerce.Persistence.Context;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerce.Infrastructure.Services
{
  public class CityService
  {
    private readonly IUnitOfWork<ECommerceDbContext> _unitOfWork;
    public CityService(IUnitOfWork<ECommerceDbContext> unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public async Task<bool> BulkCityAdd(List<City> lstCities)
    {
      var dbContext = _unitOfWork.GetDbContext();

      if (dbContext.Cities.Any())
        return false;

      try
      {
        dbContext.Cities.AddRange(lstCities);
        await dbContext.SaveChangesAsync();

        return true;
      }
      catch (Exception e)
      {
        return false;
      }
    }

    public async Task<SelectList> GetCitySelectList()
    {
      var cities = await _unitOfWork.Repository<City>().GetListAsync(t => !t.Deleted && t.Active);

      return new SelectList(cities.DataList, "Id", "Definition");
    }

    public async Task<SelectList> GetTownsByCityId(int cityId)
    {
      var towns = await _unitOfWork.Repository<Town>().GetListAsync(t => !t.Deleted && t.Active && t.CityId == cityId);

      return new SelectList(towns.DataList, "Id", "Definition");
    }
  }
}

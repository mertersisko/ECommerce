using AutoMapper;
using Common.Core.Classes;
using Common.Core.Enums;
using Common.Core.Repository;
using ECommerce.Domain.Entities;
using ECommerce.Persistence.Context;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ECommerce.Infrastructure.Services
{
  public class SupplierService
  {
    private readonly IUnitOfWork<ECommerceDbContext> _unitOfWork;
    private readonly IMapper _mapper;

    public SupplierService(IUnitOfWork<ECommerceDbContext> unitOfWork, IMapper mapper)
    {
      _unitOfWork = unitOfWork;
      _mapper = mapper;
    }

    public async Task<BaseResponse<Supplier>> GetListAsync()
    {
      return await _unitOfWork.Repository<Supplier>().GetProtectedListAsync(t=> !t.Deleted);
    }

    public async Task<BaseResponse<Supplier>> GetAsync(string key)
    {
      return await _unitOfWork.Repository<Supplier>().GetByEnchKeyAsync(key);
    }

    //Altarnatif - 1
    public async Task<Supplier> GetSupplierAsync(string key)
    {
      var result = await _unitOfWork.Repository<Supplier>().GetByEnchKeyAsync(key);

      return result.Data;
    }

    //Altarnatif - 2
    public Supplier GetSupplier(string key)
    {
      var result = _unitOfWork.Repository<Supplier>().GetByEnchKey(key);

      return result.Data;
    }

    public async Task<BaseResponse<Supplier>> AddAsync(Supplier model)
    {
      var result = await _unitOfWork.Repository<Supplier>().AddAsync(model);

      if (result.ResultStatus == ResultStatus.Success)
        result.Url = "/Supplier/List";

      return result;
    }

    public async Task<BaseResponse<Supplier>> UpdateAsync(Supplier model)
    {
      var result = await _unitOfWork.Repository<Supplier>().GetByEnchKeyAsync(model.EnchKey);

      result.Data = _mapper.Map<Supplier>(model);

      var saveResult = await _unitOfWork.Repository<Supplier>().UpdateAsync(result.Data);

      if (saveResult.ResultStatus == ResultStatus.Success)
        saveResult.Url = "/Supplier/List";

      //Faklı Kullanım
      //if (saveResult.ResultStatus == ResultStatus.Success)
      //  saveResult.Url = $"/Supplier/Update/{model.EnchKey}";

      return saveResult;
    }

    public async Task<BaseResponse<Supplier>> ChangeStatus(string key)
    {
      return await _unitOfWork.Repository<Supplier>().ChangeStatusAsync(key);
    }

    public async Task<BaseResponse<Supplier>> Delete(string key)
    {
      return await _unitOfWork.Repository<Supplier>().SetDeletedAsync(key);
    }
  }
}

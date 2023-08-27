using AutoMapper;
using Common.Core.Classes;
using Common.Core.Enums;
using Common.Core.Repository;
using Common.Helper.Encrypt;
using ECommerce.Domain.Dtos;
using ECommerce.Domain.Entities;
using ECommerce.Persistence.Context;


namespace ECommerce.Infrastructure.Services;

public class ManagerUserService
{
  private readonly IUnitOfWork<ECommerceDbContext> _unitOfWork;
  private readonly IMapper _mapper;

  public ManagerUserService(IUnitOfWork<ECommerceDbContext> unitOfWork, IMapper mapper)
  {
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }


  public async Task<BaseResponse<ManagerUser>> Login(LoginDto model)
  {
    model.Password = Md5Encrypt.Encrypt(model.Password);

    var managerUserResponse = await _unitOfWork.Repository<ManagerUser>()
      .GetAsync(t => t.EMail == model.EMail && t.Password == model.Password);

    if (managerUserResponse.Data != null)
      managerUserResponse.ResultStatus = ResultStatus.Success;
    else
    {
      managerUserResponse.Message = "Kullanıcı Bulunamadı";
      managerUserResponse.ResultStatus = ResultStatus.Error;
    }

    return managerUserResponse;
  }

  public async Task<BaseResponse<ManagerUser>> AddManagerUser(ManagerUser model)
  {
    model.Password = Md5Encrypt.Encrypt(model.Password);

    var result = await _unitOfWork.Repository<ManagerUser>().AddAsync(model);

    if (result.ResultStatus == ResultStatus.Success)
      result.Url = "/ManagerUser/List";

    return result;
  }

  public async Task<BaseResponse<ManagerUser>> UpdateManagerUser(ManagerUser model)
  {
    var managerUserResponse = await _unitOfWork.Repository<ManagerUser>().GetByEnchKeyAsync(model.EnchKey);

    managerUserResponse.Data = _mapper.Map<ManagerUser>(model);

    managerUserResponse.Data.Password = Md5Encrypt.Encrypt(model.Password);

    var result = await _unitOfWork.Repository<ManagerUser>().UpdateAsync(managerUserResponse.Data);

    if (result.ResultStatus == ResultStatus.Success)
      result.Url = "/ManagerUser/List";

    return result;

  }

  public async Task<BaseResponse<ManagerUser>> GetManagerUser(string key)
  {
    var managerUserResponse = await _unitOfWork.Repository<ManagerUser>().GetByEnchKeyAsync(key);

    if (managerUserResponse.Data != null)
      managerUserResponse.Data.Password = Md5Encrypt.Decrypt(managerUserResponse.Data.Password);

    return managerUserResponse;
  }

  public async Task<BaseResponse<ManagerUser>> GetProtectedManagerUserList()
  {
    return await _unitOfWork.Repository<ManagerUser>().GetProtectedListAsync(t => !t.Deleted);
  }

  public async Task<BaseResponse<ManagerUser>> ManagerUserChangeStatus(string key)
  {
    return await _unitOfWork.Repository<ManagerUser>().ChangeStatusAsync(key);
  }

  public async Task<BaseResponse<ManagerUser>> ManagerUserDelete(string key)
  {
    return await _unitOfWork.Repository<ManagerUser>().SetDeletedAsync(key);
  }

  public bool IsMailAdressUniq(string email)
  {
    return _unitOfWork.Repository<ManagerUser>().CheckAny(t => !t.Deleted && t.EMail == email);
  }

}
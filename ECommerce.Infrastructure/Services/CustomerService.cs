using Common.Core.Classes;
using Common.Core.Extentions;
using Common.Core.Repository;
using Common.Helper.Encrypt;
using ECommerce.Application.Repositories;
using ECommerce.Domain.Entities;
using ECommerce.Persistence.Context;

namespace ECommerce.Infrastructure.Services;

public class CustomerService
{
  private readonly IUnitOfWork<ECommerceDbContext> _unitOfWork;

  public CustomerService(IUnitOfWork<ECommerceDbContext> unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }


  public async Task<BaseResponse<Customer>> Add(Customer customer)
  {
    customer.Password = Md5Encrypt.Encrypt(customer.Password);

    return await _unitOfWork.Repository<Customer>().AddAsync(customer);
  }

  public async Task<BaseResponse<Customer>> Update(Customer customer)
  {
    customer.Password = Md5Encrypt.Encrypt(customer.Password);

    return await _unitOfWork.Repository<Customer>().UpdateAsync(customer);
  }

  public async Task<BaseResponse<Customer>> GetList()
  {
    return await _unitOfWork.Repository<Customer>().GetListAsync();
  }

  public Customer Get(int id)
  {
    var baseResponse = _unitOfWork.Repository<Customer>().Get(t => t.Id == id);

    baseResponse.Data.Password = Md5Encrypt.Decrypt(baseResponse.Data.Password);

    return baseResponse.Data;
  }

  public async Task<BaseResponse<Customer>> Login(string email, string password)
  {
    var dbContext = _unitOfWork.GetDbContext();

    var customerListAsync = await dbContext.Customers.GetNonDeletedAndActiveRecordsAsync();
    var customerList = dbContext.Customers.GetNonDeletedAndActiveRecords();


    var categoryList = await dbContext.Categories.GetNonDeletedRecords();


    var customer = await _unitOfWork.Repository<Customer>()
      .GetAsync(t => t.EMail == email && t.Password == Md5Encrypt.Encrypt(password));

    return customer;
  }

}
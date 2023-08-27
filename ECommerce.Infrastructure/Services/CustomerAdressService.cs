using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Core.Classes;
using Common.Core.Repository;
using ECommerce.Domain.Entities;
using ECommerce.Persistence.Context;

namespace ECommerce.Infrastructure.Services
{
  public class CustomerAdressService
  {
    private readonly IUnitOfWork<ECommerceDbContext> _unitOfWork;

    public CustomerAdressService(IUnitOfWork<ECommerceDbContext> unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public BaseResponse<CustomerAdress> AddCustomerAdress(CustomerAdress customerAdress)
    {
      return _unitOfWork.Repository<CustomerAdress>().Add(customerAdress);
    }

    public async Task<BaseResponse<CustomerAdress>> UpdateCustomerAdressAsync(CustomerAdress customerAdress)
    {
      return await _unitOfWork.Repository<CustomerAdress>().UpdateAsync(customerAdress);
    }
    
  }
}

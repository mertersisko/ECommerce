using Common.Core.Repository;
using ECommerce.Domain.Entities;
using ECommerce.Persistence.Context;

namespace ECommerce.Application.Repositories;

public interface ICustomerRepository : IBaseRepository<Customer, ECommerceDbContext>
{
  public Task<Customer> Login();
}
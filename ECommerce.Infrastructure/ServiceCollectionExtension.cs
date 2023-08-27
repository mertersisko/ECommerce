using System.Reflection;
using Common.Core.AutoMapperModule;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Infrastructure;

public static class ServiceCollectionExtension
{
  public static IServiceCollection AddECommerceInfrastructure(this IServiceCollection services)
  {
    services.AddScoped<CustomerService>();
    services.AddScoped<ManagerUserService>();
    services.AddScoped<SupplierService>();
    services.AddScoped<CityService>();
    services.AddScoped<CategoryService>();
    services.AddScoped<ProductService>();

    services.AddAutoMapper(config =>
    {
      config.AddProfile<IgnorePropertiesProfile<ManagerUser, ManagerUser>>();
      config.AddProfile<IgnorePropertiesProfile<Supplier, Supplier>>();
      config.AddProfile<IgnorePropertiesProfile<Category, Category>>();
      config.AddProfile<IgnorePropertiesProfile<Product, Product>>();
    }, Assembly.GetExecutingAssembly());

    return services;
  }
}
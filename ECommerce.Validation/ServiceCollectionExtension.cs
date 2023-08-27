using ECommerce.Domain.Entities;
using ECommerce.Validation.EntitiesValidation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Validation
{
  public static class ServiceCollectionExtension
  {
    public static IServiceCollection AddECommerceValidation(this IServiceCollection services)
    {
      services.AddFluentValidationClientsideAdapters();
      
      services.AddTransient<IValidator<Customer>, CustomerValidator>();
      services.AddTransient<IValidator<ManagerUser>,ManagerUserValidator>();


      return services;
    }
  }
}


using ECommerce.Persistence.Context;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Persistence;

public static class ServiceCollectionExtension
{
  public static IServiceCollection AddECommercePersistence(this IServiceCollection services)
  {
    services.AddHttpContextAccessor();
    services.AddDbContext<ECommerceDbContext>();

    return services;
  }
}
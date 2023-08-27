using System.Reflection;
using AutoMapper;
using Common.Core.Repository;
using Common.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Core;

public static class ServiceCollectionExtension
{
  public static IServiceCollection AddCommonCore(this IServiceCollection services)
  {
    
    services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
    services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

    return services;
  }
}
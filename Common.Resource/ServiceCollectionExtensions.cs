using Common.Resource.Classes;
using Common.Resource.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Reflection;

namespace Common.Resource;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddCommonResource(this IServiceCollection service)
  {
    service.AddLocalization(options => options.ResourcesPath = "Resources");
    service.AddTransient<IResourceService, ResourceService>();

    service.AddMvc(options => options.EnableEndpointRouting = false)
      .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization(o =>
      {
        var type = typeof(Resources.Resource);
        var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
        var factory = service.BuildServiceProvider().GetService<IStringLocalizerFactory>();
        var localizer = factory.Create("Resource", assemblyName.Name);
        o.DataAnnotationLocalizerProvider = (t, f) => localizer;

      });

    service.Configure<RequestLocalizationOptions>(
      options =>
      {
        var supportedCultures = new List<CultureInfo>
        {
          new("tr-TR"),
          new("en-EN"),
        };

        options.DefaultRequestCulture = new RequestCulture(culture: "tr-TR", uiCulture: "tr-TR");
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;


        options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
      });


    service.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
      .AddCookie(options =>
      {
        options.CookieManager = new ChunkingCookieManager();

        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.None;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
      });

    return service;
  }
}
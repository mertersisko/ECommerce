using Common.Core;
using Common.Resource;
using ECommerce.Infrastructure;
using ECommerce.Persistence;
using ECommerce.Validation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataProtection();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddCommonResource();
builder.Services.AddCommonCore();
builder.Services.AddECommercePersistence();
builder.Services.AddECommerceInfrastructure();
builder.Services.AddECommerceValidation();
builder.Services.AddSession(opt =>
{
  opt.IdleTimeout = TimeSpan.FromMinutes(20);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
  endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

  endpoints.MapControllerRoute(
    name : "areas",
    pattern : "{area:exists}/{controller=Home}/{action=Index}/{id?}"
  );
});



app.Run();

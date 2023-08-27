using Common.Core;
using Common.Resource;
using ECommerce.Infrastructure;
using ECommerce.Persistence;
using ECommerce.Validation;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCommonResource();
builder.Services.AddCommonCore();
builder.Services.AddECommercePersistence();
builder.Services.AddECommerceInfrastructure();
builder.Services.AddECommerceValidation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

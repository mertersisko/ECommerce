using Common.Core.Classes;
using ECommerce.Domain.Entities;
using ECommerce.Persistence.Seed;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Common.Core.Extentions;

namespace ECommerce.Persistence.Context
{
  public class ECommerceDbContext : DbContext
  {
    protected IHttpContextAccessor _httpContextAccessor;

    public ECommerceDbContext(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<CustomerAdress> CustomerAdress { get; set; }
    public DbSet<ManagerUser> ManagerUsers { get; set; }
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<BasketLine> BasketLines { get; set; }
    public DbSet<Campaign> Campaigns { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Town> Towns { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderLine> OrderLines { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductImages> ProductImages { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new ManagerUserConfigration());
      base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {

        var configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        optionsBuilder.UseSqlServer(connectionString);
      }
    }

    public override int SaveChanges()
    {
      SetBaseObjectValues();
      return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
      SetBaseObjectValues();
      return base.SaveChangesAsync(cancellationToken);
    }

    private void SetBaseObjectValues()
    {
      var managerUser = _httpContextAccessor.HttpContext.Session.Get<ManagerUser>("ManagerUser");

      var userName = managerUser != null ? managerUser.NameAndSurName : "-";

      ChangeTracker.DetectChanges();

      var entries = ChangeTracker.Entries();

      foreach (var entry in entries)
      {
        if (entry.Entity is BaseEntity trackable)
        {
          var now = DateTime.Now;

          switch (entry.State)
          {
            case EntityState.Modified:
              trackable.LastModifiedDate = now;
              trackable.LastModifiedBy = userName;
              break;

            case EntityState.Added:
              trackable.LastModifiedDate = now;
              trackable.CreatedDate = now;
              trackable.CreatedBy = userName;
              trackable.LastModifiedBy = userName;
              trackable.Active = true;
              trackable.Deleted = false;
              break;
          }
        }
      }
    }
  }
}

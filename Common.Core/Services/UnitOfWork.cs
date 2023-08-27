using Common.Core.Classes;
using Common.Core.Repository;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
namespace Common.Core.Services;

public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
{
  private readonly TContext _context;
  private bool _disposed;
  private Dictionary<string, object> _repositories;
  private readonly IDataProtectionProvider _dataProtectionProvider;

  public UnitOfWork(TContext context, IDataProtectionProvider dataProtectionProvider)
  {
    _context = context;
    _dataProtectionProvider = dataProtectionProvider;
  }

  public IBaseRepository<T, TContext> Repository<T>() where T : BaseEntity, new()
  {
    _repositories ??= new Dictionary<string, object>();

    var type = typeof(T).Name;

    if (!_repositories.ContainsKey(type))
    {
      var repositoryType = typeof(BaseRepository<,>);
      var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T), typeof(TContext)), _context, _dataProtectionProvider);
      _repositories.Add(type, repositoryInstance);
    }
    return (IBaseRepository<T, TContext>)_repositories[type];
  }

  public TContext GetDbContext()
  {
    return _context;
  }

  public void Dispose()
  {
    Dispose(true);
    GC.SuppressFinalize(this);
  }

  private void Dispose(bool disposing)
  {
    if (!_disposed)
    {
      if (disposing)
      {
        _context.Dispose();
      }
    }
    _disposed = true;
  }
}
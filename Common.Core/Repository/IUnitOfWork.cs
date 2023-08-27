using Common.Core.Classes;
using Microsoft.EntityFrameworkCore;

namespace Common.Core.Repository;

public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
{
  IBaseRepository<T, TContext> Repository<T>() where T : BaseEntity, new();
  TContext GetDbContext();
}
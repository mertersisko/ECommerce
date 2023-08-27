using Common.Core.Classes;
using Microsoft.EntityFrameworkCore;

namespace Common.Core.Extentions
{
  public static class EfLinqExtentions
  {
    public static async Task<ICollection<T>> GetNonDeletedAndActiveRecordsAsync<T>(this IQueryable<T> query) where T : BaseEntity
    {
      return await query.Where(t => t.Active && !t.Deleted).ToListAsync();
    }

    public static ICollection<T> GetNonDeletedAndActiveRecords<T>(this IQueryable<T> query) where T : BaseEntity
    {
      return query.Where(t => t.Active && !t.Deleted).ToList();
    }

    public static async Task<ICollection<T>> GetNonDeletedRecords<T>(this IQueryable<T> query) where T : BaseEntity
    {
      return await query.Where(t => !t.Deleted).ToListAsync();
    }
  }
}

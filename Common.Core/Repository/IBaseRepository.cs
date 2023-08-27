using Common.Core.Classes;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Common.Core.Repository
{
  public interface IBaseRepository<T, TContext>
    where T : BaseEntity
    where TContext : DbContext
  {
    public BaseResponse<T> Add(T entity);
    public Task<BaseResponse<T>> AddAsync(T entity);
    public BaseResponse<T> Update(T entity);
    public Task<BaseResponse<T>> UpdateAsync(T entity);
    public BaseResponse<T> Delete(T entity);
    public Task<BaseResponse<T>> DeleteAsync(T entity);
    public BaseResponse<T> Get(Expression<Func<T, bool>> filter);
    public Task<BaseResponse<T>> GetAsync(Expression<Func<T, bool>> filter);
    public Task<BaseResponse<T>> GetListAsync(Expression<Func<T, bool>> filter = null);
    public BaseResponse<T> GetProtectedList(Expression<Func<T, bool>> filter = null);
    public Task<BaseResponse<T>> GetProtectedListAsync(Expression<Func<T, bool>> filter = null);
    public BaseResponse<T> GetByEnchKey(string key);
    public Task<BaseResponse<T>> GetByEnchKeyAsync(string key);
    public BaseResponse<T> ChangeStatus(string key);
    public Task<BaseResponse<T>> ChangeStatusAsync(string key);
    public BaseResponse<T> SetDeleted(string key);
    public Task<BaseResponse<T>> SetDeletedAsync(string key);
    public bool CheckAny(Expression<Func<T, bool>> filter = null);
    public Task<bool> CheckAnyAsync(Expression<Func<T, bool>> filter = null);

    public BaseResponse<T> GetListWithInclude(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includeProperties);
    public Task<BaseResponse<T>> GetListWithIncludeAsync(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includeProperties);

    public BaseResponse<T> GetProtectedListWithInclude(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includeProperties);
    public Task<BaseResponse<T>> GetProtectedListWithIncludeAsync(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includeProperties);

    public BaseResponse<T> GetByEnchKeyWithInclude(string key , params Expression<Func<T, object>>[] includeProperties);
    public Task<BaseResponse<T>> GetByEnchKeyWithIncludeAsync(string key, params Expression<Func<T, object>>[] includeProperties);
  }
}

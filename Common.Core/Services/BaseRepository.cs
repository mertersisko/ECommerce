using Common.Core.Classes;
using Common.Core.Enums;
using Common.Core.Repository;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Common.Core.Services;

public class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity, TContext>
  where TEntity : BaseEntity
  where TContext : DbContext
{
  private readonly TContext _dbContext;
  private readonly DbSet<TEntity> _dbSet;
  private readonly IDataProtector _dataProtector;

  public BaseRepository(TContext dbContext, IDataProtectionProvider dataProtectorProvider)
  {
    _dbContext = dbContext;
    _dbSet = _dbContext.Set<TEntity>();
    _dataProtector = dataProtectorProvider.CreateProtector("CommonProtector");
  }

  public BaseResponse<TEntity> Add(TEntity entity)
  {
    using var transaction = _dbContext.Database.BeginTransaction();

    try
    {
      _dbContext.Add(entity);

      var result = _dbContext.SaveChanges();

      if (result > 0)
      {
        transaction.Commit();

        return new BaseResponse<TEntity>
        {
          Data = entity,
          ResultStatus = ResultStatus.Success,
          Message = "Kayıt Ekleme İşlemi Başarılı",
          Title = "Başarı"
        };
      }

      transaction.Rollback();

      return new BaseResponse<TEntity>
      {
        Data = entity,
        ResultStatus = ResultStatus.Error,
        Message = "Kayıt Ekleme İşlemi Sırasında Hata Oluştu",
        Title = "Hata"
      };
    }
    catch (Exception e)
    {
      transaction.Rollback();

      return new BaseResponse<TEntity>
      {
        Data = entity,
        ResultStatus = ResultStatus.Error,
        Message = "Kayıt Ekleme İşlemi Sırasında Hata Oluştu",
        Title = "Hata"
      };
    }
  }

  public async Task<BaseResponse<TEntity>> AddAsync(TEntity entity)
  {
    await using var transaction = await _dbContext.Database.BeginTransactionAsync();

    try
    {
      _dbContext.Add(entity);

      var result = await _dbContext.SaveChangesAsync();

      if (result > 0)
      {
        await transaction.CommitAsync();

        return new BaseResponse<TEntity>
        {
          Data = entity,
          ResultStatus = ResultStatus.Success,
          Message = "Kayıt Ekleme İşlemi Başarılı",
          Title = "Başarı"
        };
      }

      await transaction.RollbackAsync();

      return new BaseResponse<TEntity>
      {
        Data = entity,
        ResultStatus = ResultStatus.Error,
        Message = "Kayıt Ekleme İşlemi Sırasında Hata Oluştu",
        Title = "Hata"
      };
    }
    catch (Exception e)
    {
      await transaction.RollbackAsync();

      return new BaseResponse<TEntity>
      {
        Data = entity,
        ResultStatus = ResultStatus.Error,
        Message = "Kayıt Ekleme İşlemi Sırasında Hata Oluştu",
        Title = "Hata"
      };
    }
  }

  public BaseResponse<TEntity> Update(TEntity entity)
  {
    using var transaction = _dbContext.Database.BeginTransaction();

    try
    {
      _dbContext.Update(entity);

      var result = _dbContext.SaveChanges();

      if (result > 0)
      {
        transaction.Commit();

        return new BaseResponse<TEntity>
        {
          Data = entity,
          ResultStatus = ResultStatus.Success,
          Message = "Kayıt Güncelleme İşlemi Başarılı",
          Title = "Başarı"

        };
      }
      transaction.Rollback();

      return new BaseResponse<TEntity>
      {
        Data = entity,
        ResultStatus = ResultStatus.Error,
        Message = "Kayıt Güncelleme İşlemi Sırasında Hata Oluştu",
        Title = "Hata"
      };
    }
    catch (Exception e)
    {
      transaction.RollbackAsync();

      return new BaseResponse<TEntity>
      {
        Data = entity,
        ResultStatus = ResultStatus.Error,
        Message = "Kayıt Güncelleme İşlemi Sırasında Hata Oluştu",
        Title = "Hata"
      };
    }
  }

  public async Task<BaseResponse<TEntity>> UpdateAsync(TEntity entity)
  {
    await using var transaction = await _dbContext.Database.BeginTransactionAsync();

    try
    {
      _dbContext.Update(entity);

      var result = await _dbContext.SaveChangesAsync();

      if (result > 0)
      {
        await transaction.CommitAsync();

        return new BaseResponse<TEntity>
        {
          Data = entity,
          ResultStatus = ResultStatus.Success,
          Message = "Kayıt Güncelleme İşlemi Başarılı",
          Title = "Başarı"
        };
      }

      await transaction.RollbackAsync();

      return new BaseResponse<TEntity>
      {
        Data = entity,
        ResultStatus = ResultStatus.Error,
        Message = "Kayıt Güncelleme İşlemi Sırasında Hata Oluştu",
        Title = "Hata"
      };
    }
    catch (Exception e)
    {
      await transaction.RollbackAsync();

      return new BaseResponse<TEntity>
      {
        Data = entity,
        ResultStatus = ResultStatus.Error,
        Message = "Kayıt Güncelleme İşlemi Sırasında Hata Oluştu",
        Title = "Hata"
      };
    }
  }

  public BaseResponse<TEntity> Delete(TEntity entity)
  {
    using var transaction = _dbContext.Database.BeginTransaction();

    try
    {
      _dbContext.Remove(entity);

      var result = _dbContext.SaveChanges();

      if (result > 0)
      {
        transaction.Commit();

        return new BaseResponse<TEntity>
        {
          Data = null,
          ResultStatus = ResultStatus.Success,
          Message = "Kayıt Silme İşlemi Başarılı",
          Title = "Başarı"
        };
      }
      transaction.Rollback();

      return new BaseResponse<TEntity>
      {
        Data = null,
        ResultStatus = ResultStatus.Error,
        Message = "Kayıt Silme İşlemi Sırasında Hata Oluştu",
        Title = "Hata"
      };
    }
    catch (Exception e)
    {
      transaction.RollbackAsync();

      return new BaseResponse<TEntity>
      {
        Data = null,
        ResultStatus = ResultStatus.Success,
        Message = "Kayıt Silme İşlemi Sırasında Hata Oluştu",
        Title = "Hata"
      };
    }
  }

  public async Task<BaseResponse<TEntity>> DeleteAsync(TEntity entity)
  {
    await using var transaction = await _dbContext.Database.BeginTransactionAsync();

    try
    {
      _dbContext.Remove(entity);

      var result = await _dbContext.SaveChangesAsync();

      if (result > 0)
      {
        await transaction.CommitAsync();

        return new BaseResponse<TEntity>
        {
          Data = null,
          ResultStatus = ResultStatus.Success,
          Message = "Kayıt Silme İşlemi Başarılı",
          Title = "Başarı"
        };
      }
      await transaction.RollbackAsync();

      return new BaseResponse<TEntity>
      {
        Data = null,
        ResultStatus = ResultStatus.Error,
        Message = "Kayıt Silme İşlemi Sırasında Hata Oluştu",
        Title = "Hata"
      };
    }
    catch (Exception e)
    {
      await transaction.RollbackAsync();

      return new BaseResponse<TEntity>
      {
        Data = null,
        ResultStatus = ResultStatus.Success,
        Message = "Kayıt Silme İşlemi Sırasında Hata Oluştu",
        Title = "Hata"
      };
    }
  }

  public BaseResponse<TEntity> Get(Expression<Func<TEntity, bool>> filter)
  {
    return new BaseResponse<TEntity>
    {
      Data = _dbSet.AsNoTracking().FirstOrDefault(filter)
    };
  }

  public async Task<BaseResponse<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter)
  {
    var result = await _dbSet.AsNoTracking().FirstOrDefaultAsync(filter);

    return new BaseResponse<TEntity>
    {
      Data = result
    };
  }

  public BaseResponse<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
  {
    return new BaseResponse<TEntity>
    {
      DataList = filter == null
        ? _dbSet.AsNoTracking().ToList()
        : _dbSet.AsNoTracking().Where(filter).ToList()
    };
  }

  public async Task<BaseResponse<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null)
  {
    var dataList = filter == null
      ? await _dbSet.AsNoTracking().ToListAsync()
      : await _dbSet.AsNoTracking().Where(filter).ToListAsync();

    return new BaseResponse<TEntity>
    {
      DataList = dataList
    };
  }

  public BaseResponse<TEntity> GetProtectedList(Expression<Func<TEntity, bool>> filter = null)
  {
    var dataList = filter == null
      ? _dbSet.AsNoTracking().ToList()
      : _dbSet.AsNoTracking().Where(filter).ToList();

    dataList.ForEach(x =>
    {
      x.EnchKey = _dataProtector.Protect(x.Id.ToString());
    });

    return new BaseResponse<TEntity>
    {
      DataList = dataList
    };
  }

  public async Task<BaseResponse<TEntity>> GetProtectedListAsync(Expression<Func<TEntity, bool>> filter = null)
  {
    var dataList = filter == null
      ? await _dbSet.ToListAsync()
      : await _dbSet.Where(filter).ToListAsync();

    dataList.ForEach(x =>
    {
      x.EnchKey = _dataProtector.Protect(x.Id.ToString());
    });

    return new BaseResponse<TEntity>
    {
      DataList = dataList
    };
  }

  public BaseResponse<TEntity> GetByEnchKey(string key)
  {
    var currentObject = _dbSet.AsNoTracking().FirstOrDefault(
      t => t.Id == Convert.ToInt32(_dataProtector.Unprotect(key)));

    if (currentObject != null)
      currentObject.EnchKey = key;

    return new BaseResponse<TEntity>
    {
      Data = currentObject
    };
  }

  public async Task<BaseResponse<TEntity>> GetByEnchKeyAsync(string key)
  {
    var currentObject = await _dbSet.AsNoTracking().FirstOrDefaultAsync(
      t => t.Id == Convert.ToInt32(_dataProtector.Unprotect(key)));

    if (currentObject != null)
      currentObject.EnchKey = key;

    return new BaseResponse<TEntity>
    {
      Data = currentObject
    };
  }

  public BaseResponse<TEntity> ChangeStatus(string key)
  {
    var result = GetByEnchKey(key);

    if (result.Data != null)
    {
      result.Data.Active = !result.Data.Active;
      return Update(result.Data);
    }

    return new BaseResponse<TEntity>
    {
      Data = null,
      ResultStatus = ResultStatus.Error
    };
  }

  public async Task<BaseResponse<TEntity>> ChangeStatusAsync(string key)
  {
    var result = await GetByEnchKeyAsync(key);

    if (result.Data != null)
    {
      result.Data.Active = !result.Data.Active;
      return await UpdateAsync(result.Data);
    }

    return new BaseResponse<TEntity>
    {
      Data = null,
      ResultStatus = ResultStatus.Error
    };
  }

  public BaseResponse<TEntity> SetDeleted(string key)
  {
    var result = GetByEnchKey(key);

    if (result.Data != null)
    {
      result.Data.Deleted = true;
      result.Data.Active = false;
      return Update(result.Data);
    }

    return new BaseResponse<TEntity>
    {
      Data = null,
      ResultStatus = ResultStatus.Error
    };
  }

  public async Task<BaseResponse<TEntity>> SetDeletedAsync(string key)
  {
    var result = await GetByEnchKeyAsync(key);

    if (result.Data != null)
    {
      result.Data.Deleted = true;
      result.Data.Active = false;
      return await UpdateAsync(result.Data);
    }

    return new BaseResponse<TEntity>
    {
      Data = null,
      ResultStatus = ResultStatus.Error
    };
  }

  public bool CheckAny(Expression<Func<TEntity, bool>> filter = null)
  {
    return filter == null ? _dbSet.AsNoTracking().Any() : _dbSet.AsNoTracking().Any(filter);
  }

  public async Task<bool> CheckAnyAsync(Expression<Func<TEntity, bool>> filter = null)
  {
    return filter == null ? await _dbSet.AsNoTracking().AnyAsync() : await _dbSet.AsNoTracking().AnyAsync(filter);
  }

  public BaseResponse<TEntity> GetListWithInclude(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includeProperties)
  {
    var query = _dbSet.AsNoTracking();

    if (includeProperties != null)
      query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));;

    var dataList = filter == null
      ? query.ToList()
      : query.Where(filter).ToList();

    return new BaseResponse<TEntity>
    {
      DataList = dataList
    };
  }

  public async Task<BaseResponse<TEntity>> GetListWithIncludeAsync(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includeProperties)
  {
    var query = _dbSet.AsNoTracking();

    if (includeProperties != null)
      query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

    var dataList = filter == null
      ? await query.ToListAsync()
      : await query.Where(filter).ToListAsync();


    return new BaseResponse<TEntity>
    {
      DataList = dataList
    };
  }

  public BaseResponse<TEntity> GetProtectedListWithInclude(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includeProperties)
  {
    var query = _dbSet.AsNoTracking();

    if (includeProperties != null)
      query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));;

    var dataList = filter == null
      ? query.ToList()
      : query.Where(filter).ToList();

    dataList.ForEach(x =>
    {
      x.EnchKey = _dataProtector.Protect(x.Id.ToString());
    });

    return new BaseResponse<TEntity>
    {
      DataList = dataList
    };

  }

  public async Task<BaseResponse<TEntity>> GetProtectedListWithIncludeAsync(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includeProperties)
  {
    var query = _dbSet.AsNoTracking();

    if (includeProperties != null)
      query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

    var dataList = filter == null
      ? await query.ToListAsync()
      : await query.Where(filter).ToListAsync();

    dataList.ForEach(x =>
    {
      x.EnchKey = _dataProtector.Protect(x.Id.ToString());
    });

    return new BaseResponse<TEntity>
    {
      DataList = dataList
    };
  }


  public BaseResponse<TEntity> GetByEnchKeyWithInclude(string key, params Expression<Func<TEntity, object>>[] includeProperties)
  {
    var query = _dbSet.AsNoTracking();

    if (includeProperties != null)
      query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

    var currentObject = query.FirstOrDefault(
      t => t.Id == Convert.ToInt32(_dataProtector.Unprotect(key)));

    if (currentObject != null)
      currentObject.EnchKey = key;

    return new BaseResponse<TEntity>
    {
      Data = currentObject
    };
  }

  public async Task<BaseResponse<TEntity>> GetByEnchKeyWithIncludeAsync(string key, params Expression<Func<TEntity, object>>[] includeProperties) 
  {
    var query = _dbSet.AsNoTracking();

    if (includeProperties != null)
      query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

    var currentObject = await query.FirstOrDefaultAsync(
      t => t.Id == Convert.ToInt32(_dataProtector.Unprotect(key)));

    if (currentObject != null)
      currentObject.EnchKey = key;

    return new BaseResponse<TEntity>
    {
      Data = currentObject
    };
  }
}
using EduSmart.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using TutorCenter.Domain.Common.Models;
using TutorCenter.Infrastructure.Entity_Framework_Core;

namespace TutorCenter.Infrastructure.Persistence.Repository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity<int>
{
    protected readonly AppDbContext AppDbContext;

    public Repository(AppDbContext cEdDbAppDbContext)
    {
        AppDbContext = cEdDbAppDbContext;
    }

    public void Delete(TEntity entity)
    {
        try
        {
            AppDbContext.Set<TEntity>().Remove(entity);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Auto save changes but it is a deprecated method
    /// Remember to use non tracking record
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<bool> DeleteById(int id)
    {
        try
        {
            var deleteRecord = await AppDbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (deleteRecord == null) return false;
            AppDbContext.Set<TEntity>().Remove(deleteRecord);
            await AppDbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task SaveAll()
    {
        await AppDbContext.SaveChangesAsync();
    }

    public async Task<TEntity?> ExistenceCheck(int id)
    {
        try
        {
            return await AppDbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void Dispose()
    {
        AppDbContext.Dispose();
    }

    public virtual async Task<List<TEntity>> GetAllList()
    {
        try
        {
            return await AppDbContext.Set<TEntity>().ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public IQueryable<TEntity> GetAll()
    {
        try
        {
            return AppDbContext.Set<TEntity>().AsQueryable<TEntity>();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public virtual async Task<TEntity?> GetById(int id)
    {
        try
        {
            return await AppDbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<TEntity> Insert(TEntity entity)
    {
        try
        {
            var createdEntity = await AppDbContext.Set<TEntity>().AddAsync(entity);
            return createdEntity.Entity;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Deprecated method
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public TEntity Update(TEntity entity)
    {
        try
        {
            var updateEntity = AppDbContext.Set<TEntity>().Update(entity);
            AppDbContext.SaveChanges();
            return updateEntity.Entity;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
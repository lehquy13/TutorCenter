using Microsoft.EntityFrameworkCore;
using TutorCenter.Domain.Common.Models;
using TutorCenter.Domain.Repository;
using TutorCenter.Infrastructure.Entity_Framework_Core;

namespace TutorCenter.Infrastructure.Persistence.Repository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity<int>
{
    protected readonly AppDbContext Context;

    public Repository(AppDbContext appDbContext)
    {
        Context = appDbContext;
    }

    public void Delete(TEntity entity)
    {
        try
        {
            Context.Set<TEntity>().Remove(entity);
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
            var deleteRecord = await Context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (deleteRecord == null) return false;
            Context.Set<TEntity>().Remove(deleteRecord);
            await Context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task SaveAll()
    {
        await Context.SaveChangesAsync();
    }

    public async Task<TEntity?> ExistenceCheck(int id)
    {
        try
        {
            return await Context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void Dispose()
    {
        Context.Dispose();
    }

    public virtual async Task<List<TEntity>> GetAllList()
    {
        try
        {
            return await Context.Set<TEntity>().ToListAsync();
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
            return Context.Set<TEntity>().AsQueryable<TEntity>();
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
            return await Context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
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
            var createdEntity = await Context.Set<TEntity>().AddAsync(entity);
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
            var updateEntity = Context.Set<TEntity>().Update(entity);
            Context.SaveChanges();
            return updateEntity.Entity;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
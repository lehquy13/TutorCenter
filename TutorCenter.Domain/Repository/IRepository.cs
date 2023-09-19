﻿using TutorCenter.Domain.Common.Models;

namespace EduSmart.Domain.Repository;

public interface IRepository<TEntity> : IDisposable where TEntity : Entity<int>
{
    //Queries
    /// <summary>
    /// Get all the record of tables into a list of object
    /// </summary>
    Task<List<TEntity>> GetAllList();

    /// <summary>
    /// Get all the record of tables and able to query with linq due to the iqueryable<> return
    /// </summary>
    IQueryable<TEntity> GetAll();

    Task<TEntity?> GetById(int id);

    //Insert
    Task<TEntity> Insert(TEntity entity);

    //Update
    TEntity? Update(TEntity entity);

    //Remove
    void Delete(TEntity entity);

    Task<bool> DeleteById(int id);
    Task SaveAll();    
    Task<TEntity?> ExistenceCheck(int id);

}
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MindTrail.ApplicationContracts.RequestModels;
using MindTrail.EfCore.Context;
using MindTrail.EfCore.Interfaces.Entities;

namespace MindTrail.EfCore.QueryServices.Base;

/// <summary>
/// TODO
/// </summary>
/// <param name="dbContext">Application database context.</param>
public abstract class BaseQueryService(AppDbContext dbContext)
{
    /// <summary>
    /// Provides access to database entities and saving changes to the database.
    /// </summary>
    protected readonly AppDbContext DbContext = dbContext;

    protected static async Task<(long Total, IQueryable<TEntity> Query)> ApplyPaging<TEntity>(
        IQueryable<TEntity> query,
        PaginationModel paginationModel,
        int maxPageSize = 100)
        where TEntity : class, IPersistentEntity
    {
        if (paginationModel.PageNumber == 0)
        {
            throw new InvalidOperationException("The page number must be greater than zero.");
        }

        if (paginationModel.PageSize == 0)
        {
            throw new InvalidOperationException("The page size must be greater than zero.");
        }

        var skip = paginationModel.PageNumber == 1 ? 0 : paginationModel.PageNumber * paginationModel.PageSize;
        var take = Math.Min(paginationModel.PageSize, maxPageSize);
        var total = await query.CountAsync();

        return (total, query.Skip(skip).Take(take));
    }

    /// <summary>
    /// Returns a list of all entities from database.
    /// </summary>
    /// <typeparam name="TEntity">Type of persistent entity.</typeparam>
    /// <returns>List of all entities.</returns>
    protected IQueryable<TEntity> GetEntities<TEntity>()
        where TEntity : class, IPersistentEntity
    {
        return DbContext.Set<TEntity>();
    }
}
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MindTrail.ApplicationContracts.Models;
using MindTrail.EfCore.Context;
using MindTrail.EfCore.Interfaces.Entities;

namespace MindTrail.EfCore.Handlers.Base;

/// <summary>
/// Base class for EF Core-based query handlers.
/// </summary>
/// <param name="dbContext">Application database context.</param>
public abstract class BaseQueryHandler(AppDbContext dbContext)
{
    /// <summary>
    /// Applies pagination to the specified query
    /// and returns the total number of matching entities along with the query restricted to the requested page.
    /// </summary>
    /// <typeparam name="TEntity">Type of persistent entity.</typeparam>
    /// <param name="query">The query to paginate.</param>
    /// <param name="paginationModel">The pagination parameters.</param>
    /// <param name="maxPageSize">The maximum allowed page size, regardless of <paramref name="paginationModel"/>.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>
    /// The total number of entities matching <paramref name="query"/> before paging,
    /// and the query restricted to the requested page.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// <see cref="PaginationModel.PageNumber"/> or <see cref="PaginationModel.PageSize"/> is zero.
    /// </exception>
    protected static async Task<(long Total, IQueryable<TEntity> Query)> ApplyPaging<TEntity>(
        IQueryable<TEntity> query,
        PaginationModel paginationModel,
        int maxPageSize = 100,
        CancellationToken cancellationToken = default)
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

        var total = await query.CountAsync(cancellationToken);

        return (total, query.Skip(skip).Take(take));
    }

    /// <summary>
    /// Returns a queryable collection of all entities from the database.
    /// </summary>
    /// <typeparam name="TEntity">Type of persistent entity.</typeparam>
    protected IQueryable<TEntity> GetEntities<TEntity>()
        where TEntity : class, IPersistentEntity
    {
        return dbContext.Set<TEntity>();
    }
}
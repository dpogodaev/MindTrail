using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MindTrail.DomainEntities.Entities;
using MindTrail.EfCore.Context;
using MindTrail.EfCore.Interfaces.Entities;

namespace MindTrail.EfCore.Repositories.Base;

/// <summary>
/// Database repository with implementation of CRUD operations.
/// </summary>
public abstract class BaseRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseRepository"/> class with database context provided.
    /// </summary>
    /// <param name="dbContext">Application database context.</param>
    protected BaseRepository(AppDbContext dbContext)
    {
        DbContext = dbContext;
    }

    /// <summary>
    /// Provides access to database entities and saving changes to the database.
    /// </summary>
    protected readonly AppDbContext DbContext;

    #region CRUD operations

    /// <summary>
    /// Returns a paged result from the specified query with the specified pagination parameters.
    /// </summary>
    /// <param name="query">The query to paginate.</param>
    /// <param name="pageNumber">The current page number (starting from 1).</param>
    /// <param name="pageSize">The size of each page (number of items per page).</param>
    /// <typeparam name="TEntity">The type of the entities in the query.</typeparam>
    /// <returns>A <see cref="PagedResult{TEntity}"/> containing the paged items and information about pagination.</returns>
    protected static async Task<PagedResult<TEntity>> GetPagedResult<TEntity>(
        IQueryable<TEntity> query, int pageNumber, int pageSize) where TEntity : class, IPersistentEntity
    {
        var totalCount = await query.CountAsync();
        var skip = (pageNumber - 1) * pageSize;

        return new PagedResult<TEntity>
        {
            Items = await query.Skip(skip).Take(pageSize).ToListAsync(),
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }

    /// <summary>
    /// Returns a list of all entities from database.
    /// </summary>
    /// <typeparam name="TEntity">Type of persistent entity.</typeparam>
    /// <returns>List of all entities.</returns>
    protected DbSet<TEntity> GetEntities<TEntity>() where TEntity : class, IPersistentEntity
    {
        return DbContext.Set<TEntity>();
    }

    /// <summary>
    /// Adds a new entity to the database context.
    /// </summary>
    /// <param name="entity">Persistent entity.</param>
    /// <typeparam name="TEntity">Type of persistent entity.</typeparam>
    /// <returns>Created entity.</returns>
    protected async Task<TEntity> CreateEntityAsync<TEntity>(TEntity entity) where TEntity : IPersistentEntity
    {
        SetAuditPropertiesToCreateEntity(entity);

        var createdEntity = (TEntity)(await DbContext.AddAsync(entity)).Entity;

        await SaveChangesIfAutoSaveEnabledAsync();

        return createdEntity;
    }

    /// <summary>
    /// Updates an entity in the database context.
    /// </summary>
    /// <param name="entity">Persistent entity.</param>
    /// <typeparam name="TEntity">Type of persistent entity.</typeparam>
    /// <remarks>Supports tracked and untracked entities.</remarks>
    protected async Task UpdateEntity<TEntity>(TEntity entity) where TEntity : IPersistentEntity
    {
        if (DbContext.Entry(entity).State == EntityState.Detached)
        {
            DbContext.Attach(entity);
            DbContext.Update(entity);
        }

        SetAuditPropertiesToUpdateEntity(entity);

        await SaveChangesIfAutoSaveEnabledAsync();
    }

    /// <summary>
    /// Removes a tracked entity from the database context.
    /// </summary>
    /// <param name="entity">Persistent tracked entity.</param>
    /// <typeparam name="TEntity">Type of persistent entity.</typeparam>
    protected async Task DeleteEntity<TEntity>(TEntity entity) where TEntity : IPersistentEntity
    {
        SetAuditPropertiesToDeleteEntity(entity);

        if (entity is not ISoftDelete)
        {
            DbContext.Remove(entity);
        }

        await SaveChangesIfAutoSaveEnabledAsync();
    }

    #endregion

    #region Setting audit properties

    /// <summary>
    /// Prepares a persistent entity before adding it to the database.
    /// </summary>
    /// <remarks>
    /// If necessary, sets the <see cref="IHasCreationTime.CreationTime"/> and <see cref="IHasTenantId.TenantId"/>.
    /// </remarks>
    /// <param name="entity">Persistent entity from the database.</param>
    /// <exception cref="ArgumentNullException">Thrown when persistent entity is not specified.</exception>
    protected static void SetAuditPropertiesToCreateEntity(IPersistentEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        SetCreationTime(entity);
    }

    /// <summary>
    /// Prepares a persistent entity before updating it in the database.
    /// </summary>
    /// <remarks>
    /// If necessary, sets the <see cref="IHasModificationTime.LastModificationTime"/>.
    /// It also sets ignoring changes in the values of all audit properties.
    /// </remarks>
    /// <param name="entity">Persistent entity from the database.</param>
    /// <exception cref="ArgumentNullException">Thrown when persistent entity is not specified.</exception>
    protected void SetAuditPropertiesToUpdateEntity(IPersistentEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        IgnoreChangesOfAuditPropertyValues(entity);

        SetModificationTime(entity);
    }

    /// <summary>
    /// Prepares a persistent entity before deleting it from the database.
    /// </summary>
    /// <remarks>
    /// If necessary, sets the <see cref="IHasDeletionTime.DeletionTime"/>.
    /// </remarks>
    /// <param name="entity">Persistent entity from the database.</param>
    /// <exception cref="ArgumentNullException">Thrown when persistent entity is not specified.</exception>
    protected void SetAuditPropertiesToDeleteEntity(IPersistentEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        SetDeletionTime(entity);
    }

    #endregion

    #region Private methods

    private async Task SaveChangesIfAutoSaveEnabledAsync()
    {
        if (DbContext.IsAutoSaveEnabled)
        {
            await DbContext.SaveChangesAsync();
        }
    }

    private void IgnoreChangesOfAuditPropertyValues(IPersistentEntity entity)
    {
        IgnoreCreationTimeChanges(entity);
        IgnoreModificationTimeChanges(entity);
        IgnoreDeletionTimeChanges(entity);
    }

    private void IgnoreCreationTimeChanges(IPersistentEntity entity)
    {
        if (entity is IHasCreationTime e)
        {
            DbContext.Entry(e).Property(x => x.CreationTime).IsModified = false;
        }
    }

    private void IgnoreModificationTimeChanges(IPersistentEntity entity)
    {
        if (entity is IHasModificationTime e)
        {
            DbContext.Entry(e).Property(x => x.LastModificationTime).IsModified = false;
        }
    }

    private void IgnoreDeletionTimeChanges(IPersistentEntity entity)
    {
        if (entity is IHasDeletionTime e)
        {
            DbContext.Entry(e).Property(x => x.DeletionTime).IsModified = false;
        }
    }

    private static void SetCreationTime(IPersistentEntity entity)
    {
        if (entity is IHasCreationTime e)
        {
            e.CreationTime = DateTime.UtcNow;
        }
    }

    private void SetModificationTime(IPersistentEntity entity)
    {
        if (entity is not IHasModificationTime e) return;

        if (IsModified(entity))
        {
            e.LastModificationTime = DateTime.UtcNow;
        }
    }

    private static void SetDeletionTime(IPersistentEntity entity)
    {
        if (entity is not IHasDeletionTime e) return;

        e.DeletionTime = DateTime.UtcNow;
        e.IsDeleted = true;
    }

    private bool IsModified(IPersistentEntity entity)
    {
        return DbContext.Entry(entity).State == EntityState.Modified;
    }

    #endregion
}
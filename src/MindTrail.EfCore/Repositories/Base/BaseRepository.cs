using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MindTrail.EfCore.Context;
using MindTrail.EfCore.Interfaces.Entities;

namespace MindTrail.EfCore.Repositories.Base;

/// <summary>
/// Database repository with implementation of CRUD operations.
/// </summary>
/// <param name="dbContext">Application database context.</param>
public abstract class BaseRepository(AppDbContext dbContext)
{
    /// <summary>
    /// Provides access to database entities and saving changes to the database.
    /// </summary>
    protected readonly AppDbContext DbContext = dbContext;

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

    protected static (string? PropertyName, bool IsDescending) GetSortingOptions(string? sorting)
    {
        if (string.IsNullOrWhiteSpace(sorting))
        {
            return (null, false);
        }

        var normalizedParts = sorting
            .Trim()
            .ToUpperInvariant()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (normalizedParts.Length is < 1 or > 2)
        {
            return (null, false);
        }

        var propName = normalizedParts[0];
        var isDescending = normalizedParts is [_, "DESC"];

        return (propName, isDescending);
    }

    protected static IQueryable<TEntity> ApplyPaging<TEntity>(
        IQueryable<TEntity> query,
        uint pageNumber = 1,
        uint pageSize = 10,
        uint maxPageSize = 100)
        where TEntity : class, IPersistentEntity
    {
        if (pageNumber == 0)
        {
            throw new InvalidOperationException("The page number must be greater than zero");
        }

        if (pageSize == 0)
        {
            throw new InvalidOperationException("The page size must be greater than zero");
        }

        var skip = pageNumber == 1 ? 0 : pageNumber * pageSize;
        var take = Math.Min(pageSize, maxPageSize);

        return query.Skip((int)skip).Take((int)take);
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

    /// <summary>
    /// Adds a new entity to the database context.
    /// </summary>
    /// <param name="entity">Persistent entity.</param>
    /// <typeparam name="TEntity">Type of persistent entity.</typeparam>
    /// <returns>Created entity.</returns>
    protected async Task<TEntity> CreateEntityAsync<TEntity>(TEntity entity)
        where TEntity : IPersistentEntity
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
    protected async Task UpdateEntity<TEntity>(TEntity entity)
        where TEntity : IPersistentEntity
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
    protected async Task DeleteEntity<TEntity>(TEntity entity)
        where TEntity : IPersistentEntity
    {
        SetAuditPropertiesToDeleteEntity(entity);

        if (entity is not ISoftDelete)
        {
            DbContext.Remove(entity);
        }

        await SaveChangesIfAutoSaveEnabledAsync();
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

    private static void SetCreationTime(IPersistentEntity entity)
    {
        if (entity is IHasCreationTime e)
        {
            e.CreationTime = DateTime.UtcNow;
        }
    }

    private static void SetDeletionTime(IPersistentEntity entity)
    {
        if (entity is not IHasDeletionTime e)
        {
            return;
        }

        e.DeletionTime = DateTime.UtcNow;
        e.IsDeleted = true;
    }

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

    private void SetModificationTime(IPersistentEntity entity)
    {
        if (entity is not IHasModificationTime e)
        {
            return;
        }

        if (IsModified(entity))
        {
            e.LastModificationTime = DateTime.UtcNow;
        }
    }

    private bool IsModified(IPersistentEntity entity)
    {
        return DbContext.Entry(entity).State == EntityState.Modified;
    }
}
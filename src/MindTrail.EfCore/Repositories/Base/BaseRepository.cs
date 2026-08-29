using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MindTrail.EfCore.Context;
using MindTrail.EfCore.Interfaces.Entities;

namespace MindTrail.EfCore.Repositories.Base;

/// <summary>
/// Database repository with implementation of CRUD operations.
/// </summary>
/// <param name="dbContext">The application database context.</param>
public abstract class BaseRepository(AppDbContext dbContext)
{
    /// <summary>
    /// The database context that provides access to database entities and saves changes to the database.
    /// </summary>
    protected readonly AppDbContext DbContext = dbContext;

    /// <summary>
    /// Prepares a persistent entity before adding it to the database.
    /// </summary>
    /// <param name="entity">The persistent entity to create.</param>
    /// <remarks>
    /// If necessary, sets the <see cref="IHasCreationTime.CreationTime"/>.
    /// </remarks>
    /// <exception cref="ArgumentNullException"><paramref name="entity"/> is <c>null</c>.</exception>
    protected static void SetAuditPropertiesToCreateEntity(IPersistentEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        SetCreationTime(entity);
    }

    /// <summary>
    /// Returns a list of all entities from the database.
    /// </summary>
    /// <typeparam name="TEntity">The type of the persistent entity.</typeparam>
    /// <returns>The list of all entities.</returns>
    protected IQueryable<TEntity> GetEntities<TEntity>()
        where TEntity : class, IPersistentEntity
    {
        return DbContext.Set<TEntity>();
    }

    /// <summary>
    /// Adds a new entity to the database context.
    /// </summary>
    /// <param name="entity">The persistent entity to create.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <typeparam name="TEntity">The type of the persistent entity.</typeparam>
    /// <returns>The created entity.</returns>
    protected async Task<TEntity> CreateEntityAsync<TEntity>(
        TEntity entity,
        CancellationToken cancellationToken = default)
        where TEntity : IPersistentEntity
    {
        SetAuditPropertiesToCreateEntity(entity);

        var createdEntity = (TEntity)(await DbContext.AddAsync(entity, cancellationToken)).Entity;

        await SaveChangesIfAutoSaveEnabledAsync();

        return createdEntity;
    }

    /// <summary>
    /// Updates an entity in the database context.
    /// </summary>
    /// <param name="entity">The persistent entity to update.</param>
    /// <typeparam name="TEntity">The type of the persistent entity.</typeparam>
    /// <remarks>Supports tracked and untracked entities.</remarks>
    /// <returns>A task that represents the asynchronous operation.</returns>
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
    /// Removes an entity from the database context.
    /// </summary>
    /// <param name="entity">The persistent entity to delete.</param>
    /// <typeparam name="TEntity">The type of the persistent entity.</typeparam>
    /// <returns>A task that represents the asynchronous operation.</returns>
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
    /// <param name="entity">The persistent entity to update.</param>
    /// <exception cref="ArgumentNullException"><paramref name="entity"/> is <c>null</c>.</exception>
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
    /// <param name="entity">The persistent entity to delete.</param>
    /// <exception cref="ArgumentNullException"><paramref name="entity"/> is <c>null</c>.</exception>
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
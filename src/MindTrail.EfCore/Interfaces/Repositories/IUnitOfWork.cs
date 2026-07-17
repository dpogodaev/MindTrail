using System.Threading;
using System.Threading.Tasks;

namespace MindTrail.EfCore.Interfaces.Repositories;

/// <summary>
/// Coordinates persisting changes made through repositories and managing database transactions.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Gets a value indicating whether changes are saved automatically after entity operations.
    /// </summary>
    bool IsAutoSaveEnabled { get; }

    /// <summary>
    /// Enables automatic saving of changes after entity operations.
    /// </summary>
    void EnableAutoSave();

    /// <summary>
    /// Persists all pending changes to the database.
    /// </summary>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Begins a new database transaction.
    /// </summary>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Commits the current database transaction, making all changes made within it permanent.
    /// </summary>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Rolls back the current database transaction, discarding all changes made within it.
    /// </summary>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
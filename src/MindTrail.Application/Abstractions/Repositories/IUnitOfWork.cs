using System.Threading.Tasks;

namespace MindTrail.Application.Abstractions.Repositories;

/// <summary>
/// Coordinates persisting changes made through repositories and managing database transactions.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Enables automatic saving of changes after entity operations.
    /// </summary>
    void EnableAutoSave();

    /// <summary>
    /// Persists all pending changes to the database.
    /// </summary>
    Task SaveChangesAsync();

    /// <summary>
    /// Begins a new database transaction.
    /// </summary>
    Task BeginTransactionAsync();

    /// <summary>
    /// Commits the current database transaction, making all changes made within it permanent.
    /// </summary>
    Task CommitTransactionAsync();

    /// <summary>
    /// Rolls back the current database transaction, discarding all changes made within it.
    /// </summary>
    Task RollbackTransactionAsync();
}
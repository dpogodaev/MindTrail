using System.Threading.Tasks;

namespace MindTrail.Application.Abstractions.Repositories;

public interface IUnitOfWork
{
    /// <summary>
    /// Enables automatic saving of changes after entity operations.
    /// </summary>
    void EnableAutoSave();

    Task SaveChangesAsync();

    Task BeginTransactionAsync();

    Task CommitTransactionAsync();

    Task RollbackTransactionAsync();
}
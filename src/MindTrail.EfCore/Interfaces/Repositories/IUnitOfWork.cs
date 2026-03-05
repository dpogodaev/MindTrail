using System.Threading.Tasks;

namespace MindTrail.EfCore.Interfaces.Repositories;

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

    Task SaveChangesAsync();

    Task BeginTransactionAsync();

    Task CommitTransactionAsync();

    Task RollbackTransactionAsync();
}
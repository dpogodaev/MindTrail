using System.Threading.Tasks;

namespace MindTrail.DomainServices.Interfaces.Storages.Repositories;

public interface IUnitOfWork
{
    /// <summary>
    /// Indicates if changes should be saved automatically after entity operations.
    /// </summary>
    bool IsAutoSaveEnabled { get; set; }

    /// <summary>
    /// Enables automatic saving of changes after entity operations.
    /// </summary>
    void EnableAutoSave();

    Task SaveChangesAsync();

    void BeginTransaction();

    void CommitTransaction();

    void RollbackTransaction();
}
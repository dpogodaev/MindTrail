using System.Threading;
using System.Threading.Tasks;
using AppRepositories = MindTrail.Application.Abstractions.Repositories;
using EfRepositories = MindTrail.EfCore.Interfaces.Repositories;

namespace MindTrail.ApplicationConfigurator.Abstractions.Adapters.Repositories;

public class UnitOfWorkAdapter(
    EfRepositories.IUnitOfWork unitOfWork)
    : AppRepositories.IUnitOfWork
{
    /// <inheritdoc/>
    public void EnableAutoSave()
    {
        unitOfWork.EnableAutoSave();
    }

    /// <inheritdoc/>
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        await unitOfWork.CommitTransactionAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        await unitOfWork.RollbackTransactionAsync(cancellationToken);
    }
}
using System.Threading.Tasks;
using AppRepositories = MindTrail.Application.Abstractions.Repositories;
using EfRepositories = MindTrail.EfCore.Interfaces.Repositories;

namespace MindTrail.HostConfiguration.Abstractions.Adapters.Repositories;

public class UnitOfWorkAdapter(
    EfRepositories.IUnitOfWork unitOfWork)
    : AppRepositories.IUnitOfWork
{
    public void EnableAutoSave()
    {
        unitOfWork.EnableAutoSave();
    }

    public async Task SaveChangesAsync()
    {
        await unitOfWork.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        await unitOfWork.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await unitOfWork.CommitTransactionAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await unitOfWork.RollbackTransactionAsync();
    }
}
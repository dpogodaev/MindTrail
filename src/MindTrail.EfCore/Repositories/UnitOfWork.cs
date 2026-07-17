using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using MindTrail.EfCore.Context;
using MindTrail.EfCore.Interfaces.Repositories;

namespace MindTrail.EfCore.Repositories;

/// <summary>
/// Entity Framework implementation of <see cref="IUnitOfWork"/>.
/// </summary>
/// <param name="dbContext">Database context.</param>
/// <typeparam name="TContext">The type of application database context.</typeparam>
public class UnitOfWork<TContext>(TContext dbContext)
    : IUnitOfWork, IDisposable
    where TContext : AppDbContext
{
    private IDbContextTransaction? _transaction;
    private bool _disposed;

    /// <summary>
    /// Finalizes an instance of the <see cref="UnitOfWork{TContext}"/> class.
    /// </summary>
    ~UnitOfWork()
    {
        Dispose(false);
    }

    /// <inheritdoc cref="IUnitOfWork.IsAutoSaveEnabled"/>
    public bool IsAutoSaveEnabled
    {
        get => dbContext.IsAutoSaveEnabled;
        private set => dbContext.IsAutoSaveEnabled = value;
    }

    /// <inheritdoc cref="IUnitOfWork.EnableAutoSave"/>
    public void EnableAutoSave()
    {
        IsAutoSaveEnabled = true;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc cref="IUnitOfWork.BeginTransactionAsync"/>
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
    }

    /// <inheritdoc cref="IUnitOfWork.CommitTransactionAsync"/>
    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null)
        {
            throw new InvalidOperationException("No active transaction to commit.");
        }

        await _transaction.CommitAsync(cancellationToken);
    }

    /// <inheritdoc cref="IUnitOfWork.RollbackTransactionAsync"/>
    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null)
        {
            throw new InvalidOperationException("No active transaction to rollback.");
        }

        await _transaction.RollbackAsync(cancellationToken);
        await _transaction.DisposeAsync();
    }

    /// <summary>
    /// Public implementation of Dispose pattern callable by consumers.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Protected implementation of Dispose pattern.
    /// </summary>
    /// <param name="disposing">
    /// Indicates if the method call comes from a Dispose method (its value is true) or from a finalizer (its value is false).
    /// </param>
    private void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            dbContext.Dispose();
        }

        _disposed = true;
    }
}
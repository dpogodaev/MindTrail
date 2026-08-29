using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using MindTrail.Application.Abstractions.Repositories;
using MindTrail.EfCore.Context;

namespace MindTrail.EfCore.Repositories;

/// <summary>
/// Entity Framework implementation of <see cref="IUnitOfWork"/>.
/// </summary>
/// <param name="dbContext">The database context.</param>
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

    /// <summary>
    /// Gets a value indicating whether changes should be saved automatically after entity operations.
    /// </summary>
    /// <exception cref="ObjectDisposedException">The unit of work has been disposed.</exception>
    public bool IsAutoSaveEnabled
    {
        get
        {
            ThrowIfDisposed();
            return dbContext.IsAutoSaveEnabled;
        }

        private set
        {
            ThrowIfDisposed();
            dbContext.IsAutoSaveEnabled = value;
        }
    }

    /// <inheritdoc/>
    /// <exception cref="ObjectDisposedException">The unit of work has been disposed.</exception>
    public void EnableAutoSave()
    {
        ThrowIfDisposed();
        IsAutoSaveEnabled = true;
    }

    /// <inheritdoc/>
    /// <exception cref="ObjectDisposedException">The unit of work has been disposed.</exception>
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    /// <exception cref="ObjectDisposedException">The unit of work has been disposed.</exception>
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();

        if (_transaction is not null)
        {
            throw new InvalidOperationException("A transaction is already active.");
        }

        _transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
    }

    /// <inheritdoc/>
    /// <exception cref="ObjectDisposedException">The unit of work has been disposed.</exception>
    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();

        if (_transaction is null)
        {
            throw new InvalidOperationException("No active transaction to commit.");
        }

        try
        {
            await _transaction.CommitAsync(cancellationToken);
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    /// <inheritdoc/>
    /// <exception cref="ObjectDisposedException">The unit of work has been disposed.</exception>
    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();

        if (_transaction is null)
        {
            throw new InvalidOperationException("No active transaction to rollback.");
        }

        try
        {
            await _transaction.RollbackAsync(cancellationToken);
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
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
    /// Indicates whether the method call comes from a Dispose method (its value is true) or from a finalizer (its value is false).
    /// </param>
    private void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _transaction?.Dispose();
            _transaction = null;
        }

        _disposed = true;
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(UnitOfWork<>));
        }
    }
}
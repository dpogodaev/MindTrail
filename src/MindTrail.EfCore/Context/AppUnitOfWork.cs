using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using MindTrail.DomainServices.Interfaces.Storages.Repositories;

namespace MindTrail.EfCore.Context
{
    /// <summary>
    /// Entity Framework implementation of <see cref="IUnitOfWork"/>.
    /// </summary>
    public class AppUnitOfWork<TContext> : IUnitOfWork, IDisposable where TContext : AppDbContext
    {
        private readonly TContext _dbContext;

        private IDbContextTransaction? _transaction;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppUnitOfWork{TContext}"/> class with database context provided.
        /// </summary>
        /// <param name="dbContext">Database context.</param>
        public AppUnitOfWork(TContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region IUnitOfWork

        /// <inheritdoc cref="IUnitOfWork.IsAutoSaveEnabled"/>
        public bool IsAutoSaveEnabled
        {
            get => _dbContext.IsAutoSaveEnabled;
            set => _dbContext.IsAutoSaveEnabled = value;
        }

        /// <inheritdoc cref="IUnitOfWork.EnableAutoSave"/>
        public void EnableAutoSave()
        {
            IsAutoSaveEnabled = true;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc cref="IUnitOfWork.BeginTransaction"/>
        public void BeginTransaction()
        {
            _transaction = _dbContext.Database.BeginTransaction();
        }

        /// <inheritdoc cref="IUnitOfWork.CommitTransaction"/>
        public void CommitTransaction()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("No active transaction to commit");
            }

            _transaction.Commit();
        }

        /// <inheritdoc cref="IUnitOfWork.RollbackTransaction"/>
        public void RollbackTransaction()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("No active transaction to rollback");
            }

            _transaction.Rollback();
            _transaction.Dispose();
        }

        #endregion

        #region IDisposable

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
        protected void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _dbContext.Dispose();
            }

            _disposed = true;
        }

        /// <summary>
        /// Finalizer to ensure resources are freed if <see cref="Dispose"/> was not called.
        /// </summary>
        ~AppUnitOfWork()
        {
            Dispose(false);
        }

        #endregion
    }
}
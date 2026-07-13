using System.Threading;
using System.Threading.Tasks;

namespace MindTrail.ApplicationContracts.Interfaces.Queries;

/// <summary>
/// Handles a query of type <typeparamref name="TQuery"/> and returns a result of type <typeparamref name="TResult"/>.
/// </summary>
/// <typeparam name="TQuery">Type of the query to handle.</typeparam>
/// <typeparam name="TResult">Type of the result returned after handling the query.</typeparam>
public interface IQueryHandler<in TQuery, TResult>
    where TQuery : IQuery<TResult>
{
    /// <summary>
    /// Handles the specified query.
    /// </summary>
    /// <param name="query">The query to handle.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The result of handling the query.</returns>
    Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
}
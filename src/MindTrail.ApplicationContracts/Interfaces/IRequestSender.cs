using System.Threading;
using System.Threading.Tasks;
using MindTrail.ApplicationContracts.Interfaces.Commands;
using MindTrail.ApplicationContracts.Interfaces.Queries;

namespace MindTrail.ApplicationContracts.Interfaces;

/// <summary>
/// Sends a command or query to its corresponding handler and returns the result.
/// </summary>
public interface IRequestSender
{
    /// <summary>
    /// Sends a command to its corresponding handler.
    /// </summary>
    /// <typeparam name="TResult">Type of the result produced when the command is handled.</typeparam>
    /// <param name="command">The command to send.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The result of handling the command.</returns>
    Task<TResult> Send<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a query to its corresponding handler.
    /// </summary>
    /// <typeparam name="TResult">Type of the result produced when the query is handled.</typeparam>
    /// <param name="query">The query to send.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The result of handling the query.</returns>
    Task<TResult> Send<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);
}
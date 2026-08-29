using System.Threading;
using System.Threading.Tasks;

namespace MindTrail.ApplicationContracts.Interfaces.Commands;

/// <summary>
/// Handles a command of type <typeparamref name="TCommand"/> and returns a result of type <typeparamref name="TResult"/>.
/// </summary>
/// <typeparam name="TCommand">The type of the command to handle.</typeparam>
/// <typeparam name="TResult">The type of the result returned after handling the command.</typeparam>
public interface ICommandHandler<in TCommand, TResult>
    where TCommand : ICommand<TResult>
{
    /// <summary>
    /// Handles the specified command.
    /// </summary>
    /// <param name="command">The command to handle.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The result of handling the command.</returns>
    Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}
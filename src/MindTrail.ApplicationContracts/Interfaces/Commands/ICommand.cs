namespace MindTrail.ApplicationContracts.Interfaces.Commands;

/// <summary>
/// Represents a command that produces a result of type <typeparamref name="TResult"/> when handled.
/// </summary>
/// <typeparam name="TResult">The type of the result produced when the command is handled.</typeparam>
public interface ICommand<TResult>;
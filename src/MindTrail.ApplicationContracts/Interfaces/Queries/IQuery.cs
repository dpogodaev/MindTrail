namespace MindTrail.ApplicationContracts.Interfaces.Queries;

/// <summary>
/// Represents a query that produces a result of type <typeparamref name="TResult"/> when handled.
/// </summary>
/// <typeparam name="TResult">The type of the result produced when the query is handled.</typeparam>
public interface IQuery<TResult>;
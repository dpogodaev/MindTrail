namespace MindTrail.ApplicationContracts.Requests.Commands;

/// <summary>
/// Represents a void return type for commands that produce no result, other than confirming completion.
/// </summary>
public readonly struct VoidResult
{
    /// <summary>
    /// Gets the single value of type <see cref="VoidResult"/>.
    /// </summary>
    public static readonly VoidResult Value = default;
}
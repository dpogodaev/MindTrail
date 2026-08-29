namespace MindTrail.ApplicationContracts.Models.Cards;

/// <summary>
/// Model for filtering cards.
/// </summary>
public sealed record CardFilterModel
{
    /// <summary>
    /// Gets a filter value by number.
    /// </summary>
    /// <remarks>
    /// Performs a partial, case-insensitive match.
    /// Ignored if <c>null</c> or empty.
    /// </remarks>
    public int? Number { get; init; }

    /// <summary>
    /// Gets a filter value by title.
    /// </summary>
    /// <remarks>
    /// Performs a partial, case-insensitive match.
    /// Ignored if <c>null</c> or empty.
    /// </remarks>
    public string? Title { get; init; }

    /// <summary>
    /// Gets a filter value by content.
    /// </summary>
    /// <remarks>
    /// Performs an exact match.
    /// Ignored if <c>null</c>.
    /// </remarks>
    public string? Content { get; init; }
}
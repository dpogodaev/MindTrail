namespace MindTrail.ApplicationContracts.Models.Persons;

/// <summary>
/// Model for filtering persons.
/// </summary>
public sealed record PersonFilterModel
{
    /// <summary>
    /// Gets a filter value by full name.
    /// </summary>
    /// <remarks>
    /// Performs a partial, case-insensitive match.
    /// Ignored if <c>null</c> or empty.
    /// </remarks>
    public string? FullName { get; init; }

    /// <summary>
    /// Gets a filter value by year of birth.
    /// </summary>
    /// <remarks>
    /// Performs an exact match.
    /// Ignored if <c>null</c>.
    /// </remarks>
    public int? BirthYear { get; init; }
}
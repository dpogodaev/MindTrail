namespace MindTrail.ApplicationContracts.RequestModels;

/// <summary>
/// Model for filtering persons.
/// </summary>
public sealed record PersonFilterModel(string? FullName, int? BirthYear)
{
    /// <summary>
    /// Gets the filter value by full name.
    /// </summary>
    /// <remarks>
    /// Performs a partial, case-insensitive match.
    /// Ignored if <c>null</c> or empty.
    /// </remarks>
    public string? FullName { get; } = FullName;

    /// <summary>
    /// Gets the filter value by year of birth.
    /// </summary>
    /// <remarks>
    /// Performs an exact match.
    /// Ignored if <c>null</c>.
    /// </remarks>
    public int? BirthYear { get; } = BirthYear;
}
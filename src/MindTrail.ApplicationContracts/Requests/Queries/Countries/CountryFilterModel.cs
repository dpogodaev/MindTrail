namespace MindTrail.ApplicationContracts.Requests.Queries.Countries;

/// <summary>
/// Model for filtering countries.
/// </summary>
public sealed record CountryFilterModel(string? Code, string? Name)
{
    /// <summary>
    /// Gets the filter value by country code.
    /// </summary>
    /// <remarks>
    /// Performs a partial, case-insensitive match.
    /// Ignored if <c>null</c> or empty.
    /// </remarks>
    public string? Code { get; } = Code;

    /// <summary>
    /// Gets the filter value by the name of the country.
    /// </summary>
    /// <remarks>
    /// Performs a partial, case-insensitive match.
    /// Ignored if <c>null</c> or empty.
    /// </remarks>
    public string? Name { get; } = Name;
}
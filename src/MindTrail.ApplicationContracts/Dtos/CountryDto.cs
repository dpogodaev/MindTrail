namespace MindTrail.ApplicationContracts.Dtos;

/// <summary>
/// Information about a country.
/// </summary>
public record CountryDto
{
    /// <summary>
    /// The unique identifier (primary key) of the country.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// The country code.
    /// </summary>
    public required string Code { get; init; }

    /// <summary>
    /// The name of the country.
    /// </summary>
    public required string Name { get; init; }
}
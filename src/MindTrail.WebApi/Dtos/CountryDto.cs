namespace MindTrail.WebApi.Dtos;

/// <summary>
/// Information about the country.
/// </summary>
public record CountryDto
{
    /// <summary>
    /// Gets a unique identifier (primary key).
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets the country code.
    /// </summary>
    public required string Code { get; init; }

    /// <summary>
    /// Gets the name of the country.
    /// </summary>
    public required string Name { get; init; }
}
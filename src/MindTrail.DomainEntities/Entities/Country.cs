namespace MindTrail.DomainEntities.Entities;

/// <summary>
/// Information about the country.
/// </summary>
public class Country
{
    /// <summary>
    /// Unique identifier (primary key).
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Code.
    /// </summary>
    public required string Code { get; init; }

    /// <summary>
    /// Name.
    /// </summary>
    public required string Name { get; init; }
}
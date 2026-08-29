using System.ComponentModel.DataAnnotations.Schema;
using MindTrail.EfCore.Interfaces.Entities;

namespace MindTrail.EfCore.Entities;

/// <summary>
/// Information about a country.
/// </summary>
[Table("Countries")]
public class Country : IPersistentEntity
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
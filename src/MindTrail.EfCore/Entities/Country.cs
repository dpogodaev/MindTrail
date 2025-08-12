using System.ComponentModel.DataAnnotations.Schema;
using MindTrail.EfCore.Interfaces.Entities;

namespace MindTrail.EfCore.Entities;

/// <summary>
/// Information about the country.
/// </summary>
[Table("Countries")]
public class Country : IPersistentEntity
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
using System;
using System.ComponentModel.DataAnnotations.Schema;
using MindTrail.EfCore.Interfaces.Entities;

namespace MindTrail.EfCore.Entities;

/// <summary>
/// Information about a person.
/// </summary>
[Table("Persons")]
public class Person : IPersistentEntity, IHasCreationTime, IHasModificationTime
{
    /// <summary>
    /// Gets a unique identifier (primary key).
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets or sets the full name.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Gets or sets the year of birth.
    /// </summary>
    public int? BirthYear { get; set; }

    /// <summary>
    /// Gets or sets the ID of the country in which the person was born.
    /// </summary>
    public int? BirthCountryId { get; set; }

    /// <summary>
    /// Gets the country in which the person was born.
    /// </summary>
    [ForeignKey(nameof(BirthCountryId))]
    public Country? BirthCountry { get; init; }

    /// <inheritdoc/>
    public DateTime CreationTime { get; set; }

    /// <inheritdoc/>
    public DateTime? LastModificationTime { get; set; }
}
using System;
using System.ComponentModel.DataAnnotations.Schema;
using MindTrail.EfCore.Interfaces.Entities;

namespace MindTrail.EfCore.Entities;

/// <summary>
/// Information about the person.
/// </summary>
[Table("Persons")]
public class Person : IPersistentEntity, IHasCreationTime, IHasModificationTime
{
    /// <summary>
    /// Gets unique identifier (primary key).
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets or sets full name.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Gets or sets year of birth.
    /// </summary>
    public int? BirthYear { get; set; }

    /// <summary>
    /// Gets or sets iD of the country of birth.
    /// </summary>
    public int? BirthCountryId { get; set; }

    /// <summary>
    /// Gets country of birth.
    /// </summary>
    [ForeignKey(nameof(BirthCountryId))]
    public Country? BirthCountry { get; init; }

    /// <inheritdoc cref="IHasCreationTime.CreationTime"/>
    public DateTime CreationTime { get; set; }

    /// <inheritdoc cref="IHasModificationTime.LastModificationTime"/>
    public DateTime? LastModificationTime { get; set; }
}
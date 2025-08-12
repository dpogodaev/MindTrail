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
    /// Unique identifier (primary key).
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Full name.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Year of birth.
    /// </summary>
    public int? BirthYear { get; set; }

    /// <summary>
    /// ID of the country of birth.
    /// </summary>
    public int? BirthCountryId { get; set; }

    #region Relashanship

    /// <summary>
    /// Country of birth.
    /// </summary>
    [ForeignKey(nameof(BirthCountryId))]
    public Country? BirthCountry { get; set; }

    #endregion

    #region Audit properties

    /// <summary>
    /// Creation time of this entity.
    /// </summary>
    public DateTime CreationTime { get; set; }

    /// <summary>
    /// The last modified time for this entity.
    /// </summary>
    public DateTime? LastModificationTime { get; set; }

    #endregion
}
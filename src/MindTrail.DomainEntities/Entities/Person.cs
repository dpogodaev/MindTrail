using System;

namespace MindTrail.DomainEntities.Entities;

/// <summary>
/// Information about the person.
/// </summary>
public class Person
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
    /// Gets or sets the name of the country where the person was born.
    /// </summary>
    public string? BirthCountryName { get; set; }
}
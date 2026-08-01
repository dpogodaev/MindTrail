using System;

namespace MindTrail.ApplicationContracts.Dtos;

/// <summary>
/// Information about the person.
/// </summary>
public record PersonDto
{
    /// <summary>
    /// Gets a unique identifier (primary key).
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets the full name.
    /// </summary>
    public required string FullName { get; init; }

    /// <summary>
    /// Gets the year of birth.
    /// </summary>
    public int? BirthYear { get; init; }

    /// <summary>
    /// Gets the ID of the country in which the person was born.
    /// </summary>
    public int? BirthCountryId { get; init; }

    /// <summary>
    /// Gets the name of the country where the person was born.
    /// </summary>
    public string? BirthCountryName { get; init; }
}
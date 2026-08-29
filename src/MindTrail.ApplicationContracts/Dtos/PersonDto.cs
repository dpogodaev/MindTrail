using System;

namespace MindTrail.ApplicationContracts.Dtos;

/// <summary>
/// Information about a person.
/// </summary>
public record PersonDto
{
    /// <summary>
    /// The unique identifier (primary key) of the person.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// The full name of the person.
    /// </summary>
    public required string FullName { get; init; }

    /// <summary>
    /// The person's year of birth.
    /// </summary>
    public int? BirthYear { get; init; }

    /// <summary>
    /// The ID of the country in which the person was born.
    /// </summary>
    public int? BirthCountryId { get; init; }

    /// <summary>
    /// The name of the country in which the person was born.
    /// </summary>
    public string? BirthCountryName { get; init; }
}
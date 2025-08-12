using System;

namespace MindTrail.WebApi.Dtos;

/// <summary>
/// Information about the person.
/// </summary>
public record PersonDto
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

    /// <summary>
    /// Country of birth.
    /// </summary>
    public string? BirthCountryName { get; set; }
}
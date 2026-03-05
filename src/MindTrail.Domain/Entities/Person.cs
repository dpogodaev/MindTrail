using System;
using MindTrail.Domain.ValueObjects;

namespace MindTrail.Domain.Entities;

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
    /// Gets the full name.
    /// </summary>
    public required PersonFullName FullName { get; init; }

    /// <summary>
    /// Gets the year of birth.
    /// </summary>
    public BirthYear? BirthYear { get; init; }

    /// <summary>
    /// Gets the ID of the country in which the person was born.
    /// </summary>
    public int? BirthCountryId { get; init; }
}
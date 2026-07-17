using System;
using MindTrail.Domain.ValueObjects;

namespace MindTrail.Domain.Entities;

/// <summary>
/// Information about the person.
/// </summary>
public class Person
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Person"/> class with identifier.
    /// </summary>
    /// <param name="id">A unique identifier.</param>
    /// <param name="fullName">The full name.</param>
    /// <param name="birthYear">The year of birth.</param>
    /// <param name="birthCountryId">The ID of the country in which the person was born.</param>
    public Person(
        Guid id,
        PersonFullName fullName,
        BirthYear? birthYear = null,
        int? birthCountryId = null)
        : this(fullName, birthYear, birthCountryId)
    {
        Id = id;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Person"/> class without identifier.
    /// </summary>
    /// <param name="fullName">The full name.</param>
    /// <param name="birthYear">The year of birth.</param>
    /// <param name="birthCountryId">The ID of the country in which the person was born.</param>
    public Person(
        PersonFullName fullName,
        BirthYear? birthYear = null,
        int? birthCountryId = null)
    {
        FullName = fullName;
        BirthYear = birthYear;
        BirthCountryId = birthCountryId;
    }

    /// <summary>
    /// Gets a unique identifier.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets the full name.
    /// </summary>
    public PersonFullName FullName { get; private set; }

    /// <summary>
    /// Gets the year of birth.
    /// </summary>
    public BirthYear? BirthYear { get; private set; }

    /// <summary>
    /// Gets the ID of the country in which the person was born.
    /// </summary>
    public int? BirthCountryId { get; private set; }

    public void Rename(PersonFullName fullName)
    {
        FullName = fullName;
    }

    public void ChangeBirthInformation(BirthYear? birthYear, int? birthCountryId)
    {
        BirthYear = birthYear;
        BirthCountryId = birthCountryId;
    }
}
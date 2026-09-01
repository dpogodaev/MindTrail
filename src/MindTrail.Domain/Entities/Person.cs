using System;
using MindTrail.Domain.ValueObjects;

namespace MindTrail.Domain.Entities;

/// <summary>
/// Information about a person.
/// </summary>
public class Person
{
    /// <summary>
    /// The identifier value used for a person that has not yet been persisted.
    /// </summary>
    private static readonly Guid UnassignedId = Guid.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="Person"/> class with identifier.
    /// </summary>
    /// <param name="id">A unique identifier. Optional.</param>
    /// <param name="fullName">The full name.</param>
    /// <param name="birthYear">The year of birth. Optional.</param>
    /// <param name="birthCountryId">The ID of the country in which the person was born. Optional.</param>
    private Person(
        Guid? id,
        PersonFullName fullName,
        BirthYear? birthYear = null,
        int? birthCountryId = null)
    {
        Id = id ?? UnassignedId;
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

    /// <summary>
    /// Creates a new instance of the <see cref="Person"/> class.
    /// </summary>
    /// <param name="fullName">The full name.</param>
    /// <param name="birthYear">The year of birth. Optional.</param>
    /// <param name="birthCountryId">The ID of the country in which the person was born. Optional.</param>
    /// <returns>A new <see cref="Person"/> instance.</returns>
    public static Person Create(
        PersonFullName fullName,
        BirthYear? birthYear = null,
        int? birthCountryId = null)
    {
        return new Person(null, fullName, birthYear, birthCountryId);
    }

    /// <summary>
    /// Renames the person.
    /// </summary>
    /// <param name="fullName">The new full name.</param>
    public void Rename(PersonFullName fullName)
    {
        FullName = fullName;
    }

    /// <summary>
    /// Changes the birth information.
    /// </summary>
    /// <param name="birthYear">The year of birth. Optional.</param>
    /// <param name="birthCountryId">The ID of the country in which the person was born. Optional.</param>
    public void ChangeBirthInformation(BirthYear? birthYear, int? birthCountryId)
    {
        BirthYear = birthYear;
        BirthCountryId = birthCountryId;
    }

    /// <summary>
    /// Restores a <see cref="Person"/> instance from persisted data.
    /// </summary>
    /// <param name="id">A unique identifier.</param>
    /// <param name="fullName">The full name.</param>
    /// <param name="birthYear">The year of birth. Optional.</param>
    /// <param name="birthCountryId">The ID of the country in which the person was born. Optional.</param>
    /// <returns>A <see cref="Person"/> instance restored from persistence.</returns>
    internal static Person FromPersistence(
        Guid id,
        string fullName,
        int? birthYear = null,
        int? birthCountryId = null)
    {
        return new Person(
            id,
            PersonFullName.FromPersistence(fullName),
            BirthYear.FromPersistence(birthYear),
            birthCountryId);
    }
}
using System;
using MindTrail.DomainShared.Exceptions.Base;

namespace MindTrail.DomainShared.Exceptions;

/// <summary>
/// An exception thrown when a person with the specified name and birth year already exists.
/// </summary>
/// <param name="personId">The ID of the person that already exists.</param>
/// <param name="fullName">The full name.</param>
/// <param name="birthYear">The year of birth.</param>
public class PersonDuplicateException(Guid personId, string fullName, int? birthYear)
    : DomainException(birthYear == null
        ? $"The person with the name {fullName} already exists, try to set his year of birth."
        : $"The person with the name {fullName} and birth year {birthYear} already exists.")
{
    /// <summary>
    /// Gets the person's ID.
    /// </summary>
    public Guid PersonId { get; } = personId;

    /// <summary>
    /// Gets the full name.
    /// </summary>
    public string SpecifiedFullName { get; } = fullName;

    /// <summary>
    /// Gets the year of birth.
    /// </summary>
    public int? SpecifiedBirthYear { get; } = birthYear;
}
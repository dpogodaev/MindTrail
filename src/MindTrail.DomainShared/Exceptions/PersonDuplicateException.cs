using System;
using MindTrail.DomainShared.Exceptions.Base;

namespace MindTrail.DomainShared.Exceptions;

/// <summary>
/// An exception occurs when a person with the specified name and birth year already exists.
/// </summary>
/// <param name="fullName">Full name.</param>
/// <param name="birthYear">Year of birth.</param>
public class PersonDuplicateException(Guid personId, string fullName, uint? birthYear)
    : DomainException(birthYear == null
        ? $"The person with the name {fullName} already exists, try to set his date of birth."
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
    public int? SpecifiedBirthYear { get; } = (int?)birthYear;
}
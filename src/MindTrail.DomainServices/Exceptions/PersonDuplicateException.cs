using MindTrail.DomainServices.Exceptions.Base;

namespace MindTrail.DomainServices.Exceptions;

/// <summary>
/// An exception occurs when a person with the specified name and date of birth already exists.
/// </summary>
/// <param name="fullName">Full name.</param>
/// <param name="birthYear">Year of birth.</param>
public class PersonDuplicateException(string fullName, int? birthYear)
    : DomainException(birthYear == null
        ? "The person with the specified name already exists, try to set his date of birth."
        : "The person with the specified name and date of birth already exists.")
{
    /// <summary>
    /// Gets the full name.
    /// </summary>
    public string FullName { get; } = fullName;

    /// <summary>
    /// Gets the year of birth.
    /// </summary>
    public int? BirthYear { get; } = birthYear;
}
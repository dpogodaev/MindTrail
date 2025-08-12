using System;

namespace MindTrail.DomainServices.Exceptions;

/// <summary>
/// An exception occurs when a person with the specified name and date of birth already exists.
/// </summary>
/// <param name="message">Error message explaining why the duplicate person was detected.</param>
/// <param name="fullName">Full name.</param>
/// <param name="birthYear">Year of birth.</param>
public class PersonDuplicateException(string message, string fullName, int? birthYear) : Exception(message)
{
    /// <summary>
    /// Full name.
    /// </summary>
    public string FullName { get; } = fullName;

    /// <summary>
    /// Year of birth.
    /// </summary>
    public int? BirthYear { get; set; } = birthYear;
}
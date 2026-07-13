using System;
using MindTrail.ApplicationContracts.Interfaces.Commands;
using MindTrail.DomainShared.Exceptions;

namespace MindTrail.ApplicationContracts.Requests.Commands;

/// <summary>
/// Command for creating a person.
/// </summary>
/// <exception cref="PersonNameTooLongException">The person's name is too long.</exception>
/// <exception cref="PersonDuplicateException">The person with the specified name and date of birth already exists.</exception>
/// <exception cref="CountryNotFoundException">The specified birth country does not exist.</exception>
/// <exception cref="BirthYearOutOfRangeException">The specified birth year is out of range.</exception>
public sealed record CreatePersonCommand : ICommand<Guid>
{
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
}
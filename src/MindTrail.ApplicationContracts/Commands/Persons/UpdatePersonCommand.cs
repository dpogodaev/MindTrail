using System;
using MindTrail.ApplicationContracts.Commands.Common;
using MindTrail.ApplicationContracts.Interfaces.Commands;
using MindTrail.DomainShared.Exceptions;
using MindTrail.DomainShared.Exceptions.Persons;

namespace MindTrail.ApplicationContracts.Commands.Persons;

/// <summary>
/// Command for updating a person.
/// </summary>
/// <exception cref="PersonNameTooLongException">The person's name is too long.</exception>
/// <exception cref="PersonDuplicateException">The person with the specified name and date of birth already exists.</exception>
/// <exception cref="CountryNotFoundException">The specified birth country does not exist.</exception>
/// <exception cref="BirthYearOutOfRangeException">The specified birth year is out of range.</exception>
/// <exception cref="PersonNotFoundException">The person with the specified ID was not found.</exception>
public sealed record UpdatePersonCommand : ICommand<VoidResult>
{
    /// <summary>
    /// Gets the ID of the person to update.
    /// </summary>
    public required Guid Id { get; init; }

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
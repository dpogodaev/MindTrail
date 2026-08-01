using System;
using MindTrail.ApplicationContracts.Commands.Common;
using MindTrail.ApplicationContracts.Interfaces.Commands;
using MindTrail.DomainShared.Exceptions.Persons;

namespace MindTrail.ApplicationContracts.Commands.Persons;

/// <summary>
/// Command for deleting a person.
/// </summary>
/// <exception cref="PersonNotFoundException">The person with the specified ID was not found.</exception>
public sealed record DeletePersonCommand : ICommand<VoidResult>
{
    /// <summary>
    /// Gets the ID of the person to delete.
    /// </summary>
    public required Guid Id { get; init; }
}
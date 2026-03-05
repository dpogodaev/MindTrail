using System;
using MindTrail.DomainShared.Exceptions.Base;

namespace MindTrail.DomainShared.Exceptions;

/// <summary>
/// Exception thrown when the person was not found.
/// </summary>
/// <param name="id">Person ID.</param>
public sealed class PersonNotFoundException(Guid id)
    : DomainException($"The person with identifier '{id}' was not found.")
{
    /// <summary>
    /// Gets the person's ID.
    /// </summary>
    public Guid Id { get; } = id;
}
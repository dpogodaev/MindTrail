using System;

namespace MindTrail.DomainServices.Exceptions;

/// <summary>
/// Exception thrown when the person was not found.
/// </summary>
/// <param name="personId">Person ID.</param>
public sealed class PersonNotFoundException(Guid personId)
    : Exception($"The person with identifier '{personId}' was not found")
{
    /// <summary>
    /// Person ID.
    /// </summary>
    public Guid PersonId { get; } = personId;
}
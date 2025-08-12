using System;

namespace MindTrail.AppServices.Exceptions;

/// <summary>
/// Thrown when the target resource was not found.
/// </summary>
public sealed class NotFoundException(string msg, string id) : Exception(msg)
{
    /// <summary>
    /// Identifier to search for.
    /// </summary>
    public string Id { get; } = id;
}
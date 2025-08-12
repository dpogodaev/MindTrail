using System;

namespace MindTrail.DomainServices.Exceptions;

/// <summary>
/// An exception thrown when the person name has an invalid value.
/// </summary>
public sealed class PersonNameException(string message, string fullName) : Exception(message)
{
    /// <summary>
    /// Full name.
    /// </summary>
    public string FullName { get; } = fullName;
}
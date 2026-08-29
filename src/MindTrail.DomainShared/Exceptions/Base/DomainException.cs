using System;

namespace MindTrail.DomainShared.Exceptions.Base;

/// <summary>
/// The base class for all domain exceptions.
/// </summary>
public abstract class DomainException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DomainException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    protected DomainException(string? message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DomainException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="exception">The exception that is the cause of the current exception.</param>
    protected DomainException(string? message, Exception exception)
        : base(message, exception)
    {
    }
}
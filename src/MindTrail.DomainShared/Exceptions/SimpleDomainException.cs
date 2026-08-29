using System;
using MindTrail.DomainShared.Exceptions.Base;

namespace MindTrail.DomainShared.Exceptions;

/// <summary>
/// A general-purpose domain exception used when no specific exception type applies.
/// </summary>
public class SimpleDomainException : DomainException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleDomainException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public SimpleDomainException(string? message = "")
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleDomainException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="e">The exception that is the cause of the current exception.</param>
    public SimpleDomainException(string message, Exception e)
        : base(message, e)
    {
    }
}
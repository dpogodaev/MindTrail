using System;

namespace MindTrail.DomainServices.Exceptions.Base;

public abstract class DomainException : Exception
{
    protected DomainException(string? message)
        : base(message)
    {
    }

    protected DomainException(string? message, Exception exception)
        : base(message, exception)
    {
    }
}
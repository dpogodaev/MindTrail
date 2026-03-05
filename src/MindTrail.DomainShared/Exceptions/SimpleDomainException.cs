using System;
using MindTrail.DomainShared.Exceptions.Base;

namespace MindTrail.DomainShared.Exceptions;

public class SimpleDomainException : DomainException
{
    public SimpleDomainException(string? message = "")
        : base(message)
    {
    }

    public SimpleDomainException(string message, Exception e)
        : base(message, e)
    {
    }
}
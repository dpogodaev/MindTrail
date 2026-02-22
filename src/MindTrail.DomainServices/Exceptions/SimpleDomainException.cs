using System;
using MindTrail.DomainServices.Exceptions.Base;

namespace MindTrail.DomainServices.Exceptions;

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
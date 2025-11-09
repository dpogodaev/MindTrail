using System;
using System.Collections.Generic;
using MindTrail.DomainServices.Exceptions;
using MindTrail.DomainServices.Exceptions.Base;
using MindTrail.WebApi.Interfaces.Providers;

namespace MindTrail.WebApi.Providers;

/// <inheritdoc/>
public class ErrorCodeProvider : IErrorCodeProvider
{
    private static readonly Dictionary<Type, string> ExceptionCodeMap = new()
    {
        { typeof(PersonNameTooLongException), "PERSON_NAME_TOO_LONG" },
        { typeof(PersonDuplicateException), "PERSON_NAME_DUPLICATE" },
    };

    /// <inheritdoc cref="IErrorCodeProvider.TryGetCode"/>
    public bool TryGetCode(DomainException e, out string? code)
    {
        return ExceptionCodeMap.TryGetValue(e.GetType(), out code);
    }
}
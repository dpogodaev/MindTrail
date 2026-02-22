using System;
using System.Collections.Generic;
using MindTrail.DomainServices.Exceptions;
using MindTrail.DomainServices.Exceptions.Base;

namespace MindTrail.WebHost.Abstractions.Providers;

/// <summary>
/// Provides an error code corresponding to the specified domain exception.
/// </summary>
public class ErrorCodeProvider
{
    private const string ServiceName = "mind_trail";

    private static readonly Dictionary<Type, string> ExceptionCodeMap = new()
    {
        { typeof(PersonNameTooLongException), $"{ServiceName}.person_name_too_long" },
        { typeof(PersonDuplicateException), $"{ServiceName}.person_duplicate" },
    };

    /// <summary>
    /// Tries to provide an error code for the specified domain exception.
    /// </summary>
    /// <param name="e">The domain exception.</param>
    /// <returns>The error code corresponding to the exception, if found; <c>null</c> otherwise.</returns>
    public string? GetCode(DomainException e)
    {
        string? code;

        if (e is SimpleDomainException)
        {
            if (e.InnerException == null)
            {
                return null;
            }

            ExceptionCodeMap.TryGetValue(e.InnerException.GetType(), out code);
            return code;
        }

        ExceptionCodeMap.TryGetValue(e.GetType(), out code);
        return code;
    }
}
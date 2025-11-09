using System;
using Microsoft.Extensions.Logging;
using MindTrail.DomainServices.Exceptions.Base;

namespace MindTrail.AppServices.Extensions;

/// <summary>
/// Provides extension methods for the <see cref="Exception"/> class.
/// </summary>
public static class ExceptionExtensions
{
    /// <summary>
    /// Determines the log level for the source exception.
    /// </summary>
    /// <param name="e">The source exception.</param>
    /// <returns>
    /// <c>LogLevel.Warning</c> if the exception is a handled exception;
    /// otherwise, <c>LogLevel.Error</c>.
    /// </returns>
    public static LogLevel GetLogLevel(this Exception e)
    {
        return e is DomainException ? LogLevel.Warning : LogLevel.Error;
    }
}
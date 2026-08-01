using System;
using Microsoft.Extensions.Logging;
using MindTrail.DomainShared.Exceptions.Base;

namespace MindTrail.ApplicationConfigurator.Extensions;

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
    public static LogLevel GetExceptionLogLevel(this Exception e)
    {
        return e is DomainException
            ? LogLevel.Warning
            : LogLevel.Error;
    }
}
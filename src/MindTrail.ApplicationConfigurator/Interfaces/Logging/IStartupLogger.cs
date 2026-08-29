using System;

namespace MindTrail.ApplicationConfigurator.Interfaces.Logging;

/// <summary>
/// Logger used in the application startup process.
/// </summary>
public interface IStartupLogger
{
    /// <summary>
    /// Writes the diagnostic message at the <c>Debug</c> level.
    /// </summary>
    /// <param name="msg">The log message.</param>
    void Debug(string msg);

    /// <summary>
    /// Writes the diagnostic message at the <c>Info</c> level.
    /// </summary>
    /// <param name="msg">The log message.</param>
    /// <param name="elapsedTimeInMs">The elapsed time in milliseconds. Optional.</param>
    /// <param name="totalElapsedTimeInMs">The total elapsed time in milliseconds. Optional.</param>
    void Info(string msg, long? elapsedTimeInMs = null, long? totalElapsedTimeInMs = null);

    /// <summary>
    /// Writes the diagnostic message at the <c>Warn</c> level.
    /// </summary>
    /// <param name="msg">The log message.</param>
    void Warn(string msg);

    /// <summary>
    /// Writes the diagnostic message and exception at the <c>Error</c> level.
    /// </summary>
    /// <param name="msg">The log message.</param>
    /// <param name="e">The exception to be logged. Optional.</param>
    void Error(string msg, Exception? e = null);
}
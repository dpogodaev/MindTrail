using System;

namespace ThoughtGuide.HostConfiguration.Interfaces;

/// <summary>
/// Logger used in the application startup process.
/// </summary>
public interface IStartupLogger
{
    /// <summary>
    /// Writes the diagnostic message at the <c>Debug</c> level.
    /// </summary>
    /// <param name="msg">Log message.</param>
    void Debug(string msg);

    /// <summary>
    /// Writes the diagnostic message at the <c>Info</c> level.
    /// </summary>
    /// <param name="msg">Log message.</param>
    /// <param name="elapsedTimeInMs">Elapsed time in milliseconds.</param>
    /// <param name="totalElapsedTimeInMs">Total elapsed time in milliseconds.</param>
    void Info(string msg, long? elapsedTimeInMs = null, long? totalElapsedTimeInMs = null);

    /// <summary>
    /// Writes the diagnostic message at the <c>Warn</c> level.
    /// </summary>
    /// <param name="msg">Log message.</param>
    void Warn(string msg);

    /// <summary>
    /// Writes the diagnostic message and exception at the <c>Error</c> level.
    /// </summary>
    /// <param name="msg">A <see langword="string" /> to be written.</param>
    /// <param name="e">An exception to be logged. Optional.</param>
    void Error(string msg, Exception e = null);
}
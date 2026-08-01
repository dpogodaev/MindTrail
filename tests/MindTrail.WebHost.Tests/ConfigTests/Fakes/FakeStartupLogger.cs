using System;
using System.Collections.Generic;
using MindTrail.ApplicationConfigurator.Interfaces.Logging;

namespace MindTrail.WebHost.Tests.ConfigTests.Fakes;

/// <summary>
/// Fake to implement the interface <see cref="IStartupLogger"/>.
/// </summary>
public class FakeStartupLogger : IStartupLogger
{
    /// <summary>
    /// Gets a list of debug messages.
    /// </summary>
    public List<string> DebugMsgList { get; } = [];

    /// <summary>
    /// Gets a list of info messages.
    /// </summary>
    public List<string> InfoMsgList { get; } = [];

    /// <summary>
    /// Gets a list of warning messages.
    /// </summary>
    public List<string> WarnMsgList { get; } = [];

    /// <summary>
    /// Gets a list of error messages.
    /// </summary>
    public List<string> ErrorMsgList { get; } = [];

    /// <inheritdoc cref="IStartupLogger.Debug"/>
    public void Debug(string msg)
    {
        DebugMsgList.Add(msg);
    }

    /// <inheritdoc cref="IStartupLogger.Info"/>
    public void Info(string msg, long? elapsedTimeInMs = null, long? totalElapsedTimeInMs = null)
    {
        InfoMsgList.Add(msg);
    }

    /// <inheritdoc cref="IStartupLogger.Warn"/>
    public void Warn(string msg)
    {
        WarnMsgList.Add(msg);
    }

    /// <inheritdoc cref="IStartupLogger.Error"/>
    public void Error(string msg, Exception? e = null)
    {
        ErrorMsgList.Add(msg);
    }
}
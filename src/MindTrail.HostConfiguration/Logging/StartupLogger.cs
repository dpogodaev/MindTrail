using System;
using MindTrail.HostConfiguration.Interfaces.Logging;
using NLog;

namespace MindTrail.HostConfiguration.Logging;

/// <inheritdoc cref="IStartupLogger"/>
public class StartupLogger : IStartupLogger
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    /// <inheritdoc cref="IStartupLogger.Debug"/>
    public void Debug(string msg)
    {
        _logger.Debug("{Title}", msg);
    }

    /// <inheritdoc cref="IStartupLogger.Info"/>
    public void Info(string msg, long? elapsedTimeInMs = null, long? totalElapsedTimeInMs = null)
    {
        if (elapsedTimeInMs == null && totalElapsedTimeInMs == null)
        {
            _logger.Info("{Title}", msg);
            return;
        }

        if (elapsedTimeInMs != null && totalElapsedTimeInMs == null)
        {
            _logger.Info("{Title} {ElapsedTimeInMs}", msg, elapsedTimeInMs);
            return;
        }

        if (elapsedTimeInMs == null)
        {
            _logger.Info("{Title} {TotalElapsedTimeInMs}", msg, totalElapsedTimeInMs);
            return;
        }

        _logger.Info("{Title} {ElapsedTimeInMs} {TotalElapsedTimeInMs}", msg, elapsedTimeInMs, totalElapsedTimeInMs);
    }

    /// <inheritdoc cref="IStartupLogger.Warn"/>
    public void Warn(string msg)
    {
        _logger.Warn("{Title}", msg);
    }

    /// <inheritdoc cref="IStartupLogger.Error"/>
    public void Error(string msg, Exception? e = null)
    {
        _logger.Error("{Title}", msg);
    }
}
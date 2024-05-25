using System;
using NLog;
using ThoughtGuide.HostConfiguration.Interfaces;

namespace ThoughtGuide.HostConfiguration.Logging;

/// <inheritdoc cref="IStartupLogger"/>
public class StartupLogger : IStartupLogger
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    #region IStartupLogger

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
            _logger.Info("{Title}: elapsedTime={ElapsedTimeInMs}", msg, elapsedTimeInMs);
            return;
        }

        if (elapsedTimeInMs == null)
        {
            _logger.Info("{Title}: totalElapsedTimeInMs={TotalElapsedTimeInMs}", msg, totalElapsedTimeInMs);
            return;
        }

        _logger.Info("{Title}: elapsedTime={ElapsedTimeInMs} totalElapsedTimeInMs={TotalElapsedTimeInMs}",
            msg, elapsedTimeInMs, totalElapsedTimeInMs);
    }

    /// <inheritdoc cref="IStartupLogger.Warn"/>
    public void Warn(string msg)
    {
        _logger.Warn("{Title}", msg);
    }

    /// <inheritdoc cref="IStartupLogger.Error"/>
    public void Error(string msg, Exception e = null)
    {
        _logger.Error("{Title}", msg);
    }

    #endregion
}
using System;
using MindTrail.ApplicationConfigurator.Interfaces.Logging;
using NLog;

namespace MindTrail.ApplicationConfigurator.Logging;

/// <inheritdoc/>
public class StartupLogger : IStartupLogger
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    /// <inheritdoc/>
    public void Debug(string msg)
    {
        _logger.Debug("{Title}", msg);
    }

    /// <inheritdoc/>
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

    /// <inheritdoc/>
    public void Warn(string msg)
    {
        _logger.Warn("{Title}", msg);
    }

    /// <inheritdoc/>
    public void Error(string msg, Exception? e = null)
    {
        _logger.Error(e, "{Title}", msg);
    }
}
using System;
using System.Diagnostics;
using ThoughtGuide.Common.Interfaces.Utilities;

namespace ThoughtGuide.Common.Utilities;

/// <inheritdoc cref="IElapsedTimeMeter"/>
public class ElapsedTimeMeter : IElapsedTimeMeter
{
    private readonly Stopwatch _stopwatch = new();

    private long _totalElapsedTimeInMs;

    /// <summary>
    /// Initializes a new instance of <see cref="ElapsedTimeMeter"/> class.
    /// </summary>
    /// <param name="autoStartupEnabled">Indicates if the time meter should be started automatically. Optional.</param>
    public ElapsedTimeMeter(bool autoStartupEnabled = false)
    {
        if (autoStartupEnabled)
        {
            Start();
        }
    }

    #region IElapsedTimeMeter

    /// <inheritdoc cref="IElapsedTimeMeter.IsActive"/>
    public bool IsActive { get; private set; }

    /// <inheritdoc cref="IElapsedTimeMeter.ElapsedTimeInMs"/>
    public long ElapsedTimeInMs => GetElapsedTimeInMs();

    /// <inheritdoc cref="IElapsedTimeMeter.TotalElapsedTimeInMs"/>
    public long TotalElapsedTimeInMs => _totalElapsedTimeInMs + GetElapsedTimeInMs();

    /// <inheritdoc cref="IElapsedTimeMeter.Start"/>
    public void Start()
    {
        if (IsActive)
        {
            throw new InvalidOperationException("The time meter has already been started");
        }

        IsActive = true;

        _stopwatch.Start();
    }

    /// <inheritdoc cref="IElapsedTimeMeter.Stop"/>
    public void Stop()
    {
        if (!IsActive)
        {
            throw new InvalidOperationException("The time meter was not started");
        }

        _stopwatch.Reset(); // Resets the value of the elapsed time.
        _totalElapsedTimeInMs = 0;

        IsActive = false;
    }

    /// <inheritdoc cref="IElapsedTimeMeter.Restart"/>
    public void Restart()
    {
        if (!IsActive)
        {
            throw new InvalidOperationException("The time meter is not active");
        }

        _totalElapsedTimeInMs += GetElapsedTimeInMs();

        _stopwatch.Reset(); // Resets the value of the elapsed time.
        _stopwatch.Start();
    }

    #endregion

    #region Private methods

    private long GetElapsedTimeInMs()
    {
        return _stopwatch.ElapsedMilliseconds;
    }

    #endregion
}
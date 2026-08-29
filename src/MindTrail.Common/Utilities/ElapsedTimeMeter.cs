using System;
using System.Diagnostics;
using MindTrail.Common.Interfaces.Utilities;

namespace MindTrail.Common.Utilities;

/// <inheritdoc/>
public class ElapsedTimeMeter : IElapsedTimeMeter
{
    private readonly Stopwatch _stopwatch = new();

    private long _totalElapsedTimeInMs;

    /// <summary>
    /// Initializes a new instance of the <see cref="ElapsedTimeMeter"/> class.
    /// </summary>
    /// <param name="autoStartupEnabled"><c>true</c> to start the time meter automatically; otherwise, <c>false</c>.</param>
    public ElapsedTimeMeter(bool autoStartupEnabled = false)
    {
        if (autoStartupEnabled)
        {
            Start();
        }
    }

    /// <inheritdoc/>
    public bool IsActive { get; private set; }

    /// <inheritdoc/>
    public long ElapsedTimeInMs => GetElapsedTimeInMs();

    /// <inheritdoc/>
    public long TotalElapsedTimeInMs => _totalElapsedTimeInMs + GetElapsedTimeInMs();

    /// <inheritdoc/>
    public void Start()
    {
        if (IsActive)
        {
            throw new InvalidOperationException("The time meter has already been started.");
        }

        IsActive = true;

        _stopwatch.Start();
    }

    /// <inheritdoc/>
    public void Stop()
    {
        if (!IsActive)
        {
            throw new InvalidOperationException("The time meter was not started.");
        }

        _stopwatch.Reset(); // Resets the value of the elapsed time.
        _totalElapsedTimeInMs = 0;

        IsActive = false;
    }

    /// <inheritdoc/>
    public void Restart()
    {
        if (!IsActive)
        {
            throw new InvalidOperationException("The time meter is not active.");
        }

        _totalElapsedTimeInMs += GetElapsedTimeInMs();

        _stopwatch.Reset(); // Resets the value of the elapsed time.
        _stopwatch.Start();
    }

    private long GetElapsedTimeInMs()
    {
        return _stopwatch.ElapsedMilliseconds;
    }
}
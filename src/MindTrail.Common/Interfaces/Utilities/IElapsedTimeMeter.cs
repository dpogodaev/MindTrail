using System;

namespace MindTrail.Common.Interfaces.Utilities;

/// <summary>
/// The elapsed time meter for measuring the time interval with readings in milliseconds.
/// </summary>
public interface IElapsedTimeMeter
{
    /// <summary>
    /// Gets a value indicating whether the time meter is active.
    /// </summary>
    /// <remarks>
    /// When the time meter is not active, the values of the <see cref="ElapsedTimeInMs"/> and <see cref="TotalElapsedTimeInMs"/> are <c>0</c>.
    /// </remarks>
    bool IsActive { get; }

    /// <summary>
    /// Gets the elapsed time in milliseconds since the meter was started.
    /// </summary>
    long ElapsedTimeInMs { get; }

    /// <summary>
    /// Gets the total elapsed time in milliseconds (taking into account the restart).
    /// </summary>
    long TotalElapsedTimeInMs { get; }

    /// <summary>
    /// Starts the time meter.
    /// </summary>
    /// <remarks>After the time meter is started, the <see cref="IsActive"/> is set to <c>true</c>.</remarks>
    /// <exception cref="InvalidOperationException">The time meter has already been started.</exception>
    void Start();

    /// <summary>
    /// Stops the time meter.
    /// </summary>
    /// <remarks>
    /// After the time meter is stopped, the <see cref="IsActive"/> is set to <c>false</c>,
    /// <see cref="ElapsedTimeInMs"/> and <see cref="TotalElapsedTimeInMs"/> are set to <c>0</c>.
    /// </remarks>
    /// <exception cref="InvalidOperationException">The time meter was not started.</exception>
    void Stop();

    /// <summary>
    /// Restarts the time meter when it is active.
    /// </summary>
    /// <remarks>
    /// The <see cref="ElapsedTimeInMs"/> is set to <c>0</c>.<br/>
    /// The <see cref="TotalElapsedTimeInMs"/> is not reset.
    /// </remarks>
    /// <exception cref="InvalidOperationException">The time meter is not active.</exception>
    void Restart();
}
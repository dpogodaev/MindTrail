using System;

namespace MindTrail.Common.Interfaces.Providers;

/// <summary>
/// Current time provider.
/// </summary>
public interface ICurrentTimeProvider
{
    /// <summary>
    /// Returns the current date and time.
    /// </summary>
    /// <returns>The current date and time (UTC).</returns>
    DateTime GetCurrentTime();
}
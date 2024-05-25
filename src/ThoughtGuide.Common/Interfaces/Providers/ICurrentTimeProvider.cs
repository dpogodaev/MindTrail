using System;

namespace ThoughtGuide.Common.Interfaces.Providers;

/// <summary>
/// Current time provider.
/// </summary>
public interface ICurrentTimeProvider
{
    /// <summary>
    /// Returns the current date and time.
    /// </summary>
    DateTime GetCurrentTime();
}
using System;
using ThoughtGuide.Common.Interfaces.Providers;

namespace ThoughtGuide.Common.Providers;

/// <inheritdoc cref="ICurrentTimeProvider"/>
public class CurrentTimeProvider : ICurrentTimeProvider
{
    /// <inheritdoc cref="ICurrentTimeProvider.GetCurrentTime"/>
    public DateTime GetCurrentTime()
    {
        return DateTime.UtcNow;
    }
}
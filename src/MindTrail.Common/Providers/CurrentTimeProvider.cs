using System;
using MindTrail.Common.Interfaces.Providers;

namespace MindTrail.Common.Providers;

/// <inheritdoc cref="ICurrentTimeProvider"/>
public class CurrentTimeProvider : ICurrentTimeProvider
{
    /// <inheritdoc cref="ICurrentTimeProvider.GetCurrentTime"/>
    public DateTime GetCurrentTime()
    {
        return DateTime.UtcNow;
    }
}
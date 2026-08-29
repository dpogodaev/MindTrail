using System;
using MindTrail.Common.Interfaces.Providers;

namespace MindTrail.Common.Providers;

/// <inheritdoc/>
public class CurrentTimeProvider : ICurrentTimeProvider
{
    /// <inheritdoc/>
    public DateTime GetCurrentTime()
    {
        return DateTime.UtcNow;
    }
}
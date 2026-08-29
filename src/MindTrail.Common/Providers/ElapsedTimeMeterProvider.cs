using MindTrail.Common.Interfaces.Providers;
using MindTrail.Common.Interfaces.Utilities;
using MindTrail.Common.Utilities;

namespace MindTrail.Common.Providers;

/// <inheritdoc/>
public class ElapsedTimeMeterProvider : IElapsedTimeMeterProvider
{
    /// <inheritdoc/>
    public IElapsedTimeMeter GetElapsedTimeMeter(bool enableAutoStartup = false)
    {
        return new ElapsedTimeMeter(enableAutoStartup);
    }
}
using ThoughtGuide.Common.Interfaces.Providers;
using ThoughtGuide.Common.Interfaces.Utilities;
using ThoughtGuide.Common.Utilities;

namespace ThoughtGuide.Common.Providers;

/// <inheritdoc cref="IElapsedTimeMeterProvider"/>
public class ElapsedTimeMeterProvider : IElapsedTimeMeterProvider
{
    /// <inheritdoc cref="IElapsedTimeMeterProvider.GetElapsedTimeMeter"/>
    public IElapsedTimeMeter GetElapsedTimeMeter(bool enableAutoStartup = false)
    {
        return new ElapsedTimeMeter(enableAutoStartup);
    }
}
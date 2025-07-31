using MindTrail.Common.Interfaces.Utilities;

namespace MindTrail.Common.Interfaces.Providers;

/// <summary>
/// Used to get the elapsed time meter.
/// </summary>
public interface IElapsedTimeMeterProvider
{
    /// <summary>
    /// Returns the elapsed time meter.
    /// </summary>
    /// <param name="enableAutoStartup">Indicates if the time meter should be started automatically. Optional.</param>
    /// <returns>The elapsed time meter.</returns>
    IElapsedTimeMeter GetElapsedTimeMeter(bool enableAutoStartup = false);
}
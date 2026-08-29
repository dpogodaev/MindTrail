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
    /// <param name="enableAutoStartup"><c>true</c> to start the time meter automatically; otherwise, <c>false</c>.</param>
    /// <returns>The elapsed time meter.</returns>
    IElapsedTimeMeter GetElapsedTimeMeter(bool enableAutoStartup = false);
}
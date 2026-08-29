using System.Diagnostics;

namespace MindTrail.WebHost.Abstractions.Providers;

/// <summary>
/// Provides the trace ID of the current activity.
/// </summary>
public class TraceIdProvider
{
    /// <summary>
    /// Gets the trace ID of the current activity, if any.
    /// </summary>
    public string? TraceId { get; } = Activity.Current?.TraceId.ToString();
}
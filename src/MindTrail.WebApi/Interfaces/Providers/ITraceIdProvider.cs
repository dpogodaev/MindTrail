namespace MindTrail.WebApi.Interfaces.Providers;

/// <summary>
/// Provides access to trace ID.
/// </summary>
public interface ITraceIdProvider
{
    /// <summary>
    /// Gets the trace ID.
    /// </summary>
    string? TraceId { get; }
}
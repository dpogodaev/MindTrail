namespace MindTrail.WebApi.Interfaces.Providers;

/// <summary>
/// Provides access to trace ID.
/// </summary>
public interface ITraceIdProvider
{
    /// <summary>
    /// Trace ID.
    /// </summary>
    string? TraceId { get; }
}
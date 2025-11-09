using System.Diagnostics;
using MindTrail.WebApi.Interfaces.Providers;

namespace MindTrail.WebHost.Providers;

/// <inheritdoc/>
public class TraceIdProvider : ITraceIdProvider
{
    /// <inheritdoc cref="ITraceIdProvider.TraceId"/>
    public string? TraceId { get; } = Activity.Current?.TraceId.ToString();
}
using System.Diagnostics;

namespace MindTrail.WebHost.Abstractions.Providers;

public class TraceIdProvider
{
    public string? TraceId { get; } = Activity.Current?.TraceId.ToString();
}
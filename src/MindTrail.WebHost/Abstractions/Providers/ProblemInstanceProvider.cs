using System;
using Microsoft.Extensions.Configuration;
using MindTrail.ApplicationConfigurator.Extensions;

namespace MindTrail.WebHost.Abstractions.Providers;

/// <summary>
/// Builds the monitoring dashboard URI used as the <c>instance</c> field in Problem Details error responses.
/// </summary>
/// <param name="configuration">The application configuration.</param>
public class ProblemInstanceProvider(IConfiguration configuration)
{
    private const string InstanceConfigParam = "NLog:Loki:Instance";
    private const string ServiceNameConfigParam = "NLog:Loki:ServiceName";

    private readonly string? _instance = configuration.GetProperty(InstanceConfigParam);
    private readonly string? _serviceName = configuration.GetProperty(ServiceNameConfigParam);

    /// <summary>
    /// Returns a monitoring dashboard URI for the specified trace ID.
    /// </summary>
    /// <param name="traceId">The trace ID to include in the URI. Optional.</param>
    /// <returns>
    /// The monitoring dashboard URI, or <c>null</c> if the service name is not configured
    /// or <paramref name="traceId"/> is <c>null</c> or empty.
    /// </returns>
    public string? GetInstance(string? traceId)
    {
        if (string.IsNullOrEmpty(_serviceName) || string.IsNullOrEmpty(traceId))
        {
            return null;
        }

        var errorTime = DateTimeOffset.UtcNow;
        var fromMs = (errorTime - TimeSpan.FromMinutes(5)).ToUnixTimeMilliseconds().ToString();
        var toMs = (errorTime + TimeSpan.FromMinutes(5)).ToUnixTimeMilliseconds().ToString();

        var queryString =
            $"&timezone=utc" +
            $"&from={fromMs}" +
            $"&to={toMs}" +
            $"&var-app={_serviceName}" +
            $"&var-search={traceId}";

        if (_instance == null || !_instance.Contains('?'))
        {
            return queryString;
        }

        return _instance + queryString;
    }
}
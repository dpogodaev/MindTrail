using System;
using Microsoft.Extensions.Configuration;
using MindTrail.HostConfiguration.Extensions;

namespace MindTrail.WebHost.Abstractions.Providers;

public class ProblemInstanceProvider(IConfiguration configuration)
{
    private const string InstanceConfigParam = "NLog:Loki:Instance";
    private const string ServiceNameConfigParam = "NLog:Loki:ServiceName";

    private readonly string? _instance = configuration.GetProperty(InstanceConfigParam);
    private readonly string? _serviceName = configuration.GetProperty(ServiceNameConfigParam);

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
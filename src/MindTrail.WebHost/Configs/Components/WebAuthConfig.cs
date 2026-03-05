using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MindTrail.HostConfiguration.Extensions;
using MindTrail.HostConfiguration.Interfaces.Logging;
using MindTrail.WebAuth.Constants;
using MindTrail.WebAuth.Extensions;
using MindTrail.WebAuth.Filters;
using MindTrail.WebAuth.Interfaces.Validators;
using MindTrail.WebAuth.Settings;
using MindTrail.WebAuth.Validators;

namespace MindTrail.WebHost.Configs.Components;

/// <summary>
/// Used to configure the component <see cref="MindTrail.WebAuth"/>.
/// </summary>
internal static class WebAuthConfig
{
    private const string ApiKeyConfigParam = "App:ApiKey";
    private const string AdditionalApiKeyConfigParam = "App:AdditionalApiKeys";
    private const string DefaultAuthenticationScheme = ApiKeyConstants.ApiKeySchemeName;
    private const string ApiKeyClaimName = "FullAccessByApiKey";

    /// <summary>
    /// Adds a configuration for user authentication and authorization.
    /// </summary>
    /// <param name="services">Used to register application services.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <param name="logger">The startup logger. Optional.</param>
    public static void AddWebAuthConfig(
        this IServiceCollection services, IConfiguration configuration, IStartupLogger? logger = null)
    {
        AddAuthNConfig(services);
        AddAuthZConfig(services);

        AddSettings(services, configuration, logger);
        AddFilters(services);
        AddValidators(services);
    }

    private static void AddAuthNConfig(IServiceCollection services)
    {
        services.AddAuthentication(DefaultAuthenticationScheme)
            .AddApiKey(
                ApiKeyConstants.ApiKeySchemeName,
                options =>
                {
                    options.ApiKeyHeaderName = ApiKeyConstants.ApiKeyHeaderName;
                    options.ClaimName = ApiKeyClaimName;
                });
    }

    private static void AddAuthZConfig(IServiceCollection services)
    {
        services.AddAuthorization();
    }

    private static void AddSettings(
        IServiceCollection services, IConfiguration configuration, IStartupLogger? logger = null)
    {
        if (!configuration.TryGetProperty(ApiKeyConfigParam, out var apiKey))
        {
            logger?.Warn($"The configuration parameter '{ApiKeyConfigParam}' is not specified");
        }

        var additionalApiKeys = configuration.BindSection<Dictionary<string, string>>(AdditionalApiKeyConfigParam);

        services.AddTransient(_ => new ApiKeySettings
        {
            ApiKey = apiKey,
            HeaderName = ApiKeyConstants.ApiKeyHeaderName,
            AdditionalApiKeys = additionalApiKeys,
        });
    }

    private static void AddValidators(IServiceCollection services)
    {
        services.AddTransient<IApiKeyValidator, ApiKeyValidator>();
    }

    private static void AddFilters(IServiceCollection services)
    {
        services.AddTransient<ApiKeyAuthZFilter>();
    }
}
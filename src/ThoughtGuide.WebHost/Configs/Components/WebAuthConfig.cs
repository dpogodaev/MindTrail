using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ThoughtGuide.HostConfiguration.Extensions;
using ThoughtGuide.HostConfiguration.Interfaces;
using ThoughtGuide.WebAuth.Constants;
using ThoughtGuide.WebAuth.Extensions;
using ThoughtGuide.WebAuth.Filters;
using ThoughtGuide.WebAuth.Interfaces.Validators;
using ThoughtGuide.WebAuth.Settings;
using ThoughtGuide.WebAuth.Validators;

namespace ThoughtGuide.WebHost.Configs.Components;

/// <summary>
/// Configuration of component <see cref="ThoughtGuide.WebAuth"/>.
/// </summary>
internal static class WebAuthConfig
{
    private const string ApiKeyConfigParam = "App:ApiKey";
    private const string AdditionalApiKeyConfigParam = "App:AdditionalApiKeys";
    private const string DefaultAuthenticationScheme = ApiKeyConstants.ApiKeySchemeName;
    private const string ApiKeyClaimName = "FullAccessByApiKey";

    /// <summary>
    /// Adds configuration for component <see cref="ThoughtGuide.WebAuth"/>.
    /// </summary>
    public static void AddWebAuthConfig(this IServiceCollection services,
        IConfiguration configuration, IStartupLogger logger = null)
    {
        AddAuthNConfig(services);
        AddAuthZConfig(services);

        AddSettings(services, configuration, logger);
        AddFilters(services);
        AddValidators(services);
    }

    #region Private methods

    private static void AddAuthNConfig(IServiceCollection services)
    {
        services.AddAuthentication(DefaultAuthenticationScheme)
            .AddApiKey(ApiKeyConstants.ApiKeySchemeName,
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

    private static void AddSettings(IServiceCollection services, IConfiguration configuration,
        IStartupLogger logger = null)
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
            AdditionalApiKeys = additionalApiKeys
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

    #endregion
}
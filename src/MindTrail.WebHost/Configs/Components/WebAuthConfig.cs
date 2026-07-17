using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MindTrail.ApplicationConfigurator.Extensions;
using MindTrail.ApplicationConfigurator.Interfaces.Logging;
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

    /// <param name="services">Used to register application services.</param>
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Adds a configuration for user authentication and authorization.
        /// </summary>
        /// <param name="configuration">The application configuration.</param>
        /// <param name="logger">The startup logger. Optional.</param>
        public IServiceCollection AddWebAuthConfig(
            IConfiguration configuration,
            IStartupLogger? logger = null)
        {
            services.AddAuthNConfig();
            services.AddAuthZConfig();

            services.AddSettings(configuration, logger);
            services.AddFilters();
            services.AddValidators();

            return services;
        }

        private void AddAuthNConfig()
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

        private void AddAuthZConfig()
        {
            services.AddAuthorization();
        }

        private void AddSettings(
            IConfiguration configuration,
            IStartupLogger? logger = null)
        {
            if (!configuration.TryGetProperty(ApiKeyConfigParam, out var apiKey))
            {
                logger?.Warn($"The configuration parameter '{ApiKeyConfigParam}' is not specified");
            }

            var additionalApiKeys = configuration.BindSection<Dictionary<string, string>>(AdditionalApiKeyConfigParam);

            services.AddScoped(_ => new ApiKeySettings
            {
                ApiKey = apiKey!,
                HeaderName = ApiKeyConstants.ApiKeyHeaderName,
                AdditionalApiKeys = additionalApiKeys,
            });
        }

        private void AddValidators()
        {
            services.AddScoped<IApiKeyValidator, ApiKeyValidator>();
        }

        private void AddFilters()
        {
            services.AddScoped<ApiKeyAuthZFilter>();
        }
    }
}
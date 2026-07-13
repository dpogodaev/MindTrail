using Microsoft.Extensions.DependencyInjection;
using MindTrail.WebHost.Abstractions.Providers;
using MindTrail.WebHost.Services.Hosted;

namespace MindTrail.WebHost.Configs.Components;

/// <summary>
/// Used to configure the component <see cref="MindTrail.WebHost"/>.
/// </summary>
internal static class WebHostConfig
{
    /// <param name="services">Used to register application services.</param>
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Adds a configuration for the web host (infrastructure services, providers, adapters, etc.).
        /// </summary>
        public IServiceCollection AddWebHostConfig()
        {
            services.AddProviders();
            services.AddHostedServicesConfig();

            return services;
        }

        private void AddProviders()
        {
            services
                .AddSingleton<TraceIdProvider>()
                .AddSingleton<ErrorCodeProvider>()
                .AddSingleton<ProblemInstanceProvider>();
        }

        private void AddHostedServicesConfig()
        {
            services.AddHostedService<AppLifetimeHostedService>();
        }
    }
}
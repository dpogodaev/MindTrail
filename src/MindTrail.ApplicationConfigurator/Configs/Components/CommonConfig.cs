using Microsoft.Extensions.DependencyInjection;
using MindTrail.Common.Interfaces.Providers;
using MindTrail.Common.Providers;

namespace MindTrail.ApplicationConfigurator.Configs.Components;

/// <summary>
/// Used to configure the component <see cref="MindTrail.Common"/>.
/// </summary>
public static class CommonConfig
{
    /// <summary>
    /// Extension members for registering application services in the dependency injection container.
    /// </summary>
    /// <param name="services">Used to register application services.</param>
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Adds a configuration for all types of shared resources (providers, helpers, extensions, utils, etc.).
        /// </summary>
        /// <returns>The same <see cref="IServiceCollection"/> instance, so that additional calls can be chained.</returns>
        public IServiceCollection AddCommonConfig()
        {
            services.AddProviders();

            return services;
        }

        private void AddProviders()
        {
            services
                .AddTransient<ICurrentTimeProvider, CurrentTimeProvider>()
                .AddTransient<IElapsedTimeMeterProvider, ElapsedTimeMeterProvider>();
        }
    }
}
using Microsoft.Extensions.DependencyInjection;

namespace MindTrail.ApplicationConfigurator.Configs.Components;

/// <summary>
/// Used to configure the component <see cref="MindTrail.Domain"/>.
/// </summary>
public static class DomainConfig
{
    /// <summary>
    /// Extension members for registering application services in the dependency injection container.
    /// </summary>
    /// <param name="services">Used to register application services.</param>
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Adds a configuration for domain services.
        /// </summary>
        /// <returns>The same <see cref="IServiceCollection"/> instance, so that additional calls can be chained.</returns>
        public IServiceCollection AddDomainServicesConfig()
        {
            services.AddServices();

            return services;
        }

        private void AddServices()
        {
            // Domain services.
        }
    }
}
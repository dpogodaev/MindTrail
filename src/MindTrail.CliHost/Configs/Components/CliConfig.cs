using Microsoft.Extensions.DependencyInjection;
using MindTrail.Cli.Services;

namespace MindTrail.CliHost.Configs.Components;

/// <summary>
/// Used to configure the component <see cref="MindTrail.Cli"/>.
/// </summary>
internal static class CliConfig
{
    /// <param name="services">Used to register application services.</param>
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Adds a configuration for command line interface (hosted services, etc.).
        /// </summary>
        /// <returns>The same <see cref="IServiceCollection"/> instance, so that additional calls can be chained.</returns>
        public IServiceCollection AddCliConfig()
        {
            services.AddServices();

            return services;
        }

        private void AddServices()
        {
            services.AddHostedService<CliService>();
        }
    }
}
using System;
using Microsoft.Extensions.DependencyInjection;
using MindTrail.Application.Abstractions.Repositories;
using MindTrail.Application.Handlers;
using MindTrail.ApplicationConfigurator.Abstractions.Adapters.Repositories;
using MindTrail.ApplicationConfigurator.Extensions;
using MindTrail.ApplicationConfigurator.Logging.Handlers;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.ApplicationContracts.Interfaces.Commands;
using MindTrail.ApplicationContracts.Requests.Commands;

namespace MindTrail.ApplicationConfigurator.Configs.Components;

/// <summary>
/// Used to configure the component <see cref="MindTrail.Application"/>.
/// </summary>
public static class ApplicationConfig
{
    /// <summary>
    /// Extension members for registering application services in the dependency injection container.
    /// </summary>
    /// <param name="services">Used to register application services.</param>
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Adds a configuration for application services.
        /// </summary>
        /// <returns>The same <see cref="IServiceCollection"/> instance, so that additional calls can be chained.</returns>
        public IServiceCollection AddApplicationConfig()
        {
            services.AddRepositories();
            services.AddCommandHandlers();

            return services;
        }

        private void AddRepositories()
        {
            services
                .AddScoped<IUnitOfWork, UnitOfWorkAdapter>()
                .AddTransient<ICountryRepository, CountryRepositoryAdapter>()
                .AddTransient<IPersonRepository, PersonRepositoryAdapter>();
        }

        private void AddCommandHandlers()
        {
            services.AddScopedDecorated<
                ICommandHandler<CreatePersonCommand, Guid>,
                CreatePersonCommandHandler,
                PersonCreationCommandHandlerLogging>();
        }
    }
}
using System;
using Microsoft.Extensions.DependencyInjection;
using MindTrail.Application.Handlers;
using MindTrail.ApplicationConfigurator.Abstractions.Providers;
using MindTrail.ApplicationConfigurator.Extensions;
using MindTrail.ApplicationConfigurator.Logging.Handlers;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.ApplicationContracts.Interfaces;
using MindTrail.ApplicationContracts.Interfaces.Commands;
using MindTrail.ApplicationContracts.Interfaces.Queries;
using MindTrail.ApplicationContracts.Requests.Commands;
using MindTrail.ApplicationContracts.Requests.Queries.Countries;
using MindTrail.ApplicationContracts.Requests.Queries.Persons;
using MindTrail.EfCore.Handlers.Queries;

namespace MindTrail.ApplicationConfigurator.Configs.Components;

/// <summary>
/// Used to configure the component <see cref="MindTrail.ApplicationContracts"/>.
/// </summary>
public static class ApplicationContractsConfig
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
        public IServiceCollection AddApplicationContractsConfig()
        {
            services.AddRequestSender();
            services.AddQueryHandlers();
            services.AddCommandHandlers();

            return services;
        }

        private void AddRequestSender()
        {
            services.AddScoped<IRequestSender, RequestSenderProvider>();
        }

        private void AddQueryHandlers()
        {
            services
                .AddScoped<IQueryHandler<GetPersonByIdQuery, PersonDto?>, GetPersonByIdQueryHandler>()
                .AddScoped<IQueryHandler<GetPersonsQuery, PagedDto<PersonDto>>, GetPersonsQueryHandler>()
                .AddScoped<IQueryHandler<GetCountriesQuery, PagedDto<CountryDto>>, GetCountriesQueryHandler>();
        }

        private void AddCommandHandlers()
        {
            services
                .AddScoped<ICommandHandler<UpdatePersonCommand, VoidResult>, UpdatePersonCommandHandler>()
                .AddScoped<ICommandHandler<DeletePersonCommand, VoidResult>, DeletePersonCommandHandler>();

            services.AddScopedDecorated<
                ICommandHandler<CreatePersonCommand, Guid>,
                CreatePersonCommandHandler,
                PersonCreationCommandHandlerLogging>();
        }
    }
}
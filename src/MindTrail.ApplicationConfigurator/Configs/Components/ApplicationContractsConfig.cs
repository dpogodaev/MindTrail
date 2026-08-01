using System;
using Microsoft.Extensions.DependencyInjection;
using MindTrail.Application.Handlers.Cards;
using MindTrail.Application.Handlers.Persons;
using MindTrail.ApplicationConfigurator.Abstractions.Providers;
using MindTrail.ApplicationConfigurator.Extensions;
using MindTrail.ApplicationConfigurator.Logging.Commands;
using MindTrail.ApplicationContracts.Commands.Cards;
using MindTrail.ApplicationContracts.Commands.Common;
using MindTrail.ApplicationContracts.Commands.Persons;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.ApplicationContracts.Interfaces;
using MindTrail.ApplicationContracts.Interfaces.Commands;
using MindTrail.ApplicationContracts.Interfaces.Queries;
using MindTrail.ApplicationContracts.Queries.Cards;
using MindTrail.ApplicationContracts.Queries.Countries;
using MindTrail.ApplicationContracts.Queries.Persons;
using MindTrail.EfCore.Handlers;

namespace MindTrail.ApplicationConfigurator.Configs.Components;

/// <summary>
/// Used to configure the component <see cref="MindTrail.ApplicationContracts"/>.
/// </summary>
public static class ApplicationContractsConfig
{
    /// <summary>
    /// Adds a configuration for application contracts.
    /// </summary>
    /// <param name="services">Used to register application services.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance, so that additional calls can be chained.</returns>
    public static IServiceCollection AddApplicationContractsConfig(this IServiceCollection services)
    {
        AddRequestSender(services);
        AddQueryHandlers(services);
        AddCommandHandlers(services);

        return services;
    }

    private static void AddRequestSender(IServiceCollection services)
    {
        services.AddScoped<IRequestSender, RequestSenderProvider>();
    }

    private static void AddQueryHandlers(IServiceCollection services)
    {
        services
            .AddScoped<IQueryHandler<GetCountriesQuery, PagedDto<CountryDto>>, GetCountriesQueryHandler>();

        services
            .AddScoped<IQueryHandler<GetPersonByIdQuery, PersonDto?>, GetPersonByIdQueryHandler>()
            .AddScoped<IQueryHandler<GetPersonsQuery, PagedDto<PersonDto>>, GetPersonsQueryHandler>();

        services
            .AddScoped<IQueryHandler<GetCardByNumberQuery, CardDto?>, GetCardByNumberQueryHandler>()
            .AddScoped<IQueryHandler<GetCardsQuery, PagedDto<CardDto>>, GetCardsQueryHandler>();
    }

    private static void AddCommandHandlers(IServiceCollection services)
    {
        services
            .AddScoped<ICommandHandler<UpdatePersonCommand, VoidResult>, UpdatePersonCommandHandler>()
            .AddScoped<ICommandHandler<DeletePersonCommand, VoidResult>, DeletePersonCommandHandler>()
            .AddScopedDecorated<
                ICommandHandler<CreatePersonCommand, Guid>,
                CreatePersonCommandHandler,
                CreatePersonCommandLogging>();

        services
            .AddScoped<ICommandHandler<UpdateCardCommand, VoidResult>, UpdateCardCommandHandler>()
            .AddScoped<ICommandHandler<DeleteCardCommand, VoidResult>, DeleteCardCommandHandler>()
            .AddScopedDecorated<
                ICommandHandler<CreateCardCommand, int>,
                CreateCardCommandHandler,
                CreateCardCommandLogging>();
    }
}
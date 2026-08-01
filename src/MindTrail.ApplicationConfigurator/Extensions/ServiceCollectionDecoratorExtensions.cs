using Microsoft.Extensions.DependencyInjection;
using MindTrail.ApplicationConfigurator.Logging;

namespace MindTrail.ApplicationConfigurator.Extensions;

/// <summary>
/// Provides extension methods for registering decorated services in the dependency injection container.
/// </summary>
public static class ServiceCollectionDecoratorExtensions
{
    /// <summary>
    /// Registers <typeparamref name="TImplementation"/> as a keyed "inner" service
    /// and <typeparamref name="TDecorator"/> as the public-facing decorator for <typeparamref name="TService"/>.
    /// </summary>
    /// <typeparam name="TService">Type of the service being decorated.</typeparam>
    /// <typeparam name="TImplementation">Type of the innermost, undecorated implementation of <typeparamref name="TService"/>.</typeparam>
    /// <typeparam name="TDecorator">Type of the decorator wrapping <typeparamref name="TImplementation"/>.</typeparam>
    /// <param name="services">Used to register the service and its decorator.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance, so that additional calls can be chained.</returns>
    public static IServiceCollection AddScopedDecorated<TService, TImplementation, TDecorator>(
        this IServiceCollection services)
        where TService : class
        where TImplementation : class, TService
        where TDecorator : class, TService
    {
        services.AddKeyedScoped<TService, TImplementation>(DecoratorKeys.Inner);
        services.AddScoped<TService, TDecorator>();

        return services;
    }
}
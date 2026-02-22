using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MindTrail.DomainServices.Exceptions.Base;
using MindTrail.HostConfiguration.Interfaces;
using MindTrail.WebApi.Handlers;
using MindTrail.WebApi.Interfaces.Handlers;
using MindTrail.WebApi.Interfaces.Providers;
using MindTrail.WebHost.Abstractions.Providers;
using MindTrail.WebHost.Configs.Common;
using MindTrail.WebHost.Providers;
using MindTrail.WebHost.Settings;

namespace MindTrail.WebHost.Configs.Components;

/// <summary>
/// Used to configure the component <see cref="MindTrail.WebApi"/>.
/// </summary>
internal static class WebApiConfig
{
    /// <summary>
    /// Adds a configuration for the web API (Swagger, health checks, HTTP logging, etc.).
    /// </summary>
    /// <param name="services">Used to register application services.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <param name="logger">The startup logger. Optional.</param>
    public static void AddWebApiConfig(
        this IServiceCollection services, IConfiguration configuration, IStartupLogger? logger = null)
    {
        AddProviders(services);
        AddHandlers(services);

        services.AddHealthChecks();
        services.AddProblemDetails();
        services.AddExceptionHandler<CustomExceptionHandler>();
        services.AddEndpointsApiExplorer();
        services.AddHttpLoggingConfig(configuration, logger);
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
        services.AddSwaggerConfig(new SwaggerSettings
        {
            AppTitle = "Mind Trail API",
            XmlFilesNames = ["MindTrail.WebApi"],
        });
    }

    private static void AddProviders(IServiceCollection services)
    {
        services.AddScoped<ITraceIdProvider, TraceIdProvider>();
        services.AddScoped<IHttpErrorResultProvider, HttpErrorResultProvider>();
    }

    private static void AddHandlers(IServiceCollection services)
    {
        services.AddScoped<IDomainExceptionHandler<DomainException>, SimpleExceptionHandler>();
        services.AddScoped<IDomainExceptionHandler<DomainException>, PersonDuplicateExceptionHandler>();
        services.AddScoped<IDomainExceptionHandler<DomainException>, PersonNameTooLongExceptionHandler>();
    }
}
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MindTrail.HostConfiguration.Interfaces;
using MindTrail.WebApi.Handlers;
using MindTrail.WebHost.Configs.Common;
using MindTrail.WebHost.Settings;

namespace MindTrail.WebHost.Configs.Components;

/// <summary>
/// Configuration of component <see cref="MindTrail.WebApi"/>.
/// </summary>
internal static class WebApiConfig
{
    /// <summary>
    /// Adds configuration for component <see cref="MindTrail.WebApi"/>.
    /// </summary>
    public static void AddWebApiConfig(this IServiceCollection services,
        IConfiguration configuration, IStartupLogger logger = null)
    {
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
            XmlFilesNames = ["MindTrail.WebApi"]
        });
    }
}
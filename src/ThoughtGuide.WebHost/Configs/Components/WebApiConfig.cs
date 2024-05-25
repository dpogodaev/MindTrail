using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ThoughtGuide.HostConfiguration.Interfaces;
using ThoughtGuide.WebApi.Handlers;
using ThoughtGuide.WebHost.Configs.Common;
using ThoughtGuide.WebHost.Settings;

namespace ThoughtGuide.WebHost.Configs.Components;

/// <summary>
/// Configuration of component <see cref="ThoughtGuide.WebApi"/>.
/// </summary>
internal static class WebApiConfig
{
    /// <summary>
    /// Adds configuration for component <see cref="ThoughtGuide.WebApi"/>.
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
            AppTitle = "Thought Guide API",
            XmlFilesNames = ["ThoughtGuide.WebApi"]
        });
    }
}
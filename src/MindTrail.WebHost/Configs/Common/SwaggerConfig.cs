using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using MindTrail.WebApi.Filters;
using MindTrail.WebAuth.Constants;
using MindTrail.WebHost.Settings;
using System.Text.Json;

namespace MindTrail.WebHost.Configs.Common;

/// <summary>
/// Swagger configuration.
/// </summary>
internal static class SwaggerConfig
{
    private static SwaggerSettings _swaggerSettings;

    /// <summary>
    /// Adds a configuration for Swagger.
    /// </summary>
    /// <param name="services">Used to register application services.</param>
    /// <param name="settings">Swagger settings.</param>
    public static void AddSwaggerConfig(this IServiceCollection services, SwaggerSettings settings)
    {
        _swaggerSettings = settings;

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(settings.DocumentName, new OpenApiInfo
            {
                Title = settings.AppTitle,
                Version = settings.DocumentVersion
            });

            options.UseAllOfToExtendReferenceSchemas();
            options.OperationFilter<ResponseHeadersOperationFilter>();
            options.OperationFilter<SecurityRequirementOperationFilter>();
            options.AddSecurityDefinitionForApiKey();

            settings.XmlFilesNames.ForEach(filePath =>
                options.IncludeXmlComments(GetXmlFilePath(filePath), true));
        });
    }

    /// <summary>
    /// Configures the Swagger user interface.
    /// </summary>
    /// <param name="options">Swagger UI options.</param>
    public static void ConfigureSwaggerUI(SwaggerUIOptions options)
    {
        ArgumentNullException.ThrowIfNull(_swaggerSettings, "The Swagger is not configured");

        var url = $"/swagger/{_swaggerSettings.DocumentName}/swagger.json";
        var name = $"{_swaggerSettings.AppTitle} {_swaggerSettings.DocumentVersion}";

        options.SwaggerEndpoint(url, name);
        options.DocExpansion(_swaggerSettings.ExpansionType);
        options.DefaultModelsExpandDepth(_swaggerSettings.DefaultModelsExpandDepth);
    }

    #region Private methods

    private static void AddSecurityDefinitionForApiKey(this SwaggerGenOptions options)
    {
        options.AddSecurityDefinition(ApiKeyConstants.ApiKeySchemeName, new OpenApiSecurityScheme
            {
                Name = ApiKeyConstants.ApiKeyHeaderName,
                Scheme = ApiKeyConstants.ApiKeySchemeName,
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header
            }
        );
    }

    private static string GetXmlFilePath(string componentName)
    {
        return $"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, componentName)}.xml";
    }

    #endregion
}
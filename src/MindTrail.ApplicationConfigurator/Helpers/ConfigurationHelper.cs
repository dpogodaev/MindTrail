using System;
using Microsoft.Extensions.Configuration;

namespace MindTrail.ApplicationConfigurator.Helpers;

/// <summary>
/// Application configuration helper.
/// </summary>
public static class ConfigurationHelper
{
    /// <summary>
    /// Determines the runtime environment.
    /// </summary>
    /// <param name="isWebHost">Indicates whether it is a web host.</param>
    /// <returns>
    /// The name of the runtime environment, or <c>null</c> if the corresponding environment variable is not set.
    /// </returns>
    /// <remarks>
    /// ASP.NET Core uses an environment variable called 'ASPNETCORE_ENVIRONMENT' to identify the runtime environment.
    /// Default host uses the environment variables with prefixed with 'DOTNET_'.
    /// </remarks>
    public static string? DetermineRuntimeEnvironment(bool isWebHost)
    {
        return isWebHost
            ? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            : Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
    }

    /// <summary>
    /// Builds an application configuration based on 'appsettings.json' files and environment variables.
    /// Used when the application builder is not yet available, for example, when launching the application.
    /// </summary>
    /// <param name="runtimeEnvironment">The name of the runtime environment.</param>
    /// <returns>The application configuration.</returns>
    /// <exception cref="System.IO.FileNotFoundException">The file appsettings.json was not found.</exception>
    public static IConfiguration BuildAppConfiguration(string? runtimeEnvironment)
    {
        var configurationBuilder = new ConfigurationBuilder();

        configurationBuilder
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        if (!string.IsNullOrEmpty(runtimeEnvironment))
        {
            configurationBuilder
                .AddJsonFile($"appsettings.{runtimeEnvironment}.json", optional: true, reloadOnChange: true);
        }

        configurationBuilder.AddEnvironmentVariables();

        return configurationBuilder.Build();
    }
}
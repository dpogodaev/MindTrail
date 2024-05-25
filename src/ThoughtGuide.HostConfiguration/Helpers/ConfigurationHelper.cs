using System;
using Microsoft.Extensions.Configuration;

namespace ThoughtGuide.HostConfiguration.Helpers;

/// <summary>
/// Application configuration helper.
/// </summary>
public static class ConfigurationHelper
{
    /// <summary>
    /// Determines the runtime environment.
    /// </summary>
    /// <param name="isWebHost">Indicates if it is a web host.</param>
    /// <returns>The name of the runtime environment.</returns>
    /// <remarks>
    /// ASP.NET Core uses an environment variable called 'ASPNETCORE_ENVIRONMENT' to identify the runtime environment.
    /// Default host uses the environment variables with prefixed with 'DOTNET_'.
    /// </remarks>
    public static string DetermineRuntimeEnvironment(bool isWebHost)
    {
        return isWebHost
            ? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            : Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
    }

    /// <summary>
    /// Builds an application configuration based on 'appsettings.json' files and environment variables.
    /// Used when the application builder is not yet available, for example, when launching the application.
    /// </summary>
    /// <param name="runtimeEnvironment">Name of the runtime environment.</param>
    /// <returns>The application configuration.</returns>
    /// <exception cref="Exception">The file "appsettings.json" was not found.</exception>
    public static IConfiguration BuildAppConfiguration(string runtimeEnvironment)
    {
        return new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{runtimeEnvironment}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
    }
}
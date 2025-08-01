using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MindTrail.EfCoreMssql.Context;
using MindTrail.HostConfiguration.Extensions;
using MindTrail.HostConfiguration.Interfaces;
using MindTrail.HostConfiguration.Settings;
using MssqlDbContext = MindTrail.EfCoreMssql.Context.AppDbContext;
using PostgreSqlContext = MindTrail.EfCorePostgreSql.Context.AppDbContext;

namespace MindTrail.HostConfiguration.Configs.Components;

/// <summary>
/// Used to configure the component <see cref="MindTrail.EfCore"/>.
/// </summary>
public static class EfCoreConfig
{
    public const string EfCoreConfigSection = "EfCore";

    private const string SqlServerProviderName = "sqlserver";
    private const string PostgreSqlProviderName = "postgresql";

    /// <summary>
    /// Adds a configuration for EF Core and the specified database provider.
    /// </summary>
    /// <param name="services">Used to register application services.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <param name="logger">The startup logger. Optional.</param>
    public static void AddEfCoreConfig(this IServiceCollection services,
        IConfiguration configuration, IStartupLogger logger = null)
    {
        AddRepositories(services);
        AddDatabaseProvider(services, configuration, logger);
    }

    /// <summary>
    /// Applies any pending migrations for the context to the PostgreSql database.
    /// Will create the database if it does not already exist.
    /// </summary>
    /// <param name="host">The application host.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <param name="logger">The startup logger. Optional.</param>
    /// <exception cref="Exception">The <see cref="AppDbContext"/> is not configured.</exception>
    public static async Task ApplyMigrationAsync(
        this IHost host, IConfiguration configuration, IStartupLogger logger = null)
    {
        var databaseSettings = configuration.BindSection<EfCoreSettings>(EfCoreConfigSection);

        switch (databaseSettings.DatabaseProvider.ToLower())
        {
            case SqlServerProviderName:
                await host.ApplyMssqlMigrationAsync<MssqlDbContext>(logger);
                break;
            case PostgreSqlProviderName:
                await host.ApplyPostgreSqlMigrationAsync<PostgreSqlContext>(logger);
                break;
            default:
                HandleUnsupportedDatabaseProvider(databaseSettings);
                break;
        }
    }

    #region Private methods

    private static void AddRepositories(IServiceCollection services)
    {
    }

    private static void AddDatabaseProvider(this IServiceCollection services,
        IConfiguration configuration, IStartupLogger logger = null)
    {
        var databaseSettings = configuration.BindSection<EfCoreSettings>(EfCoreConfigSection);

        switch (databaseSettings.DatabaseProvider.ToLower())
        {
            case SqlServerProviderName:
                services.AddEfCoreMssqlConfig<MssqlDbContext>(configuration, logger);
                break;
            case PostgreSqlProviderName:
                services.AddEfCorePostgreSqlConfig<PostgreSqlContext>(configuration, logger);
                break;
            default:
                HandleUnsupportedDatabaseProvider(databaseSettings);
                break;
        }
    }

    private static void HandleUnsupportedDatabaseProvider(EfCoreSettings databaseSettings)
    {
        throw new InvalidOperationException(
            $"Unsupported database provider: '{databaseSettings.DatabaseProvider}'. " +
            $"Supported providers are: '{SqlServerProviderName}', '{PostgreSqlProviderName}'.");
    }

    #endregion
}
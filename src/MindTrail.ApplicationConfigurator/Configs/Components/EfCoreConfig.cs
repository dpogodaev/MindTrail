using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MindTrail.Application.Abstractions.Repositories;
using MindTrail.ApplicationConfigurator.Extensions;
using MindTrail.ApplicationConfigurator.Interfaces.Logging;
using MindTrail.ApplicationConfigurator.Settings;
using MindTrail.EfCore.Repositories;
using MindTrail.EfCoreMssql.Context;
using MindTrail.EfCorePostgreSql.Context;

namespace MindTrail.ApplicationConfigurator.Configs.Components;

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
    /// <returns>The same <see cref="IServiceCollection"/> instance, so that additional calls can be chained.</returns>
    public static IServiceCollection AddEfCoreConfig(
        this IServiceCollection services,
        IConfiguration configuration,
        IStartupLogger? logger = null)
    {
        services.AddDatabaseProvider(configuration, logger);

        return services;
    }

    /// <summary>
    /// Applies any pending migrations for the context to the PostgreSql database.
    /// Will create the database if it does not already exist.
    /// </summary>
    /// <param name="host">The application host.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <param name="logger">The startup logger. Optional.</param>
    /// <exception cref="Exception">The <see cref="MssqlDbContext"/> is not configured.</exception>
    /// <exception cref="Exception">The <see cref="PostgreSqlDbContext"/> is not configured.</exception>
    public static async Task ApplyMigrationAsync(
        this IHost host,
        IConfiguration configuration,
        IStartupLogger? logger = null)
    {
        var databaseSettings = configuration.BindSection<EfCoreSettings>(EfCoreConfigSection);

        if (databaseSettings == null)
        {
            throw new InvalidOperationException(
                $"The configuration section '{EfCoreConfigSection}' is not specified.");
        }

        switch (databaseSettings.DatabaseProvider.ToLower())
        {
            case SqlServerProviderName:
                await host.ApplyMssqlMigrationAsync<MssqlDbContext>(logger);
                break;
            case PostgreSqlProviderName:
                await host.ApplyPostgreSqlMigrationAsync<PostgreSqlDbContext>(logger);
                break;
            default:
                HandleUnsupportedDatabaseProvider(databaseSettings);
                break;
        }
    }

    private static void AddDatabaseProvider(
        this IServiceCollection services,
        IConfiguration configuration,
        IStartupLogger? logger = null)
    {
        var databaseSettings = configuration.BindSection<EfCoreSettings>(EfCoreConfigSection);

        if (databaseSettings == null)
        {
            throw new InvalidOperationException(
                $"The configuration section '{EfCoreConfigSection}' is not specified.");
        }

        switch (databaseSettings.DatabaseProvider.ToLower())
        {
            case SqlServerProviderName:
                services.AddEfCoreMssqlConfig<MssqlDbContext>(configuration, logger);
                services.AddScoped<IUnitOfWork, UnitOfWork<MssqlDbContext>>(sp =>
                    new UnitOfWork<MssqlDbContext>(sp.GetRequiredService<MssqlDbContext>()));
                break;
            case PostgreSqlProviderName:
                services.AddEfCorePostgreSqlConfig<PostgreSqlDbContext>(configuration, logger);
                services.AddScoped<IUnitOfWork, UnitOfWork<PostgreSqlDbContext>>(sp =>
                    new UnitOfWork<PostgreSqlDbContext>(sp.GetRequiredService<PostgreSqlDbContext>()));
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
}
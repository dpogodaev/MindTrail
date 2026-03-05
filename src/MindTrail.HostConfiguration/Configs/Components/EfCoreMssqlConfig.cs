using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MindTrail.EfCore.Context;
using MindTrail.HostConfiguration.Extensions;
using MindTrail.HostConfiguration.Interfaces.Logging;
using MindTrail.HostConfiguration.Settings;

namespace MindTrail.HostConfiguration.Configs.Components;

/// <summary>
/// Used to configure the component <see cref="MindTrail.EfCoreMssql"/>.
/// </summary>
public static class EfCoreMssqlConfig
{
    private const string ConnectionString = "DefaultConnection";

    /// <summary>
    /// Adds a configuration of EF Core database provider for SQL Server.
    /// </summary>
    /// <param name="services">Used to register application services.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <param name="logger">The startup logger. Optional.</param>
    /// <typeparam name="TContext">The type of the database context.</typeparam>
    public static void AddEfCoreMssqlConfig<TContext>(
        this IServiceCollection services,
        IConfiguration configuration,
        IStartupLogger? logger = null)
        where TContext : AppDbContext
    {
        var connectionString = configuration.GetConnectionString(ConnectionString);

        if (string.IsNullOrEmpty(connectionString))
        {
            logger?.Warn($"Connection string '{ConnectionString}' for the SQL Server database is not specified");
            return;
        }

        var settings = configuration.BindSection<EfCoreSettings>(EfCoreConfig.EfCoreConfigSection);

        services.AddDbContext<AppDbContext, TContext>(options =>
        {
            options.UseSqlServer(connectionString);

            if (settings?.EnableSensitiveDataLogging == true)
            {
                options.EnableSensitiveDataLogging();
            }
        });
    }

    /// <summary>
    /// Applies any pending migrations for the context to the SQL Server database.<br/>
    /// Will create the database if it does not already exist.
    /// </summary>
    /// <param name="host">The application host.</param>
    /// <param name="logger">The startup logger. Optional.</param>
    /// <typeparam name="TContext">The type of the database context.</typeparam>
    /// <exception cref="Exception">Thrown when <see cref="DbContext"/> is not configured.</exception>
    public static async Task ApplyMssqlMigrationAsync<TContext>(
        this IHost host,
        IStartupLogger? logger = null)
        where TContext : DbContext
    {
        using var scope = host.Services.CreateScope();

        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        if (string.IsNullOrEmpty(configuration.GetConnectionString(ConnectionString)))
        {
            return;
        }

        try
        {
            var dbContext = scope.ServiceProvider.GetService<TContext>();
            if (dbContext is null)
            {
                const string msg = $"The {nameof(TContext)} is not configured";
                logger?.Error(msg);
                throw new Exception(msg);
            }

            var settings = configuration.BindSection<EfCoreSettings>(EfCoreConfig.EfCoreConfigSection);
            if (settings?.ApplyMigrationsAutomatically == true)
            {
                var pendingMigrations = (await dbContext.Database.GetPendingMigrationsAsync()).ToArray();
                if (pendingMigrations.Length == 0)
                {
                    logger?.Info("No pending migrations were found for the SQL Server database");
                    return;
                }

                await dbContext.Database.MigrateAsync();

                logger?.Info(
                    "The following pending migrations to the SQL Server database were applied: " +
                    $"{string.Join(", ", pendingMigrations)}");
            }
        }
        catch (Exception e)
        {
            logger?.Error("An error occurred while migrating the SQL Server database", e);
            throw;
        }
    }
}
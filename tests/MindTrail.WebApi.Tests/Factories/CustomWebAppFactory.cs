using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MindTrail.Common.Interfaces.Providers;
using MindTrail.EfCoreMssql.Context;

namespace MindTrail.WebApi.Tests.Factories;

public class CustomWebAppFactory<TProgram>(
    Dictionary<string, string>? redefinedConfiguration = null,
    ICurrentTimeProvider? currentTimeProvider = null)
    : WebApplicationFactory<TProgram>
    where TProgram : class
{
    private readonly Dictionary<string, string> _redefinedConfiguration =
        redefinedConfiguration ?? new Dictionary<string, string>();

    private SqliteConnection? _connection;
    private IServiceScopeFactory? _scopeFactory;

    public int DefaultTenantId => 1;

    public void ResetDatabase()
    {
        using var scope = _scopeFactory!.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<MssqlDbContext>();

        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
    }

    /// <inheritdoc />
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((_, configBuilder) =>
        {
            DisableAutoMigration(_redefinedConfiguration);
            DisableOpenTelemetry(_redefinedConfiguration);

            configBuilder.AddInMemoryCollection(_redefinedConfiguration!);
        });

        builder.ConfigureServices(services =>
        {
            OverrideDbContext(services);
            OverrideCurrentTimeProvider(services);
            InitializeInMemoryDatabase(services);
        });
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _connection?.Close();
            _connection?.Dispose();
        }

        base.Dispose(disposing);
    }

    private static void DisableAutoMigration(Dictionary<string, string> configuration)
    {
        configuration["EfCore:ApplyMigrationsAutomatically"] = "false";
    }

    private static void DisableOpenTelemetry(Dictionary<string, string> configuration)
    {
        configuration["OpenTelemetry:Logs:Enable"] = "false";
        configuration["OpenTelemetry:Tracing:Enable"] = "false";
        configuration["OpenTelemetry:Metrics:Enable"] = "false";
    }

    private void OverrideDbContext(IServiceCollection services)
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var descriptor = services.SingleOrDefault(x => x.ServiceType == typeof(DbContextOptions<MssqlDbContext>));
        if (descriptor != null)
        {
            services.Remove(descriptor);
        }

        services.AddDbContext<MssqlDbContext>(options => { options.UseSqlite(_connection); });
    }

    private void OverrideCurrentTimeProvider(IServiceCollection services)
    {
        if (currentTimeProvider == null)
        {
            return;
        }

        var descriptor = services.FirstOrDefault(x => x.ServiceType == typeof(ICurrentTimeProvider));

        if (descriptor != null)
        {
            services.Remove(descriptor);
        }

        services.AddSingleton(currentTimeProvider);
    }

    private void InitializeInMemoryDatabase(IServiceCollection services)
    {
        var sp = services.BuildServiceProvider();
        _scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();

        using var scope = _scopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<MssqlDbContext>();

        db.Database.EnsureCreated();
    }
}
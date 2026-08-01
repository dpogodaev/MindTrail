using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MindTrail.Common.Interfaces.Providers;
using MindTrail.EfCoreMssql.Context;

namespace MindTrail.WebApi.Tests.Factories;

public class CustomWebAppFactory<TProgram>(ICurrentTimeProvider? currentTimeProvider = null)
    : WebApplicationFactory<TProgram>
    where TProgram : class
{
    private SqliteConnection? _connection;

    public void ResetDatabase()
    {
        using var scope = Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<MssqlDbContext>();

        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            OverrideCurrentTimeProvider(services);
            OverrideDbContext(services);
        });
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        var host = base.CreateHost(builder);

        using var scope = host.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<MssqlDbContext>();
        db.Database.EnsureCreated();

        return host;
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

    private void OverrideDbContext(IServiceCollection services)
    {
        var descriptorsToRemove = services
            .Where(x =>
                x.ServiceType.FullName != null &&
                (x.ServiceType.FullName.Contains("Microsoft.EntityFrameworkCore") ||
                 x.ServiceType.FullName.Contains("SqlServer")))
            .ToList();

        foreach (var descriptor in descriptorsToRemove)
        {
            services.Remove(descriptor);
        }

        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        services.AddDbContext<MssqlDbContext>(options => options.UseSqlite(_connection));
    }
}
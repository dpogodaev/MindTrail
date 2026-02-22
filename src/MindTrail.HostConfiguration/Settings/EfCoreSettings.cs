namespace MindTrail.HostConfiguration.Settings;

/// <summary>
/// Entity Framework Core settings.
/// </summary>
public record EfCoreSettings
{
    /// <summary>
    /// Database provider ("SQLServer" or "PostgreSQL"). Case-insensitive.
    /// </summary>
    public required string DatabaseProvider { get; init; }

    /// <summary>
    /// Indicates if migration should be applied to databases automatically.
    /// </summary>
    public bool ApplyMigrationsAutomatically { get; init; }

    /// <summary>
    /// Tells EF Core to include the parameter values of SQL query in its logging messages.
    /// </summary>
    public bool EnableSensitiveDataLogging { get; init; }
}
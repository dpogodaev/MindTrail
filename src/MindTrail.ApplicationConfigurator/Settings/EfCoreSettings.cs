namespace MindTrail.ApplicationConfigurator.Settings;

/// <summary>
/// Entity Framework Core settings.
/// </summary>
public record EfCoreSettings
{
    /// <summary>
    /// The database provider ("SQLServer" or "PostgreSQL"). Case-insensitive.
    /// </summary>
    public required string DatabaseProvider { get; init; }

    /// <summary>
    /// Whether migrations should be applied to databases automatically.
    /// </summary>
    public bool ApplyMigrationsAutomatically { get; init; }

    /// <summary>
    /// Whether EF Core should include the parameter values of SQL queries in its logging messages.
    /// </summary>
    public bool EnableSensitiveDataLogging { get; init; }
}
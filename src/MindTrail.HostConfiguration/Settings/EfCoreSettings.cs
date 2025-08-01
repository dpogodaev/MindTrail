namespace MindTrail.HostConfiguration.Settings;

/// <summary>
/// Entity Framework Core settings.
/// </summary>
public class EfCoreSettings
{
    /// <summary>
    /// Database provider ("SQLServer" or "PostgreSQL"). Case-insensitive.
    /// </summary>
    public string DatabaseProvider { get; set; }

    /// <summary>
    /// Indicates if migration should be applied to databases automatically.
    /// </summary>
    public bool ApplyMigrationsAutomatically { get; init; }

    /// <summary>
    /// Tells EF Core to include the parameter values of SQL query in its logging messages.
    /// </summary>
    public bool EnableSensitiveDataLogging { get; init; }
}
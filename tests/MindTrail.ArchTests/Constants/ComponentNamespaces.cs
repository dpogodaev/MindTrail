namespace MindTrail.ArchTests.Constants;

/// <summary>
/// Constants for component names.
/// </summary>
public record ComponentNamespaces
{
    private const string Solution = nameof(MindTrail);

    public const string Common = $"{Solution}.{nameof(MindTrail.Common)}";
    public const string DomainEntities = $"{Solution}.{nameof(MindTrail.DomainEntities)}";
    public const string DomainServices = $"{Solution}.{nameof(MindTrail.DomainServices)}";
    public const string AppServices = $"{Solution}.{nameof(MindTrail.AppServices)}";
    public const string EfCore = $"{Solution}.{nameof(MindTrail.EfCore)}";
    public const string EfCoreMssql = $"{Solution}.{nameof(MindTrail.EfCoreMssql)}";
    public const string EfCorePostgreSql = $"{Solution}.{nameof(MindTrail.EfCorePostgreSql)}";
    public const string HostConfiguration = $"{Solution}.{nameof(MindTrail.HostConfiguration)}";
    public const string WebHost = $"{Solution}.{nameof(MindTrail.WebHost)}";
    public const string WebApi = $"{Solution}.{nameof(MindTrail.WebApi)}";
    public const string WebAuth = $"{Solution}.{nameof(MindTrail.WebAuth)}";
    public const string CliHost = $"{Solution}.{nameof(MindTrail.CliHost)}";
    public const string Cli = $"{Solution}.{nameof(MindTrail.Cli)}";
}
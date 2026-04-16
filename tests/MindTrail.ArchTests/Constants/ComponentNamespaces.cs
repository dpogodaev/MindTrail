namespace MindTrail.ArchTests.Constants;

/// <summary>
/// Constants for component names.
/// </summary>
public record ComponentNamespaces
{
    public const string Solution = nameof(MindTrail);
    public const string Common = $"{Solution}.{nameof(MindTrail.Common)}";
    public const string Domain = $"{Solution}.{nameof(MindTrail.Domain)}";
    public const string DomainShared = $"{Solution}.{nameof(MindTrail.DomainShared)}";
    public const string Application = $"{Solution}.{nameof(MindTrail.Application)}";
    public const string ApplicationContracts = $"{Solution}.{nameof(MindTrail.ApplicationContracts)}";
    public const string EfCore = $"{Solution}.{nameof(MindTrail.EfCore)}";
    public const string EfCoreMssql = $"{Solution}.{nameof(MindTrail.EfCoreMssql)}";
    public const string EfCorePostgreSql = $"{Solution}.{nameof(MindTrail.EfCorePostgreSql)}";
    public const string ApplicationConfigurator = $"{Solution}.{nameof(MindTrail.ApplicationConfigurator)}";
    public const string WebHost = $"{Solution}.{nameof(MindTrail.WebHost)}";
    public const string WebApi = $"{Solution}.{nameof(MindTrail.WebApi)}";
    public const string WebAuth = $"{Solution}.{nameof(MindTrail.WebAuth)}";
    public const string CliHost = $"{Solution}.{nameof(MindTrail.CliHost)}";
    public const string Cli = $"{Solution}.{nameof(MindTrail.Cli)}";
}
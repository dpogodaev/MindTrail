namespace MindTrail.ArchTests.Constants;

/// <summary>
/// Constants for component names.
/// </summary>
public record ComponentNamespaces
{
    private const string Solution = nameof(MindTrail);

    public const string Common = $"{Solution}.{nameof(MindTrail.Common)}";
    public const string HostConfiguration = $"{Solution}.{nameof(MindTrail.HostConfiguration)}";
    public const string WebHost = $"{Solution}.{nameof(MindTrail.WebHost)}";
    public const string WebApi = $"{Solution}.{nameof(MindTrail.WebApi)}";
    public const string WebAuth = $"{Solution}.{nameof(MindTrail.WebAuth)}";
    public const string CliHost = $"{Solution}.{nameof(MindTrail.CliHost)}";
    public const string Cli = $"{Solution}.{nameof(MindTrail.Cli)}";
}
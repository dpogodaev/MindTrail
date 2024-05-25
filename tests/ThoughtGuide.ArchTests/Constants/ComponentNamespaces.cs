namespace ThoughtGuide.ArchTests.Constants;

/// <summary>
/// Constants for component names.
/// </summary>
public record ComponentNamespaces
{
    private const string Solution = nameof(ThoughtGuide);

    public const string Common = $"{Solution}.{nameof(ThoughtGuide.Common)}";
    public const string HostConfiguration = $"{Solution}.{nameof(ThoughtGuide.HostConfiguration)}";
    public const string WebHost = $"{Solution}.{nameof(ThoughtGuide.WebHost)}";
    public const string WebApi = $"{Solution}.{nameof(ThoughtGuide.WebApi)}";
    public const string WebAuth = $"{Solution}.{nameof(ThoughtGuide.WebAuth)}";
    public const string CliHost = $"{Solution}.{nameof(ThoughtGuide.CliHost)}";
    public const string Cli = $"{Solution}.{nameof(ThoughtGuide.Cli)}";
}
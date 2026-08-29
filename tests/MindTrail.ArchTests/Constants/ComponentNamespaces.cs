namespace MindTrail.ArchTests.Constants;

/// <summary>
/// Constants for component names.
/// </summary>
public record ComponentNamespaces
{
    /// <summary>
    /// The name of the solution's root namespace.
    /// </summary>
    public const string Solution = nameof(MindTrail);

    /// <summary>
    /// The namespace of the <see cref="MindTrail.Common"/> component.
    /// </summary>
    public const string Common = $"{Solution}.{nameof(MindTrail.Common)}";

    /// <summary>
    /// The namespace of the <see cref="MindTrail.Domain"/> component.
    /// </summary>
    public const string Domain = $"{Solution}.{nameof(MindTrail.Domain)}";

    /// <summary>
    /// The namespace of the <see cref="MindTrail.DomainShared"/> component.
    /// </summary>
    public const string DomainShared = $"{Solution}.{nameof(MindTrail.DomainShared)}";

    /// <summary>
    /// The namespace of the <see cref="MindTrail.Application"/> component.
    /// </summary>
    public const string Application = $"{Solution}.{nameof(MindTrail.Application)}";

    /// <summary>
    /// The namespace of the <see cref="MindTrail.ApplicationContracts"/> component.
    /// </summary>
    public const string ApplicationContracts = $"{Solution}.{nameof(MindTrail.ApplicationContracts)}";

    /// <summary>
    /// The namespace of the <see cref="MindTrail.EfCore"/> component.
    /// </summary>
    public const string EfCore = $"{Solution}.{nameof(MindTrail.EfCore)}";

    /// <summary>
    /// The namespace of the <see cref="MindTrail.EfCoreMssql"/> component.
    /// </summary>
    public const string EfCoreMssql = $"{Solution}.{nameof(MindTrail.EfCoreMssql)}";

    /// <summary>
    /// The namespace of the <see cref="MindTrail.EfCorePostgreSql"/> component.
    /// </summary>
    public const string EfCorePostgreSql = $"{Solution}.{nameof(MindTrail.EfCorePostgreSql)}";

    /// <summary>
    /// The namespace of the <see cref="MindTrail.ApplicationConfigurator"/> component.
    /// </summary>
    public const string ApplicationConfigurator = $"{Solution}.{nameof(MindTrail.ApplicationConfigurator)}";

    /// <summary>
    /// The namespace of the <see cref="MindTrail.WebHost"/> component.
    /// </summary>
    public const string WebHost = $"{Solution}.{nameof(MindTrail.WebHost)}";

    /// <summary>
    /// The namespace of the <see cref="MindTrail.WebApi"/> component.
    /// </summary>
    public const string WebApi = $"{Solution}.{nameof(MindTrail.WebApi)}";

    /// <summary>
    /// The namespace of the <see cref="MindTrail.WebAuth"/> component.
    /// </summary>
    public const string WebAuth = $"{Solution}.{nameof(MindTrail.WebAuth)}";

    /// <summary>
    /// The namespace of the <see cref="MindTrail.CliHost"/> component.
    /// </summary>
    public const string CliHost = $"{Solution}.{nameof(MindTrail.CliHost)}";

    /// <summary>
    /// The namespace of the <see cref="MindTrail.Cli"/> component.
    /// </summary>
    public const string Cli = $"{Solution}.{nameof(MindTrail.Cli)}";
}
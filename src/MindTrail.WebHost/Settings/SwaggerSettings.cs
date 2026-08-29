using System.Collections.Generic;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace MindTrail.WebHost.Settings;

/// <summary>
/// Swagger settings.
/// </summary>
internal class SwaggerSettings
{
    /// <summary>
    /// Gets the URI-friendly name that uniquely identifies the document.
    /// The default value is <c>v1</c>.
    /// </summary>
    public string DocumentName { get; init; } = "v1";

    /// <summary>
    /// Gets the version of the OpenAPI document.
    /// The default value is <c>v1</c>.
    /// </summary>
    public string DocumentVersion { get; init; } = "v1";

    /// <summary>
    /// Gets the title of the application.
    /// </summary>
    public required string AppTitle { get; init; }

    /// <summary>
    /// Gets the default expansion depth for models (set to <c>-1</c> to completely hide the models).
    /// The default value is <c>-1</c>.
    /// </summary>
    public int DefaultModelsExpandDepth { get; init; } = -1;

    /// <summary>
    /// Gets the expansion setting for the operations and tags.
    /// It can be <c>List</c> (expands only the tags), <c>Full</c> (expands the tags and operations) or <c>None</c> (expands nothing).
    /// The default value is <c>None</c>.
    /// </summary>
    public DocExpansion ExpansionType { get; init; } = DocExpansion.None;

    /// <summary>
    /// Gets the names of XML files (component names).
    /// </summary>
    public List<string> XmlFilesNames { get; init; } = [];
}
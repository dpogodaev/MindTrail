using System.Collections.Generic;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace ThoughtGuide.WebHost.Settings;

/// <summary>
/// Swagger settings.
/// </summary>
internal class SwaggerSettings
{
    /// <summary>
    /// URI-friendly name that uniquely identifies the document.
    /// The default value is 'v1'.
    /// </summary>
    public string DocumentName { get; init; } = "v1";

    /// <summary>
    /// The version of the OpenAPI document.
    /// The default value is 'v1'.
    /// 
    /// </summary>
    public string DocumentVersion { get; init; } = "v1";

    /// <summary>
    /// Title of the application.
    /// </summary>
    public string AppTitle { get; init; }

    /// <summary>
    /// The default expansion depth for models (set to -1 completely hide the models).
    /// The default value is '-1'.
    /// </summary>
    public int DefaultModelsExpandDepth { get; init; } = -1;

    /// <summary>
    /// Expansion setting for the operations and tags.
    /// It can be 'List' (expands only the tags), 'Full' (expands the tags and operations) or 'None' (expands nothing).
    /// The default value is 'None'.
    /// </summary>
    public DocExpansion ExpansionType { get; init; } = DocExpansion.None;

    /// <summary>
    /// Names of XML files (component names).
    /// </summary>
    public List<string> XmlFilesNames { get; init; }
}
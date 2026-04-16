using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace MindTrail.WebAuth.Options;

/// <summary>
/// Authentication options for the API key.
/// </summary>
/// <remarks>It is applied when using the attribute <see cref="AuthorizeAttribute"/>.</remarks>
public class ApiKeyAuthNOptions : AuthenticationSchemeOptions
{
    /// <summary>
    /// Gets or sets the name of the API key header.
    /// </summary>
    public string? ApiKeyHeaderName { get; set; }

    /// <summary>
    /// Gets or sets the name of claim that will be added to the user after successful authentication using the API key.
    /// </summary>
    public string? ClaimName { get; set; }
}
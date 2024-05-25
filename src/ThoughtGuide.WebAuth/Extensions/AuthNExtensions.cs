using System;
using Microsoft.AspNetCore.Authentication;
using ThoughtGuide.WebAuth.Attributes;
using ThoughtGuide.WebAuth.Handlers;
using ThoughtGuide.WebAuth.Options;

namespace ThoughtGuide.WebAuth.Extensions;

/// <summary>
/// Authentication extensions.
/// </summary>
public static class AuthNExtensions
{
    /// <summary>
    /// Adds authentication using the API key.
    /// </summary>
    /// <param name="builder">The builder used to configure authentication.</param>
    /// <param name="authenticationScheme">Name of the authentication scheme.</param>
    /// <param name="configureOptions">Used to configure the scheme options.</param>
    /// <remarks>It is applied when using the attribute <see cref="ApiKeyRequiredAttribute"/>.</remarks>
    public static void AddApiKey(this AuthenticationBuilder builder,
        string authenticationScheme, Action<ApiKeyAuthNOptions> configureOptions)
    {
        builder.AddScheme<ApiKeyAuthNOptions, ApiKeyAuthNHandler>(authenticationScheme, configureOptions);
    }
}
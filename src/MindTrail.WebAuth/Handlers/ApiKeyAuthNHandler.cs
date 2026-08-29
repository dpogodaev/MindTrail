using System;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MindTrail.WebAuth.Extensions;
using MindTrail.WebAuth.Interfaces.Validators;
using MindTrail.WebAuth.Options;

namespace MindTrail.WebAuth.Handlers;

/// <summary>
/// Handles authentication using the API key.
/// </summary>
/// <remarks>It is applied when using the attribute <see cref="AuthorizeAttribute"/>.</remarks>
public class ApiKeyAuthNHandler(
    IOptionsMonitor<ApiKeyAuthNOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    IApiKeyValidator apiKeyValidator)
    : AuthenticationHandler<ApiKeyAuthNOptions>(options, logger, encoder)
{
    /// <summary>
    /// Handles custom authentication by API key.
    /// </summary>
    /// <returns>An <see cref="AuthenticateResult"/> indicating the result of the authentication attempt.</returns>
    /// <exception cref="InvalidOperationException">The <see cref="ApiKeyAuthNOptions.ClaimName"/> is not specified.</exception>
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var apiKey = Request.GetHeaderKeyValue(Options.ApiKeyHeaderName);

        if (apiKey is null)
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        if (!apiKeyValidator.IsValid(apiKey))
        {
            const string msg = "The API key value is not valid";

            Logger.LogWarning(msg);

            return Task.FromResult(AuthenticateResult.Fail(msg));
        }

        return Task.FromResult(AuthenticateResult.Success(
            new AuthenticationTicket(BuildApiKeyClaim(), Scheme.Name)));
    }

    private ClaimsPrincipal BuildApiKeyClaim()
    {
        if (Options.ClaimName is null)
        {
            throw new InvalidOperationException("The name of claim for API key is not specified.");
        }

        var claimsIdentity = new ClaimsIdentity(Scheme.Name);

        claimsIdentity.AddClaim(new Claim(
            type: Options.ClaimName,
            value: string.Empty));

        return new ClaimsPrincipal(claimsIdentity);
    }
}
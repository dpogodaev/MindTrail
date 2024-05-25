using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ThoughtGuide.WebAuth.Extensions;
using ThoughtGuide.WebAuth.Interfaces.Validators;
using ThoughtGuide.WebAuth.Options;

namespace ThoughtGuide.WebAuth.Handlers;

/// <summary>
/// Handler of authentication using the API key.
/// </summary>
/// <remarks>It is applied when using the attribute <see cref="AuthorizeAttribute"/>.</remarks>
public class ApiKeyAuthNHandler : AuthenticationHandler<ApiKeyAuthNOptions>
{
    private readonly IApiKeyValidator _apiKeyValidator;

    /// <summary>
    /// Initializes a new instance of <see cref="ApiKeyAuthNHandler"/> class.
    /// </summary>
    public ApiKeyAuthNHandler(
        IOptionsMonitor<ApiKeyAuthNOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IApiKeyValidator apiKeyValidator)
        : base(options, logger, encoder)
    {
        _apiKeyValidator = apiKeyValidator;
    }

    /// <summary>
    /// Handles custom authentication by API key.
    /// </summary>
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var apiKey = Request.GetHeaderKeyValue(Options.ApiKeyHeaderName);

        if (apiKey is null)
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        if (_apiKeyValidator.IsNotValid(apiKey))
        {
            const string msg = "The API key value is not valid";

            Logger.LogWarning(msg);

            return Task.FromResult(AuthenticateResult.Fail(msg));
        }

        return Task.FromResult(AuthenticateResult.Success(
            new AuthenticationTicket(BuildApiKeyClaim(), Scheme.Name)));
    }

    #region Private methods

    private ClaimsPrincipal BuildApiKeyClaim()
    {
        var claimsIdentity = new ClaimsIdentity(Scheme.Name);

        claimsIdentity.AddClaim(new Claim(
            type: Options.ClaimName,
            value: string.Empty));

        return new ClaimsPrincipal(claimsIdentity);
    }

    #endregion
}
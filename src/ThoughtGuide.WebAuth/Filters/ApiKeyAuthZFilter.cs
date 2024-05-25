using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ThoughtGuide.WebAuth.Attributes;
using ThoughtGuide.WebAuth.Extensions;
using ThoughtGuide.WebAuth.Interfaces.Validators;
using ThoughtGuide.WebAuth.Settings;

namespace ThoughtGuide.WebAuth.Filters;

/// <summary>
/// Filter for authorization by API key.
/// The key must be sent in the request header.
/// </summary>
/// <remarks>It is applied when using the attribute <see cref="ApiKeyRequiredAttribute"/>.</remarks>
public class ApiKeyAuthZFilter : IAuthorizationFilter
{
    private readonly ApiKeySettings _settings;
    private readonly IApiKeyValidator _validator;

    /// <summary>
    /// Initializes a new instance of <see cref="ApiKeyAuthZFilter"/> class.
    /// </summary>
    /// <param name="settings">API key settings.</param>
    /// <param name="validator">Used to validate API key.</param>
    public ApiKeyAuthZFilter(
        ApiKeySettings settings,
        IApiKeyValidator validator)
    {
        _settings = settings;
        _validator = validator;
    }

    #region IAuthorizationFilter

    /// <inheritdoc cref="IAuthorizationFilter.OnAuthorization"/>
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var apiKey = context.HttpContext.Request.GetHeaderKeyValue(_settings.HeaderName);

        if (string.IsNullOrEmpty(apiKey))
        {
            SetStatusTo401(context, "API key is not provided");
            return;
        }

        if (!_validator.IsValid(apiKey))
        {
            SetStatusTo401(context, "API key is not valid");
        }
    }

    #endregion

    #region Private methods

    private static void SetStatusTo401(AuthorizationFilterContext context, string msg)
    {
        context.Result = new ContentResult
        {
            StatusCode = StatusCodes.Status401Unauthorized,
            Content = msg
        };
    }

    #endregion
}
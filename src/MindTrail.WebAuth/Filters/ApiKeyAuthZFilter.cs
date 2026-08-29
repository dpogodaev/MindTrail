using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MindTrail.WebAuth.Attributes;
using MindTrail.WebAuth.Extensions;
using MindTrail.WebAuth.Interfaces.Validators;
using MindTrail.WebAuth.Settings;

namespace MindTrail.WebAuth.Filters;

/// <summary>
/// Filter for authorization by API key.
/// The key must be sent in the request header.
/// </summary>
/// <param name="settings">The API key settings.</param>
/// <param name="validator">The validator used to validate the API key.</param>
/// <remarks>It is applied when using the attribute <see cref="ApiKeyRequiredAttribute"/>.</remarks>
public class ApiKeyAuthZFilter(
    ApiKeySettings settings,
    IApiKeyValidator validator)
    : IAuthorizationFilter
{
    /// <inheritdoc/>
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var apiKey = context.HttpContext.Request.GetHeaderKeyValue(settings.HeaderName);

        if (string.IsNullOrEmpty(apiKey))
        {
            SetStatusTo401(context, "API key is not provided");
            return;
        }

        if (!validator.IsValid(apiKey))
        {
            SetStatusTo401(context, "API key is not valid");
        }
    }

    private static void SetStatusTo401(AuthorizationFilterContext context, string msg)
    {
        context.Result = new ContentResult
        {
            StatusCode = StatusCodes.Status401Unauthorized,
            Content = msg,
        };
    }
}
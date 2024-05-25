using Microsoft.AspNetCore.Mvc;
using ThoughtGuide.WebAuth.Filters;

namespace ThoughtGuide.WebAuth.Attributes;

/// <summary>
/// Attribute for authorization by API key.
/// </summary>
public class ApiKeyRequiredAttribute : ServiceFilterAttribute
{
    /// <summary>
    /// Initializes a new instance of <see cref="ApiKeyRequiredAttribute"/> class.
    /// </summary>
    public ApiKeyRequiredAttribute() : base(typeof(ApiKeyAuthZFilter))
    {
    }
}
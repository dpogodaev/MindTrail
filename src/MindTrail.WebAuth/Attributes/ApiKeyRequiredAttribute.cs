using Microsoft.AspNetCore.Mvc;
using MindTrail.WebAuth.Filters;

namespace MindTrail.WebAuth.Attributes;

/// <summary>
/// Attribute for authorization by API key.
/// </summary>
public class ApiKeyRequiredAttribute() : ServiceFilterAttribute(typeof(ApiKeyAuthZFilter));
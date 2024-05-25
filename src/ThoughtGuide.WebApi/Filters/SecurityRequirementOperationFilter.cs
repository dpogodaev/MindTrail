using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using ThoughtGuide.WebAuth.Attributes;
using ThoughtGuide.WebAuth.Constants;

namespace ThoughtGuide.WebApi.Filters;

/// <summary>
/// Security requirement operation filter.
/// </summary>
public class SecurityRequirementOperationFilter : IOperationFilter
{
    #region IOperationFilter

    /// <inheritdoc cref="IOperationFilter.Apply"/>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var isAuthAttributeUsed = IsAuthAttributeUsed(context);
        var isApiKeyAttributeUsed = IsApiKeyAttributeUsed(context);

        if (!isAuthAttributeUsed && !isApiKeyAttributeUsed) return;

        AddSecurityRequirements(operation, isAuthAttributeUsed);
        AddOperationResponses(operation, isAuthAttributeUsed);
    }

    #endregion

    #region Private methods

    private static bool IsAuthAttributeUsed(OperationFilterContext context)
    {
        var attributes = context.MethodInfo?.DeclaringType?
            .GetCustomAttributes(true)
            .Union(context.MethodInfo.GetCustomAttributes(true))
            .OfType<AuthorizeAttribute>();

        return attributes is not null && attributes.Any();
    }

    private static bool IsApiKeyAttributeUsed(OperationFilterContext context)
    {
        return context.MethodInfo?.DeclaringType?.GetCustomAttributes(true)
            .Union(context.MethodInfo.GetCustomAttributes(true))
            .OfType<ServiceFilterAttribute>()
            .Any(x => x is ApiKeyRequiredAttribute) ?? false;
    }

    private static void AddSecurityRequirements(OpenApiOperation operation, bool isAuthAttributeUsed)
    {
        var securityRequirements = new List<OpenApiSecurityRequirement>();

        AddApiKeySecurityRequirement(securityRequirements);

        if (isAuthAttributeUsed)
        {
            //TODO: add security requirement (e.g. for 'Bearer').
        }

        operation.Security = securityRequirements;
    }

    private static void AddApiKeySecurityRequirement(ICollection<OpenApiSecurityRequirement> securityRequirements)
    {
        var apiKeyScheme = new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = ApiKeyConstants.ApiKeySchemeName
            }
        };

        securityRequirements.Add(new OpenApiSecurityRequirement { [apiKeyScheme] = new List<string>() });
    }

    private static void AddOperationResponses(OpenApiOperation operation, bool isAuthAttributeUsed)
    {
        AddUnauthorizedResponses(operation);

        if (isAuthAttributeUsed)
        {
            AddForbiddenResponses(operation);
        }
    }

    private static void AddForbiddenResponses(OpenApiOperation operation)
    {
        operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });
    }

    private static void AddUnauthorizedResponses(OpenApiOperation operation)
    {
        operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
    }

    #endregion
}
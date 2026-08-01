using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;
using MindTrail.WebAuth.Attributes;
using MindTrail.WebAuth.Constants;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MindTrail.WebApi.Filters;

/// <summary>
/// Adds security requirements and unauthorized/forbidden responses to the Swagger document,
/// based on the authorization attributes applied to the operation.
/// </summary>
public class SecurityRequirementOperationFilter : IOperationFilter
{
    /// <summary>
    /// Adds security requirements and unauthorized/forbidden responses to the specified Swagger operation,
    /// if it has <see cref="AuthorizeAttribute"/> or <see cref="ApiKeyRequiredAttribute"/> applied.
    /// </summary>
    /// <param name="operation">The Swagger operation to update.</param>
    /// <param name="context">The context for the operation.</param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var isAuthAttributeUsed = IsAuthAttributeUsed(context);
        var isApiKeyAttributeUsed = IsApiKeyAttributeUsed(context);

        if (!isAuthAttributeUsed && !isApiKeyAttributeUsed)
        {
            return;
        }

        AddSecurityRequirements(operation, context.Document, isAuthAttributeUsed);
        AddOperationResponses(operation, isAuthAttributeUsed);
    }

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

    private static void AddSecurityRequirements(
        OpenApiOperation operation,
        OpenApiDocument document,
        bool isAuthAttributeUsed)
    {
        var securityRequirements = new List<OpenApiSecurityRequirement>();

        AddApiKeySecurityRequirement(document, securityRequirements);

        if (isAuthAttributeUsed)
        {
            // TODO: add security requirement (e.g. for 'Bearer').
        }

        operation.Security = securityRequirements;
    }

    private static void AddApiKeySecurityRequirement(
        OpenApiDocument document,
        ICollection<OpenApiSecurityRequirement> securityRequirements)
    {
        var apiKeyScheme = new OpenApiSecuritySchemeReference(ApiKeyConstants.ApiKeySchemeName, document);

        securityRequirements.Add(new OpenApiSecurityRequirement { [apiKeyScheme] = [] });
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
        operation.Responses!.Add("403", new OpenApiResponse { Description = "Forbidden" });
    }

    private static void AddUnauthorizedResponses(OpenApiOperation operation)
    {
        operation.Responses!.Add("401", new OpenApiResponse { Description = "Unauthorized" });
    }
}
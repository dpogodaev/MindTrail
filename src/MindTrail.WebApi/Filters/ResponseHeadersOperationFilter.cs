using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi;
using MindTrail.WebApi.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MindTrail.WebApi.Filters;

/// <summary>
/// Adds the description of produced response headers to the Swagger document.
/// </summary>
public class ResponseHeadersOperationFilter : IOperationFilter
{
    /// <summary>
    /// Adds the response headers declared via <see cref="ResponseHeaderAttribute"/>
    /// to the matching responses of the specified Swagger operation.
    /// </summary>
    /// <param name="operation">The Swagger operation to update.</param>
    /// <param name="context">The context for the operation.</param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var operationAttributes = GetCustomAttributes<ResponseHeaderAttribute>(context).ToArray();
        if (operationAttributes.Length == 0)
        {
            return;
        }

        foreach (var operationResponseCode in operation.Responses!.Keys)
        {
            var relevantAttributes = GetActionAttributesWithCode(operationAttributes, operationResponseCode);
            if (relevantAttributes.Length == 0)
            {
                continue;
            }

            var operationResponse = GetActionResponseWithStatusCode(operation, operationResponseCode);

            foreach (var relevantAttribute in relevantAttributes)
            {
                AddHeaderToResponse(operationResponse, relevantAttribute);
            }
        }
    }

    private static IEnumerable<T> GetCustomAttributes<T>(OperationFilterContext context)
        where T : Attribute
    {
        var attributes = context.MethodInfo?.DeclaringType?.GetCustomAttributes(true)
            .Union(context.MethodInfo.GetCustomAttributes(true))
            .OfType<T>();

        return attributes ?? [];
    }

    private static ResponseHeaderAttribute[] GetActionAttributesWithCode(
        IEnumerable<ResponseHeaderAttribute> attributes, string code)
    {
        return attributes.Where(x => x.StatusCode.ToString() == code).ToArray();
    }

    private static IOpenApiResponse GetActionResponseWithStatusCode(OpenApiOperation operation, string code)
    {
        var operationResponse = operation.Responses![code];

        return operationResponse;
    }

    private static void AddHeaderToResponse(IOpenApiResponse response, ResponseHeaderAttribute header)
    {
        if (response.Headers == null)
        {
            if (response is OpenApiResponse concreteResponse)
            {
                concreteResponse.Headers = new ConcurrentDictionary<string, IOpenApiHeader>();
            }
            else
            {
                return;
            }
        }

        response.Headers![header.Name] = new OpenApiHeader
        {
            Schema = new OpenApiSchema { Type = header.Type },
            Description = header.Description,
        };
    }
}
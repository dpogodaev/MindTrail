using System;
using System.Collections.Generic;
using System.Linq;
using MindTrail.WebApi.Attributes;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MindTrail.WebApi.Filters;

/// <summary>
/// Swagger operation filter which is used to add the description of produced response headers to swagger document.
/// </summary>
public class ResponseHeadersOperationFilter : IOperationFilter
{
    #region IOperationFilter

    /// <inheritdoc cref="IOperationFilter.Apply"/>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var operationAttributes = GetCustomAttributes<ResponseHeaderAttribute>(context).ToArray();
        if (operationAttributes.Length == 0) return;

        foreach (var operationResponseCode in operation.Responses.Keys)
        {
            var relevantAttributes = GetActionAttributesWithCode(operationAttributes, operationResponseCode);
            if (relevantAttributes.Length == 0) continue;

            var operationResponse = GetActionResponseWithStatusCode(operation, operationResponseCode);

            foreach (var relevantAttribute in relevantAttributes)
            {
                AddHeaderToResponse(operationResponse, relevantAttribute);
            }
        }
    }

    #endregion

    #region Private methods

    private static IEnumerable<T> GetCustomAttributes<T>(OperationFilterContext context) where T : Attribute
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

    private static OpenApiResponse GetActionResponseWithStatusCode(OpenApiOperation operation, string code)
    {
        var operationResponse = operation.Responses[code];
        operationResponse.Headers ??= new Dictionary<string, OpenApiHeader>();

        return operationResponse;
    }

    private static void AddHeaderToResponse(OpenApiResponse response, ResponseHeaderAttribute header)
    {
        response.Headers[header.Name] = new OpenApiHeader
        {
            Schema = new OpenApiSchema { Type = header.Type.ToLower() },
            Description = header.Description
        };
    }

    #endregion
}
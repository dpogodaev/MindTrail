using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace MindTrail.WebApi.Tests.Extensions;

public static class ProblemDetailsExtensions
{
    public static string? GetStringParameter(this ProblemDetails problemDetails, string paramName)
    {
        return problemDetails.Extensions.TryGetValue(paramName, out var value)
            ? value?.ToString()
            : null;
    }

    public static int? GetIntParameter(this ProblemDetails problemDetails, string paramName)
    {
        if (problemDetails.Extensions.TryGetValue(paramName, out var value))
        {
            return int.TryParse(value?.ToString(), out var parsedInt)
                ? parsedInt
                : null;
        }

        return null;
    }

    public static string? GetErrorCode(this ProblemDetails problemDetails)
    {
        return problemDetails.GetStringParameter("errorCode");
    }

    public static string GetInvalidPropertyName(this ValidationProblemDetails problemDetails)
    {
        return problemDetails.Errors is not { Count: 1 }
            ? throw new InvalidOperationException("Errors must contain exactly one key")
            : problemDetails.Errors.Keys.Single();
    }

    public static string GetErrorDescription(this ValidationProblemDetails problemDetails)
    {
        return problemDetails.Errors is not { Count: 1 }
            ? throw new InvalidOperationException("Errors must contain exactly one key")
            : problemDetails.Errors.Values.Single()[^1];
    }
}
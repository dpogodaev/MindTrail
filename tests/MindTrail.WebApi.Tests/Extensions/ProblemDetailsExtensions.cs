using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace MindTrail.WebApi.Tests.Extensions;

/// <summary>
/// Provides extension methods for reading values from <see cref="ProblemDetails"/> instances in tests.
/// </summary>
public static class ProblemDetailsExtensions
{
    /// <summary>
    /// Returns the value of the specified string parameter from <see cref="ProblemDetails.Extensions"/>.
    /// </summary>
    /// <param name="problemDetails">The problem details to read the parameter from.</param>
    /// <param name="paramName">The parameter name.</param>
    /// <returns>The parameter value, or <c>null</c> if the parameter is not found.</returns>
    public static string? GetStringParameter(this ProblemDetails problemDetails, string paramName)
    {
        return problemDetails.Extensions.TryGetValue(paramName, out var value)
            ? value?.ToString()
            : null;
    }

    /// <summary>
    /// Returns the value of the specified integer parameter from <see cref="ProblemDetails.Extensions"/>.
    /// </summary>
    /// <param name="problemDetails">The problem details to read the parameter from.</param>
    /// <param name="paramName">The parameter name.</param>
    /// <returns>The parameter value, or <c>null</c> if the parameter is not found or cannot be parsed as an integer.</returns>
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

    /// <summary>
    /// Returns the value of the <c>errorCode</c> parameter from <see cref="ProblemDetails.Extensions"/>.
    /// </summary>
    /// <param name="problemDetails">The problem details to read the parameter from.</param>
    /// <returns>The error code, or <c>null</c> if the parameter is not found.</returns>
    public static string? GetErrorCode(this ProblemDetails problemDetails)
    {
        return problemDetails.GetStringParameter("errorCode");
    }

    /// <summary>
    /// Returns the name of the invalid property from the validation errors.
    /// </summary>
    /// <param name="problemDetails">The validation problem details to read the errors from.</param>
    /// <returns>The name of the invalid property.</returns>
    /// <exception cref="InvalidOperationException">The errors do not contain exactly one key.</exception>
    public static string GetInvalidPropertyName(this ValidationProblemDetails problemDetails)
    {
        return problemDetails.Errors is not { Count: 1 }
            ? throw new InvalidOperationException("Errors must contain exactly one key.")
            : problemDetails.Errors.Keys.Single();
    }

    /// <summary>
    /// Returns the description of the validation error.
    /// </summary>
    /// <param name="problemDetails">The validation problem details to read the errors from.</param>
    /// <returns>The error description.</returns>
    /// <exception cref="InvalidOperationException">The errors do not contain exactly one key.</exception>
    public static string GetErrorDescription(this ValidationProblemDetails problemDetails)
    {
        return problemDetails.Errors is not { Count: 1 }
            ? throw new InvalidOperationException("Errors must contain exactly one key.")
            : problemDetails.Errors.Values.Single()[^1];
    }
}
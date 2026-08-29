using System.Collections.Generic;
using System.Text.Json;

namespace MindTrail.WebApi.Tests.Extensions;

/// <summary>
/// Provides extension methods for building query parameter dictionaries in tests.
/// </summary>
public static class QueryParamExtensions
{
    /// <summary>
    /// Adds the specified string value to the query parameters if it is not <c>null</c> or empty.
    /// </summary>
    /// <param name="queryParams">The query parameters to add the value to.</param>
    /// <param name="key">The parameter key.</param>
    /// <param name="value">The parameter value. Optional.</param>
    /// <param name="camelCase">Whether the key should be converted to camelCase. Optional.</param>
    public static void AddIfNotNull(
        this IDictionary<string, string> queryParams,
        string key,
        string? value,
        bool camelCase = true)
    {
        if (string.IsNullOrEmpty(value))
        {
            return;
        }

        queryParams[GetKey(key, camelCase)] = value;
    }

    /// <summary>
    /// Adds the specified integer value to the query parameters if it is not <c>null</c>.
    /// </summary>
    /// <param name="queryParams">The query parameters to add the value to.</param>
    /// <param name="key">The parameter key.</param>
    /// <param name="value">The parameter value. Optional.</param>
    /// <param name="camelCase">Whether the key should be converted to camelCase. Optional.</param>
    public static void AddIfNotNull(
        this IDictionary<string, string> queryParams,
        string key,
        int? value,
        bool camelCase = true)
    {
        if (value == null)
        {
            return;
        }

        queryParams[GetKey(key, camelCase)] = value.ToString()!;
    }

    /// <summary>
    /// Adds the specified boolean value to the query parameters if it is not <c>null</c>.
    /// </summary>
    /// <param name="queryParams">The query parameters to add the value to.</param>
    /// <param name="key">The parameter key.</param>
    /// <param name="value">The parameter value. Optional.</param>
    /// <param name="camelCase">Whether the key should be converted to camelCase. Optional.</param>
    public static void AddIfNotNull(
        this IDictionary<string, string> queryParams,
        string key,
        bool? value,
        bool camelCase = true)
    {
        if (value == null)
        {
            return;
        }

        queryParams[GetKey(key, camelCase)] = value.ToString()!;
    }

    private static string GetKey(string key, bool camelCase = true)
    {
        return camelCase
            ? JsonNamingPolicy.CamelCase.ConvertName(key)
            : key;
    }
}
using System.Collections.Generic;
using System.Text.Json;

namespace MindTrail.WebApi.Tests.Extensions;

public static class QueryParamExtensions
{
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
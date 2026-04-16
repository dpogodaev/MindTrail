using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using MindTrail.WebApi.Controllers;
using MindTrail.WebApi.RequestModels;
using MindTrail.WebApi.Tests.Extensions;

namespace MindTrail.WebApi.Tests.Providers;

public class PersonApiProvider
{
    private const string BaseUrl = "api/mind-trail/v1/persons";
    private readonly HttpClient _client;
    private readonly string _apiKey;
    private readonly string _baseUrl;

    public PersonApiProvider(HttpClient client, string apiKey)
    {
        _client = client;
        _apiKey = apiKey;
        _baseUrl = _client.BaseAddress!.ToString();
    }

    /// <summary>
    /// Request for endpoint <see cref="PersonController.GetPersons"/>.
    /// </summary>
    /// <param name="query">Parameters for querying.</param>
    /// <returns>The HTTP response message received from the endpoint. </returns>
    public async Task<HttpResponseMessage> GetPersonsAsync(PersonQueryModel query)
    {
        ArgumentNullException.ThrowIfNull(query, nameof(query));

        var queryParams = new Dictionary<string, string>();

        queryParams.AddIfNotNull(nameof(query.PageNumber), query.PageNumber);
        queryParams.AddIfNotNull(nameof(query.PageSize), query.PageSize);

        if (!string.IsNullOrWhiteSpace(query.TextSearchQuery))
        {
            queryParams.AddIfNotNull(nameof(query.TextSearchQuery), query.TextSearchQuery);
            queryParams.AddIfNotNull(nameof(query.TextSearchCaseSensitive), query.TextSearchCaseSensitive);
        }

        queryParams.AddIfNotNull(nameof(query.FullName), query.FullName);
        queryParams.AddIfNotNull(nameof(query.BirthYear), query.BirthYear);

        if (query.SortField != null)
        {
            queryParams.AddIfNotNull(nameof(query.SortField), (int)query.SortField);
            queryParams.AddIfNotNull(nameof(query.SortDirection), (int?)query.SortDirection);
        }

        var request = new HttpRequestMessage(HttpMethod.Get, BaseUrl)
            .AddApiKey(_apiKey)
            .AddQueryParams(_baseUrl, queryParams);

        return await _client.SendAsync(request);
    }

    /// <summary>
    /// Request for endpoint <see cref="PersonController.CreatePerson"/>.
    /// </summary>
    /// <param name="model">The model to create a person.</param>
    /// <returns>The HTTP response message received from the endpoint. </returns>
    public async Task<HttpResponseMessage> CreatePersonAsync(PersonCreationModel model)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, BaseUrl)
            .AddApiKey(_apiKey)
            .AddContent(JsonSerializer.Serialize(model));

        return await _client.SendAsync(request);
    }
}
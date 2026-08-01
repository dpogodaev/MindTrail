using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using MindTrail.WebApi.Controllers;
using MindTrail.WebApi.Models.Persons;
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
    /// <param name="model">The model to query a list of persons.</param>
    /// <returns>The HTTP response message received from the endpoint. </returns>
    public async Task<HttpResponseMessage> GetPersonsAsync(PersonQueryModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        var queryParams = new Dictionary<string, string>();

        queryParams.AddIfNotNull(nameof(model.PageNumber), model.PageNumber);
        queryParams.AddIfNotNull(nameof(model.PageSize), model.PageSize);

        if (!string.IsNullOrWhiteSpace(model.TextSearchQuery))
        {
            queryParams.AddIfNotNull(nameof(model.TextSearchQuery), model.TextSearchQuery);
            queryParams.AddIfNotNull(nameof(model.TextSearchCaseSensitive), model.TextSearchCaseSensitive);
        }

        queryParams.AddIfNotNull(nameof(model.FullName), model.FullName);
        queryParams.AddIfNotNull(nameof(model.BirthYear), model.BirthYear);

        if (model.SortField != null)
        {
            queryParams.AddIfNotNull(nameof(model.SortField), (int)model.SortField);
            queryParams.AddIfNotNull(nameof(model.SortDirection), (int?)model.SortDirection);
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
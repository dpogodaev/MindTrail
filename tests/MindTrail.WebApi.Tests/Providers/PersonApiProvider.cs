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
    /// <param name="filter">Filter model for querying persons.</param>
    /// <returns>The HTTP response message received from the endpoint. </returns>
    public async Task<HttpResponseMessage> GetPersonsAsync(PersonFilterModel filter)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["pageNumber"] = filter.PageNumber.ToString(),
            ["pageSize"] = filter.PageSize.ToString(),
        };

        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            queryParams["search"] = filter.Search!;
        }

        if (!string.IsNullOrWhiteSpace(filter.Sorting))
        {
            queryParams["sorting"] = filter.Sorting!;
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
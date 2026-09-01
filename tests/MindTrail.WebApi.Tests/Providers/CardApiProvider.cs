using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using MindTrail.WebApi.Controllers;
using MindTrail.WebApi.Models.Cards;
using MindTrail.WebApi.Tests.Extensions;

namespace MindTrail.WebApi.Tests.Providers;

/// <summary>
/// Sends requests to the <see cref="CardController"/> endpoints.
/// </summary>
public class CardApiProvider
{
    private const string BaseUrl = "api/mind-trail/v1/cards";
    private readonly HttpClient _client;
    private readonly string _apiKey;
    private readonly string _baseUrl;

    /// <summary>
    /// Initializes a new instance of the <see cref="CardApiProvider"/> class.
    /// </summary>
    /// <param name="client">The HTTP client used to send requests.</param>
    /// <param name="apiKey">The API key used to authenticate requests.</param>
    public CardApiProvider(HttpClient client, string apiKey)
    {
        _client = client;
        _apiKey = apiKey;
        _baseUrl = _client.BaseAddress!.ToString();
    }

    /// <summary>
    /// Request for endpoint <see cref="CardController.GetCards"/>, authenticated with the provider's API key.
    /// </summary>
    /// <param name="model">The model to query a list of cards.</param>
    /// <returns>The HTTP response message received from the endpoint.</returns>
    public Task<HttpResponseMessage> GetCardsAsync(CardQueryModel model)
    {
        return GetCardsAsync(model, _apiKey);
    }

    /// <summary>
    /// Request for endpoint <see cref="CardController.GetCards"/> using the specified API key.
    /// </summary>
    /// <param name="model">The model to query a list of cards.</param>
    /// <param name="apiKey">
    /// The API key to send. If <c>null</c> or empty, the request is sent without the API key header.
    /// </param>
    /// <returns>The HTTP response message received from the endpoint.</returns>
    public async Task<HttpResponseMessage> GetCardsAsync(CardQueryModel model, string? apiKey)
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

        queryParams.AddIfNotNull(nameof(model.CardNumber), model.CardNumber);
        queryParams.AddIfNotNull(nameof(model.Title), model.Title);
        queryParams.AddIfNotNull(nameof(model.Content), model.Content);

        if (model.SortField != null)
        {
            queryParams.AddIfNotNull(nameof(model.SortField), (int)model.SortField);
            queryParams.AddIfNotNull(nameof(model.SortDirection), (int?)model.SortDirection);
        }

        var request = new HttpRequestMessage(HttpMethod.Get, BaseUrl)
            .AddQueryParams(_baseUrl, queryParams);

        if (!string.IsNullOrEmpty(apiKey))
        {
            request.AddApiKey(apiKey);
        }

        return await _client.SendAsync(request);
    }

    /// <summary>
    /// Request for endpoint <see cref="CardController.GetCardByNumber"/>.
    /// </summary>
    /// <param name="number">The number of the card to retrieve.</param>
    /// <returns>The HTTP response message received from the endpoint.</returns>
    public async Task<HttpResponseMessage> GetCardByNumberAsync(int number)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{BaseUrl}/{number}")
            .AddApiKey(_apiKey);

        return await _client.SendAsync(request);
    }

    /// <summary>
    /// Request for endpoint <see cref="CardController.CreateCard"/>.
    /// </summary>
    /// <param name="model">The model to create a card.</param>
    /// <returns>The HTTP response message received from the endpoint.</returns>
    public async Task<HttpResponseMessage> CreateCardAsync(CardCreationModel model)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, BaseUrl)
            .AddApiKey(_apiKey)
            .AddContent(JsonSerializer.Serialize(model));

        return await _client.SendAsync(request);
    }

    /// <summary>
    /// Request for endpoint <see cref="CardController.UpdateCard"/>.
    /// </summary>
    /// <param name="number">The number of the card to update.</param>
    /// <param name="model">The model to update the card.</param>
    /// <returns>The HTTP response message received from the endpoint.</returns>
    public async Task<HttpResponseMessage> UpdateCardAsync(int number, CardUpdateModel model)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, $"{BaseUrl}/{number}")
            .AddApiKey(_apiKey)
            .AddContent(JsonSerializer.Serialize(model));

        return await _client.SendAsync(request);
    }

    /// <summary>
    /// Request for endpoint <see cref="CardController.DeleteCard"/>.
    /// </summary>
    /// <param name="number">The number of the card to delete.</param>
    /// <returns>The HTTP response message received from the endpoint.</returns>
    public async Task<HttpResponseMessage> DeleteCardAsync(int number)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"{BaseUrl}/{number}")
            .AddApiKey(_apiKey);

        return await _client.SendAsync(request);
    }
}

using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindTrail.ApplicationConfigurator.Extensions;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.ApplicationContracts.Enums;
using MindTrail.Domain.ValueObjects;
using MindTrail.WebApi.Controllers;
using MindTrail.WebApi.Models.Cards;
using MindTrail.WebApi.Tests.Extensions;
using MindTrail.WebApi.Tests.Factories;
using MindTrail.WebApi.Tests.Providers;

namespace MindTrail.WebApi.Tests.ApiTests;

/// <summary>
/// Tests for <see cref="CardController"/> class.
/// </summary>
[TestClass]
[DoNotParallelize]
[TestCategory("API")]
public class CardApiTests
{
    private const int UnknownCardNumber = 999999;

    private static CustomWebAppFactory<Program>? _appFactory;
    private static IConfiguration? _configuration;
    private static string? _apiKey;

    private readonly CardCreationModel _cardCreationModel = new()
    {
        Title = "Sample title",
        Content = "Sample content",
    };

    private readonly CardUpdateModel _cardUpdateModel = new()
    {
        Title = "Updated title",
        Content = "Updated content",
    };

    private CardApiProvider? _cardApiProvider;

    [ClassInitialize]
    public static void Initialize(TestContext context)
    {
        _appFactory = new CustomWebAppFactory<Program>();
        _configuration = _appFactory.Services.GetRequiredService<IConfiguration>();
        _apiKey = _configuration.GetProperty("App:ApiKey");
    }

    [TestInitialize]
    public void TestInitialize()
    {
        var client = _appFactory!.CreateClient(new WebApplicationFactoryClientOptions());

        _cardApiProvider = new CardApiProvider(client, _apiKey!);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _appFactory!.ResetDatabase();
    }

    /// <summary>
    /// Ensures that cards are sorted by creation date in descending order by default when getting the cards list.
    /// </summary>
    [TestMethod]
    [TestCategory("API")]
    public async Task Default_sorting_by_creation_date_desc_applied_when_getting_cards_list()
    {
        // Arrange
        await _cardApiProvider!.CreateCardAsync(_cardCreationModel with { Title = "Card A" });
        await _cardApiProvider!.CreateCardAsync(_cardCreationModel with { Title = "Card B" });
        await _cardApiProvider!.CreateCardAsync(_cardCreationModel with { Title = "Card Z" });

        var queryModel = new CardQueryModel();

        // Act
        var response = await _cardApiProvider!.GetCardsAsync(queryModel);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var cards = await response.GetContentAsync<PagedDto<CardDto>>();

        Assert.IsNotNull(cards);
        Assert.HasCount(3, cards.Items);

        var firstCard = cards.Items.ElementAt(0);
        var secondCard = cards.Items.ElementAt(1);
        var thirdCard = cards.Items.ElementAt(2);

        Assert.AreEqual("Card Z", firstCard.Title);
        Assert.AreEqual("Card B", secondCard.Title);
        Assert.AreEqual("Card A", thirdCard.Title);
    }

    /// <summary>
    /// Ensures that sorting by title is applied when getting the cards list.
    /// </summary>
    [TestMethod]
    [TestCategory("API")]
    public async Task Sorting_by_title_applied_when_getting_cards_list()
    {
        // Arrange
        await _cardApiProvider!.CreateCardAsync(_cardCreationModel with { Title = "Card A" });
        await _cardApiProvider!.CreateCardAsync(_cardCreationModel with { Title = "Card B" });
        await _cardApiProvider!.CreateCardAsync(_cardCreationModel with { Title = "Card Z" });

        var queryModel = new CardQueryModel
        {
            SortField = CardSortingFieldType.Title,
            SortDirection = SortDirectionType.Asc,
        };

        // Act
        var response = await _cardApiProvider!.GetCardsAsync(queryModel);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var cards = await response.GetContentAsync<PagedDto<CardDto>>();

        Assert.IsNotNull(cards);
        Assert.HasCount(3, cards.Items);

        var firstCard = cards.Items.ElementAt(0);
        var secondCard = cards.Items.ElementAt(1);
        var thirdCard = cards.Items.ElementAt(2);

        Assert.AreEqual("Card A", firstCard.Title);
        Assert.AreEqual("Card B", secondCard.Title);
        Assert.AreEqual("Card Z", thirdCard.Title);
    }

    /// <summary>
    /// Ensures that sorting by number is applied when getting the cards list.
    /// </summary>
    [TestMethod]
    [TestCategory("API")]
    public async Task Sorting_by_number_applied_when_getting_cards_list()
    {
        // Arrange
        await _cardApiProvider!.CreateCardAsync(_cardCreationModel with { Title = "Card A" });
        await _cardApiProvider!.CreateCardAsync(_cardCreationModel with { Title = "Card B" });
        await _cardApiProvider!.CreateCardAsync(_cardCreationModel with { Title = "Card Z" });

        var queryModel = new CardQueryModel
        {
            SortField = CardSortingFieldType.Number,
            SortDirection = SortDirectionType.Asc,
        };

        // Act
        var response = await _cardApiProvider!.GetCardsAsync(queryModel);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var cards = await response.GetContentAsync<PagedDto<CardDto>>();

        Assert.IsNotNull(cards);
        Assert.HasCount(3, cards.Items);

        var firstCard = cards.Items.ElementAt(0);
        var secondCard = cards.Items.ElementAt(1);
        var thirdCard = cards.Items.ElementAt(2);

        Assert.AreEqual("Card A", firstCard.Title);
        Assert.AreEqual("Card B", secondCard.Title);
        Assert.AreEqual("Card Z", thirdCard.Title);
        Assert.IsLessThan(secondCard.Number, firstCard.Number);
        Assert.IsLessThan(thirdCard.Number, secondCard.Number);
    }

    /// <summary>
    /// Ensures that text search filtering is applied when getting the cards list.
    /// </summary>
    [TestMethod]
    [TestCategory("API")]
    public async Task Search_filtering_applied_when_getting_cards_list()
    {
        // Arrange
        await _cardApiProvider!.CreateCardAsync(_cardCreationModel with { Title = "Card A" });
        await _cardApiProvider!.CreateCardAsync(_cardCreationModel with { Title = "Card B v2" });
        await _cardApiProvider!.CreateCardAsync(_cardCreationModel with { Title = "Card Z v2" });

        var queryModel = new CardQueryModel
        {
            TextSearchQuery = "v2",
        };

        // Act
        var response = await _cardApiProvider!.GetCardsAsync(queryModel);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var cards = await response.GetContentAsync<PagedDto<CardDto>>();

        Assert.IsNotNull(cards);
        Assert.HasCount(2, cards.Items);
        Assert.AreEqual(2, cards.Total);

        var firstCard = cards.Items.ElementAt(0);
        var secondCard = cards.Items.ElementAt(1);

        Assert.AreEqual("Card Z v2", firstCard.Title);
        Assert.AreEqual("Card B v2", secondCard.Title);
    }

    /// <summary>
    /// Ensures that pagination is applied when getting the cards list.
    /// </summary>
    [TestMethod]
    [TestCategory("API")]
    public async Task Pagination_applied_when_getting_cards_list()
    {
        // Arrange
        for (var i = 1; i <= 5; i++)
        {
            await _cardApiProvider!.CreateCardAsync(_cardCreationModel with { Title = $"Card {i}" });
        }

        var queryModel = new CardQueryModel
        {
            PageNumber = 1,
            PageSize = 2,
        };

        // Act
        var response = await _cardApiProvider!.GetCardsAsync(queryModel);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var cards = await response.GetContentAsync<PagedDto<CardDto>>();

        Assert.IsNotNull(cards);
        Assert.HasCount(2, cards.Items);
        Assert.AreEqual(5, cards.Total);

        var firstCard = cards.Items.ElementAt(0);
        var secondCard = cards.Items.ElementAt(1);

        Assert.AreEqual("Card 5", firstCard.Title);
        Assert.AreEqual("Card 4", secondCard.Title);
    }

    /// <summary>
    /// Ensures that sorting in descending order is applied when getting the cards list.
    /// </summary>
    [TestMethod]
    [TestCategory("API")]
    public async Task Sorting_direction_desc_applied_when_getting_cards_list()
    {
        // Arrange
        await _cardApiProvider!.CreateCardAsync(_cardCreationModel with { Title = "Card A" });
        await _cardApiProvider!.CreateCardAsync(_cardCreationModel with { Title = "Card B" });
        await _cardApiProvider!.CreateCardAsync(_cardCreationModel with { Title = "Card Z" });

        var queryModel = new CardQueryModel
        {
            SortField = CardSortingFieldType.Title,
            SortDirection = SortDirectionType.Desc,
        };

        // Act
        var response = await _cardApiProvider!.GetCardsAsync(queryModel);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var cards = await response.GetContentAsync<PagedDto<CardDto>>();

        Assert.IsNotNull(cards);
        Assert.HasCount(3, cards.Items);

        Assert.AreEqual("Card Z", cards.Items.ElementAt(0).Title);
        Assert.AreEqual("Card B", cards.Items.ElementAt(1).Title);
        Assert.AreEqual("Card A", cards.Items.ElementAt(2).Title);
    }

    /// <summary>
    /// Ensures that filtering by title performs a partial, case-insensitive match when getting the cards list.
    /// </summary>
    [TestMethod]
    [TestCategory("API")]
    public async Task Filtering_by_title_applied_when_getting_cards_list()
    {
        // Arrange
        await _cardApiProvider!.CreateCardAsync(_cardCreationModel with { Title = "1. Red apple" });
        await _cardApiProvider!.CreateCardAsync(_cardCreationModel with { Title = "2. Green APPLE" });
        await _cardApiProvider!.CreateCardAsync(_cardCreationModel with { Title = "3. Yellow banana" });

        var queryModel = new CardQueryModel { Title = "apple" };

        // Act
        var response = await _cardApiProvider!.GetCardsAsync(queryModel);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var cards = await response.GetContentAsync<PagedDto<CardDto>>();

        Assert.IsNotNull(cards);
        Assert.HasCount(2, cards.Items);
        Assert.AreEqual(2, cards.Total);
        Assert.Contains(x => x.Title == "1. Red apple", cards.Items);
        Assert.Contains(x => x.Title == "2. Green APPLE", cards.Items);
    }

    /// <summary>
    /// Ensures that filtering by content performs a partial, case-insensitive match when getting the cards list.
    /// </summary>
    [TestMethod]
    [TestCategory("API")]
    public async Task Filtering_by_content_applied_when_getting_cards_list()
    {
        // Arrange
        await _cardApiProvider!.CreateCardAsync(_cardCreationModel with { Content = "1. Red apple" });
        await _cardApiProvider!.CreateCardAsync(_cardCreationModel with { Content = "2. Green apple" });
        await _cardApiProvider!.CreateCardAsync(_cardCreationModel with { Content = "3. Yellow banana" });

        var queryModel = new CardQueryModel { Content = "red" };

        // Act
        var response = await _cardApiProvider!.GetCardsAsync(queryModel);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var cards = await response.GetContentAsync<PagedDto<CardDto>>();

        Assert.IsNotNull(cards);
        Assert.HasCount(1, cards.Items);
        Assert.Contains(x => x.Content == "1. Red apple", cards.Items);
    }

    /// <summary>
    /// Ensures that filtering by card number is applied when getting the cards list.
    /// </summary>
    [TestMethod]
    [TestCategory("API")]
    public async Task Filtering_by_card_number_applied_when_getting_cards_list()
    {
        // Arrange
        await _cardApiProvider!.CreateCardAsync(_cardCreationModel with { Title = "Card A" });
        var targetNumber = await (
            await _cardApiProvider!.CreateCardAsync(_cardCreationModel with { Title = "Card B" })).GetIntAsync();
        await _cardApiProvider!.CreateCardAsync(_cardCreationModel with { Title = "Card C" });

        var queryModel = new CardQueryModel { CardNumber = targetNumber };

        // Act
        var response = await _cardApiProvider!.GetCardsAsync(queryModel);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var cards = await response.GetContentAsync<PagedDto<CardDto>>();

        Assert.IsNotNull(cards);
        Assert.HasCount(1, cards.Items);

        var filteredCard = cards.Items.Single();

        Assert.AreEqual(targetNumber, filteredCard.Number);
        Assert.AreEqual("Card B", filteredCard.Title);
    }

    /// <summary>
    /// Ensures that a case-sensitive text search is applied when getting the cards list.
    /// </summary>
    [TestMethod]
    [TestCategory("API")]
    public async Task Case_sensitive_search_applied_when_getting_cards_list()
    {
        // Arrange
        await _cardApiProvider!.CreateCardAsync(_cardCreationModel with { Title = "Green apple" });
        await _cardApiProvider!.CreateCardAsync(_cardCreationModel with { Title = "Red APPLE" });

        var queryModel = new CardQueryModel
        {
            TextSearchQuery = "apple",
            TextSearchCaseSensitive = true,
        };

        // Act
        var response = await _cardApiProvider!.GetCardsAsync(queryModel);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var cards = await response.GetContentAsync<PagedDto<CardDto>>();

        Assert.IsNotNull(cards);
        Assert.HasCount(1, cards.Items);
        Assert.AreEqual("Green apple", cards.Items.ElementAt(0).Title);
    }

    /// <summary>
    /// Ensures that an empty paged result is returned when no cards exist.
    /// </summary>
    [TestMethod]
    [TestCategory("API")]
    public async Task Empty_list_returned_when_no_cards_exist()
    {
        // Act
        var response = await _cardApiProvider!.GetCardsAsync(new CardQueryModel());

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var cards = await response.GetContentAsync<PagedDto<CardDto>>();

        Assert.IsNotNull(cards);
        Assert.IsEmpty(cards.Items);
        Assert.AreEqual(0, cards.Total);
    }

    /// <summary>
    /// Ensures that a card is created successfully and its number is returned in the response.
    /// </summary>
    [TestMethod]
    [TestCategory("API")]
    public async Task Card_created_successfully()
    {
        // Act
        var response = await _cardApiProvider!.CreateCardAsync(_cardCreationModel);

        // Assert
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

        var cardNumber = await response.GetIntAsync();

        Assert.IsGreaterThan(0, cardNumber);
        Assert.AreEqual($"/api/mind-trail/v1/cards/{cardNumber}", response.Headers.Location?.AbsolutePath);

        var card = await (await _cardApiProvider!.GetCardByNumberAsync(cardNumber)).GetContentAsync<CardDto>();

        Assert.IsNotNull(card);
        Assert.AreEqual(cardNumber, card.Number);
        Assert.AreEqual(_cardCreationModel.Title, card.Title);
        Assert.AreEqual(_cardCreationModel.Content, card.Content);
        Assert.IsNull(card.EditedAt);
    }

    /// <summary>
    /// Ensures that a card is created successfully when the optional content is omitted.
    /// </summary>
    [TestMethod]
    [TestCategory("API")]
    public async Task Card_created_successfully_without_content()
    {
        // Arrange
        var model = new CardCreationModel { Title = "Card without content" };

        // Act
        var response = await _cardApiProvider!.CreateCardAsync(model);

        // Assert
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

        var cardNumber = await response.GetIntAsync();
        var card = await (await _cardApiProvider!.GetCardByNumberAsync(cardNumber)).GetContentAsync<CardDto>();

        Assert.IsNotNull(card);
        Assert.AreEqual(model.Title, card.Title);
        Assert.IsNull(card.Content);
    }

    /// <summary>
    /// Ensures that a card can be retrieved by its number after creation.
    /// </summary>
    [TestMethod]
    [TestCategory("API")]
    public async Task Card_retrieved_by_number()
    {
        // Arrange
        var cardNumber = await (await _cardApiProvider!.CreateCardAsync(_cardCreationModel)).GetIntAsync();

        // Act
        var response = await _cardApiProvider!.GetCardByNumberAsync(cardNumber);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var card = await response.GetContentAsync<CardDto>();

        Assert.IsNotNull(card);
        Assert.AreEqual(cardNumber, card.Number);
        Assert.AreEqual(_cardCreationModel.Title, card.Title);
        Assert.AreEqual(_cardCreationModel.Content, card.Content);
        Assert.IsNull(card.EditedAt);
    }

    /// <summary>
    /// Ensures that a <see cref="HttpStatusCode.NotFound"/> is returned when getting a card by an unknown number.
    /// </summary>
    [TestMethod]
    [TestCategory("API")]
    public async Task Not_found_returned_when_getting_card_by_unknown_number()
    {
        // Act
        var response = await _cardApiProvider!.GetCardByNumberAsync(UnknownCardNumber);

        // Assert
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    /// <summary>
    /// Ensures that an existing card is updated successfully.
    /// </summary>
    [TestMethod]
    [TestCategory("API")]
    public async Task Card_updated_successfully()
    {
        // Arrange
        var cardNumber = await (await _cardApiProvider!.CreateCardAsync(_cardCreationModel)).GetIntAsync();

        // Act
        var response = await _cardApiProvider!.UpdateCardAsync(cardNumber, _cardUpdateModel);

        // Assert
        Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);

        var card = await (await _cardApiProvider!.GetCardByNumberAsync(cardNumber)).GetContentAsync<CardDto>();

        Assert.IsNotNull(card);
        Assert.AreEqual(_cardUpdateModel.Title, card.Title);
        Assert.AreEqual(_cardUpdateModel.Content, card.Content);
        Assert.IsNotNull(card.EditedAt);
    }

    /// <summary>
    /// Ensures that a <see cref="HttpStatusCode.NotFound"/> is returned when updating an unknown card.
    /// </summary>
    [TestMethod]
    [TestCategory("API")]
    public async Task Not_found_returned_when_updating_unknown_card()
    {
        // Act
        var response = await _cardApiProvider!.UpdateCardAsync(UnknownCardNumber, _cardUpdateModel);

        // Assert
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);

        var problemDetails = await response.GetContentAsync<ProblemDetails>();
        Assert.IsNotNull(problemDetails);
        Assert.AreEqual($"The card with the number {UnknownCardNumber} was not found.", problemDetails.Detail);
    }

    /// <summary>
    /// Ensures that a card update is rejected when the title exceeds the maximum allowed length.
    /// </summary>
    [TestMethod]
    [TestCategory("API")]
    public async Task Card_update_rejects_too_long_title()
    {
        // Arrange
        var cardNumber = await (await _cardApiProvider!.CreateCardAsync(_cardCreationModel)).GetIntAsync();

        var tooLongTitle = new string('A', CardTitle.MaxLength + 1);
        var updateModel = _cardUpdateModel with { Title = tooLongTitle };

        // Act
        var response = await _cardApiProvider!.UpdateCardAsync(cardNumber, updateModel);

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

        var problemDetails = await response.GetContentAsync<ValidationProblemDetails>();
        Assert.IsNotNull(problemDetails);

        Assert.AreEqual("The title is too long", problemDetails.Title);
        Assert.AreEqual(
            $"The maximum length of the card's title is {CardTitle.MaxLength} characters. The length of the specified title is {tooLongTitle.Length}.",
            problemDetails.Detail);
        Assert.AreEqual(CardTitle.MaxLength, problemDetails.GetIntParameter("maxLength"));
        Assert.AreEqual(tooLongTitle.Length, problemDetails.GetIntParameter("specifiedLength"));
        Assert.AreEqual("title", problemDetails.GetInvalidPropertyName());
        Assert.AreEqual(
            $"The maximum length is {CardTitle.MaxLength} characters",
            problemDetails.GetErrorDescription());
        Assert.AreEqual("mind_trail.card_title_too_long", problemDetails.GetErrorCode());
    }

    /// <summary>
    /// Ensures that a card update is rejected when the content exceeds the maximum allowed length.
    /// </summary>
    [TestMethod]
    [TestCategory("API")]
    public async Task Card_update_rejects_too_long_content()
    {
        // Arrange
        var cardNumber = await (await _cardApiProvider!.CreateCardAsync(_cardCreationModel)).GetIntAsync();

        var tooLongContent = new string('A', CardContent.MaxLength + 1);
        var updateModel = _cardUpdateModel with { Content = tooLongContent };

        // Act
        var response = await _cardApiProvider!.UpdateCardAsync(cardNumber, updateModel);

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

        var problemDetails = await response.GetContentAsync<ValidationProblemDetails>();
        Assert.IsNotNull(problemDetails);

        Assert.AreEqual("The content is too long", problemDetails.Title);
        Assert.AreEqual(
            $"The maximum length of the card's content is {CardContent.MaxLength} characters. The length of the specified content is {tooLongContent.Length}.",
            problemDetails.Detail);
        Assert.AreEqual(CardContent.MaxLength, problemDetails.GetIntParameter("maxLength"));
        Assert.AreEqual(tooLongContent.Length, problemDetails.GetIntParameter("specifiedLength"));
        Assert.AreEqual("content", problemDetails.GetInvalidPropertyName());
        Assert.AreEqual(
            $"The maximum length is {CardContent.MaxLength} characters",
            problemDetails.GetErrorDescription());
        Assert.AreEqual("mind_trail.card_content_too_long", problemDetails.GetErrorCode());
    }

    /// <summary>
    /// Ensures that an existing card is deleted successfully.
    /// </summary>
    [TestMethod]
    [TestCategory("API")]
    public async Task Card_deleted_successfully()
    {
        // Arrange
        var cardNumber = await (await _cardApiProvider!.CreateCardAsync(_cardCreationModel)).GetIntAsync();

        // Act
        var response = await _cardApiProvider!.DeleteCardAsync(cardNumber);

        // Assert
        Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);

        var getResponse = await _cardApiProvider!.GetCardByNumberAsync(cardNumber);
        Assert.AreEqual(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    /// <summary>
    /// Ensures that a <see cref="HttpStatusCode.NotFound"/> is returned when deleting an unknown card.
    /// </summary>
    [TestMethod]
    [TestCategory("API")]
    public async Task Not_found_returned_when_deleting_unknown_card()
    {
        // Act
        var response = await _cardApiProvider!.DeleteCardAsync(UnknownCardNumber);

        // Assert
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);

        var problemDetails = await response.GetContentAsync<ProblemDetails>();
        Assert.IsNotNull(problemDetails);
        Assert.AreEqual($"The card with the number {UnknownCardNumber} was not found.", problemDetails.Detail);
    }

    /// <summary>
    /// Ensures that card creation is rejected when the title exceeds the maximum allowed length.
    /// </summary>
    [TestMethod]
    [TestCategory("API")]
    public async Task Card_creation_rejects_too_long_title()
    {
        // Arrange
        var tooLongTitle = new string('A', CardTitle.MaxLength + 1);
        var model = _cardCreationModel with { Title = tooLongTitle };

        // Act
        var response = await _cardApiProvider!.CreateCardAsync(model);

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

        var problemDetails = await response.GetContentAsync<ValidationProblemDetails>();
        Assert.IsNotNull(problemDetails);

        Assert.AreEqual("The title is too long", problemDetails.Title);
        Assert.AreEqual(
            $"The maximum length of the card's title is {CardTitle.MaxLength} characters. The length of the specified title is {tooLongTitle.Length}.",
            problemDetails.Detail);
        Assert.AreEqual(CardTitle.MaxLength, problemDetails.GetIntParameter("maxLength"));
        Assert.AreEqual(tooLongTitle.Length, problemDetails.GetIntParameter("specifiedLength"));
        Assert.AreEqual("title", problemDetails.GetInvalidPropertyName());
        Assert.AreEqual(
            $"The maximum length is {CardTitle.MaxLength} characters",
            problemDetails.GetErrorDescription());
        Assert.AreEqual("mind_trail.card_title_too_long", problemDetails.GetErrorCode());
    }

    /// <summary>
    /// Ensures that card creation is rejected when the content exceeds the maximum allowed length.
    /// </summary>
    [TestMethod]
    [TestCategory("API")]
    public async Task Card_creation_rejects_too_long_content()
    {
        // Arrange
        var tooLongContent = new string('A', CardContent.MaxLength + 1);
        var model = _cardCreationModel with { Content = tooLongContent };

        // Act
        var response = await _cardApiProvider!.CreateCardAsync(model);

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

        var problemDetails = await response.GetContentAsync<ValidationProblemDetails>();
        Assert.IsNotNull(problemDetails);

        Assert.AreEqual("The content is too long", problemDetails.Title);
        Assert.AreEqual(
            $"The maximum length of the card's content is {CardContent.MaxLength} characters. The length of the specified content is {tooLongContent.Length}.",
            problemDetails.Detail);
        Assert.AreEqual(CardContent.MaxLength, problemDetails.GetIntParameter("maxLength"));
        Assert.AreEqual(tooLongContent.Length, problemDetails.GetIntParameter("specifiedLength"));
        Assert.AreEqual("content", problemDetails.GetInvalidPropertyName());
        Assert.AreEqual(
            $"The maximum length is {CardContent.MaxLength} characters",
            problemDetails.GetErrorDescription());
        Assert.AreEqual("mind_trail.card_content_too_long", problemDetails.GetErrorCode());
    }

    /// <summary>
    /// Ensures that card creation is rejected by model validation when the title is empty.
    /// </summary>
    [TestMethod]
    [TestCategory("API")]
    public async Task Card_creation_rejects_empty_title()
    {
        // Arrange
        var model = new CardCreationModel { Title = string.Empty };

        // Act
        var response = await _cardApiProvider!.CreateCardAsync(model);

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

        var problemDetails = await response.GetContentAsync<ValidationProblemDetails>();
        Assert.IsNotNull(problemDetails);
        Assert.AreEqual("title", problemDetails.GetInvalidPropertyName().ToLowerInvariant());
    }

    /// <summary>
    /// Ensures that a <see cref="HttpStatusCode.Unauthorized"/> is returned when the API key header is missing.
    /// </summary>
    [TestMethod]
    [TestCategory("API")]
    public async Task Unauthorized_returned_when_api_key_is_missing()
    {
        // Act
        var response = await _cardApiProvider!.GetCardsAsync(new CardQueryModel(), apiKey: string.Empty);

        // Assert
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    /// <summary>
    /// Ensures that a <see cref="HttpStatusCode.Unauthorized"/> is returned when the API key is invalid.
    /// </summary>
    [TestMethod]
    [TestCategory("API")]
    public async Task Unauthorized_returned_when_api_key_is_invalid()
    {
        // Act
        var response = await _cardApiProvider!.GetCardsAsync(new CardQueryModel(), apiKey: "invalid-api-key");

        // Assert
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
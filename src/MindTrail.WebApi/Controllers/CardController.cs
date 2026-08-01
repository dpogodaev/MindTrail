using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.ApplicationContracts.Interfaces;
using MindTrail.DomainShared.Exceptions.Base;
using MindTrail.DomainShared.Exceptions.Cards;
using MindTrail.WebApi.Abstractions.Providers;
using MindTrail.WebApi.Builders;
using MindTrail.WebApi.Models.Cards;
using MindTrail.WebAuth.Attributes;

namespace MindTrail.WebApi.Controllers;

/// <summary>
/// Manages operations with Cards.
/// </summary>
[ApiController]
[ApiKeyRequired]
[Route("api/mind-trail/v1/cards")]
public class CardController(
    IRequestSender requestSender,
    IHttpErrorResultProvider errorProvider)
    : ControllerBase
{
    /// <summary>
    /// Returns a paged list of <see cref="CardDto"/>.
    /// </summary>
    /// <param name="model">The model to query a list of cards.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>Paged <see cref="CardDto"/> collection.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedDto<CardDto>))]
    public async Task<ActionResult<PagedDto<CardDto>>> GetCards(
        [FromQuery] CardQueryModel model,
        CancellationToken cancellationToken)
    {
        var query = CardQueryBuilder.BuildGetCardsQuery(model);
        var cards = await requestSender.Send(query, cancellationToken);

        return Ok(cards);
    }

    /// <summary>
    /// Returns a <see cref="CardDto"/> by number.
    /// </summary>
    /// <param name="number">The number of the card to retrieve.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The requested <see cref="CardDto"/>.</returns>
    [HttpGet("{number:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CardDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<CardDto>> GetCardByNumber(
        int number,
        CancellationToken cancellationToken)
    {
        var query = CardQueryBuilder.BuildGetCardByNumberQuery(number);
        var card = await requestSender.Send(query, cancellationToken);

        return card is null
            ? errorProvider.ToNotFound()
            : Ok(card);
    }

    /// <summary>
    /// Creates a new card.
    /// </summary>
    /// <param name="model">The model to create a card.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The number of the created card.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    public async Task<ActionResult<int>> CreateCard(
        [FromBody, Required] CardCreationModel model,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = CardCommandBuilder.BuildCreateCardCommand(model);
            var cardNumber = await requestSender.Send(command, cancellationToken);

            return CreatedAtAction(
                nameof(GetCardByNumber),
                new { number = cardNumber },
                cardNumber);
        }
        catch (DomainException domainException)
        {
            switch (domainException)
            {
                case CardTitleTooLongException e:
                    return errorProvider.ToBadRequest(e, nameof(model.Title));
                case CardContentTooLongException e:
                    return errorProvider.ToBadRequest(e, nameof(model.Content));
                default: throw;
            }
        }
    }

    /// <summary>
    /// Updates an existing card.
    /// </summary>
    /// <param name="number">The number of the card to update.</param>
    /// <param name="model">The model to update the card.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>
    /// A <see cref="NoContentResult"/> if the card was updated successfully; otherwise, an appropriate error response.
    /// </returns>
    [HttpPut("{number:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> UpdateCard(
        int number,
        [FromBody, Required] CardUpdateModel model,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = CardCommandBuilder.BuildUpdateCardCommand(number, model);
            await requestSender.Send(command, cancellationToken);

            return NoContent();
        }
        catch (DomainException domainException)
        {
            switch (domainException)
            {
                case CardNotFoundException e:
                    return errorProvider.ToNotFound(e);
                case CardTitleTooLongException e:
                    return errorProvider.ToBadRequest(e, nameof(model.Title));
                case CardContentTooLongException e:
                    return errorProvider.ToBadRequest(e, nameof(model.Content));
                default: throw;
            }
        }
    }

    /// <summary>
    /// Deletes a card.
    /// </summary>
    /// <param name="number">The number of the card to delete.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>
    /// A <see cref="NoContentResult"/> if the card was deleted successfully; otherwise, an appropriate error response.
    /// </returns>
    [HttpDelete("{number:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> DeleteCard(
        int number,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = CardCommandBuilder.BuildDeleteCardCommand(number);
            await requestSender.Send(command, cancellationToken);

            return NoContent();
        }
        catch (CardNotFoundException e)
        {
            return errorProvider.ToNotFound(e);
        }
    }
}
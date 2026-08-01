using System;
using System.Threading;
using System.Threading.Tasks;
using MindTrail.Application.Abstractions.Repositories;
using MindTrail.ApplicationContracts.Commands.Cards;
using MindTrail.ApplicationContracts.Interfaces.Commands;
using MindTrail.Common.Interfaces.Providers;
using MindTrail.Domain.Entities;
using MindTrail.Domain.ValueObjects;
using MindTrail.DomainShared.Exceptions.Cards;

namespace MindTrail.Application.Handlers.Cards;

/// <inheritdoc cref="ICommandHandler{CreateCardCommand,CardDto}"/>
/// <param name="currentTimeProvider">Provides the current time.</param>
/// <param name="unitOfWork">Coordinates persisting changes made during command handling.</param>
/// <param name="cardRepository">Provides access to card data, used to create a new card.</param>
public class CreateCardCommandHandler(
    ICurrentTimeProvider currentTimeProvider,
    IUnitOfWork unitOfWork,
    ICardRepository cardRepository)
    : ICommandHandler<CreateCardCommand, int>
{
    /// <inheritdoc cref="ICommandHandler{CreateCardCommand,CardDto}.HandleAsync"/>
    /// <exception cref="CardTitleTooLongException">The card's title is too long.</exception>
    /// <exception cref="CardContentTooLongException">The card's content is too long.</exception>
    /// <returns>The number of the created card.</returns>
    public async Task<int> HandleAsync(
        CreateCardCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command);

        var cardToCreate = Card.Create(
            currentTimeProvider.GetCurrentTime(),
            CardTitle.Create(command.Title),
            CardContent.Create(command.Content));

        unitOfWork.EnableAutoSave();
        var createdCardNumber = await cardRepository.CreateCardAsync(cardToCreate, cancellationToken);

        return createdCardNumber;
    }
}
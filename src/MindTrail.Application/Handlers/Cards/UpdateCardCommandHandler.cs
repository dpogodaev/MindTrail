using System.Threading;
using System.Threading.Tasks;
using MindTrail.Application.Abstractions.Repositories;
using MindTrail.ApplicationContracts.Commands.Cards;
using MindTrail.ApplicationContracts.Commands.Common;
using MindTrail.ApplicationContracts.Interfaces.Commands;
using MindTrail.Common.Interfaces.Providers;
using MindTrail.Domain.ValueObjects;
using MindTrail.DomainShared.Exceptions.Cards;

namespace MindTrail.Application.Handlers.Cards;

/// <inheritdoc cref="ICommandHandler{UpdateCardCommandHandler,VoidResult}"/>
/// <param name="currentTimeProvider">Provides the current time.</param>
/// <param name="unitOfWork">Coordinates persisting changes made during command handling.</param>
/// <param name="cardRepository">Provides access to card data and is used to update it.</param>
public class UpdateCardCommandHandler(
    ICurrentTimeProvider currentTimeProvider,
    IUnitOfWork unitOfWork,
    ICardRepository cardRepository)
    : ICommandHandler<UpdateCardCommand, VoidResult>
{
    /// <inheritdoc cref="ICommandHandler{CardCreationCommand,VoidResult}.HandleAsync"/>
    /// <exception cref="CardTitleTooLongException">The card's title is too long.</exception>
    /// <exception cref="CardContentTooLongException">The card's content is too long.</exception>
    /// <exception cref="CardNotFoundException">The card with the specified number was not found.</exception>
    public async Task<VoidResult> HandleAsync(
        UpdateCardCommand command,
        CancellationToken cancellationToken = default)
    {
        var currentTime = currentTimeProvider.GetCurrentTime();
        var cardToUpdate = await cardRepository.GetRequiredCardByNumberAsync(command.Number, cancellationToken);

        cardToUpdate.ChangeTitle(CardTitle.Create(command.Title), currentTime);
        cardToUpdate.ChangeContent(CardContent.Create(command.Content), currentTime);

        await cardRepository.UpdateCardAsync(cardToUpdate, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return VoidResult.Value;
    }
}
using System.Threading;
using System.Threading.Tasks;
using MindTrail.Application.Abstractions.Repositories;
using MindTrail.ApplicationContracts.Commands;
using MindTrail.ApplicationContracts.Commands.Cards;
using MindTrail.ApplicationContracts.Interfaces.Commands;
using MindTrail.Common.Interfaces.Providers;
using MindTrail.Domain.ValueObjects;
using MindTrail.DomainShared.Exceptions.Cards;

namespace MindTrail.Application.Handlers.Cards;

/// <inheritdoc/>
/// <param name="currentTimeProvider">The provider of the current time.</param>
/// <param name="unitOfWork">The unit of work used to persist changes made during command handling.</param>
/// <param name="cardRepository">The repository providing access to card data, used to update a card.</param>
public class UpdateCardCommandHandler(
    ICurrentTimeProvider currentTimeProvider,
    IUnitOfWork unitOfWork,
    ICardRepository cardRepository)
    : ICommandHandler<UpdateCardCommand, VoidResult>
{
    /// <inheritdoc/>
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
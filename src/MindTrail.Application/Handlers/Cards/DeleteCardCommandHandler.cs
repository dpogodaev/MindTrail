using System;
using System.Threading;
using System.Threading.Tasks;
using MindTrail.Application.Abstractions.Repositories;
using MindTrail.ApplicationContracts.Commands;
using MindTrail.ApplicationContracts.Commands.Cards;
using MindTrail.ApplicationContracts.Interfaces.Commands;
using MindTrail.DomainShared.Exceptions.Cards;

namespace MindTrail.Application.Handlers.Cards;

/// <inheritdoc/>
/// <param name="unitOfWork">The unit of work used to persist changes made during command handling.</param>
/// <param name="cardRepository">The repository providing access to card data, used to delete a card.</param>
public class DeleteCardCommandHandler(
    IUnitOfWork unitOfWork,
    ICardRepository cardRepository)
    : ICommandHandler<DeleteCardCommand, VoidResult>
{
    /// <inheritdoc/>
    /// <exception cref="CardNotFoundException">The card with the specified number was not found.</exception>
    public async Task<VoidResult> HandleAsync(
        DeleteCardCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command);

        await cardRepository.DeleteCardAsync(command.Number, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return VoidResult.Value;
    }
}
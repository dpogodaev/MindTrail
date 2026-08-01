using System;
using System.Threading;
using System.Threading.Tasks;
using MindTrail.Application.Abstractions.Repositories;
using MindTrail.ApplicationContracts.Commands.Cards;
using MindTrail.ApplicationContracts.Commands.Common;
using MindTrail.ApplicationContracts.Interfaces.Commands;
using MindTrail.DomainShared.Exceptions.Cards;

namespace MindTrail.Application.Handlers.Cards;

/// <inheritdoc cref="ICommandHandler{CreateCardCommand,CardDto}"/>
/// <param name="unitOfWork">Coordinates persisting changes made during command handling.</param>
/// <param name="cardRepository">Provides access to card data, used to delete the card.</param>
public class DeleteCardCommandHandler(
    IUnitOfWork unitOfWork,
    ICardRepository cardRepository)
    : ICommandHandler<DeleteCardCommand, VoidResult>
{
    /// <inheritdoc cref="ICommandHandler{CreateCardCommand,CardDto}.HandleAsync"/>
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
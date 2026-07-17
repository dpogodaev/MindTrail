using System.Threading;
using System.Threading.Tasks;
using MindTrail.Application.Abstractions.Repositories;
using MindTrail.ApplicationContracts.Interfaces.Commands;
using MindTrail.ApplicationContracts.Requests.Commands;
using MindTrail.DomainShared.Exceptions;

namespace MindTrail.Application.Handlers;

/// <inheritdoc cref="ICommandHandler{DeletePersonCommandHandler,VoidResult}"/>
/// <param name="unitOfWork">Coordinates persisting changes made during command handling.</param>
/// <param name="personRepository">Provides access to delete the person.</param>
public class DeletePersonCommandHandler(
    IUnitOfWork unitOfWork,
    IPersonRepository personRepository)
    : ICommandHandler<DeletePersonCommand, VoidResult>
{
    /// <inheritdoc cref="ICommandHandler{DeletePersonCommand,VoidResult}.HandleAsync"/>
    /// <exception cref="PersonNotFoundException">The person with the specified ID was not found.</exception>
    public async Task<VoidResult> HandleAsync(
        DeletePersonCommand command,
        CancellationToken cancellationToken = default)
    {
        await personRepository.DeletePersonAsync(command.Id, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return VoidResult.Value;
    }
}
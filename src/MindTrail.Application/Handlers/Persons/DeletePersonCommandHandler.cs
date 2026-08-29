using System.Threading;
using System.Threading.Tasks;
using MindTrail.Application.Abstractions.Repositories;
using MindTrail.ApplicationContracts.Commands;
using MindTrail.ApplicationContracts.Commands.Persons;
using MindTrail.ApplicationContracts.Interfaces.Commands;
using MindTrail.DomainShared.Exceptions.Persons;

namespace MindTrail.Application.Handlers.Persons;

/// <inheritdoc/>
/// <param name="unitOfWork">The unit of work used to persist changes made during command handling.</param>
/// <param name="personRepository">The repository providing access to person data, used to delete a person.</param>
public class DeletePersonCommandHandler(
    IUnitOfWork unitOfWork,
    IPersonRepository personRepository)
    : ICommandHandler<DeletePersonCommand, VoidResult>
{
    /// <inheritdoc/>
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
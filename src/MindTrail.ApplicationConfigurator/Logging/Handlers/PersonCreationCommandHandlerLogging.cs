using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MindTrail.Application.Handlers;
using MindTrail.ApplicationConfigurator.Extensions;
using MindTrail.ApplicationContracts.Interfaces.Commands;
using MindTrail.ApplicationContracts.Requests.Commands;
using MindTrail.Common.Extensions;

namespace MindTrail.ApplicationConfigurator.Logging.Handlers;

/// <inheritdoc cref="ICommandHandler{PersonCreationCommand,PersonDto}"/>
/// <param name="logger">Used to log the outcome of handling the command.</param>
/// <param name="innerHandler">The decorated handler that performs the actual person creation.</param>
public class PersonCreationCommandHandlerLogging(
    ILogger<CreatePersonCommandHandler> logger,
    [FromKeyedServices(DecoratorKeys.Inner)]
    ICommandHandler<CreatePersonCommand, Guid> innerHandler)
    : ICommandHandler<CreatePersonCommand, Guid>
{
    /// <inheritdoc/>
    public async Task<Guid> HandleAsync(
        CreatePersonCommand command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var createdPersonId = await innerHandler.HandleAsync(command, cancellationToken);

            logger.LogDebug(
                LogEvents.Crud.Create,
                "{Title} {PersonId} {Details}",
                "Person created",
                createdPersonId,
                command.Serialize());

            return createdPersonId;
        }
        catch (Exception e)
        {
            logger.Log(
                e.GetLogLevel(),
                LogEvents.Crud.Create, e,
                "{Title} {Details}",
                "Failed to create person",
                command.Serialize());

            throw;
        }
    }
}
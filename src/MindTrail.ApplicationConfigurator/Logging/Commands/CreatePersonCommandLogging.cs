using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MindTrail.Application.Handlers.Persons;
using MindTrail.ApplicationConfigurator.Extensions;
using MindTrail.ApplicationContracts.Commands.Persons;
using MindTrail.ApplicationContracts.Interfaces.Commands;
using MindTrail.Common.Extensions;

namespace MindTrail.ApplicationConfigurator.Logging.Commands;

/// <inheritdoc/>
/// <param name="logger">The logger.</param>
/// <param name="innerHandler">The decorated handler that performs the actual person creation.</param>
public class CreatePersonCommandLogging(
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

            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug(
                    LogEvents.Crud.Create,
                    "{Title} {PersonId} {Details}",
                    "Person created",
                    createdPersonId,
                    command.Serialize());
            }

            return createdPersonId;
        }
        catch (Exception e)
        {
            logger.Log(
                e.GetExceptionLogLevel(),
                LogEvents.Crud.Create, e,
                "{Title} {Details}",
                "Failed to create person",
                command.Serialize());

            throw;
        }
    }
}
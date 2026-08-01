using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MindTrail.Application.Handlers.Cards;
using MindTrail.ApplicationConfigurator.Extensions;
using MindTrail.ApplicationContracts.Commands.Cards;
using MindTrail.ApplicationContracts.Interfaces.Commands;
using MindTrail.Common.Extensions;

namespace MindTrail.ApplicationConfigurator.Logging.Commands;

/// <inheritdoc/>
/// <param name="logger">The logger.</param>
/// <param name="innerHandler">The decorated handler that performs the actual card creation.</param>
public class CreateCardCommandLogging(
    ILogger<CreateCardCommandHandler> logger,
    [FromKeyedServices(DecoratorKeys.Inner)]
    ICommandHandler<CreateCardCommand, int> innerHandler)
    : ICommandHandler<CreateCardCommand, int>
{
    /// <inheritdoc/>
    public async Task<int> HandleAsync(
        CreateCardCommand command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var createdCardNumber = await innerHandler.HandleAsync(command, cancellationToken);

            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug(
                    LogEvents.Crud.Create,
                    "{Title} {CardNumber} {Details}",
                    "Card created",
                    createdCardNumber,
                    command.Serialize());
            }

            return createdCardNumber;
        }
        catch (Exception e)
        {
            logger.Log(
                e.GetExceptionLogLevel(),
                LogEvents.Crud.Create, e,
                "{Title} {Details}",
                "Failed to create card",
                command.Serialize());

            throw;
        }
    }
}
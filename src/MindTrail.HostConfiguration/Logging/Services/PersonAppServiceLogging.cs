using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MindTrail.Application.Services;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.ApplicationContracts.Interfaces.Services;
using MindTrail.ApplicationContracts.RequestModels;
using MindTrail.Common.Extensions;
using MindTrail.HostConfiguration.Extensions;

namespace MindTrail.HostConfiguration.Logging.Services;

public class PersonAppServiceLogging(
    ILogger<PersonAppService> logger,
    IPersonAppService personService)
    : IPersonAppService
{
    public async Task<PagedDto<PersonDto>> GetPersonsAsync(PersonFilterModel filter)
    {
        return await personService.GetPersonsAsync(filter);
    }

    public async Task<PersonDto> CreatePersonAsync(PersonCreationModel model)
    {
        try
        {
            var createdPerson = await personService.CreatePersonAsync(model);

            logger.LogDebug(
                LogEvents.Crud.Create,
                "{Title} {PersonId} {Details}",
                "Person created",
                createdPerson.Id,
                createdPerson.Serialize());

            return createdPerson;
        }
        catch (Exception e)
        {
            logger.Log(
                e.GetLogLevel(),
                LogEvents.Crud.Create, e,
                "{Title} {Details}",
                "Failed to create person",
                model.Serialize());

            throw;
        }
    }
}
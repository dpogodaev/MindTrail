using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MindTrail.AppServices.Extensions;
using MindTrail.AppServices.Interfaces.Services;
using MindTrail.AppServices.Logging;
using MindTrail.AppServices.Models;
using MindTrail.Common.Extensions;
using MindTrail.DomainEntities.Entities;
using MindTrail.DomainServices.Interfaces.Services;
using MindTrail.DomainServices.Interfaces.Storages.Repositories;

namespace MindTrail.AppServices.Services;

/// <inheritdoc/>
public class PersonAppService(
    ILogger<PersonAppService> logger,
    IUnitOfWork unitOfWork,
    IPersonService personService)
    : IPersonAppService
{
    /// <inheritdoc cref="IPersonAppService.CreatePersonAsync"/>
    public async Task<Person> CreatePersonAsync(PersonCreationModel model)
    {
        try
        {
            unitOfWork.EnableAutoSave();

            var createdPerson = await personService.CreatePersonAsync(MapModelToDomainEntity(model));

            logger.LogDebug(
                AppLogEvents.Crud.Create, "{Title} {PersonId} {Details}",
                "The person was created", createdPerson.Id, createdPerson.Serialize());

            return createdPerson;
        }
        catch (Exception e)
        {
            logger.Log(
                e.GetLogLevel(), AppLogEvents.Crud.Create, e, "{Title} {Details}",
                "Failed to create a person", model.Serialize());
            throw;
        }
    }

    private static Person MapModelToDomainEntity(PersonCreationModel model)
    {
        return new Person
        {
            FullName = model.FullName,
            BirthYear = model.BirthYear,
            BirthCountryId = model.BirthCountryId,
        };
    }
}
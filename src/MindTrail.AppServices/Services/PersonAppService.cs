using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MindTrail.AppServices.Exceptions;
using MindTrail.AppServices.Interfaces.Services;
using MindTrail.AppServices.Logging;
using MindTrail.AppServices.Models;
using MindTrail.Common.Helpers;
using MindTrail.DomainEntities.Entities;
using MindTrail.DomainServices.Exceptions;
using MindTrail.DomainServices.Interfaces.Services;
using MindTrail.DomainServices.Interfaces.Storages.Repositories;

namespace MindTrail.AppServices.Services;

public class PersonAppService(
    ILogger<PersonAppService> logger,
    IUnitOfWork unitOfWork,
    IPersonService personService) : IPersonAppService
{
    #region IPersonAppService

    /// <inheritdoc cref="IPersonAppService.CreatePersonAsync"/>
    public async Task<Person> CreatePersonAsync(PersonCreationModel model)
    {
        try
        {
            unitOfWork.EnableAutoSave();

            var createdPerson = await personService.CreatePersonAsync(MapModelToDomainEntity(model));

            LogCreation(createdPerson);
            return createdPerson;
        }
        catch (PersonNameException e)
        {
            LogCreationInvalid(e, model);
            throw new InvalidValueException(e.Message, nameof(model.FullName), e.FullName);
        }
        catch (PersonDuplicateException e)
        {
            LogCreationInvalid(e, model);
            throw new InvalidStateException(e.Message, nameof(model.FullName), e.FullName);
        }
        catch (Exception e)
        {
            LogCreationError(e, model);
            throw;
        }
    }

    #endregion

    #region Private methods

    private static Person MapModelToDomainEntity(PersonCreationModel model)
    {
        return new Person
        {
            FullName = model.FullName,
            BirthYear = model.BirthYear,
            BirthCountryId = model.BirthCountryId
        };
    }

    private void LogCreation(Person person)
    {
        logger.LogDebug(AppLogEvents.Create,
            "{EventTitle}: id={PersonId} {Details}",
            "The person was created", person.Id, $"person={Serialize(person)}");
    }

    private void LogCreationInvalid(Exception e, PersonCreationModel model)
    {
        logger.LogWarning(AppLogEvents.Create, e,
            "{EventTitle}: {Details}",
            "Failed to create a person", $"model={Serialize(model)}");
    }

    private void LogCreationError(Exception e, PersonCreationModel model)
    {
        logger.LogError(AppLogEvents.Create, e,
            "{EventTitle}: {Details}",
            "Failed to create a person", $"model={Serialize(model)}");
    }

    private static string Serialize<T>(T source) where T : class
    {
        return StringHelper.Serialize(source);
    }

    #endregion
}
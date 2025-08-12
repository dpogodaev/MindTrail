using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindTrail.AppServices.Exceptions;
using MindTrail.AppServices.Interfaces.Services;
using MindTrail.DomainEntities.Entities;
using MindTrail.WebApi.Dtos;
using MindTrail.WebApi.Helpers;
using MindTrail.WebApi.RequestModels;
using MindTrail.WebAuth.Attributes;
using AppModels = MindTrail.AppServices.Models;

namespace MindTrail.WebApi.Controllers;

/// <summary>
/// Used to perform basic operations with persons.
/// </summary>
[ApiController]
[ApiKeyRequired]
[Route("api/mind-trail/v1/persons")]
public class PersonController(IPersonAppService personService) : ControllerBase
{
    /// <summary>
    /// Creates a new person.
    /// </summary>
    /// <param name="model">The model to create a person.</param>
    /// <returns>The created person.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PersonDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestDto))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ConflictDto))]
    [Produces("application/json")]
    public async Task<IActionResult> CreatePerson([FromBody, Required] PersonCreationModel model)
    {
        try
        {
            var createdPerson = MapDomainEntityToDto(
                await personService.CreatePersonAsync(MapModelToDomainEntity(model)));

            return CreatedAtAction(nameof(CreatePerson), createdPerson);
        }
        catch (InvalidValueException e)
        {
            return BadRequest(ResponseHelper.BuildBadRequestDto(e));
        }
        catch (InvalidStateException e)
        {
            return Conflict(ResponseHelper.BuildConflictDto(e));
        }
    }

    #region Private methods

    private static AppModels.PersonCreationModel MapModelToDomainEntity(PersonCreationModel model)
    {
        return new AppModels.PersonCreationModel
        {
            FullName = model.FullName,
            BirthYear = model.BirthYear,
            BirthCountryId = model.BirthCountryId,
        };
    }

    private static PersonDto MapDomainEntityToDto(Person domainEntity)
    {
        return new PersonDto
        {
            Id = domainEntity.Id,
            FullName = domainEntity.FullName,
            BirthYear = domainEntity.BirthYear,
            BirthCountryId = domainEntity.BirthCountryId,
            BirthCountryName = domainEntity.BirthCountryName
        };
    }

    #endregion
}
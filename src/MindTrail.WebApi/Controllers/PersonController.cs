using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.ApplicationContracts.Interfaces.AppServices;
using MindTrail.DomainShared.Exceptions;
using MindTrail.DomainShared.Exceptions.Base;
using MindTrail.WebApi.Abstractions.Providers;
using MindTrail.WebApi.Mapping;
using MindTrail.WebApi.RequestModels;
using MindTrail.WebAuth.Attributes;

namespace MindTrail.WebApi.Controllers;

/// <summary>
/// Manages operations with persons.
/// </summary>
[ApiController]
[ApiKeyRequired]
[Route("api/mind-trail/v1/persons")]
public class PersonController(
    IHttpErrorResultProvider errorProvider,
    IPersonAppService personService)
    : ControllerBase
{
    /// <summary>
    /// Returns a paged list of <see cref="PersonDto"/>.
    /// </summary>
    /// <param name="query">Parameters for querying.</param>
    /// <returns>Paged <see cref="PersonDto"/> collection.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedDto<PersonDto>))]
    public async Task<IActionResult> GetPersons([FromQuery] PersonQueryModel query)
    {
        var persons = await personService.GetPersonsAsync(query.ToAppModel());

        return Ok(persons);
    }

    /// <summary>
    /// Creates a new person.
    /// </summary>
    /// <param name="model">The model to create a person.</param>
    /// <returns>The created person.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PersonDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
    [Produces("application/json")]
    public async Task<IActionResult> CreatePerson([FromBody, Required] PersonCreationModel model)
    {
        try
        {
            var createdPerson = await personService.CreatePersonAsync(model.ToAppModel());

            return CreatedAtAction(nameof(CreatePerson), createdPerson);
        }
        catch (DomainException ex)
        {
            switch (ex)
            {
                case BirthYearOutOfRangeException e:
                    return errorProvider.ToBadRequest(e, nameof(model.BirthYear));
                case PersonNameTooLongException e:
                    return errorProvider.ToBadRequest(e, nameof(model.FullName));
                case PersonDuplicateException e:
                    return errorProvider.ToConflict(e);
                case CountryNotFoundException e:
                    return errorProvider.ToConflict(e);
                default: throw;
            }
        }
    }
}
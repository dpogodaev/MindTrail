using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.ApplicationContracts.Interfaces;
using MindTrail.DomainShared.Exceptions;
using MindTrail.DomainShared.Exceptions.Base;
using MindTrail.WebApi.Abstractions.Providers;
using MindTrail.WebApi.Builders;
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
    IRequestSender requestSender,
    IHttpErrorResultProvider errorProvider)
    : ControllerBase
{
    /// <summary>
    /// Returns a paged list of <see cref="PersonDto"/>.
    /// </summary>
    /// <param name="model">The model to query a list of persons.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>Paged <see cref="PersonDto"/> collection.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedDto<PersonDto>))]
    [Produces("application/json")]
    public async Task<ActionResult<PagedDto<PersonDto>>> GetPersons(
        [FromQuery] PersonQueryModel model,
        CancellationToken cancellationToken)
    {
        var query = PersonQueryBuilder.BuildGetPersonsQuery(model);

        var result = await requestSender.Send(query, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Returns a person by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the person to retrieve.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The requested <see cref="PersonDto"/>.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<PersonDto>> GetPersonById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = PersonQueryBuilder.BuildGetPersonByIdQuery(id);

        var person = await requestSender.Send(query, cancellationToken);

        return person is null
            ? errorProvider.ToNotFound()
            : Ok(person);
    }

    /// <summary>
    /// Creates a new person.
    /// </summary>
    /// <param name="model">The model to create a person.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The ID of the created person.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
    [Produces("application/json")]
    public async Task<ActionResult<Guid>> CreatePerson(
        [FromBody, Required] PersonCreationModel model,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = PersonCommandBuilder.BuildCreatePersonCommand(model);

            var createdPersonId = await requestSender.Send(command, cancellationToken);

            return CreatedAtAction(
                nameof(GetPersonById),
                new { id = createdPersonId },
                createdPersonId);
        }
        catch (DomainException domainException)
        {
            switch (domainException)
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

    /// <summary>
    /// Updates an existing person.
    /// </summary>
    /// <param name="id">The ID of the person to update.</param>
    /// <param name="model">The model to update the person.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>
    /// A <see cref="NoContentResult"/> if the person was updated successfully;
    /// otherwise, an appropriate error response.
    /// </returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> UpdatePerson(
        Guid id,
        [FromBody, Required] PersonUpdateModel model,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = PersonCommandBuilder.BuildUpdatePersonCommand(id, model);

            await requestSender.Send(command, cancellationToken);

            return NoContent();
        }
        catch (DomainException domainException)
        {
            switch (domainException)
            {
                case PersonNotFoundException e:
                    return errorProvider.ToNotFound(e);
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

    /// <summary>
    /// Deletes a person.
    /// </summary>
    /// <param name="id">The ID of the person to delete.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>
    /// A <see cref="NoContentResult"/> if the person was deleted successfully;
    /// otherwise, an appropriate error response.
    /// </returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> DeletePerson(
        Guid id,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = PersonCommandBuilder.BuildDeletePersonCommand(id);

            await requestSender.Send(command, cancellationToken);

            return NoContent();
        }
        catch (PersonNotFoundException e)
        {
            return errorProvider.ToNotFound(e);
        }
    }
}
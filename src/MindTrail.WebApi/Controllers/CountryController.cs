using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.ApplicationContracts.Interfaces;
using MindTrail.WebApi.Builders;
using MindTrail.WebApi.Models.Countries;
using MindTrail.WebAuth.Attributes;

namespace MindTrail.WebApi.Controllers;

/// <summary>
/// Retrieves country information.
/// </summary>
[ApiController]
[ApiKeyRequired]
[Route("api/mind-trail/v1/countries")]
public class CountryController(IRequestSender requestSender)
    : ControllerBase
{
    /// <summary>
    /// Returns a paged list of <see cref="CountryDto"/>.
    /// </summary>
    /// <param name="model">The model to query a list of countries.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>Paged <see cref="CountryDto"/> collection.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedDto<CountryDto>))]
    [Produces("application/json")]
    public async Task<IActionResult> GetCountries(
        [FromQuery] CountryQueryModel model,
        CancellationToken cancellationToken)
    {
        var query = CountryQueryBuilder.BuildGetCountriesQuery(model);
        var countries = await requestSender.Send(query, cancellationToken);

        return Ok(countries);
    }
}
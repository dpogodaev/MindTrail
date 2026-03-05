using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.ApplicationContracts.Interfaces.Services;
using MindTrail.WebApi.Mapping;
using MindTrail.WebApi.RequestModels;
using MindTrail.WebAuth.Attributes;

namespace MindTrail.WebApi.Controllers;

/// <summary>
/// Retrieves country information.
/// </summary>
[ApiController]
[ApiKeyRequired]
[Route("api/mind-trail/v1/countries")]
public class CountryController(ICountryAppService countryService)
    : ControllerBase
{
    /// <summary>
    /// Returns a paged list of <see cref="CountryDto"/>.
    /// </summary>
    /// <param name="filter">Parameters for querying.</param>
    /// <returns>Paged <see cref="CountryDto"/> collection.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedDto<CountryDto>))]
    [Produces("application/json")]
    public async Task<IActionResult> GetCountries([FromQuery] CountryFilterModel filter)
    {
        var result = await countryService.GetCountriesAsync(filter.ToAppModel());

        return Ok(result);
    }
}
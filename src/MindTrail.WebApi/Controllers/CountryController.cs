using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindTrail.AppServices.Interfaces.Services;
using MindTrail.DomainEntities.Entities;
using MindTrail.DomainServices.Filters;
using MindTrail.WebApi.Dtos;
using MindTrail.WebApi.RequestModels;
using MindTrail.WebAuth.Attributes;

namespace MindTrail.WebApi.Controllers;

/// <summary>
/// Used to get information about countries.
/// </summary>
[ApiController]
[ApiKeyRequired]
[Route("api/mind-trail/v1/countries")]
public class CountryController(ICountryAppService countryService)
    : ControllerBase
{
    /// <summary>
    /// Returns a list of countries.
    /// </summary>
    /// <param name="filter">Used for filtering and pagination of countries.</param>
    /// <returns>A paged result containing a collection of countries.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResultDto<CountryDto>))]
    [Produces("application/json")]
    public async Task<IActionResult> GetCountries([FromQuery] CountryFilterModel filter)
    {
        var result = MapDomainEntityToDto(
            await countryService.GetCountriesAsync(
                MapModelToDomainEntity(filter)));

        return Ok(result);
    }

    private static CountryFilter MapModelToDomainEntity(CountryFilterModel model)
    {
        return new CountryFilter
        {
            Name = model.Name,
            PageNumber = model.PageNumber,
            PageSize = model.PageSize,
        };
    }

    private static PagedResultDto<CountryDto> MapDomainEntityToDto(PagedResult<Country> domainEntity)
    {
        return new PagedResultDto<CountryDto>
        {
            Items = domainEntity.Items.Select(MapDomainEntityToDto),
            PageNumber = domainEntity.PageNumber,
            PageSize = domainEntity.PageSize,
            TotalCount = domainEntity.TotalCount,
        };
    }

    private static CountryDto MapDomainEntityToDto(Country domainEntity)
    {
        return new CountryDto
        {
            Id = domainEntity.Id,
            Code = domainEntity.Code,
            Name = domainEntity.Name,
        };
    }
}
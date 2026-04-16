using System.Threading.Tasks;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.ApplicationContracts.RequestModels;

namespace MindTrail.Application.Abstractions.QueryServices;

public interface ICountryQueryService
{
    Task<PagedDto<CountryDto>> GetCountriesAsync(CountryQueryModel filter);
}
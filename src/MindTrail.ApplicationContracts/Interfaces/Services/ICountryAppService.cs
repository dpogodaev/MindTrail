using System.Threading.Tasks;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.ApplicationContracts.RequestModels;

namespace MindTrail.ApplicationContracts.Interfaces.Services;

public interface ICountryAppService
{
    Task<PagedDto<CountryDto>> GetCountriesAsync(CountryFilterModel filter);
}
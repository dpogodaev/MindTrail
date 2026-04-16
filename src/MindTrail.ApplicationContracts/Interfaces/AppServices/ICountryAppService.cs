using System.Threading.Tasks;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.ApplicationContracts.RequestModels;

namespace MindTrail.ApplicationContracts.Interfaces.AppServices;

public interface ICountryAppService
{
    Task<PagedDto<CountryDto>> GetCountriesAsync(CountryQueryModel filter);
}
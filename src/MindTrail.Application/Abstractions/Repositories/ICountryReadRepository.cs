using System.Threading.Tasks;
using MindTrail.ApplicationContracts.Dtos;
using MindTrail.ApplicationContracts.RequestModels;

namespace MindTrail.Application.Abstractions.Repositories;

public interface ICountryReadRepository
{
    Task<PagedDto<CountryDto>> GetCountriesAsync(CountryFilterModel filter);
}
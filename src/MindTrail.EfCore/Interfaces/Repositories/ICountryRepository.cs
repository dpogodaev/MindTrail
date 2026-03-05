using System.Linq;
using System.Threading.Tasks;
using MindTrail.EfCore.Entities;
using MindTrail.EfCore.Filters;

namespace MindTrail.EfCore.Interfaces.Repositories;

public interface ICountryRepository
{
    Task<bool> ExistsByIdAsync(int countryId);

    IQueryable<Country> GetCountriesAsReadOnly(CountryFilter filter);
}
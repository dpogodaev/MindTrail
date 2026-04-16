using System.Threading.Tasks;

namespace MindTrail.EfCore.Interfaces.Repositories;

public interface ICountryRepository
{
    Task<bool> ExistsByIdAsync(int countryId);
}
using System.Threading.Tasks;

namespace MindTrail.Domain.Abstractions.Repositories;

public interface ICountryRepository
{
    Task<bool> ExistsByIdAsync(int id);
}
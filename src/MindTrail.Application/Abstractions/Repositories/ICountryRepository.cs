using System.Threading.Tasks;

namespace MindTrail.Application.Abstractions.Repositories;

public interface ICountryRepository
{
    Task<bool> ExistsByIdAsync(int id);
}
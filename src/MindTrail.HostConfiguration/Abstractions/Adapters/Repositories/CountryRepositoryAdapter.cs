using System.Threading.Tasks;
using MindTrail.Domain.Abstractions.Repositories;
using EfRepositories = MindTrail.EfCore.Interfaces.Repositories;

namespace MindTrail.HostConfiguration.Abstractions.Adapters.Repositories;

public class CountryRepositoryAdapter(
    EfRepositories.ICountryRepository repository)
    : ICountryRepository
{
    public async Task<bool> ExistsByIdAsync(int id)
    {
        return await repository.ExistsByIdAsync(id);
    }
}
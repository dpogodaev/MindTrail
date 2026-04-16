using System.Threading.Tasks;
using MindTrail.Application.Abstractions.Repositories;
using EfRepositories = MindTrail.EfCore.Interfaces.Repositories;

namespace MindTrail.ApplicationConfigurator.Abstractions.Adapters.Repositories;

public class CountryRepositoryAdapter(
    EfRepositories.ICountryRepository repository)
    : ICountryRepository
{
    public async Task<bool> ExistsByIdAsync(int id)
    {
        return await repository.ExistsByIdAsync(id);
    }
}
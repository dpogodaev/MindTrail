using System.Threading;
using System.Threading.Tasks;

namespace MindTrail.Application.Abstractions.Repositories;

/// <summary>
/// Provides data access operations for countries.
/// </summary>
public interface ICountryRepository
{
    /// <summary>
    /// Determines whether a country with the specified ID exists.
    /// </summary>
    /// <param name="id">The ID of the country to check.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns><c>true</c> if a country with the specified ID exists; otherwise, <c>false</c>.</returns>
    Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken = default);
}
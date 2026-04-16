using MindTrail.ApplicationContracts.RequestModels.Base;

namespace MindTrail.ApplicationContracts.RequestModels;

/// <summary>
/// Model for querying a list of persons.
/// </summary>
public sealed record PersonQueryModel : BaseQueryModel
{
    /// <summary>
    /// Gets a model for filtering.
    /// </summary>
    /// <remarks>If <c>null</c>, filtering is not applied.</remarks>
    public PersonFilterModel? Filter { get; init; }

    /// <summary>
    /// Gets a model for sorting.
    /// </summary>
    /// <remarks>
    /// If <c>null</c>, sorting is applied by the time an entry was added in descending order.
    /// </remarks>
    public PersonSortingModel? Sorting { get; init; }
}
using MindTrail.ApplicationContracts.RequestModels.Base;

namespace MindTrail.ApplicationContracts.RequestModels;

/// <summary>
/// Model for querying a list of countries.
/// </summary>
public record CountryQueryModel : BaseQueryModel
{
    /// <summary>
    /// Gets a model for filtering.
    /// </summary>
    /// <remarks>If <c>null</c>, filtering is not applied.</remarks>
    public CountryFilterModel? Filter { get; init; }

    /// <summary>
    /// Gets a model for sorting.
    /// </summary>
    /// <remarks>
    /// If <c>null</c>, sorting is applied by name in ascending order.
    /// </remarks>
    public CountrySortingModel? Sorting { get; init; }
}
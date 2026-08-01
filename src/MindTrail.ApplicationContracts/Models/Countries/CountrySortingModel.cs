using MindTrail.ApplicationContracts.Enums;
using MindTrail.ApplicationContracts.Models.Base;

namespace MindTrail.ApplicationContracts.Models.Countries;

/// <summary>
/// Model for sorting countries.
/// </summary>
public sealed record CountrySortingModel : SortingModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CountrySortingModel"/> class.
    /// </summary>
    /// <param name="field">The field to sort by.</param>
    /// <param name="direction">The sort direction.</param>
    /// <remarks>If <paramref name="direction"/> is <c>null</c>, sorting is applied in ascending order.</remarks>
    public CountrySortingModel(
        CountrySortingFieldType field,
        SortDirectionType? direction = DefaultSortDirection)
        : base(direction)
    {
        Field = field;
    }

    /// <summary>
    /// Gets the field to sort by.
    /// </summary>
    public CountrySortingFieldType Field { get; }
}
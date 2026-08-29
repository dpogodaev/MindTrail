using MindTrail.ApplicationContracts.Enums;

namespace MindTrail.ApplicationContracts.Models.Base;

/// <summary>
/// The base model for sorting operations.
/// </summary>
public abstract record SortingModel
{
    protected const SortDirectionType DefaultSortDirection = SortDirectionType.Asc;

    /// <summary>
    /// Initializes a new instance of the <see cref="SortingModel"/> class.
    /// </summary>
    /// <param name="direction">The sort direction. Optional.</param>
    /// <remarks>If <paramref name="direction"/> is <c>null</c>, sorting is applied in ascending order.</remarks>
    protected SortingModel(SortDirectionType? direction = DefaultSortDirection)
    {
        Direction = direction ?? DefaultSortDirection;
    }

    /// <summary>
    /// Gets the direction to sort in.
    /// </summary>
    public SortDirectionType Direction { get; init; }
}
using MindTrail.ApplicationContracts.Enums;
using MindTrail.ApplicationContracts.Models.Base;

namespace MindTrail.ApplicationContracts.Models.Cards;

/// <summary>
/// Model for sorting cards.
/// </summary>
public sealed record CardSortingModel : SortingModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CardSortingModel"/> class.
    /// </summary>
    /// <param name="field">The field to sort by.</param>
    /// <param name="direction">The sort direction. Optional.</param>
    /// <remarks>If <paramref name="direction"/> is <c>null</c>, sorting is applied in ascending order.</remarks>
    public CardSortingModel(
        CardSortingFieldType field,
        SortDirectionType? direction = DefaultSortDirection)
        : base(direction)
    {
        Field = field;
    }

    /// <summary>
    /// Gets the field to sort by.
    /// </summary>
    public CardSortingFieldType Field { get; }
}
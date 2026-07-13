using MindTrail.ApplicationContracts.Enums;
using MindTrail.ApplicationContracts.Requests.Queries.Base;

namespace MindTrail.ApplicationContracts.Requests.Queries.Persons;

/// <summary>
/// Model for sorting persons.
/// </summary>
public sealed record PersonSortingModel : SortingModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PersonSortingModel"/> class.
    /// </summary>
    /// <param name="field">The field to sort by.</param>
    /// <param name="direction">The sort direction.</param>
    /// <remarks>If <paramref name="direction"/> is <c>null</c>, sorting is applied in ascending order.</remarks>
    public PersonSortingModel(
        PersonSortingFieldType field,
        SortDirectionType? direction = DefaultSortDirection)
        : base(direction)
    {
        Field = field;
    }

    /// <summary>
    /// Gets the field to sort by.
    /// </summary>
    public PersonSortingFieldType Field { get; }
}
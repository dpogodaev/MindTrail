using MindTrail.ApplicationContracts.Enums;
using MindTrail.ApplicationContracts.RequestModels.Base;

namespace MindTrail.ApplicationContracts.RequestModels;

public sealed record PersonSortingModel : BaseSortingModel
{
    public PersonSortingModel(PersonSortingFieldType field, SortDirectionType? direction = DefaultSortDirection)
        : base(direction)
    {
        Field = field;
    }

    public PersonSortingFieldType Field { get; }
}
using MindTrail.ApplicationContracts.Enums;
using MindTrail.ApplicationContracts.RequestModels.Base;

namespace MindTrail.ApplicationContracts.RequestModels;

public sealed record CountrySortingModel : BaseSortingModel
{
    public CountrySortingModel(CountrySortingFieldType field, SortDirectionType? direction = DefaultSortDirection)
        : base(direction)
    {
        Field = field;
    }

    public CountrySortingFieldType Field { get; }
}
using MindTrail.ApplicationContracts.Enums;

namespace MindTrail.ApplicationContracts.Models.Base;

public abstract record SortingModel
{
    protected const SortDirectionType DefaultSortDirection = SortDirectionType.Asc;

    protected SortingModel(SortDirectionType? direction = DefaultSortDirection)
    {
        Direction = direction ?? DefaultSortDirection;
    }

    public SortDirectionType Direction { get; init; }
}
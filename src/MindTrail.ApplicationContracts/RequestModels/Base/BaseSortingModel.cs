using MindTrail.ApplicationContracts.Enums;

namespace MindTrail.ApplicationContracts.RequestModels.Base;

public abstract record BaseSortingModel
{
    protected const SortDirectionType DefaultSortDirection = SortDirectionType.Asc;

    protected BaseSortingModel(SortDirectionType? direction = DefaultSortDirection)
    {
        Direction = direction ?? DefaultSortDirection;
    }

    public SortDirectionType Direction { get; init; }
}
using System;
using MindTrail.WebApi.RequestModels;
using AppLayerModels = MindTrail.ApplicationContracts.RequestModels;

namespace MindTrail.WebApi.Mapping;

internal static class PersonMapping
{
    public static AppLayerModels.PersonCreationModel ToAppModel(this PersonCreationModel model)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));

        return new AppLayerModels.PersonCreationModel
        {
            FullName = model.FullName,
            BirthYear = model.BirthYear,
            BirthCountryId = model.BirthCountryId,
        };
    }

    public static AppLayerModels.PersonQueryModel ToAppModel(this PersonQueryModel model)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));

        var paginationModel = new AppLayerModels.PaginationModel(model.PageNumber, model.PageSize);

        var filterModel = string.IsNullOrEmpty(model.FullName) && model.BirthYear == null
            ? null
            : new AppLayerModels.PersonFilterModel(model.FullName, model.BirthYear);

        var searchModel = string.IsNullOrEmpty(model.TextSearchQuery)
            ? null
            : new AppLayerModels.TextSearchModel(model.TextSearchQuery, model.TextSearchCaseSensitive);

        var sortingModel = model.SortField == null
            ? null
            : new AppLayerModels.PersonSortingModel(model.SortField.Value, model.SortDirection);

        return new AppLayerModels.PersonQueryModel
        {
            Pagination = paginationModel,
            Filter = filterModel,
            Search = searchModel,
            Sorting = sortingModel,
        };
    }
}
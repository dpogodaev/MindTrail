using System;
using MindTrail.WebApi.RequestModels;
using AppLayerModels = MindTrail.ApplicationContracts.RequestModels;

namespace MindTrail.WebApi.Mapping;

internal static class CountryMapping
{
    public static AppLayerModels.CountryQueryModel ToAppModel(this CountryQueryModel model)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));

        var paginationModel = new AppLayerModels.PaginationModel(model.PageNumber, model.PageSize);

        var filterModel = string.IsNullOrEmpty(model.Code) && string.IsNullOrEmpty(model.Name)
            ? null
            : new AppLayerModels.CountryFilterModel(model.Code, model.Name);

        var searchModel = string.IsNullOrEmpty(model.TextSearchQuery)
            ? null
            : new AppLayerModels.TextSearchModel(model.TextSearchQuery, model.TextSearchCaseSensitive);

        var sortingModel = model.SortField == null
            ? null
            : new AppLayerModels.CountrySortingModel(model.SortField.Value, model.SortDirection);

        return new AppLayerModels.CountryQueryModel
        {
            Pagination = paginationModel,
            Filter = filterModel,
            Search = searchModel,
            Sorting = sortingModel,
        };
    }
}
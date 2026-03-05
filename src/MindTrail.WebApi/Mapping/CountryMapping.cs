using System;
using MindTrail.WebApi.RequestModels;
using AppLayerModels = MindTrail.ApplicationContracts.RequestModels;

namespace MindTrail.WebApi.Mapping;

internal static class CountryMapping
{
    public static AppLayerModels.CountryFilterModel ToAppModel(this CountryFilterModel model)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));

        return new AppLayerModels.CountryFilterModel
        {
            PageNumber = model.PageNumber,
            PageSize = model.PageSize,
            Search = model.Search,
            Sorting = model.Sorting,
        };
    }
}
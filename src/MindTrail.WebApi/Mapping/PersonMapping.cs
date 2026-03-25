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

    public static AppLayerModels.PersonFilterModel ToAppModel(this PersonFilterModel model)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));

        return new AppLayerModels.PersonFilterModel
        {
            PageNumber = model.PageNumber,
            PageSize = model.PageSize,
            Search = model.Search,
            Sorting = model.Sorting,
        };
    }
}
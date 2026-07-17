using System;
using MindTrail.ApplicationContracts.Requests.Commands;
using MindTrail.WebApi.RequestModels;

namespace MindTrail.WebApi.Builders;

public static class PersonCommandBuilder
{
    public static CreatePersonCommand BuildCreatePersonCommand(PersonCreationModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        return new CreatePersonCommand
        {
            FullName = model.FullName,
            BirthYear = model.BirthYear,
            BirthCountryId = model.BirthCountryId,
        };
    }

    public static UpdatePersonCommand BuildUpdatePersonCommand(Guid id, PersonUpdateModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        return new UpdatePersonCommand
        {
            Id = id,
            FullName = model.FullName,
            BirthYear = model.BirthYear,
            BirthCountryId = model.BirthCountryId,
        };
    }

    public static DeletePersonCommand BuildDeletePersonCommand(Guid id)
    {
        return new DeletePersonCommand
        {
            Id = id,
        };
    }
}
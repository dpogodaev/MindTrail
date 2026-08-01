using System;
using MindTrail.ApplicationContracts.Commands.Persons;
using MindTrail.WebApi.Models.Persons;

namespace MindTrail.WebApi.Builders;

/// <summary>
/// Builds command objects for person operations from web API models.
/// </summary>
public static class PersonCommandBuilder
{
    /// <summary>
    /// Builds a <see cref="CreatePersonCommand"/> from the specified model.
    /// </summary>
    /// <param name="model">The model to create a person.</param>
    /// <returns>The <see cref="CreatePersonCommand"/> to send.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="model"/> is <c>null</c>.</exception>
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

    /// <summary>
    /// Builds an <see cref="UpdatePersonCommand"/> from the specified ID and model.
    /// </summary>
    /// <param name="id">The ID of the person to update.</param>
    /// <param name="model">The model to update the person.</param>
    /// <returns>The <see cref="UpdatePersonCommand"/> to send.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="model"/> is <c>null</c>.</exception>
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

    /// <summary>
    /// Builds a <see cref="DeletePersonCommand"/> for the specified ID.
    /// </summary>
    /// <param name="id">The ID of the person to delete.</param>
    /// <returns>The <see cref="DeletePersonCommand"/> to send.</returns>
    public static DeletePersonCommand BuildDeletePersonCommand(Guid id)
    {
        return new DeletePersonCommand
        {
            Id = id,
        };
    }
}
using System;
using MindTrail.ApplicationContracts.Commands.Cards;
using MindTrail.WebApi.Models.Cards;

namespace MindTrail.WebApi.Builders;

/// <summary>
/// Builds command objects for card operations from web API models.
/// </summary>
public static class CardCommandBuilder
{
    /// <summary>
    /// Builds a <see cref="CreateCardCommand"/> from the specified model.
    /// </summary>
    /// <param name="model">The model to create a card.</param>
    /// <returns>The <see cref="CreateCardCommand"/> to send.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="model"/> is <c>null</c>.</exception>
    public static CreateCardCommand BuildCreateCardCommand(CardCreationModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        return new CreateCardCommand
        {
            Title = model.Title,
            Content = model.Content,
        };
    }

    /// <summary>
    /// Builds an <see cref="UpdateCardCommand"/> from the specified number and model.
    /// </summary>
    /// <param name="number">The number of the card to update.</param>
    /// <param name="model">The model to update the card.</param>
    /// <returns>The <see cref="UpdateCardCommand"/> to send.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="model"/> is <c>null</c>.</exception>
    public static UpdateCardCommand BuildUpdateCardCommand(int number, CardUpdateModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        return new UpdateCardCommand
        {
            Number = number,
            Title = model.Title,
            Content = model.Content,
        };
    }

    /// <summary>
    /// Builds a <see cref="DeleteCardCommand"/> for the specified number.
    /// </summary>
    /// <param name="number">The number of the card to delete.</param>
    /// <returns>The <see cref="DeleteCardCommand"/> to send.</returns>
    public static DeleteCardCommand BuildDeleteCardCommand(int number)
    {
        return new DeleteCardCommand
        {
            Number = number,
        };
    }
}
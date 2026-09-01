using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindTrail.Domain.Entities;
using MindTrail.Domain.ValueObjects;

namespace MindTrail.Domain.Tests.EntitiesTests;

/// <summary>
/// Tests for <see cref="Card"/>.
/// </summary>
[TestClass]
public class CardTests
{
    private static readonly DateTime CurrentTime = new(2020, 1, 1);

    /// <summary>
    /// Ensures that a <see cref="Card"/> initializes all properties from the provided arguments when created.
    /// </summary>
    [TestMethod]
    public void Card_must_initialize_properties_when_created()
    {
        // Arrange
        const int unassignedId = 0;
        const string title = "Title";
        const string content = "Content";

        // Act
        var card = Card.Create(
            CurrentTime,
            CardTitle.Create(title),
            CardContent.Create(content));

        // Assert
        Assert.AreEqual(unassignedId, card.Id);
        Assert.AreEqual(title, card.Title);
        Assert.IsNotNull(card.Content);
        Assert.AreEqual(content, card.Content);
        Assert.AreEqual(CurrentTime, card.CreatedAt);
        Assert.IsNull(card.EditedAt);
    }

    /// <summary>
    /// Ensures that a <see cref="Card"/> is identified by its number:
    /// <see cref="Card.Number"/> returns the same value as <see cref="Card.Id"/>.
    /// </summary>
    [TestMethod]
    public void Card_Number_must_equal_Id()
    {
        // Act
        var card = Card.Create(
            CurrentTime,
            CardTitle.Create("Title"),
            content: null);

        // Assert
        Assert.AreEqual(card.Id, card.Number);
    }

    /// <summary>
    /// Ensures that a <see cref="Card"/> can be created without content.
    /// </summary>
    [TestMethod]
    public void Card_can_be_created_without_content()
    {
        // Act
        var card = Card.Create(
            CurrentTime,
            CardTitle.Create("Title"),
            content: null);

        // Assert
        Assert.IsNull(card.Content);
    }

    /// <summary>
    /// Ensures that changing the title of a <see cref="Card"/> updates it and records the edit time.
    /// </summary>
    [TestMethod]
    public void Card_must_update_title_and_record_edit_time_when_title_changed()
    {
        // Arrange
        var card = Card.Create(
            CurrentTime,
            CardTitle.Create("Title"),
            CardContent.Create("Content"));

        const string newTitle = "New Title";
        var editedAt = CurrentTime.AddDays(1);

        // Act
        card.ChangeTitle(CardTitle.Create(newTitle), editedAt);

        // Assert
        Assert.AreEqual(newTitle, card.Title.Value);
        Assert.AreEqual(editedAt, card.EditedAt);
    }

    /// <summary>
    /// Ensures that changing the content of a <see cref="Card"/> updates it and records the edit time.
    /// </summary>
    [TestMethod]
    public void Card_must_update_content_and_record_edit_time_when_content_changed()
    {
        // Arrange
        var card = Card.Create(
            CurrentTime,
            CardTitle.Create("Title"),
            CardContent.Create("Content"));

        const string newContent = "New Content";
        var editedAt = CurrentTime.AddDays(1);

        // Act
        card.ChangeContent(CardContent.Create(newContent), editedAt);

        // Assert
        Assert.IsNotNull(card.Content);
        Assert.AreEqual(newContent, card.Content.Value);
        Assert.AreEqual(editedAt, card.EditedAt);
    }

    /// <summary>
    /// Ensures that the content of a <see cref="Card"/> can be cleared and the edit time is recorded.
    /// </summary>
    [TestMethod]
    public void Card_must_allow_clearing_content()
    {
        // Arrange
        var card = Card.Create(
            CurrentTime,
            CardTitle.Create("Title"),
            CardContent.Create("Content"));

        var editedAt = CurrentTime.AddDays(1);

        // Act
        card.ChangeContent(null, editedAt);

        // Assert
        Assert.IsNull(card.Content);
        Assert.AreEqual(editedAt, card.EditedAt);
    }
}
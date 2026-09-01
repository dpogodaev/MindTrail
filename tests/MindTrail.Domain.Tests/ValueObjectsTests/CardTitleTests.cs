using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindTrail.Domain.ValueObjects;
using MindTrail.DomainShared.Exceptions.Cards;

namespace MindTrail.Domain.Tests.ValueObjectsTests;

/// <summary>
/// Tests for <see cref="CardTitle"/>.
/// </summary>
[TestClass]
public class CardTitleTests
{
    /// <summary>
    /// Ensures that the <see cref="CardTitle.Value"/> property returns the original title value.
    /// </summary>
    [TestMethod]
    public void CardTitle_Value_property_must_return_the_original_title()
    {
        // Arrange
        const string originalTitle = "My card";

        // Act
        var title = CardTitle.Create(originalTitle);

        // Assert
        Assert.AreEqual(originalTitle, title.Value);
    }

    /// <summary>
    /// Ensures that a <see cref="CardTitle"/> trims surrounding whitespace from the value.
    /// </summary>
    [TestMethod]
    public void CardTitle_must_trim_surrounding_whitespace()
    {
        // Arrange
        const string untrimmedTitle = "  My card  ";
        const string trimmedTitle = "My card";

        // Act
        var title = CardTitle.Create(untrimmedTitle);

        // Assert
        Assert.AreEqual(trimmedTitle, title.Value);
    }

    /// <summary>
    /// Ensures that a <see cref="CardTitle"/> can be created with a value of the maximum allowed length.
    /// </summary>
    [TestMethod]
    public void CardTitle_must_accept_MaxLength_as_valid_upper_boundary()
    {
        // Arrange
        var value = new string('a', CardTitle.MaxLength);

        // Act
        var title = CardTitle.Create(value);

        // Assert
        Assert.AreEqual(value, title.Value);
    }

    /// <summary>
    /// Ensures that a <see cref="CardTitle"/> cannot be created with a value longer than <see cref="CardTitle.MaxLength"/>.
    /// Violation results in a <see cref="CardTitleTooLongException"/>.
    /// </summary>
    [TestMethod]
    public void CardTitle_creation_must_reject_values_longer_than_MaxLength()
    {
        // Arrange
        var value = new string('a', CardTitle.MaxLength + 1);

        // Act
        CardTitle CreateTitle() => CardTitle.Create(value);

        // Assert
        var exception = Assert.Throws<CardTitleTooLongException>(CreateTitle);

        Assert.AreEqual(value.Length, exception.SpecifiedTitleLength);
        Assert.AreEqual(CardTitle.MaxLength, exception.MaxLength);
    }

    /// <summary>
    /// Ensures that a <see cref="CardTitle"/> cannot be created from a <c>null</c> value.
    /// </summary>
    [TestMethod]
    public void CardTitle_creation_must_reject_null()
    {
        // Act
        CardTitle CreateTitle() => CardTitle.Create(null!);

        // Assert
        Assert.Throws<ArgumentNullException>(CreateTitle);
    }

    /// <summary>
    /// Ensures that a <see cref="CardTitle"/> cannot be created from an empty or whitespace-only value.
    /// </summary>
    /// <param name="value">The invalid value.</param>
    [TestMethod]
    [DataRow("")]
    [DataRow("   ")]
    public void CardTitle_creation_must_reject_empty_or_whitespace(string value)
    {
        // Act
        CardTitle CreateTitle() => CardTitle.Create(value);

        // Assert
        Assert.Throws<ArgumentException>(CreateTitle);
    }

    /// <summary>
    /// Ensures that a <see cref="CardTitle"/> instance can be implicitly converted to <see cref="string"/>.
    /// </summary>
    [TestMethod]
    public void CardTitle_must_support_implicit_conversion_to_string()
    {
        // Arrange
        const string value = "Implicit";
        var title = CardTitle.Create(value);

        // Act
        string result = title;

        // Assert
        Assert.AreEqual(value, result);
    }

    /// <summary>
    /// Ensures that two <see cref="CardTitle"/> instances with the same value are equal.
    /// </summary>
    [TestMethod]
    public void CardTitle_instances_with_the_same_value_must_be_equal()
    {
        // Arrange
        const string value = "Same value";

        // Act
        var first = CardTitle.Create(value);
        var second = CardTitle.Create(value);

        // Assert
        Assert.AreEqual(first, second);
    }
}

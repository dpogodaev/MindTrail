using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindTrail.Domain.ValueObjects;
using MindTrail.DomainShared.Exceptions.Cards;

namespace MindTrail.Domain.Tests.ValueObjectsTests;

/// <summary>
/// Tests for <see cref="CardContent"/>.
/// </summary>
[TestClass]
public class CardContentTests
{
    /// <summary>
    /// Ensures that the <see cref="CardContent.Value"/> property returns the original content value.
    /// </summary>
    [TestMethod]
    public void CardContent_Value_property_must_return_the_original_content()
    {
        // Arrange
        const string originalContent = "Some content";

        // Act
        var content = CardContent.Create(originalContent);

        // Assert
        Assert.IsNotNull(content);
        Assert.AreEqual(originalContent, content.Value);
    }

    /// <summary>
    /// Ensures that creating a <see cref="CardContent"/> from a <c>null</c> value returns <c>null</c> rather than an instance.
    /// </summary>
    [TestMethod]
    public void CardContent_must_be_null_when_created_from_null_value()
    {
        // Act
        var content = CardContent.Create(null);

        // Assert
        Assert.IsNull(content);
    }

    /// <summary>
    /// Ensures that a <see cref="CardContent"/> preserves surrounding whitespace (unlike the card title).
    /// </summary>
    [TestMethod]
    public void CardContent_must_not_trim_surrounding_whitespace()
    {
        // Arrange
        const string untrimmedContent = "  Some content  ";

        // Act
        var content = CardContent.Create(untrimmedContent);

        // Assert
        Assert.IsNotNull(content);
        Assert.AreEqual(untrimmedContent, content.Value);
    }

    /// <summary>
    /// Ensures that a <see cref="CardContent"/> can be created with a value of the maximum allowed length.
    /// </summary>
    [TestMethod]
    public void CardContent_must_accept_MaxLength_as_valid_upper_boundary()
    {
        // Arrange
        var value = new string('a', CardContent.MaxLength);

        // Act
        var content = CardContent.Create(value);

        // Assert
        Assert.IsNotNull(content);
        Assert.AreEqual(value, content.Value);
    }

    /// <summary>
    /// Ensures that a <see cref="CardContent"/> cannot be created with a value longer than <see cref="CardContent.MaxLength"/>.
    /// Violation results in a <see cref="CardContentTooLongException"/>.
    /// </summary>
    [TestMethod]
    public void CardContent_creation_must_reject_values_longer_than_MaxLength()
    {
        // Arrange
        var value = new string('a', CardContent.MaxLength + 1);

        // Act
        CardContent? CreateContent() => CardContent.Create(value);

        // Assert
        var exception = Assert.Throws<CardContentTooLongException>(CreateContent);

        Assert.AreEqual(value.Length, exception.SpecifiedContentLength);
        Assert.AreEqual(CardContent.MaxLength, exception.MaxLength);
    }

    /// <summary>
    /// Ensures that a <see cref="CardContent"/> cannot be created from an empty or whitespace-only value.
    /// </summary>
    /// <param name="value">The invalid value.</param>
    [TestMethod]
    [DataRow("")]
    [DataRow("   ")]
    public void CardContent_creation_must_reject_empty_or_whitespace(string value)
    {
        // Act
        CardContent? CreateContent() => CardContent.Create(value);

        // Assert
        Assert.Throws<ArgumentException>(CreateContent);
    }

    /// <summary>
    /// Ensures that a <see cref="CardContent"/> instance can be implicitly converted to <see cref="string"/>.
    /// </summary>
    [TestMethod]
    public void CardContent_must_support_implicit_conversion_to_string()
    {
        // Arrange
        const string value = "Implicit";
        var content = CardContent.Create(value);

        // Act
        string result = content!;

        // Assert
        Assert.AreEqual(value, result);
    }

    /// <summary>
    /// Ensures that two <see cref="CardContent"/> instances with the same value are equal.
    /// </summary>
    [TestMethod]
    public void CardContent_instances_with_the_same_value_must_be_equal()
    {
        // Arrange
        const string value = "Same value";

        // Act
        var first = CardContent.Create(value);
        var second = CardContent.Create(value);

        // Assert
        Assert.AreEqual(first, second);
    }
}

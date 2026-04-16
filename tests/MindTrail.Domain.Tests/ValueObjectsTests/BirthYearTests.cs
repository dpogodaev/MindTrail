using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindTrail.Domain.ValueObjects;
using MindTrail.DomainShared.Exceptions;

namespace MindTrail.Domain.Tests.ValueObjectsTests;

/// <summary>
/// Tests for <see cref="BirthYear"/>.
/// </summary>
[TestClass]
public class BirthYearTests
{
    /// <summary>
    /// Ensures that the <see cref="BirthYear.Value"/> property returns the original year value passed to the constructor.
    /// </summary>
    [TestMethod]
    public void BirthYear_Value_property_must_return_the_original_year()
    {
        // Arrange
        const int originalYear = 2000;
        var currentTime = new DateTime(2020, 1, 1);

        // Act
        var birthYear = new BirthYear(originalYear, currentTime);
        var retrievedValue = birthYear.Value;

        // Assert
        Assert.AreEqual(originalYear, retrievedValue);
    }

    /// <summary>
    /// Ensures that a <see cref="BirthYear"/> can be created with the current year as the upper boundary.
    /// </summary>
    [TestMethod]
    public void BirthYear_must_accept_current_year_as_valid_upper_boundary()
    {
        // Arrange
        var currentTime = new DateTime(2020, 1, 1);
        var currentYear = currentTime.Year;

        // Act
        var birthYear = new BirthYear(currentYear, currentTime);

        // Assert
        Assert.AreEqual(currentYear, birthYear.Value);
    }

    /// <summary>
    /// Ensures that a <see cref="BirthYear"/> can be created with the minimum allowed value (<see cref="BirthYear.MinBirthYear"/>).
    /// </summary>
    [TestMethod]
    public void BirthYear_must_accept_MinBirthYear_as_valid_lower_boundary()
    {
        // Arrange
        var currentTime = new DateTime(2020, 1, 1);
        const int minYear = BirthYear.MinBirthYear;

        // Act
        var birthYear = new BirthYear(minYear, currentTime);

        // Assert
        Assert.AreEqual(minYear, birthYear.Value);
    }

    /// <summary>
    /// Ensures that a <see cref="BirthYear"/> can be created with a valid year within the allowed range.
    /// </summary>
    [TestMethod]
    public void BirthYear_must_be_created_successfully_when_value_is_within_valid_range()
    {
        // Arrange
        var currentTime = new DateTime(2020, 1, 1);
        const int validYear = BirthYear.MinBirthYear + 10;

        // Act
        var birthYear = new BirthYear(validYear, currentTime);

        // Assert
        Assert.AreEqual(validYear, birthYear.Value);
    }

    /// <summary>
    /// Ensures that a <see cref="BirthYear"/> cannot be created with a value less than <see cref="BirthYear.MinBirthYear"/>.
    /// Violation results in a <see cref="BirthYearOutOfRangeException"/>.
    /// </summary>
    [TestMethod]
    public void BirthYear_creation_must_reject_values_below_MinBirthYear()
    {
        // Arrange
        var currentTime = new DateTime(2020, 1, 1);
        const int invalidYear = BirthYear.MinBirthYear - 1;

        // Act
        BirthYear CreateBirthYear() => new(invalidYear, currentTime);

        // Assert
        var exception = Assert.Throws<BirthYearOutOfRangeException>(CreateBirthYear);

        Assert.AreEqual(invalidYear, exception.SpecifiedBirthYear);
        Assert.AreEqual(BirthYear.MinBirthYear, exception.MinBirthYear);
    }

    /// <summary>
    /// Ensures that a <see cref="BirthYear"/> cannot be created with a value greater than the current year.
    /// Violation results in a <see cref="BirthYearOutOfRangeException"/>.
    /// </summary>
    [TestMethod]
    public void BirthYear_creation_must_reject_values_greater_than_current_year()
    {
        // Arrange
        var currentTime = new DateTime(2020, 1, 1);
        var invalidYear = currentTime.Year + 1;

        // Act
        BirthYear CreateBirthYear() => new BirthYear(invalidYear, currentTime);

        // Assert
        var exception = Assert.Throws<BirthYearOutOfRangeException>(CreateBirthYear);
        Assert.AreEqual(invalidYear, exception.SpecifiedBirthYear);
        Assert.AreEqual(BirthYear.MinBirthYear, exception.MinBirthYear);
    }

    /// <summary>
    /// Ensures that a <see cref="BirthYear"/> instance can be implicitly converted to <see cref="int"/>.
    /// </summary>
    [TestMethod]
    public void BirthYear_must_support_implicit_conversion_to_int()
    {
        // Arrange
        var currentTime = new DateTime(2020, 1, 1);
        const int validYear = 1985;
        var birthYear = new BirthYear(validYear, currentTime);

        // Act
        int result = birthYear;

        // Assert
        Assert.AreEqual(validYear, result);
    }
}
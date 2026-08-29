using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MindTrail.Common.Tests.SystemTests;

/// <summary>
/// Tests for <see cref="JsonSerializer"/> class.
/// </summary>
[TestClass]
public class JsonSerializerTests
{
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    #region DeserializeAsync

    /// <summary>
    /// Test for <see cref="JsonSerializer.DeserializeAsync{TValue}(Stream,JsonSerializerOptions?,System.Threading.CancellationToken)"/> method.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [TestMethod]
    public async Task DeserializeAsync_ShouldDeserializeCamelCaseJsonCorrectly()
    {
        // Arrange
        var json = GetStream("{\"id\":1,\"name\":\"2\"}");

        // Act
        var obj = await JsonSerializer.DeserializeAsync<TestObject>(json, _options);

        // Assert
        Assert.IsNotNull(obj);
        Assert.AreEqual(1, obj.Id);
        Assert.AreEqual("2", obj.Name);
    }

    /// <summary>
    /// Test for <see cref="JsonSerializer.DeserializeAsync{TValue}(Stream,JsonSerializerOptions?,System.Threading.CancellationToken)"/> method.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [TestMethod]
    public async Task DeserializeAsync_ShouldDeserializePascalCaseJsonCorrectly()
    {
        // Arrange
        var json = GetStream("{\"Id\":1,\"Name\":\"2\"}");

        // Act
        var obj = await JsonSerializer.DeserializeAsync<TestObject>(json);

        // Assert
        Assert.IsNotNull(obj);
        Assert.AreEqual(1, obj.Id);
        Assert.AreEqual("2", obj.Name);
    }

    /// <summary>
    /// Test for <see cref="JsonSerializer.DeserializeAsync{TValue}(Stream,JsonSerializerOptions?,System.Threading.CancellationToken)"/> method.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [TestMethod]
    public async Task DeserializeAsync_ShouldHandleNullValuesCorrectly()
    {
        // Arrange
        var json = GetStream("{\"id\":1,\"name\":null}");

        // Act
        var obj = await JsonSerializer.DeserializeAsync<TestObject>(json, _options);

        // Assert
        Assert.IsNotNull(obj);
        Assert.AreEqual(1, obj.Id);
        Assert.IsNull(obj.Name);
    }

    #endregion

    private static MemoryStream GetStream(string json)
    {
        return new MemoryStream(Encoding.UTF8.GetBytes(json));
    }

    private record TestObject
    {
        public int Id { get; init; }

        public required string Name { get; init; }
    }
}
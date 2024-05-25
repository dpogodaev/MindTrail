using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace ThoughtGuide.Common.Tests.SystemTests;

/// <summary>
/// Tests for <see cref="JsonSerializer"/> class.
/// </summary>
public class JsonHelperTests
{
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    #region DeserializeAsync

    /// <summary>
    /// Test for <see cref="JsonSerializer.DeserializeAsync{TValue}(Stream,JsonSerializerOptions?,System.Threading.CancellationToken)"/> method.
    /// </summary>
    [Fact]
    public async Task DeserializeAsync_ShouldDeserializeCamelCaseJsonCorrectly()
    {
        // Arrange
        var json = GetStream("{\"id\":1,\"name\":\"2\"}");

        // Act
        var obj = await JsonSerializer.DeserializeAsync<TestObject>(json, _options);

        // Assert
        Assert.Equal(1, obj.Id);
        Assert.Equal("2", obj.Name);
    }

    /// <summary>
    /// Test for <see cref="JsonSerializer.DeserializeAsync{TValue}(Stream,JsonSerializerOptions?,System.Threading.CancellationToken)"/> method.
    /// </summary>
    [Fact]
    public async Task DeserializeAsync_ShouldDeserializePascalCaseJsonCorrectly()
    {
        // Arrange
        var json = GetStream("{\"Id\":1,\"Name\":\"2\"}");

        // Act
        var obj = await JsonSerializer.DeserializeAsync<TestObject>(json);

        // Assert
        Assert.Equal(1, obj.Id);
        Assert.Equal("2", obj.Name);
    }

    /// <summary>
    /// Test for <see cref="JsonSerializer.DeserializeAsync{TValue}(Stream,JsonSerializerOptions?,System.Threading.CancellationToken)"/> method.
    /// </summary>
    [Fact]
    public async Task DeserializeAsync_ShouldHandleNullValuesCorrectly()
    {
        // Arrange
        var json = GetStream("{\"id\":1,\"name\":null}");

        // Act
        var obj = await JsonSerializer.DeserializeAsync<TestObject>(json, _options);

        // Assert
        Assert.NotNull(obj);
        Assert.Equal(1, obj.Id);
        Assert.Null(obj.Name);
    }

    #endregion

    #region Private classes

    private record TestObject
    {
        public int Id { get; init; }
        public string Name { get; init; }
    }

    #endregion

    #region Private methods

    private static MemoryStream GetStream(string json)
    {
        return new MemoryStream(Encoding.UTF8.GetBytes(json));
    }

    #endregion
}
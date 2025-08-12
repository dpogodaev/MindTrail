using System.Linq;
using MindTrail.AppServices.Exceptions;
using MindTrail.Common.Extensions;
using MindTrail.WebApi.Dtos;

namespace MindTrail.WebApi.Helpers;

/// <summary>
/// Helper for responding to a request.
/// </summary>
public static class ResponseHelper
{
    /// <summary>
    /// Builds information about an invalid request.
    /// </summary>
    /// <param name="e">The source exception.</param>
    /// <returns>Information about an invalid request.</returns>
    public static BadRequestDto BuildBadRequestDto(InvalidValueException e)
    {
        return new BadRequestDto
        {
            PropertyName = GetPropertyName(e.PropertyName),
            PropertyValue = e.PropertyValue,
            Description = e.Message
        };
    }

    /// <summary>
    /// Builds information about conflict.
    /// </summary>
    /// <param name="e">The source exception.</param>
    /// <returns>Information about conflict.</returns>
    public static ConflictDto BuildConflictDto(InvalidStateException e)
    {
        return new ConflictDto
        {
            PropertyName = GetPropertyName(e.PropertyName),
            PropertyValue = e.PropertyValue,
            Description = e.Message
        };
    }

    #region Private methods

    private static string GetPropertyName(string? source)
    {
        if (string.IsNullOrEmpty(source))
        {
            return string.Empty;
        }

        var splittedPropertyName = source.Split(".").Select(x => x.FirstCharToLowerCase()).ToList();

        var propertyName = splittedPropertyName.Count == 1
            ? splittedPropertyName.First()
            : string.Join('.', splittedPropertyName);

        return propertyName;
    }

    #endregion
}
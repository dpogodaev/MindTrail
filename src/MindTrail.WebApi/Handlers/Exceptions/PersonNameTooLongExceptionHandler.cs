using MindTrail.DomainServices.Exceptions;
using MindTrail.WebApi.Builders;

namespace MindTrail.WebApi.Handlers.Exceptions;

/// <summary>
/// Provides an extension method to handle <see cref="PersonNameTooLongException"/> 
/// by converting it into a structured problem details response (RFC 7807).
/// </summary>
public static class PersonNameTooLongExceptionHandler
{
    /// <summary>
    /// Converts the given <see cref="PersonNameTooLongException"/> into a <see cref="ProblemDetailsBuilder"/>.
    /// </summary>
    /// <param name="e">The exception to handle.</param>
    /// <param name="invalidPropName">Name of the invalid property that caused the exception. Optional.</param>
    /// <returns>A configured <see cref="ProblemDetailsBuilder"/>.</returns>
    public static ProblemDetailsBuilder Handle(this PersonNameTooLongException e, string? invalidPropName = null)
    {
        var builder = new ProblemDetailsBuilder(e)
            .AddTitle("Invalid person's name")
            .AddParameter("maxLength", e.MaxLength);

        if (!string.IsNullOrEmpty(invalidPropName))
        {
            builder.AddValidationErrorDescription(invalidPropName, "Person's name is too long");
        }

        return builder;
    }
}
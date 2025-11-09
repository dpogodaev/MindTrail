using MindTrail.DomainServices.Exceptions;
using MindTrail.WebApi.Builders;

namespace MindTrail.WebApi.Handlers.Exceptions;

/// <summary>
/// Provides an extension method to handle <see cref="PersonDuplicateException"/> 
/// by converting it into a structured problem details response (RFC 7807).
/// </summary>
public static class PersonDuplicateExceptionHandler
{
    /// <summary>
    /// Converts the given <see cref="PersonDuplicateException"/> into a <see cref="ProblemDetailsBuilder"/>.
    /// </summary>
    /// <param name="e">The exception to handle.</param>
    /// <returns>A configured <see cref="ProblemDetailsBuilder"/>.</returns>
    public static ProblemDetailsBuilder Handle(this PersonDuplicateException e)
    {
        return new ProblemDetailsBuilder(e)
            .AddTitle("This person already exists")
            .AddParameter("fullName", e.FullName)
            .AddParameter("birthYear", e.BirthYear);
    }
}
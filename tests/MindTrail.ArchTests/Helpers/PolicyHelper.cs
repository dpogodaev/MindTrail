using System.Collections.Generic;
using System.IO;
using System.Reflection;
using NetArchTest.Rules;
using NetArchTest.Rules.Policies;

namespace MindTrail.ArchTests.Helpers;

/// <summary>
/// Policy rules helper.
/// </summary>
public static class PolicyHelper
{
    /// <summary>
    /// Builds a policy definition for the specified component.
    /// </summary>
    /// <param name="componentNamespace">The namespace of the component under test.</param>
    /// <param name="policyName">The policy name.</param>
    /// <param name="policyDescription">The description of the purpose of the policy.</param>
    /// <returns>The policy definition.</returns>
    public static PolicyDefinition BuildPolicyDefinition(
        string componentNamespace, string policyName, string policyDescription)
    {
        var policy = Policy.Define(policyName, policyDescription);
        var policyDefinition = policy.For(GetTypes(componentNamespace));

        return policyDefinition;
    }

    /// <summary>
    /// Builds a failure message.
    /// </summary>
    /// <param name="result">The result of testing the policy.</param>
    /// <returns>A failure message, or <c>null</c> if the policy was satisfied.</returns>
    public static string? BuildFailureMessage(PolicyResult result)
    {
        return result.IsSuccessful
            ? null
            : $"{result.Description}. Failed types: {string.Join(", ", result.FailingTypes)}";
    }

    /// <summary>
    /// Creates a list of dependencies allowed for the specified component.
    /// </summary>
    /// <param name="currentNamespace">The namespace of the component itself.</param>
    /// <param name="libs">The names of the external libraries the component is allowed to depend on.</param>
    /// <param name="components">The namespaces of the other components the component is allowed to depend on. Optional.</param>
    /// <returns>The list of allowed dependencies.</returns>
    public static string[] CreateAllowedDependenciesList(
        string currentNamespace,
        IEnumerable<string> libs,
        IEnumerable<string>? components = null)
    {
        var allowedDependenciesList = new List<string> { currentNamespace };

        allowedDependenciesList.AddRange(libs);

        if (components != null)
        {
            allowedDependenciesList.AddRange(components);
        }

        return allowedDependenciesList.ToArray();
    }

    private static Types GetTypes(string workingNamespace) =>
        Types.FromFile(Path.Combine(GetProjectPath()!, $"{workingNamespace}.dll"));

    private static string? GetProjectPath() => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
}
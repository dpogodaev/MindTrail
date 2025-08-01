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
    /// <param name="policyName">Policy name.</param>
    /// <param name="policyDescription">Description of the purpose of the policy.</param>
    /// <param name="componentNamespace">Namespace of the component under test.</param>
    /// <returns>Policy definition.</returns>
    public static PolicyDefinition BuildPolicyDefinition(
        string componentNamespace, string policyName, string policyDescription)
    {
        var policy = Policy.Define(policyName, policyDescription);
        var policyDefinition = policy.For(GetTypes(componentNamespace));

        return policyDefinition;
    }

    /// <summary>
    /// Builds failure message.
    /// </summary>
    /// <param name="result">The result of testing the policy.</param>
    /// <returns>A failure message.</returns>
    public static string BuildFailureMessage(PolicyResult result)
    {
        return result.IsSuccessful
            ? null
            : $"{result.Description}. Failed types: {string.Join(", ", result.FailingTypes)}";
    }

    #region Private methods

    private static Types GetTypes(string workingNamespace) =>
        Types.FromFile(Path.Combine(GetProjectPath(), $"{workingNamespace}.dll"));

    private static string GetProjectPath() => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

    #endregion
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindTrail.ArchTests.Constants;
using MindTrail.ArchTests.Extensions;
using MindTrail.ArchTests.Helpers;

namespace MindTrail.ArchTests.ArchTests;

/// <summary>
/// Architectural tests for <see cref="MindTrail.WebAuth"/> component.
/// </summary>
[TestClass]
[TestCategory("Architecture")]
public class WebAuthArchTests
{
    private const string CurrentNamespace = ComponentNamespaces.WebAuth;

    private static readonly string[] UsingLibs =
    [
        "System",
        "Microsoft"
    ];

    /// <summary>
    /// Ensures that the <see cref="MindTrail.WebAuth"/> component follows the dependency rules.
    /// </summary>
    [TestMethod]
    public void WebAuth_ShouldFollowDependencyRules()
    {
        // Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(
            componentNamespace: CurrentNamespace,
            policyName: "Component dependency policy",
            policyDescription: $"Enforces the dependencies of the {nameof(WebAuth)} component");

        policyDefinition.Add(
            definition: types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependenciesOtherThan(
                    PolicyHelper.CreateAllowedDependenciesList(
                        CurrentNamespace,
                        UsingLibs,
                        [
                            ComponentNamespaces.Common
                        ])),
            name: "Allowed dependencies",
            description: "The Web authorization component can only depend on common utilities");

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        foreach (var result in results)
        {
            Assert.IsTrue(result.IsSuccessful, PolicyHelper.BuildFailureMessage(result));
        }
    }

    /// <summary>
    /// Ensures that the <see cref="MindTrail.WebAuth"/> component's types follow the naming conventions.
    /// </summary>
    [TestMethod]
    public void WebAuth_ShouldFollowNamingConventions()
    {
        // Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(
            componentNamespace: CurrentNamespace,
            policyName: "Type naming policy",
            policyDescription: $"Enforces naming conventions for types in the {nameof(WebAuth)} component");

        policyDefinition
            .AddAttributeNamingRule(CurrentNamespace)
            .AddConstantNamingRule(CurrentNamespace)
            .AddExtensionNamingRule(CurrentNamespace)
            .AddFilterNamingRule(CurrentNamespace)
            .AddHandlerNamingRule(CurrentNamespace)
            .AddInterfaceNamingRule(CurrentNamespace)
            .AddOptionNamingRule(CurrentNamespace)
            .AddSettingNamingRule(CurrentNamespace)
            .AddValidatorNamingRule(CurrentNamespace);

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        foreach (var result in results)
        {
            Assert.IsTrue(result.IsSuccessful, PolicyHelper.BuildFailureMessage(result));
        }
    }
}
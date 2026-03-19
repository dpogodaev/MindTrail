using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindTrail.ArchTests.Constants;
using MindTrail.ArchTests.Extensions;
using MindTrail.ArchTests.Helpers;

namespace MindTrail.ArchTests.ArchTests;

/// <summary>
/// Architectural tests for <see cref="MindTrail.DomainShared"/> component.
/// </summary>
[TestClass]
[TestCategory("Architecture")]
public class DomainSharedArchTests
{
    private const string CurrentNamespace = ComponentNamespaces.DomainShared;

    private static readonly string[] UsingLibs =
    [
        "System"
    ];

    /// <summary>
    /// Verifies that the <see cref="MindTrail.DomainShared"/> component follows the dependency rules.
    /// </summary>
    [TestMethod]
    public void DomainEntities_ShouldFollowDependencyRules()
    {
        // Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(
            CurrentNamespace,
            "Component dependency policy",
            $"Enforces the dependencies of the {nameof(DomainShared)} component");

        policyDefinition.Add(
            types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependenciesOtherThan(
                    PolicyHelper.CreateAllowedDependenciesList(
                        CurrentNamespace,
                        UsingLibs)),
            "DomainShared_ShouldNotDependOn_OtherComponents",
            "Shared domain types should not have any dependencies on other components");

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        foreach (var result in results)
        {
            Assert.IsTrue(result.IsSuccessful, PolicyHelper.BuildFailureMessage(result));
        }
    }

    /// <summary>
    /// Verifies that the <see cref="MindTrail.DomainShared"/> component's types follow the naming conventions.
    /// </summary>
    [TestMethod]
    public void DomainEntities_ShouldFollowNamingConventions()
    {
        // Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(
            CurrentNamespace,
            "Type naming policy",
            $"Enforces naming conventions for types in the {nameof(DomainShared)} component");

        policyDefinition
            .AddEnumNamingRule(CurrentNamespace)
            .AddExceptionNamingRule(CurrentNamespace);

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        foreach (var result in results)
        {
            Assert.IsTrue(result.IsSuccessful, PolicyHelper.BuildFailureMessage(result));
        }
    }
}
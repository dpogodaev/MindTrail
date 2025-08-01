using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindTrail.ArchTests.Constants;
using MindTrail.ArchTests.Extensions;
using MindTrail.ArchTests.Helpers;

namespace MindTrail.ArchTests.ArchTests;

/// <summary>
/// Architectural tests for <see cref="MindTrail.DomainEntities"/> component.
/// </summary>
[TestClass]
[TestCategory("Architecture")]
public class DomainEntitiesArchTests
{
    private const string CurrentNamespace = ComponentNamespaces.DomainEntities;

    private static readonly string[] UsingLibs =
    [
        "System",
        "Microsoft"
    ];

    /// <summary>
    /// Verifies that the <see cref="MindTrail.DomainEntities"/> component follows the dependency rules.
    /// </summary>
    [TestMethod]
    public void DomainEntities_ShouldFollowDependencyRules()
    {
        // Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(CurrentNamespace,
            "Component dependency policy",
            $"Enforces the dependencies of the {nameof(DomainEntities)} component");

        policyDefinition.Add(types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependenciesOtherThan(CreateAllowedDependenciesList()),
            "DomainEntities_ShouldNotDependOn_OtherComponent",
            "Domain entities should not have any dependencies on other components");

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        foreach (var result in results)
        {
            Assert.IsTrue(result.IsSuccessful, PolicyHelper.BuildFailureMessage(result));
        }
    }

    /// <summary>
    /// Verifies that the <see cref="MindTrail.DomainEntities"/> component's types follow the naming conventions.
    /// </summary>
    [TestMethod]
    public void DomainEntities_ShouldFollowNamingConventions()
    {
        // Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(CurrentNamespace,
            "Type naming policy",
            $"Enforces naming conventions for types in the {nameof(DomainEntities)} component");

        policyDefinition
            .AddEnumNamingRule(CurrentNamespace);

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        foreach (var result in results)
        {
            Assert.IsTrue(result.IsSuccessful, PolicyHelper.BuildFailureMessage(result));
        }
    }

    #region Private methods

    private static string[] CreateAllowedDependenciesList()
    {
        var allowedDependenciesList = new List<string> { CurrentNamespace };
        allowedDependenciesList.AddRange(UsingLibs);

        return allowedDependenciesList.ToArray();
    }

    #endregion
}
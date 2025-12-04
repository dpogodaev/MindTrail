using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindTrail.ArchTests.Constants;
using MindTrail.ArchTests.Extensions;
using MindTrail.ArchTests.Helpers;

namespace MindTrail.ArchTests.ArchTests;

/// <summary>
/// Architectural tests for <see cref="MindTrail.CliHost"/> component.
/// </summary>
[TestClass]
[TestCategory("Architecture")]
public class CliHostArchTests
{
    private const string CurrentNamespace = ComponentNamespaces.CliHost;

    private static readonly string[] UsingLibs =
    [
        "System",
        "Microsoft"
    ];

    /// <summary>
    /// Verifies that the <see cref="MindTrail.CliHost"/> component follows the dependency rules.
    /// </summary>
    [TestMethod]
    public void CliHost_ShouldFollowDependencyRules()
    {
        // Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(
            CurrentNamespace,
            "Component dependency policy",
            $"Enforces the dependencies of the {nameof(CliHost)} component");

        policyDefinition.Add(
            types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.EfCore,
                    ComponentNamespaces.EfCoreMssql,
                    ComponentNamespaces.EfCorePostgreSql),
            "CliHost_ShouldNotDependOn_DomainLayer",
            "The CLI host should not have any dependencies on the application (domain) layer");

        policyDefinition.Add(
            types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.EfCore,
                    ComponentNamespaces.EfCoreMssql,
                    ComponentNamespaces.EfCorePostgreSql),
            "CliHost_ShouldNotDependOn_DataAccessLayer",
            "The CLI host should not have any dependencies on the data access layer");

        policyDefinition.Add(
            types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.WebApi,
                    ComponentNamespaces.WebAuth),
            "CliHost_ShouldNotDependOn_WebComponents",
            $"The CLI host should not depend on web-based presentation components such as {nameof(WebApi)}");

        policyDefinition.Add(
            types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependenciesOtherThan(
                    CreateAllowedDependenciesList([
                        ComponentNamespaces.Cli,
                        ComponentNamespaces.HostConfiguration
                    ])),
            "CliHost_ShouldOnlyDependOn_CliAndHostConfiguration",
            $"The CLI host can only depend on the components implementing its interface (e.g., {nameof(Cli)}) and the application configurator ({nameof(HostConfiguration)})");

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        foreach (var result in results)
        {
            Assert.IsTrue(result.IsSuccessful, PolicyHelper.BuildFailureMessage(result));
        }
    }

    /// <summary>
    /// Verifies that the <see cref="MindTrail.CliHost"/> component's types follow the naming conventions.
    /// </summary>
    [TestMethod]
    public void CliHost_ShouldFollowNamingConventions()
    {
        // Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(
            CurrentNamespace,
            "Type naming policy",
            $"Enforces naming conventions for types in the {nameof(CliHost)} component");

        policyDefinition.AddConfigNamingRule(CurrentNamespace);

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        foreach (var result in results)
        {
            Assert.IsTrue(result.IsSuccessful, PolicyHelper.BuildFailureMessage(result));
        }
    }

    #region Private methods

    private static string[] CreateAllowedDependenciesList(IEnumerable<string> allowedComponents)
    {
        var allowedDependenciesList = new List<string> { CurrentNamespace };
        allowedDependenciesList.AddRange(UsingLibs);
        allowedDependenciesList.AddRange(allowedComponents);

        return allowedDependenciesList.ToArray();
    }

    #endregion
}
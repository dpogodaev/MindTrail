using System.Collections.Generic;
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

    /// <summary>
    /// Verifies that the <see cref="MindTrail.WebAuth"/> component follows the dependency rules.
    /// </summary>
    [TestMethod]
    public void WebAuth_ShouldFollowDependencyRules()
    {
        // Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(
            CurrentNamespace,
            "Component dependency policy",
            $"Enforces the dependencies of the {nameof(WebAuth)} component");

        policyDefinition.Add(
            types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.EfCore,
                    ComponentNamespaces.EfCoreMssql,
                    ComponentNamespaces.EfCorePostgreSql),
            "WebAuth_ShouldNotDependOn_DomainLayer",
            "The Web API should not have any dependencies on the application (domain) layer");

        policyDefinition.Add(
            types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.EfCore,
                    ComponentNamespaces.EfCoreMssql,
                    ComponentNamespaces.EfCorePostgreSql),
            "WebAuth_ShouldNotDependOn_DataAccessLayer",
            "The Web API should not have any dependencies on the data access layer");

        policyDefinition.Add(
            types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.HostConfiguration,
                    ComponentNamespaces.CliHost,
                    ComponentNamespaces.WebHost),
            "WebAuth_ShouldNotDependOn_InfrastructureLayer",
            "The Web API should not have any dependencies on the infrastructure layer");

        policyDefinition.Add(
            types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependenciesOtherThan(
                    CreateAllowedDependenciesList([
                        ComponentNamespaces.Common
                    ])),
            "WebAuth_ShouldOnlyDependOn_CommonLogic",
            $"The Web authorization component can only depend on the shared logic ({nameof(Common)})");

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        foreach (var result in results)
        {
            Assert.IsTrue(result.IsSuccessful, PolicyHelper.BuildFailureMessage(result));
        }
    }

    /// <summary>
    /// Verifies that the <see cref="MindTrail.WebAuth"/> component's types follow the naming conventions.
    /// </summary>
    [TestMethod]
    public void WebAuth_ShouldFollowNamingConventions()
    {
        // Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(
            CurrentNamespace,
            "Type naming policy",
            $"Enforces naming conventions for types in the {nameof(WebAuth)} component");

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

    private static string[] CreateAllowedDependenciesList(IEnumerable<string> allowedComponents)
    {
        var allowedDependenciesList = new List<string> { CurrentNamespace };
        allowedDependenciesList.AddRange(GetUsingLibs());
        allowedDependenciesList.AddRange(allowedComponents);

        return allowedDependenciesList.ToArray();
    }

    private static IEnumerable<string> GetUsingLibs()
    {
        return
        [
            "System",
            "Microsoft"
        ];
    }
}
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindTrail.ArchTests.Constants;
using MindTrail.ArchTests.Extensions;
using MindTrail.ArchTests.Helpers;

namespace MindTrail.ArchTests.ArchTests;

/// <summary>
/// Architectural tests for <see cref="MindTrail.WebHost"/> component.
/// </summary>
[TestClass]
[TestCategory("Architecture")]
public class WebHostArchTests
{
    private const string CurrentNamespace = ComponentNamespaces.WebHost;

    private static readonly string[] UsingLibs =
    [
        "System",
        "Microsoft",
        "Swashbuckle"
    ];

    /// <summary>
    /// Verifies that the <see cref="MindTrail.WebHost"/> component follows the dependency rules.
    /// </summary>
    [TestMethod]
    public void WebHost_ShouldFollowDependencyRules()
    {
        // Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(
            CurrentNamespace,
            "Component dependency policy",
            $"Enforces the dependencies of the {nameof(WebHost)} component");

        policyDefinition.Add(
            types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.EfCore,
                    ComponentNamespaces.EfCoreMssql,
                    ComponentNamespaces.EfCorePostgreSql),
            "WebHost_ShouldNotDependOn_DomainLayer",
            "The Web host should not have any dependencies on the application (domain) layer");

        policyDefinition.Add(
            types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.EfCore,
                    ComponentNamespaces.EfCoreMssql,
                    ComponentNamespaces.EfCorePostgreSql),
            "WebHost_ShouldNotDependOn_DataAccessLayer",
            "The Web host should not have any dependencies on the data access layer");

        policyDefinition.Add(
            types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.Cli),
            "WebHost_ShouldNotDependOn_CliComponents",
            $"The Web host should not depend on command-line-based presentation components such as {nameof(Cli)}");

        policyDefinition.Add(
            types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependenciesOtherThan(
                    CreateAllowedDependenciesList([
                        ComponentNamespaces.Common,
                        ComponentNamespaces.DomainShared,
                        ComponentNamespaces.WebApi,
                        ComponentNamespaces.WebAuth,
                        ComponentNamespaces.HostConfiguration
                    ])),
            "WebHost_ShouldOnlyDependOn_WebAndHostConfiguration",
            $"The Web host can only depend on common utilities, shared domain types and the components implementing its interface, such as {nameof(WebApi)} and {nameof(WebAuth)}, and the application configurator ({nameof(HostConfiguration)})");

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        foreach (var result in results)
        {
            Assert.IsTrue(result.IsSuccessful, PolicyHelper.BuildFailureMessage(result));
        }
    }

    /// <summary>
    /// Verifies that the <see cref="MindTrail.WebHost"/> component's types follow the naming conventions.
    /// </summary>
    [TestMethod]
    public void WebHost_ShouldFollowNamingConventions()
    {
        // Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(
            CurrentNamespace,
            "Type naming policy",
            $"Enforces naming conventions for types in the {nameof(WebHost)} component");

        policyDefinition
            .AddConfigNamingRule(CurrentNamespace)
            .AddSettingNamingRule(CurrentNamespace);

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
        allowedDependenciesList.AddRange(UsingLibs);
        allowedDependenciesList.AddRange(allowedComponents);

        return allowedDependenciesList.ToArray();
    }
}
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindTrail.ArchTests.Constants;
using MindTrail.ArchTests.Extensions;
using MindTrail.ArchTests.Helpers;

namespace MindTrail.ArchTests.ArchTests;

/// <summary>
/// Architectural tests for <see cref="MindTrail.WebApi"/> component.
/// </summary>
[TestClass]
[TestCategory("Architecture")]
public class WebApiArchTests
{
    private const string CurrentNamespace = ComponentNamespaces.WebApi;

    private static readonly string[] UsingLibs =
    [
        "System",
        "Microsoft",
        "Swashbuckle"
    ];

    /// <summary>
    /// Verifies that the <see cref="MindTrail.WebApi"/> component follows the dependency rules.
    /// </summary>
    [TestMethod]
    public void WebApi_ShouldFollowDependencyRules()
    {
        // Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(
            CurrentNamespace,
            "Component dependency policy",
            $"Enforces the dependencies of the {nameof(WebApi)} component");

        policyDefinition.Add(
            types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.EfCore,
                    ComponentNamespaces.EfCoreMssql,
                    ComponentNamespaces.EfCorePostgreSql),
            "WebApi_ShouldNotDependOn_DomainLayer",
            "The Web API should not have any dependencies on the application (domain) layer");

        policyDefinition.Add(
            types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.EfCore,
                    ComponentNamespaces.EfCoreMssql,
                    ComponentNamespaces.EfCorePostgreSql),
            "WebApi_ShouldNotDependOn_DataAccessLayer",
            "The Web API should not have any dependencies on the data access layer");

        policyDefinition.Add(
            types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.HostConfiguration,
                    ComponentNamespaces.CliHost,
                    ComponentNamespaces.WebHost),
            "WebApi_ShouldNotDependOn_InfrastructureLayer",
            "The Web API should not have any dependencies on the infrastructure layer");

        policyDefinition.Add(
            types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.Cli),
            "WebApi_ShouldNotDependOn_CliComponents",
            $"The Web API should not depend on command-line-based presentation components such as {nameof(Cli)}");

        policyDefinition.Add(
            types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependenciesOtherThan(
                    CreateAllowedDependenciesList([
                        ComponentNamespaces.Common,
                        ComponentNamespaces.WebAuth
                    ])),
            "WebApi_ShouldOnlyDependOn_WebComponentsAndCommonLogic",
            $"The Web API can only depend on the shared logic ({nameof(Common)}) and on components that also implement the web interface, such as {nameof(WebAuth)}");

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        foreach (var result in results)
        {
            Assert.IsTrue(result.IsSuccessful, PolicyHelper.BuildFailureMessage(result));
        }
    }

    /// <summary>
    /// Verifies that the <see cref="MindTrail.WebApi"/> component's types follow the naming conventions.
    /// </summary>
    [TestMethod]
    public void WebApi_ShouldFollowNamingConventions()
    {
        // Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(
            CurrentNamespace,
            "Type naming policy",
            $"Enforces naming conventions for types in the {nameof(WebApi)} component");

        policyDefinition
            .AddAttributeNamingRule(CurrentNamespace)
            .AddControllerNamingRule(CurrentNamespace)
            .AddFilterNamingRule(CurrentNamespace)
            .AddModelNamingRule(CurrentNamespace);

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
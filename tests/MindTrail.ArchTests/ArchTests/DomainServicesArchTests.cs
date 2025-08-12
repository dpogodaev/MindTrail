using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindTrail.ArchTests.Constants;
using MindTrail.ArchTests.Extensions;
using MindTrail.ArchTests.Helpers;

namespace MindTrail.ArchTests.ArchTests;

/// <summary>
/// Architectural tests for <see cref="MindTrail.DomainServices"/> component.
/// </summary>
[TestClass]
[TestCategory("Architecture")]
public class DomainServicesArchTests
{
    private const string CurrentNamespace = ComponentNamespaces.DomainServices;

    private static readonly string[] UsingLibs =
    [
        "System",
        "Microsoft"
    ];

    /// <summary>
    /// Verifies that the <see cref="MindTrail.DomainServices"/> component follows the dependency rules.
    /// </summary>
    [TestMethod]
    public void DomainServices_ShouldFollowDependencyRules()
    {
        // Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(CurrentNamespace,
            "Component dependency policy",
            $"Enforces the dependencies of the {nameof(DomainServices)} component");

        policyDefinition.Add(types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.EfCore,
                    ComponentNamespaces.EfCoreMssql,
                    ComponentNamespaces.EfCorePostgreSql),
            "DomainServices_ShouldNotDependOn_DataAccessLayer",
            "Domain services should not have any dependencies on the data access layer");

        policyDefinition.Add(types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.Cli,
                    ComponentNamespaces.WebApi,
                    ComponentNamespaces.WebAuth),
            "DomainServices_ShouldNotDependOn_PresentationLayer",
            "Domain services should not have any dependencies on the presentation layer");

        policyDefinition.Add(types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.HostConfiguration,
                    ComponentNamespaces.CliHost,
                    ComponentNamespaces.WebHost),
            "DomainServices_ShouldNotDependOn_InfrastructureLayer",
            "Domain services should not have any dependencies on the infrastructure layer");

        policyDefinition.Add(types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.AppServices),
            "DomainServices_ShouldNotDependOn_AppServices",
            "Domain services should not have any dependencies on application services");

        policyDefinition.Add(types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependenciesOtherThan(
                    CreateAllowedDependenciesList([
                        ComponentNamespaces.Common,
                        ComponentNamespaces.DomainEntities
                    ])),
            "DomainServices_ShouldOnlyDependOn_DomainEntities",
            "Domain services can only depend on domain entities");

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        foreach (var result in results)
        {
            Assert.IsTrue(result.IsSuccessful, PolicyHelper.BuildFailureMessage(result));
        }
    }

    /// <summary>
    /// Verifies that the <see cref="MindTrail.DomainServices"/> component's types follow the naming conventions.
    /// </summary>
    [TestMethod]
    public void DomainServices_ShouldFollowNamingConventions()
    {
        //Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(CurrentNamespace,
            "Type naming policy",
            $"Enforces naming conventions for types in the {nameof(DomainServices)} component");

        policyDefinition
            .AddConstantNamingRule(CurrentNamespace)
            .AddExceptionNamingRule(CurrentNamespace)
            .AddInterfaceNamingRule(CurrentNamespace)
            .AddServiceNamingRule(CurrentNamespace);

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
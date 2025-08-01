using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindTrail.ArchTests.Constants;
using MindTrail.ArchTests.Extensions;
using MindTrail.ArchTests.Helpers;

namespace MindTrail.ArchTests.ArchTests;

/// <summary>
/// Architectural tests for <see cref="MindTrail.EfCore"/> component.
/// </summary>
[TestClass]
[TestCategory("Architecture")]
public class EfCoreArchTests
{
    private const string CurrentNamespace = ComponentNamespaces.EfCore;

    private static readonly string[] UsingLibs =
    [
        "System",
        "Microsoft"
    ];

    /// <summary>
    /// Verifies that the <see cref="MindTrail.EfCore"/> component follows the dependency rules.
    /// </summary>
    [TestMethod]
    public void EfCore_ShouldFollowDependencyRules()
    {
        // Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(CurrentNamespace,
            "Component dependency policy",
            $"Enforces the dependencies of the {nameof(AppServices)} component");

        policyDefinition.Add(types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.AppServices),
            "EfCore_ShouldNotDependOn_AppServices",
            "Abstraction above the data layer (EF) should not have any dependencies on the application services");

        policyDefinition.Add(types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.Cli,
                    ComponentNamespaces.WebApi,
                    ComponentNamespaces.WebAuth),
            "EfCore_ShouldNotDependOn_PresentationLayer",
            "Abstraction above the data layer (EF) should not have any dependencies on the presentation layer");

        policyDefinition.Add(types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.HostConfiguration,
                    ComponentNamespaces.CliHost,
                    ComponentNamespaces.WebHost),
            "EfCore_ShouldNotDependOn_InfrastructureLayer",
            "Abstraction above the data layer (EF) should not have any dependencies on the infrastructure layer");

        policyDefinition.Add(types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.EfCoreMssql,
                    ComponentNamespaces.EfCorePostgreSql),
            "EfCore_ShouldNotDependOn_ImplementationOfDataAccess",
            "Abstraction above the data layer (EF) should not have any dependencies on the implementation of data access");

        policyDefinition.Add(types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependenciesOtherThan(
                    CreateAllowedDependenciesList([
                        ComponentNamespaces.Common,
                        ComponentNamespaces.DomainEntities,
                        ComponentNamespaces.DomainServices
                    ])),
            "EfCore_ShouldOnlyDependOn_DomainEntitiesAndServices",
            "Abstraction above the data layer (EF) can only depend on domain entities and services");

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        foreach (var result in results)
        {
            Assert.IsTrue(result.IsSuccessful, PolicyHelper.BuildFailureMessage(result));
        }
    }

    /// <summary>
    /// Verifies that the <see cref="MindTrail.EfCore"/> component's types follow the naming conventions.
    /// </summary>
    [TestMethod]
    public void EfCore_ShouldFollowNamingConventions()
    {
        //Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(CurrentNamespace,
            "Type naming policy",
            $"Enforces naming conventions for types in the {nameof(EfCore)} component");

        policyDefinition
            .AddAdapterNamingRule(CurrentNamespace)
            .AddConfigNamingRule(CurrentNamespace)
            .AddInterfaceNamingRule(CurrentNamespace)
            .AddRepositoryNamingRule(CurrentNamespace);

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
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindTrail.ArchTests.Constants;
using MindTrail.ArchTests.Extensions;
using MindTrail.ArchTests.Helpers;

namespace MindTrail.ArchTests.ArchTests;

/// <summary>
/// Architectural tests for <see cref="MindTrail.Domain"/> component.
/// </summary>
[TestClass]
[TestCategory("Architecture")]
public class DomainArchTests
{
    private const string CurrentNamespace = ComponentNamespaces.Domain;

    private static readonly string[] UsingLibs =
    [
        "System",
        "Microsoft"
    ];

    /// <summary>
    /// Verifies that the <see cref="MindTrail.Domain"/> component follows the dependency rules.
    /// </summary>
    [TestMethod]
    public void Domain_ShouldFollowDependencyRules()
    {
        // Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(
            CurrentNamespace,
            "Component dependency policy",
            $"Enforces the dependencies of the {nameof(Domain)} component");

        policyDefinition.Add(
            types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.EfCore,
                    ComponentNamespaces.EfCoreMssql,
                    ComponentNamespaces.EfCorePostgreSql),
            "Domain_ShouldNotDependOn_DataAccessLayer",
            "The domain layer should not have any dependencies on the data access layer");

        policyDefinition.Add(
            types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.Cli,
                    ComponentNamespaces.WebApi,
                    ComponentNamespaces.WebAuth),
            "Domain_ShouldNotDependOn_PresentationLayer",
            "The domain layer should not have any dependencies on the presentation layer");

        policyDefinition.Add(
            types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.HostConfiguration,
                    ComponentNamespaces.CliHost,
                    ComponentNamespaces.WebHost),
            "Domain_ShouldNotDependOn_InfrastructureLayer",
            "The domain layer should not have any dependencies on the infrastructure layer");

        policyDefinition.Add(
            types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.Application,
                    ComponentNamespaces.ApplicationContracts),
            "Domain_ShouldNotDependOn_ApplicationLayer",
            "The domain layer should not have any dependencies on the application layer");

        policyDefinition.Add(
            types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependenciesOtherThan(
                    CreateAllowedDependenciesList(
                    [
                        ComponentNamespaces.DomainShared,
                        ComponentNamespaces.Common
                    ])),
            "Domain_ShouldOnlyDependOn_DomainSharedAndCommonUtilities",
            "The domain component can only depend on shared types and common utilities");

        policyDefinition.Add(
            types => types
                .That().ResideInNamespace($"{CurrentNamespace}.Entities")
                .ShouldNot().HaveDependenciesOtherThan(
                    CreateAllowedDependenciesList(
                        [
                            ComponentNamespaces.DomainShared,
                            $"{CurrentNamespace}.ValueObjects"
                        ],
                        ignoreCurrentNamespace: true)),
            "DomainEntities_ShouldOnlyDependOn_DomainSharedAndValueObjects",
            "Domain entities can only depend on shared types and value objects");

        policyDefinition.Add(
            types => types
                .That().ResideInNamespace($"{CurrentNamespace}.ValueObjects")
                .ShouldNot().HaveDependenciesOtherThan(
                    CreateAllowedDependenciesList(
                        [
                            $"{CurrentNamespace}.ValueObjects",
                            ComponentNamespaces.DomainShared
                        ],
                        ignoreCurrentNamespace: true)),
            "ValueObjects_ShouldOnlyDependOn_DomainShared",
            "Value objects can only depend on domain shared types");

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        foreach (var result in results)
        {
            Assert.IsTrue(result.IsSuccessful, PolicyHelper.BuildFailureMessage(result));
        }
    }

    /// <summary>
    /// Verifies that the <see cref="MindTrail.Domain"/> component's types follow the naming conventions.
    /// </summary>
    [TestMethod]
    public void Domain_ShouldFollowNamingConventions()
    {
        // Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(
            CurrentNamespace,
            "Type naming policy",
            $"Enforces naming conventions for types in the {nameof(Domain)} component");

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

    private static string[] CreateAllowedDependenciesList(
        IEnumerable<string> allowedComponents,
        bool ignoreCurrentNamespace = false)
    {
        var allowedDependenciesList =
            ignoreCurrentNamespace
                ? new List<string>()
                : new List<string> { CurrentNamespace };

        allowedDependenciesList.AddRange(UsingLibs);
        allowedDependenciesList.AddRange(allowedComponents);

        return allowedDependenciesList.ToArray();
    }
}
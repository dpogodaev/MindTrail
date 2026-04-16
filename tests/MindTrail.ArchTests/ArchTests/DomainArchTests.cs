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
        "System"
    ];

    /// <summary>
    /// Verifies that the <see cref="MindTrail.Domain"/> component follows the dependency rules.
    /// </summary>
    [TestMethod]
    public void Domain_ShouldFollowDependencyRules()
    {
        // Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(
            componentNamespace: CurrentNamespace,
            policyName: "Component dependency policy",
            policyDescription: $"Enforces the dependencies of the {nameof(Domain)} component");

        policyDefinition.Add(
            definition: types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.Application,
                    ComponentNamespaces.ApplicationContracts),
            name: "Restriction of dependency on Application layer",
            description: "The domain core should not have any dependencies on the application layer");

        policyDefinition.Add(
            definition: types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.Cli,
                    ComponentNamespaces.WebApi,
                    ComponentNamespaces.WebAuth),
            name: "Restriction of dependency on Presentation layer",
            description: "The domain core should not have any dependencies on the presentation layer");

        policyDefinition.Add(
            definition: types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.EfCore,
                    ComponentNamespaces.EfCoreMssql,
                    ComponentNamespaces.EfCorePostgreSql),
            name: "Restriction of dependency on Persistence layer",
            description: "The domain core should not have any dependencies on the persistence layer");

        policyDefinition.Add(
            definition: types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.ApplicationConfigurator,
                    ComponentNamespaces.CliHost,
                    ComponentNamespaces.WebHost),
            name: "Restriction of dependency on Infrastructure layer",
            description: "The domain core should not have any dependencies on the infrastructure layer");

        policyDefinition.Add(
            definition: types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependenciesOtherThan(
                    PolicyHelper.CreateAllowedDependenciesList(
                        CurrentNamespace,
                        UsingLibs,
                        [
                            ComponentNamespaces.DomainShared,
                            ComponentNamespaces.Common
                        ])),
            name: "Allowed dependencies for Domain Core",
            description: "The domain core can only depend on shared domain types and common utilities");

        policyDefinition.Add(
            definition: types => types
                .That().ResideInNamespace($"{CurrentNamespace}.Entities")
                .ShouldNot().HaveDependenciesOtherThan(
                    PolicyHelper.CreateAllowedDependenciesList(
                        $"{CurrentNamespace}.Entities",
                        UsingLibs,
                        [
                            $"{ComponentNamespaces.Domain}.ValueObjects",
                            ComponentNamespaces.DomainShared
                        ])),
            name: "Allowed dependencies for Domain Entities",
            description: "Domain entities can only depend on domain value objects and shared domain types");

        policyDefinition.Add(
            definition: types => types
                .That().ResideInNamespace($"{CurrentNamespace}.ValueObjects")
                .ShouldNot().HaveDependenciesOtherThan(
                    PolicyHelper.CreateAllowedDependenciesList(
                        $"{CurrentNamespace}.ValueObjects",
                        UsingLibs,
                        [
                            ComponentNamespaces.DomainShared
                        ])),
            name: "Allowed dependencies for Domain Value Objects",
            description: "Domain value objects can only depend on domain shared types");

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
            componentNamespace: CurrentNamespace,
            policyName: "Type naming policy",
            policyDescription: $"Enforces naming conventions for types in the {nameof(Domain)} component");

        policyDefinition
            .AddAbstractionNamingRule(CurrentNamespace)
            .AddRepositoryNamingRule($"{CurrentNamespace}.Abstractions")
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
}
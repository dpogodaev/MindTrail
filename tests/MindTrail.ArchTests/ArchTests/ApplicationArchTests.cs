using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindTrail.Application.Abstractions.Repositories;
using MindTrail.ArchTests.Constants;
using MindTrail.ArchTests.Extensions;
using MindTrail.ArchTests.Helpers;

namespace MindTrail.ArchTests.ArchTests;

/// <summary>
/// Architectural tests for <see cref="MindTrail.Application"/> component.
/// </summary>
[TestClass]
[TestCategory("Architecture")]
public class ApplicationArchTests
{
    private const string CurrentNamespace = ComponentNamespaces.Application;

    private static readonly string[] UsingLibs =
    [
        "System"
    ];

    /// <summary>
    /// Verifies that the <see cref="MindTrail.Application"/> component follows the dependency rules.
    /// </summary>
    [TestMethod]
    public void Application_ShouldFollowDependencyRules()
    {
        // Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(
            componentNamespace: CurrentNamespace,
            policyName: "Component dependency policy",
            policyDescription: $"Enforces the dependencies of the {nameof(Application)} component");

        policyDefinition.Add(
            definition: types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.Cli,
                    ComponentNamespaces.WebApi,
                    ComponentNamespaces.WebAuth),
            name: "Restriction of dependency on Presentation layer",
            description: "The application implementation should not have any dependencies on the presentation layer");

        policyDefinition.Add(
            definition: types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.EfCore,
                    ComponentNamespaces.EfCoreMssql,
                    ComponentNamespaces.EfCorePostgreSql),
            name: "Restriction of dependency on Persistence layer",
            description: "The application implementation should not have any dependencies on the persistence layer");

        policyDefinition.Add(
            definition: types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.HostConfiguration,
                    ComponentNamespaces.CliHost,
                    ComponentNamespaces.WebHost),
            name: "Restriction of dependency on Infrastructure layer",
            description: "The application implementation should not have any dependencies on the infrastructure layer");

        policyDefinition.Add(
            definition: types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependenciesOtherThan(
                    PolicyHelper.CreateAllowedDependenciesList(
                        CurrentNamespace,
                        UsingLibs,
                        [
                            ComponentNamespaces.Domain,
                            ComponentNamespaces.DomainShared,
                            ComponentNamespaces.ApplicationContracts,
                            ComponentNamespaces.Common
                        ])),
            name: "Allowed dependencies",
            description: "The application implementation can only depend on the domain layer, application contracts, and common utilities");

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        foreach (var result in results)
        {
            Assert.IsTrue(result.IsSuccessful, PolicyHelper.BuildFailureMessage(result));
        }
    }

    /// <summary>
    /// Verifies that the <see cref="MindTrail.Application"/> component's types follow the naming conventions.
    /// </summary>
    [TestMethod]
    public void Application_ShouldFollowNamingConventions()
    {
        // Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(
            componentNamespace: CurrentNamespace,
            policyName: "Type naming policy",
            policyDescription: $"Enforces naming conventions for types in the {nameof(Application)} component");

        policyDefinition
            .AddAbstractionNamingRule($"{CurrentNamespace}")
            .AddRepositoryNamingRule($"{CurrentNamespace}.Abstractions", [nameof(IUnitOfWork)])
            .AddMappingNamingRule(CurrentNamespace)
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
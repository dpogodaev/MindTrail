using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindTrail.ArchTests.Constants;
using MindTrail.ArchTests.Helpers;

namespace MindTrail.ArchTests.ArchTests;

/// <summary>
/// Architectural tests for <see cref="MindTrail.EfCorePostgreSql"/> component.
/// </summary>
[TestClass]
[TestCategory("Architecture")]
public class EfCorePostgreSqlArchTests
{
    private const string CurrentNamespace = ComponentNamespaces.EfCorePostgreSql;

    private static readonly string[] UsingLibs =
    [
        "System",
        "Microsoft",
        "Npgsql"
    ];

    /// <summary>
    /// Verifies that the <see cref="MindTrail.EfCorePostgreSql"/> component follows the dependency rules.
    /// </summary>
    [TestMethod]
    public void EfCorePostgreSql_ShouldFollowDependencyRules()
    {
        // Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(
            componentNamespace: CurrentNamespace,
            policyName: "Component dependency policy",
            policyDescription: $"Enforces the dependencies of the {nameof(EfCorePostgreSql)} component");

        policyDefinition.Add(
            definition: types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.Domain),
            name: "Restriction of dependency on Domain layer",
            description: "The PostgreSQL persistence implementation should not have any dependencies on the domain core");

        policyDefinition.Add(
            definition: types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.Application,
                    ComponentNamespaces.ApplicationContracts),
            name: "Restriction of dependency on Application layer",
            description: "The PostgreSQL persistence implementation should not have any dependencies on the application layer");

        policyDefinition.Add(
            definition: types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.Cli,
                    ComponentNamespaces.WebApi,
                    ComponentNamespaces.WebAuth),
            name: "Restriction of dependency on Presentation layer",
            description: "The PostgreSQL persistence implementation should not have any dependencies on the presentation layer");

        policyDefinition.Add(
            definition: types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.EfCoreMssql),
            name: "Restriction of dependency on Persistence layer",
            description: "The PostgreSQL persistence implementation should not have any dependencies on other persistence implementations, such as SQL Server");

        policyDefinition.Add(
            definition: types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.ApplicationConfigurator,
                    ComponentNamespaces.CliHost,
                    ComponentNamespaces.WebHost),
            name: "Restriction of dependency on Infrastructure layer",
            description: "The PostgreSQL persistence implementation should not have any dependencies on the infrastructure layer");

        policyDefinition.Add(
            definition: types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependenciesOtherThan(
                    PolicyHelper.CreateAllowedDependenciesList(
                        CurrentNamespace,
                        UsingLibs,
                        [
                            ComponentNamespaces.EfCore
                        ])),
            name: "Allowed dependencies",
            description: "The PostgreSQL persistence implementation can only depend on abstraction above the data layer (EF)");

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        foreach (var result in results)
        {
            Assert.IsTrue(result.IsSuccessful, PolicyHelper.BuildFailureMessage(result));
        }
    }
}
using System.Collections.Generic;
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
        "Microsoft"
    ];

    /// <summary>
    /// Verifies that the <see cref="MindTrail.EfCorePostgreSql"/> component follows the dependency rules.
    /// </summary>
    [TestMethod]
    public void EfCorePostgreSql_ShouldFollowDependencyRules()
    {
        // Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(CurrentNamespace,
            "Component dependency policy",
            $"Enforces the dependencies of the {nameof(EfCorePostgreSql)} component");

        policyDefinition.Add(types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.EfCore,
                    ComponentNamespaces.EfCoreMssql,
                    ComponentNamespaces.EfCorePostgreSql),
            "Cli_ShouldNotDependOn_DomainLayer",
            "The CLI should not have any dependencies on the application (domain) layer");

        policyDefinition.Add(types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.Cli,
                    ComponentNamespaces.WebApi,
                    ComponentNamespaces.WebAuth),
            "EfCorePostgreSql_ShouldNotDependOn_PresentationLayer",
            "The PostgreSQL data access implementation should not have any dependencies on the presentation layer");

        policyDefinition.Add(types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.HostConfiguration,
                    ComponentNamespaces.CliHost,
                    ComponentNamespaces.WebHost),
            "EfCorePostgreSql_ShouldNotDependOn_InfrastructureLayer",
            "The PostgreSQL data access implementation should not have any dependencies on the infrastructure layer");

        policyDefinition.Add(types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.EfCoreMssql),
            "EfCorePostgreSql_ShouldNotDependOn_OtherImplementationOfDataAccess",
            "The PostgreSQL data access implementation should not have any dependencies on other data access implementations such as SQL Server");

        policyDefinition.Add(types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependenciesOtherThan(
                    CreateAllowedDependenciesList([
                        ComponentNamespaces.EfCore
                    ])),
            "EfCorePostgreSql_ShouldOnlyDependOn_AbstractionAboveTheDataLayer",
            "The PostgreSQL data access implementation can only depend on abstraction above the data layer (EF)");

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
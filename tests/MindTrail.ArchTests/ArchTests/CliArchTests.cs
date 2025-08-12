using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MindTrail.ArchTests.Constants;
using MindTrail.ArchTests.Extensions;
using MindTrail.ArchTests.Helpers;
using MindTrail.Cli.Commands;

namespace MindTrail.ArchTests.ArchTests;

/// <summary>
/// Architectural tests for <see cref="MindTrail.Cli"/> component.
/// </summary>
[TestClass]
[TestCategory("Architecture")]
public class CliArchTests
{
    private const string CurrentNamespace = ComponentNamespaces.Cli;

    private static readonly string[] UsingLibs =
    [
        "System",
        "Microsoft"
    ];

    /// <summary>
    /// Verifies that the <see cref="MindTrail.Cli"/> component follows the dependency rules.
    /// </summary>
    [TestMethod]
    public void Cli_ShouldFollowDependencyRules()
    {
        //Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(CurrentNamespace,
            "Component dependency policy",
            $"Enforces the dependencies of the {nameof(Cli)} component");

        policyDefinition.Add(types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.DomainEntities,
                    ComponentNamespaces.DomainServices,
                    ComponentNamespaces.AppServices),
            "Cli_ShouldNotDependOn_DomainLayer",
            "The CLI should not have any dependencies on the application (domain) layer");

        policyDefinition.Add(types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.EfCore,
                    ComponentNamespaces.EfCoreMssql,
                    ComponentNamespaces.EfCorePostgreSql),
            "Cli_ShouldNotDependOn_DataAccessLayer",
            "The CLI should not have any dependencies on the data access layer");

        policyDefinition.Add(types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.HostConfiguration,
                    ComponentNamespaces.CliHost,
                    ComponentNamespaces.WebHost),
            "Cli_ShouldNotDependOn_InfrastructureLayer",
            "The CLI should not have any dependencies on the infrastructure layer");

        policyDefinition.Add(types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependencyOnAny(
                    ComponentNamespaces.WebApi,
                    ComponentNamespaces.WebAuth),
            "Cli_ShouldNotDependOn_WebComponents",
            $"The CLI should not depend on web-based presentation components such as {nameof(WebApi)}");

        policyDefinition.Add(types => types
                .That().ResideInNamespace(CurrentNamespace)
                .ShouldNot().HaveDependenciesOtherThan(
                    CreateAllowedDependenciesList([
                        ComponentNamespaces.Common
                    ])),
            "Cli_ShouldOnlyDependOn_CommonLogic",
            $"The CLI can only depend on the shared logic ({nameof(Common)})");

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        foreach (var result in results)
        {
            Assert.IsTrue(result.IsSuccessful, PolicyHelper.BuildFailureMessage(result));
        }
    }

    /// <summary>
    /// Verifies that the <see cref="MindTrail.Cli"/> component's types follow the naming conventions.
    /// </summary>
    [TestMethod]
    public void Cli_ShouldFollowNamingConventions()
    {
        // Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(CurrentNamespace,
            "Type naming policy",
            $"Enforces naming conventions for types in the {nameof(Cli)} component");

        policyDefinition
            .AddCommandNamingRule(CurrentNamespace, exceptionsToRule: [nameof(CommandControl)])
            .AddConstantNamingRule(CurrentNamespace)
            .AddFactoryNamingRule(CurrentNamespace)
            .AddHelperNamingRule(CurrentNamespace)
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
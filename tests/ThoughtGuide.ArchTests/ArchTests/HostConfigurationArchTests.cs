using ThoughtGuide.ArchTests.Constants;
using ThoughtGuide.ArchTests.Extensions;
using ThoughtGuide.ArchTests.Helpers;
using Xunit;

namespace ThoughtGuide.ArchTests.ArchTests;

/// <summary>
/// Architectural tests for <see cref="ThoughtGuide.HostConfiguration"/> component.
/// </summary>
public class HostConfigurationArchTests
{
    private const string WorkingNamespace = ComponentNamespaces.HostConfiguration;

    /// <summary>
    /// Tests to check the dependency policy for <see cref="ThoughtGuide.HostConfiguration"/> component.
    /// </summary>
    [Fact]
    public void DependencyOfComponentsShouldFollowCleanArchitecture()
    {
        //Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(WorkingNamespace,
            "Components dependency policy",
            "Describes the dependencies of the 'HostConfiguration' component");

        policyDefinition.Add(types => types
                .That().ResideInNamespace(WorkingNamespace)
                .ShouldNot().HaveDependencyOnAny([
                    ComponentNamespaces.WebHost,
                    ComponentNamespaces.WebApi,
                    ComponentNamespaces.WebAuth,
                    ComponentNamespaces.CliHost,
                    ComponentNamespaces.Cli
                ]),
            "The rule of dependence of the 'HostConfiguration' on other components",
            "The 'HostConfiguration' component should not have any dependencies " +
            "on other host components (e.g., 'WebHost' and 'CliHost') and their interfaces (e.g., 'WebApi' and 'Cli')");

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        Assert.All(results, x => Assert.True(x.IsSuccessful));
    }

    /// <summary>
    /// Tests to check class naming of <see cref="ThoughtGuide.HostConfiguration"/> component.
    /// </summary>
    [Fact]
    public void ClassNamesMustFollowTheNamingRules()
    {
        //Arrange
        var policyDefinition = PolicyHelper.BuildPolicyDefinition(WorkingNamespace,
            "File naming policy",
            "Describes the naming policy for files with the '.cs' extension");

        policyDefinition
            .AddConfigNamingRule(WorkingNamespace)
            .AddExtensionNamingRule(WorkingNamespace)
            .AddHelperNamingRule(WorkingNamespace)
            .AddInterfaceNamingRule(WorkingNamespace)
            .AddProviderNamingRule(WorkingNamespace);

        // Act
        var results = policyDefinition.Evaluate().Results;

        // Assert
        Assert.All(results, x => Assert.True(x.IsSuccessful));
    }
}
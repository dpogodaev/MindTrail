using NetArchTest.Rules;
using NetArchTest.Rules.Policies;

namespace MindTrail.ArchTests.Extensions;

/// <summary>
/// Policy rules extensions.
/// </summary>
public static class PolicyExtensions
{
    /// <summary>
    /// Adds a naming rule for classes that implement an adapter.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddAdapterNamingRule(
        this PolicyDefinition policyDefinition, string workingNamespace, string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for a class that implements an adapter";
        const string description = "The name of a class that implements an adapter must end with the word 'Adapter'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Adapters")
                .Should().HaveNameEndingWith("Adapter");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for classes inherited from the 'Attribute' class.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddAttributeNamingRule(
        this PolicyDefinition policyDefinition, string workingNamespace, string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for a class inherited from the 'Attribute' class";
        const string description =
            "The name of a class inherited from the 'Attribute' class must end with the word 'Attribute'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Attributes")
                .Should().HaveNameEndingWith("Attribute");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for classes that implement a command.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddCommandNamingRule(
        this PolicyDefinition policyDefinition, string workingNamespace, string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for a class that implements a command";
        const string description = "The name of a class that implements a command must end with the word 'Command'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Commands")
                .Should().HaveNameEndingWith("Command");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for classes that manage the configuration.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddConfigNamingRule(
        this PolicyDefinition policyDefinition, string workingNamespace, string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for a class that manages the configuration";
        const string description = "The name of a class that manages the configuration must end with the word 'Config'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Configs")
                .Should().HaveNameEndingWith("Config");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for classes containing constants.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddConstantNamingRule(
        this PolicyDefinition policyDefinition, string workingNamespace, string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for a class containing constants";
        const string description = "The name of a class containing constants must end with the word 'Constants'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Constants")
                .Should().HaveNameEndingWith("Constants");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for classes that implement a controller.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddControllerNamingRule(
        this PolicyDefinition policyDefinition, string workingNamespace, string[] exceptionsToRule = null)
    {
        const string name = "Naming rule a class that implements a controller";
        const string description =
            "The name of a class that implements a controller must end with the word 'Controller'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Controllers")
                .Should().HaveNameEndingWith("Controller");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for classes that implement an enum.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddEnumNamingRule(
        this PolicyDefinition policyDefinition, string workingNamespace, string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for a class that implements an enum";
        const string description = "The name of a class that implements an enum must end with the word 'Type'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Enums")
                .Should().HaveNameEndingWith("Type");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for classes inherited from the 'Exception' class.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddExceptionNamingRule(
        this PolicyDefinition policyDefinition, string workingNamespace, string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for a class inherited from the 'Exception' class";
        const string description =
            "The name of a class inherited from the 'Exception' class must end with the word 'Exception'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Exceptions")
                .Should().HaveNameEndingWith("Exception");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for classes containing extensions.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddExtensionNamingRule(
        this PolicyDefinition policyDefinition, string workingNamespace, string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for a class containing extensions";
        const string description = "The name of a class containing extensions must end with the word 'Extensions'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Extensions")
                .Should().HaveNameEndingWith("Extensions");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for classes that implement a factory.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddFactoryNamingRule(
        this PolicyDefinition policyDefinition, string workingNamespace, string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for a class that implements a factory";
        const string description = "The name of a class that implements a factory must end with the word 'Factory'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Factories")
                .Should().HaveNameEndingWith("Factory");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for classes that implement a filter.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddFilterNamingRule(
        this PolicyDefinition policyDefinition, string workingNamespace, string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for a class that implements a filter";
        const string description = "The name of a class that implements a filter must end with the word 'Filter'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Filters")
                .Should().HaveNameEndingWith("Filter");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for classes that implement a handler.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddHandlerNamingRule(
        this PolicyDefinition policyDefinition, string workingNamespace, string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for a class that implements a handler";
        const string description = "The name of a class that implements a handler must end with the word 'Handler'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Handlers")
                .Should().HaveNameEndingWith("Handler");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for classes that implement a helper.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddHelperNamingRule(
        this PolicyDefinition policyDefinition, string workingNamespace, string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for a class that implements a helper";
        const string description = "The name of a class that implements a helper must end with the word 'Helper'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Helpers")
                .Should().HaveNameEndingWith("Helper");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for interfaces.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddInterfaceNamingRule(
        this PolicyDefinition policyDefinition, string workingNamespace, string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for interfaces";
        const string description = "The name of an interface must begin with the letter 'I'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Interfaces")
                .Should().HaveNameStartingWith("I");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for classes that implement a model.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddModelNamingRule(
        this PolicyDefinition policyDefinition, string workingNamespace, string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for a class that implements a model";
        const string description = "The name of a class that implements a model must end with the word 'Model'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Models")
                .Should().HaveNameEndingWith("Model");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for classes containing options.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddOptionNamingRule(
        this PolicyDefinition policyDefinition, string workingNamespace, string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for a class containing options";
        const string description = "The name of a class containing options must end with the word 'Options'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Options")
                .Should().HaveNameEndingWith("Options");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for classes that implement a provider.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddProviderNamingRule(
        this PolicyDefinition policyDefinition, string workingNamespace, string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for a class that implements a provider";
        const string description = "The name of a class that implements a provider must end with the word 'Provider'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Providers")
                .Should().HaveNameEndingWith("Provider");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for classes that implement a repository.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddRepositoryNamingRule(
        this PolicyDefinition policyDefinition, string workingNamespace, string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for a class that implements a repository";
        const string description = "The name of a class that implements a provider must end with the word 'Repository'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Repositories")
                .Should().HaveNameEndingWith("Repository");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for classes that implement a service.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddServiceNamingRule(
        this PolicyDefinition policyDefinition, string workingNamespace, string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for a class that implements a service";
        const string description = "The name of a class that implements a service must end with the word 'Service'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Services")
                .Should().HaveNameEndingWith("Service");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for classes containing settings.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddSettingNamingRule(
        this PolicyDefinition policyDefinition, string workingNamespace, string[] exceptionsToRule = null)
    {
        const string name = "\"Naming rule for a class containing settings";
        const string description = "The name of a class containing settings must end with the word 'Settings'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Settings")
                .Should().HaveNameEndingWith("Settings");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    /// <summary>
    /// Adds a naming rule for classes that implement a validator.
    /// </summary>
    /// <param name="policyDefinition">Source policy definition.</param>
    /// <param name="workingNamespace">Working namespace.</param>
    /// <param name="exceptionsToRule">Names of classes that are exceptions to the rule.</param>
    /// <returns>Policy definition with an added rule.</returns>
    public static PolicyDefinition AddValidatorNamingRule(
        this PolicyDefinition policyDefinition, string workingNamespace, string[] exceptionsToRule = null)
    {
        const string name = "Naming rule for a class that implements a validator";
        const string description = "The name of a class that implements a validator must end with the word 'Validator'";

        ConditionList Definition(Types types)
        {
            var conditions = types
                .That().ResideInNamespace($"{workingNamespace}.Validators")
                .Should().HaveNameEndingWith("Validator");

            conditions.AddExceptionsToRule(exceptionsToRule);
            return conditions;
        }

        return policyDefinition.Add(Definition, name, description);
    }

    private static void AddExceptionsToRule(this ConditionList conditions, string[] exceptionsToRule)
    {
        if (exceptionsToRule == null)
        {
            return;
        }

        foreach (var exceptionToRule in exceptionsToRule)
        {
            conditions.Or().HaveName(exceptionToRule);
        }
    }
}
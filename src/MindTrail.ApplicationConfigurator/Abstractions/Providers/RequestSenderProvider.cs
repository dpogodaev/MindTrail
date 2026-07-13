using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MindTrail.ApplicationContracts.Interfaces;
using MindTrail.ApplicationContracts.Interfaces.Commands;
using MindTrail.ApplicationContracts.Interfaces.Queries;

namespace MindTrail.ApplicationConfigurator.Abstractions.Providers;

/// <inheritdoc cref="IRequestSender"/>
public class RequestSenderProvider(IServiceProvider serviceProvider) : IRequestSender
{
    private static readonly ConcurrentDictionary<Type, MethodInfo> HandleMethodCache = new();

    /// <inheritdoc/>
    public Task<TResult> Send<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(ICommandHandler<,>)
            .MakeGenericType(command.GetType(), typeof(TResult));

        return InvokeHandleAsync<TResult>(handlerType, command, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<TResult> Send<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IQueryHandler<,>)
            .MakeGenericType(query.GetType(), typeof(TResult));

        return InvokeHandleAsync<TResult>(handlerType, query, cancellationToken);
    }

    private Task<TResult> InvokeHandleAsync<TResult>(Type handlerType, object request,
        CancellationToken cancellationToken)
    {
        var handler = serviceProvider.GetRequiredService(handlerType);

        var handleMethod = HandleMethodCache.GetOrAdd(
            handlerType,
            static type => type.GetMethod("HandleAsync")!);

        return (Task<TResult>)handleMethod.Invoke(handler, [request, cancellationToken])!;
    }
}
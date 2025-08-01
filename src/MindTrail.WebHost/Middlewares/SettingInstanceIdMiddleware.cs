using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MindTrail.HostConfiguration.Providers;
using MindTrail.WebHost.Services.Hosted;

namespace MindTrail.WebHost.Middlewares;

/// <summary>
/// Middleware for setting the ID of the application instance used for logging.
/// </summary>
/// <param name="next">The next <see cref="RequestDelegate"/> in the middleware pipeline.</param>
public class SettingInstanceIdMiddleware(RequestDelegate next)
{
    /// <summary>
    /// Executes the middleware logic.
    /// This method is called by the ASP.NET Core pipeline.
    /// </summary>
    /// <param name="context">The HTTP context for the current request.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// The <c>Invoke</c> method is part of the middleware invocation convention in ASP.NET Core.
    /// For more information, see <see href="https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/write">Writing custom middleware</see>.
    /// </remarks>
    public async Task InvokeAsync(HttpContext context)
    {
        LoggerProvider.SetInstanceId(AppLifetimeHostedService.InstanceId);

        await next(context);
    }
}
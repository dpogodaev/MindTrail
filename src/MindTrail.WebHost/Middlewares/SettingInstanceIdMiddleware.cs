using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MindTrail.HostConfiguration.Providers;
using MindTrail.WebHost.Services.Hosted;

namespace MindTrail.WebHost.Middlewares;

public class SettingInstanceIdMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        LoggerProvider.SetInstanceId(LifetimeEventsHostedService.InstanceId);

        await next(context);
    }
}
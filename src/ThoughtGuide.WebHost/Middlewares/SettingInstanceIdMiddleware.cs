using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ThoughtGuide.HostConfiguration.Providers;
using ThoughtGuide.WebHost.Services.Hosted;

namespace ThoughtGuide.WebHost.Middlewares;

public class SettingInstanceIdMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        LoggerProvider.SetInstanceId(LifetimeEventsHostedService.InstanceId);

        await next(context);
    }
}
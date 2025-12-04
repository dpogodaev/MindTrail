using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindTrail.Common.Extensions;
using MindTrail.WebApi.Attributes;
using MindTrail.WebApi.Dtos;

namespace MindTrail.WebApi.Controllers;

/// <summary>
/// Used to get information about the application, such as version, build date, etc.
/// </summary>
[AllowAnonymous]
[ApiController]
[Route("api/mind-trail/v1")]
public class BuildInfoController : ControllerBase
{
    /// <summary>
    /// Returns the build information.
    /// </summary>
    /// <returns>The build information.</returns>
    [HttpHead("info")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ResponseHeader("X-Version", "Version number")]
    [ResponseHeader("X-Build-Date", "Build date (UTC)")]
    [ResponseHeader("X-Configuration", "Build configuration (e.g. 'Debug' or 'Release')")]
    [ResponseHeader("X-App-Name", "Application name")]
    public IActionResult HeadInfo()
    {
        var buildInfo = GetBuildInfo();

        Response.Headers.Append("X-Version", buildInfo.Version);
        Response.Headers.Append("X-Build-Date", buildInfo.BuildDate);
        Response.Headers.Append("X-Configuration", buildInfo.Configuration);
        Response.Headers.Append("X-App-Name", buildInfo.AppName);

        return Ok();
    }

    /// <summary>
    /// Returns the build information.
    /// </summary>
    /// <returns>The build information.</returns>
    [HttpGet("info")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BuildInfoDto))]
    public IActionResult GetInfo()
    {
        var buildInfo = GetBuildInfo();

        return Ok(buildInfo);
    }

    /// <summary>
    /// Default action.
    /// </summary>
    /// <returns>The build information.</returns>
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BuildInfoDto))]
    [Produces("application/json")]
    public IActionResult Default()
    {
        return GetInfo();
    }

    private static BuildInfoDto GetBuildInfo()
    {
        var assembly = Assembly.GetExecutingAssembly();

        return new BuildInfoDto
        {
            Version = assembly.GetVersion(),
            BuildDate = assembly.GetAssemblyDate(),
            Configuration = assembly.GetAssemblyConfiguration(),
            AppName = assembly.GetAssemblyProductName(),
        };
    }
}
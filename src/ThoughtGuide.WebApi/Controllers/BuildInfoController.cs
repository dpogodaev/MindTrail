using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThoughtGuide.Common.Extensions;
using ThoughtGuide.WebApi.Attributes;
using ThoughtGuide.WebApi.Dtos;

namespace ThoughtGuide.WebApi.Controllers;

/// <summary>
/// Provides information about the application, such as version, build date, etc.
/// </summary>
[AllowAnonymous]
[ApiController]
[Route("api/thought-guide/v1")]
public class BuildInfoController : ControllerBase
{
    /// <summary>
    /// Returns build information.
    /// </summary>
    [HttpHead("info")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ResponseHeader("X-Version", "Version number")]
    [ResponseHeader("X-Build-Date", "Build date (UTC)")]
    [ResponseHeader("X-Configuration", "Build configuration (e.g. 'Debug' or 'Release')")]
    [ResponseHeader("X-App-Name", "Application name")]
    public ActionResult HeadInfo()
    {
        var buildInfo = GetBuildInfo();

        Response.Headers.Append("X-Version", buildInfo.Version);
        Response.Headers.Append("X-Build-Date", buildInfo.BuildDate);
        Response.Headers.Append("X-Configuration", buildInfo.Configuration);
        Response.Headers.Append("X-App-Name", buildInfo.AppName);

        return Ok();
    }

    /// <summary>
    /// Returns build information.
    /// </summary>
    [HttpGet("info")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BuildInfoDto))]
    public ActionResult<BuildInfoDto> GetInfo()
    {
        var buildInfo = GetBuildInfo();

        return Ok(buildInfo);
    }

    /// <summary>
    /// Default action.
    /// </summary>
    /// <returns>Build information.</returns>
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BuildInfoDto))]
    public ActionResult<BuildInfoDto> Default()
    {
        return GetInfo();
    }

    #region Private methods

    private static BuildInfoDto GetBuildInfo()
    {
        var assembly = Assembly.GetExecutingAssembly();

        return new BuildInfoDto
        {
            Version = assembly.GetVersion(),
            BuildDate = assembly.GetAssemblyDate(),
            Configuration = assembly.GetAssemblyConfiguration(),
            AppName = assembly.GetAssemblyProductName()
        };
    }

    #endregion
}
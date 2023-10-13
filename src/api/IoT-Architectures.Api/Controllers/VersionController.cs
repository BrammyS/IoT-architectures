using System.Reflection;
using IoT_Architectures.Api.Core.Endpoints.Version;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IoT_Architectures.Api.Controllers;

[Route("api/version")]
[Route("api/v{apiVersion}/version")]
public class VersionController : ApiController
{
    public VersionController(IMediator mediator) : base(mediator)
    {
    }

    /// <summary>
    ///     Shows the current version and the build date of the API.
    /// </summary>
    /// <remarks>
    ///     GET: /api/version
    /// </remarks>
    /// <returns>
    ///     The requested <see cref="VersionResponse" />.
    /// </returns>
    /// <response code="200">Returned when the request was successful.</response>
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<VersionResponse>> GetVersion()
    {
        var query = new GetVersionQuery(Assembly.GetExecutingAssembly());
        var result = await Mediator.Send(query).ConfigureAwait(false);
        return Ok(result);
    }
}
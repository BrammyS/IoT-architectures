using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IoT_Architectures.Api.Controllers;

/// <summary>
///     The Base for all API controllers.
/// </summary>
[Authorize]
[ApiController]
[ApiVersion("1")]
[Route("api/v{apiVersion}/[controller]")]
public abstract class ApiController : ControllerBase
{
    /// <summary>
    ///     The <see cref="IMediator" /> used to call all handlers.
    /// </summary>
    protected readonly IMediator Mediator;

    /// <summary>
    ///     Initializes a new <see cref="ApiController" />.
    /// </summary>
    /// <param name="mediator">The <see cref="IMediator" /> used to call all handlers.</param>
    protected ApiController(IMediator mediator)
    {
        Mediator = mediator;
    }
}
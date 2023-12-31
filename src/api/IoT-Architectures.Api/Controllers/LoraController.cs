﻿using IoT_Architectures.Api.Core.Endpoints.Lora;
using IoT_Architectures.Api.Core.Endpoints.Lora.Webhook;
using IoT_Architectures.Api.Domain.Models;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IoT_Architectures.Api.Controllers;

public class LoraController : ApiController
{
    private readonly ILogger<LoraController> _logger;

    public LoraController(IMediator mediator, ILogger<LoraController> logger) : base(mediator)
    {
        _logger = logger;
    }

    /// <summary>
    ///     Handles a webhook request from the lora network.
    /// </summary>
    /// <remarks>
    ///     POST: /api/v*/lora/webhook
    /// </remarks>
    /// <returns>
    ///     A <see cref="IActionResult" />.
    /// </returns>
    /// <response code="200">Returned when the request was successful.</response>
    [HttpPost("webhook")]
    [AllowAnonymous]
    public async Task<ActionResult> HandleWebhookRequest([FromBody] List<SenML> data, [FromHeader(Name = "things-message-token")] string token)
    {
        if (Request.Body.CanSeek)
        {
            Request.Body.Seek(0, SeekOrigin.Begin);
        }

        if (!await Mediator.Send(new VerifyLoraRequestCommand(Request.Body, token)).ConfigureAwait(false))
        {
            return Unauthorized("Failed to verify token");
        }

        var command = new LoraWebhookCommand(data);
        await Mediator.Send(command).ConfigureAwait(false);
        return Ok();
    }
}
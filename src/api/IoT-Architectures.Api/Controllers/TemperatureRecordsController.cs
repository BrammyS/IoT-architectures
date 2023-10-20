using IoT_Architectures.Api.Core.Endpoints.TemperatureRecords;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace IoT_Architectures.Api.Controllers;

public class TemperatureRecordsController : ApiController
{
    public TemperatureRecordsController(IMediator mediator) : base(mediator)
    {
    }
    
    [HttpGet]
    public async Task<IActionResult> GetTemperatureRecords()
    {
        var result = await Mediator.Send(new GetTemperatureRecordsQuery()).ConfigureAwait(false);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTemperatureRecord([FromBody] CreateTemperatureRecordCommand command)
    {
        var result = await Mediator.Send(command).ConfigureAwait(false);
        return Ok(result);
    }
}
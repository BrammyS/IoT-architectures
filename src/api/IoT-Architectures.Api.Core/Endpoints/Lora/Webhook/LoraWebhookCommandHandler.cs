using System.Text.Json;
using IoT_Architectures.Api.Core.Endpoints.TemperatureRecords;
using Mediator;
using Microsoft.Extensions.Logging;

namespace IoT_Architectures.Api.Core.Endpoints.Lora.Webhook;

public class LoraWebhookCommandHandler : IRequestHandler<LoraWebhookCommand>
{
    private readonly ILogger<LoraWebhookCommandHandler> _logger;
    private readonly IMediator _mediator;

    public LoraWebhookCommandHandler(ILogger<LoraWebhookCommandHandler> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async ValueTask<Unit> Handle(LoraWebhookCommand request, CancellationToken cancellationToken)
    {
        if (_logger.IsEnabled(LogLevel.Debug))
        {
            _logger.LogDebug(
                "LoRa SenML data packet received: {Data}",
                JsonSerializer.Serialize(
                    request.Data,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    }
                )
            );
        }

        _logger.LogInformation(
            "Received a lora webhook request with {Count} data points, {PackCount} packs, {RecordCount} records",
            request.Data.Count,
            request.Data.Count(x => x.IsPack()),
            request.Data.Count(x => x.IsRecord())
        );

        // Todo: Check if its a temperature reading
        var temperatureCommand = new CreateTemperatureRecordCommand(
            request.Data.First(x => !string.IsNullOrWhiteSpace(x.Name) && x.Name == "lat").Number!.Value,
            request.Data.First(x => !string.IsNullOrWhiteSpace(x.Name) && x.Name == "lon").Number!.Value,
            420,
            request.Data.First(x => x.BaseTime != null).BaseTime!.Value
        );
        await _mediator.Send(temperatureCommand, cancellationToken).ConfigureAwait(false);

        return Unit.Value;
    }
}
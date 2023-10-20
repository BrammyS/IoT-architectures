using System.Text.Json;
using Mediator;
using Microsoft.Extensions.Logging;

namespace IoT_Architectures.Api.Core.Endpoints.Lora.Webhook;

public class LoraWebhookCommandHandler : IRequestHandler<LoraWebhookCommand>
{
    private readonly ILogger<LoraWebhookCommandHandler> _logger;

    public LoraWebhookCommandHandler(ILogger<LoraWebhookCommandHandler> logger)
    {
        _logger = logger;
    }

    public ValueTask<Unit> Handle(LoraWebhookCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Received a lora webhook request with {Count} data points, {PackCount} packs, {RecordCount} records",
            request.Data.Count,
            request.Data.Count(x => x.IsPack()),
            request.Data.Count(x => x.IsRecord())
        );
        
        var jsonString = JsonSerializer.Serialize(request.Data, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        _logger.LogInformation("Data: {Data}", jsonString);

        return Unit.ValueTask;
    }
}
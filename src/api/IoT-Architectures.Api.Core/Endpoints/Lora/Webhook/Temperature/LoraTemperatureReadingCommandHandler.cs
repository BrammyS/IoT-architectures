using IoT_Architectures.Client.Persistence.Entities;
using IoT_Architectures.Client.Persistence.UnitOfWorks;
using Mediator;
using Microsoft.Extensions.Logging;

namespace IoT_Architectures.Api.Core.Endpoints.Lora.Webhook.Temperature;

public class LoraTemperatureReadingCommandHandler : IRequestHandler<LoraTemperatureReadingCommand>
{
    private readonly ILogger<LoraTemperatureReadingCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public LoraTemperatureReadingCommandHandler(ILogger<LoraTemperatureReadingCommandHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    
    public async ValueTask<Unit> Handle(LoraTemperatureReadingCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Temperature reading received");

        var dt = DateTimeOffset.FromUnixTimeSeconds((long) request.UnixTime);
        var reading = new TemperatureRecord
        {
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            Temperature = request.Temperature,
            Timestamp = dt
        };
        
        await _unitOfWork.TemperatureRecords.AddAsync(reading).ConfigureAwait(false);
        _logger.LogInformation("Temperature reading ({TemperatureRecord}) saved to database", reading.ToString());
        
        return Unit.Value;
    }
}
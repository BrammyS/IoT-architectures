using IoT_Architectures.Client.Persistence.Entities;
using IoT_Architectures.Client.Persistence.UnitOfWorks;
using Mediator;
using Microsoft.Extensions.Logging;

namespace IoT_Architectures.Api.Core.Endpoints.TemperatureRecords.Create;

public class CreateTemperatureRecordCommandHandler : IRequestHandler<CreateTemperatureRecordCommand>
{
    private readonly ILogger<CreateTemperatureRecordCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTemperatureRecordCommandHandler(ILogger<CreateTemperatureRecordCommandHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async ValueTask<Unit> Handle(CreateTemperatureRecordCommand request, CancellationToken cancellationToken)
    {
        var dt = DateTimeOffset.FromUnixTimeSeconds((long)request.UnixTime);
        var reading = new TemperatureRecord
        {
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            Temperature = request.Temperature,
            Timestamp = dt
        };

        await _unitOfWork.TemperatureRecords.AddAsync(reading).ConfigureAwait(false);
        if (_logger.IsEnabled(LogLevel.Debug))
        {
            _logger.LogInformation("Temperature reading ({TemperatureRecord}) saved to database", reading.ToString());
        }

        return Unit.Value;
    }
}